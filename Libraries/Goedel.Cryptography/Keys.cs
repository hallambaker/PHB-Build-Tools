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

namespace Goedel.Cryptography {

    /// <summary>
    /// Describes a reference to a key
    /// </summary>
    public class KeyHandle {

        string _UDF;

        /// <summary>
        /// UDF fingerprint of the key
        /// </summary>
        public string UDF {
            get { return _UDF; }
            set { _UDF = value; }
            }

        /// <summary>
        /// Construct by key fingerprint
        /// </summary>
        /// <param name="UDF">Fingerprint of the key.</param>
        public KeyHandle(string UDF) {
            }

        /// <summary>
        /// Construct by key fingerprint and use
        /// </summary>
        /// <param name="UDF">Fingerprint of the key.</param>
        /// <param name="Application">The use.</param>
        public KeyHandle(string UDF, string Application) {
            }

        /// <summary>
        /// Construct by key fingerprint, use and name of key.
        /// </summary>
        /// <param name="UDF">Fingerprint of the key.</param>
        /// <param name="Application">The use.</param>
        /// <param name="Name">The key friendly name</param>
        public KeyHandle(string UDF, string Application, string Name) {
            }

        /// <summary>
        /// Form a KeyHandle from an end entity certificate 
        /// </summary>
        /// <param name="Certificate">The end entity certificate to construct
        /// the handle from.</param>
        public KeyHandle(Certificate Certificate) {

            }

        /// <summary>
        /// Form a KeyHandle from a certificate chain.
        /// </summary>
        /// <param name="Certificates">Certificate Chain</param>
        public KeyHandle(List<Certificate> Certificates) {

            }

        Certificate _Certificate;

        /// <summary>
        /// X.509 v3 Certificate for this key and set of uses.
        /// </summary>
        public Certificate Certificate {
            get { return _Certificate; }
            set { _Certificate = value; }
            }


        List<Certificate> _CertificateChain;

        /// <summary>
        /// X.509 v3 Certificate chain validating this certificate.
        /// </summary>
        public List<Certificate> CertificateChain {
            get { return _CertificateChain; }
            set { _CertificateChain = value; }
            }

        }

    /// <summary>
    /// Base class for all cryptographic keys.
    /// </summary>
    public abstract class CryptoKey  {

        CryptoAlgorithmID _CryptoAlgorithmID;

        /// <summary>
        /// Cryptographic Algorithm Identifier
        /// </summary>
        public CryptoAlgorithmID CryptoAlgorithmID {
            get { return _CryptoAlgorithmID; }
            set { _CryptoAlgorithmID = value; }
            }


        /// <summary>
        /// UDF fingerprint of the key
        /// </summary>
        public virtual string UDF {
            get { return null; }
            }

        }


    /// <summary>
    /// Base class for all cryptographic key pairs.
    /// </summary>
    public abstract partial class KeyPair : CryptoKey {

        /// <summary>
        /// If set to true, all keys will be generated with a
        /// prefix to identify them as being for test purposes.
        /// </summary>
        protected static bool _TestMode = false;




        /// <summary>
        /// If true, keys will be created in containers prefixed with the name
        /// "test:" to allow them to be easily identified and cleaned up.
        /// </summary>
        public static bool TestMode {
            get { return _TestMode; }
            set { _TestMode = value; }
            }

        /// <summary>
        /// Returns a signature provider for the key (if the private portion is available).
        /// </summary>
        /// <param name="BulkAlgorithm">The digest algorithm to use</param>
        public abstract CryptoProviderSignature SignatureProvider(
                    CryptoAlgorithmID BulkAlgorithm = CryptoAlgorithmID.Default);

        /// <summary>
        /// Returns an encryption provider for the key (if the public portion is available)
        /// </summary>
        /// <param name="BulkAlgorithm">The encryption algorithm to use</param>
        public abstract CryptoProviderExchange ExchangeProvider(
                    CryptoAlgorithmID BulkAlgorithm = CryptoAlgorithmID.Default);

        CryptoProviderExchange CachedExchangeProvider = null;
        CryptoProviderSignature CachedSignatureProvider = null;


        /// <summary>
        /// Return the CryptoAlgorithmID that would be used with the specified base parameters.
        /// </summary>
        /// <param name="Base"></param>
        /// <returns>The computed CryptoAlgorithmID</returns>
        public virtual CryptoAlgorithmID SignatureAlgorithmID(CryptoAlgorithmID Base) {
            return Base;
            }



        /// <summary>
        /// Perform a key exchange to encrypt a bulk or wrapped key under this one.
        /// </summary>
        /// <param name="Bulk">The provider to wrap.</param>
        /// <param name="AlgorithmID">The algorithm to use.</param>
        /// <returns></returns>
        public virtual CryptoDataExchange EncryptKey(CryptoData Bulk,
                CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default) {

            CachedExchangeProvider = CachedExchangeProvider ?? ExchangeProvider(AlgorithmID);
            var Exchange = CachedExchangeProvider.Encrypt(Bulk, Wrap: true);
            Bulk.Exchanges = Bulk.Exchanges ?? new List<CryptoDataExchange>();
            Bulk.Exchanges.Add(Exchange);

            return Exchange;
            }



        /// <summary>
        /// Sign a precomputed digest
        /// </summary>
        /// <param name="Bulk">The provider to wrap.</param>
        /// <param name="AlgorithmID">The algorithm to use.</param>
        /// <returns></returns>
        public virtual CryptoData Sign(CryptoData Bulk,
                CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default) {

            CachedSignatureProvider = CachedSignatureProvider ??
                        SignatureProvider(AlgorithmID);

            throw new NYI("Fix here");
            }


        /// <summary>
        /// Sign a precomputed digest
        /// </summary>
        /// <param name="Data">The data to sign.</param>
        /// <param name="AlgorithmID">The algorithm to use.</param>
        /// <returns></returns>
        public virtual CryptoDataSignature Sign(byte [] Data,
                CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default) {

            CachedSignatureProvider = CachedSignatureProvider ??
                        SignatureProvider(AlgorithmID);

            return CachedSignatureProvider.Sign(Data);
            }


        /// <summary>
        /// Perform a key exchange to encrypt a bulk or wrapped key under this one.
        /// </summary>
        /// <param name="EncryptedKey">The encrypted session</param>
        /// <param name="AlgorithmID">The algorithm to use.</param>
        /// <returns>The decoded data instance</returns>
        public virtual byte[] Decrypt(
                    byte[]EncryptedKey,
                    CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default) {

            CachedExchangeProvider = CachedExchangeProvider ??
                ExchangeProvider(AlgorithmID);

            return CachedExchangeProvider.Decrypt(EncryptedKey, CryptoAlgorithmID);
            }




        /// <summary>
        /// Verify a precomputed digest
        /// </summary>
        /// <param name="Bulk">The provider to wrap.</param>
        /// <param name="Signature">The signature blob value.</param>
        /// <param name="AlgorithmID">The algorithm used.</param>
        /// <returns></returns>
        public virtual bool Verify(CryptoData Bulk, Byte [] Signature,
                CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default) {
            CachedSignatureProvider = CachedSignatureProvider ??
                        SignatureProvider(AlgorithmID);

            return CachedSignatureProvider.Verify(Bulk, Signature);
            }

        /// <summary>
        /// Search for the local key with the specified UDF fingerprint.
        /// </summary>
        public abstract void GetPrivate();


        /// <summary>
        /// Search all the local machine stores to find a key pair with the specified
        /// fingerprint
        /// </summary>
        /// <param name="UDF">Fingerprint of key</param>
        /// <returns>The key pair found</returns>
        public static KeyPair FindLocal(string UDF) {
            foreach (var Delegate in Platform.FindLocalDelegates) {
                var KeyPair = Delegate(UDF);
                if (KeyPair != null) {
                    return KeyPair;
                    }
                }
            return null;
            }


        /// <summary>
        /// The public key data formatted as a PKIX KeyInfo data blob.
        /// </summary>
        public abstract SubjectPublicKeyInfo KeyInfoData { get; } 

        string _UDF = null;
        /// <summary>
        /// Returns the UDF fingerprint of the current key as a string.
        /// </summary>
        public override string UDF {
            get {
                if (_UDF == null) _UDF = Goedel.Cryptography.UDF.ToString(GetUDFBytes());
                return _UDF;
                }
            }



        /// <summary>
        /// Returns the UDF fingerprint of the current key as a byte array.
        /// </summary>
        /// <returns>The UDF fingerprint value of this key.</returns>
        public byte[] GetUDFBytes() {
            return Goedel.Cryptography.UDF.FromKeyInfo(KeyInfoData.DER());
            }


        }


    /// <summary>
    /// RSA Key Pair
    /// </summary>
    public abstract class RSAKeyPairBase : KeyPair {

        /// <summary>
        /// Return private key parameters in PKIX structure
        /// </summary>
        public abstract RSAPrivateKey RSAPrivateKey { get; }

        /// <summary>
        /// Return public key parameters in PKIX structure
        /// </summary>
        public abstract RSAPublicKey RSAPublicKey { get; }
        }



    /// <summary>
    /// RSA Key Pair
    /// </summary>
    public abstract class DHKeyPairBase : KeyPair {

        /// <summary>
        /// ASN.1 Object Identifier for the domain parameters.
        /// </summary>
        /// <remarks>
        /// Since this is not standard DH, the OID is in 
        /// PHB's OID space.
        /// </remarks>
        public const string KeyOIDDomain = "1.3.6.1.4.1.35405.1.22.0";

        /// <summary>
        /// ASN.1 Object Identifier for the public key parameters.
        /// </summary>
        /// <remarks>
        /// Since this is not standard DH, the OID is in 
        /// PHB's OID space.
        /// </remarks>
        public const string KeyOIDPublic = "1.3.6.1.4.1.35405.1.22.1";

        /// <summary>
        /// ASN.1 Object Identifier for the private key parameters.
        /// </summary>
        /// <remarks>
        /// Since this is not standard DH, the OID is in 
        /// PHB's OID space.
        /// </remarks>
        public const string KeyOIDPrivate = "1.3.6.1.4.1.35405.1.22.2";



        /// <summary>
        /// Return private key parameters in PKIX structure
        /// </summary>
        public abstract DHDomain DHDomain { get; }

        /// <summary>
        /// Return private key parameters in PKIX structure
        /// </summary>
        public abstract DHPrivateKey DHPrivateKey { get; }

        /// <summary>
        /// Return public key parameters in PKIX structure
        /// </summary>
        public abstract DHPublicKey DHPublicKey { get; }
        }



    /// <summary>
    /// Base class for all public key cryptographic providers.
    /// </summary>
    public abstract class CryptoProviderAsymmetric : CryptoProvider {
        /// <summary>
        /// Generates a new signing key pair with the default key size.
        /// </summary>
        /// <param name="KeySecurity">Specifies the protection level for the key.</param>
        /// <param name="KeySize">The key size</param>
        public abstract void Generate(KeySecurity KeySecurity, int KeySize=0);

        /// <summary>
        /// Locate the private key in the local key store.
        /// </summary>
        /// <param name="UDF">Fingerprint of key to locate.</param>
        /// <returns>True if private key exists.</returns>
        public abstract bool FindLocal(string UDF);


        /// <summary>
        /// The default digest algorithm. This may be overriden in subclasses.
        /// for example, to make a different digest algorithm the default for
        /// a particular provider.
        /// </summary>
        public virtual CryptoAlgorithmID BulkAlgorithmDefault { get; set; } = CryptoAlgorithmID.Default;



        }


    }
