// Script Syntax Version:  1.0

//  © 2015-2019 by Phill Hallam-Baker
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
using  Goedel.Utilities;
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Exceptional {
	public partial class Generate : global::Goedel.Registry.Script {

		

		//
		// GenerateCS
		//
		public void GenerateCS (Exceptions Exceptions) {
			// Goedel.Registry.Script.Header (_Output, "//", GenerateTime);
			// Goedel.Registry.Script.MITLicense (_Output, "//", 
			//     Goedel.Registry.Script.AssemblyCopyright,
			//     Goedel.Registry.Script.AssemblyCompany);
			 Registry.Boilerplate.Header(_Output, "//  ", DateTime.Now);
			_Output.Write ("\n{0}", _Indent);
			 GenerateCSX (Exceptions);
			}
		

		//
		// GenerateCSX
		//
		public void GenerateCSX (Exceptions Exceptions) {
			 Exceptions._InitChildren ();
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("//using System;\n{0}", _Indent);
			_Output.Write ("//using Goedel.Utilities;\n{0}", _Indent);
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
					_Output.Write ("#pragma warning disable IDE1006 // Naming Styles\n{0}", _Indent);
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Namespace.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
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
			_Output.Write ("    [global::System.Serializable]\n{0}", _Indent);
			_Output.Write ("	public partial class {1} : {2} {{\n{0}", _Indent, Exception.Id, Exception.BaseClass);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        ///<summary>The exception formatting delegate. May be overriden \n{0}", _Indent);
			_Output.Write ("		///locally or globally to implement different exception formatting.</summary>\n{0}", _Indent);
			_Output.Write ("		public static new global::Goedel.Utilities.ExceptionFormatDelegate ExceptionFormatDelegate {{ get; set; }} =\n{0}", _Indent);
			_Output.Write ("				global::Goedel.Utilities.GoedelException.ExceptionFormatDelegate;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		///<summary>Templates for formatting response messages.</summary>\n{0}", _Indent);
			_Output.Write ("		public static new System.Collections.Generic.List<string> Templates = \n{0}", _Indent);
			_Output.Write ("				new System.Collections.Generic.List<string> {{\n{0}", _Indent);
			 var ConsoleSep = new Separator ("", ",");
			foreach  (var console in Exception.Consoles) {
				_Output.Write ("{1}\n{0}", _Indent, ConsoleSep);
				_Output.Write ("				\"{1}\"\n{0}", _Indent, console.Message.CEscape());
				}
			_Output.Write ("				}};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("		/// Construct instance for exception\n{0}", _Indent);
			_Output.Write ("		/// </summary>		\n{0}", _Indent);
			_Output.Write ("		/// <param name=\"description\">Description of the error, may be used to override the \n{0}", _Indent);
			_Output.Write ("		/// generated message.</param>	\n{0}", _Indent);
			_Output.Write ("		/// <param name=\"inner\">Inner Exception</param>	\n{0}", _Indent);
			_Output.Write ("		/// <param name=\"args\">Optional list of parameterized arguments.</param>\n{0}", _Indent);
			_Output.Write ("		public {1}  (string description=null, System.Exception inner=null,\n{0}", _Indent, Exception.Id);
			_Output.Write ("			params object[] args) : \n{0}", _Indent);
			_Output.Write ("				base (ExceptionFormatDelegate(description, Templates,\n{0}", _Indent);
			_Output.Write ("					null, args), inner) {{\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// The public fatory delegate\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// public static {1}global::Goedel.Utilities.ThrowNewDelegate ThrowNew = _Throw;\n{0}", _Indent, Exception.Base.If("", "new "));
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        static System.Exception _Throw(object reasons) => new {1}(args:reasons) ;\n{0}", _Indent, Exception.Id);
			_Output.Write ("		\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// The public fatory delegate\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        public static {1}global::Goedel.Utilities.ThrowDelegate Throw = _Throw;\n{0}", _Indent, Exception.Base.If("", "new "));
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
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
		

		//
		// SummarizeException
		//
		public void SummarizeException (Exception Exception) {
			_Output.Write (",\n{0}", _Indent);
			_Output.Write ("				{1}.ThrowNew", _Indent, Exception.Id);
			 SummarizeListExceptions (Exception.Options);
			}
		

		//
		// SummarizeListExceptions
		//
		public void SummarizeListExceptions (List<_Choice> Exceptions) {
			foreach  (_Choice Exception in Exceptions) {
				if (  (Exception as Exception != null) ) {
					 SummarizeException (Exception as Exception );
					}
				}
			}
		}
	}
