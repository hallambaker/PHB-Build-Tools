<id>1afdeb2b-c964-42b6-8a19-6296acc0480c
<version>1
<contenttype>developerConceptualDocument

The text conversion libraries provide various useful transformations on text 
strings.

#File Tools


<dt>string Base16(this byte[] Data, int Length = -1)
<dd>

<dt>string Base32(this byte[] Data)
<dd>

<dt>string UDF(this byte[] Data, int Precision)
<dd>

<dt>string Base64(this byte[] Data) 
<dd>

<dt> string Base64URL(this byte[] Data)
<dd>

<dt>void AppendBase16(this StringBuilder StringBuilder, byte[] Data)
<dd>

<dt>void AppendBase32(this StringBuilder StringBuilder, byte[] Data)
<dd>


<dt>void AppendBase64(this StringBuilder StringBuilder, byte[] Data) 
<dd>

<dt>void AppendBase64URL(this StringBuilder StringBuilder, byte[] Data)
<dd>

See the BaseConvert class for more comprehensive functions.


<dt>string ToUTF8 (this byte[] Data)
<dd>
<dt>byte[] ToBytes (this string Text) 
<dd>

<dt>char ToASCII (this int In)
<dd>

<dt>bool IsBase64(this int c)
<dd>

<dt>int CountUTF8 (this string Text)
<dd>

<dt>byte [] ToUTF8 (this string Text) 
<dd>

<dt>int ToUTF8(this string Text, byte[] Buffer, int Position) 
<dd>


<dt>static string XMLEscape (this string In)
<dd>

<dt>string XMLAttributeEscape (this string In)
<dd>

<dt> string Wrap (this string Input, int Length=72)
<dd>


<dt>byte Byte0 (this int Data) ... byte Byte3(this int Data)
<dd>

<dt>byte Byte0 (this int long) ... byte Byte7(this int long)
<dd>

<dt>byte Byte0 (this int ulong) ... byte Byte7(this int ulong)
<dd>

<dt> byte[] BigEndian (this int Data) 
<dd>

<dt> byte[] NetworkByte(this int Data)
<dd>

<dt>void SetBigEndian (this byte[] Array, int Data)
<dt>void SetBigEndian (this byte[] Array, uint Data)
<dt>void SetBigEndian (this byte[] Array, ulong Data)
<li>void SetNetworkByte(this byte[] Array, int Data) 
void SetLittleEndian(this byte[] Array, int Data)
static byte[] BigEndian(this ulong Data)



<dt>string ToRFC3339 (this DateTime DateTime)
<dd>Convert DateTime value to RFC3339 date time representation.

<dt>DateTime FromRFC3339 (this string Text)
<dd>Convert RFC3339 date time string to DateTime.