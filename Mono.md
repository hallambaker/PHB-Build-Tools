=Using the build tools with non Windows platforms.

The build environment supports two types of cross target development:

1) Using .Net Core 10
2) Using Mono

These two approaches have very different capabilities. Mono is an environment 
that allows (almost) any .NET program to run on a wide range of platforms as
native applications. .Net Core is a sandbox environment similar to Java that
offers true 'write once run anywhere' capability but at the cost of losing 
all platform dependent features and many 'unsafe' features.

Since Mono is the more mature platform and the build tools are designed for
the type of purposes that are not really sandbox compatible, this is usually
the desired target.


==Project filename constraints.

Windows and Unix both have features that allow them to work with file paths that
contain spaces. Using these features on either platform requires code to tread 
code paths that few others have trod. And are therefore to be avoided.

* All filenames must use the standard file extensions
* Only latin character set letters (a-z, A-Z), numbers, the period (.) and 
underscore are permitted in file and directory names.
* Spaces are not permitted in any part of a file name path


==Building on Windows

The only development tool worth using on Windows is Visual Studio. The build
tools are integrated into the Visual Studio shell and work as part of the 
development chain.

The mono tools all work on Windows but the MakeyMakey tool does not support
them on Windows. Since Windows and Unix have different file path syntax, this
is not likely to change unless someone who really really likes Windows but
not Visua Studio wants to do the work.



==Building on Linux

The MakeyMakey tool converts Visual Studio solutions and makefiles to 


==Package configuration

To do any sort of development on Linus or OSX, you need the complete libraries
which are in the distribution mono-complete.

On Debian systems:

~~~~
apt-get install mono-complete
~~~~


==Running on Linux

There are three ways to run code on Linux

1) Invoking the mono CLI as a command line wrapper

2) Invoking the mono runtime automatically by installing Mono as a binfmt
runtime module for invoking executables

3) Bundle the code using the mkbundle tool.

For (2) see this note:
http://www.mono-project.com/archived/guiderunning_mono_applications/