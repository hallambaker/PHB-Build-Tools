Class Goedel.VSIXBuildShell Shell
	Brief		"Build tool for Visual Studio Integration"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Generate "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.VSIXBuild VSIXBuild "asn2"

		Script  Goedel.Tool.VSIXBuild Generate GenerateCS		"cs"
			Brief "Generate C# code"
			Default "cs"