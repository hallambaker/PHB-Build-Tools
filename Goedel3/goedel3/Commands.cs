
//  This file was automatically generated at 19-Jun-23 3:14:32 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  commandparse version 3.0.0.879
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2021
//  
//  Build Platform: Win32NT 10.0.19042.0
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

namespace Command;



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

			DefaultCommand = _Schema._DescribeCommand;
			Description = "Goedel meta-code generation tool";

		Entries = new   () {
			{"in", _Schema._DescribeCommand }
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


public class _Schema : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new ExistingFile (),
		new Flag (),
		new NewFile (),
		new Flag (),
		new Flag (),
		new Flag (),
		new Flag ()		} ;





	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile Goedel {
		get => _Data[0] as ExistingFile;
		set => _Data[0]  = value;
		}

	public virtual string _Goedel {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for parameter [lazy]</summary>
	public virtual Flag Lazy {
		get => _Data[1] as Flag;
		set => _Data[1]  = value;
		}

	public virtual string _Lazy {
		set => _Data[1].Parameter (value);
		}
	/// <summary>Field accessor for option [cs]</summary>
	public virtual NewFile GenerateCS {
		get => _Data[2] as NewFile;
		set => _Data[2]  = value;
		}

	public virtual string _GenerateCS {
		set => _Data[2].Parameter (value);
		}
	/// <summary>Field accessor for option [dlexer]</summary>
	public virtual Flag DebugLexer {
		get => _Data[3] as Flag;
		set => _Data[3]  = value;
		}

	public virtual string _DebugLexer {
		set => _Data[3].Parameter (value);
		}
	/// <summary>Field accessor for option [dparser]</summary>
	public virtual Flag DebugParser {
		get => _Data[4] as Flag;
		set => _Data[4]  = value;
		}

	public virtual string _DebugParser {
		set => _Data[4].Parameter (value);
		}
	/// <summary>Field accessor for option [dstack]</summary>
	public virtual Flag DebugStack {
		get => _Data[5] as Flag;
		set => _Data[5]  = value;
		}

	public virtual string _DebugStack {
		set => _Data[5].Parameter (value);
		}
	/// <summary>Field accessor for option [serial]</summary>
	public virtual Flag Serializer {
		get => _Data[6] as Flag;
		set => _Data[6]  = value;
		}

	public virtual string _Serializer {
		set => _Data[6].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "in",
		Brief =  "Convert a Goedel schema file to code",
		HandleDelegate =  CommandLineInterpreter.Handle_Schema,
		Lazy =  true,
        IsDefault = true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "Goedel", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 0,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "GenerateCS", 
				Default = "cs", // null if null
				Brief = "Generate C# code",
				Index = 2,
				Key = "cs"
				},
			new DescribeEntryOption () {
				Identifier = "DebugLexer", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 3,
				Key = "dlexer"
				},
			new DescribeEntryOption () {
				Identifier = "DebugParser", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 4,
				Key = "dparser"
				},
			new DescribeEntryOption () {
				Identifier = "DebugStack", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 5,
				Key = "dstack"
				},
			new DescribeEntryOption () {
				Identifier = "Serializer", 
				Default = "true", // null if null
				Brief = "<Unspecified>",
				Index = 6,
				Key = "serial"
				}
			}
		};

	}

public partial class Schema : _Schema {
    } // class Schema


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

	public virtual void Schema ( Schema Options) {
		string inputfile = null;

		inputfile = Options.Goedel.Text;

        GoedelSchema.Goedel Parse = new () {
			DebugLexer = Options.DebugLexer.Value,
			DebugParser = Options.DebugParser.Value,
			DebugStack = Options.DebugStack.Value,
			Serializer = Options.Serializer.Value,
			};
        
			
		using (Stream infile =
                    new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {
            Lexer Schema = new Lexer(inputfile);
            Schema.Process(infile, Parse);
            }
		Parse.Init();


		// Script output of type GenerateCS cs
		if (Options.GenerateCS.Text != null) {
			string outputfile = Options.GenerateCS.Text; // Automatically defaults
			if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
				return;
				}
            using Stream outputStream =
                        new FileStream(outputfile, FileMode.Create, FileAccess.Write);
            using TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8);

			GoedelSchema.GenerateParser Script = new () { 
				_Output= OutputWriter 
				};
			Script.GenerateCS (Parse);
			}
		}


    } // class _Command

public partial class Command : _Command {
    } // class Command



