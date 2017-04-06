using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace ProtoGenShell {
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

			ProtoGenShell Dispatch = new ProtoGenShell ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "protocol compiler" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "protocol" : {
							Handle_Protocol (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_Protocol (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_Protocol {
			Lazy,
			ProtoStruct,
			GenerateRFC2XML,
			GenerateHTML,
			GenerateMD,
			GenerateCS,
			GenerateC,
			GenerateH,
			}

		private static void Handle_Protocol (
					ProtoGenShell Dispatch, string[] args, int index) {
			Protocol		Options = new Protocol ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Lazy.Register ("lazy", Registry, (int) TagType_Protocol.Lazy);
			Options.ProtoStruct.Register ("protocol", Registry, (int) TagType_Protocol.ProtoStruct);
			Options.GenerateRFC2XML.Register ("xml", Registry, (int) TagType_Protocol.GenerateRFC2XML);
			Options.GenerateHTML.Register ("html", Registry, (int) TagType_Protocol.GenerateHTML);
			Options.GenerateMD.Register ("md", Registry, (int) TagType_Protocol.GenerateMD);
			Options.GenerateCS.Register ("cs", Registry, (int) TagType_Protocol.GenerateCS);
			Options.GenerateC.Register ("c", Registry, (int) TagType_Protocol.GenerateC);
			Options.GenerateH.Register ("h", Registry, (int) TagType_Protocol.GenerateH);

			// looking for parameter Param.Class}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.ProtoStruct.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Protocol TagType = (TagType_Protocol) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Protocol.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Protocol.GenerateRFC2XML : {
						int OptionParams = Options.GenerateRFC2XML.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateRFC2XML.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Protocol.GenerateHTML : {
						int OptionParams = Options.GenerateHTML.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateHTML.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Protocol.GenerateMD : {
						int OptionParams = Options.GenerateMD.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateMD.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Protocol.GenerateCS : {
						int OptionParams = Options.GenerateCS.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateCS.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Protocol.GenerateC : {
						int OptionParams = Options.GenerateC.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateC.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Protocol.GenerateH : {
						int OptionParams = Options.GenerateH.Tag (Rest);
			
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.GenerateH.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Protocol (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Protocol compiler");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Protocol		Dummy = new Protocol ();
#pragma warning restore 219

					Console.Write ("{0}protocol ", UsageFlag);
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




    public class _Protocol : Goedel.Registry.Dispatch {
		public Flag							Lazy = new Flag ("false");
		public ExistingFile					ProtoStruct = new ExistingFile ("protocol");
		public NewFile						GenerateRFC2XML = new NewFile ("xml");
		public NewFile						GenerateHTML = new NewFile ("html");
		public NewFile						GenerateMD = new NewFile ("md");
		public NewFile						GenerateCS = new NewFile ("cs");
		public NewFile						GenerateC = new NewFile ("c");
		public NewFile						GenerateH = new NewFile ("h");


		}

    public partial class Protocol : _Protocol {
        } // class Protocol



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
    public class _ProtoGenShell {


		public virtual void Protocol ( Protocol Options
				) {

			string inputfile = null;

			inputfile = Options.ProtoStruct.Text;

            Goedel.Tool.ProtoGen.ProtoStruct Parse = new Goedel.Tool.ProtoGen.ProtoStruct();


			Parse.Options = Options;
        
			
			using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }


			// Script output of type GenerateRFC2XML xml
			if (Options.GenerateRFC2XML.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateRFC2XML.Text, 
					Options.GenerateRFC2XML.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate (OutputWriter);

						Script.GenerateRFC2XML (Parse);
						}
					}
				}
			// Script output of type GenerateHTML html
			if (Options.GenerateHTML.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateHTML.Text, 
					Options.GenerateHTML.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate (OutputWriter);

						Script.GenerateHTML (Parse);
						}
					}
				}
			// Script output of type GenerateMD md
			if (Options.GenerateMD.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateMD.Text, 
					Options.GenerateMD.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate (OutputWriter);

						Script.GenerateMD (Parse);
						}
					}
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

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate (OutputWriter);

						Script.GenerateCS (Parse);
						}
					}
				}
			// Script output of type GenerateC c
			if (Options.GenerateC.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateC.Text, 
					Options.GenerateC.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate (OutputWriter);

						Script.GenerateC (Parse);
						}
					}
				}
			// Script output of type GenerateH h
			if (Options.GenerateH.Text != null) {
				string outputfile = FileTools.DefaultOutput (inputfile, Options.GenerateH.Text, 
					Options.GenerateH.Extension);
				if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
					return;
					}
				using (Stream outputStream =
							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

						Goedel.Tool.ProtoGen.Generate Script = new Goedel.Tool.ProtoGen.Generate (OutputWriter);

						Script.GenerateH (Parse);
						}
					}
				}
			}

        } // class _ProtoGenShell

    public partial class ProtoGenShell : _ProtoGenShell {
        } // class ProtoGenShell

    } // namespace ProtoGenShell


