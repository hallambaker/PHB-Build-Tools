// generate
// Parameter input
// Parameter output
// Option main
// Option builtins
// Option lazy
// end command
// about
// end command

//This file was automatically generated at 8/8/2012 2:16:24 PM
// 
//Changes to this file may be overwritten without warning
//
//Generator:  CommandParse version 1.0.4603.23880
//    Goedel Script Version : 0.1   Generated 
//    Goedel Schema Version : 0.1   Generated
//
//    Copyright : Copyright ©  2011
//
//Build Platform: Win32NT 6.1.7601.65536
//
//
//Copyright ©  2011 by Default Deny Security Inc.
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
using System.Linq;
using System.Text;

namespace CommandShell {
    class _Main {

		static char UsageFlag;
		static char UnixFlag = '-';
		static char WindowsFlag = '/';

		static char Separator;
		static char UnixSeparator = '=';
		static char WindowsSeparator = ':';

        static bool IsFlag(char c) {
            return (c == UnixFlag) | (c == WindowsFlag) ;
            }

        static _Main () {
            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

            if (OperatingSystem.Platform == PlatformID.Unix |
                    OperatingSystem.Platform == PlatformID.MacOSX) {
                UsageFlag = UnixFlag;
				Separator = UnixSeparator;
                }
            else {
                UsageFlag = WindowsFlag;
				Separator = WindowsSeparator;
                }
            }

        static void MainMethod(string[] args) {

			CommandShell Dispatch = new CommandShell ();


			try {
				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "generate" : {
							Handle_Generate (Dispatch, args, 1);
							break;
							}
						case "about" : {
							Handle_About (Dispatch, args, 1);
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
			InputFile,
			OutputFile,
			Main,
			Builtins,
			Lazy,
			}

		private static void Handle_Generate (
					CommandShell Dispatch, string[] args, int index) {
			Generate		Options = new Generate ();

			Registry Registry = new Registry ();

			Options.InputFile.Register ("input", Registry, (int) TagType_Generate.InputFile);
			Options.OutputFile.Register ("output", Registry, (int) TagType_Generate.OutputFile);
			Options.Main.Register ("main", Registry, (int) TagType_Generate.Main);
			Options.Builtins.Register ("builtins", Registry, (int) TagType_Generate.Builtins);
			Options.Lazy.Register ("lazy", Registry, (int) TagType_Generate.Lazy);

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
					throw new Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Generate TagType = (TagType_Generate) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Generate.Main : {
						int OptionParams = Options.Main.Tag (Rest);
						
						// Do we ever want to allow more than one parameter?
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Main.Parameter (args[index]);
								}
							}
						break;
						}
					case TagType_Generate.Builtins : {
						int OptionParams = Options.Builtins.Tag (Rest);
						
						// Do we ever want to allow more than one parameter?
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Builtins.Parameter (args[index]);
								}
							}
						break;
						}
					case TagType_Generate.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						// Do we ever want to allow more than one parameter?
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[index]);
								}
							}
						break;
						}
					default : throw new Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Generate (Options);

			}
		private enum TagType_About {
			}

		private static void Handle_About (
					CommandShell Dispatch, string[] args, int index) {
			About		Options = new About ();

			Registry Registry = new Registry ();



#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_About TagType = (TagType_About) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					default : throw new Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.About (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Command Line Parser Generator");
				Console.WriteLine ("");

				{
					Generate		Dummy = new Generate ();

					Console.Write ("{0}generate ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Main.Usage ("main", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Builtins.Usage ("builtins", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Generate a command line inteface parser");

				}

				{
					About		Dummy = new About ();

					Console.Write ("{0}about ", UsageFlag);
					Console.WriteLine ();

					Console.WriteLine ("    Report tool version and build date");

				}

			} // Usage 

		public class ParserException : Exception {

			public ParserException(string message)
				: base(message) {

				Console.WriteLine (message);
				}
			}


	} // class Main


	// The stub class for carrying optional parameters for each command type
	// As with the main class each consists of an abstract main class 
	// with partial virtual that can be extended as required.

	// All subclasses inherit from the abstract classes Dispatch and Type

    public abstract class Dispatch {
        }

    public abstract class Type {
        public string          TagText;
        public string          Text;

        public Type() {
            }        
        public Type(string Default) {
			Default (Default);
            }

		public virtual void Register(string Tag, Registry Registry, int Index) {
			Registry.Register (Tag, Index);
			}
        public virtual int Tag(string TagIn) {
			TagText = TagIn;
			return 1;
			}
        public virtual void Parameter(string TextIn) {
			Text = TextIn;
			}

        public virtual void Default(string TextIn) {
			Parameter(Text)
			}
		public virtual string Usage (string Tag, string Value, char Usage) {
			if (Tag == null) {
				return Value;
				}
			return Usage + Tag + " " + Value;
			}
        }


    public class RegistryEntry {
        public string              Tag;
        public int                 Index;
        }

    public class Registry {
        List <RegistryEntry>        Entries = new List<RegistryEntry>();
        
        public void Register(string Tag, int Index) {
            RegistryEntry Entry = new RegistryEntry ();
            Entry.Tag = Tag;
            Entry.Index = Index;
            Entries.Add (Entry);
            }

		public int Find (string Match) {
			RegistryEntry Entry = Entries.Find(delegate(RegistryEntry TT) {return TT.Tag == Match; });

			if (Entry == null) {
				throw new Exception ("Unknown option: " + Match);
				}
			return Entry.Index;
			}

        }



    public class _Generate : Dispatch {

		public ExistingFile			InputFile = new ExistingFile ("command");

		public NewFile			OutputFile = new NewFile ("cs");

		public Flag			Main = new  Flag ("true");

		public Flag			Builtins = new  Flag ("true");

		public Flag			Lazy = new  Flag ("false");
		}

    public partial class Generate : _Generate {
        } // class Generate


    public class _About : Dispatch {
		}

    public partial class About : _About {
        } // class About



   // Parameter type NewFile
    public abstract class _NewFile : Type {
			public override string ToString () {
			return Text;
			}
        public _NewFile() {
            }
        public _NewFile(string Default) {
			Parameter (Default);
            } 
        } // _NewFile

    public partial class  NewFile : _NewFile {
        public NewFile() {
            } 
        public NewFile(string Default) {
			Parameter (Default);
            } 
        } // NewFile


   // Parameter type ExistingFile
    public abstract class _ExistingFile : Type {
			public override string ToString () {
			return Text;
			}
        public _ExistingFile() {
            }
        public _ExistingFile(string Default) {
			Parameter (Default);
            } 
        } // _ExistingFile

    public partial class  ExistingFile : _ExistingFile {
        public ExistingFile() {
            } 
        public ExistingFile(string Default) {
			Parameter (Default);
            } 
        } // ExistingFile


   // Parameter type Flag
    public abstract class _Flag : Type {
			public override string ToString () {
			return Text;
			}
        public _Flag() {
            }
        public _Flag(string Default) {
			Parameter (Default);
            } 
        } // _Flag

    public partial class  Flag : _Flag {
        public Flag() {
            } 
        public Flag(string Default) {
			Parameter (Default);
            } 
        } // Flag






	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _CommandShell {

		public virtual void Generate ( Generate Options
				) {
			char UsageFlag = '-';
				{
					Generate		Dummy = new Generate ();

					Console.Write ("{0}generate ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Main.Usage ("main", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Builtins.Usage ("builtins", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Generate a command line inteface parser");

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"OutputFile", Options.OutputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Main", Options.Main);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Builtins", Options.Builtins);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Lazy", Options.Lazy);

			Console.WriteLine ("Not Yet Implemented");
			}
		public virtual void About ( About Options
				) {
			char UsageFlag = '-';
				{
					About		Dummy = new About ();

					Console.Write ("{0}about ", UsageFlag);
					Console.WriteLine ();

					Console.WriteLine ("    Report tool version and build date");

				}


			Console.WriteLine ("Not Yet Implemented");
			}

        } // class _CommandShell

    public partial class CommandShell : _CommandShell {
        } // class CommandShell

    } // namespace CommandShell


