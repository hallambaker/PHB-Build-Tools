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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;

namespace Goedel.Cryptography {

    // Having realized that all crypto applications invariably end up with nested
    // systems of crypto algorithm marshalling code, here is yet another set of 
    // wrapper classes.

    //

    /// <summary>
    /// <para>
    /// Numeric identifiers for Cryptographic Algorithms and suites. The identifier 
    /// space is divided up into bulk and key exchange identifiers as follows:</para>
    /// <para>0x00000000-0x0000FFFF: Bulk algorithms (digest, encryption, etc)</para>
    /// <para>0x00010000-0x7FFF0000: Asymmetric algorithms and Key Wrap.</para>
    /// </summary>
    public enum CryptoAlgorithmID {
        ///<summary>Unknown/unsupported</summary>
        Unknown = -2,

        ///<summary>Null algorithm</summary>
        NULL = -1,

        ///<summary>Default algorithm</summary>
        Default = 0,


        /// <summary>Multiplier for Bulk algorithms</summary>
        Bulk = 0x1,

        /// <summary>Mask for Bulk Algorithms</summary>
        BulkMask = 0xFFFF,


        /// <summary>Flag multiplier</summary>
        Digest = Bulk * 0x100,

        /// <summary>Flag multiplier</summary>
        MAC = Bulk * 0x200,

        /// <summary>Flag multiplier</summary>
        Encryption = Bulk * 0x300,

        /// <summary>Flag multiplier</summary>
        MaxDigest = Bulk * 0x1FF,

        /// <summary>Flag multiplier</summary>
        MaxMAC = Bulk * 0x2FF,

        /// <summary>Flag multiplier</summary>
        MaxEncryption = Bulk * 0x8FF,


        /// <summary>Extract the base algorithm version</summary>
        BaseMask = 0x7FF8FFF8,


        /// <summary>Multiplier for key management operations</summary>
        Meta = 0x10000,

        /// <summary>Mask for key management operations</summary>
        MetaMask = 0x7fff0000,

        /// <summary>Index for signature operations</summary>
        Signature = Meta * 0x100,

        /// <summary>Index for key exchange operations</summary>
        Exchange = Meta * 0x200,

        /// <summary>Index for key wrap operations</summary>
        Wrap = Meta * 0x300,


        /// <summary>Max index for signature operations</summary>
        MaxSignature = Meta * 0x1FF,

        /// <summary>Max index for key exchange operations</summary>
        MaxExchange = Meta * 0x2FF,

        /// <summary>Max index for key wrap operations</summary>
        MaxWrap = Meta * 0x3FF,



        // Bulk algorithms

        /// <summary>SHA1 (Highly deprecated but often necessary)</summary>
        SHA_1_DEPRECATED = 1,

        /// <summary>SHA2 256 bit</summary>
        SHA_2_256 = Digest + 2,

        /// <summary>SHA2 512 bit</summary>
        SHA_2_512 = Digest + 3,

        /// <summary>SHA3 256 bit</summary>
        SHA_3_256 = Digest + 4,

        /// <summary>SHA3 512 bit</summary>
        SHA_3_512 = Digest + 5,


        /// <summary>Flag for CBC mode</summary>
        ModeCBC = 1,

        /// <summary>Flag for Cipher Text Stealing Mode</summary>
        ModeCTS = 2,

        /// <summary>Flag for Galois Counter Mode</summary>
        ModeGCM = 3,

        /// <summary>Flag for HMAC SHA-2 Mode</summary>
        ModeHMAC = 4,


        /// <summary>AES 128 bit key</summary>
        AES128 = Encryption,

        /// <summary>AES 256 bit key</summary>
        AES256 = Encryption+8,


        /// <summary>AES 128 bit in CBC mode</summary>
        AES128CBC = AES128 + ModeCBC,

        /// <summary>AES 128 bit in GCM Mode</summary>
        AES128GCM = AES128 + ModeGCM,

        /// <summary>AES 128 bit in Cipher Text Stealing (CTS) mode</summary>
        AES128CTS = AES128 + ModeCTS,

        /// <summary>AES 128 bit with HMAC</summary>
        AES128HMAC = AES128 + ModeHMAC,


        /// <summary>AES 256 bit in CBC mode</summary>
        AES256CBC = AES256 + ModeCBC,

        /// <summary>AES 256 bit in GCM mode</summary>
        AES256GCM = AES256 + ModeGCM,

        /// <summary>AES 256 bit in Cipher Text Stealing (CTS) mode</summary>
        AES256CTS = AES256 + ModeCTS,

        /// <summary>AES 256 Bit with HMAC</summary>
        AES256HMAC = AES256 + ModeHMAC,


        // HMAC Modes

        /// <summary>HMAC SHA 2 with 256 bit key.</summary>
        HMAC_SHA_2_256 = 12 ,

        /// <summary>HMAC SHA 2 with 512 bit key.</summary>
        HMAC_SHA_2_512 = 13,



        /// <summary>Flag for CBC mode</summary>
        Level_Any = 0,

        /// <summary>Flag for Cipher Text Stealing Mode</summary>
        Level_Low = Meta * 1,

        /// <summary>Flag for Galois Counter Mode</summary>
        Level_High = Meta * 2,



        // Public Key Signature


        /// <summary>RSA Signature using PKCS#1.5 padding.</summary>
        RSASign = Signature,


        /// <summary>RSA Signature using PSS padding.</summary>
        RSASign_PSS = Exchange + Meta,


        /// <summary>Elliptic Curve DSA with curve 25519x</summary>
        EDDSA = Signature + Meta * 8,


        // Public Key Exchange

        /// <summary>RSA Encryption using OAEP padding.</summary>
        RSAExch = Exchange,

        /// <summary>RSA Encryption using PKCS#1.5 padding</summary>
        RSAExch_P15 = Exchange + Meta,

        /// <summary>Elliptic Curve DH with curve P256</summary>
        ECDH = Exchange + Meta * 8,

        ///<summary>Diffie Hellman 2048 bit</summary>
        DH = Exchange + Meta * 16,


        // Key Wrap

        ///<summary>Direct (no wrap)</summary>
        Direct = Wrap + Meta * 1,

        ///<summary>RFC 3394 / NIST with AES and 128 bit key</summary>
        AES128_KW = Wrap + Meta * 2,

        ///<summary>RFC 3394 / NIST with AES and 128 bit key</summary>
        AES256_KW = Wrap + Meta * 3,

        ///<summary>RFC 3394 / NIST with AES and 128 bit key</summary>
        AES128_GCM_KW = Wrap + Meta * 4,

        ///<summary>RFC 3394 / NIST with AES and 128 bit key</summary>
        AES256_GCM_KW = Wrap + Meta * 5,
        }

    /// <summary>
    /// Extension methods to extract algorithm sub types
    /// </summary>
    public static class CryptoID {


        /// <summary>
        /// Return the MAC algorithm from a possibly composite ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>The digest algorithm</returns>
        static CryptoAlgorithmID ExtractMAC (this CryptoAlgorithmID ID) {

            switch (ID.Bulk()) {
                case CryptoAlgorithmID.AES128HMAC: return CryptoAlgorithmID.HMAC_SHA_2_256;
                case CryptoAlgorithmID.AES256HMAC: return CryptoAlgorithmID.HMAC_SHA_2_512;
                }
            return ID.Bulk();
            }

        /// <summary>
        /// Return the encryption algorithm from a possibly composite ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>The encryption algorithm</returns>
        static CryptoAlgorithmID ExtractEncryption(this CryptoAlgorithmID ID) {

            switch (ID.Bulk()) {
                case CryptoAlgorithmID.AES128HMAC: return CryptoAlgorithmID.AES128CBC;
                case CryptoAlgorithmID.AES256HMAC: return CryptoAlgorithmID.AES256CBC;
                }
            return ID.Bulk();
            }


        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Bulk (this CryptoAlgorithmID ID) {
            return ID < 0? ID :ID & CryptoAlgorithmID.BulkMask;
            }

        /// <summary>
        /// Get the Meta algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The meta component.</returns>
        public static CryptoAlgorithmID Meta(this CryptoAlgorithmID ID) {
            return ID < 0 ? ID : ID & CryptoAlgorithmID.MetaMask;
            }

        /// <summary>
        /// Set algorithm defaults
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="BulkDefault"></param>
        /// <param name="MetaDefault"></param>
        /// <returns></returns>
        public static CryptoAlgorithmID Default(
                    this CryptoAlgorithmID ID,
                    CryptoAlgorithmID BulkDefault = CryptoAlgorithmID.Default,
                    CryptoAlgorithmID MetaDefault = CryptoAlgorithmID.Default) {
            var Meta = ID.Meta();
            var Bulk = ID.Bulk();
            Meta = Meta == CryptoAlgorithmID.Default ? MetaDefault : Meta;
            Bulk = Bulk == CryptoAlgorithmID.Default ? BulkDefault : Bulk;
            return Meta | Bulk;
            }


        /// <summary>
        /// Set algorithm defaults
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static CryptoAlgorithmID DefaultMeta(
                    this CryptoAlgorithmID ID,
                    CryptoAlgorithmID Default = CryptoAlgorithmID.Default) {
            var DefaultedID = ID.Meta();
            DefaultedID = DefaultedID == CryptoAlgorithmID.Default ? 
                Default.Bulk() : DefaultedID;
            return DefaultedID ;
            }

        /// <summary>
        /// Set algorithm defaults
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static CryptoAlgorithmID DefaultBulk(
                    this CryptoAlgorithmID ID,
                    CryptoAlgorithmID Default = CryptoAlgorithmID.Default) {
            var DefaultedID = ID.Bulk ();
            DefaultedID = DefaultedID == CryptoAlgorithmID.Default ? 
                    Default.Bulk() : DefaultedID;
            return DefaultedID;
            }


        /// <summary>
        /// Get the base part of an algorithm
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static CryptoAlgorithmID Base (this CryptoAlgorithmID ID) {
            return ID < 0 ? ID : ID & CryptoAlgorithmID.BaseMask;
            }

        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Digest(this CryptoAlgorithmID ID) {
            var Bulk = ID.Bulk();
            return Bulk >= (CryptoAlgorithmID.Digest) & (Bulk <= CryptoAlgorithmID.MaxDigest) ?
                Bulk : CryptoAlgorithmID.Default;
            }


        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID MAC(this CryptoAlgorithmID ID) {
            var Bulk = ID.ExtractMAC();
            return Bulk >= (CryptoAlgorithmID.MAC) & (Bulk <= CryptoAlgorithmID.MaxMAC) ?
                Bulk : CryptoAlgorithmID.Default;
            }


        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Encryption(this CryptoAlgorithmID ID) {
            var Bulk = ID.ExtractEncryption();
            return Bulk >= (CryptoAlgorithmID.Encryption) & (Bulk <= CryptoAlgorithmID.MaxEncryption) ?
                Bulk : CryptoAlgorithmID.Default;
            }

        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Mode(this CryptoAlgorithmID ID) {
            var Encryption = ID.Encryption();
            return (Encryption > 0 )? Encryption & ((CryptoAlgorithmID) 0x7) : Encryption;
            }


        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Signature(this CryptoAlgorithmID ID) {
            var Meta = ID.Meta();
            return Meta >= (CryptoAlgorithmID.Signature) & (Meta <= CryptoAlgorithmID.MaxSignature) ?
                Meta : CryptoAlgorithmID.Default;
            }

        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Exchange(this CryptoAlgorithmID ID) {
            var Meta = ID.Meta();
            return Meta >= (CryptoAlgorithmID.Exchange) & (Meta <= CryptoAlgorithmID.MaxExchange) ?
                Meta : CryptoAlgorithmID.Default;
            }

        /// <summary>
        /// Get the bulk algorithm
        /// </summary>
        /// <param name="ID">Composite identifier</param>
        /// <returns>The bulk component.</returns>
        public static CryptoAlgorithmID Wrap(this CryptoAlgorithmID ID) {
            var Meta = ID.Meta();
            return Meta >= (CryptoAlgorithmID.Wrap) & (Meta <= CryptoAlgorithmID.MaxWrap) ?
                Meta : CryptoAlgorithmID.Default;
            }


        }



    /// <summary>
    /// Defines levels of key protection to be applied.
    /// </summary>
    public enum KeySecurity {
        /// <summary>
        /// Key is a master key and will be stored in a key container marked 
        /// as archivable and user protected. Master keys SHOULD be deleted after 
        /// being escrowed and recovery verified.
        /// </summary>
        Master,

        /// <summary>
        /// Key is an administration key and will be  stored in a key container marked as non 
        /// exportable and user protected.
        /// </summary>
        Admin,

        /// <summary>
        /// Key is a device key and will be  stored in a key container bound to 
        /// the current machine that cannot be exported or archived.
        /// </summary>
        Device,

        /// <summary>
        /// Key is temporary and cannot be exported or stored.
        /// </summary>
        Ephemeral,

        /// <summary>
        /// Key may be exported.
        /// </summary>
        Exportable
        }

    /// <summary>
    /// Algorithm classes.
    /// </summary>
    public enum CryptoAlgorithmClass {

        /// <summary>Unspecified.</summary>
        NULL,

        /// <summary>Digest algorithm.</summary>
        Digest,

        /// <summary>Message Authentication Code</summary>
        MAC,

        /// <summary>Symmetric Encryption.</summary>
        Encryption,

        /// <summary>Digital Signature</summary>
        Signature,

        /// <summary>Asymmetric Encryption.</summary>
        Exchange
        }

 
    }
