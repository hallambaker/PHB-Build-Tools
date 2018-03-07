#DNS Administration policies

DNS has been around for 40 years. It is my belief that just as high level languages
tame the complexities of machine code, a high level language is required for DNS that
like a modern structured programming language, guides the administrator to best
practices.

##Domains, Hosts and Services

When DNS was first designed, no distinction was made between a host and a service 
running on the host. People quickly started adopting conventions such as calling the
mail server 'mail.example.com' and the FTP server 'ftp.example.com' but these were 
no different from host names as far as configuration was concerned.

Later, the MX and SRV records were added to the DNS. Unfortunately, some political
nonsense meant that the value of these approaches was not recognized until long after
it was to late to retrofit them to the two killer applications of the Internet, mail
and web.

Making service naming regular and predictable makes it easier for users to guess what
address to use to configure an application and the simplest approach of all is for
the user to only have one name to remember (e.g. example.com) that will allow them to
get to any service they need.

The starting point for bringing sanity to DNS management is to enforce a separation 
between hosts and services such that a domain name is associated with one or the other 
but never both. A host is a machine with a unique IP address that provides an Internet 
service. An Internet service is an interface to a set of server proceses running on one or 
more hosts that represent the same logical resource.

In very rare circumstances, it makes sense for multiple versions of a 
particular service to be offered in a particular domain. We might want to duplicate 
all the facilities of example.com in test.example.com for testing. Since I see no
particular advantage to that approach over creating a new domain test-example.com, 
I am ignoring this as a corner case.

While it is the case that some of the features that are supported in the configuration
described here cannot be made use of by any existing client, it is hoped that providing
the information that an intelligent client would require will help bring such software into
being.

### Hosts

A host is a machine with a unique IP address that provides an Internet service. The 
only records that are ever provisioned for a host are A/AAAA records and TXT records. 
A given machine may host multiple domains and have multiple IP addresses associated 
with it but never has more than one A record or more than one AAAA record.

~~~~
host1.example.com.              IN  A    10.7.198.158
host2.example.com.              IN  A    10.7.198.159
~~~~

Host1 will be our mail server and so requires an SPF record to send outbound mail. 
Note that as with DKIM, this is an authorization granted to the host and not the service
running on the host.

~~~~
host1.example.com.              IN  TXT    "v=spf1 108.7.198.158 ?all" 
~~~~

If we are going to do DKIM or DANE, our records for these will go here.

### Services

An Internet service is an interface to a set of server proceses running on one or 
more hosts that represent the same logical resource.

Traditionally, each Internet service was assigned a unique port number. This apporach
is no longer viable as the number of services in use is much larger than the available
port space. This led to the invention of the SRV record.

For each service we create two sets of records

* A set of SRV and TXT records describing the service in a consistent fashion.

* A set of A/AAAA and MX records to support legacy clients.

This means that we end up creating SRV records for protocols such as SMTP and HTTP
that will not be used by any existing client. This is intentional because SRV has clear
advantages over the existing approach and providing support for new clients that 
may be developed costs us nothing.

The SRV record configuration is specified by RFC ????.

~~~~
_splunge._tcp.example.com.      IN  SRV  1 1 80 host1.example.com.
_splunge._tcp.example.com.      IN  SRV  1 1 80 host2.example.com.
~~~~

For a new service 'SPLUNGE' we add an additional A record for each host:

~~~~
splunge.example.com.            IN  A    10.7.198.158
splunge.example.com.            IN  A    10.7.198.159
~~~~

It will quickly be appreciated that this configuration is really not sensible for 
a production site unless supported by tools. 

####Mail

For a mail forwarder, we add an MX record:

~~~~
_smtp._tcp.hallambaker.com.     IN  SRV  1 1 80 host1.example.com.
example.com.                    IN  MX   1 host1.example.com.
~~~~

Note that the SPF and DKIM records are configured at the host level.

####Web

Web is complicated by the fact that we want example.com to resolve to our Web host.
We thus end up with four records for each host

~~~~
example.com.                    IN  A    10.7.198.158
www.example.com.                IN  A    10.7.198.158
http.example.org.               IN  A    108.7.198.158
https.example.org.              IN  A    108.7.198.158
_http._tcp.example.com.         IN  SRV  1 1 80 host1.example.com.
_https._tcp.example.com.        IN  SRV  1 1 443 host1.example.com.
example.com.                    IN  A    10.7.198.159
www.example.com.                IN  A    10.7.198.159
http.example.org.               IN  A    108.7.198.159
https.example.org.              IN  A    108.7.198.159
_http._tcp.example.com.         IN  SRV  1 1 80 host2.example.com.
_https._tcp.example.com.        IN  SRV  1 1 443 host2.example.com.
~~~~

###Domains

There is a final set of records that pertain to the domain itself. At present, the only
such record is CAA:

~~~~
example.org.    IN     CAA 0 issue "comodoca.com"
example.org.    IN     CAA 0 issuewild  ";"
example.org.    IN     CAA 0 iodef "mailto:hostmaster@hallambaker.com"
~~~~


