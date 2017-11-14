
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
    Goedel.Registry.dll\
    Goedel.Document.Markdown.dll\
    Goedel.Document.RFC.dll

prebuild : 
    echo "Do Prebuild"

postbuild : 
	powershell publishtarget Goedel.Document.RFC.Convert.dll


