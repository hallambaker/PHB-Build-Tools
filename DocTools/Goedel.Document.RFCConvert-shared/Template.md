<ietf>{0}
<category>Standards Track
<ipr>trust200902
<area>sec
<title>Document Title
<abbrev>Short Title
<version>00
<author>Alice Cryptographer
    <lastname>Cryptographer
    <initials>A.
    <firstname>Alice
    <organization>Example Inc.
    <email>Alice@example.com
<keyword>Add Keywords here.

=Abstract

Documents must have an abstract.

=Definitions

==Requirements Language

   The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT",
   "SHOULD", "SHOULD NOT", "RECOMMENDED", "MAY", and "OPTIONAL" in this
   document are to be interpreted as described in RFC 2119 [!RFC2119].

=Level 1 Heading

Paragraphs are separated by a blank line. Normative references are specified
like this [!RFC2119], informative references like this [~RFC1984]. References
to Internet RFCs, drafts and W3C documents are resolved from the xml.resource.org
repository.

Links to external bibliography files from the text are currently only supported in 
the HTML and xml2rfc formats and use the syntax:

<?bibliography file=filename.xml cache=true?>

If the cache=true flag is set, any new references that are resolved will be added to your 
local references file. This reduces load on the servers and allows offline processing
for those of you who write drafts on their way to/from IETF meetings.

I have since realized that it is better to specify the bibliography cache on the command
line since everyone will want to put theirs in a different place:

[[
rfctool example.md /cache=~/.rfctoolcache.xml
]]

This should probably move out to an environment variable or registry key at some point.

==Level 2 Sub Heading

===Level 3 Sub Heading

(up to 6 levels are supported)

Preformatted paragraphs are specified using the following syntax

[[
Preformatted text goes here.
  All markup is ignored until the close sequence is reached.
  
  Word does not require escape sequences, but in Markdown these are [.[  ]..]
]]

=Lists, etc

* Bulleted list item (LI style)
# Numbered list item (NI style)
:Defined Term (DT style)
::Definition (DD style)

=Not currently supported in Markdown or Word format but supported in the code

* Tables
* Internal document references
* Comments
* Bold font
* Table of concordances of MUST/SHOULD/MAY language
* Including text from other document files.

The last is a big one for me. I generate my drafts using code generation tools that compile the specification
text, the example code and any schemas that might be required from a single master schema. Example messages,
test vectors, etc. are then generated from the example code. This ensures that the text and examples in the
document are consistent and the document is consistent with the reference code. This tool is also on SourceForge,
see Protogen.

=Meta Tag Definitions

In general the tag definitions are the same as for xml2rfc.

:ietf [identifier]
::Declare the document an IETF document 
:category
::Informational, Experimental, Standards Track, etc
:ipr
::Tags as for xml2rfc (trust200902 ... )
:area
::Tags as for xml2rfc (trust200902 ... )
:title
::Document Title
:abbrev
::Short Title
:version
::Version number
:year, month, date
::Publication date, defaults to the current day.
:author
::Begins an author definition block, the following lastname, etc. will be added to this one
:lastname, initials, firstname, organization, email
::Additional details on current author. All the xml2rfc tags are defined
:keyword
::Keywords

=How Do I?

:Q: Specify where to put the references section?.
::A: You don't, it is placed automatically when references are specified in the text.

:Q: Specify whether to include a table of contents or not.
::A: You don't the reader needs one whether you think they do or not.

:Q: Specify comments
::A: Not currently supported. It would be good to have a link to the documentation on the 
Web in this tempate I guess.

:Q: Include images?
::A: When the IETF document submissions process has a mechanism to support images, I will update
this tool. My Bootmaker project has tools for importing images from Visio, Powerpoint and Excel.

:Q: What about an option to submit the draft to the IETF?
::A: I have been thinking about that possibility. A Web Services interface to the submissions
system is really required to make the scheme work well. Though this might well be required when we get
to submissions with images.
