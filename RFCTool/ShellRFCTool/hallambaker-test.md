// This file was converted using RFCTool
<ietf>draft-hallambaker-jsonbcd-06
<title>Binary Encodings for JavaScript Object Notation: JSON-B, JSON-C, JSON-D 
<abbrev>JSON-B, JSON-C, JSON-D

<ipr>trust200902
<area>General
<workgroup>
<publisher>Internet Engineering Task Force (IETF)
<status>Standards Track

<category>info
<updates>
<obsoletes>
<author>Phillip Hallam-Baker
    <initials>P. M.
    <organization>Comodo Group Inc.  
    <surname>Hallam-Baker
    <email>philliph@comodo.com 


#Abstract

Three binary encodings for JavaScript Object Notation (JSON) are
presented. JSON-B (Binary) is a strict superset of the JSON encoding that
permits efficient binary encoding of intrinsic JavaScript data types. JSON-C
(Compact) is a strict superset of JSON-B that supports compact
representation of repeated data strings with short numeric codes. JSON-D (Data)
supports additional binary data types for integer and floating point
representations for use in scientific applications where conversion between binary
and decimal representations would cause a loss of precision. 

#Definitions 

##Requirements Language 

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT",
"SHOULD", "SHOULD NOT", "RECOMMENDED", "MAY", and "OPTIONAL" in this document
are to be interpreted as described in [RFC2119]. 

#Introduction 

JavaScript Object Notation (JSON) is a simple text encoding for the
JavaScript Data model that has found wide application beyond its original
field of use. In particular JSON has rapidly become a preferred encoding for
Web Services. 

JSON encoding supports just four fundamental data types (integer,
floating point, string and boolean), arrays and objects which consist of a list
of tag-value pairs. 

Although the JSON encoding is sufficient for many purposes it is not
always efficient. In particular there is no efficient representation for
blocks of binary data. Use of base64 encoding increases data volume by 33%.
This overhead increases exponentially in applications where nested binary
encodings are required making use of JSON encoding unsatisfactory in
cryptographic applications where nested binary structures are frequently
required. 

Another source of inefficiency in JSON encoding is the repeated
occurrence of object tags. A JSON encoding containing an array of a hundred
objects such as {"first":1,"second":2} will contain a hundred occurrences of
the string "first" (seven bytes) and a hundred occurrences of the string
"second" (eight bytes). Using two byte code sequences in place of strings
allows a saving of 11 bytes per object without loss of information, a
saving of 50%. 

A third objection to the use of JSON encoding is that floating point
numbers can only be represented in decimal form and this necessarily
involves a loss of precision when converting between binary and decimal
representations. While such issues are rarely important in network applications
they can be critical in scientific applications. It is not acceptable for
saving and restoring a data set to change the result of a calculation. 

##Objectives 

The following were identified as core objectives for a binary JSON
encoding: 

Three binary encodings are defined: 

<dl>
<dt>JSON-B (Binary)

<dd>Simply encodes JSON data in binary. Only the JavaScript data model is supported (i.e. atomic types are integers, double or string). Integers may be 8, 16, 32 or 64 bits either signed or unsigned. Floating points are IEEE 754 binary64 format [IEEE-754]. Supports chunked encoding for binary and UTF-8 string types.

<dt>JSON-C (Compact)

<dd>As JSON-B but with support for representing JSON tags in numeric code form (16 bit code space). This is done for both compact encoding and to allow simplification of encoders/decoders in constrained environments. Codes may be defined inline or by reference to a known dictionary of codes referenced via a digest value.

<dt>JSON-D (Data)

<dd>As JSON-C but with support for representing additional data types without loss of precision. In particular other IEEE 754 floating point formats, both binary and decimal and Intel's 80 bit floating point, plus 128 bit integers and bignum integers.
</dl>

#Extended JSON Grammar 

The JSON-B, JSON-C and JSON-D encodings are all based on the JSON
grammar [RFC4627] using the same syntactic structure but different lexical
encodings. 

JSON-B0 and JSON-C0 replace the JSON lexical encodings for strings and
numbers with binary encodings. JSON-B1 and JSON-C1 allow either lexical
encoding to be used. Thus any valid JSON encoding is a valid JSON-B1 or
JSON-C1 encoding. 

The grammar of JSON-B, JSON-C and JSON-D is a superset of the JSON
grammar. The following productions are added to the grammar: 

<dl>
<dt>x-value

<dd>Binary encodings for data values. As the binary value encodings are all self delimiting

<dt>x-member

<dd>An object member where the value is specified as an X-value and thus does not require a value-separator.

<dt>b-value

<dd>Binary data encodings defined in JSON-B.

<dt>b-string

<dd>Defined length string encoding defined in JSON-B.

<dt>c-def

<dd>Tag code definition defined in JSON-C. These may only appear before the beginning of an Object or Array and before any preceding white space.

<dt>c-tag

<dd>Tag code value defined in JSON-C.

<dt>d-value

<dd>Additional binary data encodings defined in JSON-D for use in scientific data applications.
</dl>

The JSON grammar is modified to permit the use of x-value productions in
place of ( value value-separator ) : 

~~~~
JSON-text = (object / array)
   
object = *cdef begin-object [
         *( member value-separator | x-member )
         (member | x-member) ] end-object
   
member = tag value
x-member = tag x-value
   
tag = string name-separator | b-string | c-tag
 
array = *cdef begin-array [  *( value value-separator | x-value )
(value | x-value) ] end-array
   
x-value = b-value / d-value
   
value = false / null / true / object / array / number / string
   
name-separator  = ws %x3A ws  ; : colon
value-separator = ws %x2C ws  ; , comma
~~~~

~~~~
The following lexical values are unchanged:
begin-array     = ws %x5B ws  ; [ left square bracket
begin-object    = ws %x7B ws  ; { left curly bracket
end-array       = ws %x5D ws  ; ] right square bracket
end-object      = ws %x7D ws  ; } right curly bracket
   
ws = *( %x20 %x09 %x0A  %x0D )
   
false = %x66.61.6c.73.65   ; false
null  = %x6e.75.6c.6c      ; null
true  = %x74.72.75.65      ; true
~~~~

The productions number and string are defined as before: 

~~~~
number = [ minus ] int [ frac ] [ exp ]
decimal-point = %x2E       ; .
digit1-9 = %x31-39         ; 1-9
e = %x65 / %x45            ; e E
exp = e [ minus / plus ] 1*DIGIT
frac = decimal-point 1*DIGIT
int = zero / ( digit1-9 *DIGIT )
minus = %x2D               ; -
plus = %x2B                ; +
zero = %x30                ; 0
   
string = quotation-mark *char quotation-mark
char = unescaped /
escape ( %x22 / %x5C / %x2F / %x62 / %x66 /
%x6E / %x72 / %x74 /  %x75 4HEXDIG )
   
escape = %x5C              ; \
quotation-mark = %x22      ; "
unescaped = %x20-21 / %x23-5B / %x5D-10FFFF
~~~~

#JSON-B 

The JSON-B encoding defines the b-value and b-string productions: 

~~~~
b-value = b-atom | b-string | b-data | b-integer |
b-float
   
b-string = *( string-chunk ) string-term
b-data = *( data-chunk ) data-last
   
b-integer = p-int8 | p-int16 | p-int32 | p-int64 | p-bignum16 |
n-int8 | n-int16 | n-int32 | n-int64 | n-bignum16
   
b-float = binary64
~~~~

The lexical encodings of the productions are defined in the following
table where the column 'tag' specifies the byte code that begins the
production, 'Fixed' specifies the number of data bytes that follow and 'Length'
specifies the number of bytes used to define the length of a variable
length field following the data bytes: 
<table=s-4-1>
<thead>
<tr>
<td>Production
<td>Tag
<td>Fixed
<td>Length
<td>Data Description
<tbody>
<tr>
<tr>
<td>string-term
<td>x80
<td>-
<td>1
<td>Terminal String 8 bit length
<tr>
<td>string-term
<td>x81
<td>-
<td>2
<td>Terminal String 16 bit length
<tr>
<td>string-term
<td>x82
<td>-
<td>4
<td>Terminal String 32 bit length
<tr>
<td>string-term
<td>x83
<td>-
<td>8
<td>Terminal String 64 bit length
<tr>
<td>string-chunk
<td>x84
<td>-
<td>1
<td>Non-terminal String 8 bit length
<tr>
<td>string-chunk
<td>x85
<td>-
<td>2
<td>Non-terminal String 16 bit length
<tr>
<td>string-chunk
<td>x86
<td>-
<td>4
<td>Non-terminal String 32 bit length
<tr>
<td>string-chunk
<td>x87
<td>-
<td>8
<td>Non-terminal String 64 bit length
<tr>
<td>string-
<td>x88
<td>-
<td>1
<td>Terminal Data 8 bit length
<tr>
<td>string-term
<td>x89
<td>-
<td>2
<td>Terminal Data 16 bit length
<tr>
<td>string-term
<td>x8A
<td>-
<td>4
<td>Terminal Data 32 bit length
<tr>
<td>string-term
<td>x8B
<td>-
<td>8
<td>Terminal Data 64 bit length
<tr>
<td>string-chunk
<td>x8C
<td>-
<td>1
<td>Non-terminal Data 8 bit length
<tr>
<td>string-chunk
<td>x8D
<td>-
<td>2
<td>Non-terminal Data 16 bit length
<tr>
<td>string-chunk
<td>x8E
<td>-
<td>4
<td>Non-terminal Data 32 bit length
<tr>
<td>string-chunk
<td>x8F
<td>-
<td>8
<td>Non-terminal Data 64 bit length
<tr>
<td>p-int8
<td>xA0
<td>1
<td>-
<td>Positive 8 bit Integer
<tr>
<td>p-int8
<td>xA1
<td>2
<td>-
<td>Positive 16 bit Integer
<tr>
<td>p-int8
<td>xA2
<td>4
<td>-
<td>Positive 32 bit Integer
<tr>
<td>p-int8
<td>xA3
<td>8
<td>-
<td>Positive 64 bit Integer
<tr>
<td>p-int8
<td>xA5
<td>-
<td>2
<td>Positive Bignum 16 bit Length
<tr>
<td>p-int8
<td>xA8
<td>1
<td>-
<td>Negative 8 bit Integer
<tr>
<td>p-int8
<td>xA8
<td>2
<td>-
<td>Negative 16 bit Integer
<tr>
<td>p-int8
<td>xAA
<td>4
<td>-
<td>Negative 32 bit Integer
<tr>
<td>p-int8
<td>xAB
<td>8
<td>-
<td>Negative 64 bit Integer
<tr>
<td>p-int8
<td>xAD
<td>-
<td>2
<td>Negative Bignum 16 bit Length
<tr>
<td>binary64
<td>x92
<td>8
<td>-
<td>IEEE 754 Floating Point Binary 64 bit
<tr>
<td>b-value
<td>xB0
<td>-
<td>-
<td>True
<tr>
<td>b-value
<td>xB1
<td>-
<td>-
<td>False
<tr>
<td>b-value
<td>xB2
<td>-
<td>-
<td>Null
</table>

A data type commonly used in networking that is not defined in this
scheme is a datetime representation. To define such a data type, a string
containing a date-time value in Internet type format is typically used. 

##JSON-B Examples 

The following examples show examples of using JSON-B encoding: 

~~~~
A0 2A                            42 (as 8 bit integer)
A1 00 2A                         42 (as 16 bit integer)
A2 00 00 00 2A                   42 (as 32 bit integer)
A3 00 00 00 00 00 00 00 2A       42 (as 64 bit integer)
A5 00 01 42                      42 (as Bignum)
   
80 05 48 65 6c 6c 6f             "Hello" (single chunk)
81 00 05 48 65 6c 6c 6f          "Hello" (single chunk)
84 05 48 65 6c 6c 6f 80 00       "Hello" (as two chunks)
   
92 3f f0 00 00 00 00 00 00       1.0
92 40 24 00 00 00 00 00 00       10.0
92 40 09 21 fb 54 44 2e ea       3.14159265359
92 bf f0 00 00 00 00 00 00       -1.0
   
B0                               true
B1                               false
B2                               null
~~~~

#JSON-C 

JSON-C (Compressed) permits numeric code values to be substituted for
strings and binary data. Tag codes MAY be 8, 16 or 32 bits long encoded in
network byte order. 

Tag codes MUST be defined before they are referenced. A Tag code MAY be
defined before the corresponding data or string value is used or at the
same time that it is used. 

A dictionary is a list of tag code definitions. An encoding MAY
incorporate definitions from a dictionary using the dict-hash production. The
dict hash production specifies a (positive) offset value to be added to the
entries in the dictionary followed by the UDF fingerprint
[I-D.hallambaker-udf] of the dictionary to be used. 

~~~~
+------------+-----+-------+--------+-------------------------------+
| Production | Tag | Fixed | Length | Data Description              |
+------------+-----+-------+--------+-------------------------------+
| c-tag      | xC0 | 1     | -      | 8 bit tag code                |
|            |     |       |        |                               |
| c-tag      | xC1 | 2     | -      | 16 bit tag code               |
|            |     |       |        |                               |
| c-tag      | xC2 | 4     | -      | 32 bit tag code               |
|            |     |       |        |                               |
| c-def      | xC4 | 1     | -      | 8 bit tag definition          |
|            |     |       |        |                               |
| c-def      | xC5 | 2     | -      | 16 bit tag definition         |
|            |     |       |        |                               |
| c-def      | xC6 | 4     | -      | 32 bit tag definition         |
|            |     |       |        |                               |
| c-tag      | xC8 | 1     | -      | 8 bit tag code & definition   |
|            |     |       |        |                               |
| c-tag      | xC9 | 2     | -      | 16 bit tag code & definition  |
|            |     |       |        |                               |
| c-tag      | xCA | 4     | -      | 32 bit tag code & definition  |
|            |     |       |        |                               |
| c-def      | xCC | 1     | -      | 8 bit tag dictionary          |
|            |     |       |        | definition                    |
|            |     |       |        |                               |
| c-def      | xCD | 2     | -      | 16 bit tag dictionary         |
|            |     |       |        | definition                    |
|            |     |       |        |                               |
| c-def      | xCE | 4     | -      | 32 bit tag dictionary         |
|            |     |       |        | definition                    |
|            |     |       |        |                               |
| dict-hash  | xD0 | 4     | 1      | UDF fingerprint of dictionary |
+------------+-----+-------+--------+-------------------------------+
~~~~

All integer values are encoded in Network Byte Order (most significant
byte first). 

##JSON-C Examples 

The following examples show examples of using JSON-C encoding: 

~~~~
C8 20 80 05 48 65 6c 6c 6f       "Hello"    20 = "Hello"
C4 21 80 05 48 65 6c 6c 6f                  21 = "Hello"
C0 20                            "Hello"
C1 00 20                         "Hello"
   
D0 00 00 01 00 20             Insert dictionary at code 256
e3 b0 c4 42 98 fc 1c 14
9a fb f4 c8 99 6f b9 24
27 ae 41 e4 64 9b 93 4c
a4 95 99 1b 78 52 b8 55       UDF (C4 21 80 05 48 65 6c 6c 6f)
~~~~

#JSON-D (Data) 

JSON-B and JSON-C only support the two numeric types defined in the
JavaScript data model: Integers and 64 bit floating point values. JSON-D
(Data) defines binary encodings for additional data types that are commonly
used in scientific applications. These comprise positive and negative 128
bit integers, six additional floating point representations defined by
IEEE 754 [RFC2119] and the Intel extended precision 80 bit floating point
representation. 

Should the need arise, even bigger bignums could be defined with the
length specified as a 32 bit value permitting bignums of up to 2^35 bits to
be represented. 

~~~~
d-value = d-integer | d-float
   
d-float = binary16 | binary32 | binary128 | binary80 |
decimal32 | decimal64 | decimal 128
+------------+-----+-------+--------+-------------------------------+
| Production | Tag | Fixed | Length | Data Description              |
+------------+-----+-------+--------+-------------------------------+
| p-int128   | xA4 | 16    | -      | Positive 128 bit Integer      |
|            |     |       |        |                               |
| n-in7128   | xAC | 16    | -      | Negative 128 bit Integer      |
|            |     |       |        |                               |
| binary16   | x90 | 2     | -      | IEEE 754 Floating Point       |
|            |     |       |        | binary16                      |
|            |     |       |        |                               |
| binary32   | x91 | 4     | -      | IEEE 754 Floating Point       |
|            |     |       |        | binary32                      |
|            |     |       |        |                               |
| binary128  | x94 | 16    | -      | IEEE 754 Floating Point       |
|            |     |       |        | binary128                     |
|            |     |       |        |                               |
| intel80    | x95 | 10    | -      | Intel 80 bit extended binary  |
|            |     |       |        | Floating Point                |
|            |     |       |        |                               |
| decimal32  | x96 | 4     | -      | IEEE 754 Floating Point       |
|            |     |       |        | decimal32                     |
|            |     |       |        |                               |
| decimal64  | x97 | 8     | -      | IEEE 754 Floating Point       |
|            |     |       |        | decimal64                     |
|            |     |       |        |                               |
| decimal128 | x98 | 18    | -      | IEEE 754 Floating Point       |
|            |     |       |        | decimal128                    |
+------------+-----+-------+--------+-------------------------------+
~~~~

#Acknowledgements 

This work was assisted by conversations with Nico Williams and other
participants on the applications area mailing list. 

#Security Considerations 

A correctly implemented data encoding mechanism should not introduce new
security vulnerabilities. However, experience demonstrates that some
data encoding approaches are more prone to introduce vulnerabilities when
incorrectly implemented than others. 

In particular, whenever variable length data formats are used, the
possibility of a buffer overrun vulnerability is introduced. While best
practice suggests that a coding language with native mechanisms for bounds
checking is the best protection against such errors, such approaches are not
always followed. While such vulnerabilities are most commonly seen in the
design of decoders, it is possible for the same vulnerabilities to be
exploited in encoders. 

A common source of such errors is the case where nested length encodings
are used. For example, a decoder relies on an outermost length encoding
that specifies a length on 50 bytes to allocate memory for the entire
result and then attempts to copy a string with a declared length of 1000
bytes within the sequence. 

The extensions to the JSON encoding described in this document are
designed to avoid such errors. Length encodings are only used to define the
length of x-value constructions which are always terminal and cannot have
nested data entries.  

#IANA Considerations 

[TBS list out all the code points that require an IANA registration] 
