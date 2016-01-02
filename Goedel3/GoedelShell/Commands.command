Class Command Command
	Brief		"Goedel meta-code generation tool"
	About "about"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	Command Schema "schema"
		DefaultCommand
		Brief	"Convert a Goedel schema file to code"

		Parser GoedelSchema Goedel "gdl"

		Lazy	 Lazy "lazy"		
			Brief "Only generate code if source or generator have changed"

		Script GoedelSchema GenerateParser GenerateCS "cs"
			Brief "Generate C# code"

		Option	DebugLexer		"dlexer"    Flag
		Option	DebugParser		"dparser"	Flag
		Option	DebugStack		"dstack"	Flag
		Option	Serializer		"serial"	Flag
			Default "true"




