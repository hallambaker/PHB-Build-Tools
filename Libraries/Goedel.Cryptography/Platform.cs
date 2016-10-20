using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
