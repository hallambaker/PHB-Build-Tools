Class $safeprojectname$ $safeprojectname$
	Brief		"<brief description here"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  DefaultCommand "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser $safeprojectname$ Domainer "domainer"

		Script  $safeprojectname$ Generate GenerateCS		"cs"
			Brief "Generate C# code"
			Default "cs"
