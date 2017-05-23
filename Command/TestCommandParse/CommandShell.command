Class CommandShell CommandShell
	Brief "Command Line Parser Generator"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"
	
	About "about"

	Command Reset "reset"
		Brief "Reset the service"

	CommandSet Web "web"
		Brief "Web profile management"
		Command WebCreate "Create"
			Brief "Create Web profile"

		Command WebDefault "Default"
		Command WebAdd "Add"
		Command WebDelete "Delete"

	CommandSet Mail "Mail"
		Brief "Mail profile management"
		Command MailCreate "Create"
			Brief "Create Mail profile"

		Command MailDefault "Default"
		Command MailAdd "Add"
		Command MailDelete "Delete"

	Command TestGenerate "in"
		Brief "Generate a command line inteface parser"

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.Command CommandParse "command"

		Script  Goedel.Tool.Command GenerateCS Generate "cs"
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