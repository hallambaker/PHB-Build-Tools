
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
	Exceptional.cs\
	GenerateCS.cs
Exceptional.cs : Exceptional.gdl 
	goedel3 Exceptional.gdl  /cs  Exceptional.cs

GenerateCS.cs : GenerateCS.script 
	gscript GenerateCS.script  GenerateCS.cs



prebuildRecurse : 


postbuildRecurse :

# Shared project, create build rules for custom tools.
prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.Exceptional.dll

