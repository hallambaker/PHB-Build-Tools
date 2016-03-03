

#Mesh Portal Service  Reference
SRV Prefix:

:_mmm._tcp

HTTP Well Known Service Prefix:

:/.well-known/mmm

Every Mesh Portal Service transaction consists of exactly one
request followed by exactly one response.
Mesh Service transactions MAY cause modification
of the data stored in the Mesh Portal or the Mesh itself
but do not cause changes to the connection state. The protocol
itself is thus idempotent. There is no set sequence
in which operations are required to be performed. It is not
necessary to perform a Hello transaction prior to
a ValidateAccount, Publish or any other transaction.

##Request Messages
A Mesh Portal Service request consists of a payload object
that inherits from the MeshRequest class. When using the 
HTTP binding, the request MUST specify the portal DNS
address in the HTTP Host field. 

###Message: MeshRequest

Base class for all request objects.


Portal: String (Optional)

:Name of the Mesh Portal Service to which the request 
is directed.

##Response Messages
A Mesh Portal Service response consists of a payload object that
inherits from the MeshResponse class. When using the
HTTP binding, the response SHOULD
report the Status response code in the HTTP response 
message. However the response code returned in the
payload object MUST always be considered authoritative.

###Message: MeshResponse

Base class for all responses. Contains only the
status code and status description fields.

A service MAY return either the response message specified
for that transaction or any parent of that message. 
Thus the MeshResponse message MAY be returned in response 
to any request.


Status: Integer (Optional)

:Status return code. The SMTP/HTTP scheme of 2xx = Success,
3xx = incomplete, 4xx = failure is followed.

StatusDescription: String (Optional)

:Text description of the status return code for debugging 
and log file use.

###Successful Response Codes
The following response codes are returned when a
transaction has completed successfully.

201 SuccessOK
:Operation completed successfully

201 SuccessCreated
:Operation completed successfully, new data item created

202 SuccessCreated
:Operation completed successfully, data item was updated

###Warning Response Codes
The following response codes are returned when a
transaction did not complete because the target
service has been redirected.

In the case that a redirect code is returned, the 
StatusDescription field contains the URI of the 
new service. Note however that the redirect location 
indicated in a status response might be incorrect
or even malicious and cannot be considered 
trustworthy without appropriate authentication.

303 RedirectPermanent
:Service has been permanently moved

307 RedirectTemporary
:Service has been temporarily moved

###Error Response Codes
A response code in the range 400-499 is
returned when the service was able to process the
transaction but the transaction resulted in an error.

401 ClientUnauthorized
:Client is not authorized to perform specified request

404 NotFound
:The requested object could not be found.

409 AlreadyExists
:The requested object already exists.

###Failure Response Codes
A response code in the range 500-599 is
returned when the service was unable to process the
transaction but the transaction due to an internal
failure.

500 ServerInternal
:An internal error occurred at the server

503 ServerOverload
:The server cannot handle the request as it is overloaded

##Imported Objects
The Mesh Service protocol makes use of JSON objects
defined in the JOSE Signatgure and Encryption specifications.

##Common Structures
The following common structures are used in the protocol
messages:

###Structure: Version

Describes a protocol version.


Major: Integer (Optional)

:Major version number of the service protocol. A higher

Minor: Integer (Optional)

:Minor version number of the service protocol.

Encodings: Encoding [0..Many]

:Enumerates alternative encodings (e.g. ASN.1, XML, JSON-B)
supported by the service. If no encodings are specified, the
JSON encoding is assumed.

URI: String [0..Many]

:The preferred URI for this service. This MAY be used to effect
a redirect in the case that a service moves.

###Structure: Encoding

Describes a message content encoding.


ID: String [0..Many]

:The IANA encoding name

Dictionary: String [0..Many]

:For encodings that employ a named dictionary for tag or data
compression, the name of the dictionary as defined by that 
encoding scheme. 	

###Structure: KeyValue

Describes a Key/Value structure used to make queries
for records matching one or more selection criteria.


Key: String (Optional)

Value: String (Optional)

#Transactions


##Transaction: Hello

Request: HelloRequest

Response:HelloResponse

Report service and version information. 

The Hello transaction provides a means of determining which protocol
versions, message encodings and transport protocols are supported by
the service.

###Message: HelloRequest

* Inherits: MeshRequest

[None]

###Message: HelloResponse

* Inherits: MeshResponse


Version: Version (Optional)

:Enumerates the protocol versions supported

Alternates: Version [0..Many]

:Enumerates alternate protocol version(s) supported

##Transaction: ValidateAccount

Request: ValidateRequest

Response:ValidateResponse

Request validation of a proposed name for a new account.

For validation of a user's account name during profile creation.

###Message: ValidateRequest

* Inherits: MeshRequest

Describes the properties of the 


Account: String (Optional)

:Account name requested

Reserve: Boolean (Optional)

:If true, request a reservation for the specified account name.
Note that the service is not obliged to honor reservation 
requests.

Language: String [0..Many]

:List of ISO language codes in order of preference. For creating
explanatory text.

###Message: ValidateResponse

* Inherits: MeshResponse


Valid: Boolean (Optional)

Minimum: Integer (Optional)

InvalidCharacters: String (Optional)

:A list of characters from the requested account that the service 
does not accept in account names.

Reason: String (Optional)

:Text explaining the reason an account name was rejected.

##Transaction: CreateAccount

Request: CreateRequest

Response:CreateResponse

Request creation of a new mesh account.

Unlike a profile, a mesh account is specific to a particular 
Mesh portal. A mesh account must be created and accepted before
a profile can be published.

###Message: CreateRequest

* Inherits: MeshRequest


Account: String (Optional)

:Account name requested

###Message: CreateResponse

* Inherits: MeshResponse

[None]

##Transaction: Get

Request: GetRequest

Response:GetResponse

Search for data in the mesh that matches a set of properties
described by a sequence of key/value pairs.

###Message: GetRequest

* Inherits: MeshRequest


Identifier: String (Optional)

:Lookup by profile ID

Account: String (Optional)

:Lookup by Account ID

KeyValues: KeyValue [0..Many]

:List of KeyValue pairs specifying the conditions to be met

NotBefore: DateTime (Optional)

NotOnOrAfter: DateTime (Optional)

Multiple: Boolean (Optional)

:If true return multiple responses if available

###Message: GetResponse

* Inherits: MeshResponse

[None]

##Transaction: GetRecords

Request: GetRequest

Response:GetRecordsResponse

Search for data in the mesh that matches a set of properties
described by a sequence of key/value pairs. If matching
entries are found, the complete Mesh records including 
metadata are returned.

###Message: GetRecordsResponse

* Inherits: MeshResponse


DataItems: DataItem [0..Many]

:List of mesh data records matching the request.

##Transaction: Publish

Request: PublishRequest

Response:PublishResponse

Publish a profile or key escrow entry to the mesh.

###Message: PublishRequest

* Inherits: MeshRequest

[None]

###Message: PublishResponse

* Inherits: MeshResponse

[None]

##Transaction: Status

Request: StatusRequest

Response:StatusResponse

Request the current status of the mesh as seen by the portal to which it
is directed.

The response to the status request contains the last signed checkpoint
and proof chains for each of the peer portals that have been checkpointed.

[Not currently implemented]

###Message: StatusRequest

* Inherits: MeshRequest

[None]

###Message: StatusResponse

* Inherits: MeshResponse


LastWriteTime: DateTime (Optional)

:Time that the last write update was made to the Mesh

LastCheckpointTime: DateTime (Optional)

:Time that the last Mesh checkpoint was calculated.

NextCheckpointTime: DateTime (Optional)

:Time at which the next Mesh checkpoint should be calculated.

CheckpointValue: String (Optional)

:Last checkpoint value.

##Transaction: ConnectStart

Request: ConnectStartRequest

Response:ConnectStartResponse

Request connection of a new device to a mesh profile

###Message: ConnectStartRequest

* Inherits: MeshRequest




SignedRequest: SignedConnectionRequest (Optional)

:

AccountID: String (Optional)

:

###Message: ConnectStartResponse

* Inherits: MeshRequest




SignedConnectionResult: String (Optional)

:

##Transaction: ConnectStatus

Request: ConnectStatusRequest

Response:ConnectStatusResponse

Request status of pending connection request of a new device 
to a mesh profile

###Message: ConnectStatusRequest

* Inherits: MeshRequest




AccountID: String (Optional)

:

DeviceID: String (Optional)

:

###Message: ConnectStatusResponse

* Inherits: MeshRequest




Result: SignedConnectionResult (Optional)

:

##Transaction: ConnectPending

Request: ConnectPendingRequest

Response:ConnectPendingResponse

Request status of pending connection request of a new device 
to a mesh profile

###Message: ConnectPendingRequest

* Inherits: MeshRequest




AccountID: String (Optional)

:

###Message: ConnectPendingResponse

* Inherits: MeshRequest




Pending: SignedConnectionRequest [0..Many]

:

##Transaction: ConnectComplete

Request: ConnectCompleteRequest

Response:ConnectCompleteResponse

Request status of pending connection request of a new device 
to a mesh profile

###Message: ConnectCompleteRequest

* Inherits: MeshRequest




Result: SignedConnectionResult (Optional)

:

AccountID: String (Optional)

:

###Message: ConnectCompleteResponse

* Inherits: MeshRequest



[None]

##Transaction: Transfer

Request: TransferRequest

Response:TransferResponse

Request a bulk transfer of the log between the specified transaction
identifiers. Requires appropriate authorization

[Not currently implemented]

###Message: TransferRequest

* Inherits: MeshRequest


NotBefore: DateTime (Optional)

:
Until: DateTime (Optional)

:
After: String (Optional)

:
MaxEntries: Integer (Optional)

:
MaxBytes: Integer (Optional)

:
###Message: TransferResponse

* Inherits: MeshResponse

[None]

