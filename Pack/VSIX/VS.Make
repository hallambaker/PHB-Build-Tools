
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

LinkFiles = \
    Goedel.Tool.Library.dll\
    Goedel.Tool.ASN-Framework.dll\
    Goedel.Tool.Command-Framework.dll\
    RFCTools-Framework.dll\
    Goedel.Tool.Domainer-Framework.dll\
    Goedel.Tool.Exceptional-Framework.dll\
    Goedel.Tool.FSRGen-Framework.dll\
    Goedel.Tool.Schema-Framework.dll\
    Goedel.Tool.Script-Framework.dll\
    Goedel.Tool.Makey-Framework.dll\
    Goedel.Tool.Protogen-Framework.dll\
    Goedel.Tool.RegistryConfig-Framework.dll\
    Goedel.Tool.VSIXBuild-Framework.dll

ToolTargets = \
	GoedelBinding.cs

GoedelBinding.cs : GoedelBinding.vsixbuild 
	vsixbuild GoedelBinding.vsixbuild  GoedelBinding.cs


prebuildRecurse : 


postbuildRecurse :


prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget VSIX.dll

