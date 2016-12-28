using System.Numerics;
using System.Collections.Generic;
using Goedel.Utilities;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography {
    class DHKeyPair : DHKeyPairBase {

        /// <summary>
        /// The internal DH parameters
        /// </summary>
        DiffeHellmanPublic PublicKey { get; set; }
        DiffeHellmanPrivate PrivateKey { get { return PublicKey as DiffeHellmanPrivate; }  }

        /// <summary>
        /// Return private key parameters in PKIX structure
        /// </summary>
        public override DHDomain DHDomain { get {
                return DHPublicKey.Domain;
                } }

        /// <summary>
        /// Return private key parameters in PKIX structure
        /// </summary>
        public override DHPrivateKey DHPrivateKey {
            get {
                _DHPrivateKey = _DHPrivateKey ?? new DHPrivateKey() {
                    Domain = DHDomain,
                    Private = PrivateKey.Private.ToByteArray()
                    };
                return _DHPrivateKey;
                }
            }
        DHPrivateKey _DHPrivateKey = null;

        /// <summary>
        /// Return public key parameters in PKIX structure
        /// </summary>
        public override DHPublicKey DHPublicKey {
            get {
                _DHPublicKey = _DHPublicKey ?? new DHPublicKey() {
                    Domain = DHDomain,
                    Public = PublicKey.Public.ToByteArray()
                    };
                return _DHPublicKey;
                }
            }
        DHPublicKey _DHPublicKey = null;

        /// <summary>
        /// The public key data formatted as a PKIX KeyInfo data blob.
        /// </summary>
        public override SubjectPublicKeyInfo KeyInfoData {
            get {
                return new SubjectPublicKeyInfo(DHKeyPairBase.KeyOIDPublic, DHPublicKey.DER());
                }
            }

        /// <summary>
        /// Stub method to return a signature provider. This provider does not implement
        /// signature and so always returns null. 
        /// </summary>
        /// <param name="Bulk">The digest algorithm to use</param>
        public override CryptoProviderSignature SignatureProvider(
                    CryptoAlgorithmID Bulk = CryptoAlgorithmID.Default) {
            throw new NYI("To do");
            }


        /// <summary>
        /// Returns an encryption provider for the key (if the public portion is available)
        /// </summary>
        /// <param name="Bulk">The encryption algorithm to use</param>
        public override CryptoProviderExchange ExchangeProvider(
                    CryptoAlgorithmID Bulk = CryptoAlgorithmID.Default) {
            throw new NYI("To do");
            }



        /// <summary>
        /// Create a new DH keypair.
        /// </summary>
        /// <param name="KeySecurity">The key security model</param>
        /// <param name="KeySize">The key size</param>
        public DHKeyPair(KeySecurity KeySecurity = KeySecurity.Ephemeral, int KeySize = 2048) {
            var PublicKey = new DiffeHellmanPrivate(KeySize);
            Platform.WriteToKeyStore(this, KeySecurity);
            }

        /// <summary>
        /// Create a new DH keypair.
        /// </summary>
        /// <param name="PublicKey">The public key to create a provider for</param>
        public DHKeyPair(DiffeHellmanPublic PublicKey) {
            this.PublicKey = PublicKey;
            }


        /// <summary>
        /// Retrieve the private key from local storage (if not already available)
        /// </summary>
        public override void GetPrivate() {
            if (PrivateKey != null) {
                return; // Already got the private value
                }

            var Private = Platform.FindInKeyStore(UDF) as DHKeyPair;
            PublicKey = Private.PublicKey;
            }

        /// <summary>
        /// Split the private key into a number of recryption keys.
        /// <para>
        /// Since the
        /// typical use case for recryption requires both parts of the generated machine
        /// to be used on a machine that is not the machine on which they are created, the
        /// key security level is always to permit export.</para>
        /// </summary>
        /// <param name="Shares">The number of keys to create.</param>
        /// <returns>The created keys</returns>
        public KeyPair[] GenerateRecryptionSet(int Shares) {
            var Private = PrivateKey.MakeRecryption(Shares);

            var Result = new KeyPair[Shares];
            for (var i=0; i<Shares; i++) {
                Result[i] = new DHKeyPair(Private[i]);
                }

            return Result;
            }


        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to a private key
        /// </summary>
        /// <param name="Private">Private key parameters</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DiffeHellmanPrivate Private) {
            return Private.Agreement(PublicKey);
            }


        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to a private key
        /// </summary>
        /// <param name="Public">Public key parameters</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DHKeyPair Public) {
            return PrivateKey.Agreement(Public.PublicKey);
            }

        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to a private key
        /// </summary>
        /// <param name="Public">Public key parameters</param>
        /// <param name="Carry">Recryption carry over value, to be combined with the
        /// result of this key agreement.</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DHKeyPair Public, BigInteger Carry) {
            return PrivateKey.Agreement(Public.PublicKey, Carry);
            }

        }
    }
