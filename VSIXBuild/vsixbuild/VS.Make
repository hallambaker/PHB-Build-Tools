
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
	VSIXBuild.cs
VSIXBuild.cs : VSIXBuild.command 
	commandparse VSIXBuild.command  /cs  VSIXBuild.cs



prebuildRecurse : 
	cd ..\Goedel.Tool.VSIXBuild && nmake /c /f VS.make prebuild 


postbuildRecurse :
	cd ..\Goedel.Tool.VSIXBuild && nmake /c /f VS.make postbuild 

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	version version.version AssemblyVersion.cs
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget vsixbuild.exe $(LinkFiles) 

