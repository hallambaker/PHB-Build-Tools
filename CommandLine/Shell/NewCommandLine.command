Class CommandShell CommandShell
	Brief "Command Line Parser Generator"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"
	
	About "about"

	Command Generate "generate"
		Brief "Generate a command line inteface parser"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser CommandP CommandParse "command"

		Script  CommandP GenerateCS Generate "cs"
			Brief "Generate code for C#"

		Option Main				"main" Flag
			Default "true"
			Brief	"If set, generate a main class"
		Option Builtins			"builtins"	Flag
			Default "true"
			Brief	"If set, include the built in types NewFile, SourceFile and Flags."
		Option Catcher			"catch" Flag
			Default "true"
			Brief "If set, wrap the main calling loop with a try/catch structure."
