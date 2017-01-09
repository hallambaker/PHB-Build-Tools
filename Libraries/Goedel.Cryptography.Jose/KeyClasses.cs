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
using Goedel.Cryptography;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography.Jose {

    public partial class Key {

        /// <summary>
        /// Extract a KeyPair object from the JOSE data structure.
        /// </summary>
        /// <returns>The extracted key pair</returns>
        public virtual KeyPair GetKeyPair () {

            throw new InternalError("GetKeyPair method not implemented in child class");
            }

        /// <summary>
        /// Return the public portion of the key pair.
        /// </summary>
        /// <param name="KeyPair">The key pair.</param>
        /// <returns>Public portion.</returns>
        public static Key GetPublic(KeyPair KeyPair) {
            if (KeyPair as RSAKeyPairBase != null) {
                return new PublicKeyRSA (KeyPair as RSAKeyPairBase);
                }
            if (KeyPair as DHKeyPairBase != null) {
                return new PublicKeyDH(KeyPair as DHKeyPairBase);
                }
            return null;
            }

        /// <summary>
        /// Return the private portion of the keypair.
        /// </summary>
        /// <param name="KeyPair">The key pair.</param>
        /// <returns>The private data.</returns>
        public static Key GetPrivate(KeyPair KeyPair) {
            if (KeyPair as RSAKeyPairBase != null) {
                return new PrivateKeyRSA (KeyPair as RSAKeyPairBase);
                }
            if (KeyPair as DHKeyPairBase != null) {
                return new PrivateKeyDH(KeyPair as DHKeyPairBase);
                }
            return null;
            }

        }

   }
