
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
    Goedel.Command.dll\
    Goedel.Protocol.Framework.dll\
    Goedel.Registry.dll\
    Goedel.Utilities.dll\
    Goedel.Trace.Server.dll\
    Goedel.Trace.dll

prebuild : 
    echo "Do Prebuild"

postbuild : 
	powershell publishtarget TraceServer.exe $(LinkFiles) 


