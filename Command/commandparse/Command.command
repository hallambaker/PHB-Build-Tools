Class CommandShell CommandShell
	Brief "Command Line Parser Generator"
	About "about"
		Brief		"Report version and compilation date."
	Help "help"
		Brief		"Command guide."

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"
	

	Command GenerateCommand "in"
		Brief "Generate a command line inteface parser"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.Command CommandParse "command"

		Script  Goedel.Tool.Command GenerateCS Generate "cs"
			Brief "Generate code for C#"
			Default "cs"

		Option Main				"main" Flag
			Default "true"
			Brief	"If set, generate a main class"
		Option Builtins			"builtins"	Flag
			Default "true"
			Brief	"If set, include the built in types NewFile, SourceFile and Flags."
		Option Catcher			"catch" Flag
			Default "true"
			Brief "If set, wrap the main calling loop with a try/catch structure."
