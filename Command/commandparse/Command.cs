using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace CommandShell {
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
        public static void Help (DispatchShell Dispatch, string[] args, int index) =>
            Brief(Description, DefaultCommand, Entries);

        public static DescribeCommandEntry DescribeHelp = new DescribeCommandEntry() {
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
        public static void About (DispatchShell Dispatch, string[] args, int index) =>
            FileTools.About();


        public static DescribeCommandEntry DescribeAbout = new DescribeCommandEntry() {
            Identifier = "about",
            HandleDelegate = About,
            Entries = new List<DescribeEntry>() { }
            };

        static bool IsFlag(char c) =>
            (c == UnixFlag) | (c == WindowsFlag) ;



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

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"about", DescribeAbout },
				{"in", _GenerateCommand._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			CommandShell Dispatch = new CommandShell ();

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
			GenerateCommand		Options = new GenerateCommand ();
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


    public class _GenerateCommand : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new Flag (),
			new ExistingFile (),
			new NewFile (),
			new Flag (),
			new Flag (),
			new Flag ()			} ;

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

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "in",
			Brief =  "Generate a command line inteface parser",
			HandleDelegate =  CommandLineInterpreter.Handle_GenerateCommand,
			Lazy =  true,
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
    public class _CommandShell : global::Goedel.Command.DispatchShell {

		public virtual void GenerateCommand ( GenerateCommand Options) {
			string inputfile = null;

			inputfile = Options.CommandParse.Text;

            Goedel.Tool.Command.CommandParse Parse = new Goedel.Tool.Command.CommandParse() {
			    Main = Options.Main.Value,
			    Builtins = Options.Builtins.Value,
			    Catcher = Options.Catcher.Value,
				};
        
			
			using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }


			// Script output of type Generate cs
			if (Options.Generate.Text != null) {
				string outputfile = Options.Generate.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.Command.GenerateCS Script = new Goedel.Tool.Command.GenerateCS () { _Output= OutputWriter };

						Script.Generate (Parse);
						}
					}
				}
			}


        } // class _CommandShell

    public partial class CommandShell : _CommandShell {
        } // class CommandShell

    } // namespace CommandShell


