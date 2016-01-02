Class Goedel.VSIXBuildShell Shell
	Brief		"Build tool for Visual Studio Integration"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Generate "cs"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.VSIXBuild VSIXBuild "asn2"

		Script  Goedel.VSIXBuild Generate GenerateCS		"cs"
			Brief "Generate C# code"
