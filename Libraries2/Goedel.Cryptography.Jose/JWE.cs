//   Copyright © 2015 by Comodo Group Inc.
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
//  
//  

using System.Collections.Generic;
using Goedel.Utilities;


namespace Goedel.Cryptography.Jose {

    /// <summary>
    /// Represents a JWE data structure.
    /// </summary>
    public partial class JoseWebEncryption {

        /// <summary>The signed data.</summary>
        public override byte[] Data {
            get { return CipherText; }
            set { CipherText = value; }
            }


        /// <summary>
        /// The decrypted plaintext value
        /// </summary>
        public byte[] Plaintext {
            get {
                _Plaintext = _Plaintext ?? Decrypt();
                return _Plaintext;
                }
            }
        byte[] _Plaintext = null;

        /// <summary>
        /// The decrypted plaintext as a string.
        /// </summary>
        public string UTF8 {
            get { return Plaintext.ToString(); }
            }

        /// <summary>Caches the CryptoData instance</summary>
        protected CryptoData _CryptoDataEncrypt = null;

        /// <summary>
        /// Call GetCryptoData and return the result, unless GetCryptoData has been
        /// called previously on this instance in which case return the last result. 
        /// </summary>
        public CryptoData CryptoDataEncrypt {
            get {
                _CryptoDataEncrypt = _CryptoDataEncrypt ?? GetCryptoData();
                return _CryptoDataEncrypt;
                }
            }


        /// <summary>
        /// Construct a JWE instance from binary data. 
        /// </summary>
        /// <param name="Data">Key information to construct the bulk and optionally
        /// key exchange headers</param>
        /// <param name="ContentType">The type of content being encrypted.</param>
        /// <param name="EncryptionKey">Optional Encryption key.</param>
        /// <param name="SigningKey">Optional signing key.</param>
        /// <param name="EncryptID">Composite ID for encryption and key exchange</param>
        /// <param name="SignID">Composite ID for signature and digest</param>
        public JoseWebEncryption(byte[] Data,
                    KeyPair EncryptionKey = null,
                    KeyPair SigningKey = null,
                    string ContentType = null,
                    CryptoAlgorithmID EncryptID = CryptoAlgorithmID.Default,
                    CryptoAlgorithmID SignID = CryptoAlgorithmID.Default
                    ) {

            var Provider = CryptoCatalog.Default.GetEncryption(EncryptID);
            var EncryptEncoder = Provider.MakeEncoder(Algorithm: EncryptID);
            _CryptoDataEncrypt = EncryptEncoder;

            if (EncryptionKey != null) {
                AddRecipient(EncryptionKey, EncryptID);
                }

            var DigestProvider = SigningKey != null ? CryptoCatalog.Default.GetDigest(SignID) : null;


            BindCryptoData(EncryptEncoder, ContentType, DigestProvider);

            EncryptEncoder.Write(Data);
            EncryptEncoder.Complete();

            CipherText = EncryptEncoder.OutputData;

            if (SigningKey != null) {
                
                _CryptoDataDigest = DigestProvider.Process(CipherText);
                AddSignature(SigningKey, DigestProvider.CryptoAlgorithmID);
                }

            }


        /// <summary>
        /// Construct a JWE instance from binary data. 
        /// </summary>
        /// <param name="Data">Key information to construct the bulk and optionally
        /// key exchange headers</param>
        /// <param name="ContentType">The type of content being encrypted.</param>
        /// <param name="Encrypt">Encryption key are derived.</param>
        /// <param name="Authenticate">Authentication keys are derived.</param>
        /// <param name="EncryptID">Bulk ID for encryption</param>
        /// <param name="AuthenticateID">Bulk ID for authentication</param>
        public JoseWebEncryption(byte[] Data,
                    byte[] Encrypt, byte[] Authenticate=null,
                    string ContentType = null,
                    CryptoAlgorithmID EncryptID = CryptoAlgorithmID.Default,
                    CryptoAlgorithmID AuthenticateID = CryptoAlgorithmID.Default
                    ) {

            var Provider = CryptoCatalog.Default.GetEncryption(EncryptID);
            var EncryptEncoder = Provider.MakeEncoder(Algorithm: EncryptID);
            _CryptoDataEncrypt = EncryptEncoder;
            EncryptEncoder.Key = Encrypt;

            BindCryptoData(EncryptEncoder, ContentType, null);

            EncryptEncoder.Write(Data);
            EncryptEncoder.Complete();

            CipherText = EncryptEncoder.OutputData;

            }



        /// <summary>
        /// Construct a JWE instance from binary data. 
        /// </summary>
        /// <param name="Text">Key information to construct the bulk and optionally
        /// key exchange headers</param>
        /// <param name="ContentType">The type of content being encrypted.</param>
        /// <param name="EncryptionKey">Optional Encryption key.</param>
        /// <param name="SigningKey">Optional signing key.</param>
        /// <param name="Algorithm">Specify the Meta and Bulk algorithms</param>
        public JoseWebEncryption(string Text,
                    KeyPair EncryptionKey = null,
                    KeyPair SigningKey = null,
                    string ContentType = null,
                    CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default) :
                this(System.Text.Encoding.UTF8.GetBytes(Text),
                        EncryptionKey, SigningKey, ContentType, Algorithm) { }


        /// <summary>
        /// Add a recipient to an existing JWE header.
        /// </summary>
        /// <remarks>If custom crypto suites are used, the caller is responsible for 
        /// ensuring that the exchange algorithm is compatible with the bulk algorithm 
        /// already selected. </remarks>
        /// <param name="EncryptionKey">The recipient key to add.</param>
        /// <param name="ProviderAlgorithm">Algorithm parameters (if supported)</param>
        /// <returns>The recipient instance</returns>
        public Recipient AddRecipient (KeyPair EncryptionKey,
                CryptoAlgorithmID ProviderAlgorithm = CryptoAlgorithmID.Default) {

            var ExchangeData = EncryptionKey.EncryptKey(CryptoDataEncrypt, ProviderAlgorithm);

            var Recipient = new Recipient(ExchangeData);
            Recipients = Recipients ?? new List<Recipient>();
            Recipients.Add(Recipient);

            return Recipient;
            }

        /// <summary>
        /// Finish processing of the data and write out the integrity data
        /// </summary>
        public void Complete () {
            CryptoDataEncrypt.Complete();
            CipherText = _CryptoDataEncrypt.OutputData;
            JTag = _CryptoDataEncrypt.Integrity;

            CipherText = CryptoDataEncrypt.ProcessedData;


            // Sign here?
            }

        void BindCryptoData(CryptoData Data, string ContentType, 
                    CryptoProviderDigest Digest) {
            _CryptoDataEncrypt = Data;

            var enc = Data.AlgorithmIdentifier.Bulk();
            var encID = enc.ToJoseID();

            var ProtectedTBW = new Header() {
                cty = ContentType,
                enc = encID
                };
            if (Digest != null) {
                ProtectedTBW.dig = Digest.CryptoAlgorithmID.ToJoseID();
                }

            Protected = ProtectedTBW.ToJson();
            IV = Data.IV;

            if (Digest != null) {
                Unprotected = new Header() {
                    dig = Digest.CryptoAlgorithmID.ToJoseID()
                    };
                }

            }

        /// <summary>
        /// Decrypt the content using the corresponding private key in the local 
        /// key store (if found). Throws exception otherwise.
        /// </summary>
        /// <returns>The decrypted data</returns>
        public byte[] Decrypt() {
            return Decrypt(null);
            }

        /// <summary>
        /// Decrypt the content using the specified private key.
        /// </summary>
        /// <param name="DecryptionKey">The decryption key.</param>
        /// <returns>The decrypted data</returns>
        public byte[] Decrypt(KeyPair DecryptionKey) {

            var Recipient = MatchRecipient(DecryptionKey);
            var AlgorithmJose = Recipient?.Header.alg;
            var ExchangeID = AlgorithmJose.FromJoseID();

            var ProtectedText = Protected.ToUTF8();
            var Header = new Header(ProtectedText);
            var BulkID = Header.enc.FromJoseID();

            var Exchange = DecryptionKey.Decrypt(Recipient.EncryptedKey,
                        AlgorithmID: BulkID);

            var Provider = CryptoCatalog.Default.GetEncryption(BulkID);
            //var EncryptEncoder = Provider.MakeDecryptor(Exchange, IV, Algorithm: BulkID);

            var Result = Provider.Decrypt(CipherText, Exchange, IV);

            return Result;
            }

        /// <summary>
        /// Match a recipient header by key.
        /// </summary>
        /// <param name="DecryptionKey">Key</param>
        /// <returns>The Recipient data for the specified key, if found.</returns>
        public Recipient MatchRecipient(KeyPair DecryptionKey) {
            return MatchRecipient(DecryptionKey.UDF);
            }

        /// <summary>
        /// Match a recipient header by key identifier.
        /// </summary>
        /// <param name="UDF">Key fingerprint</param>
        /// <returns>The Recipient data for the specified key, if found.</returns>
        public Recipient MatchRecipient(string UDF) {
            foreach (var Recipient in Recipients) {
                if (Recipient?.Header?.kid == UDF) {
                    return Recipient;
                    }
                }
            return null;
            }

        }

    /// <summary>
    /// Represents a JWE recipient
    /// </summary>
    public partial class Recipient {

        /// <summary>
        /// Encrypt to the specified key of the specified profile.
        /// </summary>
        /// <param name="RecipientData">KeyPair for the recipient.</param>
        public Recipient(CryptoDataExchange RecipientData) {
            var Key = RecipientData.Meta;
            Header = new Header() {
                alg = Key?.CryptoAlgorithmID.Meta().ToJoseID(),
                kid = Key?.UDF
                };
            EncryptedKey = RecipientData.Exchange;
            }

        }

    }
