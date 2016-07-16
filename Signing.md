=Signing Assemblies

The VSIX integration requires assemblies to be signed. To do this we
need to create a signing key.

Each developer and each development machine should have their own 
separate keys and developer keys should be separate from release 
keys.

All keys are stored in the platform CSP under the following names

* SigningKeyDeveloper
* SigningKeyRelease

==Creating a developer key pair

Key pairs are created using the sn.exe tool. This is not in the
default command tool path. So the easiest way to run it is to
start the Visual Studio Developer Console. This is installed in the
Visual Studio start menu folder.

Note that since the tool runs in the windows systemn directory by 
default, you will need to cd to a different directory.

To create a key pair:

~~~~
cd c:\Users\<homedirectory>
sn -k 2048 SigningKeyDeveloper.snk
~~~~


==Naming the developer key pair

The next step is to name the developer key pair. To do this we use the
strong name tool again.

~~~~
sn -i SigningKeyDeveloper.snk SigningKeyDeveloper
del SigningKeyDeveloper.snk
~~~~

==Setting permissions on the key

This last piece is serious stupid. By default, Visual Studio uses machine
keys. The keys themselves persist after a reboot but the access credential
does not.

So you need to go into the key store, work out which of the files has the keys 
you just created (creation date) and change the permissions to grant 
yourself access rights.

C:\ProgramData\Microsoft\Crypto\RSA\MachineKeys

http://stackoverflow.com/questions/32745415/why-do-i-lose-my-key-container-when-i-reboot


==Note Well

It is essential that the key be exactly 2048 bits, no more, no less. This
is because we are going to be using the resigning feature to generate 
release builds.


