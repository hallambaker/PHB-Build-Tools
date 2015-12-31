Class FSRGenShell FSRGenShell
	Brief		"FSR compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  FSR "fsr"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser FSRGen FSRStruct "protocol"

		Script  FSRGen Generate GenerateH			"h"
			Brief "Generate C header"

		Script  FSRGen Generate GenerateCS			"cs"
			Brief "Generate cs class"