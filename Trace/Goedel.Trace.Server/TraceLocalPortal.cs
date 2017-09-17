using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Trace;
using Goedel.Protocol;

namespace Goedel.Trace.Server {


    /// <summary>
    /// Abstract interface to a local service provider.
    /// </summary>
    public abstract class TraceLocalPortal : TracePortal {
        public string ServiceName { get; set; }

        /// <summary>
        /// The local PublicMeshServiceHost.
        /// </summary>
        public TraceLocalServiceProvider ConfirmServiceHost;
        }


    /// <summary>
    /// Direct connection to service provider via API calls. 
    /// </summary>
    public class ConfirmPortalDirect : TraceLocalPortal {

        /// <summary>
        /// Create new portal using the default stores.
        /// </summary>
        public ConfirmPortalDirect () {
            ConfirmServiceHost = new TraceLocalServiceProvider(ServiceName);
            }

        /// <summary>
        /// Return a MeshService object for the named portal service.
        /// </summary>
        /// <param name="Account">The account to get.</param>
        /// <param name="Portal">The portal to get the service from.</param>
        /// <returns>The service instance</returns> 
        public override TraceService GetService (string Portal, string Account) {
            var Session = new DirectSession(null);
            ConfirmServiceClient = new TraceServiceLocal(ConfirmServiceHost, Session);
            return ConfirmServiceClient;
            }
        }


    /// <summary>
    /// Direct connection to service provider via JSON encoding, decoding and dispatch.
    /// Useful for producing documentation and for testing.
    /// </summary>
    public class TracePortalLocal : TraceLocalPortal {

        /// <summary>
        /// Create new portal using the default stores.
        /// </summary>
        public TracePortalLocal (string ServiceName) {
            this.ServiceName = ServiceName;
            ConfirmServiceHost = new TraceLocalServiceProvider(ServiceName);
            }

        /// <summary>
        /// Return a MeshService object for the named portal service.
        /// </summary>
        /// <param name="Account">The account to get.</param>
        /// <param name="Service">The service to get the service from.</param> 
        /// <returns>The service instance</returns>
        public override TraceService GetService (string Service, string Account) {
            var Session = new LocalRemoteSession(ConfirmServiceHost, ServiceName, Account);
            ConfirmServiceClient = new TraceServiceClient(Session);
            return ConfirmServiceClient;
            }

        }
    }
