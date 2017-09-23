<id>87a98061-24fe-4bd7-a67f-5e1e2b1a92f7
<version>1
<contenttype>developerConceptualDocument

This document describes the coding standards for libraries in the
PHBBuild Tools, support libraries and the Mathematical Mesh.

# Coding Standards

Every project contains the following elements:

* A reference to the library Goedel.Utilities

* A file nameed _Exceptions.Exceptional_ that defines the exceptions thrown by modules in that project.

*A GNU Make file called _Makefile_ which contains the build instructions for non-Windows platforms.
This is generated automatically from the  Visual Studio project file.

*A Microsoft build makefile file called _VS.Make_ which contains additional build instructions 
for Windows (if any). For executables, this is a call to ilmerge to make a standalone executable.

Projects that implement a code synthesizer contain the following
additional items.

* At least one schema definition file with the extension _.gdl_

* A C# file with the extension _.cs_ generated from the schema using the Goedel3 custom tool

* At least one script file with the extension _.script_

* A C# file with the extension  _.cs_ generated from the script using the GScript custom tool

Projects that define a command line tool contain the following
additional items.

* At least one command line interface definition file with the extension <localUri>.command</localUri>

* A C# file with the extension  <localUri>.cs</localUri> generated from command line 
interface definition fileusing the ComandCS custom tool

Note that a project that defines a command line tool should  be a thin layer
interface to a separate library that implements the functionality. This allows 
the functionality to be provided in other forms, such as the Visual Studio 
integration packages.

#Library References

Every code module contains a reference to the support library Goedel.Utilities.
This provides convenience functions for testing assertions and throwing
exceptions if the test fails. It also provides functions to convert to
and from various base encodings used in internet code and to perform extraction
of bytes from integer values.

#Exceptions

Every code project contains a file called
_Exceptions.Exceptional_ that defines the exceptions
thrown by modules in this library.

#C# Coding conventions

The following rules are enforced:

* All classes, properties, fields and methods begin with an upper case character.

* All delegate types end with the suffix 'Delegate'

* All properties are expression bodied where possible.

* All code is fully documented with XML comment tags

