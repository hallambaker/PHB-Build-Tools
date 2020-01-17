# PHB Build Tools: RFC Tool

# General

* Should eref wrap the hypertext link it contains?

* Pointers to normative and informational documents


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

Sourcecode needs to accept language type specifier.


# Generation bugs

## SVG Bugs

Double escaping occurring on text (DARE) &lt;300 Bytes&gt;

Markers not supported


## XML v3 generation bugs


## HTML generation bugs

The DL enclosure is missing when a DL list follows immediately after a section heading.

Figures / Artwork / Source


# Markdown generation bugs

Should make Github/kramdown source notation a switchable option.
