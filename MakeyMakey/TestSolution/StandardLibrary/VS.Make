
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

LinkFiles = 

ToolTargets = \
	CommandLex.cs\
	CommandSplit.cs\
	Exceptions.cs\
	Main.cs

CommandLex.fsm :  CommandLex.cs
	fsrgen CommandLex.fsm CommandLex.cs

CommandSplit.fsm :  CommandSplit.cs
	fsrgen CommandSplit.fsm CommandSplit.cs

Exceptions.exceptional :  Exceptions.cs
	exceptional Exceptions.exceptional Exceptions.cs

Main.command :  Main.cs
	commandparse Main.command Main.cs


prebuildRecurse : 

postbuildRecurse : 

prebuild : prebuildRecurse $(ToolTargets)
    echo Completed prebuild $(MAKEDIR)

postbuild : postbuildRecurse
    echo Completed postbuild $(MAKEDIR)


