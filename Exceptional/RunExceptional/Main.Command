Class ExceptionalShell ExceptionalShell
	Brief		"Manage declaration of exceptions"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Exception "exception"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Exceptional Exceptions "exceptional"

		Script  Exceptional Generate GenerateCS		"cs"
			Brief "Generate C# code"
