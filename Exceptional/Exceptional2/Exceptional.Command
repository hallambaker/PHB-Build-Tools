Class ExceptionalShell ExceptionalShell
	Brief		"Manage declaration of exceptions"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Exception "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.Exceptional Exceptions "exceptional"

		Script  Goedel.Tool.Exceptional Generate GenerateCS		"cs"
			Brief "Generate C# code"
			Default "cs"
