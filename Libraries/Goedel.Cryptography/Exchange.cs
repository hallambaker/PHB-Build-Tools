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
    /// Base provider for public key encryption and symmetric key wrap.
    /// 
    /// NB these classes do not support bulk encryption.
    /// </summary>
    public abstract class CryptoProviderExchange : CryptoProviderAsymmetric {
        /// <summary>
        /// The type of algorithm
        /// </summary>
        public override CryptoAlgorithmClass AlgorithmClass { get { return CryptoAlgorithmClass.Exchange; } }


        /// <summary>
        /// Encrypt key data.
        /// </summary>
        /// <param name="Input">The key data to encrypt.</param>
        /// <returns>Encrypted data</returns>
        public abstract byte[] Encrypt(byte[] Input);

        /// <summary>
        /// Encrypt key data.
        /// </summary>
        /// <param name="Input">The key data to encrypt.</param>
        /// <returns>Encrypted data</returns>
        public CryptoData Encrypt(CryptoData Input) {
            var Result = Encrypt(Input.Key);
            return new CryptoData(CryptoAlgorithmID, OID, null, Result, null, null);
            }


        /// <summary>
        /// JSON Key use.
        /// </summary>
        public override string JSONKeyUse { get { return "enc"; } }


        /// <summary>
        /// Decrypt data. Note that this is only possibly when the corresponding private
        /// key is available on the local machine.
        /// </summary>
        /// <param name="Input">The data to decrypt.</param>
        /// <returns>Decrypted data.</returns>
        public abstract byte[] Decrypt(byte[] Input);


        /// <summary>
        /// Decrypt data. Note that this is only possibly when the corresponding private
        /// key is available on the local machine.
        /// </summary>
        /// <param name="Input">The data to decrypt.</param>
        /// <returns>Decrypted data.</returns>
        public CryptoData Decrypt(CryptoData Input) {
            var Result = Decrypt(Input.Data);
            return new CryptoData(CryptoAlgorithmID, OID, null, null, Result, null);
            }



        }

    }