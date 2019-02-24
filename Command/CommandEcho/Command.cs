using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
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
        public static void Help (DispatchShell Dispatch, string[] args, int index) =>
            Brief(Description, DefaultCommand, Entries);

        public static DescribeCommandEntry DescribeHelp = new DescribeCommandEntry() {
            Identifier = "help",
            HandleDelegate = Help,
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

				DefaultCommand = _Script._DescribeCommand;
				Description = "Goedel meta-code generation tool";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"script", _Script._DescribeCommand },
				{"schema", _Schema._DescribeCommand },
				{"about", _About._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			Command Dispatch = new Command ();

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
			Script		Options = new Script ();
			ProcessOptions (Args, Index, Options);
			Dispatch._PreProcess (Options);
			Dispatch.Script (Options);
			}

		public static void Handle_Schema (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			Command Dispatch =	DispatchIn as Command;
			Schema		Options = new Schema ();
			ProcessOptions (Args, Index, Options);
			Dispatch._PreProcess (Options);
			Dispatch.Schema (Options);
			}

		public static void Handle_About (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			Command Dispatch =	DispatchIn as Command;
			About		Options = new About ();
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


    public class _Script : Goedel.Command.Dispatch ,
							ILanguages {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new ExistingFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new Flag (),
			new Flag (),
			new Flag ()			} ;

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
		/// <summary>Field accessor for option [line]</summary>
		public virtual Flag CommentLine {
			get => _Data[5] as Flag;
			set => _Data[5]  = value;
			}

		public virtual string _CommentLine {
			set => _Data[5].Parameter (value);
			}
		/// <summary>Field accessor for option [link]</summary>
		public virtual Flag Directive {
			get => _Data[6] as Flag;
			set => _Data[6]  = value;
			}

		public virtual string _Directive {
			set => _Data[6].Parameter (value);
			}
		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "script",
			Brief =  "Convert a Goedel script file to code",
			HandleDelegate =  CommandLineInterpreter.Handle_Script,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "InputFile", 
					Default = "script", // null if null
					Brief = "<Unspecified>",
					Index = 0,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "CSharp", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = "cs"
					},
				new DescribeEntryOption () {
					Identifier = "C", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 2,
					Key = "c"
					},
				new DescribeEntryOption () {
					Identifier = "Java", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 3,
					Key = "java"
					},
				new DescribeEntryOption () {
					Identifier = "Lazy", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 4,
					Key = "lazy"
					},
				new DescribeEntryOption () {
					Identifier = "CommentLine", 
					Default = null, // null if null
					Brief = "If set, include source in generated as comments",
					Index = 5,
					Key = "line"
					},
				new DescribeEntryOption () {
					Identifier = "Directive", 
					Default = null, // null if null
					Brief = "If set, link generated code to source",
					Index = 6,
					Key = "link"
					}
				}
			};

		}

    public partial class Script : _Script {
        } // class Script

    public class _Schema : Goedel.Command.Dispatch ,
							ILanguages {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new ExistingFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new Flag (),
			new Flag (),
			new Flag (),
			new Flag ()			} ;

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

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
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
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = "cs"
					},
				new DescribeEntryOption () {
					Identifier = "C", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 2,
					Key = "c"
					},
				new DescribeEntryOption () {
					Identifier = "Java", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 3,
					Key = "java"
					},
				new DescribeEntryOption () {
					Identifier = "Lazy", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 4,
					Key = "lazy"
					},
				new DescribeEntryOption () {
					Identifier = "DebugLexer", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 5,
					Key = "dlexer"
					},
				new DescribeEntryOption () {
					Identifier = "DebugParser", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 6,
					Key = "dparser"
					},
				new DescribeEntryOption () {
					Identifier = "DebugStack", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 7,
					Key = "dstack"
					}
				}
			};

		}

    public partial class Schema : _Schema {
        } // class Schema
	public interface ILanguages {
		NewFile			CSharp{get; set;}
		NewFile			C{get; set;}
		NewFile			Java{get; set;}
		Flag			Lazy{get; set;}
		}


    public class _About : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
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


    public partial class  String : _String {
        public static String Factory (string Value) {
            var Result = new String();
            Result.Default(Value);
            return Result;
            }
        } // String


    public partial class  Integer : _Integer {
        public static Integer Factory (string Value) {
            var Result = new Integer();
            Result.Default(Value);
            return Result;
            }
        } // Integer


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

		public virtual void Script ( Script Options) =>
			CommandLineInterpreter.DescribeValues (Options);

		public virtual void Schema ( Schema Options) =>
			CommandLineInterpreter.DescribeValues (Options);

		public virtual void About ( About Options) =>
			CommandLineInterpreter.DescribeValues (Options);


        } // class _Command

    public partial class Command : _Command {
        } // class Command

    } // namespace Command


