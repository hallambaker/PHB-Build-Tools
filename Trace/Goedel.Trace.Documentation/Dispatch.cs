using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;
using Goedel.IO;
using Goedel.Trace;
using Goedel.Trace.Client;
using Goedel.Trace.Server;
using Goedel.Protocol.Debug;

namespace Goedel.Trace.Documentation {
    public partial class Shell {
        public override void Register (Register Options) {
            var Output = Options.Out.Value;




            // Call the code to make the examples
            var Examples = new TraceExamples(Options);

            //// Output the result to a file
            //using (var Writer = Output.OpenTextWriterNew()) {
            //    var ExampleGenerator = new ExampleGenerator(Writer);
            //    ExampleGenerator.AdminExamples(Examples);
            //    }

            }
        }

    public partial class TraceExamples {

        public string ServiceAddress = "trace.example.com";

        public TraceClient TraceClient;
        public TraceServerPortalTraced Portal;
        public TraceDictionary Traces { get; set; }

        /// <summary>
        /// Generate a set of example messages.
        /// </summary>
        /// <param name="Options"></param>
        public TraceExamples (Register Options) {
            StartService();

            Test();
            }

        /// <summary>
        /// Start Mesh/Recrypt as a direct service
        /// </summary>
        void StartService () {


            //// These go in front
            //Portal = new TraceServerPortalTraced();
            //TracePortal.Default = Portal;
            //Portal.Label("Default");


            NetworkTraceListener.Initialize();


            //TraceClient = new TraceClient(ServiceAddress);

            }


        void Test () {


            System.Diagnostics.Trace.WriteLine("Hello world");
            System.Diagnostics.Trace.WriteLine("Hello world", "Category");


            //TraceClient.NewFile("Debug");

            //TraceClient.Trace("This is a test 1");
            //TraceClient.Trace("This is a test 2");
            //TraceClient.Trace("This is a test 3");
            //TraceClient.Trace("This is a test 4");
            //TraceClient.Trace("This is a test 5");
            }
        }

    }
