using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace DomainerShell; 
    public partial class CommandLineInterpreter : CommandLineInterpreterBase {
        
	/// <summary>The command entries</summary>
        public static SortedDictionary<string, DescribeCommand> Entries;
        /// <summary>The default command.</summary>
        public static DescribeCommandEntry DefaultCommand;
        /// <summary>Description of the comman</summary>
        public static string Description = "<Not specified>";

	static char UnixFlag = '-';
	static char WindowsFlag = '/';


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dispatch"></param>
        /// <param name="args"></param>
        /// <param name="index"></param>
        public static void Help(DispatchShell Dispatch, string[] args, int index) => Brief(Description, DefaultCommand, Entries);

        public static DescribeCommandEntry DescribeHelp = new() {
            Identifier = "help",
            HandleDelegate = Help,
            Entries = new List<DescribeEntry>() { }
            };

        /// <summary>
        /// Describe the application invoked by the command.
        /// </summary>
        /// <param name="Dispatch">The command description.</param>
        /// <param name="args">The set of arguments.</param>
        /// <param name="index">The first unparsed argument.</param>
        public static void About(DispatchShell Dispatch, string[] args, int index) => FileTools.About();

        public static DescribeCommandEntry DescribeAbout = new() {
            Identifier = "about",
            HandleDelegate = About,
            Entries = new List<DescribeEntry>() { }
            };

        static bool IsFlag(char c) => (c == UnixFlag) | (c == WindowsFlag);


        static CommandLineInterpreter () {
            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

            if (OperatingSystem.Platform == PlatformID.Unix |
                    OperatingSystem.Platform == PlatformID.MacOSX) {
                FlagIndicator = UnixFlag;
                }
            else {
                FlagIndicator = WindowsFlag;
                }

			DefaultCommand = _GenerateDomainer._DescribeCommand;
			Description = "Manage DNS Resource and Query Records";

		Entries = new  SortedDictionary<string, DescribeCommand> () {
			{"about", DescribeAbout },
			{"in", _GenerateDomainer._DescribeCommand },
			{"help", DescribeHelp }
			}; // End Entries



            }

        static void Main(string[] args) {
		var CLI = new CommandLineInterpreter ();
		CLI.MainMethod (args);
		}

        public void MainMethod(string[] Args) {
		DomainerShell Dispatch = new();

		MainMethod (Dispatch, Args);
		}


        public void MainMethod(DomainerShell Dispatch, string[] Args) => Dispatcher(Entries, DefaultCommand, Dispatch, Args, 0); // Main



        public static void Handle_GenerateDomainer (
				DispatchShell  DispatchIn, string[] Args, int Index) {
		DomainerShell Dispatch =	DispatchIn as DomainerShell;
		GenerateDomainer		Options = new();
		ProcessOptions (Args, Index, Options);
		Dispatch.GenerateDomainer (Options);
		}


} // class Main


// The stub class for carrying optional parameters for each command type
// As with the main class each consists of an abstract main class 
// with partial virtual that can be extended as required.

// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
// and Goedel.Command.Type


    public class _GenerateDomainer : Goedel.Command.Dispatch  {

	public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
		new Flag (),
		new ExistingFile (),
		new NewFile ()			} ;

	/// <summary>Field accessor for parameter [lazy]</summary>
	public virtual Flag Lazy {
		get => _Data[0] as Flag;
		set => _Data[0]  = value;
		}

	public virtual string _Lazy {
		set => _Data[0].Parameter (value);
		}
	/// <summary>Field accessor for parameter []</summary>
	public virtual ExistingFile Domainer {
		get => _Data[1] as ExistingFile;
		set => _Data[1]  = value;
		}

	public virtual string _Domainer {
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
	public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

	public static DescribeCommandEntry _DescribeCommand = new() {
		Identifier = "in",
		Brief =  "<Unspecified>",
		HandleDelegate =  CommandLineInterpreter.Handle_GenerateDomainer,
		Lazy =  true,
		Entries = new List<DescribeEntry> () {
			new DescribeEntryParameter () {
				Identifier = "Domainer", 
				Default = null, // null if null
				Brief = "<Unspecified>",
				Index = 1,
				Key = ""
				},
			new DescribeEntryOption () {
				Identifier = "GenerateCS", 
				Default = "cs", // null if null
				Brief = "Generate C# code",
				Index = 2,
				Key = "cs"
				}
			}
		};

	}

    public partial class GenerateDomainer : _GenerateDomainer {
        } // class GenerateDomainer


    public partial class  NewFile : _NewFile {
        public static NewFile Factory (string Value) {
            var Result = new NewFile();
            Result.Default(Value);
            return Result;
            }
        } // NewFile


    public partial class  ExistingFile : _ExistingFile {
        public static ExistingFile Factory (string Value) {
            var Result = new ExistingFile();
            Result.Default(Value);
            return Result;
            }
        } // ExistingFile


    public partial class  Flag : _Flag {
        public static Flag Factory (string Value) {
            var Result = new Flag();
            Result.Default(Value);
            return Result;
            }
        } // Flag



// The stub class just contains routines that echo their arguments and
// write 'not yet implemented'

// Eventually there will be a compiler option to suppress the debugging
// to eliminate the redundant code
    public class _DomainerShell : global::Goedel.Command.DispatchShell {

	public virtual void GenerateDomainer ( GenerateDomainer Options) {
		string inputfile = null;

		inputfile = Options.Domainer.Text;

            Goedel.Tool.Domainer.Domainer Parse = new() {
			};
        
		
		using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new(inputfile);

                Schema.Process(infile, Parse);
                }


		// Script output of type GenerateCS cs
		if (Options.GenerateCS.Text != null) {
			string outputfile = Options.GenerateCS.Text; // Automatically defaults
			if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
				return;
				}
                using Stream outputStream =
                            new FileStream(outputfile, FileMode.Create, FileAccess.Write);
                using TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8);
                Goedel.Tool.Domainer.Generate Script = new() {
				_Output = OutputWriter
				};
			Script.GenerateCS(Parse);
                }
		}


        } // class _DomainerShell

    public partial class DomainerShell : _DomainerShell {
        } // class DomainerShell

    

