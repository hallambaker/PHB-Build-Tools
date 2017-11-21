
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

LinkFiles = \
    Goedel.FSR.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll

ToolTargets = \
	 GenerateMakefile.cs

GenerateMakefile.cs :  GenerateMakefile.script
	gscript GenerateMakefile.script GenerateMakefile.cs

prebuild : $(ToolTargets)
    echo "Completed prebuild"

postbuild : 
    echo "Completed postbuild"

# powershell publishtarget Goedel.Tool.Makey.dll


