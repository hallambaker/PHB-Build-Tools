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
using Goedel.Cryptography.PKIX;
using Goedel.Utilities;
using Goedel.Protocol;

namespace Goedel.Cryptography.Jose {

    public partial class Key {

        /// <summary>
        /// Extract a KeyPair object from the JOSE data structure.
        /// </summary>
        /// <param name="Exportable">If true, private key parameters may be exported</param>
        /// <returns>The extracted key pair</returns>
        public virtual KeyPair GetKeyPair (bool Exportable=false) {

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

        /// <summary>
        /// Convert PKIX parameters to JSON structure.
        /// </summary>
        /// <param name="PKIXKey">The PKIX key parameters</param>
        /// <returns>The JOSE key</returns>
        public static Key Factory (IPKIXPublicKey PKIXKey) {

            if (PKIXKey as PKIXPrivateKeyRSA != null) {
                return new PrivateKeyRSA(PKIXKey as PKIXPrivateKeyRSA);
                }
            if (PKIXKey as PKIXPrivateKeyDH != null) {
                return new PrivateKeyDH(PKIXKey as PKIXPrivateKeyDH);
                }
            if (PKIXKey as PKIXPublicKeyRSA != null) {
                return new PublicKeyRSA(PKIXKey as PKIXPublicKeyRSA);
                }
            if (PKIXKey as PKIXPublicKeyDH != null) {
                return new PublicKeyDH(PKIXKey as PKIXPublicKeyDH);
                }
            return null;
            }

        /// <summary>
        /// Convert PKIX parameters to JSON structure.
        /// </summary>
        /// <param name="PKIXKey">The PKIX key parameters</param>
        /// <returns>The JOSE key</returns>
        public static Key Factory(IPKIXPrivateKey PKIXKey) {

            if (PKIXKey as PKIXPrivateKeyRSA != null) {
                return new PrivateKeyRSA(PKIXKey as PKIXPrivateKeyRSA);
                }
            if (PKIXKey as PKIXPrivateKeyDH != null) {
                return new PrivateKeyDH(PKIXKey as PKIXPrivateKeyDH);
                }
            return null;
            }


        }

    public partial class KeyContainer {

        /// <summary>
        /// The Key data
        /// </summary>
        public Key Key {
            get => Key.FromJSON(new JSONReader (KeyData.ToUTF8())); }

        string KeyText { get => KeyData.ToUTF8(); }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public KeyContainer () { }

        /// <summary>
        /// Construct a Key Container with the specified Key security level.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="KeySecurity"></param>
        public KeyContainer(Key Key, KeySecurity KeySecurity) :
                    this (Key.ToJson(true), KeySecurity){
            }


        /// <summary>
        /// Construct a Key Container with the specified Key security level.
        /// </summary>
        /// <param name="KeyData"></param>
        /// <param name="KeySecurity"></param>
        public KeyContainer (byte[] KeyData, KeySecurity KeySecurity) {
            Exportable = KeySecurity.IsExportable();
            this.KeyData = KeyData;
            }

        }

    }
