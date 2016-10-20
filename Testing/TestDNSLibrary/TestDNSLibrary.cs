using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Platform;
using Goedel.Platform.Framework;

namespace TestDNS_Library {
    class TestDNSLibrary {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            // Use the DNS UDP client.
            Framework.Initialize();

            var Service1 = DNSClient.Resolve("prismproof.org");

            var Service2 = DNSClient.Resolve("prismproof.org", "mmm");

            var Service3 = DNSClient.Resolve("prismproof.org", "www", 80);


            }
        }
    }
