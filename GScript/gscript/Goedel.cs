using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Utilities;

namespace GoedelShell {
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

				DefaultCommand = _Generate._DescribeCommand;
				Description = "Goedel meta-code generation tool";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"in", _Generate._DescribeCommand },
				{"wrap", _Wrap._DescribeCommand },
				{"about", _About._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			GoedelShell Dispatch = new GoedelShell ();

			MainMethod (Dispatch, Args);
			}


        public void MainMethod(GoedelShell Dispatch, string[] Args) {
			Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);
            } // Main



		public static void Handle_Generate (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			GoedelShell Dispatch =	DispatchIn as GoedelShell;
			Generate		Options = new Generate ();
			ProcessOptions (Args, Index, Options);
			Dispatch.Generate (Options);
			}

		public static void Handle_Wrap (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			GoedelShell Dispatch =	DispatchIn as GoedelShell;
			Wrap		Options = new Wrap ();
			ProcessOptions (Args, Index, Options);
			Dispatch.Wrap (Options);
			}

		public static void Handle_About (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			GoedelShell Dispatch =	DispatchIn as GoedelShell;
			About		Options = new About ();
			ProcessOptions (Args, Index, Options);
			Dispatch.About (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Generate : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new ExistingFile (),
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

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "in",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_Generate,
			Lazy =  false,
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

    public class _Wrap : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new ExistingFile (),
			new NewFile (),
			new String (),
			new String (),
			new String (),
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

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
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


    public partial class  ExistingFile : _ExistingFile {
        public static ExistingFile Factory (string Value) {
            var Result = new ExistingFile();
            Result.Default(Value);
            return Result;
            }
        } // ExistingFile


    public partial class  NewFile : _NewFile {
        public static NewFile Factory (string Value) {
            var Result = new NewFile();
            Result.Default(Value);
            return Result;
            }
        } // NewFile


    public partial class  Flag : _Flag {
        public static Flag Factory (string Value) {
            var Result = new Flag();
            Result.Default(Value);
            return Result;
            }
        } // Flag


    public partial class  String : _String {
        public static String Factory (string Value) {
            var Result = new String();
            Result.Default(Value);
            return Result;
            }
        } // String



	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _GoedelShell : global::Goedel.Command.DispatchShell {

		public virtual void Generate ( Generate Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void Wrap ( Wrap Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void About ( About Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}


        } // class _GoedelShell

    public partial class GoedelShell : _GoedelShell {
        } // class GoedelShell

    } // namespace GoedelShell

