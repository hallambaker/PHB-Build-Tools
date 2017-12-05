
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
	FSRSchema.cs\
	GenerateC.cs\
	GenerateCS.cs

FSRSchema.cs : FSRSchema.gdl 
	goedel3 FSRSchema.gdl FSRSchema.cs

GenerateC.cs : GenerateC.script 
	gscript GenerateC.script GenerateC.cs

GenerateCS.cs : GenerateCS.script 
	gscript GenerateCS.script GenerateCS.cs


prebuildRecurse : 


postbuildRecurse :


prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


