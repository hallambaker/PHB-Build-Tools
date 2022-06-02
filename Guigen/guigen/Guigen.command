Class Goedel.Shell.Guigen GuigenShell
	Brief		"GUI compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  MakeGui "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.Guigen Guigen "protocol"

		Script  Goedel.Tool.Guigen Generate GenerateCS			"cs"
			Brief "Generate cs class"
			Default "cs"

		Script  Goedel.Tool.Guigen Generate GeneratePs1			"ps1"
			Brief "Generate cs class"
			Default "ps1"

