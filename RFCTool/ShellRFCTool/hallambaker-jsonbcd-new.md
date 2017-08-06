// This file was converted using RFCTool
<ietf>draft-hallambaker-jsonbcd
<title>Binary Encodings for JavaScript Object Notation: JSON-B, JSON-C, JSON-D
<abbrev>JSON-B, JSON-C, JSON-D
<version>07

<ipr>trust200902

<author>Phillip Hallam-Baker    
    <initials>P. M.    
    <organization>Comodo Group Inc.    
    <surname>Hallam-Baker    
    <email>philliph@comodo.com

<keyword>JSON
<keyword>Binary encoding

#Abstract

#Definitions

##Requirements Language

#Introduction

##Objectives

<ul>
<ul>
<li>Low overhead encoding and decoding

<li>Easy to convert existing encoders and decoders to add binary support

<li>Efficient encoding of binary data

<li>Ability to convert from JSON to binary encoding in a streaming mode (i.e. without reading the entire binary data block before beginning encoding.

<li>Lossless encoding of JavaScript data types

<li>The ability to support JSON tag compression and extended data types are considered desirable but not essential for typical network applications.
</ul>
</ul>

<dl>
<dl>
<dt>JSON-B (Binary)

<dd>Simply encodes JSON data in binary. Only the JavaScript data model is supported (i.e. atomic types are integers, double or string). Integers may be 8, 16, 32 or 64 bits either signed or unsigned. Floating points are IEEE 754 binary64 format [IEEE-754]. Supports chunked encoding for binary and UTF-8 string types.

<dt>JSON-C (Compact)

<dd>As JSON-B but with support for representing JSON tags in numeric code form (16 bit code space). This is done for both compact encoding and to allow simplification of encoders/decoders in constrained environments. Codes may be defined inline or by reference to a known dictionary of codes referenced via a digest value.

<dt>JSON-D (Data)

<dd>As JSON-C but with support for representing additional data types without loss of precision; IEEE 754 floating point formats, both binary and decimal, Intel's 80 bit floating point, plus 128 bit integers and big integers.
</dl>
</dl>

#Extended JSON Grammar

<dl>
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

<dd>Tag code definition defined in JSON-C. These may only appear before the beginning of an Object or Array and before any preceeding white space.

<dt>c-tag

<dd>Tag code value defined in JSON-C.

<dt>d-value

<dd>Additional binary data encodings defined in JSON-D for use in scientific data applications.
</dl>
</dl>

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
begin-array     = ws %x5B ws  ; [ left square bracket
begin-object    = ws %x7B ws  ; { left curly bracket
end-array       = ws %x5D ws  ; ] right square bracket
end-object      = ws %x7D ws  ; } right curly bracket
   
ws = *( %x20 %x09 %x0A  %x0D )
   
false = %x66.61.6c.73.65   ; false
null  = %x6e.75.6c.6c      ; null
true  = %x74.72.75.65      ; true
~~~~

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

~~~~
b-value = b-atom | b-string | b-data | b-integer |
b-float
   
b-string = *( string-chunk ) string-term
b-data = *( data-chunk ) data-last
   
b-integer = p-int8 | p-int16 | p-int32 | p-int64 | p-bignum16 |
n-int8 | n-int16 | n-int32 | n-int64 | n-bignum16
   
b-float = binary64
~~~~
<table=s-4-4>
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
<td>Non-Terminal String 8 bit length                      
<tr>
<td>string-chunk
<td>x85
<td>-
<td>2
<td>Non-Terminal String 16 bit length                      
<tr>
<td>string-chunk
<td>x86
<td>-
<td>4
<td>Non-Terminal String 32 bit length                      
<tr>
<td>string-chunk
<td>x87
<td>-
<td>8
<td>Non-Terminal String 64 bit length                      
<tr>
<td>data-term  
<td>x88
<td>-
<td>1
<td>Terminal String 8 bit length                      
<tr>
<td>data-term  
<td>x89
<td>-
<td>2
<td>Terminal String 16 bit length                      
<tr>
<td>data-term  
<td>x8A
<td>-
<td>4
<td>Terminal String 32 bit length                      
<tr>
<td>data-term  
<td>X8B
<td>-
<td>8
<td>Terminal String 64 bit length                      
<tr>
<td>data-chunk
<td>x8C
<td>-
<td>1
<td>Non-Terminal String 8 bit length                      
<tr>
<td>data-chunk
<td>x8D
<td>-
<td>2
<td>Non-Terminal String 16 bit length                      
<tr>
<td>data-chunk
<td>x8E
<td>-
<td>4
<td>Non-Terminal String 32 bit length                      
<tr>
<td>data-chunk
<td>x8F
<td>-
<td>8
<td>Non-Terminal String 64 bit length
<tr>
<td>p-int8       
<td>xA0
<td>1
<td>-
<td>Positive 8 bit Integer
<tr>
<td>p-int16    
<td>xA1
<td>2
<td>-
<td>Positive 16 bit Integer
<tr>
<td>p-int32
<td>xA2
<td>4
<td>-
<td>Positive 32 bit Integer
<tr>
<td>p-int64
<td>xA3
<td>8
<td>-
<td>Positive 64 bit Integer
<tr>
<td>p-bignum16 
<td>xA5
<td>-
<td>2
<td>Positive Bignum 16 bit length
<tr>
<td>n-int8       
<td>xA8
<td>1
<td>-
<td>Negative 8 bit Integer
<tr>
<td>n-int16    
<td>xA9
<td>2
<td>-
<td>Negative 16 bit Integer
<tr>
<td>n-int32
<td>xAA
<td>4
<td>-
<td>Negative 32 bit Integer
<tr>
<td>n-int64
<td>xAB
<td>8
<td>-
<td>Negative 64 bit Integer
<tr>
<td>n-bignum16 
<td>xAD
<td>-
<td>2
<td>Negative Bignum 16 bit length
<tr>
<td>binary64
<td>X92
<td>8
<td>-
<td>IEEE 754 Floating Point 64 bit
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

##JSON-B Examples

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
<table=s-5-4>
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
<td>c-tag  
<td>xC0
<td>1
<td>-
<td>8 bit tag code
<tr>
<td>c-tag  
<td>xC1
<td>2
<td>-
<td>16 bit tag code
<tr>
<td>c-tag  
<td>xC2
<td>4
<td>-
<td>32 bit tag code
<tr>
<td>c-def
<td>xC4
<td>1
<td>-
<td>8 bit tag definition
<tr>
<td>c-def  
<td>xC5
<td>2
<td>-
<td>16 bit tag definition
<tr>
<td>c-def  
<td>xC6
<td>4
<td>-
<td>32 bit tag definition
<tr>
<td>c-tag  
<td>xC8
<td>1
<td>-
<td>8 bit tag code & definition
<tr>
<td>c-tag  
<td>xC9
<td>2
<td>-
<td>16 bit tag code & definition
<tr>
<td>c-tag  
<td>xCA
<td>4
<td>-
<td>32 bit tag code & definition
<tr>
<td>c-def  
<td>xCC
<td>1
<td>-
<td>8 bit tag dictionary definition
<tr>
<td>c-def  
<td>xCD
<td>2
<td>-
<td>16 bit tag dictionary definition
<tr>
<td>c-def  
<td>xCE
<td>4
<td>-
<td>32 bit tag dictionary definition
<tr>
<td>dict-hash
<td>xDO
<td>4
<td>1
<td>UDF fingerprint of dictionary
</table>

##JSON-C Examples

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

~~~~
d-value = d-integer | d-float
   
d-float = binary16 | binary32 | binary128 | binary80 |
decimal32 | decimal64 | decimal 128
~~~~
<table=s-6-4>
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
<td>p-int64
<td>xA4
<td>16
<td>-
<td>Positive 128 bit Integer
<tr>
<td>p-int64
<td>xAC
<td>16
<td>-
<td>Positive 128 bit Integer
<tr>
<td>p-int64
<td>xA3
<td>8
<td>-
<td>Positive 64 bit Integer
<tr>
<td>binary16
<td>X92
<td>2
<td>-
<td>IEEE 754 Floating Point 16 bit
<tr>
<td>binary32
<td>X92
<td>4
<td>-
<td>IEEE 754 Floating Point 32 bit
<tr>
<td>binary128
<td>X92
<td>16
<td>-
<td>IEEE 754 Floating Point 128 bit
<tr>
<td>intel80
<td>X92
<td>10
<td>-
<td>Intel 80 bit extended binary Floating Point
<tr>
<td>decimal32
<td>X92
<td>4
<td>-
<td>IEEE 754 Floating Point Decimal 32 bit
<tr>
<td>decimal64
<td>X92
<td>8
<td>-
<td>IEEE 754 Floating Point Decimal 64 bit
<tr>
<td>decimal128
<td>X92
<td>16
<td>-
<td>IEEE 754 Floating Point Decimal 128 bit
</table>

#Acknowledgements

#Security Considerations

#IANA Considerations
