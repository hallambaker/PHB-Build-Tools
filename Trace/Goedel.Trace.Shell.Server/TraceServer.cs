using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Utilities;

namespace Goedel.Trace.Shell.Server {
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

				DefaultCommand = _Start._DescribeCommand;
				Description = "Trace service";

			Entries = new  SortedDictionary<string, DescribeCommand> () {
				{"about", DescribeAbout },
				{"start", _Start._DescribeCommand },
				{"help", DescribeHelp }
				}; // End Entries



            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}

        public void MainMethod(string[] Args) {
			TraceShell Dispatch = new TraceShell ();

			MainMethod (Dispatch, Args);
			}


        public void MainMethod(TraceShell Dispatch, string[] Args) {
			Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);
            } // Main



		public static void Handle_Start (
					DispatchShell  DispatchIn, string[] Args, int Index) {
			TraceShell Dispatch =	DispatchIn as TraceShell;
			Start		Options = new Start ();
			ProcessOptions (Args, Index, Options);
			Dispatch.Start (Options);
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Command.Type


    public class _Start : Goedel.Command.Dispatch  {

		public override Goedel.Command.Type[] _Data {get; set;} = new Goedel.Command.Type [] {
			new String (),
			new String (),
			new Integer (),
			new Flag (),
			new Flag (),
			new Flag (),
			new Flag ()			} ;

		/// <summary>Field accessor for parameter []</summary>
		public virtual String ServiceAddress {
			get => _Data[0] as String;
			set => _Data[0]  = value;
			}

		public virtual string _ServiceAddress {
			set => _Data[0].Parameter (value);
			}
		/// <summary>Field accessor for parameter []</summary>
		public virtual String HostAddress {
			get => _Data[1] as String;
			set => _Data[1]  = value;
			}

		public virtual string _HostAddress {
			set => _Data[1].Parameter (value);
			}
		/// <summary>Field accessor for option [Port]</summary>
		public virtual Integer Port {
			get => _Data[2] as Integer;
			set => _Data[2]  = value;
			}

		public virtual string _Port {
			set => _Data[2].Parameter (value);
			}
		/// <summary>Field accessor for option [verify]</summary>
		public virtual Flag Verify {
			get => _Data[3] as Flag;
			set => _Data[3]  = value;
			}

		public virtual string _Verify {
			set => _Data[3].Parameter (value);
			}
		/// <summary>Field accessor for option [log]</summary>
		public virtual Flag Log {
			get => _Data[4] as Flag;
			set => _Data[4]  = value;
			}

		public virtual string _Log {
			set => _Data[4].Parameter (value);
			}
		/// <summary>Field accessor for option [fallback]</summary>
		public virtual Flag Fallback {
			get => _Data[5] as Flag;
			set => _Data[5]  = value;
			}

		public virtual string _Fallback {
			set => _Data[5].Parameter (value);
			}
		/// <summary>Field accessor for option [multi]</summary>
		public virtual Flag Multithread {
			get => _Data[6] as Flag;
			set => _Data[6]  = value;
			}

		public virtual string _Multithread {
			set => _Data[6].Parameter (value);
			}
		public override DescribeCommandEntry DescribeCommand {get; set;} = _DescribeCommand;

		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {
			Identifier = "start",
			Brief =  "Start the Trace service",
			HandleDelegate =  CommandLineInterpreter.Handle_Start,
			Lazy =  false,
			Entries = new List<DescribeEntry> () {
				new DescribeEntryParameter () {
					Identifier = "ServiceAddress", 
					Default = null, // null if null
					Brief = "Portal DNS address",
					Index = 0,
					Key = ""
					},
				new DescribeEntryParameter () {
					Identifier = "HostAddress", 
					Default = null, // null if null
					Brief = "Host address for Web Service Endpoint",
					Index = 1,
					Key = ""
					},
				new DescribeEntryOption () {
					Identifier = "Port", 
					Default = null, // null if null
					Brief = "Port",
					Index = 2,
					Key = "port"
					},
				new DescribeEntryOption () {
					Identifier = "Verify", 
					Default = null, // null if null
					Brief = "Verify configuration only",
					Index = 3,
					Key = "verify"
					},
				new DescribeEntryOption () {
					Identifier = "Log", 
					Default = null, // null if null
					Brief = "Write log results to file (if specified by sender)",
					Index = 4,
					Key = "log"
					},
				new DescribeEntryOption () {
					Identifier = "Fallback", 
					Default = "true", // null if null
					Brief = "Bind to fallback Web Service Endpoint (default)",
					Index = 5,
					Key = "fallback"
					},
				new DescribeEntryOption () {
					Identifier = "Multithread", 
					Default = "true", // null if null
					Brief = "run as multithreaded service (default)",
					Index = 6,
					Key = "multi"
					}
				}
			};

		}

    public partial class Start : _Start {
        } // class Start


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
    public class _TraceShell : global::Goedel.Command.DispatchShell {

		public virtual void Start ( Start Options) {
			CommandLineInterpreter.DescribeValues (Options);
			}


        } // class _TraceShell

    public partial class TraceShell : _TraceShell {
        } // class TraceShell

    } // namespace TraceShell


