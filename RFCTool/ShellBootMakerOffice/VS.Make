
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
#    ..\..\..\..\..\..\Windows\assembly\GAC_MSIL\office\15.0.0.0__71e9bce111e9429c\office.dll\
#    ..\..\..\..\..\..\Windows\assembly\GAC_MSIL\Microsoft.Office.Interop.Excel\15.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.Excel.dll\
#    ..\..\..\..\..\..\Windows\assembly\GAC_MSIL\Microsoft.Office.Interop.PowerPoint\15.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.PowerPoint.dll\
#    ..\..\..\..\..\..\Windows\assembly\GAC_MSIL\Microsoft.Office.Interop.Visio\14.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.Visio.dll\


LinkFiles = \
    Goedel.Command.dll\
    Goedel.FSR.dll\
    Goedel.IO.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll\
    Goedel.Document.Markdown.dll\
    OfficeLib.dll\
    Goedel.Document.OpenXML.dll

prebuild : 
    echo "Do Prebuild"

postbuild : 
	powershell publishtarget bootmaker.exe $(LinkFiles) 


