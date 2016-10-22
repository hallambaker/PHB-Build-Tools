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
using System.Security.Cryptography;
using Goedel.Cryptography;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography.Framework {


    /// <summary>
    /// RSA Key Pair
    /// </summary>
    public partial class RSAKeyPair : RSAKeyPairBase {
        /// <summary>
        /// Return the underlying .NET cryptographic provider.
        /// </summary>
        public AsymmetricAlgorithm AsymmetricAlgorithm {
            get { return _Provider; }
            }


        private RSACryptoServiceProvider _Provider;
        private RSAParameters _Parameters;



        /// <summary>
        /// Return private key parameters in PKIX structure
        /// </summary>
        public override RSAPrivateKey RSAPrivateKey {
            get { return _Parameters.RSAPrivateKey(); }  }

        /// <summary>
        /// Return public key parameters in PKIX structure
        /// </summary>
        public override RSAPublicKey RSAPublicKey {
            get { return _Parameters.RSAPublicKey(); }
            }

        /// <summary>
        /// Returns a signature provider for the key (if the private portion is available).
        /// </summary>
        public override Goedel.Cryptography.CryptoProviderSignature SignatureProvider {
            get {
                GetPrivate();
                return new CryptoProviderSignatureRSA(this);
                }
            }

        /// <summary>
        /// Returns a signature provider for the key (if the private portion is available).
        /// </summary>
        public override Goedel.Cryptography.CryptoProviderSignature VerificationProvider {
            get { return new CryptoProviderSignatureRSA(this); }
            }


        /// <summary>
        /// Returns an encryption provider for the key (if the public portion is available)
        /// </summary>
        public override Goedel.Cryptography.CryptoProviderExchange ExchangeProviderEncrypt {
            get { return new CryptoProviderExchangeRSA(this); }
            }

        /// <summary>
        /// Returns an encryption provider for the key (if the public portion is available)
        /// </summary>
        public override Goedel.Cryptography.CryptoProviderExchange ExchangeProviderDecrypt {
            get {
                GetPrivate();
                return new CryptoProviderExchangeRSA(this);
                }
            }

        /// <summary>
        /// The Windows RSA provider.
        /// </summary>
        public RSACryptoServiceProvider Provider {
            get {

                if (_Provider == null) { GetProvider(); }

                return _Provider;
                }
            }

        void GetProvider() {
            _Provider = new RSACryptoServiceProvider();
            _Provider.ImportParameters(_Parameters);
            }


        /// <summary>
        /// Find a KeyPair with the specified container fingerprint in the local key store.
        /// </summary>
        /// <param name="UDF">Fingerprint of key.</param>
        /// <returns>RSAKeyPair</returns>
        public static new RSAKeyPair FindLocal(string UDF) {
            var Provider = PlatformLocateRSAProvider(UDF);
            if (Provider == null) return null;
            return new RSAKeyPair(Provider);
            }





        /// <summary>
        /// Return a PKIX SubjectPublicKeyInfo structure for the key.
        /// </summary>

        public override SubjectPublicKeyInfo KeyInfoData {
            get { return GetKeyInfo(); }
            }

        SubjectPublicKeyInfo GetKeyInfo() {
            var RSAParameters = Provider.ExportParameters(false);
            var RSAPublicKey = RSAParameters.RSAPublicKey();
            var Result = new SubjectPublicKeyInfo(CryptoConfig.MapNameToOID("RSA"),
                        RSAPublicKey.DER());

            return Result;
            }

        /// <summary>
        /// Generate an ephemeral RSA key with the specified key size.
        /// </summary>
        /// <param name="KeySize">Size of key in multiples of 64 bits.</param>
        public RSAKeyPair(int KeySize)
            : this(KeySize, true) {
            }

        /// <summary>
        /// Generate an ephemeral RSA key with the specified key size.
        /// </summary>
        /// <param name="KeySize">Size of key in multiples of 64 bits.</param>
        /// <param name="Exportable">If true, key may be exported, otherwise machine bound.</param>
        public RSAKeyPair(int KeySize, bool Exportable) {
            var Parameters = new CspParameters();
            if (Exportable) {
                Parameters.Flags = CspProviderFlags.UseArchivableKey | CspProviderFlags.CreateEphemeralKey;
                }
            else {
                Parameters.Flags = CspProviderFlags.UseNonExportableKey | CspProviderFlags.CreateEphemeralKey;
                }
            _Provider = new RSACryptoServiceProvider(KeySize, Parameters);
            }


        /// <summary>
        /// Create a new KeyPair with the specified container fingerprint.
        /// </summary>
        /// <param name="UDF">Fingerprint of key.</param>
        public RSAKeyPair(string UDF) {
            _Provider = PlatformLocateRSAProvider(UDF);
            }


        /// <summary>
        /// Generate a KeyPair from a .NET Provider.
        /// </summary>
        /// <param name="RSACryptoServiceProvider">The platform cryptographic provider.</param>
        public RSAKeyPair(RSACryptoServiceProvider RSACryptoServiceProvider) {
            _Provider = RSACryptoServiceProvider;
            }


        /// <summary>
        /// Generate a KeyPair from a .NET set of parameters.
        /// </summary>
        /// <param name="RSAParameters">The RSA parameters.</param>
        public RSAKeyPair(RSAParameters RSAParameters) {
            _Parameters = RSAParameters;

            _Provider = new RSACryptoServiceProvider();
            _Provider.ImportParameters(RSAParameters);
            }


        /// <summary>
        /// Makes a key persistent on the local machine with the specified level of
        /// protection.
        /// </summary>
        /// <param name="KeySecurity">Key protection level to be applied.</param>
        public void Persist(KeySecurity KeySecurity) {

            if (Provider == null) throw new System.Exception("No provider set");

            var Parameters = new CspParameters();
            switch (KeySecurity) {
                case KeySecurity.Master:
                Parameters.Flags = CspProviderFlags.UseArchivableKey | CspProviderFlags.UseUserProtectedKey;
                Parameters.Flags = CspProviderFlags.NoFlags;
                break;
                case KeySecurity.Admin:
                Parameters.Flags = CspProviderFlags.UseArchivableKey | CspProviderFlags.UseUserProtectedKey;
                Parameters.Flags = CspProviderFlags.NoFlags;
                break;
                case KeySecurity.Device:
                Parameters.Flags = CspProviderFlags.UseNonExportableKey;
                break;
                case KeySecurity.Ephemeral:
                Parameters.Flags = CspProviderFlags.UseNonExportableKey;
                break;
                }

            Parameters.KeyContainerName = Container.Name(UDF);

            var NewProvider = new RSACryptoServiceProvider(Parameters);
            var KeyParams = Provider.ExportParameters(true);

            NewProvider.ImportParameters(KeyParams);
            Provider.Dispose();
            _Provider = NewProvider;

            if (KeySecurity == KeySecurity.Master) {
                KeyParams = Provider.ExportParameters(true);
                }
            }

        /// <summary>
        /// Locate a key stored in the platform cryptographic key store.
        /// </summary>
        /// <param name="UDF"></param>
        /// <returns>cryptographic provider matching the specified fingerprint</returns>
        static RSACryptoServiceProvider PlatformLocateRSAProvider(string UDF) {
            var Parameters = new CspParameters();
            Parameters.KeyContainerName = Container.Name(UDF);
            return new RSACryptoServiceProvider(Parameters);
            }

        /// <summary>
        /// Retrieve the private key from local storage.
        /// </summary>
        public override void GetPrivate() {
            if (Provider.PublicOnly == false) {
                return;
                }
            //Goedel.Debug.Trace.WriteLine("Get Private for {0}", UDF);

            var cp = new CspParameters();
            cp.KeyContainerName = Container.Name(UDF);

            _Provider = new RSACryptoServiceProvider(cp);

            }
        }

    }
