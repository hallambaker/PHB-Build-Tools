#RFCTool Next Generation

Objectives:

* Support the RFC7991 schema for XML input and output

* Support for SVG diagrams.

* Support for decent HTML output



##Paragraph styles in text

:cref
::Wraps a comment


Link types

:iref item="term" primary="true" | "false" subitem="tem"
::Define a term for the document index with optional subitem.

:relref section="2.3" target="RFC9999"
::Link to a part of a document cited in a <reference>

:eref  target="http://www.example.com/reports/r12.html"
::External link, i.e. it is an <a> element

:xref format="counter" | "default" | "title"
::Reference to an anchor in the document. The format element allows the 
included text to be the section number or the title.



## Schema changes

* Wrap MUST, SHOULD, etc. with <bcp14> element <bcp14>MUST</bcp14>, etc.

* 


##Parsing



###Markdown parser

Move to the pandoc style markdown.


###Word parser



##Output


###HTML
