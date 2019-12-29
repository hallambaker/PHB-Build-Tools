
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

LinkFiles = 

ToolTargets = \
	Commands.cs
Commands.cs : Commands.command 
	commandparse Commands.command  /cs  Commands.cs



prebuildRecurse : 
	cd ..\Goedel.Tool.Schema && nmake /c /f VS.make prebuild 


postbuildRecurse :
	cd ..\Goedel.Tool.Schema && nmake /c /f VS.make postbuild 

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget goedel3.exe $(LinkFiles) 

