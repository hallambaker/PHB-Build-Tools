Class Shell.Annotate Shell
	Brief		"Process XML2RFC to create a HTML file for annotations"


	About "about"


	Command SingleFile "file"
		DefaultCommand
		Parameter InputFile		"input"		ExistingFile
		Parameter OutputFile	"output"	NewFile
			Default "html"