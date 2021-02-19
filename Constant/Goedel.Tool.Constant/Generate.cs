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
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Constant {
	public partial class Generate : global::Goedel.Registry.Script {

		

		//
		// GenerateCS
		//
		public void GenerateCS (Constant Constant) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using System.Runtime.CompilerServices;\n{0}", _Indent);
			_Output.Write ("using System.Text;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("namespace {1} {{\n{0}", _Indent, Constant.NameSpaceName);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var item in Constant.Enums) {
				_Output.Write ("    ///<summary>{1}</summary>\n{0}", _Indent, item.Title);
				_Output.Write ("    public enum {1} {{\n{0}", _Indent, item.Id.Label);
				_Output.Write ("        ///<summary>Undefined type</summary>\n{0}", _Indent);
				_Output.Write ("        Unknown = -1", _Indent);
				foreach  (var entry in item.Integer) {
					if (  (entry.Reserve.End == 0) ) {
						_Output.Write (",\n{0}", _Indent);
						_Output.Write ("        ///<summary>{1}</summary>\n{0}", _Indent, entry.Title);
						_Output.Write ("        {1} = {2}", _Indent, entry.Id.Label, entry.Value);
						}
					}
				foreach  (var entry in item.UDF) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("        ///<summary>{1}</summary>\n{0}", _Indent, entry.Title);
					_Output.Write ("        {1} = {2}", _Indent, entry.Id, entry.Value);
					}
				foreach  (var entry in item.Code) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("        ///<summary>{1}</summary>\n{0}", _Indent, entry.Title);
					_Output.Write ("        {1}", _Indent, entry.Id.Label);
					}
				_Output.Write ("        }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<summary>\n{0}", _Indent);
			foreach  (var line in Constant.Namespace.Text) {
				_Output.Write ("    ///{1}\n{0}", _Indent, line);
				}
			_Output.Write ("    ///</summary>\n{0}", _Indent);
			_Output.Write ("    public static partial class {1} {{\n{0}", _Indent, Constant.Class);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var file in Constant.Files) {
				_Output.Write ("        // File: {1}\n{0}", _Indent, file.Id.Label);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var entry in file.Entries) {
					switch (entry._Tag ()) {
						case ConstantType.Code: {
						  Code code = (Code) entry; 
						_Output.Write ("        ///<summary>{1}</summary>\n{0}", _Indent, code.Title);
						_Output.Write ("        public const string {1} = \"{2}\";\n{0}", _Indent, code.Id.Label, code.Id.Label);
						_Output.Write ("\n{0}", _Indent);
						break; }
						case ConstantType.String: {
						  String String = (String) entry; 
						_Output.Write ("        ///<summary>\n{0}", _Indent);
						foreach  (var line in String.Description.Text) {
							_Output.Write ("        ///{1}\n{0}", _Indent, line);
							}
						_Output.Write ("        ///</summary>\n{0}", _Indent);
						_Output.Write ("        public const string {1} = \"{2}\";\n{0}", _Indent, String.Id.Label, String.Value);
						_Output.Write ("\n{0}", _Indent);
						break; }
						case ConstantType.Enum: {
						  Enum item = (Enum) entry; 
						_Output.Write ("\n{0}", _Indent);
						foreach  (var code in item.Code) {
							_Output.Write ("        ///<summary>Jose enumeration tag for {1}.{2}</summary>\n{0}", _Indent, item.Id.Label, code.Id.Label);
							_Output.Write ("        public const string  {1}{2}Tag = \"{3}\";\n{0}", _Indent, item.Id.Label, code.Id.Label, code.Id.Label);
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("        /// <summary>\n{0}", _Indent);
						_Output.Write ("        /// Convert the string <paramref name=\"text\"/> to the corresponding enumeration\n{0}", _Indent);
						_Output.Write ("        /// value.\n{0}", _Indent);
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						_Output.Write ("        /// <param name=\"text\">The string to convert.</param>\n{0}", _Indent);
						_Output.Write ("        /// <returns>The enumeration value.</returns>\n{0}", _Indent);
						_Output.Write ("        public static {1} To{2} (this string text) =>\n{0}", _Indent, item.Id.Label, item.Id.Label);
						_Output.Write ("            text switch {{\n{0}", _Indent);
						foreach  (var code in item.Code) {
							_Output.Write ("                {1}{2}Tag => {3}.{4},\n{0}", _Indent, item.Id.Label, code.Id.Label, item.Id.Label, code.Id.Label);
							}
						_Output.Write ("                _ => {1}.Unknown\n{0}", _Indent, item.Id.Label);
						_Output.Write ("                }};\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("        /// <summary>\n{0}", _Indent);
						_Output.Write ("        /// Convert the enumerated value <paramref name=\"data\"/> to the corresponding string\n{0}", _Indent);
						_Output.Write ("        /// value.\n{0}", _Indent);
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						_Output.Write ("        /// <param name=\"data\">The enumerated value.</param>\n{0}", _Indent);
						_Output.Write ("        /// <returns>The text value.</returns>\n{0}", _Indent);
						_Output.Write ("        public static string ToLabel (this {1} data) =>\n{0}", _Indent, item.Id.Label);
						_Output.Write ("            data switch {{\n{0}", _Indent);
						foreach  (var code in item.Code) {
							_Output.Write ("                {1}.{2} => {3}{4}Tag,\n{0}", _Indent, item.Id.Label, code.Id.Label, item.Id.Label, code.Id.Label);
							}
						_Output.Write ("                _ => null\n{0}", _Indent);
						_Output.Write ("                }};\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						break; }
						case ConstantType.Function: {
						  Function function = (Function) entry; 
						_Output.Write ("        /// <summary>\n{0}", _Indent);
						if (  (function.Description != null) ) {
							foreach  (var line in function.Description.Text) {
								_Output.Write ("        /// {1}\n{0}", _Indent, line);
								}
							}
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						foreach  (var parameter in function.Parameter) {
							_Output.Write ("        /// <param name=\"in{1}\">{2}</param>\n{0}", _Indent, parameter.Id, parameter.Title);
							}
						_Output.Write ("        /// <returns></returns>\n{0}", _Indent);
						_Output.Write ("        public static string KeyDerivationKeyName (", _Indent);
						
						 var separator = new Separator (",");
						foreach  (var parameter in function.Parameter) {
							_Output.Write ("{1}\n{0}", _Indent, separator);
							_Output.Write ("                    {1} in{2}", _Indent, parameter.Type, parameter.Id);
							}
						_Output.Write (") {{\n{0}", _Indent);
						foreach  (var parameter in function.Parameter) {
							_Output.Write ("            var {1} = in{2}{3}; \n{0}", _Indent, parameter.Id, parameter.Id, parameter.Convert?.Type.If());
							}
						_Output.Write ("\n{0}", _Indent);
						foreach  (var formula in function.Formula) {
							_Output.Write ("            var ", _Indent);
							foreach  (var line in formula.Text) {
								_Output.Write ("{1} ", _Indent, line);
								}
							_Output.Write (";\n{0}", _Indent);
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("            return {1};\n{0}", _Indent, function.Return.Label);
						_Output.Write ("            }}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
					break; }
						}
					}
				}
			_Output.Write ("        }}\n{0}", _Indent);
			_Output.Write ("    }}\n{0}", _Indent);
			}
		}
	}
