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



        /// <summary>
        /// Construct a JWE instance from a CryptoData object
        /// </summary>
        /// <param name="Data">Key information to construct the bulk and optionally
        /// key exchange headers</param>
        /// <param name="ContentType">The type of content being encrypted.</param>
        /// <param name="SigningKey">Optional signing key.</param>
        public JoseWebEncryption    (CryptoData Data,
                    string ContentType = null,
                    KeyPair SigningKey = null
                    ) {

            BindCryptoData(Data, ContentType);
            }


        /// <summary>
        /// Construct a JWE instance from binary data. 
        /// </summary>
        /// <param name="Data">Key information to construct the bulk and optionally
        /// key exchange headers</param>
        /// <param name="ContentType">The type of content being encrypted.</param>
        /// <param name="EncryptionKey">Optional Encryption key.</param>
        /// <param name="SigningKey">Optional signing key.</param>
        /// <param name="AlgorithmID">Specify the Meta and Bulk algorithms</param>
        public JoseWebEncryption(byte[] Data,
                    KeyPair EncryptionKey = null,
                    KeyPair SigningKey = null,
                    string ContentType = null,
                    CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default
                    ) {

            var Provider = CryptoCatalog.Default.GetEncryption(AlgorithmID);
            var Encoder = Provider.MakeEncoder(Algorithm: AlgorithmID);
            _CryptoData = Encoder;

            if (EncryptionKey != null) {
                AddRecipient(EncryptionKey, AlgorithmID);
                }

            Encoder.Write(Data);
            Encoder.Complete();

            BindCryptoData(Encoder, ContentType);
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

            //var ExchangeProvider = EncryptionKey.ExchangeProvider(CryptoData, ProviderAlgorithm);
            //var ExchangeData = ExchangeProvider.MakeEncoder(CryptoData);

            var ExchangeData = EncryptionKey.EncryptKey(CryptoData, ProviderAlgorithm);

            var Recipient = new Recipient(ExchangeData);
            Recipients = Recipients ?? new List<Recipient>();
            Recipients.Add(Recipient);

            return Recipient;
            }

        /// <summary>
        /// Finish processing of the data and write out the integrity data
        /// </summary>
        public void Complete () {
            CryptoData.Complete();
            CipherText = _CryptoData.OutputData;
            JTag = _CryptoData.Integrity;

            CipherText = CryptoData.ProcessedData;


            // Sign here?
            }

        void BindCryptoData(CryptoData Data, string ContentType) {
            _CryptoData = Data;

            var enc = Data.AlgorithmIdentifier.Bulk();
            var encID = enc.ToJoseID();

            var ProtectedTBW = new Header() {
                cty = ContentType,
                enc = encID
                };

            Protected = ProtectedTBW.GetBytes();
            IV = Data.IV;
            CipherText = Data.OutputData;
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
