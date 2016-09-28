using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace Goedel.Shell.Makey {
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

			MakeyShell Dispatch = new MakeyShell ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "brief" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "in" : {
							Handle_Project (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_Project (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_Project {
			Lazy,
			InputFile,
			OutputFile,
			}

		private static void Handle_Project (
					MakeyShell Dispatch, string[] args, int index) {
			Project		Options = new Project ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Lazy.Register ("lazy", Registry, (int) TagType_Project.Lazy);
			Options.InputFile.Register ("Input", Registry, (int) TagType_Project.InputFile);
			Options.OutputFile.Register ("Make", Registry, (int) TagType_Project.OutputFile);

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

				TagType_Project TagType = (TagType_Project) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Project.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Project (Options);

			}

		private static void Usage () {

				Console.WriteLine ("brief");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Project		Dummy = new Project ();
#pragma warning restore 219

					Console.Write ("{0}in ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "Input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "Make", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert Visual Studio Project File");

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




    public class _Project : Goedel.Registry.Dispatch {
		public Flag							Lazy = new Flag ("false");
		public ExistingFile			InputFile = new ExistingFile ();
		public NewFile			OutputFile = new NewFile ("makefile");


		}

    public partial class Project : _Project {
        } // class Project



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
    public class _MakeyShell {


		public virtual void Project ( Project Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Project		Dummy = new Project ();
#pragma warning restore 219

					Console.Write ("{0}in ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "Input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "Make", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert Visual Studio Project File");

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"OutputFile", Options.OutputFile);
			Console.WriteLine ("Not Yet Implemented");
			}

        } // class _MakeyShell

    public partial class MakeyShell : _MakeyShell {
        } // class MakeyShell

    } // namespace MakeyShell


