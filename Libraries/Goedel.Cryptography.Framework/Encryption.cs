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
using System.Security.Cryptography;
using Goedel.Utilities;

namespace Goedel.Cryptography.Framework {
    /// <summary>
    /// Provider for bulk encryption algorithms (e.g. AES).
    /// </summary>
    public abstract class CryptoProviderEncryption : 
                Goedel.Cryptography.CryptoProviderEncryption {

        /// <summary>
        /// The type of algorithm
        /// </summary>
        public override CryptoAlgorithmClass AlgorithmClass {
            get { return CryptoAlgorithmClass.Encryption; } }

        /// <summary>
        /// The .NET cryptographic provider (for use by sub classes).
        /// </summary>
        protected SymmetricAlgorithm Provider { get; set; }
        
        ICryptoTransform Transform = null;
        bool Encrypting;

        /// <summary>
        /// The size of the required key
        /// </summary>
        public override int KeySize { get { return Provider.KeySize; } }

        /// <summary>
        /// The size of the required IV. If zero, no IV is required.
        /// </summary>
        public override int IVSize { get { return Provider.IV.Length * 8; } }

        /// <summary>
        /// If set to true, the initialization vector (if used) will be prepended to the
        /// beginning of the output byte stream.
        /// </summary>
        public bool AppendIV = false;

        /// <summary>
        /// If set to true, the authentication code (if created) will be appended to the
        /// end of the output byte stream.
        /// 
        /// Since we don't currently have a GCM mode, this isn't currently used.
        /// </summary>
        public bool AppendIntegrity = false;


        /// <summary>
        /// Constructor for initializing a delegate class.
        /// </summary>
        /// <param name="SymmetricAlgorithm">Cryptographic provider.</param>
        /// <param name="KeySize">Key size in bits.</param>
        /// <param name="CipherMode">Cipher mode to use</param>
        protected CryptoProviderEncryption(SymmetricAlgorithm SymmetricAlgorithm,
                int KeySize, CipherMode CipherMode) {
            this.Provider = SymmetricAlgorithm;
            Provider.KeySize = KeySize;
            Provider.Mode = CipherMode;
            }


        /// <param name="Algorithm">The key wrap algorithm</param>
        /// <param name="Bulk">The bulk provider to use. If specified, the parameters from
        /// the specified provider will be used. Otherwise a new bulk provider will 
        /// be created and returned as part of the result.</param>
        /// <param name="OutputStream">Output stream</param>
        /// <returns>Instance describing the key agreement parameters.</returns>
        public override CryptoDataEncoder MakeEncoder(
                            CryptoProviderBulk Bulk = null,
                            CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default,
                            Stream OutputStream = null
                            ) {

            return MakeEncryptor (Algorithm, OutputStream, 
                Provider.Key, Provider.IV);
            }

        /// <summary>
        /// Create an encoder for a bulk algorithm and optional key wrap or exchange.
        /// </summary>

        /// <param name="Algorithm">The key wrap algorithm</param>
        /// <param name="OutputStream">Output stream</param>
        /// <param name="IV">Initialization vector for symmetric encryption</param>
        /// <param name="Key">Encryption Key</param>
        /// <returns>Instance describing the key agreement parameters.</returns>
        public override CryptoDataEncoder MakeEncryptor(
                            CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default,
                            Stream OutputStream = null,
                            byte[] Key = null, byte[] IV = null
                            ) {
            var Result = new CryptoDataEncoder(CryptoAlgorithmID, this);
            Result.OutputStream = OutputStream ?? new MemoryStream();
            Result.IV = IV;
            Result.Key = Key;
            BindEncoder(Result);

            return Result;
            }


        /// <summary>
        /// Create a crypto stream from this provider.
        /// </summary>
        /// <param name="Encoder"></param>
        public override void BindEncoder(CryptoDataEncoder Encoder) {
            var Transform = Provider.CreateEncryptor(Encoder.Key, Encoder.IV);
            Encoder.InputStream = new CryptoStream(
                    Encoder.OutputStream, Transform, CryptoStreamMode.Write);
            }


        /// <summary>
        /// Encrypt the specified byte array
        /// </summary>
        /// <param name="Data">The input to process</param>
        /// <param name="IV">The Initialization Vector</param>
        /// <param name="Key">The key</param>
        /// <returns>The result of the cryptographic operation.</returns>
        public override byte[] Encrypt(byte[] Data, 
                        byte[] Key = null, byte[] IV = null) {

            Key = Key ?? Provider.Key;
            IV = IV ?? Provider.IV;
            var Transform = Provider.CreateEncryptor(Key, IV);
            return Process(Data, Transform);
            }

        /// <summary>
        /// Encrypt the specified byte array
        /// </summary>
        /// <param name="Data">The input to process</param>
        /// <param name="IV">The Initialization Vector</param>
        /// <param name="Key">The key</param>
        /// <returns>The result of the cryptographic operation.</returns>
        public override byte[] Decrypt(byte[] Data,
                        byte[] Key = null, byte[] IV = null) {
            Key = Key ?? Provider.Key;
            IV = IV ?? Provider.IV;
            var Transform = Provider.CreateDecryptor(Key, IV);
            return Process(Data, Transform);
            }


        byte[] Process (byte[] Data, ICryptoTransform Transform) { 
            byte[] Result;
            using (var Output = new MemoryStream ()) {
                using (var Input = new CryptoStream(Output, Transform, CryptoStreamMode.Write)) {
                    Input.Write(Data, 0, Data.Length);
                    }
                Result = Output.ToArray();
                }
            return Result;
            }
        /// <summary>
        /// Complete processing at the end of an encoding or decoding operation
        /// </summary>
        /// <param name="CryptoData"></param>
        public override void Complete(CryptoData CryptoData) {
            (CryptoData.InputStream as CryptoStream).FlushFinalBlock();
            base.Complete(CryptoData);
            }

        }



    /// <summary>
    /// Provider for the SHA-2 256 bit Hash Algorithm
    /// </summary>
    public class CryptoProviderEncryptAES : CryptoProviderEncryption {

        /// <summary>
        /// The CryptoAlgorithmID Identifier.
        /// </summary>
        public override CryptoAlgorithmID CryptoAlgorithmID {
            get {
                return (Provider.KeySize == 128) ? 
                    Goedel.Cryptography.CryptoAlgorithmID.AES128CBC :
                    Goedel.Cryptography.CryptoAlgorithmID.AES256CBC;
                }
            }

        /// <summary>
        /// Return a CryptoAlgorithm structure with properties describing this provider.
        /// </summary>
        public override CryptoAlgorithm CryptoAlgorithm {
            get {
                return (Provider.KeySize == 128) ?
                    CryptoAlgorithm128 : CryptoAlgorithm256;
                }
            }

        static CryptoAlgorithm CryptoAlgorithmAny = new CryptoAlgorithm(
                    Goedel.Cryptography.CryptoAlgorithmID.AES256, 128,
                            _AlgorithmClass, Factory);
        static CryptoAlgorithm CryptoAlgorithm128 = new CryptoAlgorithm(
                    Goedel.Cryptography.CryptoAlgorithmID.AES128CBC, 128, 
                            _AlgorithmClass, Factory);
        static CryptoAlgorithm CryptoAlgorithm256 = new CryptoAlgorithm(
                    Goedel.Cryptography.CryptoAlgorithmID.AES256CBC, 256, 
                            _AlgorithmClass, Factory);



        /// <summary>
        /// Register this provider in the specified crypto catalog. A provider may 
        /// register itself multiple times to describe different configurations that 
        /// are supported.
        /// </summary>
        /// <param name="Catalog">The catalog to register the provider to, if
        /// null, the default catalog is used.</param>
        /// <returns>Description of the principal algorithm registration.</returns>
        public static CryptoAlgorithm Register(CryptoCatalog Catalog = null) {
            Catalog = Catalog ?? CryptoCatalog.Default;
            var Default = Catalog.Add(CryptoAlgorithm256);
            Catalog.Add(CryptoAlgorithmAny);
            Catalog.Add(CryptoAlgorithm128);
            return Default;
            }

        private static CryptoProvider Factory(int KeySize,
                            CryptoAlgorithmID Bulk = CryptoAlgorithmID.Default) {
            return new CryptoProviderEncryptAES(KeySize);
            }

        /// <summary>
        /// Default algorithm key size.
        /// </summary>
        public override int Size {
            get {
                return Provider.KeySize;
                }
            }

        /// <summary>
        /// Create an AES provider with the specified key size.
        /// </summary>
        /// <param name="KeySize">The key size. Valid sizes are 128, 192 and 256 bits.</param>
        public CryptoProviderEncryptAES(int KeySize)
            : base(new AesManaged(), KeySize, CipherMode.CBC) {
            }

        /// <summary>
        /// Create an AES provider with the specified key size and mode.
        /// </summary>
        /// <param name="KeySize">Key Size in bits.</param>
        /// <param name="CipherMode">The cipher mode to use (CBC or CTS).</param>
        public CryptoProviderEncryptAES(int KeySize, CipherMode CipherMode)
            : base(new AesManaged(), KeySize, CipherMode) {
            }
        }
    }