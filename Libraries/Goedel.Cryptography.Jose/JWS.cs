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
using System.Text;
using Goedel.Cryptography;
using Goedel.Utilities;

namespace Goedel.Cryptography.Jose {


    public partial class JoseWebSignature {

        /// <summary>
        /// Construct a JWS instance from a CryptoData Result.
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="ContentType"></param>
        public JoseWebSignature(CryptoData Data,
            string ContentType = null
            ) {

            var ProtectedTBW = new Header() {
                cty = ContentType,
                alg = Data.AlgorithmIdentifier.Meta().ToJoseID(),
                enc = Data.AlgorithmIdentifier.Bulk().ToJoseID()
                };
            //var RecipientHeader = new Header() {
            //    kid = Data.Meta?.UDF
            //    };
            //var Recipient = new Recipient() {
            //    Header = RecipientHeader,
            //    EncryptedKey = Data.Exchange
            //    };
            }

        /// <summary>
        /// Sign binary data.
        /// </summary>
        /// <param name="Data">The data to sign</param>
        /// <param name="SigningKey">The signature key</param>
        /// <param name="ContentType">Optional IANA content type identifier. 
        /// Omitted if null</param>
        /// <param name="Algorithm">The signature and encryption algorithm</param>
        public JoseWebSignature (byte [] Data, 
                    KeyPair SigningKey = null, 
                    string ContentType = null, 
                    CryptoAlgorithmID Algorithm=CryptoAlgorithmID.Default) {
            //if (SigningKey != null) {
            //    var Signer = SigningKey.SignatureProvider(Algorithm);
            //    _CryptoData = Signer.Sign(Data);
            //    }
            //else {

            //    }

            var Encoder = CryptoCatalog.Default.GetDigest(Algorithm);
            _CryptoData = Encoder.Process(Data);
            Payload = Data;

            if (SigningKey != null) {
                AddSignature(SigningKey, Algorithm);
                }

            Bind(_CryptoData);
            }

        /// <summary>
        /// Sign text as UTF8 encoding.
        /// </summary>
        /// <param name="Text">The text to sign</param>
        /// <param name="SigningKey">The signature key</param>
        /// <param name="ContentType">Optional IANA content type identifier. 
        /// Omitted if null</param>
        /// <param name="Algorithm">The signature and encryption algorithm</param>
        public JoseWebSignature(
                    string Text, 
                    KeyPair SigningKey=null,
                    string ContentType = null, 
                    CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default) : 
                this (System.Text.Encoding.UTF8.GetBytes(Text),
                        SigningKey, ContentType, Algorithm) { }


        /// <summary>
        /// Add a signature to an existing data item.
        /// </summary>
        /// <param name="SignerKey">The signing key</param>
        /// <param name="ContentType">The content type</param>
        /// <param name="ProviderAlgorithm">The provider algorithm</param>
        /// <returns>The signature instance.</returns>
        public Signature AddSignature(KeyPair SignerKey,
                CryptoAlgorithmID ProviderAlgorithm = CryptoAlgorithmID.Default, 
                string ContentType = null) {

            Signatures = Signatures ?? new List<Signature>();

            var ProtectedTBE = new Header() {
                cty = ContentType,
                val = CryptoData.Integrity
                };
            var Protected = ProtectedTBE.ToJson();
            var SignatureData = SignerKey.Sign(Protected);

            var Signature = new Signature() {
                Protected = Protected,
                SignatureValue = SignatureData.Signature
                };
            Signatures.Add(Signature);
            return Signature;
            }

        ///// <summary>
        ///// Add a signature instance
        ///// </summary>
        ///// <param name="CryptoData"></param>

        ///// <returns></returns>
        //public Signature AddSignature(CryptoData CryptoData, string ContentType = null) {



        //    //var RecipientHeader = new Header() {
        //    //    kid = Data.Meta?.UDF
        //    //    };
        //    //var Recipient = new Recipient() {
        //    //    Header = RecipientHeader,
        //    //    EncryptedKey = Data.Exchange
        //    //    };
        //    return null;
        //    }


        /// <summary>
        /// Recalculate the CryptoData object parameters. This causes 
        /// </summary>
        /// <returns></returns>
        public CryptoData GetCryptoData () {
            return _CryptoData;
            }

        /// <summary>Caches the CryptoData instance</summary>
        protected CryptoData _CryptoData = null;

        /// <summary>
        /// Call GetCryptoData and return the result, unless GetCryptoData has been
        /// called previously on this instance in which case return the last result. 
        /// </summary>
        public CryptoData CryptoData {
            get {
                _CryptoData = _CryptoData ?? GetCryptoData();
                return _CryptoData;
                }
            }

        private void Bind (CryptoData Data,
                string ContentType = null
                ) {
            Unprotected =  new Header() {
                dig = Data.AlgorithmIdentifier.Digest().ToJoseID()
                };
            }

        ///// <summary>
        ///// Parse the binary signature header
        ///// </summary>
        //public Header ParsedHeader {
        //    get {
        //        _ParsedHeader = _ParsedHeader ?? Cryptography.Jose.Header.From(Protected);
        //        return _ParsedHeader;
        //        }
        //    }
        //Header _ParsedHeader = null;

        /// <summary>
        /// Verify the specified signature.
        /// </summary>
        /// <param name="UDF">The UDF of the purported signature verification key.</param>
        /// <param name="Public">The public signature verification key.</param>
        /// <returns>True if verification succeeds, otherwise false.</returns>
        public bool Verify(string UDF, KeyPair Public) {
            Assert.True(UDF == Public.UDF, FingerprintMatchFailed.Throw);
            return Verify(Public);
            }

        /// <summary>
        /// Verify the specified signature.
        /// </summary>
        /// <param name="Public">The public signature verification key.</param>
        /// <returns>True if verification succeeds, otherwise false.</returns>
        public bool Verify(KeyPair Public) {
            //var Verifier = Public.SignatureProvider(
            //        BulkAlgorithm, ProviderAlgorithm);
            throw new NYI("To do");

            //var Decoder = Verifier.BindDecoder(null);
            //Decoder.Bulk.Process(Payload);
            //Decoder.Complete();
            //return Decoder.Verified == true;
            }


        //// Replace with a dictionary in some JSON catalog.
        //CryptoAlgorithmID BulkAlgorithm {
        //    get { return CryptoAlgorithmID.SHA_2_512; }
        //    }
        //CryptoAlgorithmID ProviderAlgorithm {
        //    get { return CryptoAlgorithmID.RSASign; }
        //    }
        }


    }
