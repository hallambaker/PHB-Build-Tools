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
namespace Goedel.Tool.Exceptional {
	/// <summary>A Goedel script.</summary>
	public partial class Generate : global::Goedel.Registry.Script {
		/// <summary>Default constructor.</summary>
		public Generate () : base () {
			}
		/// <summary>Constructor with output stream.</summary>
		/// <param name="Output">The output stream</param>
		public Generate (TextWriter Output) : base (Output) {
			}

		

		//
		// GenerateCS
		//
		public void GenerateCS (Exceptions Exceptions) {
			// Goedel.Registry.Script.Header (_Output, "//", GenerateTime);
			// Goedel.Registry.Script.MITLicense (_Output, "//", 
			//     Goedel.Registry.Script.AssemblyCopyright,
			//     Goedel.Registry.Script.AssemblyCompany);
			 GenerateCSX (Exceptions);
			}
		

		//
		// GenerateCSX
		//
		public void GenerateCSX (Exceptions Exceptions) {
			 Exceptions._InitChildren ();
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Toplevel in Exceptions.Top) {
				switch (Toplevel._Tag ()) {
					case ExceptionsType.Using: {
					  Using Using = (Using) Toplevel; 
					_Output.Write ("using {1};\n{0}", _Indent, Using.Id);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Toplevel in Exceptions.Top) {
				switch (Toplevel._Tag ()) {
					case ExceptionsType.Namespace: {
					  Namespace Namespace = (Namespace) Toplevel; 
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Namespace.Id);
					_Output.Write ("\n{0}", _Indent);
					
					 WriteListExceptions (Namespace.Options);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
				break; }
					}
				}
			}
		

		//
		// WriteException
		//
		public void WriteException (Exception Exception) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			foreach  (_Choice Optionlevel in Exception.Options) {
				switch (Optionlevel._Tag ()) {
					case ExceptionsType.Description: {
					  Description Description = (Description) Optionlevel; 
					foreach  (String Text in Description.Text) {
						_Output.Write ("    /// {1}\n{0}", _Indent, Text);
						}
				break; }
					}
				}
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    public class {1} : {2} {{\n{0}", _Indent, Exception.Id, Exception.BaseClass);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Construct instance for exception {1}\n{0}", _Indent, Exception.Console.Quoted());
			_Output.Write ("        /// </summary>		\n{0}", _Indent);
			_Output.Write ("		public {1} () : base ({2}) {{\n{0}", _Indent, Exception.Id, Exception.Console.Quoted());
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("        \n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Construct instance for exception {1}\n{0}", _Indent, Exception.Console.Quoted());
			_Output.Write ("        /// </summary>		\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Description\">Description of the error</param>	\n{0}", _Indent);
			_Output.Write ("		public {1} (string Description) : base (Description) {{\n{0}", _Indent, Exception.Id);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Construct instance for exception ", _Indent);
			_Output.Write ("		/// containing an inner exception.\n{0}", _Indent);
			_Output.Write ("        /// </summary>		\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Description\">Description of the error</param>	\n{0}", _Indent);
			_Output.Write ("		/// <param name=\"Inner\">Inner Exception</param>	\n{0}", _Indent);
			_Output.Write ("		public {1} (string Description, System.Exception Inner) : \n{0}", _Indent, Exception.Id);
			_Output.Write ("				base (Description, Inner) {{\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (Exception.Base) ) {
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				_Output.Write ("        /// User data associated with the exception.\n{0}", _Indent);
				_Output.Write ("        /// </summary>	\n{0}", _Indent);
				_Output.Write ("		public object UserData;\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			foreach  (var Object in Exception.Objects) {
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Construct instance for exception using a userdata parameter of\n{0}", _Indent);
				_Output.Write ("		/// type {1} and the format string {2}\n{0}", _Indent, Object.Type, Object.Text.Quoted());
				_Output.Write ("        /// </summary>		\n{0}", _Indent);
				_Output.Write ("        /// <param name=\"Object\">User data</param>	\n{0}", _Indent);
				_Output.Write ("		public {1} ({2} Object) : \n{0}", _Indent, Exception.Id, Object.Type);
				_Output.Write ("				base (global::System.String.Format ({1}", _Indent, Object.Text.Quoted());
				foreach  (var Parameter in Object.Parameters) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("					Object.{1}", _Indent, Parameter.Name);
					}
				_Output.Write ("					)) {{\n{0}", _Indent);
				_Output.Write ("			UserData = Object;\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Construct instance for exception using a userdata parameter of\n{0}", _Indent);
				_Output.Write ("		/// type {1} and the format string {2}\n{0}", _Indent, Object.Type, Object.Text.Quoted());
				_Output.Write ("        /// </summary>		\n{0}", _Indent);
				_Output.Write ("        /// <param name=\"Object\">User data</param>	\n{0}", _Indent);
				_Output.Write ("		/// <param name=\"Inner\">Inner Exception</param>	\n{0}", _Indent);
				_Output.Write ("		public {1} ({2} Object, System.Exception Inner) : \n{0}", _Indent, Exception.Id, Object.Type);
				_Output.Write ("				base (global::System.String.Format ({1}", _Indent, Object.Text.Quoted());
				foreach  (var Parameter in Object.Parameters) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("					Object.{1}", _Indent, Parameter.Name);
					}
				_Output.Write ("					), Inner) {{\n{0}", _Indent);
				_Output.Write ("			UserData = Object;\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// The public fatory delegate\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        public static {1}global::Goedel.Utilities.ThrowDelegate Throw = _Throw;\n{0}", _Indent, Exception.Base.If("", "new "));
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        static System.Exception _Throw(object Reason) {{\n{0}", _Indent);
			_Output.Write ("			if (Reason as string != null) {{\n{0}", _Indent);
			_Output.Write ("				return new {1}(Reason as string);\n{0}", _Indent, Exception.Id);
			_Output.Write ("				}}\n{0}", _Indent);
			foreach  (var Object in Exception.Objects) {
				_Output.Write ("			else if (Reason as {1} != null) {{\n{0}", _Indent, Object.Type);
				_Output.Write ("				return new {1}(Reason as {2});\n{0}", _Indent, Exception.Id, Object.Type);
				_Output.Write ("				}}\n{0}", _Indent);
				}
			_Output.Write ("			else {{\n{0}", _Indent);
			_Output.Write ("				return new {1}();\n{0}", _Indent, Exception.Id);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("        }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			 WriteListExceptions (Exception.Options);
			}
		

		//
		// WriteListExceptions
		//
		public void WriteListExceptions (List<_Choice> Exceptions) {
			foreach  (_Choice Exception in Exceptions) {
				if (  (Exception as Exception != null) ) {
					 WriteException (Exception as Exception );
					}
				}
			}
		}
	}
