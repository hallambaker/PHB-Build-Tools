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
        private const string Group2048PText = @"
            87A8E61D B4B6663C FFBBD19C 65195999 8CEEF608 660DD0F2
            5D2CEED4 435E3B00 E00DF8F1 D61957D4 FAF7DF45 61B2AA30
            16C3D911 34096FAA 3BF4296D 830E9A7C 209E0C64 97517ABD
            5A8A9D30 6BCF67ED 91F9E672 5B4758C0 22E0B1EF 4275BF7B
            6C5BFC11 D45F9088 B941F54E B1E59BB8 BC39A0BF 12307F5C
            4FDB70C5 81B23F76 B63ACAE1 CAA6B790 2D525267 35488A0E
            F13C6D9A 51BFA4AB 3AD83477 96524D8E F6A167B5 A41825D9
            67E144E5 14056425 1CCACB83 E6B486F6 B3CA3F79 71506026
            C0B857F6 89962856 DED4010A BD0BE621 C3A3960A 54E710C3
            75F26375 D7014103 A4B54330 C198AF12 6116D227 6E11715F
            693877FA D7EF09CA DB094AE9 1E1A1597";

        private const string Group2048GText = @"
            3FB32C9B 73134D0B 2E775066 60EDBD48 4CA7B18F 21EF2054
            07F4793A 1A0BA125 10DBC150 77BE463F FF4FED4A AC0BB555
            BE3A6C1B 0C6B47B1 BC3773BF 7E8C6F62 901228F8 C28CBB18
            A55AE313 41000A65 0196F931 C77A57F2 DDF463E5 E9EC144B
            777DE62A AAB8A862 8AC376D2 82D6ED38 64E67982 428EBC83
            1D14348F 6F2F9193 B5045AF2 767164E1 DFC967C1 FB3F2E55
            A4BD1BFF E83B9C80 D052B985 D182EA0A DB2A3B73 13D3FE14
            C8484B1E 052588B9 B7D2BBD2 DF016199 ECD06E15 57CD0915
            B3353BBB 64E0EC37 7FD02837 0DF92B52 C7891428 CDC67EB6
            184B523D 1DB246C3 2F630784 90F00EF8 D647D148 D4795451
            5E2327CF EF98C582 664B4C0F 6CC41659";

        private const string Group2048QText = @"
            8CF83642 A709A097 B4479976 40129DA2 99B1A47D 1EB3750B
            A308B0FE 64F5FBD3";

        static readonly BigInteger Group2048P =
                new BigInteger(BaseConvert.FromBase16String(Group2048PText));
        static readonly BigInteger Group2048G =
                new BigInteger(BaseConvert.FromBase16String(Group2048GText));
        static readonly BigInteger Group2048Q =
                new BigInteger(BaseConvert.FromBase16String(Group2048QText));


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
        /// Perform Diffie Hellman Key Agreement using the public key of the counterparty 
        /// parameters and this private key.
        /// </summary>
        /// <param name="Private">The counterparty private key</param>
        /// <returns>The key agreement value ZZ</returns>
        internal protected BigInteger Agreement(BigInteger Private) {
            return BigInteger.ModPow(Public, Private, Modulus);
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

            return Agreement (Private.Private);
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




        /// <summary>
        /// Make a recryption keyset by splitting the private key.
        /// </summary>
        /// <param name="Shares">Number of shares to create</param>
        /// <returns>Array shares.</returns>
        public BigInteger[] MakeRecryption (int Shares) {
            BigInteger Accumulator = 0;
            var Result = new BigInteger[Shares];

            for (var i = 1; i < Shares; i++) {
                Result [i] = Platform.GetRandomBigInteger(Modulus);
                Accumulator = (Accumulator + Result[i]) % Modulus;
                }
            Result[0] = (Private - Accumulator) % Modulus;

            return Result;
            }

        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to this private key
        /// </summary>
        /// <param name="Public">Set of newly created DH parameters for Alice</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DiffeHellmanPublic Public) {
            Verify(Public);
            return Public.Agreement(Private);
            }

        /// <summary>
        /// Perform a Diffie Hellman Key Agreement to this private key
        /// </summary>
        /// <param name="Public">Set of newly created DH parameters for Alice</param>
        /// <param name="Carry">Recryption carry over value, to be combined with the
        /// result of this key agreement.</param>
        /// <returns>The key agreement value ZZ</returns>
        public BigInteger Agreement(DiffeHellmanPublic Public, BigInteger Carry) {
            var Partial = Agreement(Public);
            return Partial + Carry;
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
