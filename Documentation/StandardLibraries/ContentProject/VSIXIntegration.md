<id>23330bd8-ccd3-4de5-b806-860b84e42380
<version>1
<contenttype>developerConceptualDocument

The Goedel coding system used to build the code automation tools supports
deep integration with Visual Studio via VSIX extension plug ins.

#Tool Sets

For convenience, the tools are divided into three groups with related functionality which 
may be installed separately:

<dl>
<dt>PHB Build Tools
<dd>The principal tools used to build the Mesh applications.
<dt>Goedel Tools
<dd>The tool building tools used to build all the other tools, including themselves.
<dt>Documentation tools
<dd>Tools used to build documentation.
</dl>

The chief value of using code generation tools is code consistency and mantinability.
It is easy enough to write a simple routine to parse a set of command line arguments but
trying to read a routine that has been edited twenty times to add or reorganize 
functionality is much harder. Using a tool to automate the process means that the
same result is achieved every time.


# PHB Build Tools

The PHB Build Tools set contains code tools that do not depend on other code tools.

<dl>
<dt>ASN2CS
<dd>Tool for generating ASN1 parser/encoder without the hassle of Assinine.One

<dt>DomainerCS
<dd>Scripting tool, converts descriptions of DNS resource records to
a class providing scripting, serialization and presentation support.

<dt>ProtoGen
<dd>Protocol compiler. Generates code for implementing JSON/REST
based Web Services in C# or C.

<dt>RegistryCS
<dd>Tool for generating code to map a data structure to Windows registry keys.

<dt>TrojanGTK
<dd>Tool for generating a GUI interface using GTK#

<dt>VSIXBuild
<dd>Tool for generating VSIX manifests for code generators built using Goedel and other tools
</dl>

#Goedel Tools

The Goedel tool set contains the tools that are used to build the Goedel tool
set and all the other tools. These are kept separate from the other tool builds
as development of code that writes itself is inherently complicated. Each one of
the five tools is dependent on the other five.

<dl>
<dt>CommandCS
<dd>Tool for generating a command line parser. Windows and Unix style command line
syntax are supported. 

<dt>Exceptional
<dd>Tool for generating exception declarations from description file.

<dt>FSRCS
<dd>Tool for generating code to implement a Finite State Recognizer from
a set of state transitions. Mostly useful for implementing Lexical
analysers.

<dt>Goedel3
<dd>Scripting tool, converts a file containing scripting commands to
C# code that implements the commands.

<dt>GScript
<dd>Scripting tool, converts a file containing scripting commands to
C# code that implements the commands.
</dl>

#Documentation tools

The documentation tools are designed to support generation of two types of documentation:

* Internet Drafts (and RFCs)

* AML files for use with Sandcastle Help File Builder.

<dl>
<dt>RFC2TXT
<dd>Convert RFC source in Markdown, XML or HTML to Plaintext format.

<dt>RFC2XML
<dd>Convert RFC source in Markdown, XML or HTML to XML2RFC format.

<dt>RFC2MD
<dd>Convert RFC source in Markdown, XML or HTML to Markdown format.

<dt>RFC2HTML
<dd>Convert RFC source in Markdown, XML or HTML to HTML

<dt>MD2AML
<dd>Convert Markdown, XML or HTML to AML format.
</dl>
