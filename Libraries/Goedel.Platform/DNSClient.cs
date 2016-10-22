using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace Goedel.Platform {

    /// <summary>
    /// DNS client.
    /// </summary>
    public class DNSClient {



        /// <summary>
        /// Make a DNS request to the default client.
        /// </summary>
        /// <param name="Request">DNS request set</param>
        /// <returns>Task instance.</returns>
        public virtual async Task<DNSResponse> QueryAsync (DNSRequest Request) {
            var Result = Platform.QueryAsyncDelegate(Platform.Client, Request);
            await Result;
            return Result.Result;
            }




        /// <summary>
        /// Resolve a DNS name to an address and service characteristics.
        /// </summary>
        /// <param name="Address">The address to use</param>
        /// <returns>IP Destination describing the resolution results</returns>
        public static ServiceDescription Resolve (string Address) {
            return Resolve(Address, null, -1);
            }

        /// <summary>
        /// Resolve a DNS name to an address and service characteristics.
        /// </summary>
        /// <param name="Address">The address to use</param>
        /// <param name="Service">The DNS service prefix</param>
        /// <returns>IP Destination describing the resolution results</returns>
        public static ServiceDescription Resolve(string Address,
            string Service) {
            return Resolve (Address, Service, -1);
            }


        /// <summary>
        /// Resolve a DNS name to an address and service characteristics.
        /// </summary>
        /// <param name="Address">The address to use</param>
        /// <param name="Service">The DNS service prefix</param>
        /// <param name="Port">The default DNS port number</param>
        /// <returns>IP Destination describing the resolution results</returns>
        public static ServiceDescription Resolve (string Address, 
            string Service, int Port) {
            return null;
            }



        }



    /// <summary>
    /// Represents an internet destination, this may be a single IPv4 or IPv6 
    /// address or a sequence of prioritized IP addresses.
    /// </summary>
    public class ServiceDescription {

        }


    /// <summary>
    /// Represents an internet destination, this may be a single IPv4 or IPv6 
    /// address or a sequence of prioritized IP addresses.
    /// </summary>
    public class ServiceDescriptionDNS : ServiceDescription {

        }

    }
