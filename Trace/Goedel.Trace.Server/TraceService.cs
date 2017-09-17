using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Trace;
using Goedel.Protocol;


namespace Goedel.Trace.Server {
    public class TraceServiceLocal : TraceService {
        TraceLocalServiceProvider Provider;

        public TraceServiceLocal (
                    TraceLocalServiceProvider ServiceProvider,
                    JPCSession Session) {
            this.Provider = ServiceProvider;
            ServiceProvider.Interfaces.Add(this);
            ServiceProvider.Service = this;
            this.JPCSession = Session;
            }


        /// <summary>
        /// Base method for implementing the transaction  Hello.
        /// </summary>
        /// <param name="Request">The request object to send to the host.</param>
        /// <returns>The response object from the service</returns>
        public override HelloResponse Hello (
                HelloRequest Request) {

            var Version = new Goedel.Protocol.Version() {
                Major = 0,
                Minor = 1
                };

            var Response = new HelloResponse() {
                Version = Version
                };

            return Response;
            }


        public override PostResponse Post (PostRequest Request) {
            // NYI: Write results to file as a log
            // NB: STRIP OUT THE DIRECTORY FROM THE FILE NAME PATH OR WILL HAVE
            // SEVERE SECURITY ISSUE

            if (Request.NewFile != null) {
                Console.WriteLine("==== {0} ====", Request.NewFile);
                }
            if (Request.Entries != null) {
                foreach (var Entry in Request?.Entries) {
                    if (Entry.Text != null) {
                        Console.WriteLine(Entry.Text);
                        }
                    }
                }


            var Response = new PostResponse() {
                };

            return Response;
            }

        }
    }
