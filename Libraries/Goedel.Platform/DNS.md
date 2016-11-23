#DNS Client API


The DNSClient API is part of the Goedel.Platform library. Since the DNSClient integration
needs access to information not provided by the portable library, it is necessary to
link to and initialize either the Goedel.Platform.Framework or the 
Goedel.Platform.Universal library.


Example use:

~~~~
using Goedel.Platform;
using Goedel.Platform.Framework;

namespace TestDNS {

    public class Main {
         public static void main () {
             //Register DNS Client with the portable libraries            
             Framework.Initialize();  
            
            var service = DNSClient.Resolve("prismproof.org", 
                "mmm", Fallback:DNSFallback.Prefix);

            //Connect to service
            }
        }
    }
~~~~



##Retry strategy

The (planned) retry strategy is based off figures from Windows 10 with the maximum timeout
cut down to 5 seconds instead of 10.

If one DNS resolver is configured:

<ul>
* Try resolver A
* If no response, wait 1 second, retry resolver A
* If no response, wait 2 seconds, retry resolver A
* If no response, wait 2 seconds, retry resolver A
<ul>

If two DNS resolvers are configured:

<ul>
* Try resolver A and B in parallel
* If no response, wait 1 second, retry resolver A
* If no response, wait 1 seconds, retry resolver B
<ul>




##Not Yet Implemented

The code does not currently actually call the DNS Client. Instead, it assumes the 
DNS SRV lookup fails and performs the fallback approach (go to 
http://mmm.prismproof.org/.well-known/mmm/)

The DNS classes in Goedel.Platform need to be redesigned to cope with

<ul>
* Out of order responses to multiple requests

* Responses that don't match the request

* Retry if request not received

* Timeout if request fails.

* Batch multiple queries into one

* DNS TTL values are saved and the retrieved DNS parameters flushed
when they become stale.
</ul>

Another part of the limitation is in 

Goedel.Protocol.Framework.WebRemoteSession

This needs to be modified so that the ServiceDescription parameters are 
updated properly