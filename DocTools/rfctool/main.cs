
//  This file was automatically generated at 2/24/2021 5:14:44 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  commandparse version 3.0.0.575
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2019
//  
//  Build Platform: Win32NT 10.0.18362.0
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Utilities;
#pragma warning disable IDE1006
#pragma warning disable CS1591

namespace MakeRFC {



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

				DefaultCommand = _RFC._DescribeCommand;
				Description = "brief";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"rfc", _RFC._DescribeCommand },
				{"new", _Template._DescribeCommand },
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




		public static void Handle_RFC (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			Shell Dispatch =	DispatchIn as Shell;
			RFC		Options = new RFC ();
			ProcessOptions (Args, Index, Options);
			Dispatch._PreProcess (Options);
			Dispatch.RFC (Options);
			}

		public static void Handle_Template (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			Shell Dispatch =	DispatchIn as Shell;
			Template		Options = new Template ();
			ProcessOptions (Args, Index, Options);
			Dispatch._PreProcess (Options);
			Dispatch.Template (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _RFC : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new Flag (),
			new ExistingFile (),
			new String (),
			new String (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new ExistingFile (),
			new ExistingFile (),
			new ExistingFile (),
			new ExistingFile (),
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
		public virtual ExistingFile InputFile {
			get => _Data[1] as ExistingFile;
			set => _Data[1]  = value;
			}

		public virtual string _InputFile {
			set => _Data[1].Parameter (value);
			}
		/// <summary>Field accessor for option [in]</summary>
		public virtual String InputFormat {
			get => _Data[2] as String;
			set => _Data[2]  = value;
			}

		public virtual string _InputFormat {
			set => _Data[2].Parameter (value);
			}
		/// <summary>Field accessor for option [catalog]</summary>
		public virtual String Catalog {
			get => _Data[3] as String;
			set => _Data[3]  = value;
			}

		public virtual string _Catalog {
			set => _Data[3].Parameter (value);
			}
		/// <summary>Field accessor for option [html]</summary>
		public virtual NewFile HTML {
			get => _Data[4] as NewFile;
			set => _Data[4]  = value;
			}

		public virtual string _HTML {
			set => _Data[4].Parameter (value);
			}
		/// <summary>Field accessor for option [xml]</summary>
		public virtual NewFile XML {
			get => _Data[5] as NewFile;
			set => _Data[5]  = value;
			}

		public virtual string _XML {
			set => _Data[5].Parameter (value);
			}
		/// <summary>Field accessor for option [txt]</summary>
		public virtual NewFile TXT {
			get => _Data[6] as NewFile;
			set => _Data[6]  = value;
			}

		public virtual string _TXT {
			set => _Data[6].Parameter (value);
			}
		/// <summary>Field accessor for option [md]</summary>
		public virtual NewFile MD {
			get => _Data[7] as NewFile;
			set => _Data[7]  = value;
			}

		public virtual string _MD {
			set => _Data[7].Parameter (value);
			}
		/// <summary>Field accessor for option [docx]</summary>
		public virtual NewFile DOC {
			get => _Data[8] as NewFile;
			set => _Data[8]  = value;
			}

		public virtual string _DOC {
			set => _Data[8].Parameter (value);
			}
		/// <summary>Field accessor for option [aml]</summary>
		public virtual NewFile AML {
			get => _Data[9] as NewFile;
			set => _Data[9]  = value;
			}

		public virtual string _AML {
			set => _Data[9].Parameter (value);
			}
		/// <summary>Field accessor for option [w3c]</summary>
		public virtual NewFile W3C {
			get => _Data[10] as NewFile;
			set => _Data[10]  = value;
			}

		public virtual string _W3C {
			set => _Data[10].Parameter (value);
			}
		/// <summary>Field accessor for option [bib]</summary>
		public virtual ExistingFile Bibliography {
			get => _Data[11] as ExistingFile;
			set => _Data[11]  = value;
			}

		public virtual string _Bibliography {
			set => _Data[11].Parameter (value);
			}
		/// <summary>Field accessor for option [cache]</summary>
		public virtual ExistingFile Cache {
			get => _Data[12] as ExistingFile;
			set => _Data[12]  = value;
			}

		public virtual string _Cache {
			set => _Data[12].Parameter (value);
			}
		/// <summary>Field accessor for option [style]</summary>
		public virtual ExistingFile Stylesheet {
			get => _Data[13] as ExistingFile;
			set => _Data[13]  = value;
			}

		public virtual string _Stylesheet {
			set => _Data[13].Parameter (value);
			}
		/// <summary>Field accessor for option [boiler]</summary>
		public virtual ExistingFile Boilerplate {
			get => _Data[14] as ExistingFile;
			set => _Data[14]  = value;
			}

		public virtual string _Boilerplate {
			set => _Data[14].Parameter (value);
			}
		/// <summary>Field accessor for option [auto]</summary>
		public virtual Flag Auto {
			get => _Data[15] as Flag;
			set => _Data[15]  = value;
			}

		public virtual string _Auto {
			set => _Data[15].Parameter (value);
			}
		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "rfc",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_RFC,
			Lazy =  true,
            IsDefault = true,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "InputFile", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "InputFormat", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 2,
					Key = "in"
					},
				new DescribeEntryOption () {
					Identifier = "Catalog", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 3,
					Key = "catalog"
					},
				new DescribeEntryOption () {
					Identifier = "HTML", 
					Default = "html", // null if null
					Brief = "<Unspecified>",
					Index = 4,
					Key = "html"
					},
				new DescribeEntryOption () {
					Identifier = "XML", 
					Default = "xml", // null if null
					Brief = "<Unspecified>",
					Index = 5,
					Key = "xml"
					},
				new DescribeEntryOption () {
					Identifier = "TXT", 
					Default = "txt", // null if null
					Brief = "<Unspecified>",
					Index = 6,
					Key = "txt"
					},
				new DescribeEntryOption () {
					Identifier = "MD", 
					Default = "md", // null if null
					Brief = "<Unspecified>",
					Index = 7,
					Key = "md"
					},
				new DescribeEntryOption () {
					Identifier = "DOC", 
					Default = "docx", // null if null
					Brief = "<Unspecified>",
					Index = 8,
					Key = "docx"
					},
				new DescribeEntryOption () {
					Identifier = "AML", 
					Default = "aml", // null if null
					Brief = "<Unspecified>",
					Index = 9,
					Key = "aml"
					},
				new DescribeEntryOption () {
					Identifier = "W3C", 
					Default = "html", // null if null
					Brief = "<Unspecified>",
					Index = 10,
					Key = "w3c"
					},
				new DescribeEntryOption () {
					Identifier = "Bibliography", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 11,
					Key = "bib"
					},
				new DescribeEntryOption () {
					Identifier = "Cache", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 12,
					Key = "cache"
					},
				new DescribeEntryOption () {
					Identifier = "Stylesheet", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 13,
					Key = "style"
					},
				new DescribeEntryOption () {
					Identifier = "Boilerplate", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 14,
					Key = "boiler"
					},
				new DescribeEntryOption () {
					Identifier = "Auto", 
					Default = "false", // null if null
					Brief = "<Unspecified>",
					Index = 15,
					Key = "auto"
					}
				}
			};

		}

    public partial class RFC : _RFC {
        } // class RFC

    public class _Template : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new String (),
			new NewFile (),
			new NewFile (),
			new NewFile (),
			new NewFile ()			} ;





		/// <summary>Field accessor for parameter []</summary>
		public virtual String Identifier {
			get => _Data[0] as String;
			set => _Data[0]  = value;
			}

		public virtual string _Identifier {
			set => _Data[0].Parameter (value);
			}
		/// <summary>Field accessor for option [html]</summary>
		public virtual NewFile HTML {
			get => _Data[1] as NewFile;
			set => _Data[1]  = value;
			}

		public virtual string _HTML {
			set => _Data[1].Parameter (value);
			}
		/// <summary>Field accessor for option [xml]</summary>
		public virtual NewFile XML {
			get => _Data[2] as NewFile;
			set => _Data[2]  = value;
			}

		public virtual string _XML {
			set => _Data[2].Parameter (value);
			}
		/// <summary>Field accessor for option [md]</summary>
		public virtual NewFile MD {
			get => _Data[3] as NewFile;
			set => _Data[3]  = value;
			}

		public virtual string _MD {
			set => _Data[3].Parameter (value);
			}
		/// <summary>Field accessor for option [docx]</summary>
		public virtual NewFile DOC {
			get => _Data[4] as NewFile;
			set => _Data[4]  = value;
			}

		public virtual string _DOC {
			set => _Data[4].Parameter (value);
			}
		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "new",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_Template,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "Identifier", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 0,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "HTML", 
					Default = "html", // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = "html"
					},
				new DescribeEntryOption () {
					Identifier = "XML", 
					Default = "xml", // null if null
					Brief = "<Unspecified>",
					Index = 2,
					Key = "xml"
					},
				new DescribeEntryOption () {
					Identifier = "MD", 
					Default = "md", // null if null
					Brief = "<Unspecified>",
					Index = 3,
					Key = "md"
					},
				new DescribeEntryOption () {
					Identifier = "DOC", 
					Default = "docx", // null if null
					Brief = "<Unspecified>",
					Index = 4,
					Key = "docx"
					}
				}
			};

		}

    public partial class Template : _Template {
        } // class Template


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
    public class _Shell : global::Goedel.Command.DispatchShell {

		public virtual void RFC ( RFC Options) =>
			CommandLineInterpreter.DescribeValues (Options);

		public virtual void Template ( Template Options) =>
			CommandLineInterpreter.DescribeValues (Options);


        } // class _Shell

    public partial class Shell : _Shell {
        } // class Shell

    } // namespace Shell


