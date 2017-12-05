
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

LinkFiles = 

ToolTargets = \
	MarkDown.cs\
	MarkParagraph.cs\
	MarkWordParagraph.cs\
	Schema.cs

MarkDown.cs : MarkDown.fsr 
	fsrgen MarkDown.fsr MarkDown.cs

MarkParagraph.cs : MarkParagraph.fsr 
	fsrgen MarkParagraph.fsr MarkParagraph.cs

MarkWordParagraph.cs : MarkWordParagraph.fsr 
	fsrgen MarkWordParagraph.fsr MarkWordParagraph.cs

Schema.cs : Schema.gdl 
	goedel3 Schema.gdl Schema.cs


prebuildRecurse : 


postbuildRecurse :


prebuild : prebuildRecurse $(ToolTargets)

postbuild : postbuildRecurse


