// Script Syntax Version:  1.0

//  Unknown by Unknown
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
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Command {
	public partial class GenerateCS : global::Goedel.Registry.Script {

		  public GenerateCS (TextWriter Output) : base (Output) {}
		//
		// To Do list:
		//
		//	*	Should sense the console width and properly format the usage output
		//	*	Should support brief descriptions of the options and output on usage
		//  *	Write backing code for ExistingFile NewFile Directory etc
		//	*	Support for shell mode
		//	*	Default command
		//	*	Predispatch call
		//  *	Allow debug stubs to be turned off to make code compact
		//
		 TYPE<_Choice> TypeType;
		 CommandParse CommandParse;
		 string NameClassType = "Goedel.Registry.Type";
		 string NameClassFile = "Goedel.Registry._File";
		 string NameClassFlag = "Goedel.Registry._Flag";
		 string NameDispatchType = "Goedel.Registry.Dispatch";
		 Command DefaultCommand = null;
		

		//
		// Normalize
		//
		public void Normalize (List<_Choice> Choices) {
			foreach  (_Choice Item in Choices) {
				switch (Item._Tag ()) {
					case CommandParseType.Class: {
					  Class Class = (Class) Item; 
					foreach  (_Choice Entry in Class.Entries) {
						switch (Entry._Tag ()) {
							case  CommandParseType.Library: {
							
							 CommandParse.Main = false;
							break; }
							case CommandParseType.Command: {
							  Command Command = (Command) Entry; 
							
							 List <_Choice> Extras = new List <_Choice>();
							foreach  (_Choice CommandEntry in Command.Entries) {
								switch (CommandEntry._Tag ()) {
									case CommandParseType.DefaultCommand: { 
									
									 DefaultCommand = Command;
									break; }
									case CommandParseType.Parameter: { 
									break; }
									case CommandParseType.Option: { 
									break; }
									case CommandParseType.Include: {
									  Include Include = (Include) CommandEntry; 
									
									 if (Include.Id.ID == null) {
									
											throw new System.Exception ("Internal parser error"); }
									
									 if (Include.Id.ID.Declared == false) {
									
											throw new System.Exception ("OptionSet not defined: " + Include.Id.ID.Label); } 
									
									 if (Include.Id.ID.Object == null) {
									
											throw new System.Exception ("OptionSet not defined: " + Include.Id.ID.Label); } 
									
									 _Choice Choice = (_Choice) ( Include.Id.ID.Object);
									switch (Choice._Tag ()) {
										case CommandParseType.OptionSet: {
										  OptionSet OptionSet = (OptionSet) Choice; 
										foreach  (_Choice OptionSetEntry in OptionSet.Options) {
											switch (OptionSetEntry._Tag ()) {
												case CommandParseType.Option: {
												  Option Option = (Option) OptionSetEntry; 
												
												 Extras.Add (Option);
											break; }
												}
											}
									break; }
										}
									
									
								break; }
									}
								}
							foreach  (_Choice Option in Extras) {
								 Command.Entries.Add (Option);
								}
						break; }
							}
						}
				break; }
					}
				}
			}
		

		//
		// Generate
		//
		public void Generate (CommandParse CommandParseIn) {
			// Goedel.Registry.Script.Header (_Output, "//", GenerateTime);
			// Goedel.Registry.Script.MITLicense (_Output, "//", 
			//			Goedel.Registry.Script.AssemblyCopyright, Goedel.Registry.Script.AssemblyCompany);
			 GenerateX (CommandParseIn);
			}
		

		//
		// GenerateX
		//
		public void GenerateX (CommandParse CommandParseIn) {
			 CommandParse = CommandParseIn;
			Normalize (CommandParse.Top);
			 TypeType = CommandParse.Registry.FindType ("TypeType");
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using System.IO;\n{0}", _Indent);
			_Output.Write ("using System.Text;\n{0}", _Indent);
			_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Item in CommandParse.Top) {
				switch (Item._Tag ()) {
					case CommandParseType.Class: { 
					
					GenerateClass ((Class)Item);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// GenerateClass
		//
		public void GenerateClass (Class Class) {
			_Output.Write ("namespace {1} {{\n{0}", _Indent, Class.Namespace);
			_Output.Write ("    public partial class CommandLineInterpreter : CommandLineInterpreterBase {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		static char UsageFlag;\n{0}", _Indent);
			_Output.Write ("		static char UnixFlag = '-';\n{0}", _Indent);
			_Output.Write ("		static char WindowsFlag = '/';\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        static bool IsFlag(char c) {{\n{0}", _Indent);
			_Output.Write ("            return (c == UnixFlag) | (c == WindowsFlag) ;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        static CommandLineInterpreter () {{\n{0}", _Indent);
			_Output.Write ("            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            if (OperatingSystem.Platform == PlatformID.Unix |\n{0}", _Indent);
			_Output.Write ("                    OperatingSystem.Platform == PlatformID.MacOSX) {{\n{0}", _Indent);
			_Output.Write ("                UsageFlag = UnixFlag;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            else {{\n{0}", _Indent);
			_Output.Write ("                UsageFlag = WindowsFlag;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  CommandParse.Main ) {
				_Output.Write ("        static void Main(string[] args) {{\n{0}", _Indent);
				_Output.Write ("			var CLI = new CommandLineInterpreter ();\n{0}", _Indent);
				_Output.Write ("			CLI.MainMethod (args);\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				} else {
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("        public void MainMethod(string[] args) {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			{1} Dispatch = new {2} ();\n{0}", _Indent, Class.Id, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  CommandParse.Catcher ) {
				_Output.Write ("			try {{\n{0}", _Indent);
				}
			_Output.Write ("				if (args.Length == 0) {{\n{0}", _Indent);
			_Output.Write ("					throw new ParserException (\"No command specified\");\n{0}", _Indent);
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("                if (IsFlag(args[0][0])) {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("                    switch (args[0].Substring(1).ToLower()) {{\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Cast = (Command) Entry; 
					_Output.Write ("						case \"{1}\" : {{\n{0}", _Indent, Cast.Tag.ToLower());
					_Output.Write ("							Handle_{1} (Dispatch, args, 1);\n{0}", _Indent, Cast.Id);
					_Output.Write ("							break;\n{0}", _Indent);
					_Output.Write ("							}}\n{0}", _Indent);
					break; }
					case CommandParseType.About: {
					  About Cast = (About) Entry; 
					_Output.Write ("						case \"{1}\" : {{\n{0}", _Indent, Cast.Tag.ToLower());
					_Output.Write ("							FileTools.About ();\n{0}", _Indent);
					_Output.Write ("							break;\n{0}", _Indent);
					_Output.Write ("							}}\n{0}", _Indent);
					break; }
					case CommandParseType.Brief: {
					  Brief Cast = (Brief) Entry; 
					_Output.Write ("						case \"{1}\" : {{\n{0}", _Indent, Cast.Text.ToLower());
					_Output.Write ("							Usage ();\n{0}", _Indent);
					_Output.Write ("							break;\n{0}", _Indent);
					_Output.Write ("							}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("						default: {{\n{0}", _Indent);
			_Output.Write ("							throw new ParserException(\"Unknown Command: \" + args[0]);\n{0}", _Indent);
			_Output.Write ("                            }}\n{0}", _Indent);
			_Output.Write ("                        }}\n{0}", _Indent);
			_Output.Write ("                    }}\n{0}", _Indent);
			_Output.Write ("                else {{\n{0}", _Indent);
			if (  DefaultCommand != null ) {
				_Output.Write ("					Handle_{1} (Dispatch, args, 0);\n{0}", _Indent, DefaultCommand.Id);
				} else {
				_Output.Write ("                    throw new ParserException (\"No command specified\");\n{0}", _Indent);
				}
			_Output.Write ("                    }}\n{0}", _Indent);
			if (  CommandParse.Catcher ) {
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("            catch (System.Exception Exception) {{\n{0}", _Indent);
				_Output.Write ("                if (Exception.GetType() == typeof (ParserException)) {{\n{0}", _Indent);
				_Output.Write ("                    Usage ();\n{0}", _Indent);
				_Output.Write ("                    }}\n{0}", _Indent);
				_Output.Write ("                else {{\n{0}", _Indent);
				_Output.Write ("                   Console.WriteLine(\"Application: {{0}}\", Exception.Message);\n{0}", _Indent);
				_Output.Write ("                    }}\n{0}", _Indent);
				_Output.Write ("                }}\n{0}", _Indent);
				}
			_Output.Write ("            }} // Main\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					_Output.Write ("		private enum TagType_{1} {{\n{0}", _Indent, Command.Id);
					foreach  (_Choice CommandEntry in Command.Entries) {
						switch (CommandEntry._Tag ()) {
							case CommandParseType.Parameter: {
							  Parameter Param = (Parameter) CommandEntry; 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
							break; }
							case CommandParseType.Parser: {
							  Parser Param = (Parser) CommandEntry; 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Class);
							break; }
							case CommandParseType.Option: {
							  Option Param = (Option) CommandEntry; 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
							break; }
							case CommandParseType.Lazy: {
							  Lazy Param = (Lazy) CommandEntry; 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
							break; }
							case CommandParseType.Script: {
							  Script Param = (Script) CommandEntry; 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Id);
						break; }
							}
						}
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		private static void Handle_{1} (\n{0}", _Indent, Command.Id);
					_Output.Write ("					{1} Dispatch, string[] args, int index) {{\n{0}", _Indent, Class.Id);
					_Output.Write ("			{1}		Options = new {2} ();\n{0}", _Indent, Command.Id, Command.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			var Registry = new Goedel.Registry.Registry ();\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (_Choice CommandEntry in Command.Entries) {
						switch (CommandEntry._Tag ()) {
							case CommandParseType.Parameter: {
							  Parameter Param = (Parameter) CommandEntry; 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Name, Param.Text, Command.Id, Param.Name);
							break; }
							case CommandParseType.Parser: {
							  Parser Param = (Parser) CommandEntry; 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Class, Param.Extension, Command.Id, Param.Class);
							break; }
							case CommandParseType.Option: {
							  Option Param = (Option) CommandEntry; 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Name, Param.Command, Command.Id, Param.Name);
							break; }
							case CommandParseType.Lazy: {
							  Lazy Param = (Lazy) CommandEntry; 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Name, Param.Tag, Command.Id, Param.Name);
							break; }
							case CommandParseType.Script: {
							  Script Param = (Script) CommandEntry; 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Id, Param.Extension, Command.Id, Param.Id);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					foreach  (_Choice CommandEntry in Command.Entries) {
						switch (CommandEntry._Tag ()) {
							case CommandParseType.Parameter: {
							  Parameter Param = (Parameter) CommandEntry; 
							_Output.Write ("			// looking for parameter Param.Name}}\n{0}", _Indent);
							_Output.Write ("			if (index < args.Length && !IsFlag (args [index][0] )) {{\n{0}", _Indent);
							_Output.Write ("				// Have got the parameter, call the parameter value method\n{0}", _Indent);
							_Output.Write ("				Options.{1}.Parameter (args [index]);\n{0}", _Indent, Param.Name);
							_Output.Write ("				index++;\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
							break; }
							case CommandParseType.Parser: {
							  Parser Param = (Parser) CommandEntry; 
							_Output.Write ("			// looking for parameter Param.Class}}\n{0}", _Indent);
							_Output.Write ("			if (index < args.Length && !IsFlag (args [index][0] )) {{\n{0}", _Indent);
							_Output.Write ("				// Have got the parameter, call the parameter value method\n{0}", _Indent);
							_Output.Write ("				Options.{1}.Parameter (args [index]);\n{0}", _Indent, Param.Class);
							_Output.Write ("				index++;\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("#pragma warning disable 162\n{0}", _Indent);
					_Output.Write ("			for (int i = index; i< args.Length; i++) {{\n{0}", _Indent);
					_Output.Write ("				if 	(!IsFlag (args [i][0] )) {{\n{0}", _Indent);
					_Output.Write ("					throw new System.Exception (\"Unexpected parameter: \" + args[i]);}}			\n{0}", _Indent);
					_Output.Write ("				string Rest = args [i].Substring (1);\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("				TagType_{1} TagType = (TagType_{2}) Registry.Find (Rest);\n{0}", _Indent, Command.Id, Command.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("				// here have the cases for what to do with it.\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("				switch (TagType) {{\n{0}", _Indent);
					foreach  (_Choice CommandEntry in Command.Entries) {
						switch (CommandEntry._Tag ()) {
							case CommandParseType.Option: {
							  Option Option = (Option) CommandEntry; 
							_Output.Write ("					case TagType_{1}.{2} : {{\n{0}", _Indent, Command.Id, Option.Name);
							_Output.Write ("						int OptionParams = Options.{1}.Tag (Rest);\n{0}", _Indent, Option.Name);
							_Output.Write ("						\n{0}", _Indent);
							_Output.Write ("						if (OptionParams>0 && ((i+1) < args.Length)) {{\n{0}", _Indent);
							_Output.Write ("							if 	(!IsFlag (args [i+1][0] )) {{\n{0}", _Indent);
							_Output.Write ("								i++;								\n{0}", _Indent);
							_Output.Write ("								Options.{1}.Parameter (args[i]);\n{0}", _Indent, Option.Name);
							_Output.Write ("								}}\n{0}", _Indent);
							_Output.Write ("							}}\n{0}", _Indent);
							_Output.Write ("						break;\n{0}", _Indent);
							_Output.Write ("						}}\n{0}", _Indent);
							break; }
							case CommandParseType.Lazy: {
							  Lazy Lazy = (Lazy) CommandEntry; 
							_Output.Write ("					case TagType_{1}.{2} : {{\n{0}", _Indent, Command.Id, Lazy.Name);
							_Output.Write ("						int OptionParams = Options.{1}.Tag (Rest);\n{0}", _Indent, Lazy.Name);
							_Output.Write ("						\n{0}", _Indent);
							_Output.Write ("						if (OptionParams>0 && ((i+1) < args.Length)) {{\n{0}", _Indent);
							_Output.Write ("							if 	(!IsFlag (args [i+1][0] )) {{\n{0}", _Indent);
							_Output.Write ("								i++;								\n{0}", _Indent);
							_Output.Write ("								Options.{1}.Parameter (args[i]);\n{0}", _Indent, Lazy.Name);
							_Output.Write ("								}}\n{0}", _Indent);
							_Output.Write ("							}}\n{0}", _Indent);
							_Output.Write ("						break;\n{0}", _Indent);
							_Output.Write ("						}}\n{0}", _Indent);
							break; }
							case CommandParseType.Script: {
							  Script Script = (Script) CommandEntry; 
							_Output.Write ("					case TagType_{1}.{2} : {{\n{0}", _Indent, Command.Id, Script.Id);
							_Output.Write ("						int OptionParams = Options.{1}.Tag (Rest);\n{0}", _Indent, Script.Id);
							_Output.Write ("			\n{0}", _Indent);
							_Output.Write ("						if (OptionParams>0 && ((i+1) < args.Length)) {{\n{0}", _Indent);
							_Output.Write ("							if 	(!IsFlag (args [i+1][0] )) {{\n{0}", _Indent);
							_Output.Write ("								i++;								\n{0}", _Indent);
							_Output.Write ("								Options.{1}.Parameter (args[i]);\n{0}", _Indent, Script.Id);
							_Output.Write ("								}}\n{0}", _Indent);
							_Output.Write ("							}}\n{0}", _Indent);
							_Output.Write ("						break;\n{0}", _Indent);
							_Output.Write ("						}}\n{0}", _Indent);
						break; }
							}
						}
					_Output.Write ("					default : throw new System.Exception (\"Internal error\");\n{0}", _Indent);
					_Output.Write ("					}}\n{0}", _Indent);
					_Output.Write ("				}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("#pragma warning restore 162\n{0}", _Indent);
					_Output.Write ("			Dispatch.{1} (Options);\n{0}", _Indent, Command.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		private static void Usage () {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Brief: {
					  Brief Cast = (Brief) Entry; 
					_Output.Write ("				Console.WriteLine (\"{1}\");\n{0}", _Indent, Cast.Text);
					_Output.Write ("				Console.WriteLine (\"\");\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					
					CommandUsage (Command);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("			}} // Usage \n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		public class ParserException : System.Exception {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			public ParserException(string message)\n{0}", _Indent);
			_Output.Write ("				: base(message) {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("				Console.WriteLine (message);\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	}} // class Main\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// The stub class for carrying optional parameters for each command type\n{0}", _Indent);
			_Output.Write ("	// As with the main class each consists of an abstract main class \n{0}", _Indent);
			_Output.Write ("	// with partial virtual that can be extended as required.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch \n{0}", _Indent);
			_Output.Write ("	// and Goedel.Registry.Type\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Cast = (Command) Entry; 
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("    public class _{1} : {2} {{\n{0}", _Indent, Cast.Id, NameDispatchType);
					foreach  (_Choice OptionC in Cast.Entries) {
						switch (OptionC._Tag ()) {
							case CommandParseType.Parser: {
							  Parser Parser = (Parser) OptionC; 
							_Output.Write ("		public ExistingFile					{1} = new ExistingFile (\"{2}\");\n{0}", _Indent, Parser.Class, Parser.Extension);
							break; }
							case CommandParseType.Generator: { 
							break; }
							case CommandParseType.Script: {
							  Script Script = (Script) OptionC; 
							_Output.Write ("		public NewFile						{1} = new NewFile (\"{2}\");\n{0}", _Indent, Script.Id, Script.Extension);
							break; }
							case CommandParseType.Lazy: {
							  Lazy Lazy = (Lazy) OptionC; 
							_Output.Write ("		public Flag							{1} = new Flag (\"false\");\n{0}", _Indent, Lazy.Name);
							break; }
							case CommandParseType.Parameter: {
							  Parameter Parameter = (Parameter) OptionC; 
							
							 string DefaultParameter = null;
							foreach  (_Choice Modifier in Parameter.Modifier) {
								switch (Modifier._Tag ()) {
									case CommandParseType.Default: {
									  Default Default = (Default) Modifier; 
									
									 DefaultParameter = Default.Text;
								break; }
									}
								}
							if (  DefaultParameter == null ) {
								_Output.Write ("		public {1}			{2} = new {3} ();\n{0}", _Indent, Parameter.Type, Parameter.Name, Parameter.Type);
								} else {
								_Output.Write ("		public {1}			{2} = new {3} (\"{4}\");\n{0}", _Indent, Parameter.Type, Parameter.Name, Parameter.Type, DefaultParameter);
								}
							break; }
							case CommandParseType.Option: {
							  Option Option = (Option) OptionC; 
							_Output.Write ("\n{0}", _Indent);
							
							 string DefaultOption = null;
							foreach  (_Choice Modifier in Option.Modifier) {
								switch (Modifier._Tag ()) {
									case CommandParseType.Default: {
									  Default Default = (Default) Modifier; 
									
									 DefaultOption = Default.Text;
								break; }
									}
								}
							if (  DefaultOption == null ) {
								_Output.Write ("		public {1}			{2} = new  {3} ();\n{0}", _Indent, Option.Type, Option.Name, Option.Type);
								} else {
								_Output.Write ("		public {1}			{2} = new  {3} (\"{4}\");\n{0}", _Indent, Option.Type, Option.Name, Option.Type, DefaultOption);
								}
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("    public partial class {1} : _{2} {{\n{0}", _Indent, Cast.Id, Cast.Id);
					_Output.Write ("        }} // class {1}\n{0}", _Indent, Cast.Id);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (ID<_Choice> ID in TypeType.IDs) {
				_Output.Write ("    // Parameter type {1}\n{0}", _Indent, ID);
				if (  (ID.ToString() == "NewFile") | (ID.ToString() == "ExistingFile") ) {
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassFile);
					} else if (  (ID.ToString() == "Flag")) {
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassFlag);
					} else {
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassType);
					}
				_Output.Write ("        public _{1}() {{\n{0}", _Indent, ID);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("        public _{1}(string Value) {{\n{0}", _Indent, ID);
				_Output.Write ("			Default (Value);\n{0}", _Indent);
				_Output.Write ("            }} \n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				if (  CommandParse.Builtins ) {
					if (  (ID.ToString() == "Flag") ) {
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						} else if (  (ID.ToString() == "NewFile")) {
						_Output.Write ("\n{0}", _Indent);
						} else if (  (ID.ToString() == "ExistingFile")) {
						_Output.Write ("\n{0}", _Indent);
						} else {
						_Output.Write ("		public string			Value {{\n{0}", _Indent);
						_Output.Write ("			get {{return Text;}}\n{0}", _Indent);
						_Output.Write ("			}}\n{0}", _Indent);
						}
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        }} // _{1}\n{0}", _Indent, ID);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("    public partial class  {1} : _{2} {{\n{0}", _Indent, ID, ID);
				_Output.Write ("        public {1}() {{\n{0}", _Indent, ID);
				_Output.Write ("            }} \n{0}", _Indent);
				_Output.Write ("        public {1}(string Value) {{\n{0}", _Indent, ID);
				_Output.Write ("			Default (Value);\n{0}", _Indent);
				_Output.Write ("            }} \n{0}", _Indent);
				_Output.Write ("        }} // {1}\n{0}", _Indent, ID);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// The stub class just contains routines that echo their arguments and\n{0}", _Indent);
			_Output.Write ("	// write 'not yet implemented'\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// Eventually there will be a compiler option to suppress the debugging\n{0}", _Indent);
			_Output.Write ("	// to eliminate the redundant code\n{0}", _Indent);
			_Output.Write ("    public class _{1} {{\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				 bool DefaultOutput = true;
				 string Lazy = null;
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					foreach  (_Choice CommandEntry in Command.Entries) {
						switch (CommandEntry._Tag ()) {
							case CommandParseType.Parser: { 
							
							 DefaultOutput = false;
							break; }
							case CommandParseType.Generator: { 
							
							 DefaultOutput = false;
							break; }
							case CommandParseType.Script: { 
							
							 DefaultOutput = false;
							break; }
							case CommandParseType.Lazy: {
							  Lazy Cast = (Lazy) CommandEntry; 
							
							 Lazy = Cast.Name.ToString ();
						break; }
							}
						}
					_Output.Write ("		public virtual void {1} ( {2} Options\n{0}", _Indent, Command.Id, Command.Id);
					_Output.Write ("				) {{\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					if (  DefaultOutput ) {
						_Output.Write ("			char UsageFlag = '-';\n{0}", _Indent);
						CommandUsage (Command);
						_Output.Write ("\n{0}", _Indent);
						foreach  (_Choice CommandEntry in Command.Entries) {
							switch (CommandEntry._Tag ()) {
								case CommandParseType.Parameter: {
								  Parameter Parameter = (Parameter) CommandEntry; 
								_Output.Write ("				Console.WriteLine (\"    {{0}}\\t{{1}} = [{{2}}]\", \"{1}\", \n{0}", _Indent, Parameter.Type);
								_Output.Write ("							\"{1}\", Options.{2});\n{0}", _Indent, Parameter.Name, Parameter.Name);
								break; }
								case CommandParseType.Option: {
								  Option Option = (Option) CommandEntry; 
								_Output.Write ("				Console.WriteLine (\"    {{0}}\\t{{1}} = [{{2}}]\", \"{1}\", \n{0}", _Indent, Option.Type);
								_Output.Write ("							\"{1}\", Options.{2});\n{0}", _Indent, Option.Name, Option.Name);
							break; }
								}
							}
						_Output.Write ("			Console.WriteLine (\"Not Yet Implemented\");\n{0}", _Indent);
						} else {
						_Output.Write ("			string inputfile = null;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						foreach  (_Choice CommandEntry in Command.Entries) {
							switch (CommandEntry._Tag ()) {
								case CommandParseType.Parser: {
								  Parser Parser = (Parser) CommandEntry; 
								_Output.Write ("			inputfile = Options.{1}.Text;\n{0}", _Indent, Parser.Class);
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("            {1}.{2} Parse = new {3}.{4}();\n{0}", _Indent, Parser.Namespace, Parser.Class, Parser.Namespace, Parser.Class);
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("\n{0}", _Indent);
								foreach  (_Choice CommandEntry2 in Command.Entries) {
									switch (CommandEntry2._Tag ()) {
										case CommandParseType.Option: {
										  Option Option = (Option) CommandEntry2; 
										_Output.Write ("			Parse.{1} = Options.{2}.Value;\n{0}", _Indent, Option.Name, Option.Name);
									break; }
										}
									}
								_Output.Write ("			Parse.Options = Options;\n{0}", _Indent);
								_Output.Write ("        \n{0}", _Indent);
								_Output.Write ("			\n{0}", _Indent);
								_Output.Write ("			using (Stream infile =\n{0}", _Indent);
								_Output.Write ("                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {{\n{0}", _Indent);
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("                Lexer Schema = new Lexer(inputfile);\n{0}", _Indent);
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("                Schema.Process(infile, Parse);\n{0}", _Indent);
								_Output.Write ("                }}\n{0}", _Indent);
								_Output.Write ("\n{0}", _Indent);
								break; }
								case CommandParseType.Generator: { 
							break; }
								}
							}
						_Output.Write ("\n{0}", _Indent);
						foreach  (_Choice CommandEntry in Command.Entries) {
							switch (CommandEntry._Tag ()) {
								case CommandParseType.Script: {
								  Script Script = (Script) CommandEntry; 
								_Output.Write ("			// Script output of type {1} {2}\n{0}", _Indent, Script.Id, Script.Extension);
								_Output.Write ("			if (Options.{1}.Text != null) {{\n{0}", _Indent, Script.Id);
								_Output.Write ("				string outputfile = FileTools.DefaultOutput (inputfile, Options.{1}.Text, \n{0}", _Indent, Script.Id);
								_Output.Write ("					Options.{1}.Extension);\n{0}", _Indent, Script.Id);
								if (  Lazy != null ) {
									_Output.Write ("				if (Options.{1}.IsSet & FileTools.UpToDate (inputfile, outputfile)) {{\n{0}", _Indent, Lazy);
									_Output.Write ("					return;\n{0}", _Indent);
									_Output.Write ("					}}\n{0}", _Indent);
									_Output.Write ("				using (Stream outputStream =\n{0}", _Indent);
									_Output.Write ("							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {{\n{0}", _Indent);
									_Output.Write ("					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {{\n{0}", _Indent);
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("						{1}.{2} Script = new {3}.{4} (OutputWriter);\n{0}", _Indent, Script.Namespace, Script.Class, Script.Namespace, Script.Class);
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("						Script.{1} (Parse);\n{0}", _Indent, Script.Id);
									_Output.Write ("						}}\n{0}", _Indent);
									_Output.Write ("					}}\n{0}", _Indent);
									}
								_Output.Write ("				}}\n{0}", _Indent);
							break; }
								}
							}
						}
					_Output.Write ("			}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        }} // class _{1}\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public partial class {1} : _{2} {{\n{0}", _Indent, Class.Id, Class.Id);
			_Output.Write ("        }} // class {1}\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }} // namespace {1}\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// CommandUsage
		//
		public void CommandUsage (Command Command) {
			_Output.Write ("				{{\n{0}", _Indent);
			_Output.Write ("#pragma warning disable 219\n{0}", _Indent);
			_Output.Write ("					{1}		Dummy = new {2} ();\n{0}", _Indent, Command.Id, Command.Id);
			_Output.Write ("#pragma warning restore 219\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("					Console.Write (\"{{0}}{1} \", UsageFlag);\n{0}", _Indent, Command.Tag);
			foreach  (_Choice CommandEntry in Command.Entries) {
				switch (CommandEntry._Tag ()) {
					case CommandParseType.Parameter: {
					  Parameter Parameter = (Parameter) CommandEntry; 
					_Output.Write ("					Console.Write (\"[{{0}}] \", Dummy.{1}.Usage (null, \"{2}\", UsageFlag));\n{0}", _Indent, Parameter.Name, Parameter.Text);
				break; }
					}
				}
			foreach  (_Choice CommandEntry in Command.Entries) {
				switch (CommandEntry._Tag ()) {
					case CommandParseType.Option: {
					  Option Option = (Option) CommandEntry; 
					_Output.Write ("					Console.Write (\"[{{0}}] \", Dummy.{1}.Usage (\"{2}\", \"value\", UsageFlag));\n{0}", _Indent, Option.Name, Option.Command);
				break; }
					}
				}
			_Output.Write ("					Console.WriteLine ();\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice CommandEntry in Command.Entries) {
				switch (CommandEntry._Tag ()) {
					case CommandParseType.Brief: {
					  Brief CommandBreif = (Brief) CommandEntry; 
					_Output.Write ("					Console.WriteLine (\"    {1}\");\n{0}", _Indent, CommandBreif.Text);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("				}}\n{0}", _Indent);
			}
		}
	}
