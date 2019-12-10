# PHB Build Tools: RFC Tool

## SeriesInfo

[x] Flattened the old SeriesInfo crap down
[ ] Implement the new approach


    attribute category { "std" | "bcp" | "info" | "exp" | "historic" }?,
    attribute submissionType {
      "IETF" | "IAB" | "IRTF" | "independent"

    [Where is the link type? How is an ISSN or DOI specified?]




## Issues with existing tool

* RFC8659 has a DOI, this is not specified in the XML file as a SeriesInfo.
