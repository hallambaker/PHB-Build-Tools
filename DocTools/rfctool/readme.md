# PHB Build Tools: RFC Tool

The RFC Tool converts specifications written in HTML, Markdown, XML or
Microsoft Word to the formats required by Internet standards organizations.

Currently, the only format supported is the IETF RFC format but support
for W3C and OASIS formats would only require an appropriate output
module to be written.



## Formatting

###Citations

''''
<norm="RFC2119"/>
<info="RFC4086"/>
''''

### Including files

''''
<include="..\Examples\UDFVariousUDF.md">
<imgref="UDFDigestEARLRAW.jpg">JPEG
<figuresvg="UDFDigestEARLRAW.svg">QR Code with embedded decryption and location key
''''


### Cross references

<id="">




## Tag Definitions





### Attribute Tags

* meta

#### Document Description

* title
* abbrev
* keyword
* version
* year
* month
* day
	


#### IETF Specific Tags

* ietf
* ipr
* area
* workgroup
* publisher
* status
* number
* category
* updates
* obsoletes
* seriesnumber



#### Layout control

I don't actually believe layout is something that the document author
should control. These are features provided for the benefit of the 
reader and the choice of whether they should be displayed belongs to them.

* layout 
  - master 
  - navigator 
  - toc 
  - tof 
  - tor 
  - tod  

#### Specifying an author

The following tags are used to identify an author:

* author
  * firstname
  * surname
  * organization
  * organizationabbrev
  * street
  * city
  * code
  * country
  * phone
  * uir
  * initials
  * email