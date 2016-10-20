using System;
using System.Collections.Generic;
using Goedel.Cryptography;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography.Jose {

    /// <summary>
    /// Represents an RSA Public Key.
    /// </summary>
    public partial class PublicKeyRSA {

        /// <summary>
        /// Construct from the spcified RSA Key
        /// </summary>
        /// <param name="KeyPair">An RSA key Pair.</param>
        public PublicKeyRSA(RSAKeyPairBase KeyPair) {
            kid = KeyPair.UDF;
            var RSAPublicKey = KeyPair.RSAPublicKey;

            n = RSAPublicKey.Modulus;
            e = RSAPublicKey.PublicExponent;
            }

        /// <summary>
        /// Construct from a PKIX RSAPublicKey structure.
        /// </summary>
        /// <param name="RSAPublicKey">RSA Public Key.</param>
        public PublicKeyRSA(RSAPublicKey RSAPublicKey) {
            this.n = RSAPublicKey.Modulus;
            this.e = RSAPublicKey.PublicExponent;
            }


        /// <summary>
        /// Return the parameters as a PKIX RSAPublicKey structure;
        /// </summary>
        public virtual RSAPublicKey RSAPublicKey {
            get {
                return new RSAPublicKey() {
                    Modulus = n,
                    PublicExponent = e
                    };
                }
            }


        ///// <summary>
        ///// Extract an RSA KeyPair.
        ///// </summary>
        ///// <returns></returns>
        //public override KeyPair GetKeyPair() {
        //    var Parameters = RSAParameters();
        //    return new RSAKeyPair(RSAPublicKey);
        //    }



        }

    /// <summary>
    /// Represents an RSA Private Key.
    /// </summary>
    public partial class PrivateKeyRSA {

        /// <summary>
        /// Construct from the spcified RSA Key
        /// </summary>
        /// <param name="KeyPair">An RSA key Pair.</param>
        public PrivateKeyRSA(RSAKeyPairBase KeyPair) {
            kid = KeyPair.UDF;
            var RSAPrivateKey = KeyPair.RSAPrivateKey;
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
        public PrivateKeyRSA(RSAPrivateKey RSAPrivateKey) {
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
        public virtual RSAPrivateKey RSAPrivateKey {
            get {
                return new RSAPrivateKey() {
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

    }
