<id>3f75377a-fb17-44ab-a877-a38d0c9cf37b
<version>1
<contenttype>developerConceptualDocument

The file tools provide various convenience functions and syntactic sugar around
the .NET file handling libraries.

#File Tools


<dl>
<dt>FileStream OpenFileRead(this string Filename)
<dd>

<dt>FileStream OpenFileReadShared(this string Filename)
<dd>
<dt>TextReader OpenTextReader(this FileStream FileStream)
<dd>
<dt>TextReader OpenTextReader (this string Filename)
<dd>
<dt>string OpenReadToEnd (this string Filename)
<dd>
<dt>string OpenReadToEnd (this string Filename)
<dd>
<dt>static FileStream OpenFileNew(this string Filename)
<dd>
<dt>FileStream OpenFileWrite(this string Filename)
<dd>
<dt>FileStream OpenFileWriteShare(this string Filename)
<dd>

<dt>FileStream OpenFileAppend(this string Filename) 
<dd>
<dt>FileStream OpenFileAppendShare(this string Filename)
<dd>
<dt>TextWriter OpenTextWriter(this FileStream FileStream)
<dd>
<dt>TextWriter OpenTextWriter(this string Filename)
<dd>

<dt>TextWriter OpenTextWriterNew (this string Filename)
<dd>
<dt>void WriteFileNew(this string Filename, string Text)
<dd>
<dt>void WriteFileNew(this string Filename, byte[] Data)
<dd>
<dt>void Write (this FileStream FileStream, byte[] Data)
<dd>
</dl>

DirectoryTools.DirectoryDelete (string Path)