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

The header element needs to work for drafts when submitting via the tool

 submissionType="independent" category="info"

````
<rfc xmlns:xi="http://www.w3.org/2001/XInclude" docName="draft-hallambaker-mesh-cryptography-05" 
     indexInclude="false" ipr="trust200902" scripts="Common,Latin" sortRefs="true" symRefs="true" 
     tocDepth="3" tocInclude="true" version="3" submissionType="independent" category="info" xml:lang="en"><front>
<title abbrev="Mesh Cryptographic Algorithms">Mathematical Mesh 3.0 Part VIII: Cryptographic Algorithms</title>
<seriesInfo name="Internet-Draft" value="draft-hallambaker-mesh-cryptography"/>
````

Artwork type elements are not being correctly rendered, the code is lost.


## HTML generation bugs

Tables

Figures / Artwork / Source


# Markdown generation bugs

Should make Github/kramdown source notation a switchable option.
