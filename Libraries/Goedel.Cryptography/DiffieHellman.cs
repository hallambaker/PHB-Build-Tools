using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Goedel.Utilities;

namespace Goedel.Cryptography {

    /// <summary>
    /// Diffie Hellman shared group parameters.
    /// </summary>
    public class DiffeHellmanPublic {

        /// <summary>
        /// Parameters from rfc3526. These are generated using a rigid construction
        /// that has been widely reviewed but not by me.
        /// </summary>
        private const string Group2048PText = @"00
            FFFFFFFF FFFFFFFF C90FDAA2 2168C234 C4C6628B 80DC1CD1
            29024E08 8A67CC74 020BBEA6 3B139B22 514A0879 8E3404DD
            EF9519B3 CD3A431B 302B0A6D F25F1437 4FE1356D 6D51C245
            E485B576 625E7EC6 F44C42E9 A637ED6B 0BFF5CB6 F406B7ED
            EE386BFB 5A899FA5 AE9F2411 7C4B1FE6 49286651 ECE45B3D
            C2007CB8 A163BF05 98DA4836 1C55D39A 69163FA8 FD24CF5F
            83655D23 DCA3AD96 1C62F356 208552BB 9ED52907 7096966D
            670C354E 4ABC9804 F1746C08 CA18217C 32905E46 2E36CE3B
            E39E772C 180E8603 9B2783A2 EC07A28F B5C55DF0 6F4C52C9
            DE2BCBF6 95581718 3995497C EA956AE5 15D22618 98FA0510
            15728E5A 8AACAA68 FFFFFFFF FFFFFFFF";

        private const string Group4096PText = @"00
            FFFFFFFF FFFFFFFF C90FDAA2 2168C234 C4C6628B 80DC1CD1
            29024E08 8A67CC74 020BBEA6 3B139B22 514A0879 8E3404DD
            EF9519B3 CD3A431B 302B0A6D F25F1437 4FE1356D 6D51C245
            E485B576 625E7EC6 F44C42E9 A637ED6B 0BFF5CB6 F406B7ED
            EE386BFB 5A899FA5 AE9F2411 7C4B1FE6 49286651 ECE45B3D
            C2007CB8 A163BF05 98DA4836 1C55D39A 69163FA8 FD24CF5F
            83655D23 DCA3AD96 1C62F356 208552BB 9ED52907 7096966D
            670C354E 4ABC9804 F1746C08 CA18217C 32905E46 2E36CE3B
            E39E772C 180E8603 9B2783A2 EC07A28F B5C55DF0 6F4C52C9
            DE2BCBF6 95581718 3995497C EA956AE5 15D22618 98FA0510
            15728E5A 8AAAC42D AD33170D 04507A33 A85521AB DF1CBA64
            ECFB8504 58DBEF0A 8AEA7157 5D060C7D B3970F85 A6E1E4C7
            ABF5AE8C DB0933D7 1E8C94E0 4A25619D CEE3D226 1AD2EE6B
            F12FFA06 D98A0864 D8760273 3EC86A64 521F2B18 177B200C
            BBE11757 7A615D6C 770988C0 BAD946E2 08E24FA0 74E5AB31
            43DB5BFC E0FD108E 4B82D120 A9210801 1A723C12 A787E6D7
            88719A10 BDBA5B26 99C32718 6AF4E23C 1A946834 B6150BDA
            2583E9CA 2AD44CE8 DBBBC2DB 04DE8EF9 2E8EFC14 1FBECAA6
            287C5947 4E6BC05D 99B2964F A090C3A2 233BA186 515BE7ED
            1F612970 CEE2D7AF B81BDD76 2170481C D0069127 D5B05AA9
            93B4EA98 8D8FDDC1 86FFB7DC 90A6C08F 4DF435C9 34063199
            FFFFFFFF FFFFFFFF";

        static readonly BigInteger Group2048P = Group2048PText.HexToBigInteger();
        static readonly BigInteger Group2048G = new BigInteger(2);

        /// <summary>Group modulus</summary>
        public BigInteger Modulus { get; set; }

        /// <summary>Generator</summary>
        public BigInteger Generator { get; set; }

        /// <summary>Public Key</summary>
        public BigInteger Public { get; set; }


        /// <summary>
        /// Create a new set of Diffie Hellman group parameters.
        /// </summary>
        /// <param name="Bits">The number of bits, this identifies the group modulus </param>
        public DiffeHellmanPublic(int Bits=2048) {
            SetModulus(Bits);
            }

        /// <summary>
        /// Create a new set of Diffie Hellman group parameters.
        /// </summary>
        /// <param name="Bits">The number of bits, this identifies the group modulus </param>
        /// <param name="Generator">The generator parameter, g.</param>
        public DiffeHellmanPublic(int Bits, BigInteger Generator) {
            SetModulus(Bits);
            this.Generator = Generator;
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

        private void SetModulus(int Bits) {
            switch (Bits) {
                case 2048: {
                    Modulus = Group2048P;
                    Generator = Group2048G;
                    return;
                    }
                }
            throw new KeySizeNotSupported();
            }


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


        /// <summary>
        /// Perform final stage in a Diffie Hellman Agreement to reduce 
        /// </summary>
        /// <param name="Carry">The partial recryption results.</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(BigInteger[] Carry) {

            Assert.True(Carry.Length >= 1, InsufficientResults.Throw);

            var Accumulator = Carry[0];
            for (var i = 1; i< Carry.Length; i++) {
                Accumulator = Accumulator * Carry[i];
                }
            return Accumulator % Modulus;
            }


        /// <summary>
        /// Perform a Diffie Hellman Key agreement to this private key.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public CryptoData Agreement (CryptoData Input) {
            //if (IsRecryption) {
            //    if (Input.Recrypt == null) {
            //        var Result = 
            //        }
            //    else {

            //        }
            //    }
            //else {

            //    }

            throw new NYI();
            //return null;
            }


        }



    //public class DiffieHellmanKeyPair : KeyPair {

    //    }


    //public class DiffieHellman : CryptoProviderRecryption {
    //    }
    }
