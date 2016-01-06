Class Goedel.Trojan CommandShell
	Brief "Graphical Interface Builder"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command Generate "generate"
		Brief "Generate a graphical user interface"
		DefaultCommand
		
		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"
		Parameter InputFile "input" ExistingFile
			Default "cmd"

		Parameter OutputFile "output" ExistingFile


