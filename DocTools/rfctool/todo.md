# PHB Build Tools: RFC Tool


# General

Handling of xref references inside the document is wonky

* Pointers to normative and informational documents
* Pointers to anchors inside the document
* Naming anchors inside the document.

# Testing

Need a full test suite with an input document in each input language.

Check that documents round trip.

````
A=input.xml
   B=XML(A), C=XML(B)  => B == C
   D=MD(A), E=MD(B)  => D == E
   F=OOXML(A), G=OOXML(B)  => F == G
   H=C=XML(D) => H == B
   I=C=XML(F) => I == B
````

Build set of input test docs with different repertoirs

* Simple
* Table
* Figures
* References
* Source Code


# SVG

Need a tool to read in SVG and strip stuff out that isn't to spec.

# Parser bugs

## XML v3

Including SVG files


## XML v2 (deprecated)

TextTable input is not correctly handled.



## Markdown

Check Tables / figures etc still work

Accept both Github/kramdown source notation

Accept kramdown style metadata


## Word

Does not support nested lists, probably never will

Implement anchors


# Generation bugs

## XML v3 generation bugs


Artwork type elements are not being correctly rendered, the code is lost.


### DL Lists

The DD tag is not being properly filled out in DL lists. This could be due to a bug in 
the word parser or the generator.

## HTML generation bugs

Tables

Figures / Artwork / Source


# Markdown generation bugs

Should make Github/kramdown source notation a switchable option.
