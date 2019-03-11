Class Shell.Bootmaker Shell
	Brief		"Process Markdown to create a Bootstrap HTML site"


	About "about"

	Command Site "site"
		DefaultCommand
		Parameter InputDir		"input"		ExistingFile
		Parameter OutputDir		"output"	NewFile

		Option Tag "tags" ExistingFile
			Default "TagDefinitions.mdsd"

	Command SingleFile "file"
		Parameter InputFile		"input"		ExistingFile
		Parameter OutputFile	"output"	NewFile
			Default "html"