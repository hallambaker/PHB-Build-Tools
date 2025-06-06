
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
    Goedel.IO.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll

ToolTargets = \
	Exceptions.cs\
	ParserGenerator.cs
Exceptions.cs : Exceptions.exceptional 
	exceptional Exceptions.exceptional  /cs  Exceptions.cs

ParserGenerator.cs : ParserGenerator.script 
	gscript ParserGenerator.script  ParserGenerator.cs



prebuildRecurse : 


postbuildRecurse :

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	version version.version AssemblyVersion.cs
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.Schema.dll

