
//This file was automatically generated at 3/20/2015 7:35:56 PM
// 
//Changes to this file may be overwritten without warning
//
//Generator:  CommandParse version 1.0.5517.27811
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

namespace GoedelShell {
    class _Main {

		static char UsageFlag;
		static char UnixFlag = '-';
		static char WindowsFlag = '/';

		//static char Separator;
		//static char UnixSeparator = '=';
		//static char WindowsSeparator = ':';

        static bool IsFlag(char c) {
            return (c == UnixFlag) | (c == WindowsFlag) ;
            }

        static _Main () {
            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

            if (OperatingSystem.Platform == PlatformID.Unix |
                    OperatingSystem.Platform == PlatformID.MacOSX) {
                UsageFlag = UnixFlag;
				//Separator = UnixSeparator;
                }
            else {
                UsageFlag = WindowsFlag;
				//Separator = WindowsSeparator;
                }
            }

        static void Main(string[] args) {

			GoedelShell Dispatch = new GoedelShell ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "generate" : {
							Handle_Generate (Dispatch, args, 1);
							break;
							}
						case "wrap" : {
							Handle_Wrap (Dispatch, args, 1);
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
            } // Main


		private enum TagType_Generate {
			InputFile,
			OutputFile,
			CommentLine,
			Directive,
			Lazy,
			}

		private static void Handle_Generate (
					GoedelShell Dispatch, string[] args, int index) {
			Generate		Options = new Generate ();

			var Registry = new Goedel.Registry.Registry ();

			Options.InputFile.Register ("input", Registry, (int) TagType_Generate.InputFile);
			Options.OutputFile.Register ("output", Registry, (int) TagType_Generate.OutputFile);
			Options.CommentLine.Register ("line", Registry, (int) TagType_Generate.CommentLine);
			Options.Directive.Register ("link", Registry, (int) TagType_Generate.Directive);
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
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Generate TagType = (TagType_Generate) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Generate.CommentLine : {
						int OptionParams = Options.CommentLine.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.CommentLine.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Generate.Directive : {
						int OptionParams = Options.Directive.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Directive.Parameter (args[i]);
								}
							}
						break;
						}
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
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Generate (Options);

			}
		private enum TagType_Wrap {
			InputFile,
			OutputFile,
			Lazy,
			}

		private static void Handle_Wrap (
					GoedelShell Dispatch, string[] args, int index) {
			Wrap		Options = new Wrap ();

			var Registry = new Goedel.Registry.Registry ();

			Options.InputFile.Register ("input", Registry, (int) TagType_Wrap.InputFile);
			Options.OutputFile.Register ("output", Registry, (int) TagType_Wrap.OutputFile);
			Options.Lazy.Register ("lazy", Registry, (int) TagType_Wrap.Lazy);

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

				TagType_Wrap TagType = (TagType_Wrap) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Wrap.Lazy : {
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
			Dispatch.Wrap (Options);

			}
		private enum TagType_About {
			}

		private static void Handle_About (
					GoedelShell Dispatch, string[] args, int index) {
			About		Options = new About ();

			var Registry = new Goedel.Registry.Registry ();



#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_About TagType = (TagType_About) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.About (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Goedel meta-code generation tool");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					Generate		Dummy = new Generate ();
#pragma warning restore 219

					Console.Write ("{0}generate ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CommentLine.Usage ("line", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Directive.Usage ("link", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

				}

				{
#pragma warning disable 219
					Wrap		Dummy = new Wrap ();
#pragma warning restore 219

					Console.Write ("{0}wrap ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

				}

				{
#pragma warning disable 219
					About		Dummy = new About ();
#pragma warning restore 219

					Console.Write ("{0}about ", UsageFlag);
					Console.WriteLine ();

					Console.WriteLine ("    Report tool version and build date");

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
		public ExistingFile			InputFile = new ExistingFile ();
		public NewFile			OutputFile = new NewFile ("cs");

		public Flag			CommentLine = new  Flag ();

		public Flag			Directive = new  Flag ();

		public Flag			Lazy = new  Flag ();


		}

    public partial class Generate : _Generate {
        } // class Generate


    public class _Wrap : Goedel.Registry.Dispatch {
		public ExistingFile			InputFile = new ExistingFile ();
		public NewFile			OutputFile = new NewFile ("cs");

		public Flag			Lazy = new  Flag ();


		}

    public partial class Wrap : _Wrap {
        } // class Wrap


    public class _About : Goedel.Registry.Dispatch {


		}

    public partial class About : _About {
        } // class About



    // Parameter type NewFile
    public abstract class _NewFile : Goedel.Registry.Type {
        public _NewFile() {
            }
        public _NewFile(string Value) {
			Default (Value);
            } 

		// Builtin for NewFile
		public string Extension = "";

		public override void Default(string TextIn) {
			Extension = TextIn;
			}
		public string			Value {
			get {return Text;}
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
    public abstract class _ExistingFile : Goedel.Registry.Type {
        public _ExistingFile() {
            }
        public _ExistingFile(string Value) {
			Default (Value);
            } 

		// Builtin for ExistingFile
		public string Extension = "";

		public override void Default(string TextIn) {
			Extension = TextIn;
			}
		public string			Value {
			get {return Text;}
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
    public abstract class _Flag : Goedel.Registry.Type {
        public _Flag() {
            }
        public _Flag(string Value) {
			Default (Value);
            } 

		// Builtin for flag
	    public bool         IsSet;

		public bool			Value {
			get {return IsSet;}
			}

        public override void  Register(string Tag, Goedel.Registry.Registry Registry, int Index) {
            Registry.Register (Tag, Index);
            Registry.Register ("no" + Tag, Index);
            }

        public override int Tag(string Tag) {
            if ((Tag.Length > 2) && Tag[0] == 'n' && Tag[1] == 'o') {
                IsSet = false;
                }
            else {
                IsSet = true;
                }

            return 0; // number of required parameters is 0
            }

        public override void Parameter(string Text) {
            //Text = (Text == null) ? "true" : Text;
            switch (Text.ToLower()) {
                case "true":
                case "1":
                    IsSet = true;
                    break;
                case "false":
                case "0":
                    IsSet = false;
                    break;
                default :
                    throw new System.Exception ("Flag value not recognized" + Text);
                }
            }
        public override string ToString() {
            return IsSet ? "true" : "false";
            }

		public override string Usage (string Tag, string Value, char Usage) {
			return Usage + "[no]" + Tag;
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
    public class _GoedelShell {


		public virtual void Generate ( Generate Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Generate		Dummy = new Generate ();
#pragma warning restore 219

					Console.Write ("{0}generate ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CommentLine.Usage ("line", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Directive.Usage ("link", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"OutputFile", Options.OutputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"CommentLine", Options.CommentLine);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Directive", Options.Directive);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Lazy", Options.Lazy);
			Console.WriteLine ("Not Yet Implemented");
			}
		public virtual void Wrap ( Wrap Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Wrap		Dummy = new Wrap ();
#pragma warning restore 219

					Console.Write ("{0}wrap ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.OutputFile.Usage (null, "output", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"OutputFile", Options.OutputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Lazy", Options.Lazy);
			Console.WriteLine ("Not Yet Implemented");
			}
		public virtual void About ( About Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					About		Dummy = new About ();
#pragma warning restore 219

					Console.Write ("{0}about ", UsageFlag);
					Console.WriteLine ();

					Console.WriteLine ("    Report tool version and build date");

				}

			Console.WriteLine ("Not Yet Implemented");
			}

        } // class _GoedelShell

    public partial class GoedelShell : _GoedelShell {
        } // class GoedelShell

    } // namespace GoedelShell


