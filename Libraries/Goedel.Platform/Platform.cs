using System.Threading.Tasks;

namespace Goedel.Platform {

    /// <summary>
    /// Contains static links to delegates to perform platform specific
    /// operations not supported in the portable libraries. Delegates must 
    /// be bound before use.
    /// </summary>
    public static class Platform {

        /// <summary>
        /// Make a DNS Request of the specified client.
        /// </summary>
        /// <param name="Request">DNS request set</param>
        /// <param name="DNSClient">Client to which request is directed</param>
        /// <returns>Task instance.</returns>
        public delegate Task<DNSResponse> QueryAsyncDelegateType(DNSClient DNSClient, DNSRequest Request);

        /// <summary>Make a DNS Request of the specified client.</summary>
        public static QueryAsyncDelegateType QueryAsyncDelegate;

        /// <summary>Default client context for DNS query</summary>
        public static DNSClient Client;

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


        }
    }
