=Configuring the Windows Build environment

To configure the Windows build environment it is necessary to configure the following

* Path to the build scripts
* Developer code signing key (for strong assemblies)
* Nuget package repository (for referenced code)
* Install .Net 4.6.2

==Path to the build scripts

The build environment makes use of some powershell batch files. These require 
some environment variables to be set up and for execution of unsigned
powershell scripts to be enabled.

Control Panel\System and Security\System
Advanced Settings
Environment variables

Path				add in the path to your personal tools directory>
ToolPath			Set to the directory where you want tools to be written
ToolLibraryPath		Set to the directory where you want to copy the libraries (.dll)


===To enable unrestricted shell scripts to run

You need to execute the following line in a Powershell console while running
with admin privileges. However there is a catch, the permissions for 32 and 64 bit 
powershell are separate and there is no indication of which one you are running
so the safest thing is to search for powershell.exe in the system directory
and execute this statement in each one.

~~~~
set-executionpolicy unrestricted -scope localmachine -force
~~~~

A better solution would be to leave signing turned on and to sign the scripts with
a temporary developer key but that isn't practical till after we have the Mesh 
running.

==Developer Code Signing key

The build tools require a signing key to be defined and placed in a container 
called SigningKeyDeveloper

https://msdn.microsoft.com/en-us/library/k5b5tt23.aspx

To create a signing key, you need to either use the developer console or
add the location of the developer tools to your path.

~~~~
C:\Users\hallam>sn -k 2048 SigningKeyDeveloper.snk

Microsoft (R) .NET Framework Strong Name Utility  Version 4.0.30319.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Key pair written to SigningKeyDeveloper.snk

C:\Users\hallam>sn -i SigningKeyDeveloper.snk SigningKeyDeveloper

Microsoft (R) .NET Framework Strong Name Utility  Version 4.0.30319.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Key pair installed into 'SigningKeyDeveloper'
~~~~

==Nuget package repository

This is described in the Nuget configuration file:

 %APPDATA%\Roaming\NuGet\NuGet.Config 

 https://docs.nuget.org/ndocs/consume-packages/configuring-nuget-behavior

 ==Install .Net 4.6.2

 Even though the libraries don't use .Net 4.6.2 at the moment, it is necessary
 to install it in order to avoid issues with Visual Studio. The problem being that
 once a project has been compiled under 4.6.2, VS remembers this and insists
 on being able to build under 4.6.2 even if that is not selected.
