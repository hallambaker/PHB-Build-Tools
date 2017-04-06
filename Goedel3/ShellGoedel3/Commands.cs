using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace Command {
    public partial class CommandLineInterpreter : CommandLineInterpreterBase {

		static char UsageFlag;
		static char UnixFlag = '-';
		static char WindowsFlag = '/';

        static bool IsFlag(char c) {
            return (c == UnixFlag) | (c == WindowsFlag) ;
            }

        static CommandLineInterpreter () {
            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

            if (OperatingSystem.Platform == PlatformID.Unix |
                    OperatingSystem.Platform == PlatformID.MacOSX) {
                UsageFlag = UnixFlag;
                }
            else {
                UsageFlag = WindowsFlag;
                }
            }

        static void Main(string[] args) {
			var CLI = new CommandLineInterpreter ();
			CLI.MainMethod (args);
			}
        public void MainMethod(string[] args) {

			Command Dispatch = new Command ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "goedel meta-code generation tool" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "in" : {
							Handle_Schema (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_Schema (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_Schema {
			Goedel,
			Lazy,
			GenerateCS,
			DebugLexer,
			DebugParser,
			DebugStack,
			Serializer,
			}

		private static void Handle_Schema (
					Command Dispatch, string[] args, int index) {
			Schema		Options = new Schema ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Goedel.Register ("gdl", Registry, (int) TagType_Schema.Goedel);
			Options.Lazy.Register ("lazy", Registry, (int) TagType_Schema.Lazy);
			Options.GenerateCS.Register ("cs", Registry, (int) TagType_Schema.GenerateCS);
			Options.DebugLexer.Register ("dlexer", Registry, (int) TagType_Schema.DebugLexer);
			Options.DebugParser.Register ("dparser", Registry, (int) TagType_Schema.DebugParser);
			Options.DebugStack.Register ("dstack", Registry, (int) TagType_Schema.DebugStack);
			Options.Serializer.Register ("serial", Registry, (int) TagType_Schema.Serializer);

			// looking for parameter Param.Class}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.Goedel.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Schema TagType = (TagType_Schema) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Schema.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.GenerateCS : {
						int OptionParams = Options.GenerateCS.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateCS.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.DebugLexer : {
						int OptionParams = Options.DebugLexer.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.DebugLexer.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.DebugParser : {
						int OptionParams = Options.DebugParser.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.DebugParser.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.DebugStack : {
						int OptionParams = Options.DebugStack.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.DebugStack.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.Serializer : {
						int OptionParams = Options.Serializer.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Serializer.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Schema (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Goedel meta-code generation tool");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Schema		Dummy = new Schema ();
#pragma warning restore 219

					Console.Write ("{0}in ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.DebugLexer.Usage ("dlexer", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugParser.Usage ("dparser", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugStack.Usage ("dstack", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Serializer.Usage ("serial", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert a Goedel schema file to code");

				}

			} // Usage 

		public class ParserException : System.Exception {

			public ParserException(string message)
				: base(message) {

				Console.WriteLine (message);
				}
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch 
	// and Goedel.Registry.Type




    public class _Schema : Goedel.Registry.Dispatch {
		public ExistingFile					Goedel = new ExistingFile ("gdl");
		public Flag							Lazy = new Flag ("false");
		public NewFile						GenerateCS = new NewFile ("cs");

		public Flag			DebugLexer = new  Flag ();

		public Flag			DebugParser = new  Flag ();

		public Flag			DebugStack = new  Flag ();

		public Flag			Serializer = new  Flag ("true");


		}

    public partial class Schema : _Schema {
        } // class Schema



    // Parameter type NewFile
    public abstract class _NewFile : Goedel.Registry._File {
        public _NewFile() {
            }
        public _NewFile(string Value) {
			Default (Value);
            } 



        } // _NewFile

    public partial class  NewFile : _NewFile {
        public NewFile() {
            } 
        public NewFile(string Value) {
			Default (Value);
            } 
        } // NewFile


    // Parameter type ExistingFile
    public abstract class _ExistingFile : Goedel.Registry._File {
        public _ExistingFile() {
            }
        public _ExistingFile(string Value) {
			Default (Value);
            } 



        } // _ExistingFile

    public partial class  ExistingFile : _ExistingFile {
        public ExistingFile() {
            } 
        public ExistingFile(string Value) {
			Default (Value);
            } 
        } // ExistingFile


    // Parameter type Flag
    public abstract class _Flag : Goedel.Registry._Flag {
        public _Flag() {
            }
        public _Flag(string Value) {
			Default (Value);
            } 




        } // _Flag

    public partial class  Flag : _Flag {
        public Flag() {
            } 
        public Flag(string Value) {
			Default (Value);
            } 
        } // Flag




	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _Command {


		public virtual void Schema ( Schema Options
				) {

			string inputfile = null;

			inputfile = Options.Goedel.Text;

            GoedelSchema.Goedel Parse = new GoedelSchema.Goedel();


			Parse.DebugLexer = Options.DebugLexer.Value;
			Parse.DebugParser = Options.DebugParser.Value;
			Parse.DebugStack = Options.DebugStack.Value;
			Parse.Serializer = Options.Serializer.Value;
			Parse.Options = Options;
        
			
			using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }


			// Script output of type GenerateCS cs
			if (Options.GenerateCS.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateCS.Text, 
					Options.GenerateCS.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
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


