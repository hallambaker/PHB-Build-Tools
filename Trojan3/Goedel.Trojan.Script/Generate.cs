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
// #using System.Linq 
using  System.Linq;
// #pclass Goedel.Trojan.Script GenerateGTK 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Trojan.Script {
	public partial class GenerateGTK : global::Goedel.Registry.Script {
		public GenerateGTK () : base () {
			}
		public GenerateGTK (TextWriter Output) : base (Output) {
			}

		// #! 
		//
		// #% System.DateTime GenerateTime = System.DateTime.UtcNow; 
		 System.DateTime GenerateTime = System.DateTime.UtcNow;
		// #% string GoedelNamespace = "Goedel.Trojan"; 
		 string GoedelNamespace = "Goedel.Trojan";
		// #% string TitleField = "Title"; 
		 string TitleField = "Title";
		// #% Separator Separator; 
		 Separator Separator;
		// #method GenerateCS GUISchema GUISchema 
		

		//
		// GenerateCS
		//
		public void GenerateCS (GUISchema GUISchema) {
			// #% GUISchema._InitChildren (); 
			 GUISchema._InitChildren ();
			//  
			_Output.Write ("\n{0}", _Indent);
			// //This file was generated automatically. 
			_Output.Write ("//This file was generated automatically.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using Goedel.Registry; 
			_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
			// using Goedel.Trojan; 
			_Output.Write ("using Goedel.Trojan;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var GUI in GUISchema.GUIs) 
			foreach  (var GUI in GUISchema.GUIs) {
				//  
				_Output.Write ("\n{0}", _Indent);
				// namespace #{GUI.Namespace} { 
				_Output.Write ("namespace {1} {{\n{0}", _Indent, GUI.Namespace);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 	// Make extensible partial classes for all the toplevel classes 
				_Output.Write ("	// Make extensible partial classes for all the toplevel classes\n{0}", _Indent);
				// 	// This allows an implementation to decorate any class at will. 
				_Output.Write ("	// This allows an implementation to decorate any class at will.\n{0}", _Indent);
				// 	public abstract partial class Object:  #{GoedelNamespace}.Object { 
				_Output.Write ("	public abstract partial class Object:  {1}.Object {{\n{0}", _Indent, GoedelNamespace);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				// 	public abstract partial class Menu:  #{GoedelNamespace}.Menu { 
				_Output.Write ("	public abstract partial class Menu:  {1}.Menu {{\n{0}", _Indent, GoedelNamespace);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				// 	public abstract partial class Window:  #{GoedelNamespace}.Window { 
				_Output.Write ("	public abstract partial class Window:  {1}.Window {{\n{0}", _Indent, GoedelNamespace);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				// 	public abstract partial class Wizard:  #{GoedelNamespace}.Wizard { 
				_Output.Write ("	public abstract partial class Wizard:  {1}.Wizard {{\n{0}", _Indent, GoedelNamespace);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				// 	public abstract partial class Model:  #{GoedelNamespace}.Model { 
				_Output.Write ("	public abstract partial class Model:  {1}.Model {{\n{0}", _Indent, GoedelNamespace);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 	//The main declarations 
				_Output.Write ("	//The main declarations\n{0}", _Indent);
				// 	public partial class #{GUI.Id} : _#{GUI.Id} { 
				_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, GUI.Id, GUI.Id);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 	public partial class _#{GUI.Id} : Model { 
				_Output.Write ("	public partial class _{1} : Model {{\n{0}", _Indent, GUI.Id);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         public _#{GUI.Id}() { 
				_Output.Write ("        public _{1}() {{\n{0}", _Indent, GUI.Id);
				//             _About = new About(this) { 
				_Output.Write ("            _About = new About(this) {{\n{0}", _Indent);
				// #if GUI.About != null 
				if (  GUI.About != null ) {
					// 				Name = "#{GUI.About.Title}" 
					_Output.Write ("				Name = \"{1}\"\n{0}", _Indent, GUI.About.Title);
					// #end if 
					}
				// 				}; 
				_Output.Write ("				}};\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Command in GUI.Commands)  
				foreach  (var Command in GUI.Commands)  {
					// 		public virtual void #{Command.Id} (#! 
					_Output.Write ("		public virtual void {1} (", _Indent, Command.Id);
					// #if (Command.Parameter != null)  
					if (  (Command.Parameter != null)  ) {
						// #{Command.Parameter} #{Command.Parameter}#! 
						_Output.Write ("{1} {2}", _Indent, Command.Parameter, Command.Parameter);
						// #end if		 
						}
					// ) { 
					_Output.Write (") {{\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Object in GUI.Objects)  
				foreach  (var Object in GUI.Objects)  {
					// 		protected #{Object.Id} Selected_#{Object.Id} = null ; 
					_Output.Write ("		protected {1} Selected_{2} = null ;\n{0}", _Indent, Object.Id, Object.Id);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				//         public override void  Dispach(string Command) { 
				_Output.Write ("        public override void  Dispach(string Command) {{\n{0}", _Indent);
				//             switch (Command) { 
				_Output.Write ("            switch (Command) {{\n{0}", _Indent);
				// #foreach (var Command in GUI.Commands)  
				foreach  (var Command in GUI.Commands)  {
					// #if (Command.Parameter != null)  
					if (  (Command.Parameter != null)  ) {
						//                 case "#{Command.Id}_#{Command.Parameter}": { 
						_Output.Write ("                case \"{1}_{2}\": {{\n{0}", _Indent, Command.Id, Command.Parameter);
						//                         #{Command.Id}(Selected_#{Command.Parameter}); 
						_Output.Write ("                        {1}(Selected_{2});\n{0}", _Indent, Command.Id, Command.Parameter);
						// #else	 
						} else {
						//                 case "#{Command.Id}": { 
						_Output.Write ("                case \"{1}\": {{\n{0}", _Indent, Command.Id);
						//                         #{Command.Id}(); 
						_Output.Write ("                        {1}();\n{0}", _Indent, Command.Id);
						// #end if 
						}
					//                         return; 
					_Output.Write ("                        return;\n{0}", _Indent);
					//                         } 
					_Output.Write ("                        }}\n{0}", _Indent);
					// #end foreach 
					}
				//                 } 
				_Output.Write ("                }}\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         public bool Active(String Command) { 
				_Output.Write ("        public bool Active(String Command) {{\n{0}", _Indent);
				//             switch (Command) { 
				_Output.Write ("            switch (Command) {{\n{0}", _Indent);
				// #foreach (var Command in GUI.Commands)  
				foreach  (var Command in GUI.Commands)  {
					// #if (Command.Parameter != null)  
					if (  (Command.Parameter != null)  ) {
						//                 case "#{Command.Id}_#{Command.Parameter}": { 
						_Output.Write ("                case \"{1}_{2}\": {{\n{0}", _Indent, Command.Id, Command.Parameter);
						//                         return Selected_#{Command.Parameter}# != null; 
						_Output.Write ("                        return Selected_{1} != null;\n{0}", _Indent, Command.Parameter);
						//                         } 
						_Output.Write ("                        }}\n{0}", _Indent);
						// #end if	 
						}
					// #end foreach 
					}
				//                 } 
				_Output.Write ("                }}\n{0}", _Indent);
				//             return true; 
				_Output.Write ("            return true;\n{0}", _Indent);
				//             } 
				_Output.Write ("            }}\n{0}", _Indent);
				// 		} 
				_Output.Write ("		}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// 	// Windows 
				_Output.Write ("	// Windows\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Window in GUI.Windows)  
				foreach  (var Window in GUI.Windows)  {
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public partial class #{Window.Id} : _#{Window.Id} { 
					_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, Window.Id, Window.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		string _Title = "#{Window.Tag}"; 
					_Output.Write ("		string _Title = \"{1}\";\n{0}", _Indent, Window.Tag);
					// 		public override string Title { 
					_Output.Write ("		public override string Title {{\n{0}", _Indent);
					// 			get {return _Title;} 
					_Output.Write ("			get {{return _Title;}}\n{0}", _Indent);
					// 			set {_Title = value;} 
					_Output.Write ("			set {{_Title = value;}}\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		public #{Window.Id}  (Model Model, Binding Binding) { 
					_Output.Write ("		public {1}  (Model Model, Binding Binding) {{\n{0}", _Indent, Window.Id);
					// 			// Call backing code to populate the data model 
					_Output.Write ("			// Call backing code to populate the data model\n{0}", _Indent);
					// 			Populate (); 
					_Output.Write ("			Populate ();\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			// Initialize the view and controller 
					_Output.Write ("			// Initialize the view and controller\n{0}", _Indent);
					// 			Initialize (Model, Binding); 
					_Output.Write ("			Initialize (Model, Binding);\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public abstract class _#{Window.Id} : Window { 
					_Output.Write ("	public abstract class _{1} : Window {{\n{0}", _Indent, Window.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #if (Window.Menu != null) 
					if (  (Window.Menu != null) ) {
						// 		Menu _Menu = new #{Window.Menu.Id} (); 
						_Output.Write ("		Menu _Menu = new {1} ();\n{0}", _Indent, Window.Menu.Id);
						//         public override #{GoedelNamespace}.Menu Menu { get { return _Menu; } } 
						_Output.Write ("        public override {1}.Menu Menu {{ get {{ return _Menu; }} }}\n{0}", _Indent, GoedelNamespace);
						// #end if		 
						}
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Menu in GUI.Menus)  
				foreach  (var Menu in GUI.Menus)  {
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public partial class #{Menu.Id} : _#{Menu.Id} { 
					_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, Menu.Id, Menu.Id);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public partial class _#{Menu.Id} : Menu { 
					_Output.Write ("	public partial class _{1} : Menu {{\n{0}", _Indent, Menu.Id);
					// 	 
					_Output.Write ("	\n{0}", _Indent);
					// 		public override List<MenuEntry> Entries { 
					_Output.Write ("		public override List<MenuEntry> Entries {{\n{0}", _Indent);
					//             get { return _Entries; } 
					_Output.Write ("            get {{ return _Entries; }}\n{0}", _Indent);
					//             set { _Entries = value; } 
					_Output.Write ("            set {{ _Entries = value; }}\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #call MakeMenu Menu 
					MakeMenu (Menu);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// 	// Wizards 
				_Output.Write ("	// Wizards\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Wizard in GUI.Wizards)  
				foreach  (var Wizard in GUI.Wizards)  {
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public partial class #{Wizard.Id} : _#{Wizard.Id} { 
					_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, Wizard.Id, Wizard.Id);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public partial class _#{Wizard.Id} : Wizard { 
					_Output.Write ("	public partial class _{1} : Wizard {{\n{0}", _Indent, Wizard.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         public override string Title => #{Wizard.Tag.Quoted()}; 
					_Output.Write ("        public override string Title => {1};\n{0}", _Indent, Wizard.Tag.Quoted());
					//         public override List<string> Texts => _Texts; 
					_Output.Write ("        public override List<string> Texts => _Texts;\n{0}", _Indent);
					//         public override List<Step> Steps => _Steps; 
					_Output.Write ("        public override List<Step> Steps => _Steps;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	// #{Wizard.Id} 
					_Output.Write ("	// {1}\n{0}", _Indent, Wizard.Id);
					// 		List<string> _Texts = #! 
					_Output.Write ("		List<string> _Texts = ", _Indent);
					// #call MakeTextList Wizard.Entries 
					MakeTextList (Wizard.Entries);
					// ; 
					_Output.Write (";\n{0}", _Indent);
					// 		List<Step> _Steps = new List<Step> {#! 
					_Output.Write ("		List<Step> _Steps = new List<Step> {{", _Indent);
					// #% Separator = new Separator (","); 
					 Separator = new Separator (",");
					// #foreach (var Entry in Wizard.Entries) 
					foreach  (var Entry in Wizard.Entries) {
						// #% var Step = Entry as Step; 
						 var Step = Entry as Step;
						// #if Step != null 
						if (  Step != null ) {
							// #{Separator} 
							_Output.Write ("{1}\n{0}", _Indent, Separator);
							// 			new Step () {Object = new #{Step.Id} (),  
							_Output.Write ("			new Step () {{Object = new {1} (), \n{0}", _Indent, Step.Id);
							// 				Title = #{Step.Tag.Quoted()}, Description = 
							_Output.Write ("				Title = {1}, Description =\n{0}", _Indent, Step.Tag.Quoted());
							// 		#! 
							_Output.Write ("		", _Indent);
							// #indent 
							_Indent = _Indent + "\t";
							// #indent 
							_Indent = _Indent + "\t";
							// #call MakeTextList Step.Entries 
							MakeTextList (Step.Entries);
							// #outdent 
							_Indent = _Indent.Remove (0,1);
							// #outdent 
							_Indent = _Indent.Remove (0,1);
							// }#! 
							_Output.Write ("}}", _Indent);
							// #end if 
							}
						// #end foreach 
						}
					// }; 
					_Output.Write ("}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end foreach 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				// 	// Objects 
				_Output.Write ("	// Objects\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #foreach (var Object in GUI.Objects)  
				foreach  (var Object in GUI.Objects)  {
					// 	public partial class #{Object.Id} : _#{Object.Id}{ 
					_Output.Write ("	public partial class {1} : _{2}{{\n{0}", _Indent, Object.Id, Object.Id);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public class _#{Object.Id} #! 
					_Output.Write ("	public class _{1} ", _Indent, Object.Id);
					// #if (Object.ParentObject == null) 
					if (  (Object.ParentObject == null) ) {
						// : Object #! 
						_Output.Write (": Object ", _Indent);
						// #else 
						} else {
						// : #{Object.ParentObject.Id} #! 
						_Output.Write (": {1} ", _Indent, Object.ParentObject.Id);
						// #end if	 
						}
					// { 
					_Output.Write ("{{\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #call DeclareFields Object.Entries 
					DeclareFields (Object.Entries);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		public override List<ObjectEntry> Entries { 
					_Output.Write ("		public override List<ObjectEntry> Entries {{\n{0}", _Indent);
					//             get { return _Entries; } 
					_Output.Write ("            get {{ return _Entries; }}\n{0}", _Indent);
					//             set { _Entries = value; } 
					_Output.Write ("            set {{ _Entries = value; }}\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		List<ObjectEntry> _Entries = new List<ObjectEntry> { 
					_Output.Write ("		List<ObjectEntry> _Entries = new List<ObjectEntry> {{\n{0}", _Indent);
					// #% Separator =new Separator (","); 
					 Separator =new Separator (",");
					// #% MakeWidgets (Object, Separator); 
					 MakeWidgets (Object, Separator);
					// 			} ; 
					_Output.Write ("			}} ;\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
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
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	} 
			_Output.Write ("	}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #block MakeTextList 
		

		//
		//  MakeTextList
		//

			// #% public void MakeTextList (List<_Choice> Entries) { 
			 public void MakeTextList (List<_Choice> Entries) {
			// #% var Separator = new Separator (","); 
			 var Separator = new Separator (",");
			// 		new List<string> {#! 
			_Output.Write ("		new List<string> {{", _Indent);
			// #foreach (var Entry in Entries) 
			foreach  (var Entry in Entries) {
				// #% var Text = Entry as Text; 
				 var Text = Entry as Text;
				// #if Text != null 
				if (  Text != null ) {
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			#{Text.Data.Quoted()} #! 
					_Output.Write ("			{1} ", _Indent, Text.Data.Quoted());
					// #end if 
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			}#! 
			_Output.Write ("			}}", _Indent);
			// #% } 
			 }
			// #end block  
		
		//  
		//  
		//  
		// #method MakeMenu Menu Menu 
		

		//
		// MakeMenu
		//
		public void MakeMenu (Menu Menu) {
			// 		List<MenuEntry> _Entries = new List<MenuEntry> {#! 
			_Output.Write ("		List<MenuEntry> _Entries = new List<MenuEntry> {{", _Indent);
			// #% var Separator = new Separator (","); 
			 var Separator = new Separator (",");
			// #foreach (var Entry in Menu.Entries) 
			foreach  (var Entry in Menu.Entries) {
				// #switchcast GUISchemaType Entry 
				switch (Entry._Tag ()) {
					// #casecast Menu SubMenu 
					case GUISchemaType.Menu: {
					  Menu SubMenu = (Menu) Entry; 
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new SubMenu ("#{SubMenu.Id}",  "#{SubMenu.Tag}", new #{SubMenu.Id}())#! 
					_Output.Write ("			new SubMenu (\"{1}\",  \"{2}\", new {3}())", _Indent, SubMenu.Id, SubMenu.Tag, SubMenu.Id);
					// #casecast Command Command 
					break; }
					case GUISchemaType.Command: {
					  Command Command = (Command) Entry; 
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new MenuEntry ("#{Command.Id}",  "#{Command.Tag}")#! 
					_Output.Write ("			new MenuEntry (\"{1}\",  \"{2}\")", _Indent, Command.Id, Command.Tag);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// }; 
			_Output.Write ("}};\n{0}", _Indent);
			// #end method 
			}
		//  
		//  
		// #block MakeWidgets 
		

		//
		//  MakeWidgets
		//

			// #% public void MakeWidgets (_Choice Choice, Separator Separator) { 
			 public void MakeWidgets (_Choice Choice, Separator Separator) {
			// #% List<_Choice> Entries = null; 
			 List<_Choice> Entries = null;
			// #% var Object = Choice as Object; 
			 var Object = Choice as Object;
			// #if (Object != null) 
			if (  (Object != null) ) {
				// #% Entries = Object.Entries; 
				 Entries = Object.Entries;
				// #if (Object.ParentObject != null) 
				if (  (Object.ParentObject != null) ) {
					// #% MakeWidgets (Object.ParentObject, Separator); 
					 MakeWidgets (Object.ParentObject, Separator);
					// #end if 
					}
				// #elseif (Choice as Enumerate != null) 
				} else if (  (Choice as Enumerate != null)) {
				// #% var Enumerate = Choice as Enumerate; 
				 var Enumerate = Choice as Enumerate;
				// #% Entries = Enumerate.Entries; 
				 Entries = Enumerate.Entries;
				// #else 
				} else {
				// #% var Option = Choice as Option; 
				 var Option = Choice as Option;
				// #% Entries = Option.Entries; 
				 Entries = Option.Entries;
				// #end if 
				}
			// #foreach (var Entry in Entries) 
			foreach  (var Entry in Entries) {
				// #if (Entry as Command != null)  
				if (  (Entry as Command != null)  ) {
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new ObjectCommand { 
					_Output.Write ("			new ObjectCommand {{\n{0}", _Indent);
					// 						Id = "#{Entry.FieldName}",   
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					// 						Label = "#{Entry.FieldTag}"}#! 
					_Output.Write ("						Label = \"{1}\"}}", _Indent, Entry.FieldTag);
					// #elseif (Entry as Inherit != null)  
					} else if (  (Entry as Inherit != null) ) {
					// #elseif (Entry as Text != null)  
					} else if (  (Entry as Text != null) ) {
					// #% var Text = Entry as Text; 
					 var Text = Entry as Text;
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new ObjectText { 
					_Output.Write ("			new ObjectText {{\n{0}", _Indent);
					// 						Text = "#{Text}" 
					_Output.Write ("						Text = \"{1}\"\n{0}", _Indent, Text);
					// 						}#! 
					_Output.Write ("						}}", _Indent);
					// #elseif (Entry as Enumerate != null)  
					} else if (  (Entry as Enumerate != null) ) {
					// #% var Enumerate = Entry as Enumerate; 
					 var Enumerate = Entry as Enumerate;
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new ObjectFieldEnumerate { 
					_Output.Write ("			new ObjectFieldEnumerate {{\n{0}", _Indent);
					// 						Id = "#{Entry.FieldName}",   
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					// 						Label = "#{Entry.FieldTag}", 
					_Output.Write ("						Label = \"{1}\",\n{0}", _Indent, Entry.FieldTag);
					// 						Entries = new List<ObjectEntry> {#! 
					_Output.Write ("						Entries = new List<ObjectEntry> {{", _Indent);
					// #indent 
					_Indent = _Indent + "\t";
					// #% var SubSeparator = new Separator (","); 
					 var SubSeparator = new Separator (",");
					// #%  MakeWidgets (Enumerate, SubSeparator); 
					  MakeWidgets (Enumerate, SubSeparator);
					// #outdent 
					_Indent = _Indent.Remove (0,1);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 							} 
					_Output.Write ("							}}\n{0}", _Indent);
					// 						}#! 
					_Output.Write ("						}}", _Indent);
					// #elseif (Entry as Option != null)  
					} else if (  (Entry as Option != null) ) {
					// #% var Option = Entry as Option; 
					 var Option = Entry as Option;
					// #{Separator}	 
					_Output.Write ("{1}	\n{0}", _Indent, Separator);
					// 			new ObjectFieldOption { 
					_Output.Write ("			new ObjectFieldOption {{\n{0}", _Indent);
					// 						Id = "#{Entry.FieldName}",   
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					// 						Label = "#{Entry.FieldTag}", 
					_Output.Write ("						Label = \"{1}\",\n{0}", _Indent, Entry.FieldTag);
					// 						Entries = new List<ObjectEntry> {#! 
					_Output.Write ("						Entries = new List<ObjectEntry> {{", _Indent);
					// #indent 
					_Indent = _Indent + "\t";
					// #% var SubSeparator = new Separator (","); 
					 var SubSeparator = new Separator (",");
					// #%  MakeWidgets (Option, SubSeparator); 
					  MakeWidgets (Option, SubSeparator);
					// #outdent 
					_Indent = _Indent.Remove (0,1);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 							} 
					_Output.Write ("							}}\n{0}", _Indent);
					// 						}#! 
					_Output.Write ("						}}", _Indent);
					// #else  
					} else {
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new ObjectField#{Entry.WidgetType} {Id = "#{Entry.FieldName}",  
					_Output.Write ("			new ObjectField{1} {{Id = \"{2}\", \n{0}", _Indent, Entry.WidgetType, Entry.FieldName);
					// 						Label = "#{Entry.FieldTag}" // #{Entry.FieldIndex} 
					_Output.Write ("						Label = \"{1}\" // {2}\n{0}", _Indent, Entry.FieldTag, Entry.FieldIndex);
					// 					    }#! 
					_Output.Write ("					    }}", _Indent);
					// #end if 
					}
				// #end foreach 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		//  
		// #method MakeFieldArray Object Object 
		

		//
		// MakeFieldArray
		//
		public void MakeFieldArray (Object Object) {
			// #if (Object.ParentObject != null) 
			if (  (Object.ParentObject != null) ) {
				// #call MakeFieldArray Object.ParentObject 
				MakeFieldArray (Object.ParentObject);
				// #end if 
				}
			// #foreach (var Entry in Object.Entries) 
			foreach  (var Entry in Object.Entries) {
				// #if ((Entry.FieldNumber >= 0)) 
				if (  ((Entry.FieldNumber >= 0)) ) {
					// #{Separator} 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					// 			new FieldValue#{Entry.WidgetType} ()  /* #{Entry.FieldName} #{Entry.FieldNumber} */ #! 
					_Output.Write ("			new FieldValue{1} ()  /* {2} {3} */ ", _Indent, Entry.WidgetType, Entry.FieldName, Entry.FieldNumber);
					// #end if	 
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		//  
		// #method DeclareFields List<_Choice> Entries 
		

		//
		// DeclareFields
		//
		public void DeclareFields (List<_Choice> Entries) {
			// #foreach (var Entry in Entries) 
			foreach  (var Entry in Entries) {
				// #if (Entry.FieldNumber >= 0) 
				if (  (Entry.FieldNumber >= 0) ) {
					// #if (Entry as Enumerate != null)  
					if (  (Entry as Enumerate != null)  ) {
						// 		public enum #{Entry.FieldType} {#! 
						_Output.Write ("		public enum {1} {{", _Indent, Entry.FieldType);
						// #% Separator = new Separator (","); 
						 Separator = new Separator (",");
						// #foreach (var EnumEntry in (Entry as Enumerate).Entries) 
						foreach  (var EnumEntry in (Entry as Enumerate).Entries) {
							// #switchcast GUISchemaType EnumEntry 
							switch (EnumEntry._Tag ()) {
								// #casecast Radio Radio 
								case GUISchemaType.Radio: {
								  Radio Radio = (Radio) EnumEntry; 
								// #{Separator} 
								_Output.Write ("{1}\n{0}", _Indent, Separator);
								// 			#{Radio.Id}  /* #{Radio.Tag} */ 
								_Output.Write ("			{1}  /* {2} */\n{0}", _Indent, Radio.Id, Radio.Tag);
								// #end switchcast 
							break; }
								}
							// #end foreach 
							}
						// 			}; 
						_Output.Write ("			}};\n{0}", _Indent);
						// 		public #{Entry.FieldType} #{Entry.FieldName}; 
						_Output.Write ("		public {1} {2};\n{0}", _Indent, Entry.FieldType, Entry.FieldName);
						// #else 
						} else {
						// 		/// <summary> 
						_Output.Write ("		/// <summary>\n{0}", _Indent);
						//         /// #{Entry.FieldName} 
						_Output.Write ("        /// {1}\n{0}", _Indent, Entry.FieldName);
						//         /// </summary> 
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						// 		public #{Entry.FieldType} #{Entry.FieldName} { 
						_Output.Write ("		public {1} {2} {{\n{0}", _Indent, Entry.FieldType, Entry.FieldName);
						// 			get { 
						_Output.Write ("			get {{\n{0}", _Indent);
						// #if (Entry as Item != null) 
						if (  (Entry as Item != null) ) {
							// 				return (#{Entry.FieldType}) #{Entry.FieldIndex}; 
							_Output.Write ("				return ({1}) {2};\n{0}", _Indent, Entry.FieldType, Entry.FieldIndex);
							// #else 
							} else {
							// 				return #{Entry.FieldIndex}; 
							_Output.Write ("				return {1};\n{0}", _Indent, Entry.FieldIndex);
							// #end if 
							}
						// 				} 
						_Output.Write ("				}}\n{0}", _Indent);
						// 			set { 
						_Output.Write ("			set {{\n{0}", _Indent);
						// 				#{Entry.FieldIndex} = value; 
						_Output.Write ("				{1} = value;\n{0}", _Indent, Entry.FieldIndex);
						// 				} 
						_Output.Write ("				}}\n{0}", _Indent);
						// 			} 
						_Output.Write ("			}}\n{0}", _Indent);
						// 			 
						_Output.Write ("			\n{0}", _Indent);
						// #end if 
						}
					// #end if 
					}
				// #switchcast GUISchemaType Entry 
				switch (Entry._Tag ()) {
					// #casecast Item Item 
					case GUISchemaType.Item: {
					  Item Item = (Item) Entry; 
					// #casecast List List 
					break; }
					case GUISchemaType.List: {
					  List List = (List) Entry; 
					// #casecast Set Set 
					break; }
					case GUISchemaType.Set: {
					  Set Set = (Set) Entry; 
					// #casecast Option Option 
					break; }
					case GUISchemaType.Option: {
					  Option Option = (Option) Entry; 
					// #call DeclareFields Option.Entries 
					
					DeclareFields (Option.Entries);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #end pclass 
		}
	}
