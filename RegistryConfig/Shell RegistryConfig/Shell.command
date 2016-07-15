Class CommandShell CommandShell
	Brief "Windows Registry Coding Tool"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"
	
	About "about"

	Command Generate "generate"
		Brief "Generate code to serialize/deserialize schema"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser RegistryConfig ConfigItems "schema"

		Script  RegistryConfig GenerateCS Generate "cs"
			Brief "Generate code for C#"
