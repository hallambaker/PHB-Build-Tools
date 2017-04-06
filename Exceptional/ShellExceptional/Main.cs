using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace ExceptionalShell {
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

			ExceptionalShell Dispatch = new ExceptionalShell ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "manage declaration of exceptions" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "in" : {
							Handle_Exception (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_Exception (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_Exception {
			Lazy,
			Exceptions,
			GenerateCS,
			}

		private static void Handle_Exception (
					ExceptionalShell Dispatch, string[] args, int index) {
			Exception		Options = new Exception ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Lazy.Register ("lazy", Registry, (int) TagType_Exception.Lazy);
			Options.Exceptions.Register ("exceptional", Registry, (int) TagType_Exception.Exceptions);
			Options.GenerateCS.Register ("cs", Registry, (int) TagType_Exception.GenerateCS);

			// looking for parameter Param.Class}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.Exceptions.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Exception TagType = (TagType_Exception) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Exception.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Exception.GenerateCS : {
						int OptionParams = Options.GenerateCS.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateCS.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Exception (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Manage declaration of exceptions");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Exception		Dummy = new Exception ();
#pragma warning restore 219

					Console.Write ("{0}in ", UsageFlag);
					Console.WriteLine ();

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




    public class _Exception : Goedel.Registry.Dispatch {
		public Flag							Lazy = new Flag ("false");
		public ExistingFile					Exceptions = new ExistingFile ("exceptional");
		public NewFile						GenerateCS = new NewFile ("cs");


		}

    public partial class Exception : _Exception {
        } // class Exception



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
    public class _ExceptionalShell {


		public virtual void Exception ( Exception Options
				) {

			string inputfile = null;

			inputfile = Options.Exceptions.Text;

            Goedel.Tool.Exceptional.Exceptions Parse = new Goedel.Tool.Exceptional.Exceptions();


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


