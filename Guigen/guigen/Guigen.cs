
//  This file was automatically generated at 9/14/2023 5:43:07 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  commandparse version 3.0.0.879
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2021
//  
//  Build Platform: Win32NT 10.0.22621.0
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

namespace Goedel.Shell.Guigen;



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

			DefaultCommand = _MakeGui._DescribeCommand;
			Description = "GUI compiler";

		Entries = new   () {
			{"in", _MakeGui._DescribeCommand },
			{"about", DescribeAbout }
			}; // End Entries



        }

    static void Main(string[] args) {
		var CLI = new CommandLineInterpreter ();
		CLI.MainMethod (args);
		}

    public void MainMethod(string[] Args) {
		GuigenShell Dispatch = new ();

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


    public void MainMethod(GuigenShell Dispatch, string[] Args) =>
		Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




	public static void Handle_MakeGui (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		GuigenShell Dispatch =	DispatchIn as GuigenShell;
		MakeGui		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.MakeGui (Options);
		}


} // class Main


// The stub class for carrying optional parameters for each command type
// As with the main class each consists of an abstract main class 
// with partial virtual that can be extended as required.

// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
// and Goedel.Command.Type


public class _MakeGui : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new Flag (),
		new ExistingFile (),
		new NewFile (),
		new NewFile ()		} ;





	/// <summary>Field accessor for parameter [lazy]</summary>
	public virtual Flag Lazy {
		get => _Data[0] as Flag;
		set => _Data[0]  = value;
		}

	public virtual string _Lazy {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile Guigen {
		get => _Data[1] as ExistingFile;
		set => _Data[1]  = value;
		}

	public virtual string _Guigen {
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
	/// <summary>Field accessor for option [resx]</summary>
	public virtual NewFile GenerateResx {
		get => _Data[3] as NewFile;
		set => _Data[3]  = value;
		}

	public virtual string _GenerateResx {
		set => _Data[3].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "in",
		Brief =  "<Unspecified>",
		HandleDelegate =  CommandLineInterpreter.Handle_MakeGui,
		Lazy =  true,
        IsDefault = true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "Guigen", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 1,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "GenerateCS", 
				Default = "cs", // null if null
				Brief = "Generate cs class",
				Index = 2,
				Key = "cs"
				},
			new DescribeEntryOption () {
				Identifier = "GenerateResx", 
				Default = "resx", // null if null
				Brief = "Generate resource file",
				Index = 3,
				Key = "resx"
				}
			}
		};

	}

public partial class MakeGui : _MakeGui {
    } // class MakeGui


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
public class _GuigenShell : global::Goedel.Command.DispatchShell {

	public virtual void MakeGui ( MakeGui Options) {
		string inputfile = null;

		inputfile = Options.Guigen.Text;

        Goedel.Tool.Guigen.Guigen Parse = new () {
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

			Goedel.Tool.Guigen.Generate Script = new () { 
				_Output= OutputWriter 
				};
			Script.GenerateCS (Parse);
			}
		// Script output of type GenerateResx resx
		if (Options.GenerateResx.Text != null) {
			string outputfile = Options.GenerateResx.Text; // Automatically defaults
			if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
				return;
				}
            using Stream outputStream =
                        new FileStream(outputfile, FileMode.Create, FileAccess.Write);
            using TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8);

			Goedel.Tool.Guigen.Generate Script = new () { 
				_Output= OutputWriter 
				};
			Script.GenerateResx (Parse);
			}
		}


    } // class _GuigenShell

public partial class GuigenShell : _GuigenShell {
    } // class GuigenShell



