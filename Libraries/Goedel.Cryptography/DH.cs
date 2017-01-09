using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Goedel.ASN;
using Goedel.Utilities;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography {


    /// <summary>
    /// Diffie Hellman shared group parameters.
    /// </summary>
    public class DiffeHellmanPublic : KeyPair {


        /// <summary>
        /// The shared domain parameters
        /// </summary>
        public DHDomain DHDomain;


        /// <summary>Group modulus</summary>
        public BigInteger Modulus { get; }

        /// <summary>Generator</summary>
        public BigInteger Generator { get; }

        /// <summary>Public Key</summary>
        public BigInteger Public { get; protected set; }





        /// <summary>
        /// Create a new set of Diffie Hellman group parameters.
        /// </summary>
        /// <param name="Bits">The number of bits, this identifies the group modulus </param>
        public DiffeHellmanPublic(int Bits=2048) {
            switch (Bits) {
                case 2048: {
                    DHDomain = DHDomain.DHDomain2048;
                    break;
                    }
                case 4096: {
                    DHDomain = DHDomain.DHDomain4096;
                    break;
                    }
                default:  {
                    throw new KeySizeNotSupported();
                    }
                }
            Modulus = DHDomain.BigIntegerP;
            Generator = DHDomain.BigIntegerG;
            }

        /// <summary>
        /// Create a new set of Diffie Hellman parameters using the shared modulus, 
        /// a newly constructed generator and public and private keys.
        /// </summary>
        /// <param name="DHDomain">The shared parameters</param>
        public DiffeHellmanPublic(DHDomain DHDomain)  {
            Modulus = DHDomain.BigIntegerP;
            Generator = DHDomain.BigIntegerG;
            }


        /// <summary>
        /// Create a new set of Diffie Hellman group parameters.
        /// </summary>
        /// <param name="Generator">The generator parameter, g.</param>
        /// <param name="Modulus">The modulus parameter, p.</param>
        /// <param name="Public">The public parameter, g^x mod p.</param>
        public DiffeHellmanPublic(BigInteger Modulus, BigInteger Generator,
                        BigInteger? Public) {
            this.Modulus = Modulus;
            this.Generator = Generator;
            if (Public != null) {
                this.Public = (BigInteger)Public;
                }
            }

        /// <summary>
        /// Get the public key parameters
        /// </summary>
        /// <param name="PKIXParameters"></param>
        public DiffeHellmanPublic (DHPublicKey PKIXParameters) {
            var Domain = PKIXParameters.Domain;
            Assert.NotNull(Domain, UnknownNamedParameters.Throw);

            Modulus = Domain.BigIntegerP;
            Generator = Domain.BigIntegerG;
            Public = PKIXParameters.Public.ToBigInteger();

            }


        // Implement inherited methods

        /// <summary>
        /// Get the private key component if available on this machine
        /// </summary>
        public override void GetPrivate() {
            // NYI Implement DH GetPrivate
            throw new NotImplementedException();
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BulkAlgorithm"></param>
        /// <returns></returns>
        public override CryptoProviderExchange ExchangeProvider(
                        CryptoAlgorithmID BulkAlgorithm = CryptoAlgorithmID.Default) {
            throw new NotImplementedException();
            }

        /// <summary>
        /// Return a signature provider for the specified key
        /// </summary>
        /// <param name="BulkAlgorithm">The digest algorithm</param>
        /// <returns>Always throws error as the provider is not supported.</returns>
        public override CryptoProviderSignature SignatureProvider(CryptoAlgorithmID BulkAlgorithm = CryptoAlgorithmID.Default) {
            throw new InvalidOperation("Diffie Hellman provider does not support signature");
            }

        /// <summary>
        /// Return a populated PKIX SubjectPublicKeyInfo section for UDF calculation
        /// </summary>
        public override SubjectPublicKeyInfo KeyInfoData {
            get {
                // NYI keyinfo DH
                throw new NotImplementedException();
                }
            }


        // Implementation


        /// <summary>
        /// Create a new ephemeral private key and use it to perform a key
        /// agreement.
        /// </summary>
        /// <param name="Public">Set of newly created DH parameters for Alice</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement (out DiffeHellmanPublic Public) {
            var Private = new DiffeHellmanPrivate(this);
            Public = Private.DiffeHellmanPublic;
            return Private.Agreement(this);
            }


        /// <summary>
        /// Check that the Diffie Hellman parameters presented match those of this Key.
        /// </summary>
        /// <param name="Key"></param>
        public void Verify (DiffeHellmanPublic Key) {
            Assert.True(Key.Modulus == Modulus, CryptographicException.Throw);
            Assert.True(Key.Generator == Generator, CryptographicException.Throw);
            }


        /// <summary>
        /// Perform final stage in a Diffie Hellman Agreement to reduce an 
        /// array of carry returns to a single agreement result.
        /// </summary>
        /// <param name="Carry">The partial recryption results.</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(BigInteger[] Carry) {

            Assert.True(Carry.Length >= 1, InsufficientResults.Throw);

            var Accumulator = Carry[0];
            for (var i = 1; i < Carry.Length; i++) {
                Accumulator = Accumulator * Carry[i];
                }
            return Accumulator % Modulus;
            }


        }

    /// <summary>
    /// Represents a set of Diffie Hellman parameters.
    /// </summary>
    public class DiffeHellmanPrivate : DiffeHellmanPublic {

        /// <summary>Private Key</summary>
        public BigInteger Private { get; set; }

        /// <summary></summary>
        public bool IsRecryption { get; set; }

        DiffeHellmanPublic _DiffeHellmanPublic = null;

        /// <summary>
        /// Return the public key.
        /// </summary>
        public DiffeHellmanPublic DiffeHellmanPublic {
            get {
                if (_DiffeHellmanPublic == null) {
                    _DiffeHellmanPublic = new DiffeHellmanPublic(
                        Modulus: Modulus, Generator: Generator, Public: Public);
                    }
                return _DiffeHellmanPublic;
                }
            }


        /// <summary>
        /// Create a new set of Diffie Hellman parameters using the shared modulus, 
        /// a newly constructed generator and public and private keys.
        /// </summary>
        /// <param name="DHDomain">The shared parameters</param>
        public DiffeHellmanPrivate(DHDomain DHDomain) : base (DHDomain) {
            Private = Platform.GetRandomBigInteger(Modulus);
            Public = BigInteger.ModPow(Generator, Private, Modulus);
            IsRecryption = false;
            }


        /// <summary>
        /// Create a new set of Diffie Hellman parameters using the shared modulus, 
        /// a newly constructed generator and public and private keys.
        /// </summary>
        /// <param name="Bits">The number of bits in the modulus to be created. Valid values
        /// are 2048 and 4096</param>
        public DiffeHellmanPrivate(int Bits=2048) : base(Bits) {
            Private = Platform.GetRandomBigInteger(Modulus);
            Public = BigInteger.ModPow(Generator, Private, Modulus);
            IsRecryption = false;
            }

        /// <summary>
        /// Create a new set of Diffie Hellman parameters using the shared modulus, 
        /// a newly constructed generator and public and private keys.
        /// </summary>
        /// <param name="DiffeHellmanPublic">Public key to use to specify the
        /// modulus and generator</param>
        public DiffeHellmanPrivate(DiffeHellmanPublic DiffeHellmanPublic) : 
                    base(Modulus:DiffeHellmanPublic.Modulus,
                        Generator:DiffeHellmanPublic.Generator,
                        Public: null) {
            Private = Platform.GetRandomBigInteger(Modulus);
            Public = BigInteger.ModPow(Generator, Private, Modulus);
            IsRecryption = false;
            }

        private DiffeHellmanPrivate (BigInteger Private) {
            this.Private = Private;
            Public = BigInteger.ModPow(Generator, Private, Modulus);
            }


        /// <summary>
        /// Make a recryption keyset by splitting the private key.
        /// </summary>
        /// <param name="Shares">Number of shares to create</param>
        /// <returns>Array shares.</returns>
        public DiffeHellmanPrivate[] MakeRecryption (int Shares) {
            BigInteger Accumulator = 0;
            var Result = new DiffeHellmanPrivate[Shares];

            for (var i = 1; i < Shares; i++) {
                var NewPrivate = Platform.GetRandomBigInteger(Modulus);

                Result[i] = new DiffeHellmanPrivate(NewPrivate) { IsRecryption = true};
                Accumulator = (Accumulator + NewPrivate) % Modulus;
                }

            Result[0] = new DiffeHellmanPrivate((Modulus + Private - Accumulator) % Modulus) {
                IsRecryption = true };
            return Result;
            }


        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to this private key
        /// </summary>
        /// <param name="Public">Set of newly created DH parameters for Alice</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DiffeHellmanPublic Public) {
            Verify(Public);
            return BigInteger.ModPow(Public.Public, Private, Modulus);
            }

        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to this private key
        /// </summary>
        /// <param name="Public">Set of newly created DH parameters for Alice</param>
        /// <param name="Carry">Recryption carry over value, to be combined with the
        /// result of this key agreement.</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DiffeHellmanPublic Public, BigInteger Carry) {
            Verify(Public);
            var Partial = Agreement(Public);
            return (Partial * Carry) % Modulus;
            }


        }


    /// <summary>
    /// Represent the result of a Diffie Hellman Key exchange.
    /// </summary>
    public class DiffieHellmanResult {

        /// <summary>The key agreement result</summary>
        public BigInteger Agreement;

        /// <summary>The key agreement result as a byte array</summary>
        public byte[] IKM { get { return Agreement.ToByteArray(); } }

        /// <summary>Carry from proxy recryption efforts</summary>
        public BigInteger Carry;

        /// <summary>Public key generated by ephemeral key generation.</summary>
        public DiffeHellmanPublic Public;

        string _Salt;
        KeyDerive _KeyDerive = null;

        /// <summary>Salt to use in HKDF key derivation. If set will set the 
        /// Key derivation function to HKDF with the specified salt.</summary>
        public string Salt {
            get {
                return _Salt;
                }
            set {
                _Salt = value;
                _KeyDerive = new KeyDeriveHKDF(IKM, _Salt); }
            }

        /// <summary>Key derivation function. May be overriden, defaults to KDF.</summary>
        public KeyDerive KeyDerive {
            get {
                _KeyDerive = _KeyDerive ?? new KeyDeriveHKDF(IKM, _Salt);
                return _KeyDerive;
                }
            set {
                _KeyDerive = value;
                }
            }

        /// <summary>
        /// Convert values to PKIX array.
        /// </summary>
        /// <returns></returns>
        public byte[] ToPKIX () {

            return null; // NYI write PKI wrapper to make this a binary blob.
            }
        }

    }
