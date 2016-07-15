
//This file was automatically generated at 1/2/2016 12:38:00 AM
// 
//Changes to this file may be overwritten without warning
//
//Generator:  CommandParse version 1.0.5842.41656
//    Goedel Script Version : 0.1   Generated 
//    Goedel Schema Version : 0.1   Generated
//
//    Copyright : Copyright ©  2012
//
//Build Platform: Win32NT 6.2.9200.0
//
//
//Copyright ©  2012 by Default Deny Security Inc.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.
//
//


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace CommandShell {
    class _Main {

		static char UsageFlag;
		static char UnixFlag = '-';
		static char WindowsFlag = '/';

        static bool IsFlag(char c) {
            return (c == UnixFlag) | (c == WindowsFlag) ;
            }

        static _Main () {
			// For compatability with .NET Core, remove all references to operating
			// system version. Since this is only used for giving help, this does not
			// matter a great deal.

		    UsageFlag = WindowsFlag;

            //System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

            //if (OperatingSystem.Platform == PlatformID.Unix |
            //        OperatingSystem.Platform == PlatformID.MacOSX) {
            //    UsageFlag = UnixFlag;
            //    }
            //else {
            //    UsageFlag = WindowsFlag;
            //    }
            }

        static void Main(string[] args) {

			CommandShell Dispatch = new CommandShell ();


			try {
				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "command line parser generator" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "generate" : {
							Handle_Generate (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_Generate (Dispatch, args, 0);
                    }
				}


            catch (System.Exception Exception) {
                if (Exception.GetType() == typeof (ParserException)) {
                    Usage ();
                    }
                else {
                   Console.WriteLine("Application: {0}", Exception.Message);
                    }
                }
            } // Main


		private enum TagType_Generate {
			Lazy,
			CommandParse,
			Generate,
			Main,
			Builtins,
			Catcher,
			}

		private static void Handle_Generate (
					CommandShell Dispatch, string[] args, int index) {
			Generate		Options = new Generate ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Lazy.Register ("lazy", Registry, (int) TagType_Generate.Lazy);
			Options.CommandParse.Register ("command", Registry, (int) TagType_Generate.CommandParse);
			Options.Generate.Register ("cs", Registry, (int) TagType_Generate.Generate);
			Options.Main.Register ("main", Registry, (int) TagType_Generate.Main);
			Options.Builtins.Register ("builtins", Registry, (int) TagType_Generate.Builtins);
			Options.Catcher.Register ("catch", Registry, (int) TagType_Generate.Catcher);

			// looking for parameter Param.Class}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.CommandParse.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Generate TagType = (TagType_Generate) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Generate.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Generate.Generate : {
						int OptionParams = Options.Generate.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Generate.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Generate.Main : {
						int OptionParams = Options.Main.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Main.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Generate.Builtins : {
						int OptionParams = Options.Builtins.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Builtins.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Generate.Catcher : {
						int OptionParams = Options.Catcher.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Catcher.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Generate (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Command Line Parser Generator");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Generate		Dummy = new Generate ();
#pragma warning restore 219

					Console.Write ("{0}generate ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.Main.Usage ("main", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Builtins.Usage ("builtins", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Catcher.Usage ("catch", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Generate a command line inteface parser");

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




    public class _Generate : Goedel.Registry.Dispatch {
		public Flag							Lazy = new Flag ("false");
		public ExistingFile					CommandParse = new ExistingFile ("command");
		public NewFile						Generate = new NewFile ("cs");

		public Flag			Main = new  Flag ("true");

		public Flag			Builtins = new  Flag ("true");

		public Flag			Catcher = new  Flag ("true");


		}

    public partial class Generate : _Generate {
        } // class Generate



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
    public class _CommandShell {


		public virtual void Generate ( Generate Options
				) {

			string inputfile = null;

			inputfile = Options.CommandParse.Text;

            CommandP.CommandParse Parse = new CommandP.CommandParse();


			Parse.Main = Options.Main.Value;
			Parse.Builtins = Options.Builtins.Value;
			Parse.Catcher = Options.Catcher.Value;
			Parse.Options = Options;
        
			
			using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }


			// Script output of type Generate cs
			if (Options.Generate.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.Generate.Text, 
					Options.Generate.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						CommandP.GenerateCS Script = new CommandP.GenerateCS (OutputWriter);

						Script.Generate (Parse);
						}
					}
				}
			}

        } // class _CommandShell

    public partial class CommandShell : _CommandShell {
        } // class CommandShell

    } // namespace CommandShell


