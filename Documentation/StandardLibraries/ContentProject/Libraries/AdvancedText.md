<id>85d58409-5b24-430c-94b8-43fad4ea2892
<version>1
<contenttype>developerConceptualDocument

The _Goedel.Utilities_ namespace contains simple text handling tools that do not require
the use of a lexical analyzer or support classes. The _Goedel.Registry_ namespace contains
more advanced text utilities that do make use of or define additional classes.

#GScript Support Classes

##Convert text to language sensitive label

These routines are typically used in GScript projects to generate variables that comply with
requirements for computing languages such as C#. Since most projects only generate a 
single output type, the language target may be defaulted by setting the propery
ExtensionMethods._Target

~~~~cs
ExtensionMethods._Target = "cs";
Console.Write (" Test".Label());
~~~~

Should produce the output 

~~~~
_Test
~~~~

Alternatively, call the methods directly, this has the advantage of being type safe.

~~~~
Console.Write (" Test".CS());
~~~~


##Quoted string

* string Quoted (this string Base)

* string Quoted(this List<string> Base)

##Conditional Text

<dt>string If(this bool Value, string Text) 
<dd>return the string value if the condition is met, otherwise an empty string.

<dt>string If(this bool Value, string TrueText, string FalseText) 
<dd>Return the first string value if a condition is met, otherwise return the second

<dt>To Be Specified stub. Writes out the value to the console an returns the string.
<dd>string TBS (this string Value, bool Bold=true) 

##Separator Class

It is often necessary to create lists that have punctuation (e.g. a comma) between
the items without a trailing comma. The separator class allows this to be done with
a single line of code rather than having an if/then/else construct every time this
is needed.

The constructor for Separator specifies either a Next string or a First string and
a Next string. The first time the ToString method is called on the class, the First 
string is output (or an empty string if only one string was specified). The second and
future times the <tt>ToString()<tt> method is called, the <tt>Next<tt> string is returned.

~~~~cs
var Texts = new List<string> {"one", "two", "three"};
var Separator = new Separator (", ");
foreach (var Text in Texts) {
	Console.Write ("{0}{1}", Separator.ToString(), Text);
    } 

// Output is one,two,three
~~~~

The Separator instance can be reset by setting the <tt>IsFirst</tt> property to <tt>true</tt>

##XMLTextWriter Class

The <tt>XMLTextWriter</tt> class is a simpler and more reliable means of generating XML text 
output than the .NET classes. Sorry, but faffing about fighting the tools meant for 
convenience to do the right thing gets tedious.


##Script Class

The Script class is the base class for all script classes generated using GScript. It
contains tools that allow line indentation to be set globally and to expose various
machine and assembly properties in a convenient fashion.

##Boilerplate Class

The Boilerplate class generates boilerplate text for the major open source licenses.

#Goedel3 Support Classes.

##Lexer Class

The Lexer class is the base class for the Goedel schema parser. The code is old and does not
use many of the features of the code that was built with it.

<dt>Source and Position Classes
<dd>Tracks source files and the line and column number at which errors occurred.

<dt>Registry
<dd>Tracks Labels, Identifiers and Tokens defined in a schema. A label must occur
exactly once. An Identifier may occur any number of times but must be matched by
a Label definition. A Token may occur any number of times and may or may not be 
matched by a label.


