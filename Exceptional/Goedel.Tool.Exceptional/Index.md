=Exceptional


==Source file

Sample file

~~~~
Using Goedel.FSR

Namespace Goedel.Cryptography
    
	Exception CryptographicException
        Console "A cryptographic exception occurred"


		Exception NoProviderSpecified
			Console "No provider specified"
			Description
				|The specified key did not have a valid cryptographic
				|provider. This may be because the key algorithm is 
				|not supported or the key parameters were found to be invalid.

		Exception ParseError
			Object ExceptionData "The file {0} could not be read"
				String
			Object LexData "A parse error was encountered in file {0} Line {1} Col {2}"
				FileName
				Line
				Col
~~~~

* The Using field imports names from the specified name space allowing
   use of that type in output formatting.

* The toplevel namespace should match that of the class being defined.

* Exceptions may be nested

* Console is simply text written to the terminal. 

* Object allows an object parameter to be specified containing fields that 
   provide more detailed descriptions such as the line number an error occurred.


==Use in a program.

~~~~
using Goedel.Utilities;  // For the assertion package

class Test () {
    
	Assert (false, NoProviderSpecified.Throw); // throw an exception

    }

~~~~

==With parameters

Parameters may be passed in using two different mechanisms. If the 

~~~~
using Goedel.Utilities;  // For the assertion package

class Test () {
    
	Assert (false, ParseError.Throw, String:"filename" ); 

	var LexData = new LexData () {FileName="Filename", Line=10, Col=23}
	Assert (false, ParseError.Throw, Reason : LexData ); 

    }

~~~~