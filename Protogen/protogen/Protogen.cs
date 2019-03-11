using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.Utilities;

namespace ProtoGenShell {



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

				DefaultCommand = _Protocol._DescribeCommand;
				Description = "Protocol compiler";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"protocol", _Protocol._DescribeCommand },
				{"about", DescribeAbout }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			ProtoGenShell Dispatch = new ProtoGenShell ();

			MainMethod (Dispatch, Args);
			}


        public void MainMethod(ProtoGenShell Dispatch, string[] Args) =>
			Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




		public static void Handle_Protocol (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			ProtoGenShell Dispatch =	DispatchIn as ProtoGenShell;
			Protocol		Options = new Protocol ();
			ProcessOptions (Args, Index, Options);
			Dispatch._PreProcess (Options);
			Dispatch.Protocol (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Protocol : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new Flag (),
			new ExistingFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
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
		public virtual ExistingFile ProtoStruct {
			get => _Data[1] as ExistingFile;
			set => _Data[1]  = value;
			}

		public virtual string _ProtoStruct {
			set => _Data[1].Parameter (value);
			}
		/// <summary>Field accessor for option [xml]</summary>
		public virtual NewFile GenerateRFC2XML {
			get => _Data[2] as NewFile;
			set => _Data[2]  = value;
			}

		public virtual string _GenerateRFC2XML {
			set => _Data[2].Parameter (value);
			}
		/// <summary>Field accessor for option [html]</summary>
		public virtual NewFile GenerateHTML {
			get => _Data[3] as NewFile;
			set => _Data[3]  = value;
			}

		public virtual string _GenerateHTML {
			set => _Data[3].Parameter (value);
			}
		/// <summary>Field accessor for option [md]</summary>
		public virtual NewFile GenerateMD {
			get => _Data[4] as NewFile;
			set => _Data[4]  = value;
			}

		public virtual string _GenerateMD {
			set => _Data[4].Parameter (value);
			}
		/// <summary>Field accessor for option [cs]</summary>
		public virtual NewFile GenerateCS {
			get => _Data[5] as NewFile;
			set => _Data[5]  = value;
			}

		public virtual string _GenerateCS {
			set => _Data[5].Parameter (value);
			}
		/// <summary>Field accessor for option [c]</summary>
		public virtual NewFile GenerateC {
			get => _Data[6] as NewFile;
			set => _Data[6]  = value;
			}

		public virtual string _GenerateC {
			set => _Data[6].Parameter (value);
			}
		/// <summary>Field accessor for option [h]</summary>
		public virtual NewFile GenerateH {
			get => _Data[7] as NewFile;
			set => _Data[7]  = value;
			}

		public virtual string _GenerateH {
			set => _Data[7].Parameter (value);
			}
		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "protocol",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_Protocol,
			Lazy =  true,
            IsDefault = true,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "ProtoStruct", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "GenerateRFC2XML", 
					Default = "xml", // null if null
					Brief = "Generate documentation in RFC2XML format",
					Index = 2,
					Key = "xml"
					},
				new DescribeEntryOption () {
					Identifier = "GenerateHTML", 
					Default = "html", // null if null
					Brief = "Generate documentation in HTML format",
					Index = 3,
					Key = "html"
					},
				new DescribeEntryOption () {
					Identifier = "GenerateMD", 
					Default = "md", // null if null
					Brief = "Generate documentation in MarkDown format",
					Index = 4,
					Key = "md"
					},
				new DescribeEntryOption () {
					Identifier = "GenerateCS", 
					Default = "cs", // null if null
					Brief = "Generate C# code",
					Index = 5,
					Key = "cs"
					},
				new DescribeEntryOption () {
					Identifier = "GenerateC", 
					Default = "c", // null if null
					Brief = "Generate C code",
					Index = 6,
					Key = "c"
					},
				new DescribeEntryOption () {
					Identifier = "GenerateH", 
					Default = "h", // null if null
					Brief = "Generate C header",
					Index = 7,
					Key = "h"
					}
				}
			};

		}

    public partial class Protocol : _Protocol {
        } // class Protocol


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
        public Enumeration(DescribeEntryEnumerate description, string value=null) : base(description, value){
            }
        } // _Enumeration<T>

	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _ProtoGenShell : global::Goedel.Command.DispatchShell {

		public virtual void Protocol ( Protocol Options) {
			string inputfile = null;

			inputfile = Options.ProtoStruct.Text;

            Goedel.Tool.ProtoGen.ProtoStruct Parse = new Goedel.Tool.ProtoGen.ProtoStruct() {
				};
        
			
			using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }


			// Script output of type GenerateRFC2XML xml
			if (Options.GenerateRFC2XML.Text != null) {
				string outputfile = Options.GenerateRFC2XML.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate () { _Output= OutputWriter };

						Script.GenerateRFC2XML (Parse);
						}
					}
				}
			// Script output of type GenerateHTML html
			if (Options.GenerateHTML.Text != null) {
				string outputfile = Options.GenerateHTML.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate () { _Output= OutputWriter };

						Script.GenerateHTML (Parse);
						}
					}
				}
			// Script output of type GenerateMD md
			if (Options.GenerateMD.Text != null) {
				string outputfile = Options.GenerateMD.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate () { _Output= OutputWriter };

						Script.GenerateMD (Parse);
						}
					}
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

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate () { _Output= OutputWriter };

						Script.GenerateCS (Parse);
						}
					}
				}
			// Script output of type GenerateC c
			if (Options.GenerateC.Text != null) {
				string outputfile = Options.GenerateC.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate () { _Output= OutputWriter };

						Script.GenerateC (Parse);
						}
					}
				}
			// Script output of type GenerateH h
			if (Options.GenerateH.Text != null) {
				string outputfile = Options.GenerateH.Text; // Automatically defaults
				if (Options.Lazy.Value & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate () { _Output= OutputWriter };

						Script.GenerateH (Parse);
						}
					}
				}
			}


        } // class _ProtoGenShell

    public partial class ProtoGenShell : _ProtoGenShell {
        } // class ProtoGenShell

    } // namespace ProtoGenShell


