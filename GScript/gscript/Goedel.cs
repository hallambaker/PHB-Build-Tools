
//  This file was automatically generated at 12/18/2021 1:09:17 AM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  commandparse version 3.0.0.761
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2021
//  
//  Build Platform: Win32NT 10.0.18362.0
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Utilities;
#pragma warning disable IDE1006
#pragma warning disable CS1591

namespace GoedelShell;



public partial class CommandLineInterpreter : CommandLineInterpreterBase {
        



	/// <summary>The command entries</summary>
    public static SortedDictionary<string, DescribeCommand> Entries { get; set; }
	/// <summary>The default command.</summary>
	public static DescribeCommandEntry DefaultCommand { get; set; }
	/// <summary>Description of the comman</summary>
	public static string Description { get; set; } = "<Not specified>";

	static readonly char UnixFlag = '-';
	static readonly char WindowsFlag = '/';






    static CommandLineInterpreter () {
        System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

        if (OperatingSystem.Platform == PlatformID.Unix |
                OperatingSystem.Platform == PlatformID.MacOSX) {
            FlagIndicator = UnixFlag;
            }
        else {
            FlagIndicator = WindowsFlag;
            }

			DefaultCommand = _Generate._DescribeCommand;
			Description = "Goedel meta-code generation tool";

		Entries = new   () {
			{"in", _Generate._DescribeCommand },
			{"wrap", _Wrap._DescribeCommand },
			{"about", _About._DescribeCommand }
			}; // End Entries



        }

    static void Main(string[] args) {
		var CLI = new CommandLineInterpreter ();
		CLI.MainMethod (args);
		}

    public void MainMethod(string[] Args) {
		GoedelShell Dispatch = new ();

		try {
			MainMethod (Dispatch, Args);
			}
        catch (Goedel.Command.ParserException) {
			Brief(Description, DefaultCommand, Entries);
			}
        catch (System.Exception Exception) {
            Console.WriteLine("Application: {0}", Exception.Message);
            }
		}


    public void MainMethod(GoedelShell Dispatch, string[] Args) =>
		Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




	public static void Handle_Generate (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		GoedelShell Dispatch =	DispatchIn as GoedelShell;
		Generate		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.Generate (Options);
		}

	public static void Handle_Wrap (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		GoedelShell Dispatch =	DispatchIn as GoedelShell;
		Wrap		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.Wrap (Options);
		}

	public static void Handle_About (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		GoedelShell Dispatch =	DispatchIn as GoedelShell;
		About		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.About (Options);
		}


} // class Main


// The stub class for carrying optional parameters for each command type
// As with the main class each consists of an abstract main class 
// with partial virtual that can be extended as required.

// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
// and Goedel.Command.Type


public class _Generate : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new ExistingFile (),
		new NewFile (),
		new Flag (),
		new Flag (),
		new Flag ()		} ;





	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile InputFile {
		get => _Data[0] as ExistingFile;
		set => _Data[0]  = value;
		}

	public virtual string _InputFile {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for parameter []</summary>
	public virtual NewFile OutputFile {
		get => _Data[1] as NewFile;
		set => _Data[1]  = value;
		}

	public virtual string _OutputFile {
		set => _Data[1].Parameter (value);
		}
	/// <summary>Field accessor for option [line]</summary>
	public virtual Flag CommentLine {
		get => _Data[2] as Flag;
		set => _Data[2]  = value;
		}

	public virtual string _CommentLine {
		set => _Data[2].Parameter (value);
		}
	/// <summary>Field accessor for option [link]</summary>
	public virtual Flag Directive {
		get => _Data[3] as Flag;
		set => _Data[3]  = value;
		}

	public virtual string _Directive {
		set => _Data[3].Parameter (value);
		}
	/// <summary>Field accessor for option [lazy]</summary>
	public virtual Flag Lazy {
		get => _Data[4] as Flag;
		set => _Data[4]  = value;
		}

	public virtual string _Lazy {
		set => _Data[4].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "in",
		Brief =  "<Unspecified>",
		HandleDelegate =  CommandLineInterpreter.Handle_Generate,
		Lazy =  false,
        IsDefault = true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "InputFile", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 0,
				Key = ""
				},
			new DescribeEntryParameter () {
				Identifier = "OutputFile", 
				Default = "cs", // null if null
				Brief = "<Unspecified>",
				Index = 1,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "CommentLine", 
				Default = null, // null if null
				Brief = "If set, include source in generated as comments",
				Index = 2,
				Key = "line"
				},
			new DescribeEntryOption () {
				Identifier = "Directive", 
				Default = null, // null if null
				Brief = "If set, link generated code to source",
				Index = 3,
				Key = "link"
				},
			new DescribeEntryOption () {
				Identifier = "Lazy", 
				Default = null, // null if null
				Brief = "Only generate code if source or generator have changed",
				Index = 4,
				Key = "lazy"
				}
			}
		};

	}

public partial class Generate : _Generate {
    } // class Generate

public class _Wrap : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new ExistingFile (),
		new NewFile (),
		new String (),
		new String (),
		new String (),
		new Flag ()		} ;





	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile InputFile {
		get => _Data[0] as ExistingFile;
		set => _Data[0]  = value;
		}

	public virtual string _InputFile {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for option [cs]</summary>
	public virtual NewFile CS {
		get => _Data[1] as NewFile;
		set => _Data[1]  = value;
		}

	public virtual string _CS {
		set => _Data[1].Parameter (value);
		}
	/// <summary>Field accessor for option [namespace]</summary>
	public virtual String Namespace {
		get => _Data[2] as String;
		set => _Data[2]  = value;
		}

	public virtual string _Namespace {
		set => _Data[2].Parameter (value);
		}
	/// <summary>Field accessor for option [class]</summary>
	public virtual String Class {
		get => _Data[3] as String;
		set => _Data[3]  = value;
		}

	public virtual string _Class {
		set => _Data[3].Parameter (value);
		}
	/// <summary>Field accessor for option [variable]</summary>
	public virtual String Variable {
		get => _Data[4] as String;
		set => _Data[4]  = value;
		}

	public virtual string _Variable {
		set => _Data[4].Parameter (value);
		}
	/// <summary>Field accessor for option [lazy]</summary>
	public virtual Flag Lazy {
		get => _Data[5] as Flag;
		set => _Data[5]  = value;
		}

	public virtual string _Lazy {
		set => _Data[5].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "wrap",
		Brief =  "<Unspecified>",
		HandleDelegate =  CommandLineInterpreter.Handle_Wrap,
		Lazy =  false,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "InputFile", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 0,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "CS", 
				Default = "cs", // null if null
				Brief = "<Unspecified>",
				Index = 1,
				Key = "cs"
				},
			new DescribeEntryOption () {
				Identifier = "Namespace", 
				Default = "Constants", // null if null
				Brief = "<Unspecified>",
				Index = 2,
				Key = "namespace"
				},
			new DescribeEntryOption () {
				Identifier = "Class", 
				Default = "Constants", // null if null
				Brief = "<Unspecified>",
				Index = 3,
				Key = "class"
				},
			new DescribeEntryOption () {
				Identifier = "Variable", 
				Default = "Value", // null if null
				Brief = "<Unspecified>",
				Index = 4,
				Key = "variable"
				},
			new DescribeEntryOption () {
				Identifier = "Lazy", 
				Default = null, // null if null
				Brief = "Only generate code if source or generator have changed",
				Index = 5,
				Key = "lazy"
				}
			}
		};

	}

public partial class Wrap : _Wrap {
    } // class Wrap

public class _About : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {		} ;





	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "about",
		Brief =  "Report tool version and build date",
		HandleDelegate =  CommandLineInterpreter.Handle_About,
		Lazy =  false,
		Entries = new List<DescribeEntry> () {
			}
		};

	}

public partial class About : _About {
    } // class About


public partial class  Flag : Goedel.Command._Flag {
    public Flag(string value=null) : base (value) {}
    } // Flag

public partial class  File : Goedel.Command._File {
	public File(string value=null) : base (value) {}
    } // File

public partial class  NewFile : Goedel.Command._NewFile {
	public NewFile(string value=null) : base (value) {}
    } // NewFile

public partial class  ExistingFile : Goedel.Command._ExistingFile {
	public ExistingFile(string value=null) : base (value) {}
    } // ExistingFile

public partial class  Integer : Goedel.Command._Integer {
	public Integer(string value=null) : base (value) {}
    } // Integer

public partial class  String : Goedel.Command._String {
	public String(string value=null) : base (value) {}
    } // String



public partial class  Enumeration<T> : _Enumeration<T> {
    public Enumeration(DescribeEntryEnumerate description) : base(description){
        }
    } // _Enumeration<T>

// The stub class just contains routines that echo their arguments and
// write 'not yet implemented'

// Eventually there will be a compiler option to suppress the debugging
// to eliminate the redundant code
public class _GoedelShell : global::Goedel.Command.DispatchShell {

	public virtual void Generate ( Generate Options) =>
		CommandLineInterpreter.DescribeValues (Options);

	public virtual void Wrap ( Wrap Options) =>
		CommandLineInterpreter.DescribeValues (Options);

	public virtual void About ( About Options) =>
		CommandLineInterpreter.DescribeValues (Options);


    } // class _GoedelShell

public partial class GoedelShell : _GoedelShell {
    } // class GoedelShell



