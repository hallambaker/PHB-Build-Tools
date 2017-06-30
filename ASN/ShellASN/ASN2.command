Class Goedel.Shell.ASN2 ASN2Shell
	Brief		"ASN2 Encoder/Decoder"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Generate "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.ASN ASN2 "asn2"

		Script  Goedel.Tool.ASN Generate GenerateCS		"cs"
			Brief "Generate C# code"
			Default "cs"
