﻿Class Goedel.Shell.FSRGen FSRGenShell
	Brief		"FSR compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  FSR "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.FSRGen FSRSchema "protocol"

		Script  Goedel.Tool.FSRGen Generate GenerateH			"h"
			Brief "Generate C header"
			Default "h"

		Script  Goedel.Tool.FSRGen Generate GenerateCS			"cs"
			Brief "Generate cs class"
			Default "cs"



