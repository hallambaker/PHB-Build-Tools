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
			// #% Exceptions._InitChildren (); 
			 Exceptions._InitChildren ();
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Exceptions.Top) 
			foreach  (_Choice Toplevel in Exceptions.Top) {
				// #switchcast ExceptionsType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast Using Using 
					case ExceptionsType.Using: {
					  Using Using = (Using) Toplevel; 
					// using #{Using.Id}; 
					_Output.Write ("using {1};\n{0}", _Indent, Using.Id);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
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
					//  
					_Output.Write ("\n{0}", _Indent);
					// #% WriteListExceptions (Namespace.Options); 
					
					 WriteListExceptions (Namespace.Options);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		//  
		// #method WriteException Exception Exception 
		

		//
		// WriteException
		//
		public void WriteException (Exception Exception) {
			//  
			_Output.Write ("\n{0}", _Indent);
			//     /// <summary> 
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			// #foreach (_Choice Optionlevel in Exception.Options) 
			foreach  (_Choice Optionlevel in Exception.Options) {
				// #switchcast ExceptionsType Optionlevel 
				switch (Optionlevel._Tag ()) {
					// #casecast Description Description 
					case ExceptionsType.Description: {
					  Description Description = (Description) Optionlevel; 
					// #foreach (String Text in Description.Text) 
					foreach  (String Text in Description.Text) {
						//     /// #{Text} 
						_Output.Write ("    /// {1}\n{0}", _Indent, Text);
						// #end foreach 
						}
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//     /// </summary> 
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			//     public class #{Exception.Id} : #{Exception.BaseClass} { 
			_Output.Write ("    public class {1} : {2} {{\n{0}", _Indent, Exception.Id, Exception.BaseClass);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		/// <summary> 
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			//         /// Create an instance of the exception. 
			_Output.Write ("        /// Create an instance of the exception.\n{0}", _Indent);
			//         /// </summary>		 
			_Output.Write ("        /// </summary>		\n{0}", _Indent);
			// 		public #{Exception.Id} () : base () { 
			_Output.Write ("		public {1} () : base () {{\n{0}", _Indent, Exception.Id);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//          
			_Output.Write ("        \n{0}", _Indent);
			// 		/// <summary> 
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			//         /// Create an instance of the exception. 
			_Output.Write ("        /// Create an instance of the exception.\n{0}", _Indent);
			//         /// </summary>		 
			_Output.Write ("        /// </summary>		\n{0}", _Indent);
			//         /// <param name="Description">Description of the error</param>	 
			_Output.Write ("        /// <param name=\"Description\">Description of the error</param>	\n{0}", _Indent);
			// 		public #{Exception.Id} (string Description) : base (Description) { 
			_Output.Write ("		public {1} (string Description) : base (Description) {{\n{0}", _Indent, Exception.Id);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		/// <summary> 
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			//         /// Create an instance of the exception. 
			_Output.Write ("        /// Create an instance of the exception.\n{0}", _Indent);
			//         /// </summary>		 
			_Output.Write ("        /// </summary>		\n{0}", _Indent);
			//         /// <param name="Description">Description of the error</param>	 
			_Output.Write ("        /// <param name=\"Description\">Description of the error</param>	\n{0}", _Indent);
			// 		/// <param name="Inner">Inner Exception</param>	 
			_Output.Write ("		/// <param name=\"Inner\">Inner Exception</param>	\n{0}", _Indent);
			// 		public #{Exception.Id} (string Description, System.Exception Inner) :  
			_Output.Write ("		public {1} (string Description, System.Exception Inner) : \n{0}", _Indent, Exception.Id);
			// 				base (Description, Inner) { 
			_Output.Write ("				base (Description, Inner) {{\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Exception.Base) 
			if (  (Exception.Base) ) {
				// 		public object UserData; 
				_Output.Write ("		public object UserData;\n{0}", _Indent);
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Object in Exception.Objects) 
			foreach  (var Object in Exception.Objects) {
				// 		/// <summary> 
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				//         /// Create an instance of the exception. 
				_Output.Write ("        /// Create an instance of the exception.\n{0}", _Indent);
				//         /// </summary>		 
				_Output.Write ("        /// </summary>		\n{0}", _Indent);
				//         /// <param name="Object">User data</param>	 
				_Output.Write ("        /// <param name=\"Object\">User data</param>	\n{0}", _Indent);
				// 		/// <param name="Inner">Inner Exception</param>	 
				_Output.Write ("		/// <param name=\"Inner\">Inner Exception</param>	\n{0}", _Indent);
				// 		public #{Exception.Id} (#{Object.Type} Object) :  
				_Output.Write ("		public {1} ({2} Object) : \n{0}", _Indent, Exception.Id, Object.Type);
				// 				base (String.Format (#{Object.Text.Quoted()}#! 
				_Output.Write ("				base (String.Format ({1}", _Indent, Object.Text.Quoted());
				// #foreach (var Parameter in Object.Parameters) 
				foreach  (var Parameter in Object.Parameters) {
					// , 
					_Output.Write (",\n{0}", _Indent);
					// 					Object.#{Parameter.Name}#! 
					_Output.Write ("					Object.{1}", _Indent, Parameter.Name);
					// #end foreach				 
					}
				// 					)) { 
				_Output.Write ("					)) {{\n{0}", _Indent);
				// 			UserData = Object; 
				_Output.Write ("			UserData = Object;\n{0}", _Indent);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 		/// <summary> 
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				//         /// Create an instance of the exception. 
				_Output.Write ("        /// Create an instance of the exception.\n{0}", _Indent);
				//         /// </summary>		 
				_Output.Write ("        /// </summary>		\n{0}", _Indent);
				//         /// <param name="Object">User data</param>	 
				_Output.Write ("        /// <param name=\"Object\">User data</param>	\n{0}", _Indent);
				// 		/// <param name="Inner">Inner Exception</param>	 
				_Output.Write ("		/// <param name=\"Inner\">Inner Exception</param>	\n{0}", _Indent);
				// 		public #{Exception.Id} (#{Object.Type} Object, System.Exception Inner) :  
				_Output.Write ("		public {1} ({2} Object, System.Exception Inner) : \n{0}", _Indent, Exception.Id, Object.Type);
				// 				base (String.Format (#{Object.Text.Quoted()}#! 
				_Output.Write ("				base (String.Format ({1}", _Indent, Object.Text.Quoted());
				// #foreach (var Parameter in Object.Parameters) 
				foreach  (var Parameter in Object.Parameters) {
					// , 
					_Output.Write (",\n{0}", _Indent);
					// 					Object.#{Parameter.Name}#! 
					_Output.Write ("					Object.{1}", _Indent, Parameter.Name);
					// #end foreach				 
					}
				// 					), Inner) { 
				_Output.Write ("					), Inner) {{\n{0}", _Indent);
				// 			UserData = Object; 
				_Output.Write ("			UserData = Object;\n{0}", _Indent);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		 
			_Output.Write ("		\n{0}", _Indent);
			// 		/// <summary> 
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			//         /// The public fatory delegate 
			_Output.Write ("        /// The public fatory delegate\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         public static #{Exception.Base.If("", "new ")}global::Goedel.Utilities.ThrowDelegate Throw; 
			_Output.Write ("        public static {1}global::Goedel.Utilities.ThrowDelegate Throw;\n{0}", _Indent, Exception.Base.If("", "new "));
			//  
			_Output.Write ("\n{0}", _Indent);
			//         static System.Exception _Throw(object Reason) { 
			_Output.Write ("        static System.Exception _Throw(object Reason) {{\n{0}", _Indent);
			// 			if (Reason as string != null) { 
			_Output.Write ("			if (Reason as string != null) {{\n{0}", _Indent);
			// 				return new #{Exception.Id}(Reason as string); 
			_Output.Write ("				return new {1}(Reason as string);\n{0}", _Indent, Exception.Id);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			else { 
			_Output.Write ("			else {{\n{0}", _Indent);
			// 				return new #{Exception.Id}(#{Exception.Console.Quoted()}); 
			_Output.Write ("				return new {1}({2});\n{0}", _Indent, Exception.Id, Exception.Console.Quoted());
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//         } 
			_Output.Write ("        }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #% WriteListExceptions (Exception.Options); 
			 WriteListExceptions (Exception.Options);
			// #end method 
			}
		//  
		// #method WriteListExceptions List<_Choice> Exceptions 
		

		//
		// WriteListExceptions
		//
		public void WriteListExceptions (List<_Choice> Exceptions) {
			// #foreach (_Choice Exception in Exceptions) 
			foreach  (_Choice Exception in Exceptions) {
				// #if (Exception as Exception != null) 
				if (  (Exception as Exception != null) ) {
					// #% WriteException (Exception as Exception ); 
					 WriteException (Exception as Exception );
					// #end if 
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #end pclass 
		}
	}
