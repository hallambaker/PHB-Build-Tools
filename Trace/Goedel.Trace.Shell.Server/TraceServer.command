Class Goedel.Trace.Shell.Server TraceShell
	Brief		"Trace service"
	About "about"

	Command Start "start"
		DefaultCommand
		Brief "Start the Trace service"

		Parameter ServiceAddress "portal" String
			Brief "Portal DNS address"

		Parameter HostAddress "host" String
			Brief "Host address for Web Service Endpoint"

		Option Port "Port" Integer
			Brief "Port"

		Option Verify			"verify"		Flag
			Brief "Verify configuration only"

		Option Log			"log"		Flag
			Brief "Write log results to file (if specified by sender)"

		Option Fallback			"fallback"	Flag
			Brief "Bind to fallback Web Service Endpoint (default)"
			Default "true"

		Option Multithread		"multi"	Flag
			Brief "run as multithreaded service (default)"
			Default "true"