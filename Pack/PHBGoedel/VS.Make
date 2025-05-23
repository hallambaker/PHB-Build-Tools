
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
    Goedel.Tool.Command.dll\
    Goedel.Tool.Exceptional.dll\
    Goedel.Tool.FSRGen.dll\
    Goedel.Tool.Schema.dll\
    Goedel.Tool.Script.dll\
    Goedel.FSR.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll

prebuild : 
    echo "Do Prebuild"

postbuild : 
	powershell publishtarget PHBGoedel.dll


