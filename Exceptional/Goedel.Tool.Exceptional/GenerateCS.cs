// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Copyright ©  2011 by Default Deny Security Inc.
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
// #pclass Goedel.Tool.Exceptional Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Exceptional {
	public partial class Generate : global::Goedel.Registry.Script {
		public Generate () : base () {
			}
		public Generate (TextWriter Output) : base (Output) {
			}

		// #% DateTime GenerateTime = DateTime.UtcNow; 
		 DateTime GenerateTime = DateTime.UtcNow;
		//  
		// #method GenerateCS Exceptions Exceptions 
		

		//
		// GenerateCS
		//
		public void GenerateCS (Exceptions Exceptions) {
			// #! Goedel.Registry.Script.Header (_Output, "//", GenerateTime); 
			// Goedel.Registry.Script.Header (_Output, "//", GenerateTime);
			// #! Goedel.Registry.Script.MITLicense (_Output, "//",  
			// Goedel.Registry.Script.MITLicense (_Output, "//", 
			// #!     Goedel.Registry.Script.AssemblyCopyright, 
			//     Goedel.Registry.Script.AssemblyCopyright,
			// #!     Goedel.Registry.Script.AssemblyCompany); 
			//     Goedel.Registry.Script.AssemblyCompany);
			// #% GenerateCSX (Exceptions); 
			 GenerateCSX (Exceptions);
			// #end method 
			}
		//  
		// #method GenerateCSX Exceptions Exceptions 
		

		//
		// GenerateCSX
		//
		public void GenerateCSX (Exceptions Exceptions) {
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Exceptions.Top) 
			foreach  (_Choice Toplevel in Exceptions.Top) {
				// #switchcast ExceptionsType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast Namespace Namespace 
					case ExceptionsType.Namespace: {
					  Namespace Namespace = (Namespace) Toplevel; 
					// namespace #{Namespace.Id} { 
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Namespace.Id);
					// #foreach (_Choice Nextlevel in Namespace.Options) 
					foreach  (_Choice Nextlevel in Namespace.Options) {
						// #switchcast ExceptionsType Nextlevel 
						switch (Nextlevel._Tag ()) {
							// #casecast Parent Parent 
							case ExceptionsType.Parent: {
							  Parent Parent = (Parent) Nextlevel; 
							//     // #{Parent.Id} 
							_Output.Write ("    // {1}\n{0}", _Indent, Parent.Id);
							// 	public class #{Parent.Id} : System.Exception { 
							_Output.Write ("	public class {1} : System.Exception {{\n{0}", _Indent, Parent.Id);
							// 		public #{Parent.Id} (String TagIn) { 
							_Output.Write ("		public {1} (String TagIn) {{\n{0}", _Indent, Parent.Id);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// 		public #{Parent.Id} () { 
							_Output.Write ("		public {1} () {{\n{0}", _Indent, Parent.Id);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// 		} 
							_Output.Write ("		}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #foreach (_Choice Entrylevel in Parent.Exceptions) 
							foreach  (_Choice Entrylevel in Parent.Exceptions) {
								// #switchcast ExceptionsType Entrylevel 
								switch (Entrylevel._Tag ()) {
									// #casecast Exception Exception 
									case ExceptionsType.Exception: {
									  Exception Exception = (Exception) Entrylevel; 
									//  
									_Output.Write ("\n{0}", _Indent);
									// #foreach (_Choice Optionlevel in Exception.Options) 
									foreach  (_Choice Optionlevel in Exception.Options) {
										// #switchcast ExceptionsType Optionlevel 
										switch (Optionlevel._Tag ()) {
											// #casecast Description Description 
											case ExceptionsType.Description: {
											  Description Description = (Description) Optionlevel; 
											// #foreach (String Text in Description.Text) 
											foreach  (String Text in Description.Text) {
												//     // #{Text} 
												_Output.Write ("    // {1}\n{0}", _Indent, Text);
												// #end foreach 
												}
											// #end switchcast 
										break; }
											}
										// #end foreach 
										}
									//  
									_Output.Write ("\n{0}", _Indent);
									// #foreach (_Choice Optionlevel in Exception.Options) 
									foreach  (_Choice Optionlevel in Exception.Options) {
										// #switchcast ExceptionsType Optionlevel 
										switch (Optionlevel._Tag ()) {
											// #casecast Console Console 
											case ExceptionsType.Console: {
											  Console Console = (Console) Optionlevel; 
											//     // Console:"#{Console.Message}" 
											_Output.Write ("    // Console:\"{1}\"\n{0}", _Indent, Console.Message);
											// #end switchcast 
										break; }
											}
										// #end foreach 
										}
									//  
									_Output.Write ("\n{0}", _Indent);
									// 	public class #{Exception.Id} : #{Parent.Id}  { 
									_Output.Write ("	public class {1} : {2}  {{\n{0}", _Indent, Exception.Id, Parent.Id);
									//  
									_Output.Write ("\n{0}", _Indent);
									// 		public #{Exception.Id} (String TagIn) { 
									_Output.Write ("		public {1} (String TagIn) {{\n{0}", _Indent, Exception.Id);
									// 			} 
									_Output.Write ("			}}\n{0}", _Indent);
									// 		public #{Exception.Id} () { 
									_Output.Write ("		public {1} () {{\n{0}", _Indent, Exception.Id);
									// 			} 
									_Output.Write ("			}}\n{0}", _Indent);
									// 		} 
									_Output.Write ("		}}\n{0}", _Indent);
									//  
									_Output.Write ("\n{0}", _Indent);
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
					//     } 
					_Output.Write ("    }}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		// #end pclass 
		}
	}
