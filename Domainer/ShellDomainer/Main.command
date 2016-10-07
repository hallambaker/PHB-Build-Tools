Class DomainerShell DomainerShell
	Brief		"Manage DNS Resource and Query Records"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Domainer "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.Domainer Domainer "domainer"

		Script  Goedel.Tool.Domainer Generate GenerateCS		"cs"
			Brief "Generate C# code"
