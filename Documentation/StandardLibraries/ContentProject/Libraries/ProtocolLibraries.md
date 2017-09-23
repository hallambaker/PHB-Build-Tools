<id>98313b54-9855-415f-9cf8-7a3beb669ae7
<version>1
<contenttype>developerConceptualDocument

Goedel.Protocol namespace contains classes that support Web Services and 
data schemas built using the PROTOGEN tool.


#Schema Definition

The Schema definition file has the default extension .protocol and contains the
protocol definition and documentation.

The top level entry in the schema is a Protocol object. This specifies the
namespace in which the generated classes are to be generated and the base name
for the schema and dispatch classes:

~~~~
Protocol Goedel.Account AccountProtocol AccountProtocol
    Using Goedel.Cryptography.Jose 
	Description
		|Mesh/Account protocol schema.
~~~~

A Web service is specified using the Service object. Here the service class name is 
_ AccountService_ and the DNS service prefix is <tt>_mmmaccount._tcp</tt> 
The .Well-Known service prefix is _mmmaccount_ and the base classes for 
requests and responses are _AccountRequest_ and _AccountResponse_

~~~~
    Service AccountService "_mmmaccount._tcp" "mmmaccount" AccountRequest AccountResponse
		Description
			|Every Mesh/Account Service transaction consists of exactly one
			|request followed by exactly one response.
~~~~

The request message must be a subclass of Goedel.Protocol.Request. Request classes
may specify additional protocol specific fields but in this case we use the 
base definition.

~~~~
	Message AccountRequest
		External Goedel.Protocol.Request
		Description
			|Base class for all request messages.
~~~~

The schema definition language distinguishes between Messages and Structures. 
All I can say is it seemed a good idea at the time. It appears that the 
distinction is not actually very useful but is build into the documentation 
part of the tool.

~~~~
	Structure AccountData
		Description
			|The data associated with an account
		String AccountId
			Description
				|The account identifier
~~~~

A Web Service supports a sequence of Transactions. At the moment, the only type
of transaction supported in the tool is a request/response pair. More complex
protocol interactions may be defined by using the Goedel.Protocol methods 
directly.

Here we have a _Create_ transaction which is part of the _Admin_ group. The
request is _CreateRequest_ and the response is _CreateResponse_

~~~~
	Transaction Admin Create CreateRequest CreateResponse
		Description
			|Create new account
~~~~

Finally we have the protocol message(s). The _CreateRequest_ message uses the
_AccountData_ structure defined above.

~~~~
	Message CreateRequest
		Inherits AccountRequest
		Description
			|Create a new account

		Struct AccountData Data
			Description
				|Describes the account to be created
~~~~

The PROTOGEN tool is being documented in an Internet Draft but the current version
is out of date.

#Generated Code

The PROTOGEN tool creates three outputs:

* Server implementation code

* Client implementation code

* Documentation

PROTOGEN generates abstract classes for the base classes of the protocol, service and client
and classes for each of the messages/structures defined in the schema.

The protocol base class contains all the control information used to serialize and 
deserialize protocol messages that is not contained in the message/structure classes.




##Service Implementation

The service class contains the information from the _Service_ object as properties
and contains a sequence of virtual methods implementing each transaction.

The Create transaction is implemented as follows:

~~~~C#
    ...
    public abstract partial class AccountService : Goedel.Protocol.JPCInterface {
	    ...

        public virtual CreateResponse Create (
                CreateRequest Request) {
            return null;
            }
	    }
~~~~

Since the service class is abstract, the service implementer will have to at
minimum subclass this class. To implement a working protocol, they will have
to override each of the transaction dispatch methods with their own code.

###Service Provider

The protogen support libraries perform all the necessary serialization and 
deserialization of messages, interfacing to the Web Server, etc. that is
required to implement the service. The service provider is the glue that connects
the support libraries to the service implementation.

The service provider class contains a multiplexer that receives a data 
stream containing a message and decides which transaction to call:

~~~~C#
    public partial class AccountServiceProvider : Goedel.Protocol.JPCProvider {

		...
		public override Goedel.Protocol.JSONObject Dispatch(JPCSession  Session,  
								Goedel.Protocol.JSONReader JSONReader) {

			JSONReader.StartObject ();
			string token = JSONReader.ReadToken ();
			JSONObject Response = null;

			switch (token) {
			    ...
				case "Create" : {
					var Request = new CreateRequest();
					Request.Deserialize (JSONReader);
					Response = Service.Create (Request);
					break;
					}
~~~~

In the next itteration of the code, the switch will make it possible to hook in a
call to an authorization routine before the transaction is dispatched.


##Server Code

To create a service, we first create an instance of our particular implementation.
This will typically have parameters for defining a persistence store to connect to.

~~~~cs
            var MeshServiceProvider = new MyAccountServiceProvider("example.com");
~~~~

Next we initialize a JPC server (JSON Procedure Call) and register our provider to it.

~~~~cs
            var Server = new JPCServer();
            var HostReg = Server.Add(MeshServiceProvider);
~~~~

A single server can support multiple providers for the same or different services. 
This is very useful when the services provided are linked or multiple versions of 
the same protocol are supported.

Next we create one or more interfaces for the service. A service might be exposed 
as a UDP service as well as HTTP or over QUIC. A web service might be exposed on
multiple DNS addresses. 

~~~~cs
            var Interface = new PublicMeshService(MeshServiceProvider, null);
            var InterfaceReg = HostReg.Add (Interface);
~~~~

Finally, we begin the service:

~~~~cs
                Server.RunBlocking ();
~~~~


##Client Implementation

The client implementation is defined in almost the same way as the Service. The 
difference being that instead of receiving a request and generating a response, 
we are generating a request and waiting for a response.

The client class is a subclass of the service class. Each transaction is mapped
on to a stub that wraps the request/response:

~~~~
    public partial class AccountServiceClient : AccountService {
    ...
        public override CreateResponse Create (
                CreateRequest Request) {
                
            var ResponseData = JPCRemoteSession.Post("Create", Request);
            var Response = CreateResponse.FromJSON(ResponseData.JSONReader(), true);

            return Response;
        }
~~~~

##Portals

To perform a remote transaction, we first create a client for the service we wish to connect to
and then make a request off that client.

To create a client, we make use of a portal class. Here we a client off the default portal:

~~~~
        public Create (string Address, AccountID) {
            Client = AccountPortal.Default.GetService(Address);
			var AccountData = new AccountData() {
                AccountId = AccountID,
                };
            return Client.Create(Request);

            }
~~~~

At the moment, the developer has to create their own portal, this is the default code:

~~~~
        public abstract class AccountPortal {
            public abstract AccountService GetService (string Portal, string Account);
            public static AccountPortal Default { get; set; } 
            }

        public class AccountPortalRemote : AccountPortal {
            public AccountService GetService (string Domain, string Account) {
                var Session = new WebRemoteSession(Domain, AccountService.WellKnown, Account);
                AccountServiceClient = new AccountServiceClient(Session);
                return AccountServiceClient;
                }
            }
~~~~

The reason for this somewhat indirect approach is that it allows the connection to
a service to be redirected for test purposes or to permit some different discovery mechanism
to be used.

For example, when debugging a service implementation, it is generally desirable to be able
to test the service dispatch code by simply connecting the client directly to the service
without serializing and deserializing the requests.
