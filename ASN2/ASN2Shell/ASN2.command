Class ASN2Shell ASN2Shell
	Brief		"ASN2 Encoder/Decoder"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  Generate "cs"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser ASN ASN2 "asn2"

		Script  ASN Generate GenerateCS		"cs"
			Brief "Generate C# code"
