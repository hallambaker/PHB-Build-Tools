<id>43492f57-fc54-4884-8024-eb99454fe959
<version>2
<contenttype>developerConceptualDocument

The Exceptional tool is used to streamline use and reporting of 
exceptions. Each project has a file Exceptions.Exceptional that contains
the set of exceptions declared. Exceptions are thrown using either the
C# throw keyword or the convenience routines provided in the Assert class.

#Declaring exceptions using Exceptional</title>

The exceptions definition file for Goedel.Cryptography begins as follows:

~~~~
[Namespace Goedel.Cryptography
    Exception CryptographicException
        Console "A cryptographic exception occurred"
        Description
            |Base class for cryptographic exceptions.
		
        Exception ImplementationLimit
            Console "Some implementation limit hit"
            Description
                |Placeholder exception to be thrown as a placeholder to mark
                |code needing improvement.      
~~~~

The first line defines the namespace for the generated code
(Goedel.Cryptography). Following this there are two nested exception
declarations. The first, CryptographicException is the base class 
for all the cryptographic exceptions thrown. The second is a more specific
exception that is thrown when an implementation limit is hit.

Each exception has a report string that is written to the console
when the exception is hit and a longer description that is used in the
documentation of the exception. The console report may include 
standard C# format strings.



The Exceptions.Exceptional file is converted to a C# file using the
tool 'exceptional' This is available as either a command line tool 
or in the Visual Studio environment as an integrated runtime tool.

The code generator creates the usual constructors defined for a 
C# exception class, i.e.

~~~~C#
public CryptographicException () : base ("A cryptographic exception occurred") {
    }
public CryptographicException (string Description) : base (Description) {
    }
public CryptographicException (string Description, System.Exception Inner) :
          base (Description, Inner) {
    }
public static global::Goedel.Utilities.ThrowDelegate Throw;

static System.Exception _Throw(object Reason) {
    if (Reason as string != null) {
        return new CryptographicException(Reason as string);
        }
    else {
        return new CryptographicException();
        }
    }
~~~~


#Throwing exceptions.

The exceptions can be thrown using the usual throw new x () style.
The console text will be added automatically.

The assert utilities provide a way to simplify the typical
type of 'test/throw' pre or post condition checking. These
utilities take the condition to be tested as their first argument
and a delegate that throws the exception if the check fails as
the second. For example:

~~~~C#
Assert.True (1==2, CryptographicException.Throw);
~~~~


Context specific data may be passed to the exception using a third optional
parameter of type object. This is used in applications such as parsers
to report where an error (line, colum, file name).

~~~~C#
Assert.True (Token != Token.Invalid, ParseError, Position);
~~~~

This style of exception handling greatly simplifies tasks such as
parameter validation. What would take dozens of lines in normal C# can be
achieved in three or four.

The following assertion methods are defined:

<dl>
<dt>False
<dd>Throws the specified exception if the test value is not false.
<dt>True
<dd>Throws the specified exception if the test value is not true.
<dt>Null
<dd>Throws the specified exception if the test value is not null.
<dt>NotNull
<dd>Throws the specified exception if the test value is null.
<dt>Fail
<dd>Always throws the specified exception, used to mark code that should be unreachable
<dt>Fail
<dd>Always throws the NYI exception, used to mark code that is not yet implemented.
</dl>

Note that this scheme is carefully designed to ensure that exceptions are only
created when they are about to be thrown and that the whole scheme is thread safe.


#Exceptions with Parameters

Parameters may be passed to an exception to provide context. All parameters
are passed into the exception by means of an optional object. Individual
parameters being passed as fields on the object. These parameters are passed
into the exception report using the standard C# string formatting commands.

~~~~
Exception FileReadError
		Console "The file could not be read"
		Description
			  |The file could not be read.
		Object ExceptionData "The file {0} could not be read"
			  String
~~~~

An exception may even be created that takes multiple parameter objects.
for use in different contexts. The compiler will select the correct format 
string for the object passed. If no object is specified, the default string is 
used.

To thow an exception of the desired type, the caller just presents the 
object when calling it. This may be done with either a direct call to
throw the exception or with the assert convenience function.

~~~~C#
throw new FileReadError (ExceptionData.Box(String:Filename));
Assert.True (false, FileReadError.Throw, ExceptionData.Box(String:Filename));      
~~~~

The class ExceptionData is provided to provide a convenient way to 
pass a string or integer parameter into an exception. The static
method Box is a factory method that returns a class with the 
requested contents.

#Further Work Required

<ul>
<li>The exceptions thrown in code throwing exceptions are not added to 
the XML comments automatically.

<li>Generated classes should be defined as partial so as to allow convenience 
constructors to be defined at the user level.
</ul>
