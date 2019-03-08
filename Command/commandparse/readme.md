# commandparse


A tool that generates and documents command line parsing tools in the tradition
established by the VMS VERB domain specific language.

Commands are described in a domain specific language defining the command line 
parameters and options and describing them in the default language.

Supported features

* Generates C# code that can be run on Windows, Linux or OSX using either .NET Core 2.0
or .Net Framework.

* Generate command documentation.

* Parse parameters and options in Windows and unix styles.

* Default option and parameter values

* Option sets

* Enumerated option values (work in progress)

* Automatically generate main program code for Goedel domain specific languages.


Planned features

* Re-enable support for C language binding.

* Allow support for additional languages (German, French, etc) by means of 
user editable configuration files (JSON).

## Example

The following is the '.command' file for commandparse itself.

''''
Class CommandShell CommandShell
	Brief "Command Line Parser Generator"

	Type NewFile			"file"
	Type ExistingFile		"file"
	Type Flag				"flag"
	
	About "about"
		Brief "Give the tool version information"

	Help "help"
		Brief "Describe commands"

	Command GenerateCommand "in"
		Brief "Generate a command line inteface parser"
		DefaultCommand

		Parser Goedel.Tool.Command CommandParse "command"
			Brief "Parse the input file <command>"

		Script  Goedel.Tool.Command GenerateCS Generate "cs"
			Brief "Generate code for C#"
			Default "cs"

		Lazy Lazy "lazy"	
			Brief "Only generate code if source or generator have changed"

		Option Main				"main" Flag
			Default "true"
			Brief	"If set, generate a main class"
		Option Builtins			"builtins"	Flag
			Default "true"
			Brief	"If set, include the built in types NewFile, SourceFile and Flags."
		Option Catcher			"catch" Flag
			Default "true"
			Brief "If set, wrap the main calling loop with a try/catch structure."
''''

We can use the tool to compile its own configuration file as follows:

''''
commandparse Command.command /cs /nomain /nocatcher
''''

There is no need to specify the output file, this is implicit from the use of the /cs flag
to specify that C# output should be generated.

If the tool supported generation of C output, we could generate both outputs simultaneously
by specifying 'commandparse Command.command /cs /c'

Generated code also accepts input in unix style syntax:

''''
commandparse Command.command -cs -nomain -nocatcher
''''

Tools created using the command may be called using Windows or unix style syntax
regardless of the platform on which they are run. To compile the above verbs, we 
can use the Windows style syntax:

## Automatic Commands

The /about and /help command are generated automatically. The entries in the command
description file are there to allow the command names and descriptions to be changed 
to match locale settings.

The '/about' command gives description of the tool itself including the version number
and the date and time it was compiled:

''''
>commandparse /about
commandparse

  CopyRight : Copyright c  2017
  Version   : 1.0.0.0
  Compiled  : 2019-02-24 17:43:36 -05:00

''''

The '/help' command gives a description of each command:

''''
>commandparse /help
Command Line Parser Generator

<inputfile>     Parse the input file <command>
    /cs	<out>       Generate code for C#
	/lazy           Only generate code if source or generator have changed
    /[no]main       If set, generate a main class
    /[no]builtins   If set, include the built in types NewFile, SourceFile and Flags.
    /[no]catch      If set, wrap the main calling loop with a try/catch structure.
/about          Give the tool version information
/help           Describe commands
''''

The same information may be extracted from the API to provide formatted
output for use in documentation.

## Using commandparse to create Goedel tools

The command parse tool may be used to create a Goedel domain specific language without
the need to write any wrapper code.

The 'Parser' declaration specifies the namespace and output class specified in
the Goedel schema:

''''
Class Goedel.Tool.Command CommandParse
	TopType Class
		Namespace	Token	ClassType
		...
''''

The 'Script' declaration specifies the namespace, class and entry point specified
in the 'Gscript' file and the default output extension:

''''
#script 1.0
#license MITLicense
#xclass Goedel.Tool.Command GenerateCS
#method Generate CommandParse CommandParseIn
...

''''