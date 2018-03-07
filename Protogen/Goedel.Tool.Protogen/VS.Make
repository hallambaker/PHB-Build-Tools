
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
	Document.cs\
	GenerateC.cs\
	GenerateCSharp.cs\
	ProtoGen.cs

Document.cs : Document.script 
	gscript Document.script  Document.cs

GenerateC.cs : GenerateC.script 
	gscript GenerateC.script  GenerateC.cs

GenerateCSharp.cs : GenerateCSharp.script 
	gscript GenerateCSharp.script  GenerateCSharp.cs

ProtoGen.cs : ProtoGen.gdl 
	goedel3 ProtoGen.gdl  /cs  ProtoGen.cs


prebuildRecurse : 


postbuildRecurse :


prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.Protogen.dll

