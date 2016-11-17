<id>f49b1505-1206-4ffe-8f23-43f42f61e4bd
<version>4
<contenttype>developerConceptualDocument

The cryptography libraries provide a single consistent high level interface 
to all the cryptographic resources used in the Mathematical Mesh.

# Cryptography Libraries

The Cryptography libraries are organized around the following principal classes

:CryptoCatalog

::A Registry that permits a CryptoProvider or CryptoAlgorithm to be obtained.

:CryptoAlgorithm

::Describes a cryptographic algorithm and contains factories for Key and
CryptoProvider classes.

:Key

::Base class for all cryptographic keys, whether symmetric or asymmetric.

:CryptoData

::Base class for all ciphertext, plaintext and associated data.

:CryptoProvider

::Base class for all classes that perform operations on *CryptoData* using a *Key*


##Initialization

The portable library Goedel.Cryptography does not provide access to any cryptographic
algorithm providers. To make use of the library it is necessary to link to either 
Goedel.Cryptography.Framework or Goedel.Cryptography.Universal and make a
call to the corresponding initialize method as follows:

~~~~
        public void TestInitialize() {
            Framework.Cryptography.Initialize();
            }
~~~~

It is only necessary to call the initializer once per process. Once the library is 
initialized, further attempts to initialize the library are ignored.

##Discovering Algorithms


###Discovery by Name

###Shortcut Discovery

For convenience, the class Goedel.Cryptography.Platform contains convenience
shortcuts for the default implementation of the most commonly used algorithms.

~~~~
~~~~


##Creating Keys

###Public Key Pair


##Creating a CryptoProvider


##Performing Operations

###Digest Operation

###Public Key Encryption Operation

###Public Key Signed Encryption Operation
