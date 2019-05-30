Class Command Command
	Brief		"Goedel meta-code generation tool"

	About "about"
		Brief "Report tool version and build date"

	Help "help"
		Brief "Help on commands and usage"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type String				"string"
	Type Integer			"int"
	Type Flag				"flag"

	CommandSet Fred "fred"
		Brief "Useless"

	Command Script "script"
		DefaultCommand
		Brief	"Convert a Goedel script file to code"
		Parameter InputFile "in" ExistingFile
			Brief "Input file"
			Default "script"
		Include Languages
		Include Reporting
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
			Brief "Report debug output for the lexical analyzer"
		Option	DebugParser		"dparser"	Flag
			Brief "Report debug output for the parser"
		Option	DebugStack		"dstack"	Flag
			Brief "Report debug output for the parse stack"

	OptionSet Reporting
		Enumerate EnumReporting "report"
			Brief "Reporting level"
			Case Report "report"
				Brief "Report output (default)"
			Case Silent "silent"
				Brief "Suppress output"
			Case Verbose "verbose"
				Brief "Verbose reports"
			Case Json "json"
				Brief "Report output in JSON format"


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
