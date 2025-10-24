
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
    Goedel.Registry.dll\
    Goedel.Utilities.dll

ToolTargets = \
	FrameData.cs\
	Exceptions.cs\
	FramePages.cs
FrameData.cs : FrameData.gdl 
	goedel3 FrameData.gdl  /cs  FrameData.cs

Exceptions.cs : Exceptions.exceptional 
	exceptional Exceptions.exceptional  /cs  Exceptions.cs

FramePages.cs : FramePages.script 
	gscript FramePages.script  FramePages.cs



prebuildRecurse : 


postbuildRecurse :

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Html.exe $(LinkFiles) 

