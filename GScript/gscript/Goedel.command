Class GoedelShell GoedelShell
	Brief		"Goedel meta-code generation tool"


	Command Generate "in"
		DefaultCommand
		Parameter InputFile "input" ExistingFile
		Parameter OutputFile "output" NewFile
			Default "cs"

		Option  CommentLine		"line"		Flag
			Brief "If set, include source in generated as comments"
		Option	Directive		"link"		Flag
			Brief "If set, link generated code to source"
		Option  Lazy			"lazy"		Flag
			Brief "Only generate code if source or generator have changed"

	Command Wrap "wrap"
		Parameter InputFile "input" ExistingFile

		Option CS "cs" NewFile
			Default "cs"

		Option Namespace "namespace" String
			Default "Constants"
		Option Class "class" String
			Default "Constants"
		Option Variable "variable" String
			Default "Value"

		Option  Lazy			"lazy"		Flag
			Brief "Only generate code if source or generator have changed"

	Command About "about"
		Brief "Report tool version and build date"