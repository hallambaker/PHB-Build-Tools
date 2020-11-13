
# Supplemental Makefile for Visual Studios Projects
#
# Visual Studio runs tools for most projects but not for shared projects.
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
    Goedel.Tool.Core.dll\
    Goedel.Document.RFCSVG.dll

ToolTargets = \
	main.cs
main.cs : main.command 


#	commandparse main.command  /cs  main.cs



prebuildRecurse : 
	cd ..\Goedel.Document.Markdown && nmake /c /f VS.make prebuild 
	cd ..\Goedel.Document.OpenXML && nmake /c /f VS.make prebuild 
	cd ..\..\DocTools\Goedel.Document.RFC && nmake /c /f VS.make prebuild 
	cd ..\..\DocTools\Goedel.Document.RFCConvert && nmake /c /f VS.make prebuild 


postbuildRecurse :
	cd ..\Goedel.Document.Markdown && nmake /c /f VS.make postbuild 
	cd ..\Goedel.Document.OpenXML && nmake /c /f VS.make postbuild 
	cd ..\..\DocTools\Goedel.Document.RFC && nmake /c /f VS.make postbuild 
	cd ..\..\DocTools\Goedel.Document.RFCConvert && nmake /c /f VS.make postbuild 

# Non shared project, nothing to do
prebuild : prebuildRecurse $(ToolTargets)
	version version.version AssemblyVersion.cs
	

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget rfctool.exe $(LinkFiles) 

