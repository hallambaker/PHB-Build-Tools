Class Goedel.Shell.Constant ConstantShell
	Brief		"Constant compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Generate "in"
		DefaultCommand
		Parameter InputFile "input" ExistingFile
			Default "cs"
		Option OutputFile "cs" NewFile
			Default "cs"
		Option MarkDown "md" Flag
