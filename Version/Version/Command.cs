
//  This file was automatically generated at 8/29/2023 3:15:43 PM
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
using Goedel.Utilities;
#pragma warning disable IDE0079
#pragma warning disable IDE1006
#pragma warning disable CS1591

namespace Goedel.Tool.Version;



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

			DefaultCommand = _Version._DescribeCommand;
			Description = "Version Management tool";

		Entries = new   () {
			{"version", _Version._DescribeCommand },
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




	public static void Handle_Version (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		Command Dispatch =	DispatchIn as Command;
		Version		Options = new ();
		ProcessOptions (Args, Index, Options);
		Dispatch._PreProcess (Options);
		Dispatch.Version (Options);
		}


} // class Main


// The stub class for carrying optional parameters for each command type
// As with the main class each consists of an abstract main class 
// with partial virtual that can be extended as required.

// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
// and Goedel.Command.Type


public class _Version : Goedel.Command.Dispatch {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type[] {
		new ExistingFile (),
		new ExistingFile ()		} ;





	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile InputFile {
		get => _Data[0] as ExistingFile;
		set => _Data[0]  = value;
		}

	public virtual string _InputFile {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile OutputFile {
		get => _Data[1] as ExistingFile;
		set => _Data[1]  = value;
		}

	public virtual string _OutputFile {
		set => _Data[1].Parameter (value);
		}
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public readonly static DescribeCommandEntry _DescribeCommand = new   () {
		Identifier = "version",
		Brief =  "Read version file and update values",
		HandleDelegate =  CommandLineInterpreter.Handle_Version,
		Lazy =  false,
        IsDefault = true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "InputFile", 
				Default = "version", // null if null
				Brief = "Input file",
				Index = 0,
				Key = ""
				},
			new DescribeEntryParameter () {
				Identifier = "OutputFile", 
				Default = "cs", // null if null
				Brief = "Output file",
				Index = 1,
				Key = ""
				}
			}
		};

	}

public partial class Version : _Version {
    } // class Version


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

	public virtual void Version ( Version Options) =>
		CommandLineInterpreter.DescribeValues (Options);


    } // class _Command

public partial class Command : _Command {
    } // class Command



