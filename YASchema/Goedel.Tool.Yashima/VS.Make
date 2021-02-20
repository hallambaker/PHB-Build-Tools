
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
	Generate.cs


YaschemaSchema.cs : YaschemaSchema.gdl 
	goedel3 YaschemaSchema.gdl  /cs  YaschemaSchema.cs

Generate.cs : Generate.Script 
	gscript Generate.Script  Generate.cs



prebuildRecurse : 


postbuildRecurse :

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	version version.version AssemblyVersion.cs
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.Yaschema.dll

