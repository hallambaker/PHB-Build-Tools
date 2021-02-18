Class Goedel.Shell.Yaschema YaschemaShell
	Brief		"Yaschema compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Yaschema "yaschema"
		DefaultCommand
		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"


		Parser Goedel.Tool.Yaschema YaschemaStruct "yaschema"

		Script  Goedel.Tool.Yaschema Generate GenerateCS "cs"
			Brief "Generate code in C#"
			Default "cs"

		Script  Goedel.Tool.Yaschema Generate GenerateMD "md"
			Brief "Generate Markdown documentation"
			Default "md"