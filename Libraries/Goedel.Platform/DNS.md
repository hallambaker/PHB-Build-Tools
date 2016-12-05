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


The DNS classes in Goedel.Platform need to implement

<ul>
* Retry if request not received

* DNS TTL values are saved and the retrieved DNS parameters flushed
when they become stale.
</ul>

Another part of the limitation is in 

Goedel.Protocol.Framework.WebRemoteSession

This needs to be modified so that the ServiceDescription parameters are 
updated properly

##Test suite

A test suite is needed to perform the following unit test matrix:

Resolver configuration

<ul>
* Unavailable U1=10.6.6.6, U2=10.66.66.66

* Available: A1=8.8.8.8, A2=8.8.4.4
</ul>

The tests need to be run to check for correct behavior in the following 
configurations

<ul>
* U1

* U1, U2

* A1

* A1, A2

* A1, U1

* U1, A1
</ul>


Zone file:


<dl>
:null_test.prismproof.org

::No servers reachable

:default_test.prismproof.org

::mmm.default_test

:srv1_test.prismproof.org

::srv->test1.prismproof.org

:srv2_test.prismproof.org

::srv->test1.prismproof.org{0,100}, test2.prismproof.org {0,50}

:srv3_test.prismproof.org

::srv->test1.prismproof.org{0,100}, test2.prismproof.org {0,50}, test3.prismproof.org {1,100}
</dl>

Each test needs to be done multiple times with a check to see that the balancing works.
