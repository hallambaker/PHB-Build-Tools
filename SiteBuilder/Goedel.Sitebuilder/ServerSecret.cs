#region // Copyright - MIT License
//  © 2021 by Phill Hallam-Baker
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
#endregion


using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Goedel.Sitebuilder;


/// <summary>
/// Cookie manager, encrypts and authenticates strings of data under a shared secret 
/// that can be rotated as needed.
/// </summary>
public record ServerCookieManager {

    ServerSecret[] ServerSecrets { get; } 
    ServerSecret Latest { get; set; }
    int CurrentSlot { get; set; } = 0;
    int Lifetime { get; } = 24;


    const int NonceLength = 96/8;

    const int TagLength = 128/8;


    int PrefixLength => 1 + NonceLength + TagLength;

    /// <summary>Instance returning a new cookie manager.</summary>
    /// <param name="seed">Optional seed value, if empty, a random seed is generated.</param>

    public ServerCookieManager (string seed=null) {

        ServerSecrets = new ServerSecret[Lifetime];
        Latest = new(seed:seed);
        ServerSecrets[0] = Latest;
        }


    void Generate() {
        lock (this) {
            CurrentSlot = (CurrentSlot + 1) % Lifetime;
            Latest = new();
            ServerSecrets[CurrentSlot] = Latest;
            }
        }

    /// <summary>
    /// Return a cookie encrypted under the current server secret containing the encypted 
    /// <paramref name="identifier"/> with lifetime <paramref name="lifetime"/>/
    /// </summary>
    /// <param name="identifier">The identifier to bind to the cookie.</param>
    /// <param name="lifetime">The validity interval in hours.</param>
    /// <returns></returns>
    public byte[] GetCookie(string identifier, int lifetime = -1) {

        lifetime = lifetime <= 0 ? Lifetime : lifetime;
        var expiry = DateTime.UtcNow.AddHours(lifetime);

        using var writer = new MemoryStream ();

        var dateArray = new byte[8];
        dateArray.SetBigEndian((ulong) expiry.ToBinary());
        writer.Write (dateArray, 0, dateArray.Length);

        var idArray = identifier.ToBytes ();
        writer.Write(idArray, 0, idArray.Length);

        var buffer = writer.ToArray ();
        var nonce = Shake128.HashData(buffer, 12);


        // get the current secret, we need to acquire the lock for this
        byte[] secret;
        lock (this) {
            secret = Latest.Secret;
            }

        var provider = new AesGcm(secret, TagLength);

        var length = buffer.Length + NonceLength + TagLength + 1;

        var result = new byte[length];
        result[0] = (byte)CurrentSlot;
        Array.Copy (nonce, 0, result, 1, NonceLength);

        // perform the encryption
        var plaintextSpan = new ReadOnlySpan<byte>(buffer, 0, buffer.Length);

        var associatedDataSpan = new ReadOnlySpan<byte> (result, 0, 1);
        var nonceSpan = new ReadOnlySpan<byte> (result, 1, NonceLength);
        var tagSpan = new Span<byte>(result, 1 + NonceLength, TagLength);
        var ciphertextSpan = new Span<byte>(result, PrefixLength, buffer.Length);

        provider.Encrypt (nonceSpan, plaintextSpan,  ciphertextSpan, tagSpan, associatedDataSpan);

        return result;

        }


    /// <summary>Return a <see cref="Cookie"/> value to clear cookie <paramref name="name"/>.</summary>
    /// <param name="name">The cookie to clear.</param>
    /// <returns>The HTTP cookie.</returns>
    public static Cookie ClearCookie(
                string name) => new(name, "") {
            HttpOnly = true,
            Discard = true
            };

    /// <summary>Return a <see cref="Cookie"/> value to set the cookie <paramref name="name"/>
    /// to the value <paramref name="identifier"/>..</summary>
    /// <param name="name">The cookie to clear.</param>
    /// <param name="identifier">The cookie identifier.</param>
    /// <param name="lifetime">Cookie lifetime.</param>
    /// <returns>The HTTP cookie.</returns>
    public Cookie GetCookie(
                string name,
                string identifier, int lifetime = -1) {
        var cookieData = GetCookie(identifier, lifetime);

        var cookie = new Cookie(name, cookieData.ToStringBase64url()) {
            HttpOnly = true,
            Discard = true
            };

        return cookie;
        }


    /// <summary>Parse the cookie data <paramref name="cookie"/>.</summary>
    /// <param name="cookie">The cookie binary data.</param>
    /// <returns>The parsed value.</returns>
    public string ParseCookie(byte[] cookie) {
        ServerSecret? secret = null;
        var index = cookie[0];
        lock (this) {
            if (index < ServerSecrets.Length) {
                secret = ServerSecrets[index];
                }
            }

        // NYI: need to extend this to check for expired secret.
        secret.AssertNotNull(NYI.Throw);

        var provider = new AesGcm(secret.Secret, TagLength);

        var resultLength = cookie.Length - PrefixLength;
        var result = new byte [resultLength];

        var plaintextSpan = new Span<byte>(result, 0, result.Length);

        var associatedDataSpan = new ReadOnlySpan<byte>(cookie, 0, 1);
        var nonceSpan = new ReadOnlySpan<byte>(cookie, 1, NonceLength);
        var tagSpan = new ReadOnlySpan<byte>(cookie, 1 + NonceLength, TagLength);
        var ciphertextSpan = new Span<byte>(cookie, PrefixLength, resultLength);

        provider.Decrypt(nonceSpan, ciphertextSpan, tagSpan, plaintextSpan, associatedDataSpan);

        var trimmed = result[8..];

        return trimmed.ToUTF8();
        }

    /// <summary>Try to get a cookie <paramref name="tag"/> bound to the identifier <paramref name="id"/>
    /// in <paramref name="request"/></summary>
    /// <param name="request">The HTTP request.</param>
    /// <param name="tag">The tag value.</param>
    /// <param name="id">The identifier (if found).</param>
    /// <returns>True if found.</returns>
    public bool TryGetCookie(
                    HttpListenerRequest request, 
                    string tag,
                    [NotNullWhen(true)]out string id) {
        id = null;
        try {
            var cookie = request.Cookies[tag];
            if (cookie is null) {
                return false;
                }
            var cookieData = cookie.Value.FromBase64();
            id = ParseCookie(cookieData);
            return id != null;
            }

        catch { 
            
            return false;
            }

        }

    }


/// <summary>Manages a single server secret. In normal operation, the server
/// will have multiple secrets active with the secrets being retired on a 
/// rolling basis.</summary>
public record ServerSecret {

    /// <summary>The expiry time.</summary>
    public DateTime Expire { get; } 

    /// <summary>The secret value.</summary>
    public byte[] Secret { get; }

    /// <summary>Constructor.</summary>
    /// <param name="expiryHours">Expiry of the secret in hours.</param>
    /// <param name="seed">Optional seed value.</param>
    public ServerSecret(int expiryHours = 24, string seed=null) {
        Secret = seed == null ? Platform.GetRandomBits(128) :
            Shake128.HashData(seed.ToBytes(), 16);
        Expire = DateTime.Now.AddHours(expiryHours+1);
        }

    }
