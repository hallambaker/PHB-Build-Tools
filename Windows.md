=Configuring the Windows Build environment

To configure the Windows build environment it is necessary to configure the following

* Developer code signing key (for strong assemblies)
* Nuget package repository (for referenced code)


==Developer Code Signing key

(Put it in the container described in AssemblyInfo.cs)


==Nuget package repository

This is described in the Nuget configuration file:

 %APPDATA%\Roaming\NuGet\NuGet.Config 


 https://docs.nuget.org/ndocs/consume-packages/configuring-nuget-behavior

