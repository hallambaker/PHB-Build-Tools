Class Goedel.Shell.Makey MakeyShell
	Brief		"brief"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Project "in"
		DefaultCommand
		Brief "Convert Visual Studio Project File"

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parameter InputFile "Input" ExistingFile
			Brief "Project File"

		Parameter OutputFile "Make" NewFile
			Brief "Makefile"
			Default "makefile"