// Script Syntax Version:  1.0

//  © 2015-2021 by Threshold Secrets LLC.
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
namespace Goedel.Tool.Exceptional;
public partial class Generate : global::Goedel.Registry.Script {

	 static bool DirectGeneration = true;
	
	/// <summary>	
	/// GenerateCS
	/// </summary>
	/// <param name="Exceptions"></param>
	public void GenerateCS (Exceptions Exceptions) {
		// Goedel.Registry.Script.Header (_Output, "//", GenerateTime);
		// Goedel.Registry.Script.MITLicense (_Output, "//", 
		//     Goedel.Registry.Script.AssemblyCopyright,
		//     Goedel.Registry.Script.AssemblyCompany);
		 Registry.Boilerplate.Header(_Output, "//  ", DateTime.Now);
		_Output.Write ("\n{0}", _Indent);
		 GenerateCSX (Exceptions);
		}
	
	/// <summary>	
	/// GenerateCSX
	/// </summary>
	/// <param name="Exceptions"></param>
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
				_Output.Write ("#pragma warning disable IDE0079\n{0}", _Indent);
				_Output.Write ("#pragma warning disable IDE1006 // Naming Styles\n{0}", _Indent);
				_Output.Write ("namespace {1} ;\n{0}", _Indent, Namespace.Id);
				_Output.Write ("\n{0}", _Indent);
				
				 WriteListExceptions (Namespace.Options);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("/// <summary>\n{0}", _Indent);
				_Output.Write ("/// Extensions class defining logging events and convenience methods.\n{0}", _Indent);
				_Output.Write ("/// </summary>\n{0}", _Indent);
				_Output.Write ("public  static partial class EventExtensions {{\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				if (  (DirectGeneration) ) {
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					_Output.Write ("    /// Static initializer, is called once when the module loads.\n{0}", _Indent);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("    static EventExtensions() {{\n{0}", _Indent);
					foreach  (_Choice Entry in Namespace.Options) {
						if (  (Entry is IEvent logEvent) ) {
							 WriteEventInit (logEvent);
							}
						}
					_Output.Write ("        }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (_Choice Entry in Namespace.Options) {
						if (  (Entry is IEvent logEvent) ) {
							 WriteEvent (logEvent);
							}
						}
					} else {
					_Output.Write ("\n{0}", _Indent);
					foreach  (_Choice Entry in Namespace.Options) {
						if (  (Entry is IEvent logEvent) ) {
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("	/// <summary>\n{0}", _Indent);
							_Output.Write ("    /// Write an event of type {1} to <paramref name=\"logger\"/> \n{0}", _Indent, logEvent.Name);
							_Output.Write ("    /// </summary>\n{0}", _Indent);
							_Output.Write ("    /// <param name=\"logger\">The logger to write the output to.</param>\n{0}", _Indent);
							foreach  (var param in logEvent.TypedParameters) {
								_Output.Write ("	/// <param name=\"{1}\">{2}</param>\n{0}", _Indent, param.Name, param.Text);
								}
							_Output.Write ("	/// <param name=\"_exception\">Exception (if thrown)</param>\n{0}", _Indent);
							_Output.Write ("    [LoggerMessage(\n{0}", _Indent);
							_Output.Write ("        EventId = {1},\n{0}", _Indent, logEvent.EventId);
							_Output.Write ("		Level = LogLevel.{1},\n{0}", _Indent, logEvent.LogLevel);
							_Output.Write ("        Message = \"{1}\")]\n{0}", _Indent, logEvent.Pattern);
							_Output.Write ("    public static partial void {1}(\n{0}", _Indent, logEvent.Name);
							_Output.Write ("        ILogger logger", _Indent);
							foreach  (var param in logEvent.TypedParameters) {
								_Output.Write (",\n{0}", _Indent);
								_Output.Write ("			{1} {2}", _Indent, param.Type, param.Name);
								}
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("			Exception _exception=null);\n{0}", _Indent);
							}
						}
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("	}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// WriteEventInit
	/// </summary>
	/// <param name="logEvent"></param>
	public void WriteEventInit (IEvent logEvent) {
		_Output.Write ("        _{1} = LoggerMessage.Define", _Indent, logEvent.Name);
		if (  (!logEvent.IsEmpty) ) {
			 var separator = new Separator (",");
			_Output.Write ("<", _Indent);
			foreach  (var param in logEvent.TypedParameters) {
				_Output.Write ("{1}{2}", _Indent, separator.ToString(), param.Type);
				}
			_Output.Write (">", _Indent);
			}
		_Output.Write ("(\n{0}", _Indent);
		_Output.Write ("            LogLevel.{1}, new EventId({2}, nameof(_{3})),\n{0}", _Indent, logEvent.LogLevel, logEvent.EventId, logEvent.Name);
		_Output.Write ("            \"{1}\");\n{0}", _Indent, logEvent.Pattern);
		}
	
	/// <summary>	
	/// WriteEvent
	/// </summary>
	/// <param name="logEvent"></param>
	public void WriteEvent (IEvent logEvent) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    private static readonly Action<ILogger", _Indent);
		foreach  (var param in logEvent.TypedParameters) {
			_Output.Write (", {1}", _Indent, param.Type);
			}
		_Output.Write (", Exception> _{1};\n{0}", _Indent, logEvent.Name);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("    /// Write an event of type {1} to <paramref name=\"logger\"/> \n{0}", _Indent, logEvent.Name);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("    /// <param name=\"logger\">The logger to write the output to.</param>\n{0}", _Indent);
		foreach  (var param in logEvent.TypedParameters) {
			_Output.Write ("	/// <param name=\"{1}\">{2}</param>\n{0}", _Indent, param.Name, param.Text);
			}
		_Output.Write ("	/// <param name=\"_exception\">Exception (if thrown)</param>\n{0}", _Indent);
		_Output.Write ("    public static void {1}(\n{0}", _Indent, logEvent.Name);
		_Output.Write ("			this ILogger logger", _Indent);
		foreach  (var param in logEvent.TypedParameters) {
			_Output.Write (",\n{0}", _Indent);
			_Output.Write ("			{1} {2}", _Indent, param.Type, param.Name);
			}
		_Output.Write (",\n{0}", _Indent);
		_Output.Write ("			Exception _exception=null) {{\n{0}", _Indent);
		_Output.Write ("        _{1}(logger", _Indent, logEvent.Name);
		foreach  (var param in logEvent.TypedParameters) {
			_Output.Write (", {1}", _Indent, param.Name);
			}
		_Output.Write (", _exception);\n{0}", _Indent);
		_Output.Write ("        }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// WriteException
	/// </summary>
	/// <param name="Exception"></param>
	public void WriteException (Exception Exception) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("/// <summary>\n{0}", _Indent);
		foreach  (_Choice Optionlevel in Exception.Options) {
			switch (Optionlevel._Tag ()) {
				case ExceptionsType.Description: {
				  Description Description = (Description) Optionlevel; 
				foreach  (String Text in Description.Text) {
					_Output.Write ("/// {1}\n{0}", _Indent, Text);
					}
			break; }
				}
			}
		_Output.Write ("/// </summary>\n{0}", _Indent);
		_Output.Write ("[global::System.Serializable]\n{0}", _Indent);
		_Output.Write ("public partial class {1} : {2} {{\n{0}", _Indent, Exception.Id, Exception.BaseClass);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>The exception formatting delegate. May be overriden \n{0}", _Indent);
		_Output.Write ("	///locally or globally to implement different exception formatting.</summary>\n{0}", _Indent);
		_Output.Write ("	public static new global::Goedel.Utilities.ExceptionFormatDelegate ExceptionFormatDelegate {{ get; set; }} =\n{0}", _Indent);
		_Output.Write ("			global::Goedel.Utilities.GoedelException.ExceptionFormatDelegate;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<summary>Templates for formatting response messages.</summary>\n{0}", _Indent);
		_Output.Write ("	public static new System.Collections.Generic.List<string> Templates {{get; set;}} = \n{0}", _Indent);
		_Output.Write ("			new () {{\n{0}", _Indent);
		 var ConsoleSep = new Separator ("", ",");
		foreach  (var console in Exception.Consoles) {
			_Output.Write ("{1}\n{0}", _Indent, ConsoleSep);
			_Output.Write ("			\"{1}\"\n{0}", _Indent, console.Message.CEscape());
			}
		_Output.Write ("			}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("	/// Construct instance for exception\n{0}", _Indent);
		_Output.Write ("	/// </summary>		\n{0}", _Indent);
		_Output.Write ("	/// <param name=\"description\">Description of the error, may be used to override the \n{0}", _Indent);
		_Output.Write ("	/// generated message.</param>	\n{0}", _Indent);
		_Output.Write ("	/// <param name=\"inner\">Inner Exception</param>	\n{0}", _Indent);
		_Output.Write ("	/// <param name=\"args\">Optional list of parameterized arguments.</param>\n{0}", _Indent);
		_Output.Write ("	public {1}  (string description=null, System.Exception inner=null,\n{0}", _Indent, Exception.Id);
		_Output.Write ("		params object[] args) : \n{0}", _Indent);
		_Output.Write ("			base (ExceptionFormatDelegate(description, Templates,\n{0}", _Indent);
		_Output.Write ("				null, args), inner) {{\n{0}", _Indent);
		_Output.Write ("		}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("    /// The public fatory delegate\n{0}", _Indent);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("    /// public static {1}global::Goedel.Utilities.ThrowNewDelegate ThrowNew = _Throw;\n{0}", _Indent, Exception.Base.If("", "new "));
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    static System.Exception _Throw(object reasons) => new {1}(args:reasons) ;\n{0}", _Indent, Exception.Id);
		_Output.Write ("		\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("    /// The public fatory delegate\n{0}", _Indent);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("    public static {1}global::Goedel.Utilities.ThrowDelegate Throw {{get;}} = _Throw;\n{0}", _Indent, Exception.Base.If("", "new "));
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		 WriteListExceptions (Exception.Options);
		}
	
	/// <summary>	
	/// WriteListExceptions
	/// </summary>
	/// <param name="Exceptions"></param>
	public void WriteListExceptions (List<_Choice> Exceptions) {
		foreach  (_Choice Exception in Exceptions) {
			if (  (Exception as Exception != null) ) {
				 WriteException (Exception as Exception );
				}
			}
		}
	
	/// <summary>	
	/// SummarizeException
	/// </summary>
	/// <param name="Exception"></param>
	public void SummarizeException (Exception Exception) {
		_Output.Write (",\n{0}", _Indent);
		_Output.Write ("			{1}.ThrowNew", _Indent, Exception.Id);
		 SummarizeListExceptions (Exception.Options);
		}
	
	/// <summary>	
	/// SummarizeListExceptions
	/// </summary>
	/// <param name="Exceptions"></param>
	public void SummarizeListExceptions (List<_Choice> Exceptions) {
		foreach  (_Choice Exception in Exceptions) {
			if (  (Exception as Exception != null) ) {
				 SummarizeException (Exception as Exception );
				}
			}
		}

	}
