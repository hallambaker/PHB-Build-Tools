using System.Threading.Tasks;

namespace Goedel.Platform {

    /// <summary>
    /// Contains static links to delegates to perform platform specific
    /// operations not supported in the portable libraries. Delegates must 
    /// be bound before use.
    /// </summary>
    public static class Platform {

        /// <summary>Default client context for DNS query</summary>
        public static DNSClient DNSClient;

        /// <summary>Fill byte buffer with cryptographically strong random numbers.</summary>
        public delegate void GetRandomBytesDelegate(byte[] Data, int Offset, int Count);

        /// <summary>Fill byte buffer with cryptographically strong random numbers</summary>
        public static GetRandomBytesDelegate GetRandomBytesD;

        /// <summary>
        /// Get a specified number of random bytes.
        /// </summary>
        /// <param name="Length">Number of bytes to get</param>
        /// <returns>Random data</returns>
        public static byte[] GetRandomBytes(int Length) {
            var Data = new byte[Length];
            GetRandomBytesD(Data, 0, Length);
            return Data;
            }


        /// <summary>
        /// Return a randomly assigned UDP port.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomPort() {
            var Bytes = GetRandomBytes(3);
            int Result = Bytes[0] + 256 * Bytes[1];

            if (Result < 4096) {
                Result = Result + 256 * (Bytes[2] | 0x10);
                }

            return Result;
            }

        /// <summary>
        /// Return a randomly assigned UDP port.
        /// </summary>
        /// <returns>Random integer</returns>
        public static ushort GetRandom16 () {
            var Bytes = GetRandomBytes(2);
            var Result = Bytes[0] + 256 * Bytes[1];

            return (ushort) Result;
            }


        }
    }
