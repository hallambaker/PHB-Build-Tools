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
using Goedel.Cryptography;

namespace Goedel.Cryptography.Framework {


    /// <summary>
    /// Provider for RSA encryption.
    /// </summary>
    public class CryptoProviderExchangeRSA : CryptoProviderExchange {


        RSAKeyPair _RSAKeyPair;

        /// <summary>
        /// Return the key as a RSAKeyPair;
        /// </summary>
        public override Goedel.Cryptography.KeyPair KeyPair {
            get { return _RSAKeyPair; }
            set { _RSAKeyPair = value as RSAKeyPair; }
            }

        /// <summary>
        /// The wrapped provider class.
        /// </summary>
        RSACryptoServiceProvider Provider {
            get { return _RSAKeyPair.Provider; }
            }

        /// <summary>
        /// The default key size
        /// </summary>
        
        public int KeySize {
            get { return _KeySize; }
            set { _KeySize = value; }
            }
        private int _KeySize;

        /// <summary>
        /// The key fingerprint.
        /// </summary>
        public override string UDF {
            get {
                if (KeyPair == null) return null;
                return KeyPair.UDF;
                }
            }

        /// <summary>
        /// If true (default), OAEP padding will be used. If false, deprecated PKCS#1.5 
        /// padding is used.
        /// </summary>
        protected bool OAEP;

        /// <summary>
        /// Return a provider with the specified key size.
        /// </summary>
        /// <param name="KeySize">Key length in bits.</param>
        public CryptoProviderExchangeRSA(int KeySize) {
            this.KeySize = KeySize;
            this.OAEP = true;
            }

        /// <summary>
        /// Create an instance of the RSA crypto provider.
        /// </summary>
        /// <param name="RSAKeyPair">RSAKeyPair to use.</param>
        public CryptoProviderExchangeRSA(RSAKeyPair RSAKeyPair) {
            this.KeyPair = RSAKeyPair;
            this.OAEP = true;
            }

        /// <summary>
        /// Returns the default crypto provider.
        /// </summary>
        public override GetCryptoProvider GetCryptoProvider {
            get {
                return Factory;
                }
            }

        private static CryptoProvider Factory(int KeySize, CryptoAlgorithmID DigestAlgorithm) {
            return new CryptoProviderExchangeRSA(KeySize);
            }

        /// <summary>
        /// The CryptoAlgorithmID Identifier.
        /// </summary>
        public override CryptoAlgorithmID CryptoAlgorithmID {
            get {
                if (KeySize == 2048) {
                    return CryptoAlgorithmID.RSAExch2048;
                    }
                return CryptoAlgorithmID.RSAExch4096;
                }
            }
        /// <summary>
        /// .NET Framework name
        /// </summary>
        public override string Name {
            get {
                return "RSA";
                }
            }

        /// <summary>
        /// ASN.1 Object Identifier.
        /// </summary>
        public override string OID {
            get {
                return CryptoConfig.MapNameToOID(Name);
                }
            }
 


        /// <summary>
        /// JSON Algorithm Name
        /// </summary>
        public override string JSONName {
            get {
                return "RSAES"; // NYI placeholder for now
                }
            }
        /// <summary>
        /// Default algorithm key size.
        /// </summary>
        public override int Size {
            get {
                return KeySize;
                }
            }

        /// <summary>
        /// Generate a new RSA Key Pair with the Key size specified when the 
        /// instance was created.
        /// </summary>
        /// <param name="KeySecurity">The key security mode</param>
        public override void Generate(KeySecurity KeySecurity) {
        _RSAKeyPair = new RSAKeyPair(KeySize);
        _RSAKeyPair.Persist(KeySecurity);
            }

        /// <summary>
        /// Locate private key in local key store.
        /// </summary>
        /// <param name="UDF">Fingerprint of key</param>
        /// <returns>true if found, otherwise false.</returns>
        public override bool FindLocal(string UDF) {
            _RSAKeyPair = new RSAKeyPair(UDF);
            return _RSAKeyPair.Provider != null;
            }

        /// <summary>
        /// Encrypt data block. Block MUST be smaller than the key length or
        /// an exception will be thrown.
        /// </summary>
        /// <param name="Input">Data to encrypt.</param>
        /// <returns>Encrypted data.</returns>
        public override byte[] Encrypt(byte[] Input) {
            return Provider.Encrypt(Input, OAEP);
            }

        /// <summary>
        /// Decrypt data block.
        /// </summary>
        /// <param name="Input">Data to decrypt.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] Input) {
            return Provider.Decrypt(Input, OAEP);
            }



        /// <summary>
        /// JSON Key type.
        /// </summary>
        public override string JSONKeyType { get { return "rsa"; } }


        }


    /// <summary>
    /// Deprecated implementation of RSA. For compatibility only.
    /// </summary>
    public class CryptoProviderExchangeRSAPKCS : CryptoProviderExchangeRSA {
        /// <summary>
        /// The CryptoAlgorithmID Identifier.
        /// </summary>
        public override CryptoAlgorithmID CryptoAlgorithmID {
            get {
                if (KeySize == 2048) {
                    return CryptoAlgorithmID.RSAExch2048_P15;
                    }
                return CryptoAlgorithmID.RSAExch4096_P15;
                }
            }


        /// <summary>
        /// RSA provider that defaults to the PKCS#1.5 padding. For compatibility use only.
        /// </summary>
        /// <param name="KeySize">The key size in bits. Note that implementations are only
        /// required to support 2048 and 4096 bits.</param>
        public CryptoProviderExchangeRSAPKCS(int KeySize) : base(KeySize) {
            this.OAEP = false;
            }

        /// <summary>
        /// Returns a delegate that creates an instance of this class.
        /// </summary>
        public override GetCryptoProvider GetCryptoProvider {
            get {
                return Factory;
                }
            }

        private static CryptoProvider Factory(int KeySize, CryptoAlgorithmID Ignore) {
            return new CryptoProviderExchangeRSAPKCS(KeySize);
            }
        }


    }