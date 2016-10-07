Class ProtoGenShell ProtoGenShell
	Brief		"Protocol compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Protocol "protocol"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"


		Parser Goedel.Tool.ProtoGen ProtoStruct "protocol"

		Script  Goedel.Tool.ProtoGen Generate GenerateRFC2XML "xml"
			Brief "Generate documentation in RFC2XML format"

		Script  Goedel.Tool.ProtoGen Generate GenerateHTML "html"	
			Brief "Generate documentation in HTML format"

		Script  Goedel.Tool.ProtoGen Generate GenerateMD "md"	
			Brief "Generate documentation in MarkDown format"


		Script  Goedel.Tool.ProtoGen Generate GenerateCS		"cs"
			Brief "Generate C# code"

		Script  Goedel.Tool.ProtoGen Generate GenerateC			"c"
			Brief "Generate C code"

		Script  Goedel.Tool.ProtoGen Generate GenerateH			"h"
			Brief "Generate C header"