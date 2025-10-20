Class SitebuilderShell SitebuilderShell
	Brief		"GUI compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Sitebuilder "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.Sitebuilder FrameStruct "gui"

		Script  Goedel.Tool.Sitebuilder GenerateBacking CreateFrame			"cs"
			Brief "Generate cs class"
			Default "cs"


