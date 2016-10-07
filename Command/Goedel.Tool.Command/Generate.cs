// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

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
// #xclass Goedel.Tool.Command GenerateCS 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Command {
	public partial class GenerateCS : global::Goedel.Registry.Script {

		// #%  public GenerateCS (TextWriter Output) : base (Output) {} 
		  public GenerateCS (TextWriter Output) : base (Output) {}
		// #! 
		//
		// #! To Do list: 
		// To Do list:
		// #! 
		//
		// #!	*	Should sense the console width and properly format the usage output 
		//	*	Should sense the console width and properly format the usage output
		// #!	*	Should support brief descriptions of the options and output on usage 
		//	*	Should support brief descriptions of the options and output on usage
		// #!  *	Write backing code for ExistingFile NewFile Directory etc 
		//  *	Write backing code for ExistingFile NewFile Directory etc
		// #!	*	Support for shell mode 
		//	*	Support for shell mode
		// #!	*	Default command 
		//	*	Default command
		// #!	*	Predispatch call 
		//	*	Predispatch call
		// #!  *	Allow debug stubs to be turned off to make code compact 
		//  *	Allow debug stubs to be turned off to make code compact
		// #! 
		//
		// #% TYPE<_Choice> OptionSetType; 
		 TYPE<_Choice> OptionSetType;
		// #% TYPE<_Choice> TypeType; 
		 TYPE<_Choice> TypeType;
		// #% CommandParse CommandParse; 
		 CommandParse CommandParse;
		//  
		// #% string NameClassType = "Goedel.Registry.Type"; 
		 string NameClassType = "Goedel.Registry.Type";
		// #% string NameClassFile = "Goedel.Registry._File"; 
		 string NameClassFile = "Goedel.Registry._File";
		// #% string NameClassFlag = "Goedel.Registry._Flag"; 
		 string NameClassFlag = "Goedel.Registry._Flag";
		// #% string NameDispatchType = "Goedel.Registry.Dispatch"; 
		 string NameDispatchType = "Goedel.Registry.Dispatch";
		// #% Command DefaultCommand = null; 
		 Command DefaultCommand = null;
		// #method Normalize List<_Choice> Choices 
		

		//
		// Normalize
		//
		public void Normalize (List<_Choice> Choices) {
			// #foreach (_Choice Item in Choices) 
			foreach  (_Choice Item in Choices) {
				// #switchcast CommandParseType Item 
				switch (Item._Tag ()) {
					// #casecast Class Class 
					case CommandParseType.Class: {
					  Class Class = (Class) Item; 
					// #foreach (_Choice Entry in Class.Entries) 
					foreach  (_Choice Entry in Class.Entries) {
						// #switchcast CommandParseType Entry 
						switch (Entry._Tag ()) {
							// #casecast Command Command 
							case CommandParseType.Command: {
							  Command Command = (Command) Entry; 
							// #% List <_Choice> Extras = new List <_Choice>(); 
							
							 List <_Choice> Extras = new List <_Choice>();
							// #foreach (_Choice CommandEntry in Command.Entries) 
							foreach  (_Choice CommandEntry in Command.Entries) {
								// #switchcast CommandParseType CommandEntry 
								switch (CommandEntry._Tag ()) {
									// #casecast DefaultCommand Default 
									case CommandParseType.DefaultCommand: {
									  DefaultCommand Default = (DefaultCommand) CommandEntry; 
									// #% DefaultCommand = Command; 
									
									 DefaultCommand = Command;
									// #casecast Parameter Parameter 
									break; }
									case CommandParseType.Parameter: {
									  Parameter Parameter = (Parameter) CommandEntry; 
									// #casecast Option Option 
									break; }
									case CommandParseType.Option: {
									  Option Option = (Option) CommandEntry; 
									// #casecast Include Include 
									break; }
									case CommandParseType.Include: {
									  Include Include = (Include) CommandEntry; 
									// #% if (Include.Id.ID == null) { 
									
									 if (Include.Id.ID == null) {
									// #%		throw new System.Exception ("Internal parser error"); } 
									
											throw new System.Exception ("Internal parser error"); }
									// #% if (Include.Id.ID.Declared == false) { 
									
									 if (Include.Id.ID.Declared == false) {
									// #%		throw new System.Exception ("OptionSet not defined: " + Include.Id.ID.Label); }  
									
											throw new System.Exception ("OptionSet not defined: " + Include.Id.ID.Label); } 
									// #% if (Include.Id.ID.Object == null) { 
									
									 if (Include.Id.ID.Object == null) {
									// #%		throw new System.Exception ("OptionSet not defined: " + Include.Id.ID.Label); }  
									
											throw new System.Exception ("OptionSet not defined: " + Include.Id.ID.Label); } 
									// #% _Choice Choice = (_Choice) ( Include.Id.ID.Object); 
									
									 _Choice Choice = (_Choice) ( Include.Id.ID.Object);
									// #switchcast CommandParseType Choice 
									switch (Choice._Tag ()) {
										// #casecast OptionSet OptionSet 
										case CommandParseType.OptionSet: {
										  OptionSet OptionSet = (OptionSet) Choice; 
										// #foreach (_Choice OptionSetEntry in OptionSet.Options) 
										foreach  (_Choice OptionSetEntry in OptionSet.Options) {
											// #switchcast CommandParseType OptionSetEntry 
											switch (OptionSetEntry._Tag ()) {
												// #casecast Option Option 
												case CommandParseType.Option: {
												  Option Option = (Option) OptionSetEntry; 
												// #% Extras.Add (Option); 
												
												 Extras.Add (Option);
												// #end switchcast 
											break; }
												}
											// #end foreach 
											}
										// #end switchcast 
									break; }
										}
									// #% 
									
									
									// #end switchcast 
								break; }
									}
								// #end foreach 
								}
							// #foreach (_Choice Option in Extras) 
							foreach  (_Choice Option in Extras) {
								// #% Command.Entries.Add (Option); 
								 Command.Entries.Add (Option);
								// #end foreach 
								}
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// #end switchcast 
				break; }
					}
				// #end foreach      
				}
			// #end method 
			}
		//  
		// #method Generate CommandParse CommandParseIn 
		

		//
		// Generate
		//
		public void Generate (CommandParse CommandParseIn) {
			// #! Goedel.Registry.Script.Header (_Output, "//", GenerateTime); 
			// Goedel.Registry.Script.Header (_Output, "//", GenerateTime);
			// #! Goedel.Registry.Script.MITLicense (_Output, "//",  
			// Goedel.Registry.Script.MITLicense (_Output, "//", 
			// #!			Goedel.Registry.Script.AssemblyCopyright, Goedel.Registry.Script.AssemblyCompany); 
			//			Goedel.Registry.Script.AssemblyCopyright, Goedel.Registry.Script.AssemblyCompany);
			// #% GenerateX (CommandParseIn); 
			 GenerateX (CommandParseIn);
			// #end method 
			}
		//  
		//  
		// #method GenerateX CommandParse CommandParseIn 
		

		//
		// GenerateX
		//
		public void GenerateX (CommandParse CommandParseIn) {
			// #% CommandParse = CommandParseIn; 
			 CommandParse = CommandParseIn;
			// #call Normalize CommandParse.Top 
			Normalize (CommandParse.Top);
			// #% OptionSetType = CommandParse.Registry.FindType ("OptionSetType"); 
			 OptionSetType = CommandParse.Registry.FindType ("OptionSetType");
			// #% TypeType = CommandParse.Registry.FindType ("TypeType"); 
			 TypeType = CommandParse.Registry.FindType ("TypeType");
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using System.IO; 
			_Output.Write ("using System.IO;\n{0}", _Indent);
			// using System.Text; 
			_Output.Write ("using System.Text;\n{0}", _Indent);
			// using Goedel.Registry; 
			_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Item in CommandParse.Top) 
			foreach  (_Choice Item in CommandParse.Top) {
				// #switchcast CommandParseType Item 
				switch (Item._Tag ()) {
					// #casecast Class Cast 
					case CommandParseType.Class: {
					  Class Cast = (Class) Item; 
					// #call GenerateClass (Class)Item 
					
					GenerateClass ((Class)Item);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method GenerateClass Class Class 
		

		//
		// GenerateClass
		//
		public void GenerateClass (Class Class) {
			// namespace #{Class.Namespace} { 
			_Output.Write ("namespace {1} {{\n{0}", _Indent, Class.Namespace);
			//     class _Main { 
			_Output.Write ("    class _Main {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		static char UsageFlag; 
			_Output.Write ("		static char UsageFlag;\n{0}", _Indent);
			// 		static char UnixFlag = '-'; 
			_Output.Write ("		static char UnixFlag = '-';\n{0}", _Indent);
			// 		static char WindowsFlag = '/'; 
			_Output.Write ("		static char WindowsFlag = '/';\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         static bool IsFlag(char c) { 
			_Output.Write ("        static bool IsFlag(char c) {{\n{0}", _Indent);
			//             return (c == UnixFlag) | (c == WindowsFlag) ; 
			_Output.Write ("            return (c == UnixFlag) | (c == WindowsFlag) ;\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         static _Main () { 
			_Output.Write ("        static _Main () {{\n{0}", _Indent);
			// 			// For compatability with .NET Core, remove all references to operating 
			_Output.Write ("			// For compatability with .NET Core, remove all references to operating\n{0}", _Indent);
			// 			// system version. Since this is only used for giving help, this does not 
			_Output.Write ("			// system version. Since this is only used for giving help, this does not\n{0}", _Indent);
			// 			// matter a great deal. 
			_Output.Write ("			// matter a great deal.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		    UsageFlag = WindowsFlag; 
			_Output.Write ("		    UsageFlag = WindowsFlag;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             //System.OperatingSystem OperatingSystem = System.Environment.OSVersion; 
			_Output.Write ("            //System.OperatingSystem OperatingSystem = System.Environment.OSVersion;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             //if (OperatingSystem.Platform == PlatformID.Unix | 
			_Output.Write ("            //if (OperatingSystem.Platform == PlatformID.Unix |\n{0}", _Indent);
			//             //        OperatingSystem.Platform == PlatformID.MacOSX) { 
			_Output.Write ("            //        OperatingSystem.Platform == PlatformID.MacOSX) {{\n{0}", _Indent);
			//             //    UsageFlag = UnixFlag; 
			_Output.Write ("            //    UsageFlag = UnixFlag;\n{0}", _Indent);
			//             //    } 
			_Output.Write ("            //    }}\n{0}", _Indent);
			//             //else { 
			_Output.Write ("            //else {{\n{0}", _Indent);
			//             //    UsageFlag = WindowsFlag; 
			_Output.Write ("            //    UsageFlag = WindowsFlag;\n{0}", _Indent);
			//             //    } 
			_Output.Write ("            //    }}\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if CommandParse.Main 
			if (  CommandParse.Main ) {
				//         static void Main(string[] args) { 
				_Output.Write ("        static void Main(string[] args) {{\n{0}", _Indent);
				// #else 
				} else {
				//         static void MainMethod(string[] args) { 
				_Output.Write ("        static void MainMethod(string[] args) {{\n{0}", _Indent);
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			#{Class.Id} Dispatch = new #{Class.Id} (); 
			_Output.Write ("			{1} Dispatch = new {2} ();\n{0}", _Indent, Class.Id, Class.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if CommandParse.Catcher 
			if (  CommandParse.Catcher ) {
				// 			try { 
				_Output.Write ("			try {{\n{0}", _Indent);
				// #end if 
				}
			// 				if (args.Length == 0) { 
			_Output.Write ("				if (args.Length == 0) {{\n{0}", _Indent);
			// 					throw new ParserException ("No command specified"); 
			_Output.Write ("					throw new ParserException (\"No command specified\");\n{0}", _Indent);
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//                 if (IsFlag(args[0][0])) { 
			_Output.Write ("                if (IsFlag(args[0][0])) {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//                     switch (args[0].Substring(1).ToLower()) { 
			_Output.Write ("                    switch (args[0].Substring(1).ToLower()) {{\n{0}", _Indent);
			// #foreach (_Choice Entry in Class.Entries) 
			foreach  (_Choice Entry in Class.Entries) {
				// #switchcast CommandParseType Entry 
				switch (Entry._Tag ()) {
					// #casecast Command Cast 
					case CommandParseType.Command: {
					  Command Cast = (Command) Entry; 
					// 						case "#{Cast.Tag.ToLower()}" : { 
					_Output.Write ("						case \"{1}\" : {{\n{0}", _Indent, Cast.Tag.ToLower());
					// 							Handle_#{Cast.Id} (Dispatch, args, 1); 
					_Output.Write ("							Handle_{1} (Dispatch, args, 1);\n{0}", _Indent, Cast.Id);
					// 							break; 
					_Output.Write ("							break;\n{0}", _Indent);
					// 							} 
					_Output.Write ("							}}\n{0}", _Indent);
					// #casecast About Cast 
					break; }
					case CommandParseType.About: {
					  About Cast = (About) Entry; 
					// 						case "#{Cast.Tag.ToLower()}" : { 
					_Output.Write ("						case \"{1}\" : {{\n{0}", _Indent, Cast.Tag.ToLower());
					// 							FileTools.About (); 
					_Output.Write ("							FileTools.About ();\n{0}", _Indent);
					// 							break; 
					_Output.Write ("							break;\n{0}", _Indent);
					// 							} 
					_Output.Write ("							}}\n{0}", _Indent);
					// #casecast Brief Cast 
					break; }
					case CommandParseType.Brief: {
					  Brief Cast = (Brief) Entry; 
					// 						case "#{Cast.Text.ToLower()}" : { 
					_Output.Write ("						case \"{1}\" : {{\n{0}", _Indent, Cast.Text.ToLower());
					// 							Usage (); 
					_Output.Write ("							Usage ();\n{0}", _Indent);
					// 							break; 
					_Output.Write ("							break;\n{0}", _Indent);
					// 							} 
					_Output.Write ("							}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 						default: { 
			_Output.Write ("						default: {{\n{0}", _Indent);
			// 							throw new ParserException("Unknown Command: " + args[0]); 
			_Output.Write ("							throw new ParserException(\"Unknown Command: \" + args[0]);\n{0}", _Indent);
			//                             } 
			_Output.Write ("                            }}\n{0}", _Indent);
			//                         } 
			_Output.Write ("                        }}\n{0}", _Indent);
			//                     } 
			_Output.Write ("                    }}\n{0}", _Indent);
			//                 else { 
			_Output.Write ("                else {{\n{0}", _Indent);
			// #if DefaultCommand != null 
			if (  DefaultCommand != null ) {
				// 					Handle_#{DefaultCommand.Id} (Dispatch, args, 0); 
				_Output.Write ("					Handle_{1} (Dispatch, args, 0);\n{0}", _Indent, DefaultCommand.Id);
				// #else 
				} else {
				//                     throw new ParserException ("No command specified"); 
				_Output.Write ("                    throw new ParserException (\"No command specified\");\n{0}", _Indent);
				// #end if 
				}
			//                     } 
			_Output.Write ("                    }}\n{0}", _Indent);
			// #if CommandParse.Catcher 
			if (  CommandParse.Catcher ) {
				// 				} 
				_Output.Write ("				}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//             catch (System.Exception Exception) { 
				_Output.Write ("            catch (System.Exception Exception) {{\n{0}", _Indent);
				//                 if (Exception.GetType() == typeof (ParserException)) { 
				_Output.Write ("                if (Exception.GetType() == typeof (ParserException)) {{\n{0}", _Indent);
				//                     Usage (); 
				_Output.Write ("                    Usage ();\n{0}", _Indent);
				//                     } 
				_Output.Write ("                    }}\n{0}", _Indent);
				//                 else { 
				_Output.Write ("                else {{\n{0}", _Indent);
				//                    Console.WriteLine("Application: {0}", Exception.Message); 
				_Output.Write ("                   Console.WriteLine(\"Application: {{0}}\", Exception.Message);\n{0}", _Indent);
				//                     } 
				_Output.Write ("                    }}\n{0}", _Indent);
				//                 } 
				_Output.Write ("                }}\n{0}", _Indent);
				// #end if 
				}
			//             } // Main 
			_Output.Write ("            }} // Main\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Entry in Class.Entries) 
			foreach  (_Choice Entry in Class.Entries) {
				// #switchcast CommandParseType Entry 
				switch (Entry._Tag ()) {
					// #casecast Command Command 
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					// 		private enum TagType_#{Command.Id} { 
					_Output.Write ("		private enum TagType_{1} {{\n{0}", _Indent, Command.Id);
					// #foreach (_Choice CommandEntry in Command.Entries) 
					foreach  (_Choice CommandEntry in Command.Entries) {
						// #switchcast CommandParseType CommandEntry 
						switch (CommandEntry._Tag ()) {
							// #casecast Parameter Param 
							case CommandParseType.Parameter: {
							  Parameter Param = (Parameter) CommandEntry; 
							// 			#{Param.Name}, 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
							// #casecast Parser Param 
							break; }
							case CommandParseType.Parser: {
							  Parser Param = (Parser) CommandEntry; 
							// 			#{Param.Class}, 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Class);
							// #casecast Option Param 
							break; }
							case CommandParseType.Option: {
							  Option Param = (Option) CommandEntry; 
							// 			#{Param.Name}, 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
							// #casecast Lazy Param 
							break; }
							case CommandParseType.Lazy: {
							  Lazy Param = (Lazy) CommandEntry; 
							// 			#{Param.Name}, 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
							// #casecast Script Param 
							break; }
							case CommandParseType.Script: {
							  Script Param = (Script) CommandEntry; 
							// 			#{Param.Id}, 
							_Output.Write ("			{1},\n{0}", _Indent, Param.Id);
							// #end switchcast 
						break; }
							}
						// #end foreach			 
						}
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		private static void Handle_#{Command.Id} ( 
					_Output.Write ("		private static void Handle_{1} (\n{0}", _Indent, Command.Id);
					// 					#{Class.Id} Dispatch, string[] args, int index) { 
					_Output.Write ("					{1} Dispatch, string[] args, int index) {{\n{0}", _Indent, Class.Id);
					// 			#{Command.Id}		Options = new #{Command.Id} (); 
					_Output.Write ("			{1}		Options = new {2} ();\n{0}", _Indent, Command.Id, Command.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			var Registry = new Goedel.Registry.Registry (); 
					_Output.Write ("			var Registry = new Goedel.Registry.Registry ();\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (_Choice CommandEntry in Command.Entries) 
					foreach  (_Choice CommandEntry in Command.Entries) {
						// #switchcast CommandParseType CommandEntry 
						switch (CommandEntry._Tag ()) {
							// #casecast Parameter Param 
							case CommandParseType.Parameter: {
							  Parameter Param = (Parameter) CommandEntry; 
							// 			Options.#{Param.Name}.Register ("#{Param.Text}", Registry, (int) TagType_#{Command.Id}.#{Param.Name}); 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Name, Param.Text, Command.Id, Param.Name);
							// #casecast Parser Param 
							break; }
							case CommandParseType.Parser: {
							  Parser Param = (Parser) CommandEntry; 
							// 			Options.#{Param.Class}.Register ("#{Param.Extension}", Registry, (int) TagType_#{Command.Id}.#{Param.Class}); 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Class, Param.Extension, Command.Id, Param.Class);
							// #casecast Option Param 
							break; }
							case CommandParseType.Option: {
							  Option Param = (Option) CommandEntry; 
							// 			Options.#{Param.Name}.Register ("#{Param.Command}", Registry, (int) TagType_#{Command.Id}.#{Param.Name}); 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Name, Param.Command, Command.Id, Param.Name);
							// #casecast Lazy Param 
							break; }
							case CommandParseType.Lazy: {
							  Lazy Param = (Lazy) CommandEntry; 
							// 			Options.#{Param.Name}.Register ("#{Param.Tag}", Registry, (int) TagType_#{Command.Id}.#{Param.Name}); 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Name, Param.Tag, Command.Id, Param.Name);
							// #casecast Script Param 
							break; }
							case CommandParseType.Script: {
							  Script Param = (Script) CommandEntry; 
							// 			Options.#{Param.Id}.Register ("#{Param.Extension}", Registry, (int) TagType_#{Command.Id}.#{Param.Id}); 
							_Output.Write ("			Options.{1}.Register (\"{2}\", Registry, (int) TagType_{3}.{4});\n{0}", _Indent, Param.Id, Param.Extension, Command.Id, Param.Id);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (_Choice CommandEntry in Command.Entries) 
					foreach  (_Choice CommandEntry in Command.Entries) {
						// #switchcast CommandParseType CommandEntry 
						switch (CommandEntry._Tag ()) {
							// #casecast Parameter Param 
							case CommandParseType.Parameter: {
							  Parameter Param = (Parameter) CommandEntry; 
							// 			// looking for parameter #Param.Name} 
							_Output.Write ("			// looking for parameter Param.Name}}\n{0}", _Indent);
							// 			if (index < args.Length && !IsFlag (args [index][0] )) { 
							_Output.Write ("			if (index < args.Length && !IsFlag (args [index][0] )) {{\n{0}", _Indent);
							// 				// Have got the parameter, call the parameter value method 
							_Output.Write ("				// Have got the parameter, call the parameter value method\n{0}", _Indent);
							// 				Options.#{Param.Name}.Parameter (args [index]); 
							_Output.Write ("				Options.{1}.Parameter (args [index]);\n{0}", _Indent, Param.Name);
							// 				index++; 
							_Output.Write ("				index++;\n{0}", _Indent);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// #casecast Parser Param 
							break; }
							case CommandParseType.Parser: {
							  Parser Param = (Parser) CommandEntry; 
							// 			// looking for parameter #Param.Class} 
							_Output.Write ("			// looking for parameter Param.Class}}\n{0}", _Indent);
							// 			if (index < args.Length && !IsFlag (args [index][0] )) { 
							_Output.Write ("			if (index < args.Length && !IsFlag (args [index][0] )) {{\n{0}", _Indent);
							// 				// Have got the parameter, call the parameter value method 
							_Output.Write ("				// Have got the parameter, call the parameter value method\n{0}", _Indent);
							// 				Options.#{Param.Class}.Parameter (args [index]); 
							_Output.Write ("				Options.{1}.Parameter (args [index]);\n{0}", _Indent, Param.Class);
							// 				index++; 
							_Output.Write ("				index++;\n{0}", _Indent);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// ##pragma warning disable 162 
					_Output.Write ("#pragma warning disable 162\n{0}", _Indent);
					// 			for (int i = index; i< args.Length; i++) { 
					_Output.Write ("			for (int i = index; i< args.Length; i++) {{\n{0}", _Indent);
					// 				if 	(!IsFlag (args [i][0] )) { 
					_Output.Write ("				if 	(!IsFlag (args [i][0] )) {{\n{0}", _Indent);
					// 					throw new System.Exception ("Unexpected parameter: " + args[i]);}			 
					_Output.Write ("					throw new System.Exception (\"Unexpected parameter: \" + args[i]);}}			\n{0}", _Indent);
					// 				string Rest = args [i].Substring (1); 
					_Output.Write ("				string Rest = args [i].Substring (1);\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 				TagType_#{Command.Id} TagType = (TagType_#{Command.Id}) Registry.Find (Rest); 
					_Output.Write ("				TagType_{1} TagType = (TagType_{2}) Registry.Find (Rest);\n{0}", _Indent, Command.Id, Command.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 				// here have the cases for what to do with it. 
					_Output.Write ("				// here have the cases for what to do with it.\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 				switch (TagType) { 
					_Output.Write ("				switch (TagType) {{\n{0}", _Indent);
					// #foreach (_Choice CommandEntry in Command.Entries) 
					foreach  (_Choice CommandEntry in Command.Entries) {
						// #switchcast CommandParseType CommandEntry 
						switch (CommandEntry._Tag ()) {
							// #casecast Option Option 
							case CommandParseType.Option: {
							  Option Option = (Option) CommandEntry; 
							// 					case TagType_#{Command.Id}.#{Option.Name} : { 
							_Output.Write ("					case TagType_{1}.{2} : {{\n{0}", _Indent, Command.Id, Option.Name);
							// 						int OptionParams = Options.#{Option.Name}.Tag (Rest); 
							_Output.Write ("						int OptionParams = Options.{1}.Tag (Rest);\n{0}", _Indent, Option.Name);
							// 						 
							_Output.Write ("						\n{0}", _Indent);
							// 						if (OptionParams>0 && ((i+1) < args.Length)) { 
							_Output.Write ("						if (OptionParams>0 && ((i+1) < args.Length)) {{\n{0}", _Indent);
							// 							if 	(!IsFlag (args [i+1][0] )) { 
							_Output.Write ("							if 	(!IsFlag (args [i+1][0] )) {{\n{0}", _Indent);
							// 								i++;								 
							_Output.Write ("								i++;								\n{0}", _Indent);
							// 								Options.#{Option.Name}.Parameter (args[i]); 
							_Output.Write ("								Options.{1}.Parameter (args[i]);\n{0}", _Indent, Option.Name);
							// 								} 
							_Output.Write ("								}}\n{0}", _Indent);
							// 							} 
							_Output.Write ("							}}\n{0}", _Indent);
							// 						break; 
							_Output.Write ("						break;\n{0}", _Indent);
							// 						} 
							_Output.Write ("						}}\n{0}", _Indent);
							// #casecast Lazy Lazy 
							break; }
							case CommandParseType.Lazy: {
							  Lazy Lazy = (Lazy) CommandEntry; 
							// 					case TagType_#{Command.Id}.#{Lazy.Name} : { 
							_Output.Write ("					case TagType_{1}.{2} : {{\n{0}", _Indent, Command.Id, Lazy.Name);
							// 						int OptionParams = Options.#{Lazy.Name}.Tag (Rest); 
							_Output.Write ("						int OptionParams = Options.{1}.Tag (Rest);\n{0}", _Indent, Lazy.Name);
							// 						 
							_Output.Write ("						\n{0}", _Indent);
							// 						if (OptionParams>0 && ((i+1) < args.Length)) { 
							_Output.Write ("						if (OptionParams>0 && ((i+1) < args.Length)) {{\n{0}", _Indent);
							// 							if 	(!IsFlag (args [i+1][0] )) { 
							_Output.Write ("							if 	(!IsFlag (args [i+1][0] )) {{\n{0}", _Indent);
							// 								i++;								 
							_Output.Write ("								i++;								\n{0}", _Indent);
							// 								Options.#{Lazy.Name}.Parameter (args[i]); 
							_Output.Write ("								Options.{1}.Parameter (args[i]);\n{0}", _Indent, Lazy.Name);
							// 								} 
							_Output.Write ("								}}\n{0}", _Indent);
							// 							} 
							_Output.Write ("							}}\n{0}", _Indent);
							// 						break; 
							_Output.Write ("						break;\n{0}", _Indent);
							// 						} 
							_Output.Write ("						}}\n{0}", _Indent);
							// #casecast Script Script 
							break; }
							case CommandParseType.Script: {
							  Script Script = (Script) CommandEntry; 
							// 					case TagType_#{Command.Id}.#{Script.Id} : { 
							_Output.Write ("					case TagType_{1}.{2} : {{\n{0}", _Indent, Command.Id, Script.Id);
							// 						int OptionParams = Options.#{Script.Id}.Tag (Rest); 
							_Output.Write ("						int OptionParams = Options.{1}.Tag (Rest);\n{0}", _Indent, Script.Id);
							// 			 
							_Output.Write ("			\n{0}", _Indent);
							// 						if (OptionParams>0 && ((i+1) < args.Length)) { 
							_Output.Write ("						if (OptionParams>0 && ((i+1) < args.Length)) {{\n{0}", _Indent);
							// 							if 	(!IsFlag (args [i+1][0] )) { 
							_Output.Write ("							if 	(!IsFlag (args [i+1][0] )) {{\n{0}", _Indent);
							// 								i++;								 
							_Output.Write ("								i++;								\n{0}", _Indent);
							// 								Options.#{Script.Id}.Parameter (args[i]); 
							_Output.Write ("								Options.{1}.Parameter (args[i]);\n{0}", _Indent, Script.Id);
							// 								} 
							_Output.Write ("								}}\n{0}", _Indent);
							// 							} 
							_Output.Write ("							}}\n{0}", _Indent);
							// 						break; 
							_Output.Write ("						break;\n{0}", _Indent);
							// 						} 
							_Output.Write ("						}}\n{0}", _Indent);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// 					default : throw new System.Exception ("Internal error"); 
					_Output.Write ("					default : throw new System.Exception (\"Internal error\");\n{0}", _Indent);
					// 					} 
					_Output.Write ("					}}\n{0}", _Indent);
					// 				} 
					_Output.Write ("				}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// ##pragma warning restore 162 
					_Output.Write ("#pragma warning restore 162\n{0}", _Indent);
					// 			Dispatch.#{Command.Id} (Options); 
					_Output.Write ("			Dispatch.{1} (Options);\n{0}", _Indent, Command.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		private static void Usage () { 
			_Output.Write ("		private static void Usage () {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Entry in Class.Entries) 
			foreach  (_Choice Entry in Class.Entries) {
				// #switchcast CommandParseType Entry 
				switch (Entry._Tag ()) {
					// #casecast Brief Cast 
					case CommandParseType.Brief: {
					  Brief Cast = (Brief) Entry; 
					// 				Console.WriteLine ("#{Cast.Text}"); 
					_Output.Write ("				Console.WriteLine (\"{1}\");\n{0}", _Indent, Cast.Text);
					// 				Console.WriteLine (""); 
					_Output.Write ("				Console.WriteLine (\"\");\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Entry in Class.Entries) 
			foreach  (_Choice Entry in Class.Entries) {
				// #switchcast CommandParseType Entry 
				switch (Entry._Tag ()) {
					// #casecast Command Command 
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					// #call CommandUsage Command 
					
					CommandUsage (Command);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 			} // Usage  
			_Output.Write ("			}} // Usage \n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public class ParserException : System.Exception { 
			_Output.Write ("		public class ParserException : System.Exception {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			public ParserException(string message) 
			_Output.Write ("			public ParserException(string message)\n{0}", _Indent);
			// 				: base(message) { 
			_Output.Write ("				: base(message) {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 				Console.WriteLine (message); 
			_Output.Write ("				Console.WriteLine (message);\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	} // class Main 
			_Output.Write ("	}} // class Main\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// The stub class for carrying optional parameters for each command type 
			_Output.Write ("	// The stub class for carrying optional parameters for each command type\n{0}", _Indent);
			// 	// As with the main class each consists of an abstract main class  
			_Output.Write ("	// As with the main class each consists of an abstract main class \n{0}", _Indent);
			// 	// with partial virtual that can be extended as required. 
			_Output.Write ("	// with partial virtual that can be extended as required.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch  
			_Output.Write ("	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch \n{0}", _Indent);
			// 	// and Goedel.Registry.Type 
			_Output.Write ("	// and Goedel.Registry.Type\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Entry in Class.Entries) 
			foreach  (_Choice Entry in Class.Entries) {
				// #switchcast CommandParseType Entry 
				switch (Entry._Tag ()) {
					// #casecast Command Cast 
					case CommandParseType.Command: {
					  Command Cast = (Command) Entry; 
					//  
					_Output.Write ("\n{0}", _Indent);
					//     public class _#{Cast.Id} : #{NameDispatchType} { 
					_Output.Write ("    public class _{1} : {2} {{\n{0}", _Indent, Cast.Id, NameDispatchType);
					// #foreach (_Choice OptionC in Cast.Entries) 
					foreach  (_Choice OptionC in Cast.Entries) {
						// #switchcast CommandParseType OptionC 
						switch (OptionC._Tag ()) {
							// #casecast Parser Parser 
							case CommandParseType.Parser: {
							  Parser Parser = (Parser) OptionC; 
							// 		public ExistingFile					#{Parser.Class} = new ExistingFile ("#{Parser.Extension}"); 
							_Output.Write ("		public ExistingFile					{1} = new ExistingFile (\"{2}\");\n{0}", _Indent, Parser.Class, Parser.Extension);
							// #casecast Generator Generator 
							break; }
							case CommandParseType.Generator: {
							  Generator Generator = (Generator) OptionC; 
							// #casecast Script Script 
							break; }
							case CommandParseType.Script: {
							  Script Script = (Script) OptionC; 
							// 		public NewFile						#{Script.Id} = new NewFile ("#{Script.Extension}"); 
							_Output.Write ("		public NewFile						{1} = new NewFile (\"{2}\");\n{0}", _Indent, Script.Id, Script.Extension);
							// #casecast Lazy Lazy 
							break; }
							case CommandParseType.Lazy: {
							  Lazy Lazy = (Lazy) OptionC; 
							// 		public Flag							#{Lazy.Name} = new Flag ("false"); 
							_Output.Write ("		public Flag							{1} = new Flag (\"false\");\n{0}", _Indent, Lazy.Name);
							// #casecast Parameter Parameter 
							break; }
							case CommandParseType.Parameter: {
							  Parameter Parameter = (Parameter) OptionC; 
							// #% string DefaultParameter = null; 
							
							 string DefaultParameter = null;
							// #foreach (_Choice Modifier in Parameter.Modifier) 
							foreach  (_Choice Modifier in Parameter.Modifier) {
								// #switchcast CommandParseType Modifier 
								switch (Modifier._Tag ()) {
									// #casecast Default Default 
									case CommandParseType.Default: {
									  Default Default = (Default) Modifier; 
									// #% DefaultParameter = Default.Text; 
									
									 DefaultParameter = Default.Text;
									// #end switchcast 
								break; }
									}
								// #end foreach 
								}
							// #if DefaultParameter == null 
							if (  DefaultParameter == null ) {
								// 		public #{Parameter.Type}			#{Parameter.Name} = new #{Parameter.Type} (); 
								_Output.Write ("		public {1}			{2} = new {3} ();\n{0}", _Indent, Parameter.Type, Parameter.Name, Parameter.Type);
								// #else 
								} else {
								// 		public #{Parameter.Type}			#{Parameter.Name} = new #{Parameter.Type} ("#{DefaultParameter}"); 
								_Output.Write ("		public {1}			{2} = new {3} (\"{4}\");\n{0}", _Indent, Parameter.Type, Parameter.Name, Parameter.Type, DefaultParameter);
								// #end if 
								}
							// #casecast Option Option 
							break; }
							case CommandParseType.Option: {
							  Option Option = (Option) OptionC; 
							//  
							_Output.Write ("\n{0}", _Indent);
							// #% string DefaultOption = null; 
							
							 string DefaultOption = null;
							// #foreach (_Choice Modifier in Option.Modifier) 
							foreach  (_Choice Modifier in Option.Modifier) {
								// #switchcast CommandParseType Modifier 
								switch (Modifier._Tag ()) {
									// #casecast Default Default 
									case CommandParseType.Default: {
									  Default Default = (Default) Modifier; 
									// #% DefaultOption = Default.Text; 
									
									 DefaultOption = Default.Text;
									// #end switchcast 
								break; }
									}
								// #end foreach 
								}
							// #if DefaultOption == null 
							if (  DefaultOption == null ) {
								// 		public #{Option.Type}			#{Option.Name} = new  #{Option.Type} (); 
								_Output.Write ("		public {1}			{2} = new  {3} ();\n{0}", _Indent, Option.Type, Option.Name, Option.Type);
								// #else 
								} else {
								// 		public #{Option.Type}			#{Option.Name} = new  #{Option.Type} ("#{DefaultOption}"); 
								_Output.Write ("		public {1}			{2} = new  {3} (\"{4}\");\n{0}", _Indent, Option.Type, Option.Name, Option.Type, DefaultOption);
								// #end if 
								}
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//     public partial class #{Cast.Id} : _#{Cast.Id} { 
					_Output.Write ("    public partial class {1} : _{2} {{\n{0}", _Indent, Cast.Id, Cast.Id);
					//         } // class #{Cast.Id} 
					_Output.Write ("        }} // class {1}\n{0}", _Indent, Cast.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (ID<_Choice> ID in TypeType.IDs) 
			foreach  (ID<_Choice> ID in TypeType.IDs) {
				//     // Parameter type #{ID} 
				_Output.Write ("    // Parameter type {1}\n{0}", _Indent, ID);
				// #if (ID.ToString() == "NewFile") | (ID.ToString() == "ExistingFile") 
				if (  (ID.ToString() == "NewFile") | (ID.ToString() == "ExistingFile") ) {
					//     public abstract class _#{ID} : #{NameClassFile} { 
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassFile);
					// #elseif (ID.ToString() == "Flag") 
					} else if (  (ID.ToString() == "Flag")) {
					//     public abstract class _#{ID} : #{NameClassFlag} { 
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassFlag);
					// #else 
					} else {
					//     public abstract class _#{ID} : #{NameClassType} { 
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassType);
					// #end if 
					}
				//         public _#{ID}() { 
				_Output.Write ("        public _{1}() {{\n{0}", _Indent, ID);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//         public _#{ID}(string Value) { 
				_Output.Write ("        public _{1}(string Value) {{\n{0}", _Indent, ID);
				// 			Default (Value); 
				_Output.Write ("			Default (Value);\n{0}", _Indent);
				//             }  
				_Output.Write ("            }} \n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #if CommandParse.Builtins 
				if (  CommandParse.Builtins ) {
					// #if (ID.ToString() == "Flag") 
					if (  (ID.ToString() == "Flag") ) {
						//  
						_Output.Write ("\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// #elseif (ID.ToString() == "NewFile") 
						} else if (  (ID.ToString() == "NewFile")) {
						//  
						_Output.Write ("\n{0}", _Indent);
						// #elseif (ID.ToString() == "ExistingFile") 
						} else if (  (ID.ToString() == "ExistingFile")) {
						//  
						_Output.Write ("\n{0}", _Indent);
						// #else 
						} else {
						// 		public string			Value { 
						_Output.Write ("		public string			Value {{\n{0}", _Indent);
						// 			get {return Text;} 
						_Output.Write ("			get {{return Text;}}\n{0}", _Indent);
						// 			} 
						_Output.Write ("			}}\n{0}", _Indent);
						// #end if 
						}
					// #end if 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				//         } // _#{ID} 
				_Output.Write ("        }} // _{1}\n{0}", _Indent, ID);
				//  
				_Output.Write ("\n{0}", _Indent);
				//     public partial class  #{ID} : _#{ID} { 
				_Output.Write ("    public partial class  {1} : _{2} {{\n{0}", _Indent, ID, ID);
				//         public #{ID}() { 
				_Output.Write ("        public {1}() {{\n{0}", _Indent, ID);
				//             }  
				_Output.Write ("            }} \n{0}", _Indent);
				//         public #{ID}(string Value) { 
				_Output.Write ("        public {1}(string Value) {{\n{0}", _Indent, ID);
				// 			Default (Value); 
				_Output.Write ("			Default (Value);\n{0}", _Indent);
				//             }  
				_Output.Write ("            }} \n{0}", _Indent);
				//         } // #{ID} 
				_Output.Write ("        }} // {1}\n{0}", _Indent, ID);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// The stub class just contains routines that echo their arguments and 
			_Output.Write ("	// The stub class just contains routines that echo their arguments and\n{0}", _Indent);
			// 	// write 'not yet implemented' 
			_Output.Write ("	// write 'not yet implemented'\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// Eventually there will be a compiler option to suppress the debugging 
			_Output.Write ("	// Eventually there will be a compiler option to suppress the debugging\n{0}", _Indent);
			// 	// to eliminate the redundant code 
			_Output.Write ("	// to eliminate the redundant code\n{0}", _Indent);
			//     public class _#{Class.Id} { 
			_Output.Write ("    public class _{1} {{\n{0}", _Indent, Class.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Entry in Class.Entries) 
			foreach  (_Choice Entry in Class.Entries) {
				// #% bool DefaultOutput = true; 
				 bool DefaultOutput = true;
				// #% string Lazy = null; 
				 string Lazy = null;
				// #switchcast CommandParseType Entry 
				switch (Entry._Tag ()) {
					// #casecast Command Command 
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					// #foreach (_Choice CommandEntry in Command.Entries) 
					foreach  (_Choice CommandEntry in Command.Entries) {
						// #switchcast CommandParseType CommandEntry 
						switch (CommandEntry._Tag ()) {
							// #casecast Parser Cast 
							case CommandParseType.Parser: {
							  Parser Cast = (Parser) CommandEntry; 
							// #% DefaultOutput = false; 
							
							 DefaultOutput = false;
							// #casecast Generator Cast 
							break; }
							case CommandParseType.Generator: {
							  Generator Cast = (Generator) CommandEntry; 
							// #% DefaultOutput = false; 
							
							 DefaultOutput = false;
							// #casecast Script Cast 
							break; }
							case CommandParseType.Script: {
							  Script Cast = (Script) CommandEntry; 
							// #% DefaultOutput = false; 
							
							 DefaultOutput = false;
							// #casecast Lazy Cast 
							break; }
							case CommandParseType.Lazy: {
							  Lazy Cast = (Lazy) CommandEntry; 
							// #% Lazy = Cast.Name.ToString (); 
							
							 Lazy = Cast.Name.ToString ();
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// 		public virtual void #{Command.Id} ( #{Command.Id} Options 
					_Output.Write ("		public virtual void {1} ( {2} Options\n{0}", _Indent, Command.Id, Command.Id);
					// 				) { 
					_Output.Write ("				) {{\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #if DefaultOutput 
					if (  DefaultOutput ) {
						// 			char UsageFlag = '-'; 
						_Output.Write ("			char UsageFlag = '-';\n{0}", _Indent);
						// #call CommandUsage Command 
						CommandUsage (Command);
						//  
						_Output.Write ("\n{0}", _Indent);
						// #foreach (_Choice CommandEntry in Command.Entries) 
						foreach  (_Choice CommandEntry in Command.Entries) {
							// #switchcast CommandParseType CommandEntry 
							switch (CommandEntry._Tag ()) {
								// #casecast Parameter Parameter 
								case CommandParseType.Parameter: {
								  Parameter Parameter = (Parameter) CommandEntry; 
								// 				Console.WriteLine ("    {0}\t{1} = [{2}]", "#{Parameter.Type}",  
								_Output.Write ("				Console.WriteLine (\"    {{0}}\\t{{1}} = [{{2}}]\", \"{1}\", \n{0}", _Indent, Parameter.Type);
								// 							"#{Parameter.Name}", Options.#{Parameter.Name}); 
								_Output.Write ("							\"{1}\", Options.{2});\n{0}", _Indent, Parameter.Name, Parameter.Name);
								// #casecast Option Option 
								break; }
								case CommandParseType.Option: {
								  Option Option = (Option) CommandEntry; 
								// 				Console.WriteLine ("    {0}\t{1} = [{2}]", "#{Option.Type}",  
								_Output.Write ("				Console.WriteLine (\"    {{0}}\\t{{1}} = [{{2}}]\", \"{1}\", \n{0}", _Indent, Option.Type);
								// 							"#{Option.Name}", Options.#{Option.Name}); 
								_Output.Write ("							\"{1}\", Options.{2});\n{0}", _Indent, Option.Name, Option.Name);
								// #end switchcast 
							break; }
								}
							// #end foreach 
							}
						// 			Console.WriteLine ("Not Yet Implemented"); 
						_Output.Write ("			Console.WriteLine (\"Not Yet Implemented\");\n{0}", _Indent);
						// #else 
						} else {
						// 			string inputfile = null; 
						_Output.Write ("			string inputfile = null;\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// #foreach (_Choice CommandEntry in Command.Entries) 
						foreach  (_Choice CommandEntry in Command.Entries) {
							// #switchcast CommandParseType CommandEntry 
							switch (CommandEntry._Tag ()) {
								// #casecast Parser Parser 
								case CommandParseType.Parser: {
								  Parser Parser = (Parser) CommandEntry; 
								// 			inputfile = Options.#{Parser.Class}.Text; 
								_Output.Write ("			inputfile = Options.{1}.Text;\n{0}", _Indent, Parser.Class);
								//  
								_Output.Write ("\n{0}", _Indent);
								//             #{Parser.Namespace}.#{Parser.Class} Parse = new #{Parser.Namespace}.#{Parser.Class}(); 
								_Output.Write ("            {1}.{2} Parse = new {3}.{4}();\n{0}", _Indent, Parser.Namespace, Parser.Class, Parser.Namespace, Parser.Class);
								//  
								_Output.Write ("\n{0}", _Indent);
								//  
								_Output.Write ("\n{0}", _Indent);
								// #foreach (_Choice CommandEntry2 in Command.Entries) 
								foreach  (_Choice CommandEntry2 in Command.Entries) {
									// #switchcast CommandParseType CommandEntry2 
									switch (CommandEntry2._Tag ()) {
										// #casecast Option Option 
										case CommandParseType.Option: {
										  Option Option = (Option) CommandEntry2; 
										// 			Parse.#{Option.Name} = Options.#{Option.Name}.Value; 
										_Output.Write ("			Parse.{1} = Options.{2}.Value;\n{0}", _Indent, Option.Name, Option.Name);
										// #end switchcast 
									break; }
										}
									// #end foreach 
									}
								// 			Parse.Options = Options; 
								_Output.Write ("			Parse.Options = Options;\n{0}", _Indent);
								//          
								_Output.Write ("        \n{0}", _Indent);
								// 			 
								_Output.Write ("			\n{0}", _Indent);
								// 			using (Stream infile = 
								_Output.Write ("			using (Stream infile =\n{0}", _Indent);
								//                         new FileStream(inputfile, FileMode.Open, FileAccess.Read)) { 
								_Output.Write ("                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {{\n{0}", _Indent);
								//  
								_Output.Write ("\n{0}", _Indent);
								//                 Lexer Schema = new Lexer(inputfile); 
								_Output.Write ("                Lexer Schema = new Lexer(inputfile);\n{0}", _Indent);
								//  
								_Output.Write ("\n{0}", _Indent);
								//                 Schema.Process(infile, Parse); 
								_Output.Write ("                Schema.Process(infile, Parse);\n{0}", _Indent);
								//                 } 
								_Output.Write ("                }}\n{0}", _Indent);
								//  
								_Output.Write ("\n{0}", _Indent);
								// #casecast Generator Generator 
								break; }
								case CommandParseType.Generator: {
								  Generator Generator = (Generator) CommandEntry; 
								// #end switchcast 
							break; }
								}
							// #end foreach 
							}
						//  
						_Output.Write ("\n{0}", _Indent);
						// #foreach (_Choice CommandEntry in Command.Entries) 
						foreach  (_Choice CommandEntry in Command.Entries) {
							// #switchcast CommandParseType CommandEntry 
							switch (CommandEntry._Tag ()) {
								// #casecast Script Script 
								case CommandParseType.Script: {
								  Script Script = (Script) CommandEntry; 
								// 			// Script output of type #{Script.Id} #{Script.Extension} 
								_Output.Write ("			// Script output of type {1} {2}\n{0}", _Indent, Script.Id, Script.Extension);
								// 			if (Options.#{Script.Id}.Text != null) { 
								_Output.Write ("			if (Options.{1}.Text != null) {{\n{0}", _Indent, Script.Id);
								// 				string outputfile = FileTools.DefaultOutput (inputfile, Options.#{Script.Id}.Text,  
								_Output.Write ("				string outputfile = FileTools.DefaultOutput (inputfile, Options.{1}.Text, \n{0}", _Indent, Script.Id);
								// 					Options.#{Script.Id}.Extension); 
								_Output.Write ("					Options.{1}.Extension);\n{0}", _Indent, Script.Id);
								// #if Lazy != null 
								if (  Lazy != null ) {
									// 				if (Options.#{Lazy}.IsSet & FileTools.UpToDate (inputfile, outputfile)) { 
									_Output.Write ("				if (Options.{1}.IsSet & FileTools.UpToDate (inputfile, outputfile)) {{\n{0}", _Indent, Lazy);
									// 					return; 
									_Output.Write ("					return;\n{0}", _Indent);
									// 					} 
									_Output.Write ("					}}\n{0}", _Indent);
									// 				using (Stream outputStream = 
									_Output.Write ("				using (Stream outputStream =\n{0}", _Indent);
									// 							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) { 
									_Output.Write ("							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {{\n{0}", _Indent);
									// 					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) { 
									_Output.Write ("					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {{\n{0}", _Indent);
									//  
									_Output.Write ("\n{0}", _Indent);
									// 						#{Script.Namespace}.#{Script.Class} Script = new #{Script.Namespace}.#{Script.Class} (OutputWriter); 
									_Output.Write ("						{1}.{2} Script = new {3}.{4} (OutputWriter);\n{0}", _Indent, Script.Namespace, Script.Class, Script.Namespace, Script.Class);
									//  
									_Output.Write ("\n{0}", _Indent);
									// 						Script.#{Script.Id} (Parse); 
									_Output.Write ("						Script.{1} (Parse);\n{0}", _Indent, Script.Id);
									// 						} 
									_Output.Write ("						}}\n{0}", _Indent);
									// 					} 
									_Output.Write ("					}}\n{0}", _Indent);
									// #end if		 
									}
								// 				} 
								_Output.Write ("				}}\n{0}", _Indent);
								// #end switchcast 
							break; }
								}
							// #end foreach 
							}
						// #end if 
						}
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//         } // class _#{Class.Id} 
			_Output.Write ("        }} // class _{1}\n{0}", _Indent, Class.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     public partial class #{Class.Id} : _#{Class.Id} { 
			_Output.Write ("    public partial class {1} : _{2} {{\n{0}", _Indent, Class.Id, Class.Id);
			//         } // class #{Class.Id} 
			_Output.Write ("        }} // class {1}\n{0}", _Indent, Class.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     } // namespace #{Class.Id} 
			_Output.Write ("    }} // namespace {1}\n{0}", _Indent, Class.Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		// // Types 
		// //      NewFile   ExistingFile 
		// //		Flag, String, Integer, enumeration 
		//  
		// #method CommandUsage Command Command 
		

		//
		// CommandUsage
		//
		public void CommandUsage (Command Command) {
			// 				{ 
			_Output.Write ("				{{\n{0}", _Indent);
			// ##pragma warning disable 219 
			_Output.Write ("#pragma warning disable 219\n{0}", _Indent);
			// 					#{Command.Id}		Dummy = new #{Command.Id} (); 
			_Output.Write ("					{1}		Dummy = new {2} ();\n{0}", _Indent, Command.Id, Command.Id);
			// ##pragma warning restore 219 
			_Output.Write ("#pragma warning restore 219\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 					Console.Write ("{0}#{Command.Tag} ", UsageFlag); 
			_Output.Write ("					Console.Write (\"{{0}}{1} \", UsageFlag);\n{0}", _Indent, Command.Tag);
			// #foreach (_Choice CommandEntry in Command.Entries) 
			foreach  (_Choice CommandEntry in Command.Entries) {
				// #switchcast CommandParseType CommandEntry 
				switch (CommandEntry._Tag ()) {
					// #casecast Parameter Parameter 
					case CommandParseType.Parameter: {
					  Parameter Parameter = (Parameter) CommandEntry; 
					// 					Console.Write ("[{0}] ", Dummy.#{Parameter.Name}.Usage (null, "#{Parameter.Text}", UsageFlag)); 
					_Output.Write ("					Console.Write (\"[{{0}}] \", Dummy.{1}.Usage (null, \"{2}\", UsageFlag));\n{0}", _Indent, Parameter.Name, Parameter.Text);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #foreach (_Choice CommandEntry in Command.Entries) 
			foreach  (_Choice CommandEntry in Command.Entries) {
				// #switchcast CommandParseType CommandEntry 
				switch (CommandEntry._Tag ()) {
					// #casecast Option Option 
					case CommandParseType.Option: {
					  Option Option = (Option) CommandEntry; 
					// 					Console.Write ("[{0}] ", Dummy.#{Option.Name}.Usage ("#{Option.Command}", "value", UsageFlag)); 
					_Output.Write ("					Console.Write (\"[{{0}}] \", Dummy.{1}.Usage (\"{2}\", \"value\", UsageFlag));\n{0}", _Indent, Option.Name, Option.Command);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 					Console.WriteLine (); 
			_Output.Write ("					Console.WriteLine ();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice CommandEntry in Command.Entries) 
			foreach  (_Choice CommandEntry in Command.Entries) {
				// #switchcast CommandParseType CommandEntry 
				switch (CommandEntry._Tag ()) {
					// #casecast Brief CommandBreif 
					case CommandParseType.Brief: {
					  Brief CommandBreif = (Brief) CommandEntry; 
					// 					Console.WriteLine ("    #{CommandBreif.Text}"); 
					_Output.Write ("					Console.WriteLine (\"    {1}\");\n{0}", _Indent, CommandBreif.Text);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// #end method 
			}
		//  
		// #end xclass 
		}
	}
