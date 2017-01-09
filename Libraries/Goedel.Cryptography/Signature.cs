﻿//   Copyright © 2015 by Comodo Group Inc.
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

    /// <summary>
    /// Crypto provider for digital signature algorithms.
    /// 
    /// The chief reason this is necessary is the execrable nature of the .NET APIs
    /// in which the base class does not expose methods such as sign.
    /// </summary>
    public abstract class CryptoProviderSignature : CryptoProviderAsymmetric {

        /// <summary>The crypto algorithm class.</summary>
        protected static CryptoAlgorithmClass _AlgorithmClass =
            CryptoAlgorithmClass.Signature;

        /// <summary>Return the crypto algorithm class.</summary>
        public override CryptoAlgorithmClass AlgorithmClass {
            get { return _AlgorithmClass; }
            }

        /// <summary>
        /// The default digest algorithm. This may be overridden in subclasses.
        /// for example, to make a different digest algorithm the default for
        /// a particular provider.
        /// </summary>
        public override CryptoAlgorithmID BulkAlgorithmDefault { get; set; } =
                CryptoCatalog.Default.AlgorithmDigest;


        /// <summary>
        /// Create a digest encoder that is compatible with this signature provider. The
        /// signature is not added at this stage. 
        /// </summary>
        /// <param name="Algorithm">The bulk algorithm to use. This parameter is ignored
        /// if a bulk provider is specified.</param>
        /// <param name="Bulk">The bulk provider to use. If specified, the parameters from
        /// the specified provider will be used. Otherwise a new bulk provider will 
        /// be created and returned as part of the result.</param>
        /// <param name="OutputStream"></param>
        /// <returns>Instance describing the key agreement parameters.</returns>
        public override CryptoDataEncoder MakeEncoder(
                            CryptoProviderBulk Bulk = null,
                            CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default,
                            Stream OutputStream = null
                            ) {

            var DefaultedAlgorithm = Algorithm.Default(
                BulkDefault: BulkAlgorithmDefault,
                MetaDefault: CryptoAlgorithmID);

            Bulk = Bulk ?? CryptoCatalog.Default.GetDigest(DefaultedAlgorithm);

            var Encoder = Bulk.MakeEncoder (Algorithm: DefaultedAlgorithm.Bulk());

            return Encoder;
            }

        /*
         * Convenience methods 
         */

        /// <summary>
        /// Create an encoder with a signature data entry for this provider.
        /// </summary>
        /// <param name="Digest"></param>
        /// <returns></returns>
        public CryptoDataSignature MakeSigner(CryptoAlgorithmID Digest = CryptoAlgorithmID.Default) {
            var Encoder = MakeEncoder(Algorithm: Digest);
            return new CryptoDataSignature(CryptoAlgorithmID, Encoder, this);
            }

        /// <summary>
        /// Sign data using the default digest (requires private key).
        /// </summary>
        /// <param name="Data">Data to be signed.</param>
        /// <param name="Digest">Digest algorithm identifier</param>
        /// <returns>Signature.</returns>
        public CryptoDataSignature Sign(byte[] Data, CryptoAlgorithmID Digest= CryptoAlgorithmID.Default) {

            var Encoder = MakeEncoder(Algorithm:Digest);
            var Signer = new CryptoDataSignature(CryptoAlgorithmID, Encoder, this);
            Encoder.Write(Data);
            Encoder.Complete();
            return Signer;
            }

        ///// <summary>
        ///// Sign data using the default digest (requires private key).
        ///// </summary>
        ///// <param name="Data">Data to be signed.</param>
        ///// <param name="Digest">Digest algorithm identifier</param>
        ///// <returns>Signature.</returns>
        //public bool Verify (byte[] Data, CryptoAlgorithmID Digest = CryptoAlgorithmID.Default) {

        //    var Encoder = MakeDecoder(Algorithm: Digest);
        //    Encoder.InputStream.Write(Data, 0, Data.Length);
        //    Encoder.Complete();
        //    return Encoder.Verify;
        //    }


        /// <summary>
        /// Complete processing at the end of an encoding or decoding operation
        /// </summary>
        /// <param name="CryptoData">Structure to write result to</param>
        public override void Complete(CryptoData CryptoData) {
            var CryptoDataSignature = CryptoData as CryptoDataSignature;
            var Bulk = CryptoDataSignature?.BulkData;

            if (Bulk as CryptoDataEncoder != null) {
                Sign(CryptoDataSignature);
                }
            //if (Bulk as CryptoDataDecoder != null) {
            //    Verify(CryptoDataSignature);
            //    }
            }

        /// <summary>
        /// Sign text(requires private key).
        /// </summary>
        /// <param name="Text">Text to be converted to UTF8 and signed.</param>
        /// <param name="Digest">Digest algorithm identifier</param>
        /// <returns>Signature.</returns>
        public CryptoData Sign(string Text, 
                    CryptoAlgorithmID Digest = CryptoAlgorithmID.Default) {
            return Sign(Encoding.UTF8.GetBytes(Text), Digest);
            }

        /* 
        * Methods that MUST be implemented in instance classes.
        */

        /// <summary>
        /// Sign the integrity value specified in the CryptoDataEncoder
        /// </summary>
        /// <param name="Data"></param>
        public abstract void Sign(CryptoDataSignature Data);

        /// <summary>
        /// Verify the signature value
        /// </summary>
        /// <param name="Bulk">The provider to wrap.</param>
        /// <param name="Signature">The signature blob value.</param>
        /// <param name="AlgorithmID">The algorithm used.</param>
        /// <returns>True if the verification operation succeeded, otherwise false</returns>
        public abstract bool Verify(CryptoData Bulk, Byte[] Signature,
                CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default);

        }


    }
