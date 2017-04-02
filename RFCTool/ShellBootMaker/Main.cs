using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace Shell.Bootmaker {
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

			Shell Dispatch = new Shell ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "process markdown to create a bootstrap html site" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "site" : {
							Handle_Site (Dispatch, args, 1);
							break;
							}
						case "file" : {
							Handle_File (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_Site (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_Site {
			InputDir,
			OutputDir,
			Tag,
			}

		private static void Handle_Site (
					Shell Dispatch, string[] args, int index) {
			Site		Options = new Site ();

			var Registry = new Goedel.Registry.Registry ();

			Options.InputDir.Register ("input", Registry, (int) TagType_Site.InputDir);
			Options.OutputDir.Register ("output", Registry, (int) TagType_Site.OutputDir);
			Options.Tag.Register ("tags", Registry, (int) TagType_Site.Tag);

			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.InputDir.Parameter (args [index]);
				index++;
				}
			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.OutputDir.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Site TagType = (TagType_Site) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Site.Tag : {
						int OptionParams = Options.Tag.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Tag.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Site (Options);

			}
		private enum TagType_File {
			InputFile,
			OutputFile,
			}

		private static void Handle_File (
					Shell Dispatch, string[] args, int index) {
			File		Options = new File ();

			var Registry = new Goedel.Registry.Registry ();

			Options.InputFile.Register ("input", Registry, (int) TagType_File.InputFile);
			Options.OutputFile.Register ("output", Registry, (int) TagType_File.OutputFile);

			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.InputFile.Parameter (args [index]);
				index++;
				}
			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.OutputFile.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_File TagType = (TagType_File) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.File (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Process Markdown to create a Bootstrap HTML site");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Site		Dummy = new Site ();
#pragma warning restore 219

					Console.Write ("{0}site ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputDir.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputDir.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Tag.Usage ("tags", "value", UsageFlag));
					Console.WriteLine ();

				}

				{
#pragma warning disable 219
					File		Dummy = new File ();
#pragma warning restore 219

					Console.Write ("{0}file ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
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




    public class _Site : Goedel.Registry.Dispatch {
		public ExistingFile			InputDir = new ExistingFile ();
		public NewFile			OutputDir = new NewFile ();

		public ExistingFile			Tag = new  ExistingFile ("TagDefinitions.mdsd");


		}

    public partial class Site : _Site {
        } // class Site


    public class _File : Goedel.Registry.Dispatch {
		public ExistingFile			InputFile = new ExistingFile ();
		public NewFile			OutputFile = new NewFile ("html");


		}

    public partial class File : _File {
        } // class File



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


    // Parameter type NewDirectory
    public abstract class _NewDirectory : Goedel.Registry.Type {
        public _NewDirectory() {
            }
        public _NewDirectory(string Value) {
			Default (Value);
            } 

		public string			Value {
			get {return Text;}
			}

        } // _NewDirectory

    public partial class  NewDirectory : _NewDirectory {
        public NewDirectory() {
            } 
        public NewDirectory(string Value) {
			Default (Value);
            } 
        } // NewDirectory


    // Parameter type ExistingDirectory
    public abstract class _ExistingDirectory : Goedel.Registry.Type {
        public _ExistingDirectory() {
            }
        public _ExistingDirectory(string Value) {
			Default (Value);
            } 

		public string			Value {
			get {return Text;}
			}

        } // _ExistingDirectory

    public partial class  ExistingDirectory : _ExistingDirectory {
        public ExistingDirectory() {
            } 
        public ExistingDirectory(string Value) {
			Default (Value);
            } 
        } // ExistingDirectory


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




	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _Shell {


		public virtual void Site ( Site Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Site		Dummy = new Site ();
#pragma warning restore 219

					Console.Write ("{0}site ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputDir.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputDir.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Tag.Usage ("tags", "value", UsageFlag));
					Console.WriteLine ();

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputDir", Options.InputDir);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"OutputDir", Options.OutputDir);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"Tag", Options.Tag);
			Console.WriteLine ("Not Yet Implemented");
			}
		public virtual void File ( File Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					File		Dummy = new File ();
#pragma warning restore 219

					Console.Write ("{0}file ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.WriteLine ();

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"OutputFile", Options.OutputFile);
			Console.WriteLine ("Not Yet Implemented");
			}

        } // class _Shell

    public partial class Shell : _Shell {
        } // class Shell

    } // namespace Shell


