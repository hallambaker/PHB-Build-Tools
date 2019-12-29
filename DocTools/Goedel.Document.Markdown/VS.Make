
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

LinkFiles = 

ToolTargets = \
	MarkDown.cs\
	MarkParagraph.cs\
	MarkWordParagraph.cs\
	Schema.cs
MarkDown.cs : MarkDown.fsr 
	fsrgen MarkDown.fsr  /cs  MarkDown.cs

MarkParagraph.cs : MarkParagraph.fsr 
	fsrgen MarkParagraph.fsr  /cs  MarkParagraph.cs

MarkWordParagraph.cs : MarkWordParagraph.fsr 
	fsrgen MarkWordParagraph.fsr  /cs  MarkWordParagraph.cs

Schema.cs : Schema.gdl 
	goedel3 Schema.gdl  /cs  Schema.cs



prebuildRecurse : 


postbuildRecurse :

# Shared project, create build rules for custom tools.
prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


postbuildwindows : 
	powershell publishtarget Goedel.Document.Markdown.dll

