using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace Goedel.VSIXBuildShell {



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

				DefaultCommand = _Generate._DescribeCommand;
				Description = "Build tool for Visual Studio Integration";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"in", _Generate._DescribeCommand },
				{"about", DescribeAbout }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			Shell Dispatch = new Shell ();

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


        public void MainMethod(Shell Dispatch, string[] Args) =>
			Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




		public static void Handle_Generate (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			Shell Dispatch =	DispatchIn as Shell;
			Generate		Options = new Generate ();
			ProcessOptions (Args, Index, Options);
			Dispatch._PreProcess (Options);
			Dispatch.Generate (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Generate : Goedel.Command.Dispatch  {

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
		public virtual ExistingFile VSIXBuild {
			get => _Data[1] as ExistingFile;
			set => _Data[1]  = value;
			}

		public virtual string _VSIXBuild {
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
			HandleDelegate =  CommandLineInterpreter.Handle_Generate,
			Lazy =  true,
            IsDefault = true,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "VSIXBuild", 
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

    public partial class Generate : _Generate {
        } // class Generate


    public partial class  NewFile : _NewFile {
        //public static NewFile Factory (string Value) {
        //    var Result = new NewFile();
        //    Result.Default(Value);
        //    return Result;
        //    }
        } // NewFile


    public partial class  ExistingFile : _ExistingFile {
        //public static ExistingFile Factory (string Value) {
        //    var Result = new ExistingFile();
        //    Result.Default(Value);
        //    return Result;
        //    }
        } // ExistingFile


    public partial class  Flag : _Flag {
        //public static Flag Factory (string Value) {
        //    var Result = new Flag();
        //    Result.Default(Value);
        //    return Result;
        //    }
        } // Flag


    public partial class  Enumeration<T> : _Enumeration<T> {
        public Enumeration(DescribeEntryEnumerate description) : base(description){
            }

        //public static Enumeration<T> Factory (string Value) {
        //    var Result = new Enumeration<T>();
        //    Result.Default(Value);
        //    return Result;
        //    }
        } // _Enumeration<T>

	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _Shell : global::Goedel.Command.DispatchShell {

		public virtual void Generate ( Generate Options) {
			string inputfile = null;

			inputfile = Options.VSIXBuild.Text;

            Goedel.Tool.VSIXBuild.VSIXBuild Parse = new Goedel.Tool.VSIXBuild.VSIXBuild() {
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

						Goedel.Tool.VSIXBuild.Generate Script = new Goedel.Tool.VSIXBuild.Generate () { _Output= OutputWriter };

						Script.GenerateCS (Parse);
						}
					}
				}
			}


        } // class _Shell

    public partial class Shell : _Shell {
        } // class Shell

    } // namespace Shell


