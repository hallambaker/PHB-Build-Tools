Class Goedel.Tool.Version Command
	Brief		"Version Management tool"

	About "about"
		Brief "Report tool version and build date"

	Help "help"
		Brief "Help on commands and usage"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type String				"string"
	Type Integer			"int"
	Type Flag				"flag"


	Command Version "version"
		DefaultCommand
		Brief	"Read version file and update values"
		Parameter InputFile "in" ExistingFile
			Brief "Input file"
			Default "version"
		Parameter OutputFile "out" ExistingFile
			Brief "Output file"
			Default "cs"