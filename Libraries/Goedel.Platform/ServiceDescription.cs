using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using Goedel.Utilities;

namespace Goedel.Platform {
    /// <summary>
    /// Represents an Internet destination, this may be a single IPv4 or IPv6 
    /// address or a sequence of prioritized IP addresses.
    /// </summary>
    public class ServiceDescription {
        Random Random = new Random();

        /// <summary>The list of policy entries</summary>
        public List<ServiceEntry> Entries = new List<ServiceEntry>();

        /// <summary>Sorted Array of policy entries</summary>
        public ServiceEntry[] SortedEntries = null;

        /// <summary>The list of policy entries</summary>
        public ServiceEntry Default { get; set; }

        /// <summary>The target address for discovery</summary>
        public string Address { get; set; }

        /// <summary>The service being discovered</summary>
        public string Service { get; set; }

        /// <summary>The service address being discovered</summary>
        public string ServiceAddress { get; set; }

        /// <summary>The default path for the Web Service Endpoint</summary>
        string DefaultPath {
            get { return "/.well-known/" + Service + "/"; }
            }


        int Index;
        /// <summary>Returns the next service entry</summary>
        public ServiceEntry Next {
            get {
                if (Entries.Count <= 0) {
                    return Default;
                    }
                if (SortedEntries == null) {
                    Sort();
                    Index = 0;
                    }
                return Index < SortedEntries.Length ? SortedEntries[Index++] : null;
                }
            }

        /// <summary>
        /// Standard constructor, used for when policy description data is found
        /// </summary>
        public ServiceDescription(string Address, string Service) {
            this.Service = Service;
            this.Address = Address;
            ServiceAddress = "_" + Service + "._tcp." + Address;
            }

        /// <summary>
        /// Fallback constructor, construct entry with no policy information.
        /// </summary>
        /// <param name="Address">DNS address of service</param>
        /// <param name="Service">Service identifier (without decorations)</param>
        /// <param name="Port">IP port number</param>
        /// <param name="Fallback">Fallback mode for if no SRV record is found</param>
        public ServiceDescription(string Address, string Service, int? Port, 
                        DNSFallback Fallback = DNSFallback.Prefix) {
            this.Address = Address;
            this.Service = Service;
            this.Fallback(Port, Fallback);
            }

        /// <summary>
        /// Fallback discovery for if no SRV records are found.
        /// </summary>
        /// <param name="Port">IP port number</param>
        /// <param name="Fallback">Fallback mode for if no SRV record is found</param>
        public void Fallback(int? Port = null, DNSFallback Fallback = DNSFallback.Prefix) {
            var ServiceEntry = new ServiceEntry() {
                Address = ServiceDescription.Fallback(Address, Service, Fallback),
                Port = Port,
                Path = DefaultPath
                };
            Default = ServiceEntry;
            }

        /// <summary>
        /// Add a service entry. NB a service entry can only be attached to one
        /// description.
        /// </summary>
        /// <param name="ServiceEntry">The service entry to add</param>
        public void Add(ServiceEntry ServiceEntry) {
            ServiceEntry.ServiceDescription = this;
            Entries.Add(ServiceEntry);
            }


        /// <summary>
        /// Sort the entries into order using the SRV weightings and priorities.
        /// </summary>
        public void Sort() {
            Index = 0;

            SortedEntries = new ServiceEntry[Entries.Count];

            for (var i = 0; i < Entries.Count; i++) {
                int Weight = 0;
                int Priority = UInt16.MaxValue;
                int Pick = 0;
                for (var j = 0; j < Entries.Count; j++) {
                    var Entry = Entries[j];
                    if (!Entry.Flag & (Entry.Priority <= Priority)) {
                        Priority = Entry.Priority;
                        Weight += Entry.Weight;
                        Pick = j;
                        }
                    if (Weight > 0) {
                        var Score = Random.Next(Weight);
                        Weight = 0;
                        // Weighted selection
                        for (var k = 0; k < Entries.Count; k++) {
                            var Entry2 = Entries[j];
                            if (!Entry.Flag & (Entry.Priority == Priority)) {
                                Score -= Entry2.Weight;
                                if (Score < 0) {
                                    Pick = k;
                                    break; // quit for(k) loop
                                    }
                                }
                            }
                        }
                    SortedEntries[j] = Entries[Pick];
                    Entries[Pick].Flag = true;
                    }
                }

            }

        /// <summary>
        /// Calculate fallback address if SRV resolution is not available
        /// </summary>
        /// <param name="Address">The stem address</param>
        /// <param name="Service">The IANA assigned service identifier. This is the actual
        /// identifier as assigned and not the SRV prefix formed from the identifier.</param>
        /// <param name="Fallback">The fallback mode.</param>
        /// <returns>The constructed string</returns>
        public static string Fallback(string Address, string Service, DNSFallback Fallback) {
            if (Service == null) {
                return Address;
                }
            switch (Fallback) {
                case DNSFallback.Address: return Address;
                case DNSFallback.Prefix: return Service + "." + Address;
                default: return null;
                }
            }




        }

    }
