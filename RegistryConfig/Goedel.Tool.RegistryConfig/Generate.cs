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
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.RegistryConfig;
public partial class GenerateCS : global::Goedel.Registry.Script {

	 DateTime GenerateTime = DateTime.UtcNow;
	

	//
	// Generate
	//
	public void Generate (ConfigItems ConfigItems) {
		 ConfigItems.Normalize();
		_Output.Write ("// Generated on {1}\n{0}", _Indent, GenerateTime);
		_Output.Write ("using System;\n{0}", _Indent);
		_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
		_Output.Write ("using System.IO;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Entry in ConfigItems.Top) {
			switch (Entry._Tag ()) {
				case ConfigItemsType.Class: {
				  Class Class = (Class) Entry; 
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("namespace {1}  {{\n{0}", _Indent, Class.Namespace);
				_Output.Write ("	\n{0}", _Indent);
				_Output.Write ("    /// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Convenience class creating accessors for registry 'Class.Id'\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public partial class {1} : Goedel.Mesh.ConfigRegistry {{\n{0}", _Indent, Class.Id);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Construct a new empty instance.\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("		public {1} () : base (\"{2}\") {{\n{0}", _Indent, Class.Id, Class.Id);
				_Output.Write ("			}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Construct a new instance from the specified file\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("        /// <param name=\"FileName\">The file to read</param>/// \n{0}", _Indent);
				_Output.Write ("		public {1} (string FileName) : base (\"{2}\", FileName) {{\n{0}", _Indent, Class.Id, Class.Id);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var Field in Class.Fields) {
					_Output.Write ("		\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Accessor for key \"Field.Key\" of type {1}\n{0}", _Indent, Field.RegistryType);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("		public virtual {1} {2} {{\n{0}", _Indent, Field.CType, Field.Id);
					_Output.Write ("			get {{ return Get{1} (\"{2}\"); }}\n{0}", _Indent, Field.RegistryType, Field.Key);
					_Output.Write ("			set {{ Set (\"{1}\", value); }}\n{0}", _Indent, Field.Key);
					_Output.Write ("			}}\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("	}}\n{0}", _Indent);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		}

	}
