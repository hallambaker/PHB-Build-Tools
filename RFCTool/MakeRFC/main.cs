
//This file was automatically generated at 1/3/2016 6:29:56 PM
// 
//Changes to this file may be overwritten without warning
//
//Generator:  CommandParse version 1.0.5845.1140
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace MakeRFC {
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

			Shell Dispatch = new Shell ();


				if (args.Length == 0) {
					throw new ParserException ("No command specified");
					}

                if (IsFlag(args[0][0])) {


                    switch (args[0].Substring(1).ToLower()) {
						case "process documents in html2rfc and xml2rfc format" : {
							Usage ();
							break;
							}
						case "about" : {
							FileTools.About ();
							break;
							}
						case "brief" : {
							Usage ();
							break;
							}
						case "rfc" : {
							Handle_HTML (Dispatch, args, 1);
							break;
							}
						case "new" : {
							Handle_Template (Dispatch, args, 1);
							break;
							}
						default: {
							throw new ParserException("Unknown Command: " + args[0]);
                            }
                        }
                    }
                else {
					Handle_HTML (Dispatch, args, 0);
                    }
            } // Main


		private enum TagType_HTML {
			Lazy,
			InputFile,
			InputFormat,
			Catalog,
			HTML,
			XML,
			TXT,
			MD,
			DOC,
			W3C,
			Bibliography,
			Cache,
			Stylesheet,
			Boilerplate,
			}

		private static void Handle_HTML (
					Shell Dispatch, string[] args, int index) {
			HTML		Options = new HTML ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Lazy.Register ("lazy", Registry, (int) TagType_HTML.Lazy);
			Options.InputFile.Register ("input", Registry, (int) TagType_HTML.InputFile);
			Options.InputFormat.Register ("in", Registry, (int) TagType_HTML.InputFormat);
			Options.Catalog.Register ("catalog", Registry, (int) TagType_HTML.Catalog);
			Options.HTML.Register ("html", Registry, (int) TagType_HTML.HTML);
			Options.XML.Register ("xml", Registry, (int) TagType_HTML.XML);
			Options.TXT.Register ("txt", Registry, (int) TagType_HTML.TXT);
			Options.MD.Register ("md", Registry, (int) TagType_HTML.MD);
			Options.DOC.Register ("docx", Registry, (int) TagType_HTML.DOC);
			Options.W3C.Register ("w3c", Registry, (int) TagType_HTML.W3C);
			Options.Bibliography.Register ("bib", Registry, (int) TagType_HTML.Bibliography);
			Options.Cache.Register ("cache", Registry, (int) TagType_HTML.Cache);
			Options.Stylesheet.Register ("style", Registry, (int) TagType_HTML.Stylesheet);
			Options.Boilerplate.Register ("boiler", Registry, (int) TagType_HTML.Boilerplate);

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

				TagType_HTML TagType = (TagType_HTML) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_HTML.Lazy : {
						int OptionParams = Options.Lazy.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Lazy.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.InputFormat : {
						int OptionParams = Options.InputFormat.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.InputFormat.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.Catalog : {
						int OptionParams = Options.Catalog.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Catalog.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.HTML : {
						int OptionParams = Options.HTML.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.HTML.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.XML : {
						int OptionParams = Options.XML.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.XML.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.TXT : {
						int OptionParams = Options.TXT.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.TXT.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.MD : {
						int OptionParams = Options.MD.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.MD.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.DOC : {
						int OptionParams = Options.DOC.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.DOC.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.W3C : {
						int OptionParams = Options.W3C.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.W3C.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.Bibliography : {
						int OptionParams = Options.Bibliography.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Bibliography.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.Cache : {
						int OptionParams = Options.Cache.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Cache.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.Stylesheet : {
						int OptionParams = Options.Stylesheet.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Stylesheet.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_HTML.Boilerplate : {
						int OptionParams = Options.Boilerplate.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.Boilerplate.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.HTML (Options);

			}
		private enum TagType_Template {
			Identifier,
			HTML,
			XML,
			MD,
			DOC,
			}

		private static void Handle_Template (
					Shell Dispatch, string[] args, int index) {
			Template		Options = new Template ();

			var Registry = new Goedel.Registry.Registry ();

			Options.Identifier.Register ("identifier", Registry, (int) TagType_Template.Identifier);
			Options.HTML.Register ("html", Registry, (int) TagType_Template.HTML);
			Options.XML.Register ("xml", Registry, (int) TagType_Template.XML);
			Options.MD.Register ("md", Registry, (int) TagType_Template.MD);
			Options.DOC.Register ("docx", Registry, (int) TagType_Template.DOC);

			// looking for parameter Param.Name}
			if (index < args.Length && !IsFlag (args [index][0] )) {
				// Have got the parameter, call the parameter value method
				Options.Identifier.Parameter (args [index]);
				index++;
				}

#pragma warning disable 162
			for (int i = index; i< args.Length; i++) {
				if 	(!IsFlag (args [i][0] )) {
					throw new System.Exception ("Unexpected parameter: " + args[i]);}			
				string Rest = args [i].Substring (1);

				TagType_Template TagType = (TagType_Template) Registry.Find (Rest);

				// here have the cases for what to do with it.

				switch (TagType) {
					case TagType_Template.HTML : {
						int OptionParams = Options.HTML.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.HTML.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Template.XML : {
						int OptionParams = Options.XML.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.XML.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Template.MD : {
						int OptionParams = Options.MD.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.MD.Parameter (args[i]);
								}
							}
						break;
						}
					case TagType_Template.DOC : {
						int OptionParams = Options.DOC.Tag (Rest);
						
						if (OptionParams>0 && ((i+1) < args.Length)) {
							if 	(!IsFlag (args [i+1][0] )) {
								i++;								
								Options.DOC.Parameter (args[i]);
								}
							}
						break;
						}
					default : throw new System.Exception ("Internal error");
					}
				}

#pragma warning restore 162
			Dispatch.Template (Options);

			}

		private static void Usage () {

				Console.WriteLine ("Process documents in HTML2RFC and XML2RFC format");
				Console.WriteLine ("");
				Console.WriteLine ("brief");
				Console.WriteLine ("");

				{
#pragma warning disable 219
					HTML		Dummy = new HTML ();
#pragma warning restore 219

					Console.Write ("{0}rfc ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.InputFormat.Usage ("in", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Catalog.Usage ("catalog", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.HTML.Usage ("html", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.XML.Usage ("xml", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.TXT.Usage ("txt", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.MD.Usage ("md", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DOC.Usage ("docx", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.W3C.Usage ("w3c", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Bibliography.Usage ("bib", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Cache.Usage ("cache", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Stylesheet.Usage ("style", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Boilerplate.Usage ("boiler", "value", UsageFlag));
					Console.WriteLine ();

				}

				{
#pragma warning disable 219
					Template		Dummy = new Template ();
#pragma warning restore 219

					Console.Write ("{0}new ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.Identifier.Usage (null, "identifier", UsageFlag));
					Console.Write ("[{0}] ", Dummy.HTML.Usage ("html", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.XML.Usage ("xml", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.MD.Usage ("md", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DOC.Usage ("docx", "value", UsageFlag));
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




    public class _HTML : Goedel.Registry.Dispatch {
		public Flag							Lazy = new Flag ("false");
		public ExistingFile			InputFile = new ExistingFile ();

		public String			InputFormat = new  String ();

		public String			Catalog = new  String ();

		public NewFile			HTML = new  NewFile ("html");

		public NewFile			XML = new  NewFile ("xml");

		public NewFile			TXT = new  NewFile ("txt");

		public NewFile			MD = new  NewFile ("md");

		public NewFile			DOC = new  NewFile ("docx");

		public NewFile			W3C = new  NewFile ("html");

		public ExistingFile			Bibliography = new  ExistingFile ();

		public ExistingFile			Cache = new  ExistingFile ();

		public ExistingFile			Stylesheet = new  ExistingFile ();

		public ExistingFile			Boilerplate = new  ExistingFile ();


		}

    public partial class HTML : _HTML {
        } // class HTML


    public class _Template : Goedel.Registry.Dispatch {
		public String			Identifier = new String ();

		public NewFile			HTML = new  NewFile ("html");

		public NewFile			XML = new  NewFile ("xml");

		public NewFile			MD = new  NewFile ("md");

		public NewFile			DOC = new  NewFile ("docx");


		}

    public partial class Template : _Template {
        } // class Template



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




	// The stub class just contains routines that echo their arguments and
	// write 'not yet implemented'

	// Eventually there will be a compiler option to suppress the debugging
	// to eliminate the redundant code
    public class _Shell {


		public virtual void HTML ( HTML Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					HTML		Dummy = new HTML ();
#pragma warning restore 219

					Console.Write ("{0}rfc ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.InputFile.Usage (null, "input", UsageFlag));
					Console.Write ("[{0}] ", Dummy.InputFormat.Usage ("in", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Catalog.Usage ("catalog", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.HTML.Usage ("html", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.XML.Usage ("xml", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.TXT.Usage ("txt", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.MD.Usage ("md", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DOC.Usage ("docx", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.W3C.Usage ("w3c", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Bibliography.Usage ("bib", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Cache.Usage ("cache", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Stylesheet.Usage ("style", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.Boilerplate.Usage ("boiler", "value", UsageFlag));
					Console.WriteLine ();

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"InputFile", Options.InputFile);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "String", 
							"InputFormat", Options.InputFormat);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "String", 
							"Catalog", Options.Catalog);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"HTML", Options.HTML);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"XML", Options.XML);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"TXT", Options.TXT);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"MD", Options.MD);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"DOC", Options.DOC);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"W3C", Options.W3C);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"Bibliography", Options.Bibliography);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"Cache", Options.Cache);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"Stylesheet", Options.Stylesheet);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "ExistingFile", 
							"Boilerplate", Options.Boilerplate);
			Console.WriteLine ("Not Yet Implemented");
			}
		public virtual void Template ( Template Options
				) {

			char UsageFlag = '-';
				{
#pragma warning disable 219
					Template		Dummy = new Template ();
#pragma warning restore 219

					Console.Write ("{0}new ", UsageFlag);
					Console.Write ("[{0}] ", Dummy.Identifier.Usage (null, "identifier", UsageFlag));
					Console.Write ("[{0}] ", Dummy.HTML.Usage ("html", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.XML.Usage ("xml", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.MD.Usage ("md", "value", UsageFlag));
					Console.Write ("[{0}] ", Dummy.DOC.Usage ("docx", "value", UsageFlag));
					Console.WriteLine ();

				}

				Console.WriteLine ("    {0}\t{1} = [{2}]", "String", 
							"Identifier", Options.Identifier);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"HTML", Options.HTML);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"XML", Options.XML);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"MD", Options.MD);
				Console.WriteLine ("    {0}\t{1} = [{2}]", "NewFile", 
							"DOC", Options.DOC);
			Console.WriteLine ("Not Yet Implemented");
			}

        } // class _Shell

    public partial class Shell : _Shell {
        } // class Shell

    } // namespace Shell


