using System;
using System.Diagnostics;

namespace Goedel.Trace.Client {
    public class NetworkTraceListener : TraceListener {

        public TraceClient TraceClient;

        static bool Initialized = false;

        /// <summary>
        /// Perform standard initialization
        /// </summary>
        public static void Initialize () {
            if (Initialized) {
                return;
                }
            var Listener = new NetworkTraceListener();
            System.Diagnostics.Trace.Listeners.Add(Listener);

            }

        public override string Name { get; set; } = "Network";
        public override bool IsThreadSafe => false;

        public NetworkTraceListener () : this ("Default") {
            }

        public NetworkTraceListener (string Name) {

            // Create network client
            TraceClient = new TraceClient();  // default to 127.0.0.1
            TraceClient.NewFile(Name);
            }

        public override void Write (string Message) {
            TraceClient.Trace(Message);
            }

        public override void WriteLine (string Message) {
            TraceClient.Trace(Message);
            }

        public override void Write (string Message, string Category) {
            TraceClient.Trace(Message, Category);
            }

        public override void WriteLine (string Message, string Category) {
            TraceClient.Trace(Message, Category);
            }


        }
    }
