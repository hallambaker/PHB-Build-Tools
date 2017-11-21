
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
	$(MSBuildThisFileDirectory)CommandLex.cs\
	$(MSBuildThisFileDirectory)CommandSplit.cs\
	$(MSBuildThisFileDirectory)Exceptions.cs\
	$(MSBuildThisFileDirectory)Main.cs

$(MSBuildThisFileDirectory)CommandLex.fsm :  $(MSBuildThisFileDirectory)CommandLex.cs
	fsrgen $(MSBuildThisFileDirectory)CommandLex.fsm $(MSBuildThisFileDirectory)CommandLex.cs

$(MSBuildThisFileDirectory)CommandSplit.fsm :  $(MSBuildThisFileDirectory)CommandSplit.cs
	fsrgen $(MSBuildThisFileDirectory)CommandSplit.fsm $(MSBuildThisFileDirectory)CommandSplit.cs

$(MSBuildThisFileDirectory)Exceptions.exceptional :  $(MSBuildThisFileDirectory)Exceptions.cs
	exceptional $(MSBuildThisFileDirectory)Exceptions.exceptional $(MSBuildThisFileDirectory)Exceptions.cs

$(MSBuildThisFileDirectory)Main.command :  $(MSBuildThisFileDirectory)Main.cs
	commandparse $(MSBuildThisFileDirectory)Main.command $(MSBuildThisFileDirectory)Main.cs


prebuildRecurse : 

postbuildRecurse : 

prebuild : prebuildRecurse $(ToolTargets)
    echo Completed prebuild $(MAKEDIR)

postbuild : postbuildRecurse
    echo Completed postbuild $(MAKEDIR)


