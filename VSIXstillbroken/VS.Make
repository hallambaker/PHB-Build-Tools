
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
	Exceptions - Copy.cs\
	Exceptions.cs\
	Headers - Copy.cs\
	Headers.cs

Exceptions - Copy.cs : Exceptions - Copy.exceptional 
	exceptional Exceptions - Copy.exceptional  /cs  Exceptions - Copy.cs

Exceptions.cs : Exceptions.exceptional 
	exceptional Exceptions.exceptional  /cs  Exceptions.cs

Headers - Copy.cs : Headers - Copy.script 
	gscript Headers - Copy.script  Headers - Copy.cs

Headers.cs : Headers.script 
	gscript Headers.script  Headers.cs


prebuildRecurse : 


postbuildRecurse :


prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget VSIXstillbroken.dll

