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
// #pclass MakeWidgets GenerateGTK 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace MakeWidgets {
	public partial class GenerateGTK : global::Goedel.Registry.Script {
		public GenerateGTK () : base () {
			}
		public GenerateGTK (TextWriter Output) : base (Output) {
			}

		//  
		// #% System.DateTime GenerateTime = System.DateTime.UtcNow; 
		 System.DateTime GenerateTime = System.DateTime.UtcNow;
		// #method GenerateCS string Title 
		

		//
		// GenerateCS
		//
		public void GenerateCS (string Title) {
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #% string Namespace = "Goedel.Trojan.GTK"; 
			 string Namespace = "Goedel.Trojan.GTK";
			// #% List<string> Classes = new List<string> { 
			 List<string> Classes = new List<string> {
			// #%   "FormWidgetSet"} 	; 
			   "FormWidgetSet"} 	;
			//  
			_Output.Write ("\n{0}", _Indent);
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// namespace #{Namespace} { 
			_Output.Write ("namespace {1} {{\n{0}", _Indent, Namespace);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Class in Classes)  
			foreach  (var Class in Classes)  {
				//  
				_Output.Write ("\n{0}", _Indent);
				//     public partial class #{Class} { 
				_Output.Write ("    public partial class {1} {{\n{0}", _Indent, Class);
				//         int Row; 
				_Output.Write ("        int Row;\n{0}", _Indent);
				//         GridForm GridForm; 
				_Output.Write ("        GridForm GridForm;\n{0}", _Indent);
				//         ErrorLabel ErrorLabel = null; 
				_Output.Write ("        ErrorLabel ErrorLabel = null;\n{0}", _Indent);
				//         bool _Changed = false; 
				_Output.Write ("        bool _Changed = false;\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// The object field that this object maps to. 
				_Output.Write ("        /// The object field that this object maps to.\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public ObjectField ObjectField { get { return ObjectEntry;  }  } 
				_Output.Write ("        public ObjectField ObjectField {{ get {{ return ObjectEntry;  }}  }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// Destruction method, called when the widget is being destroyed. 
				_Output.Write ("        /// Destruction method, called when the widget is being destroyed.\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public override void Destroy  () { 
				_Output.Write ("        public override void Destroy  () {{\n{0}", _Indent);
				//             base.Destroy(); 
				_Output.Write ("            base.Destroy();\n{0}", _Indent);
				//             ObjectField.Destroy(this); 
				_Output.Write ("            ObjectField.Destroy(this);\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// Error text to report 
				_Output.Write ("        /// Error text to report\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public string ReasonInvalid { set { 
				_Output.Write ("        public string ReasonInvalid {{ set {{\n{0}", _Indent);
				//                 ErrorLabel.Raise(ref ErrorLabel, GridForm, Row, value); 
				_Output.Write ("                ErrorLabel.Raise(ref ErrorLabel, GridForm, Row, value);\n{0}", _Indent);
				//                 } } 
				_Output.Write ("                }} }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// When set true, user can modify contents, otherwise value is fixed. 
				_Output.Write ("        /// When set true, user can modify contents, otherwise value is fixed.\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public bool ReadOnly { set { Sensitive = !value; } } 
				_Output.Write ("        public bool ReadOnly {{ set {{ Sensitive = !value; }} }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// Popup tool tip 
				_Output.Write ("        /// Popup tool tip\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public string Tip { set { TooltipText = value; } } 
				_Output.Write ("        public string Tip {{ set {{ TooltipText = value; }} }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// Copy values from widget to model value field. 
				_Output.Write ("        /// Copy values from widget to model value field.\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public void Apply() { 
				_Output.Write ("        public void Apply() {{\n{0}", _Indent);
				//             ObjectEntry.Apply(); 
				_Output.Write ("            ObjectEntry.Apply();\n{0}", _Indent);
				//             _Changed = false; 
				_Output.Write ("            _Changed = false;\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				//         /// Set to true if the user has changed the value. 
				_Output.Write ("        /// Set to true if the user has changed the value.\n{0}", _Indent);
				//         /// </summary> 
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				//         public bool UserChangedValue { get { return _Changed; } 
				_Output.Write ("        public bool UserChangedValue {{ get {{ return _Changed; }}\n{0}", _Indent);
				//             set { _Changed = value; } } 
				_Output.Write ("            set {{ _Changed = value; }} }}\n{0}", _Indent);
				//     } 
				_Output.Write ("    }}\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// } 
			_Output.Write ("}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		// #end pclass 
		}
	}
