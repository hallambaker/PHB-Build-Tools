
# Supplemental Makefile for Visual Studios Projects
#
# Visual Studio is responsible for managing rules for production of
# code but does not provide support for pre and post build actions.
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
    Goedel.IO.dll\
    Goedel.Tool.Shell.dll

ToolTargets = \
	Command.cs

Command.cs : Command.command 
	commandparse Command.command  Command.cs


prebuildRecurse : 
	cd ..\Goedel.Tool.Command && nmake /c /f VS.make prebuild 


postbuildRecurse :
	cd ..\Goedel.Tool.Command && nmake /c /f VS.make postbuild 


prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget commandparse.exe $(LinkFiles) 

