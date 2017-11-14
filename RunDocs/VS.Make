
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
    Goedel.IO.dll\
    Goedel.Utilities.dll\
    Goedel.Document.Markdown.dll\
    OfficeLib.dll\
    Goedel.Document.OpenXML.dll\
    Goedel.Document.RFC.Convert.dll\
    Goedel.Document.Test.dll\
    Goedel.Document.RFC.dll

prebuild : 
    echo "Do Prebuild"

postbuild : 
	powershell publishtarget RunDocs.exe $(LinkFiles) 


