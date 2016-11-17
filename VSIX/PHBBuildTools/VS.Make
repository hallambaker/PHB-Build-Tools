
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
    Goedel.Tool.ASN.dll\
    Goedel.Tool.Domainer.dll\
    Goedel.ASN.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll\
    Goedel.Tool.ProtoGen.dll\
    Goedel.Tool.RegistryConfig.dll\
    libTrojanScript.dll\
    Goedel.Tool.VSIXBuild.dll

prebuild : 
    echo "Do Prebuild"

postbuild : 
	powershell publishtarget PHBBuildTools.dll


