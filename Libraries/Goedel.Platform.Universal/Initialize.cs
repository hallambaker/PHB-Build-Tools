using System;
using System.Collections.Generic;
using System.Linq;
using Goedel.Platform;
using System.Threading.Tasks;


namespace Goedel.Platform.Universal {

    /// <summary>
    /// Network initialization. Bind the .Net implementation methods
    /// to the static delegates in the portable libraries.
    /// </summary>
    public static partial class Universal {
        /// <summary>
        /// Initialize the network and cryptography stacks for use with a
        /// .NET Framework or Mono app.
        /// (if this can be found)
        /// </summary>
        public static void Initialize() {
            //Goedel.Platform.Platform.Client = new DNSClientUDP();
            //Goedel.Platform.Platform.QueryDelegate = DNSClientUDP.QueryAsync;
            //Goedel.Platform.Platform.GetRandomBytes = GetRandomBytes;
            }

        }
    }
