
//  This file was automatically generated at 2/14/2025 1:03:23 AM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  commandparse version 3.0.0.879
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2021
//  
//  Build Platform: Win32NT 10.0.26100.0
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Utilities;
#pragma warning disable IDE0079
#pragma warning disable IDE1006
#pragma warning disable CS1591

namespace Command;

// Enumeration type
public enum EnumReporting {
    /// <summary>Case "report": Report output (default)</summary>
    Report,
    /// <summary>Case "silent": Suppress output</summary>
    Silent,
    /// <summary>Case "verbose": Verbose reports</summary>
    Verbose,
    /// <summary>Case "json": Report output in JSON format</summary>
    Json
	}



public partial class CommandLineInterpreter : CommandLineInterpreterBase {
        



	/// <summary>The command entries</summary>
    public static SortedDictionary<string, DescribeCommand> Entries { get; set; }
	/// <summary>The default command.</summary>
	public static DescribeCommandEntry DefaultCommand { get; set; }
	/// <summary>Description of the comman</summary>
	public static string Description { get; set; } = "<Not specified>";

	static readonly char UnixFlag = '-';
	static readonly char WindowsFlag = '/';

    /// <summary>
    /// Default help dispatch
    /// </summary>
    /// <param name="Dispatch">The command description.</param>
    /// <param name="args">The set of arguments.</param>
    /// <param name="index">The first unparsed argument.</param>
    public static void Help (DispatchShell Dispatch, string[] args, int index) =>
        Brief(Description, DefaultCommand, Entries);

    public readonly static DescribeCommandEntry DescribeHelp = new () {
        Identifier = "help",
        HandleDelegate = Help,
        Entries = new () { }
        };
    /// <summary>
    /// Describe the application invoked by the command.
    /// </summary>
    /// <param name="Dispatch">The command description.</param>
    /// <param name="args">The set of arguments.</param>
    /// <param name="index">The first unparsed argument.</param>
    public static void About (DispatchShell Dispatch, string[] args, int index) =>
        FileTools.About();


    public readonly static DescribeCommandEntry DescribeAbout = new () {
        Identifier = "about",
        HandleDelegate = About,
        Entries = new () { }
        };

	public readonly static DescribeEntryEnumerate DescribeEnumReporting = new  () {
        Identifier = "report",
        Brief = "Reporting level",
        Entries = new () { 
			new DescribeCase () {
				Identifier = "report",
				Brief = "Report output (default)",
				Value = (int) EnumReporting.Report
				},
			new DescribeCase () {
				Identifier = "silent",
				Brief = "Suppress output",
				Value = (int) EnumReporting.Silent
				},
			new DescribeCase () {
				Identifier = "verbose",
				Brief = "Verbose reports",
				Value = (int) EnumReporting.Verbose
				},
			new DescribeCase () {
				Identifier = "json",
				Brief = "Report output in JSON format",
				Value = (int) EnumReporting.Json
				}
			}
		};



	public static DescribeCommandSet DescribeCommandSet_Fred => new  () {
        Identifier = "fred",
		Brief = "Useless",
		Entries = new  () {
			} // End Entries
		};


    static CommandLineInterpreter () {
        System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

        if (OperatingSystem.Platform == PlatformID.Unix |
                OperatingSystem.Platform == PlatformID.MacOSX) {
            FlagIndicator = UnixFlag;
            }
        else {
            FlagIndicator = WindowsFlag;
            }

			DefaultCommand = _Script._DescribeCommand;
			Description = "Goedel meta-code generation tool";

		Entries = new   () {
			{"fred", DescribeCommandSet_Fred},
			{"script", _Script._DescribeCommand },
			{"schema", _Schema._DescribeCommand },
			{"about", DescribeAbout },
			{"help", DescribeHelp }
			}; // End Entries



        }

    static void Main(string[] args) {
		var CLI = new CommandLineInterpreter ();
		CLI.MainMethod (args);
		}

    public void MainMethod(string[] Args) {
		Command Dispatch = new ();

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


    public void MainMethod(Command Dispatch, string[] Args) =>
		Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




	public static void Handle_Script (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		Command Dispatch =	DispatchIn as Command;
		Script		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.Script (Options);
		}

	public static void Handle_Schema (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		Command Dispatch =	DispatchIn as Command;
		Schema		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.Schema (Options);
		}


} // class Main


// The stub class for carrying optional parameters for each command type
// As with the main class each consists of an abstract main class 
// with partial virtual that can be extended as required.

// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
// and Goedel.Command.Type


public class _Script : Goedel.Command.Dispatch ,
						ILanguages,
						IReporting{

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new ExistingFile (),
		new NewFile (),
		new NewFile (),
		new NewFile (),
		new Flag (),
		new Enumeration<EnumReporting> (CommandLineInterpreter.DescribeEnumReporting),
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
	/// <summary>Field accessor for option [cs]</summary>
	public virtual NewFile CSharp {
		get => _Data[1] as NewFile;
		set => _Data[1]  = value;
		}

	public virtual string _CSharp {
		set => _Data[1].Parameter (value);
		}
	/// <summary>Field accessor for option [c]</summary>
	public virtual NewFile C {
		get => _Data[2] as NewFile;
		set => _Data[2]  = value;
		}

	public virtual string _C {
		set => _Data[2].Parameter (value);
		}
	/// <summary>Field accessor for option [java]</summary>
	public virtual NewFile Java {
		get => _Data[3] as NewFile;
		set => _Data[3]  = value;
		}

	public virtual string _Java {
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
	/// <summary>Field accessor for parameter [report]</summary>
	public virtual Enumeration<EnumReporting> EnumReporting {
		get => _Data[5] as Enumeration<EnumReporting>;
		set => _Data[5]  = value;
		}

	public virtual string _EnumReporting {
		set => _Data[5].Parameter (value);
		}
	/// <summary>Field accessor for option [line]</summary>
	public virtual Flag CommentLine {
		get => _Data[6] as Flag;
		set => _Data[6]  = value;
		}

	public virtual string _CommentLine {
		set => _Data[6].Parameter (value);
		}
	/// <summary>Field accessor for option [link]</summary>
	public virtual Flag Directive {
		get => _Data[7] as Flag;
		set => _Data[7]  = value;
		}

	public virtual string _Directive {
		set => _Data[7].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "script",
		Brief =  "Convert a Goedel script file to code",
		HandleDelegate =  CommandLineInterpreter.Handle_Script,
		Lazy =  false,
        IsDefault = true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "InputFile", 
				Default = "script", // null if null
				Brief = "Input file",
				Index = 0,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "CSharp", 
				Default = "cs", // null if null
				Brief = "Generate C# code",
				Index = 1,
				Key = "cs"
				},
			new DescribeEntryOption () {
				Identifier = "C", 
				Default = "c", // null if null
				Brief = "Generate C code",
				Index = 2,
				Key = "c"
				},
			new DescribeEntryOption () {
				Identifier = "Java", 
				Default = "java", // null if null
				Brief = "Generate java code",
				Index = 3,
				Key = "java"
				},
			new DescribeEntryOption () {
				Identifier = "Lazy", 
				Default = null, // null if null
				Brief = "Only generate code if source or generator have changed",
				Index = 4,
				Key = "lazy"
				},
			new DescribeEntryEnumerate () {
				Identifier = "EnumReporting", 
				Default = null, // null if null
				Brief = "Reporting level",
				Index = 5,
				Key = "report"
				},
			new DescribeEntryOption () {
				Identifier = "CommentLine", 
				Default = null, // null if null
				Brief = "If set, include source in generated as comments",
				Index = 6,
				Key = "line"
				},
			new DescribeEntryOption () {
				Identifier = "Directive", 
				Default = null, // null if null
				Brief = "If set, link generated code to source",
				Index = 7,
				Key = "link"
				}
			}
		};

	}

public partial class Script : _Script {
    } // class Script

public class _Schema : Goedel.Command.Dispatch ,
						ILanguages{

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new ExistingFile (),
		new NewFile (),
		new NewFile (),
		new NewFile (),
		new Flag (),
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
	/// <summary>Field accessor for option [cs]</summary>
	public virtual NewFile CSharp {
		get => _Data[1] as NewFile;
		set => _Data[1]  = value;
		}

	public virtual string _CSharp {
		set => _Data[1].Parameter (value);
		}
	/// <summary>Field accessor for option [c]</summary>
	public virtual NewFile C {
		get => _Data[2] as NewFile;
		set => _Data[2]  = value;
		}

	public virtual string _C {
		set => _Data[2].Parameter (value);
		}
	/// <summary>Field accessor for option [java]</summary>
	public virtual NewFile Java {
		get => _Data[3] as NewFile;
		set => _Data[3]  = value;
		}

	public virtual string _Java {
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
	/// <summary>Field accessor for option [dlexer]</summary>
	public virtual Flag DebugLexer {
		get => _Data[5] as Flag;
		set => _Data[5]  = value;
		}

	public virtual string _DebugLexer {
		set => _Data[5].Parameter (value);
		}
	/// <summary>Field accessor for option [dparser]</summary>
	public virtual Flag DebugParser {
		get => _Data[6] as Flag;
		set => _Data[6]  = value;
		}

	public virtual string _DebugParser {
		set => _Data[6].Parameter (value);
		}
	/// <summary>Field accessor for option [dstack]</summary>
	public virtual Flag DebugStack {
		get => _Data[7] as Flag;
		set => _Data[7]  = value;
		}

	public virtual string _DebugStack {
		set => _Data[7].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "schema",
		Brief =  "Convert a Goedel schema file to code",
		HandleDelegate =  CommandLineInterpreter.Handle_Schema,
		Lazy =  false,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "InputFile", 
				Default = "gdl", // null if null
				Brief = "<Unspecified>",
				Index = 0,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "CSharp", 
				Default = "cs", // null if null
				Brief = "Generate C# code",
				Index = 1,
				Key = "cs"
				},
			new DescribeEntryOption () {
				Identifier = "C", 
				Default = "c", // null if null
				Brief = "Generate C code",
				Index = 2,
				Key = "c"
				},
			new DescribeEntryOption () {
				Identifier = "Java", 
				Default = "java", // null if null
				Brief = "Generate java code",
				Index = 3,
				Key = "java"
				},
			new DescribeEntryOption () {
				Identifier = "Lazy", 
				Default = null, // null if null
				Brief = "Only generate code if source or generator have changed",
				Index = 4,
				Key = "lazy"
				},
			new DescribeEntryOption () {
				Identifier = "DebugLexer", 
				Default = null, // null if null
				Brief = "Report debug output for the lexical analyzer",
				Index = 5,
				Key = "dlexer"
				},
			new DescribeEntryOption () {
				Identifier = "DebugParser", 
				Default = null, // null if null
				Brief = "Report debug output for the parser",
				Index = 6,
				Key = "dparser"
				},
			new DescribeEntryOption () {
				Identifier = "DebugStack", 
				Default = null, // null if null
				Brief = "Report debug output for the parse stack",
				Index = 7,
				Key = "dstack"
				}
			}
		};

	}

public partial class Schema : _Schema {
    } // class Schema
public interface IReporting {
	}

public interface ILanguages {
	NewFile			CSharp{get; set;}
	NewFile			C{get; set;}
	NewFile			Java{get; set;}
	Flag			Lazy{get; set;}
	}



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
public class _Command : global::Goedel.Command.DispatchShell {

	public virtual void Script ( Script Options) =>
		CommandLineInterpreter.DescribeValues (Options);

	public virtual void Schema ( Schema Options) =>
		CommandLineInterpreter.DescribeValues (Options);


    } // class _Command

public partial class Command : _Command {
    } // class Command



