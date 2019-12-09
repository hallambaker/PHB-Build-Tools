Class MakeRFC Shell
	Brief		"Process documents in HTML2RFC and XML2RFC format"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"

	About "about"
	Brief "brief"

	Command RFC "rfc"
		DefaultCommand
		Lazy Lazy "lazy"	
			Brief "Only generate code if source or program have changed"

		Parameter InputFile "input" ExistingFile

		Option InputFormat "in" String

		Option Catalog "catalog" String

		Option HTML "html" NewFile
			Default "html"
		Option XML "xml" NewFile
			Default "xml"
		Option TXT "txt" NewFile
			Default "txt"
		Option MD "md" NewFile
			Default "md"
		Option DOC "docx" NewFile
			Default "docx"

		Option AML "aml" NewFile
			Default "aml"


		Option W3C "w3c" NewFile
			Default "html"

		Option Bibliography "bib" ExistingFile
		Option Cache "cache" ExistingFile

		Option Stylesheet "style" ExistingFile
		Option Boilerplate "boiler" ExistingFile

		Option Auto "auto" Flag
			Default "false"


	Command Template "new"
		Parameter Identifier "identifier" String
		Option HTML "html" NewFile
			Default "html"
		Option XML "xml" NewFile
			Default "xml"
		Option MD "md" NewFile
			Default "md"
		Option DOC "docx" NewFile
			Default "docx"