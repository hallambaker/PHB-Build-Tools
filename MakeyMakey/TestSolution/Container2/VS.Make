
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

ToolTargets = 


prebuildRecurse : \
	cd Container2\..\SharedProject && nmake /c /f VS.make prebuild 

postbuildRecurse : \
	cd Container2\..\SharedProject && nmake /c /f VS.make postbuild 

prebuild : prebuildRecurse $(ToolTargets)
    echo Completed prebuild $(MAKEDIR)

postbuild : postbuildRecurse
    echo Completed postbuild $(MAKEDIR)


