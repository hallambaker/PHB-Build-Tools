
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
    Goedel.Protocol.dll\
    Goedel.Tool.Core.dll

ToolTargets = \
	Document.cs\
	Exceptions.cs\
	GenerateC.cs\
	GenerateCSharp.cs\
	ProtoGen.cs
Document.cs : Document.script 
	gscript Document.script  Document.cs

Exceptions.cs : Exceptions.exceptional 
	exceptional Exceptions.exceptional  /cs  Exceptions.cs

GenerateC.cs : GenerateC.script 
	gscript GenerateC.script  GenerateC.cs

GenerateCSharp.cs : GenerateCSharp.script 
	gscript GenerateCSharp.script  GenerateCSharp.cs

ProtoGen.cs : ProtoGen.gdl 
	goedel3 ProtoGen.gdl  /cs  ProtoGen.cs



prebuildRecurse : 


postbuildRecurse :

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Tool.Protogen.dll

