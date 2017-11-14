using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace Command {
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
        public static void Help (DispatchShell Dispatch, string[] args, int index) {
            Brief(Description, DefaultCommand, Entries);
            }

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
        public static void About (DispatchShell Dispatch, string[] args, int index) {
            FileTools.About();
            }

        public static DescribeCommandEntry DescribeAbout = new DescribeCommandEntry() {
            Identifier = "about",
            HandleDelegate = About,
            Entries = new List<DescribeEntry>() { }
            };

        static bool IsFlag(char c) {
            return (c == UnixFlag) | (c == WindowsFlag) ;
            }


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

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"about", DescribeAbout },
				{"in", _Schema._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			Command Dispatch = new Command ();

			MainMethod (Dispatch, Args);
			}


        public void MainMethod(Command Dispatch, string[] Args) {
			Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);
            } // Main



		public static void Handle_Schema (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			Command Dispatch =	DispatchIn as Command;
			Schema		Options = new Schema ();
			ProcessOptions (Args, Index, Options);
			Dispatch.Schema (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Schema : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new ExistingFile (),
			new Flag (),
			new NewFile (),
			new Flag (),
			new Flag (),
			new Flag (),
			new Flag ()			} ;

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

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "in",
			Brief =  "Convert a Goedel schema file to code",
			HandleDelegate =  CommandLineInterpreter.Handle_Schema,
			Lazy =  true,
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
    public class _Command : global::Goedel.Command.DispatchShell {

		public virtual void Schema ( Schema Options) {
			string inputfile = null;

			inputfile = Options.Goedel.Text;

            GoedelSchema.Goedel Parse = new GoedelSchema.Goedel() {
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


			// Script output of type GenerateCS cs
			if (Options.GenerateCS.Text != null) {
				string outputfile = Options.GenerateCS.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						GoedelSchema.GenerateParser Script = new GoedelSchema.GenerateParser (OutputWriter);

						Script.GenerateCS (Parse);
						}
					}
				}
			}


        } // class _Command

    public partial class Command : _Command {
        } // class Command

    } // namespace Command


