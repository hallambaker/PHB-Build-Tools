
//  This file was automatically generated at 2/23/2021 3:33:27 PM
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

namespace Goedel.Shell.Constant {



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
				Description = "Constant compiler";

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
			ConstantShell Dispatch = new ConstantShell ();

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


        public void MainMethod(ConstantShell Dispatch, string[] Args) =>
			Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);




		public static void Handle_Generate (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			ConstantShell Dispatch =	DispatchIn as ConstantShell;
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
			new ExistingFile (),
			new NewFile (),
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
		public virtual NewFile OutputFile {
			get => _Data[1] as NewFile;
			set => _Data[1]  = value;
			}

		public virtual string _OutputFile {
			set => _Data[1].Parameter (value);
			}
		/// <summary>Field accessor for option [md]</summary>
		public virtual Flag MarkDown {
			get => _Data[2] as Flag;
			set => _Data[2]  = value;
			}

		public virtual string _MarkDown {
			set => _Data[2].Parameter (value);
			}
		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "in",
			Brief =  "<Unspecified>",
			HandleDelegate =  CommandLineInterpreter.Handle_Generate,
			Lazy =  false,
            IsDefault = true,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "InputFile", 
					Default = "cs", // null if null
					Brief = "<Unspecified>",
					Index = 0,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "OutputFile", 
					Default = "cs", // null if null
					Brief = "<Unspecified>",
					Index = 1,
					Key = "cs"
					},
				new DescribeEntryOption () {
					Identifier = "MarkDown", 
					Default = null, // null if null
					Brief = "<Unspecified>",
					Index = 2,
					Key = "md"
					}
				}
			};

		}

    public partial class Generate : _Generate {
        } // class Generate


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
    public class _ConstantShell : global::Goedel.Command.DispatchShell {

		public virtual void Generate ( Generate Options) =>
			CommandLineInterpreter.DescribeValues (Options);


        } // class _ConstantShell

    public partial class ConstantShell : _ConstantShell {
        } // class ConstantShell

    } // namespace ConstantShell


