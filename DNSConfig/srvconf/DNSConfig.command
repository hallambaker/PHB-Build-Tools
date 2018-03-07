Class Goedel.Shell.DNSConfig DNSConfigShell
	Brief		"DNS configuration compiler"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"

	Command  DNS "in"
		DefaultCommand

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Parser Goedel.Tool.DNSConfig DNSConfig "protocol"


		Script  Goedel.Tool.DNSConfig Generate GenerateZone			"zone"
			Brief "Generate zone file"
			Default "zone"

			