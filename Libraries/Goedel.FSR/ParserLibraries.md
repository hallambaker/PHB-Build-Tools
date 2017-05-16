<id>9a1e36d4-be25-49e5-8182-8eebe08681d8
<version>1
<contenttype>developerConceptualDocument

The parser libraries provide a means of serializing and deserializing data to
and from ASN.1, JSON or TLS schema formats and encoding and decoding XML, HTML and
Markdown texts. 

At present the data encoding libraries use an assortment of schema definition
languages. It is hoped that at some point it will be possible to use a single
schema language to serve all the formats.

The document handling libraries convert all input text to a stripped form which
corresponds loosely to HTML/2.0 or Markdown.

# FSR Specification File

FSR is a tool from builting Finite State Recognizers from the state transition 
table. The reason this approach is preferred over the Regular Expressions used
in the traditional Lex, is that it makes it much easier to specify actions.

The following text fragment shows an FSR for parsing command line input. 

* The first line specifies the namespace and class to generate.

* The Charset entries specify character ranges for later use. 
   These are not actually needed in the example though.

* The Token entries declare output tokens. The string id just for documentation.

* The State entries declare the set of FSR states and the transitions out of
   that state.

~~~~
FSR Goedel.Registry CommandLex

	Charset Digit		"0" "9"
	Charset alpha		"a" "z"
	Charset ALPHA		"A" "Z"

	Token Empty		""
	Token Value			"example.file"
	Token Flag			"/flag"
	Token FlagValue		"/flag:example.file"


	
	State ItemStart Reset Empty		// Start of file
		On " \t\n" GoTo Fail
		On "/-" GoTo StartFlag
		Any GoTo IsValue

	State IsValue AddValue Value
		Any GoTo IsValue

	State StartFlag Ignore Flag
		Any GoTo IsFlag

	State IsFlag AddFlag Flag
		On ":=" GoTo StartFlagValue
		Any GoTo IsFlag

	State StartFlagValue Ignore FlagValue
		Any GoTo IsFlagValue		

	State IsFlagValue AddValue FlagValue
		Any GoTo IsFlagValue		

	State Fail Abort Empty
~~~~

# Callback code

The FSR tool generates a class that calls a set of methods specified
in the State entries. In this case our methods are:

* Reset
* AddValue
* AddFlag
* Ignore
* Fail

Unlike other Goedel tools, FSR does not generate dummy virtual methods 
to be overwritten in code. These must be specified for the example to compile:

~~~~
namespace Goedel.Registry {
    public partial class CommandLex {
        /// <summary>
        /// Return the resulting string value
        /// </summary>
        public string Value { get => BuildValue.ToString(); }

        /// <summary>
        /// Return the resulting string value
        /// </summary>
        public string Flag { get => BuildFlag.ToString(); }

        StringBuilder BuildValue = new StringBuilder();
        StringBuilder BuildFlag = new StringBuilder();

        /// <summary>
        /// Reset the value buffers to start a new parse.
        /// </summary>
        public override void Reset () {
            BuildValue.Clear();
            BuildFlag.Clear();
            }

        /// <summary>
        /// Add a character to the value buffer
        /// </summary>
        /// <param name="c">The character read</param>
        public virtual void AddValue (int c) {
            BuildValue.Append((char)c);
            }

		... other implementation methods.
		}
	}
~~~~

#LexReader Library

~~~~
        /// <summary>
        /// Construct a parser to read from a string to be specified in GetToken (data)
        /// </summary>
        LexStringReader LexStringReader;
        public CommandLex () {
            LexStringReader = new LexStringReader(null);
            Reader = LexStringReader;
            }

        /// <summary>
        /// Parse the specified string. Note, this is only valid if no LexReader
        /// was specified in the constructor.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Token GetToken (string Data) {
            LexStringReader.String = Data;
            Reset();
            return GetToken();
            }
~~~~

