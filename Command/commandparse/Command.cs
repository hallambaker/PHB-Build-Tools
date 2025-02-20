
//  This file was automatically generated at 2/20/2025 1:35:39 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  commandparse version 3.0.0.978
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
using Goedel.Registry;
using Goedel.Utilities;
#pragma warning disable IDE0079
#pragma warning disable IDE1006
#pragma warning disable CS1591

namespace CommandShell;



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





    static CommandLineInterpreter () {
        System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

        if (OperatingSystem.Platform == PlatformID.Unix |
                OperatingSystem.Platform == PlatformID.MacOSX) {
            FlagIndicator = UnixFlag;
            }
        else {
            FlagIndicator = WindowsFlag;
            }

			DefaultCommand = _GenerateCommand._DescribeCommand;
			Description = "Command Line Parser Generator";

		Entries = new   () {
			{"in", _GenerateCommand._DescribeCommand },
			{"about", DescribeAbout },
			{"help", DescribeHelp }
			}; // End Entries



        }

    static void Main(string[] args) {
		var CLI = new CommandLineInterpreter ();
		CLI.MainMethod (args);
		}

    public void MainMethod(string[] Args) {
		CommandShell Dispatch = new ();

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


    public void MainMethod(CommandShell Dispatch, string[] Args) =>
		Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




	public static void Handle_GenerateCommand (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		CommandShell Dispatch =	DispatchIn as CommandShell;
		GenerateCommand		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.GenerateCommand (Options);
		}


} // class Main


// The stub class for carrying optional parameters for each command type
// As with the main class each consists of an abstract main class 
// with partial virtual that can be extended as required.

// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
// and Goedel.Command.Type


public class _GenerateCommand : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new Flag (),
		new ExistingFile (),
		new NewFile (),
		new Flag (),
		new Flag (),
		new Flag ()		} ;





	/// <summary>Field accessor for parameter [lazy]</summary>
	public virtual Flag Lazy {
		get => _Data[0] as Flag;
		set => _Data[0]  = value;
		}

	public virtual string _Lazy {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile CommandParse {
		get => _Data[1] as ExistingFile;
		set => _Data[1]  = value;
		}

	public virtual string _CommandParse {
		set => _Data[1].Parameter (value);
		}
	/// <summary>Field accessor for option [cs]</summary>
	public virtual NewFile Generate {
		get => _Data[2] as NewFile;
		set => _Data[2]  = value;
		}

	public virtual string _Generate {
		set => _Data[2].Parameter (value);
		}
	/// <summary>Field accessor for option [main]</summary>
	public virtual Flag Main {
		get => _Data[3] as Flag;
		set => _Data[3]  = value;
		}

	public virtual string _Main {
		set => _Data[3].Parameter (value);
		}
	/// <summary>Field accessor for option [builtins]</summary>
	public virtual Flag Builtins {
		get => _Data[4] as Flag;
		set => _Data[4]  = value;
		}

	public virtual string _Builtins {
		set => _Data[4].Parameter (value);
		}
	/// <summary>Field accessor for option [catch]</summary>
	public virtual Flag Catcher {
		get => _Data[5] as Flag;
		set => _Data[5]  = value;
		}

	public virtual string _Catcher {
		set => _Data[5].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "in",
		Brief =  "Generate a command line inteface parser",
		HandleDelegate =  CommandLineInterpreter.Handle_GenerateCommand,
		Lazy =  true,
        IsDefault = true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "CommandParse", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 1,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "Generate", 
				Default = "cs", // null if null
				Brief = "Generate code for C#",
				Index = 2,
				Key = "cs"
				},
			new DescribeEntryOption () {
				Identifier = "Main", 
				Default = "true", // null if null
				Brief = "If set, generate a main class",
				Index = 3,
				Key = "main"
				},
			new DescribeEntryOption () {
				Identifier = "Builtins", 
				Default = "true", // null if null
				Brief = "If set, include the built in types NewFile, SourceFile and Flags.",
				Index = 4,
				Key = "builtins"
				},
			new DescribeEntryOption () {
				Identifier = "Catcher", 
				Default = "true", // null if null
				Brief = "If set, wrap the main calling loop with a try/catch structure.",
				Index = 5,
				Key = "catch"
				}
			}
		};

	}

public partial class GenerateCommand : _GenerateCommand {
    } // class GenerateCommand


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
public class _CommandShell : global::Goedel.Command.DispatchShell {

	public virtual void GenerateCommand ( GenerateCommand Options) {
		string inputfile = null;

		inputfile = Options.CommandParse.Text;

        Goedel.Tool.Command.CommandParse Parse = new () {
			Main = Options.Main.Value,
			Builtins = Options.Builtins.Value,
			Catcher = Options.Catcher.Value,
			};
        
			
		using (Stream infile =
                    new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {
            Lexer Schema = new Lexer(inputfile);
            Schema.Process(infile, Parse);
            }
		Parse.Init();


		// Script output of type Generate cs
		if (Options.Generate.Text != null) {
			string outputfile = Options.Generate.Text; // Automatically defaults
			if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
				return;
				}
            using Stream outputStream =
                        new FileStream(outputfile, FileMode.Create, FileAccess.Write);
            using TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8);

			Goedel.Tool.Command.GenerateCS Script = new () { 
				_Output= OutputWriter 
				};
			Script.Generate (Parse);
			}
		}


    } // class _CommandShell

public partial class CommandShell : _CommandShell {
    } // class CommandShell



