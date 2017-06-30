Class Goedel.Trojan.Script CommandShell
	Brief "Graphical Interface Builder"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command Generate "gtk"
		Brief "Generate a graphical user interface using GTK toolset"
		DefaultCommand
		
		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"
		
		
		Parser Goedel.Trojan.Script GUISchema "gui"

		Script Goedel.Trojan.Script GenerateGTK GenerateCS "cs"
			Brief "Generate C# code"
			Default "cs"
