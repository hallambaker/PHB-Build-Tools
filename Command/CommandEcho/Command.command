Class Command Command
	Brief		"Goedel meta-code generation tool"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type String				"string"
	Type Integer			"int"
	Type Flag				"flag"

	Command Script "script"
		DefaultCommand
		Brief	"Convert a Goedel script file to code"
		Parameter InputFile "input" ExistingFile
			Default "script"
		Include Languages
		Option  CommentLine		"line"		Flag
			Brief "If set, include source in generated as comments"
		Option	Directive		"link"		Flag
			Brief "If set, link generated code to source"

	Command Schema "schema"
		Brief	"Convert a Goedel schema file to code"
		Parameter InputFile "input" ExistingFile
			Default "gdl"
		Include Languages
		Option	DebugLexer		"dlexer"    Flag
		Option	DebugParser		"dparser"	Flag
		Option	DebugStack		"dstack"	Flag

	OptionSet Languages
		Option	CSharp			"cs"		NewFile
			Default "cs"
			Brief "Generate C# code"
		Option	C				"c"			NewFile
			Default "c"
			Brief "Generate C code"
		Option	Java			"java"		NewFile
			Default "java"
			Brief "Generate java code"
		Option  Lazy			"lazy"		Flag
			Brief "Only generate code if source or generator have changed"

	Command About "about"
		Brief "Report tool version and build date"

