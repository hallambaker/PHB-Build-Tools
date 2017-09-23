<id>f49b1505-1206-4ffe-8f23-43f42f61e4bd
<version>4
<contenttype>developerConceptualDocument

The cryptography libraries provide a single consistent high level interface 
to all the cryptographic resources used in the Mathematical Mesh.

# Cryptography Libraries

The Cryptography libraries are organized around the following principal classes

:CryptoCatalog

::A Registry that permits a CryptoProvider or CryptoAlgorithm to be obtained.


:CryptoAlgorithmID

::Enumeration that represents a cryptographic algorithm. This may be a bulk algorithm,
a meta algorithm or a combination of the two. For example, digest and signature or
encryption and exchange.

:CryptoAlgorithm

::Describes a cryptographic algorithm and contains factories for Key and
CryptoProvider classes.

:Key

::Base class for all cryptographic keys, whether symmetric or asymmetric.

:CryptoProvider

::Base class for all classes that perform operations on *CryptoData* using a *Key*

:CryptoProcessor

::Base class for CryptoDataEncoder and CryptoDecoder 

:CryptoData

::Base class for all ciphertext, plaintext and associated data.


##Initialization

One of the core goals of the Mesh project is to enable applications to make direct access
to the security features provided by the host machine. Cryptographic keys should be stored
using all the protections provided by the host, not the lowest common denominator supported
by all the hosts. So even though the libraries are implemented in C# which
generates platform independent code, applications should link to the support library for
the specific platform.

The library Goedel.Cryptography does not provide access to any cryptographic
algorithm providers. To make use of the library it is necessary to link to either 
Goedel.Cryptography.Windows, Goedel.Cryptography.OSX or Goedel.Cryptography.Linux and 
make a call to the corresponding initialize method as follows:

~~~~c#
using Goedel.Mesh.Platform.Windows;
...

namespace foo {
    public void TestInitialize() {
        CryptographyWindows.Initialize(TestMode);
        }
	}
~~~~

It is only necessary to call the initializer once per process. Once the library is 
initialized, further attempts to initialize the library are ignored.


##The Algorithm Catalog

The catalog provides a means of locating CryptoAlgorithm descriptions of
algorithms and factory methods for the corresponding providers. 

The algorithm identifier used inside the libraries is the CryptoAlgorithmID enumeration, 
which may be used to identify a bulk algorithm, a meta algorithm (i.e. signature, 
exchange or keywrap) or a combination of the two.

Additional routines provide means of converting between 
CryptoAlgorithmID identifiers and the identifiers used in encodings such
as OIDs, URIs, PEM names and JSON names.

The catalog is initialized with entries for the algorithms supported by the
platform cryptography providers. This insulates the library code from the
current transition between the providers supported in the .NET Framework, 
.NET Core and .NET Standard libraries.

Note that it is permitted to have more than one provider for a given algorithm in
the catalog. This permits support for cryptographic hardware.

###Discovery by ID or Name

To make use of an algorithm, we may locate a provider by CryptoAlgorithmID which is 
typically obtained from an encoding specific identifier.

The following example shows a JSON algorithm identifier (RSA Signature with
SHA-2-512 digest) being converted to an algorithm identifier which is then
used to obtain an encoder which is used to process the data:

~~~~C#
    var DigestID = "RS512".FromJoseID();
    var Encoder = CryptoCatalog.Default.GetDigest(DigestID);
    var DigestOfData = Encoder.Process(Data);
~~~~

Note that it is not necessary to break out the digest component from the combined
identifier, this is performed automatically by the GetDigest method. 

###Shortcut Discovery

For convenience, the Platform class provides static links to the default
CryptoAlgorithm entries for the most commonly used algorithms:

* SHA-2 256 and 512 bit
* SHA-1 (deprecated)
* HMAC-SHA-2 256 and 512 bit
* AES-CBC 256 bit
* RSA Signature
* RSA Encryption
* DH Exchange

~~~~C#
var Digest = Platform.SHA2_256;
var Result = Digest.Process(TestString);
var Text = BaseConvert.ToBase16String(Result);
~~~~

###Converting between identifiers

For convenience, a set of Extension methods are defined for converting between
ASN.1, XML and JSON identifiers and the library identifiers. These are:

* CryptoAlgorithmID FromXMLID (this string URL)

* CryptoAlgorithmID FromOID(this string OID)

* CryptoAlgorithmID FromJoseID (this string JoseID) 

* string ToOID(this CryptoAlgorithmID ID)

* string ToXMLID(this CryptoAlgorithmID ID)

* string ToJoseID(this CryptoAlgorithmID ID) 


###Operations on Algorithm Identifiers

Extract specific parts of an algorithm identifier:

* static CryptoAlgorithmID Bulk (this CryptoAlgorithmID ID) 

* static CryptoAlgorithmID Meta(this CryptoAlgorithmID ID)

To extract a specific algorithm type:

* static CryptoAlgorithmID Digest(this CryptoAlgorithmID ID) 

* static CryptoAlgorithmID MAC(this CryptoAlgorithmID ID)

* static CryptoAlgorithmID Encryption(this CryptoAlgorithmID ID)

* static CryptoAlgorithmID Mode(this CryptoAlgorithmID ID)

* static CryptoAlgorithmID Signature(this CryptoAlgorithmID ID)

* static CryptoAlgorithmID Exchange(this CryptoAlgorithmID ID)

The Meta and/or Bulk entries in an Algorithm identifier may have the value Default.
The CryptoCatalog class provides the following methods override default entries to
the defaults for that type of algorithm in the catalog:

* SignatureDefaults

* EncryptionDefaults

##Creating Keys

Key creation is a two step process.

1. A provider is created for the desired algorithm

2. Generation of a keypair with the specified key size is requested of the provider.

~~~~C#
Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch);
Encrypter.Generate(KeySecurity.Ephemeral, KeySize: 2048);
~~~~

The KeySecurity enumeration specifies the level of security to be applied to a key,
whether it should be persisted in long term storage and whether it may be exported or
not.

Marking the key ephemeral in the above example means that it will be deleted after use
and the API does not support export. Note however that while it is highly desirable for 
a platform to support hardware measures that prevent export of keys marked as such, most
platforms do not provide that level of security.

##JSON Object Signing and Encryption

The Goedel.Cryptography.Jose namespace supports JSON Object Signing and Encryption.

###Simple signing and encryption

Convenience routines are provided to sign and encrypt compact data. 
These may be used to operate on:

* Binary data (byte[])

* UTF8 Encoded string (string)

* JSON Object (Class inheriting from JSONObject)

Implementations should make use of the convenience routines wherever possible as this 
will allow them to automatically take advantage of implementations that make use of 
streaming when this is implemented.

Signing:

~~~~C#
var JWS = new JoseWebSignature(TestString, SignerKeyPair);
~~~~

Encryption:

~~~~C#
var JWE = new JoseWebEncryption(TestString, EncrypterKeyPair);
~~~~

Signing and Encryption:

~~~~C#
var JWES = new JoseWebEncryption(TestString, EncrypterKeyPair, SignerKeyPair);
~~~~

###Multiple Signers

To add a signer to an existing JWS, the AddSignature method is used:

~~~~C#
AddSignature(SigningKey);
~~~~

###Multiple Recipients

To add recipient to an existing JWE, the AddSignature method is used:

~~~~C#
AddRecipient(EncryptionKey);
~~~~

###Signing and Encrypting streamed data

At present the libraries do not support processing of streamed data. This is due to 
an intentional decision to defer this work until a suitable format for this type of data 
has been developed.


