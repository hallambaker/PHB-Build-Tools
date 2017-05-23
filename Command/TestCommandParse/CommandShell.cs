using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace CommandShell {
    public partial class CommandLineInterpreter : CommandLineInterpreterBase {


		static char UnixFlag = '-';
		static char WindowsFlag = '/';

		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dispatch"></param>
        /// <param name="args"></param>
        /// <param name="index"></param>
        public static void Help (DispatchShell Dispatch, string[] args, int index) {
            Brief();
            }

        public static DescribeCommandEntry DescribeHelp = new DescribeCommandEntry() {
            Identifier = "help",
            HandleDelegate = Brief,
            Entries = new List<DescribeEntry>() { }
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dispatch"></param>
        /// <param name="args"></param>
        /// <param name="index"></param>
        public static new void About (DispatchShell Dispatch, string[] args, int index) {
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

		static DescribeCommandSet DescribeCommandSet_Web = new DescribeCommandSet () {
            Identifier = "web",
			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"create", _WebCreate._DescribeCommand },
				{"default", _WebDefault._DescribeCommand },
				{"add", _WebAdd._DescribeCommand },
				{"delete", _WebDelete._DescribeCommand }
				} // End Entries
			};

		static DescribeCommandSet DescribeCommandSet_Mail = new DescribeCommandSet () {
            Identifier = "mail",
			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"create", _MailCreate._DescribeCommand },
				{"default", _MailDefault._DescribeCommand },
				{"add", _MailAdd._DescribeCommand },
				{"delete", _MailDelete._DescribeCommand }
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

				Description = "Command Line Parser Generator";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"about", DescribeAbout },
				{"reset", _Reset._DescribeCommand },
				{"web", DescribeCommandSet_Web},
				{"mail", DescribeCommandSet_Mail},
				{"in", _TestGenerate._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			CommandShell Dispatch = new CommandShell ();

			MainMethod (Dispatch, Args);
			}


        public void MainMethod(CommandShell Dispatch, string[] Args) {
			Dispatcher (Entries, Dispatch, Args, 0);
            } // Main



		public static void Handle_Reset (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			Reset		Options = new Reset ();
			ProcessOptions (Args, Index, Options);
			Dispatch.Reset (Options);
			}

		public static void Handle_WebCreate (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			WebCreate		Options = new WebCreate ();
			ProcessOptions (Args, Index, Options);
			Dispatch.WebCreate (Options);
			}

		public static void Handle_WebDefault (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			WebDefault		Options = new WebDefault ();
			ProcessOptions (Args, Index, Options);
			Dispatch.WebDefault (Options);
			}

		public static void Handle_WebAdd (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			WebAdd		Options = new WebAdd ();
			ProcessOptions (Args, Index, Options);
			Dispatch.WebAdd (Options);
			}

		public static void Handle_WebDelete (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			WebDelete		Options = new WebDelete ();
			ProcessOptions (Args, Index, Options);
			Dispatch.WebDelete (Options);
			}

		public static void Handle_MailCreate (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			MailCreate		Options = new MailCreate ();
			ProcessOptions (Args, Index, Options);
			Dispatch.MailCreate (Options);
			}

		public static void Handle_MailDefault (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			MailDefault		Options = new MailDefault ();
			ProcessOptions (Args, Index, Options);
			Dispatch.MailDefault (Options);
			}

		public static void Handle_MailAdd (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			MailAdd		Options = new MailAdd ();
			ProcessOptions (Args, Index, Options);
			Dispatch.MailAdd (Options);
			}

		public static void Handle_MailDelete (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			MailDelete		Options = new MailDelete ();
			ProcessOptions (Args, Index, Options);
			Dispatch.MailDelete (Options);
			}

		public static void Handle_TestGenerate (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			CommandShell Dispatch =	DispatchIn as CommandShell;
			TestGenerate		Options = new TestGenerate ();
			ProcessOptions (Args, Index, Options);
			Dispatch.TestGenerate (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Reset : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "reset",
			Brief =  "Reset the service",
			HandleDelegate =  CommandLineInterpreter.Handle_Reset,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class Reset : _Reset {
        } // class Reset

    public class _WebCreate : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "create",
			Brief =  "Create Web profile",
			HandleDelegate =  CommandLineInterpreter.Handle_WebCreate,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class WebCreate : _WebCreate {
        } // class WebCreate

    public class _WebDefault : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "default",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_WebDefault,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class WebDefault : _WebDefault {
        } // class WebDefault

    public class _WebAdd : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "add",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_WebAdd,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class WebAdd : _WebAdd {
        } // class WebAdd

    public class _WebDelete : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "delete",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_WebDelete,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class WebDelete : _WebDelete {
        } // class WebDelete

    public class _MailCreate : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "create",
			Brief =  "Create Mail profile",
			HandleDelegate =  CommandLineInterpreter.Handle_MailCreate,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class MailCreate : _MailCreate {
        } // class MailCreate

    public class _MailDefault : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "default",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_MailDefault,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class MailDefault : _MailDefault {
        } // class MailDefault

    public class _MailAdd : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "add",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_MailAdd,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class MailAdd : _MailAdd {
        } // class MailAdd

    public class _MailDelete : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {			} ;

		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "delete",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_MailDelete,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				}
			};

		}

    public partial class MailDelete : _MailDelete {
        } // class MailDelete

    public class _TestGenerate : Goedel.Command.Dispatch  {

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
			HandleDelegate =  CommandLineInterpreter.Handle_TestGenerate,
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
					Default = null, // null if null
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

    public partial class TestGenerate : _TestGenerate {
        } // class TestGenerate


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

		public virtual void Reset ( Reset Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void WebCreate ( WebCreate Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void WebDefault ( WebDefault Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void WebAdd ( WebAdd Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void WebDelete ( WebDelete Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void MailCreate ( MailCreate Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void MailDefault ( MailDefault Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void MailAdd ( MailAdd Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void MailDelete ( MailDelete Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}

		public virtual void TestGenerate ( TestGenerate Options) {
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
				string outputfile = FileTools.DefaultOutput (inputfile, Options.Generate.Text, 
					Options.Generate.Extension);
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.Command.GenerateCS Script = new Goedel.Tool.Command.GenerateCS (OutputWriter);

						Script.Generate (Parse);
						}
					}
				}
			}


        } // class _CommandShell

    public partial class CommandShell : _CommandShell {
        } // class CommandShell

    } // namespace CommandShell


