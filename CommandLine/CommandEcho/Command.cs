//   Copyright © 2015 by Default Deny Security Inc.
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace Command {
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

			Command Dispatch = new Command ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "script" : {
							Handle_Script (Dispatch, args, 1);
							break;
							}
						case "schema" : {
							Handle_Schema (Dispatch, args, 1);
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
					Handle_Script (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_Script {
			InputFile,
			CommentLine,
			Directive,
			CSharp,
			C,
			Java,
			Lazy,
			}

		private static void Handle_Script (
					Command Dispatch, string[] args, int index) {
			Script		Options = new Script ();

			Registry Registry = new Registry ();

			Options.InputFile.Register ("input", Registry, (int) TagType_Script.InputFile);
			Options.CommentLine.Register ("line", Registry, (int) TagType_Script.CommentLine);
			Options.Directive.Register ("link", Registry, (int) TagType_Script.Directive);
			Options.CSharp.Register ("cs", Registry, (int) TagType_Script.CSharp);
			Options.C.Register ("c", Registry, (int) TagType_Script.C);
			Options.Java.Register ("java", Registry, (int) TagType_Script.Java);
			Options.Lazy.Register ("lazy", Registry, (int) TagType_Script.Lazy);

			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.InputFile.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Script TagType = (TagType_Script) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Script.CommentLine : {
						int OptionParams = Options.CommentLine.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.CommentLine.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Script.Directive : {
						int OptionParams = Options.Directive.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Directive.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Script.CSharp : {
						int OptionParams = Options.CSharp.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.CSharp.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Script.C : {
						int OptionParams = Options.C.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.C.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Script.Java : {
						int OptionParams = Options.Java.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Java.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Script.Lazy : {
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
			Dispatch.Script (Options);

			}
		private enum TagType_Schema {
			InputFile,
			DebugLexer,
			DebugParser,
			DebugStack,
			CSharp,
			C,
			Java,
			Lazy,
			}

		private static void Handle_Schema (
					Command Dispatch, string[] args, int index) {
			Schema		Options = new Schema ();

			Registry Registry = new Registry ();

			Options.InputFile.Register ("input", Registry, (int) TagType_Schema.InputFile);
			Options.DebugLexer.Register ("dlexer", Registry, (int) TagType_Schema.DebugLexer);
			Options.DebugParser.Register ("dparser", Registry, (int) TagType_Schema.DebugParser);
			Options.DebugStack.Register ("dstack", Registry, (int) TagType_Schema.DebugStack);
			Options.CSharp.Register ("cs", Registry, (int) TagType_Schema.CSharp);
			Options.C.Register ("c", Registry, (int) TagType_Schema.C);
			Options.Java.Register ("java", Registry, (int) TagType_Schema.Java);
			Options.Lazy.Register ("lazy", Registry, (int) TagType_Schema.Lazy);

			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.InputFile.Parameter (args [index]);
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
					case TagType_Schema.CSharp : {
						int OptionParams = Options.CSharp.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.CSharp.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.C : {
						int OptionParams = Options.C.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.C.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Schema.Java : {
						int OptionParams = Options.Java.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Java.Parameter (args[i]);
								}
							}
						break;
						}
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
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Schema (Options);

			}
		private enum TagType_About {
			}

		private static void Handle_About (
					Command Dispatch, string[] args, int index) {
			About		Options = new About ();

			Registry Registry = new Registry ();



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
					Script		Dummy = new Script ();
#pragma warning restore 219

					Console.Write ("{0}script ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CommentLine.Usage ("line", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Directive.Usage ("link", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CSharp.Usage ("cs", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.C.Usage ("c", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Java.Usage ("java", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert a Goedel script file to code");

				}

				{
#pragma warning disable 219
					Schema		Dummy = new Schema ();
#pragma warning restore 219

					Console.Write ("{0}schema ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugLexer.Usage ("dlexer", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugParser.Usage ("dparser", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugStack.Usage ("dstack", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CSharp.Usage ("cs", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.C.Usage ("c", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Java.Usage ("java", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert a Goedel schema file to code");

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




    public class _Script : Goedel.Registry.Dispatch {
		public ExistingFile			InputFile = new ExistingFile ("script");

		public Flag			CommentLine = new  Flag ();

		public Flag			Directive = new  Flag ();

		public NewFile			CSharp = new  NewFile ("cs");

		public NewFile			C = new  NewFile ("c");

		public NewFile			Java = new  NewFile ("java");

		public Flag			Lazy = new  Flag ();


		}

    public partial class Script : _Script {
        } // class Script


    public class _Schema : Goedel.Registry.Dispatch {
		public ExistingFile			InputFile = new ExistingFile ("gdl");

		public Flag			DebugLexer = new  Flag ();

		public Flag			DebugParser = new  Flag ();

		public Flag			DebugStack = new  Flag ();

		public NewFile			CSharp = new  NewFile ("cs");

		public NewFile			C = new  NewFile ("c");

		public NewFile			Java = new  NewFile ("java");

		public Flag			Lazy = new  Flag ();


		}

    public partial class Schema : _Schema {
        } // class Schema


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


    // Parameter type String
    public abstract class _String : Goedel.Registry.Type {
        public _String() {
            }
        public _String(string Value) {
			Default (Value);
            } 

		public string			Value {
			get {return Text;}
			}

        } // _String

    public partial class  String : _String {
        public String() {
            } 
        public String(string Value) {
			Default (Value);
            } 
        } // String


    // Parameter type Integer
    public abstract class _Integer : Goedel.Registry.Type {
        public _Integer() {
            }
        public _Integer(string Value) {
			Default (Value);
            } 

		public string			Value {
			get {return Text;}
			}

        } // _Integer

    public partial class  Integer : _Integer {
        public Integer() {
            } 
        public Integer(string Value) {
			Default (Value);
            } 
        } // Integer


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

        public override void  Register(string Tag, Registry Registry, int Index) {
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
    public class _Command {


		public virtual void Script ( Script Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Script		Dummy = new Script ();
#pragma warning restore 219

					Console.Write ("{0}script ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CommentLine.Usage ("line", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Directive.Usage ("link", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CSharp.Usage ("cs", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.C.Usage ("c", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Java.Usage ("java", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert a Goedel script file to code");

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"CommentLine", Options.CommentLine);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Directive", Options.Directive);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"CSharp", Options.CSharp);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"C", Options.C);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"Java", Options.Java);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"Lazy", Options.Lazy);
			Console.WriteLine ("Not Yet Implemented");
			}
		public virtual void Schema ( Schema Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Schema		Dummy = new Schema ();
#pragma warning restore 219

					Console.Write ("{0}schema ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugLexer.Usage ("dlexer", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugParser.Usage ("dparser", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DebugStack.Usage ("dstack", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.CSharp.Usage ("cs", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.C.Usage ("c", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Java.Usage ("java", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Lazy.Usage ("lazy", "value", UsageFlag));
					Console.WriteLine ();

					Console.WriteLine ("    Convert a Goedel schema file to code");

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"DebugLexer", Options.DebugLexer);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"DebugParser", Options.DebugParser);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "Flag", 
							"DebugStack", Options.DebugStack);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"CSharp", Options.CSharp);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"C", Options.C);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"Java", Options.Java);
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

        } // class _Command

    public partial class Command : _Command {
        } // class Command

    } // namespace Command


