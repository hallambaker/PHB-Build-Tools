
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
	FSRSchema.cs\
	GenerateC.cs\
	GenerateCS.cs
FSRSchema.cs : FSRSchema.gdl 
	goedel3 FSRSchema.gdl  /cs  FSRSchema.cs

GenerateC.cs : GenerateC.script 
	gscript GenerateC.script  GenerateC.cs

GenerateCS.cs : GenerateCS.script 
	gscript GenerateCS.script  GenerateCS.cs



prebuildRecurse : 


postbuildRecurse :

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	version version.version AssemblyVersion.cs
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.FSRGen.dll

