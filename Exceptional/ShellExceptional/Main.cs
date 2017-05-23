using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace ExceptionalShell {
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


        static CommandLineInterpreter () {
            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

            if (OperatingSystem.Platform == PlatformID.Unix |
                    OperatingSystem.Platform == PlatformID.MacOSX) {
                FlagIndicator = UnixFlag;
                }
            else {
                FlagIndicator = WindowsFlag;
                }

				DefaultCommand = _Exception._DescribeCommand;
				Description = "Manage declaration of exceptions";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"about", DescribeAbout },
				{"in", _Exception._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			ExceptionalShell Dispatch = new ExceptionalShell ();

			MainMethod (Dispatch, Args);
			}


        public void MainMethod(ExceptionalShell Dispatch, string[] Args) {
			Dispatcher (Entries, Dispatch, Args, 0);
            } // Main



		public static void Handle_Exception (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			ExceptionalShell Dispatch =	DispatchIn as ExceptionalShell;
			Exception		Options = new Exception ();
			ProcessOptions (Args, Index, Options);
			Dispatch.Exception (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Exception : Goedel.Command.Dispatch  {

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
		public virtual ExistingFile Exceptions {
			get => _Data[1] as ExistingFile;
			set => _Data[1]  = value;
			}

		public virtual string _Exceptions {
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

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "in",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_Exception,
			Lazy =  true,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "Exceptions", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "GenerateCS", 
					Default = null, // null if null
					Brief = "Generate C# code",
					Index = 2,
					Key = "cs"
					}
				}
			};

		}

    public partial class Exception : _Exception {
        } // class Exception


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
    public class _ExceptionalShell : global::Goedel.Command.DispatchShell {

		public virtual void Exception ( Exception Options) {
			string inputfile = null;

			inputfile = Options.Exceptions.Text;

            Goedel.Tool.Exceptional.Exceptions Parse = new Goedel.Tool.Exceptional.Exceptions() {
				};
        
			
			using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }


			// Script output of type GenerateCS cs
			if (Options.GenerateCS.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateCS.Text, 
					Options.GenerateCS.Extension);
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.Exceptional.Generate Script = new Goedel.Tool.Exceptional.Generate (OutputWriter);

						Script.GenerateCS (Parse);
						}
					}
				}
			}


        } // class _ExceptionalShell

    public partial class ExceptionalShell : _ExceptionalShell {
        } // class ExceptionalShell

    } // namespace ExceptionalShell


