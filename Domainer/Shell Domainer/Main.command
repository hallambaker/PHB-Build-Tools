Class DomainerShell DomainerShell
	Brief		"Manage DNS Resource and Query Records"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Domainer "domainer"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser GoedelDomainer Domainer "domainer"

		Script  GoedelDomainer Generate GenerateCS		"cs"
			Brief "Generate C# code"
