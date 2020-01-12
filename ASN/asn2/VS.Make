
# Supplemental Makefile for Visual Studios Projects
#
# Visual Studio runs tools for most projects but not for shared projects.
# 
# Prebuild items supported:
#   [None currently]
# 
# PostBuild items supported:
#   * Copy library to locations on disk
#   * Build ilMerge executables
#   * Postprocess VSIX projects
#   * Copy executables


.PHONY : all always clean install publish prebuild prebuildRecurse postbuild postbuildRecurse

MSBuildThisFileDirectory = 

LinkFiles = \
    Goedel.Tool.Core.dll

ToolTargets = \
	ASN2.cs
ASN2.cs : ASN2.command 
	commandparse ASN2.command  /cs  ASN2.cs



prebuildRecurse : 
	cd ..\Goedel.Tool.ASN && nmake /c /f VS.make prebuild 
	cd ..\..\..\mmm\Libraries\Goedel.ASN && nmake /c /f VS.make prebuild 


postbuildRecurse :
	cd ..\Goedel.Tool.ASN && nmake /c /f VS.make postbuild 
	cd ..\..\..\mmm\Libraries\Goedel.ASN && nmake /c /f VS.make postbuild 

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	version version.version AssemblyVersion.cs
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget asn2.exe $(LinkFiles) 

