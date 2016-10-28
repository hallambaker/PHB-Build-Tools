using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;

namespace Goedel.Cryptography {
    /// <summary>
    /// Static class containing delegates to platform specific 
    /// factory methods. 
    /// </summary>
    public static class Platform {

        /// <summary>Default SHA-2-512 provider optimized for small data items</summary>
        public static CryptoAlgorithm SHA2_512;
        /// <summary>Default SHA-2-256 provider optimized for small data items</summary>
        public static CryptoAlgorithm SHA2_256;
        /// <summary>Default SHA-1 provider optimized for small data items</summary>
        public static CryptoAlgorithm SHA1;

        /// <summary>Default HMAC-SHA2-512 provider optimized for small data items</summary>
        public static CryptoAlgorithm HMAC_SHA2_256;
        /// <summary>Default HMAC-SHA2-512 provider optimized for small data items</summary>
        public static CryptoAlgorithm HMAC_SHA2_384;
        /// <summary>Default HMAC-SHA2-512 provider optimized for small data items</summary>
        public static CryptoAlgorithm HMAC_SHA2_512;


        /// <summary>Default AES-256 provider optimized for small data items</summary>
        public static CryptoAlgorithm AES_256;

        /// <summary>Fill byte buffer with cryptographically strong random numbers.</summary>
        public delegate void GetRandomBytesDelegateType(byte[] Data, int Offset, int Count);

        /// <summary>Fill byte buffer with cryptographically strong random numbers</summary>
        public static GetRandomBytesDelegateType GetRandomBytesDelegate;


        /// <summary>
        /// Get a specified number of random bytes.
        /// </summary>
        /// <param name="Length">Number of bytes to get</param>
        /// <returns>Random data</returns>
        public static byte[] GetRandomBytes(int Length) {
            var Data = new byte[Length];
            GetRandomBytesDelegate(Data, 0, Length);
            return Data;
            }

        /// <summary>
        /// Get a Big integer with a specified number of random bits.
        /// </summary>
        /// <param name="Bits">Number of bits to get</param>
        /// <returns>Random data</returns>
        public static BigInteger GetRandomBigInteger(int Bits) {
            var RandomBytes = GetRandomBytes(Bits / 8);
            return new BigInteger(RandomBytes);
            }


        /// <summary>
        /// Get a Big Integer that is smaller than the input value
        /// </summary>
        /// <param name="Ceiling">Number of bits to get</param>
        /// <returns>Random data</returns>
        public static BigInteger GetRandomBigInteger(BigInteger Ceiling) {
            Assert.True(Ceiling > 0, CryptographicException.Throw);

            var Bits = Ceiling.ToByteArray().Length * 8;
            var Test = GetRandomBigInteger(Bits);

            while (Test >= Ceiling) {
                Test = GetRandomBigInteger(Bits);
                }

            return Test;
            }

        ///// <summary>
        ///// Find a random prime with the specified number of bits.
        ///// </summary>
        ///// <param name="Bits">Number of bits in prime to be created</param>
        ///// <returns>The generated prime</returns>
        //public static BigInteger GetRandomPrime (int Bits) {

        //    var Data = GetRandomBytes(Bits / 8);

        //    // Set the most and least significant bits to 1. 
        //    // The C# BigInteger class stores data in little endian fashion.
        //    Data[0] |= (byte) 0x01;
        //    Data[Data.Length-1] |= (byte)0x80;
        //    var Result = new BigInteger(Data);

        //    var Prime = Result.IsPrime();
        //    while (!Prime) {
        //        Result += 2;
        //        Prime = Result.IsPrime();
        //        }

        //    return Result;
        //    }



        /// <summary>Find a key by fingerprint in the local key stores</summary>
        /// <param name="UDF"></param>
        /// <returns></returns>
        public delegate KeyPair FindLocalDelegateType(string UDF);

        /// <summary>
        /// Catalog of all local key stores.
        /// </summary>
        public static List<FindLocalDelegateType> FindLocalDelegates =
            new List<FindLocalDelegateType>();


        }
    }
