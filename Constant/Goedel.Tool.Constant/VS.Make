
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
	ConstantSchema.cs\
	Generate.cs
ConstantSchema.cs : ConstantSchema.gdl 
	goedel3 ConstantSchema.gdl  /cs  ConstantSchema.cs

Generate.cs : Generate.Script 
	gscript Generate.Script  Generate.cs



prebuildRecurse : 


postbuildRecurse :

# Shared project, create build rules for custom tools.
prebuild : prebuildRecurse $(ToolTargets)
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.Domainer.dll

