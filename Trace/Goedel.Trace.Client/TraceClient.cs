using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Protocol;
using Goedel.Trace;

namespace Goedel.Trace.Client {

    /// <summary>
    /// The RecryptClient class is a convenience interface to a RecryptService instance.
    /// This provides a single point at which dispatch methods for the various transactions 
    /// may perform sanity checking on input and output variables, enforce timeouts,
    /// attempt retry etc.
    /// </summary>
    public class TraceClient {
        TraceService Service;
        int ProcessId;


        public TraceClient (string Address=null) {
            Service = TracePortal.Default.GetService(Address);

            var CurrentProcess = Process.GetCurrentProcess();
            ProcessId = CurrentProcess.Id;
            }


        public HelloResponse Hello () {
            var Request = new HelloRequest() { };
            return Service.Hello(Request);
            }

        public void NewFile (string FileName) {
            var Request = new PostRequest() { NewFile = FileName };
            Service.Post(Request);
            }

        public void Trace (string Text, string Category = null) {
            Trace(new List<string> { Text });
            }

        public void Trace (List<string> Texts) {
            // TBS: the meat here

            var Request = new PostRequest() { Entries = new List<TraceEntry>() };
            foreach (var Text in Texts) {
                Request.Entries.Add(new TraceEntry() {
                    Text = Text,
                    Issued = DateTime.Now,
                    ProcessId = ProcessId });
                }

            Service.Post(Request);
            }

        }
    }
