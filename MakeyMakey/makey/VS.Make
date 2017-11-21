
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

LinkFiles = \
    Goedel.FSR.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll

ToolTargets = \
	Commands.command

Commands.cs :  Commands.command
	commandparse Commands.command /cs=Commands.cs

prebuildRecurse : 
	cd ..\Goedel.Tool.Makey && nmake /c /f VS.make prebuild

prebuild : prebuildRecurse $(ToolTargets)
    echo Completed prebuild $(MAKEDIR)

postbuildRecurse : 
	cd ..\Goedel.Tool.Makey && nmake /c /f VS.make postbuild

postbuild : postbuildRecurse
    echo Completed postbuild $(MAKEDIR)

# powershell publishtarget Goedel.Tool.Makey.dll


