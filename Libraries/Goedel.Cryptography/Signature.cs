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


namespace Goedel.Cryptography {

    /// <summary>
    /// Crypto provider for digital signature algorithms.
    /// 
    /// The chief reason this is necessary is the excrable nature of the .NET APIs
    /// in which the base class does not expose methods such as sign.
    /// </summary>
    public abstract class CryptoProviderSignature : CryptoProviderAsymmetric {
        /// <summary>
        /// The type of algorithm
        /// </summary>
        public override CryptoAlgorithmClass AlgorithmClass { get { return CryptoAlgorithmClass.Signature; } }

        /// <summary>
        /// The digest algorithm.
        /// </summary>
        public CryptoAlgorithmID DigestAlgorithm {
            get { return _DigestAlgorithm; }
            set { _DigestAlgorithm = value; }
            }
        private CryptoAlgorithmID _DigestAlgorithm;

        /// <summary>
        /// Return a provider for the current digest algorithm.
        /// </summary>
        /// <returns>Digest provider</returns>
        public CryptoProviderDigest GetDigestProvider() {
            return CryptoCatalog.Default.GetDigest(DigestAlgorithm);
            }

        
        /// <summary>
        /// Sign a previously computed digest (requires private key).
        /// </summary>
        /// <param name="Data">Computed digest</param>
        /// <returns>Signature</returns>
        public abstract CryptoData Sign(CryptoData Data);


        /// <summary>
        /// Sign data using the default digest (requires private key).
        /// </summary>
        /// <param name="Data">Data to be signed.</param>
        /// <returns>Signature.</returns>
        public CryptoData Sign(byte[] Data) {
            var DigestProvider = GetDigestProvider();
            var DigestResult = DigestProvider.Process(Data);
            return Sign(DigestResult);
            }

        /// <summary>
        /// Verify signature.
        /// </summary>
        /// <param name="Data">Computed digest</param>
        /// <param name="Signature">Signature</param>
        /// <returns>True if signature verification is successful, otherwise false.</returns>
        public abstract bool Verify(CryptoData Data, byte[] Signature);

        /// <summary>
        /// Verify signature.
        /// </summary>
        /// <param name="Data">Computed digest</param>
        /// <param name="Signature">Signature</param>
        /// <returns>True if signature verification is successful, otherwise false.</returns>
        public virtual bool Verify(CryptoData Data, CryptoData Signature) {
            return Verify(Data, Signature.Integrity);
            }

        /// <summary>
        /// Verify signature.
        /// </summary>
        /// <param name="Data">Computed digest</param>
        /// <param name="Signature">Signature</param>
        /// <returns>True if signature verification is successful, otherwise false.</returns>
        public virtual bool Verify(byte[] Data, byte[] Signature) {
            var DigestProvider = GetDigestProvider();
            var DigestResult = DigestProvider.Process(Data);
            return Verify(DigestResult, Signature);
            }


        /// <summary>
        /// JSON Key use.
        /// </summary>
        public override string JSONKeyUse { get { return "sig"; } }


        }


    }
