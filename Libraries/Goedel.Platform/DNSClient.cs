using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Goedel.Utilities;


namespace Goedel.Platform {

    


    /// <summary>DNS Fallback Options</summary>
    public enum DNSFallback {
        /// <summary>Do not attempt fallback if no SRV record exists</summary>
        None,
        /// <summary>Attempt to resolve &lt;Address&gt; if SRV resolution fails</summary>
        Address,
        /// <summary>Attempt to resolve &lt;Service&gt;.&lt;Address&gt; if SRV resolution fails</summary>
        Prefix

        }

    /// <summary>Transport security policy</summary>
    public enum TransportSecurity {
        /// <summary>Do not accept TLX</summary>
        Refuse = 0,
        /// <summary>Allow any transport security protocol</summary>
        Any = 0x8FFE,
        /// <summary>Require transport security</summary>
        Require = 1,
        /// <summary>Accept TLS 1.1 or earlier</summary>
        TLS_1_1 = 0x2,
        /// <summary>Accept TLS 1.2</summary>
        TLS_1_2 = 0x4,
        /// <summary>Accept TLS 1.3</summary>
        TLS_1_3 = 0x8,

        /// <summary>No value specified</summary>
        NULL = -1
        }


    /// <summary>Transport protocol</summary>
    public enum Transport {
        /// <summary>Accept HTTP 1.1</summary>
        HTTP = 1,
        /// <summary>Accept HTTP/2</summary>
        HTTP2 = 2,
        /// <summary>Accept QUIC</summary>
        QUIC = 4,

        /// <summary>No value specified</summary>
        NULL = -1
        }


    /// <summary>
    /// DNS client.
    /// </summary>
    public abstract class DNSClient {

        /// <summary>Return a DNS Client Context in which to make a set of queries.
        /// </summary>
        /// <returns>The DNS Client Context</returns>
        public abstract DNSContext GetContext();





        /// <summary>
        /// Resolve a DNS name to an address and service characteristics.
        /// </summary>
        /// <param name="Address">The address to use</param>
        /// <param name="Service">The DNS service prefix</param>
        /// <param name="Port">The default DNS port number</param>
        /// <param name="Fallback">The fallback mode to use if SRV lookup fails</param>
        /// <returns>IP Destination describing the resolution results</returns>
        public static ServiceDescription ResolveService(string Address, string Service = null,
                    int? Port = null, DNSFallback Fallback = DNSFallback.Prefix) {

            var ServiceDescription = ResolveServiceAsync(Address, Service);
            ServiceDescription.Wait();

            return ServiceDescription.Result;

            }

        /// <summary>
        /// Perform Asynchronous query for Service discovery and description records
        /// using the platform default DNSClient.
        /// </summary>
        /// <param name="Address">The address to query</param>
        /// <param name="Service">The IANA service name</param>
        /// <param name="Port">The default DNS port number</param>
        /// <param name="Fallback">The fallback mode to use if SRV lookup fails</param> 
        /// <param name="Timeout">Timeout for the request</param> 
        /// <returns>Description of the discovered services.</returns>
        public static async Task<ServiceDescription> ResolveServiceAsync (string Address, 
                        string Service = null,
                        int? Port = null, DNSFallback Fallback = DNSFallback.Prefix,
                        int Timeout = 2500) {

            var Context = Platform.DNSClient.GetContext();
            return await Context.QueryServiceAsync(Address, Service, Port, Fallback);
            }

        }


    /// <summary>
    /// Multiple managed DNS queries.
    /// </summary>
    public abstract class DNSContext : IDisposable {

        /// <summary>The DNS client to use</summary>
        public DNSClient DNSClient = Platform.DNSClient;

        /// <summary>Scoreboard of current requests.</summary>
        List<DNSRequest> PendingRequests = new List<DNSRequest>();

        /// <summary>The timeout value</summary>
        int Timeout;
        int Retry;

        CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        Task TaskRetry; // Task that expires when it is time to retry requests
        Task TaskTimeout ; // Task that expires when it is time to give up


        /// <summary>
        /// If true there are pending requests and the context has not timed out
        /// </summary>
        public bool Pending {  get { return Active & (PendingRequests.Count > 0);  } }


        /// <summary>
        /// If true, the context has not timed out
        /// </summary>
        public bool Active = true;

        ushort IDCounter;
        ushort NextID { get {
                IDCounter = (ushort)(((int)IDCounter + 1) & 0xffff);
                return IDCounter;
                } }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Timeout">The maximum length of time to wait for a query to be satisfied</param>
        /// <param name="Retry">Retry interval.</param>
        public DNSContext (int Timeout=5000, int Retry=1000) {
            this.Timeout = Timeout;
            this.Retry = Retry;
            IDCounter = Platform.GetRandom16();

            TaskTimeout = Task.Delay(Timeout);
            TaskRetry = Task.Delay(Retry);

            }

        /// <summary>
        /// Make a DNS request to the default client without waiting for a response
        /// </summary>
        /// <param name="Request">DNS request set</param>
        /// <returns>Task instance.</returns>
        public abstract void SendRequest(DNSRequest Request);


        /// <summary>
        /// Get the next DNS response
        /// </summary>
        /// <returns>The first valid response received.</returns>
        public abstract Task<DNSResponse> GetResponseAsync();

        bool Disposed = false;

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose() {
            if (Disposed) {
                return;
                }
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        /// <summary>
        /// Destructor, free resources;
        /// </summary>
        /// <param name="Disposing">Disposing flag</param>
        protected virtual void Dispose (bool Disposing) {
            if(Disposing) {
                Close();
                }
            }

        /// <summary>
        /// Close the context.
        /// </summary>
        public virtual void Close () {
            }

        /// <summary>
        /// Return the next response to a pending DNS request.
        /// </summary>
        /// <returns>Task instance.</returns>
        public virtual async Task<DNSResponse> NextAsync() {

            while (Active) {
                var Result = GetResponseAsync();

                await Task.WhenAny(Result, TaskTimeout);

                if (Result.IsCompleted) {
                    var Response = Result.Result;
                    for (var i = 0; i < PendingRequests.Count; i++) {
                        if (PendingRequests[i].ID == Response.ID) {
                            PendingRequests.RemoveAt(i);
                            return Response;
                            }
                        }
                    }
                if (TaskTimeout.IsCompleted) { // query has expired
                    Active = false;
                    return null;
                    }

                if (TaskRetry.IsCompleted) { // attempt a retransmit of the query
                    TaskRetry = Task.Delay(Retry);
                    }
                }
            return null;
            }


        /// <summary>
        /// Make a DNS request to the default client without waiting for a response
        /// </summary>
        /// <param name="Request">DNS request set</param>
        /// <returns>Task instance.</returns>
        public void QueueRequest(DNSRequest Request) {
            SendRequest(Request);
            PendingRequests.Add(Request);
            }

        /// <summary>
        /// Make a DNS request to the default client without waiting for a response
        /// </summary>
        /// <param name="Address">The DNS address to query</param>
        /// <param name="DNSTypeCode">The DNS Type code to query</param>
        /// <returns>Task instance.</returns>
        public void QueueRequest(string Address, DNSTypeCode DNSTypeCode) {
            var Request = new DNSRequest(Address, DNSTypeCode);
            Request.Flags = DNSFlags.RD | DNSFlags.OPCODE_QUERY;
            Request.ID = NextID;
            QueueRequest(Request);
            }


        /// <summary>
        /// Perform Asynchronous query for Service discovery and description records.
        /// </summary>
        /// <param name="Address">The address to query</param>
        /// <param name="Service">The IANA service name</param>
        /// <param name="Fallback">Fallback mode for if no SRV record is found</param>
        /// <returns>Description of the discovered services.</returns>
        public async Task<ServiceDescription> QueryServiceAsync(string Address,
                        string Service = null, int? Port = null, 
                        DNSFallback Fallback = DNSFallback.Prefix) {

            if (Service == null) {
                return new ServiceDescription(Address, Service, Port, Fallback);
                }

            var ServiceDescription = new ServiceDescription(Address, Service);

            QueueRequest(ServiceDescription.ServiceAddress, DNSTypeCode.SRV);
            QueueRequest(ServiceDescription.ServiceAddress, DNSTypeCode.TXT);

            while (Pending) {
                var Result = await NextAsync();

                foreach (var Record in Result.Answers) {
                    Add(ServiceDescription, Record);
                    }
                foreach (var Record in Result.Additional) {
                    Add(ServiceDescription, Record);
                    }
                }

            if (ServiceDescription.Entries.Count == 0) {
                ServiceDescription.Fallback(Port, Fallback);
                }

            return ServiceDescription;
            }


        /// <summary>
        /// Add information from the received record iff it is within the baliwick.
        /// </summary>
        /// <param name="ServiceDescription">The service description to add to</param>
        /// <param name="Record">DNS record to add data from</param>
        void Add(ServiceDescription ServiceDescription, DNSRecord Record) {

            if (Record.Code == DNSTypeCode.SRV) {
                var RecordSRV = Record as DNSRecord_SRV;

                Debug.WriteLine(String.Format("SRV {0} -> {1}",
                        RecordSRV.Domain.Name, RecordSRV.Target.Name));
                }
            else if (Record.Code == DNSTypeCode.TXT) {

                var RecordTXT = Record as DNSRecord_TXT;

                Debug.WriteLine(String.Format("TXT {0} -> {1}",
                        RecordTXT.Domain.Name, RecordTXT.Text));
                }
            else if (Record.Code == DNSTypeCode.A) {


                }
            else if (Record.Code == DNSTypeCode.AAAA) {


                }

            }


        }



    /// <summary>
    /// Represents an Internet destination, this may be a single IPv4 or IPv6 
    /// address or a sequence of prioritized IP addresses.
    /// </summary>
    public class ServiceEntry {

        string _Address;
        /// <summary>The DNS Address to resolve</summary>
        public virtual string Address {
            get { return _Address ?? ServiceDescription?.Default.Address; }
            set { _Address = value; } }

        int? _Port;
        /// <summary>The port number to connect to</summary>
        public virtual int? Port { 
            get {return _Port ?? ServiceDescription?.Default.Port;}
            set { _Port = value;}}

        /// <summary>Priority of this service entry</summary>
        public virtual int Priority { get; set; }
        /// <summary>Weight of this service entry</summary>
        public virtual int Weight { get; set; }

        string _Path;
        /// <summary>URI path to connect to (will default to /.well-known/&lt;Service&gt;</summary>
        public virtual string Path {
            get { return _Path ?? ServiceDescription?.Default.Path; }
            set { _Path = value; } }

        TransportSecurity? _TransportSecurity;
        /// <summary>Transport security setting</summary>
        public virtual TransportSecurity? TransportSecurity {
            get { return _TransportSecurity ?? ServiceDescription?.Default.TransportSecurity; }
            set { _TransportSecurity = value; }
            }

        Transport? _Transport;
        /// <summary>Transport setting</summary>
        public virtual Transport? Transport {
            get { return _Transport ?? ServiceDescription?.Default.Transport; }
            set { _Transport = value; }
            }

        string _URI;
        /// <summary>Security policy URI</summary>
        public virtual string URI {
            get { return _URI ?? ServiceDescription?.Default.URI; }
            set { _URI = value; }
            }

        string _UDF;
        /// <summary>Security policy fingerprint</summary>
        public virtual string UDF {
            get { return _UDF ?? ServiceDescription?.Default.UDF; }
            set { _UDF = value; }
            }

        /// <summary>Internal flag used in the sorting algorithm to mark allocated entries. </summary>
        public bool Flag { get; set; } = false;


        /// <summary>The service description to which this entry is attached</summary>
        public ServiceDescription ServiceDescription { get; set; }


        /// <summary>Calculate the Web Service Endpoint for a HTTP binding</summary>
        public string HTTPEndpoint {
            get {
                if (_Port == null) {
                    return "http://" + Address + Path;
                    }
                else {
                    return "http://" + Address + ":" + Port.ToString() + Path;
                    }
                }
            }

        }



    }
