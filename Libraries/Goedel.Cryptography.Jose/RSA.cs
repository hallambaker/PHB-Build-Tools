using System;
using System.Collections.Generic;
using Goedel.Cryptography;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography.Jose {

    /// <summary>
    /// Represents an RSA Public Key.
    /// </summary>
    public partial class PublicKeyRSA : Key {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PublicKeyRSA () { }

        /// <summary>
        /// Construct from the specified RSA Key
        /// </summary>
        /// <param name="KeyPair">An RSA key Pair.</param>
        public PublicKeyRSA(RSAKeyPairBase KeyPair) {
            kid = KeyPair.UDF;
            var RSAPublicKey = KeyPair.PKIXPublicKeyRSA;

            n = RSAPublicKey.Modulus;
            e = RSAPublicKey.PublicExponent;
            }

        /// <summary>
        /// Construct from a PKIX RSAPublicKey structure.
        /// </summary>
        /// <param name="RSAPublicKey">RSA Public Key.</param>
        public PublicKeyRSA(PKIXPublicKeyRSA RSAPublicKey) {
            this.n = RSAPublicKey.Modulus;
            this.e = RSAPublicKey.PublicExponent;
            }


        /// <summary>
        /// Return the parameters as a PKIX RSAPublicKey structure;
        /// </summary>
        public virtual PKIXPublicKeyRSA PKIXParameters {
            get => new PKIXPublicKeyRSA() {
                Modulus = n,
                PublicExponent = e
                };
            }

        /// <summary>
        /// Extract a KeyPair object from the JOSE data structure.
        /// </summary>
        /// <param name="Exportable">If true the private key may be exported.</param>
        /// <returns>The extracted key pair</returns>
        public override KeyPair GetKeyPair(bool Exportable = false) {

            var PKIXParams = PKIXParameters;
            var KeyPair = RSAKeyPairBase.KeyPairFactory(PKIXParams);

            return KeyPair;
            }

        }

    /// <summary>
    /// Represents an RSA Private Key.
    /// </summary>
    public partial class PrivateKeyRSA {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PrivateKeyRSA () { }

        /// <summary>
        /// Construct from the specified RSA Key
        /// </summary>
        /// <param name="KeyPair">An RSA key Pair.</param>
        public PrivateKeyRSA(RSAKeyPairBase KeyPair) {
            kid = KeyPair.UDF;
            var RSAPrivateKey = KeyPair.PKIXPrivateKeyRSA;
            n = RSAPrivateKey.Modulus;
            e = RSAPrivateKey.PublicExponent;
            d = RSAPrivateKey.PrivateExponent;
            p = RSAPrivateKey.Prime1;
            q = RSAPrivateKey.Prime2;
            dp = RSAPrivateKey.Exponent1;
            dq = RSAPrivateKey.Exponent2;
            qi = RSAPrivateKey.Coefficient;
            }


        /// <summary>
        /// Construct from a PKIX RSAPublicKey structure.
        /// </summary>
        /// <param name="RSAPrivateKey">RSA Public Key.</param>
        public PrivateKeyRSA(PKIXPrivateKeyRSA RSAPrivateKey) {
            n = RSAPrivateKey.Modulus;
            e = RSAPrivateKey.PublicExponent;
            d = RSAPrivateKey.PrivateExponent;
            p = RSAPrivateKey.Prime1;
            q = RSAPrivateKey.Prime2;
            dp = RSAPrivateKey.Exponent1;
            dq = RSAPrivateKey.Exponent2;
            qi = RSAPrivateKey.Coefficient;
            }

        /// <summary>
        /// Return the parameters as PKIX RSAPrivateKey structure;
        /// </summary>
        public virtual PKIXPrivateKeyRSA RSAPrivateKey {
            get => new PKIXPrivateKeyRSA() {
                    Modulus = n,
                    PublicExponent = e,
                    PrivateExponent = d,
                    Prime1 = p,
                    Prime2 = q,
                    Exponent1 = dp,
                    Exponent2 = dq,
                    Coefficient = qi
                    };
            }

        }

    }
