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
using  System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Trojan.Script {
	/// <summary>A Goedel script.</summary>
	public partial class GenerateGTK : global::Goedel.Registry.Script {
		/// <summary>Default constructor.</summary>
		public GenerateGTK () : base () {
			}
		/// <summary>Constructor with output stream.</summary>
		/// <param name="Output">The output stream</param>
		public GenerateGTK (TextWriter Output) : base (Output) {
			}

		//
		 string GoedelNamespace = "Goedel.Trojan";
		 Separator Separator;
		 public bool FALSE = false;
		

		//
		// GenerateCS
		//
		public void GenerateCS (GUISchema GUISchema) {
			 GUISchema._InitChildren ();
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("//This file was generated automatically.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
			_Output.Write ("using Goedel.Trojan;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var GUI in GUISchema.GUIs) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("namespace {1} {{\n{0}", _Indent, GUI.Namespace);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	// Make extensible partial classes for all the toplevel classes\n{0}", _Indent);
				_Output.Write ("	// This allows an implementation to decorate any class at will.\n{0}", _Indent);
				_Output.Write ("	public abstract partial class Object:  {1}.Object {{\n{0}", _Indent, GoedelNamespace);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("	public abstract partial class Menu:  {1}.Menu {{\n{0}", _Indent, GoedelNamespace);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("	public abstract partial class Window:  {1}.Window {{\n{0}", _Indent, GoedelNamespace);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// The application data model. This inherits the field declarations and\n{0}", _Indent);
				_Output.Write ("	/// stub callback methods from the template. The stub callbacks should be\n{0}", _Indent);
				_Output.Write ("	/// overwritten by the user's code. \n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, GUI.Id, GUI.Id);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// The template data model  constructed from the specification.\n{0}", _Indent);
				_Output.Write ("	/// Contains stub methods for each callback.\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public abstract class _{1} : {2}.Model {{\n{0}", _Indent, GUI.Id, GoedelNamespace);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Default constructor.\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("        public _{1}() {{\n{0}", _Indent, GUI.Id);
				_Output.Write ("            _About = new About(this) {{\n{0}", _Indent);
				if (  GUI.About != null ) {
					_Output.Write ("				Name = \"{1}\"\n{0}", _Indent, GUI.About.Title);
					}
				_Output.Write ("				}};\n{0}", _Indent);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var Command in GUI.Commands) {
					_Output.Write ("{1} ", _Indent, CommentSummary(8, Command.CommentSummary));
					_Output.Write ("		public virtual void {1} (", _Indent, Command.Id);
					if (  (Command.Parameter != null)  ) {
						_Output.Write ("Object Object ", _Indent);
						}
					_Output.Write (") {{\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				//		protected #{Object.Id} Selected_#{Object.Id} = null ;
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("{1} ", _Indent, CommentSummary(8, "Dispatch command callback with required parameters."));
				_Output.Write ("        public override void  Dispatch(string Command) {{\n{0}", _Indent);
				_Output.Write ("            switch (Command) {{\n{0}", _Indent);
				foreach  (var Command in GUI.Commands)  {
					if (  (Command.Parameter != null)  ) {
						_Output.Write ("                case \"{1}\": {{\n{0}", _Indent, Command.Id);
						_Output.Write ("                        {1}(Selected as Object);\n{0}", _Indent, Command.Id);
						} else {
						_Output.Write ("                case \"{1}\": {{\n{0}", _Indent, Command.Id);
						_Output.Write ("                        {1}();\n{0}", _Indent, Command.Id);
						}
					_Output.Write ("                        return;\n{0}", _Indent);
					_Output.Write ("                        }}\n{0}", _Indent);
					}
				_Output.Write ("                }}\n{0}", _Indent);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				if (  FALSE ) {
					_Output.Write ("{1} ", _Indent, CommentSummary(8, "Report if selection criteria for specified command are met."));
					_Output.Write ("        public bool Active(String Command) {{\n{0}", _Indent);
					_Output.Write ("            switch (Command) {{\n{0}", _Indent);
					foreach  (var Command in GUI.Commands)  {
						if (  (Command.Parameter != null)  ) {
							_Output.Write ("                case \"{1}_{2}\": {{\n{0}", _Indent, Command.Id, Command.Parameter);
							_Output.Write ("						// NYI here make a list of all the possibilities\n{0}", _Indent);
							_Output.Write ("                        //return Selected_{1} != null;\n{0}", _Indent, Command.Parameter);
							_Output.Write ("						return true;\n{0}", _Indent);
							_Output.Write ("                        }}\n{0}", _Indent);
							}
						}
					_Output.Write ("                }}\n{0}", _Indent);
					_Output.Write ("            return true;\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					}
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	// Windows\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/*\n{0}", _Indent);
				_Output.Write ("	* Window declarations\n{0}", _Indent);
				_Output.Write ("	*/\n{0}", _Indent);
				foreach  (var Window in GUI.Windows)  {
					_Output.Write ("{1} ", _Indent, CommentSummary(8, Window.CommentSummary));
					_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, Window.Id, Window.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		string _Title = \"{1}\";\n{0}", _Indent, Window.Tag);
					_Output.Write ("		public override string Title {{\n{0}", _Indent);
					_Output.Write ("			get {{return _Title;}}\n{0}", _Indent);
					_Output.Write ("			set {{_Title = value;}}\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		public {1}  ({2}.Model Model, Binding Binding) {{\n{0}", _Indent, Window.Id, GoedelNamespace);
					_Output.Write ("			// Call backing code to populate the data model\n{0}", _Indent);
					_Output.Write ("			Populate ();\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			// Initialize the view and controller\n{0}", _Indent);
					_Output.Write ("			Initialize (Model, Binding);\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	public abstract class _{1} : Window {{\n{0}", _Indent, Window.Id);
					_Output.Write ("\n{0}", _Indent);
					if (  (Window.Menu != null) ) {
						_Output.Write ("		Menu _Menu = new {1} ();\n{0}", _Indent, Window.Menu.Id);
						_Output.Write ("        public override {1}.Menu Menu {{ get {{ return _Menu; }} }}\n{0}", _Indent, GoedelNamespace);
						}
					_Output.Write ("		}}\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/*\n{0}", _Indent);
				_Output.Write ("	* Menu declarations\n{0}", _Indent);
				_Output.Write ("	*/\n{0}", _Indent);
				foreach  (var Menu in GUI.Menus)  {
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("{1} ", _Indent, CommentSummary(8, Menu.CommentSummary));
					_Output.Write ("	public partial class {1} : Menu {{\n{0}", _Indent, Menu.Id);
					_Output.Write ("	\n{0}", _Indent);
					_Output.Write ("		public override List<MenuEntry> Entries {{\n{0}", _Indent);
					_Output.Write ("            get {{ return _Entries; }}\n{0}", _Indent);
					_Output.Write ("            set {{ _Entries = value; }}\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					MakeMenu (Menu);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/*\n{0}", _Indent);
				_Output.Write ("	* Wizard declarations\n{0}", _Indent);
				_Output.Write ("	*/\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var Wizard in GUI.Wizards)  {
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	/// <summary>\n{0}", _Indent);
					_Output.Write ("    /// Wizard callback class.\n{0}", _Indent);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	public partial class {1} : _{2} {{\n{0}", _Indent, Wizard.Id, Wizard.Id);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	/// <summary>\n{0}", _Indent);
					_Output.Write ("    /// Template class for wizard. The application programmer implements\n{0}", _Indent);
					_Output.Write ("	/// the wizard by overriding wizard methods. Note that since the user \n{0}", _Indent);
					_Output.Write ("	/// may backtrack when implementing a method, callbacks MUST tolerate\n{0}", _Indent);
					_Output.Write ("	/// being called multiple times. It is also permitted for a user to \n{0}", _Indent);
					_Output.Write ("	/// cancel a wizard before the final commit.\n{0}", _Indent);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("	public partial class _{1} : Wizard {{\n{0}", _Indent, Wizard.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        public override string Title => {1};\n{0}", _Indent, Wizard.Tag.Quoted());
					_Output.Write ("        public override List<string> Texts => _Texts;\n{0}", _Indent);
					_Output.Write ("        public override List<Step> Steps => _Steps;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Default constructor\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"Model\">Model to bind to</param>\n{0}", _Indent);
					_Output.Write ("        public _{1}(Model Model) : base(Model) {{\n{0}", _Indent, Wizard.Id);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					DeclareFields (Wizard.Entries);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		// {1}\n{0}", _Indent, Wizard.Id);
					_Output.Write ("		List<string> _Texts = ", _Indent);
					MakeTextList (Wizard.Entries);
					_Output.Write (";\n{0}", _Indent);
					_Output.Write ("		List<Step> _Steps = new List<Step> {{", _Indent);
					 Separator = new Separator (",");
					foreach  (var Entry in Wizard.Entries) {
						 var Step = Entry as Step;
						if (  Step != null ) {
							_Output.Write ("{1}\n{0}", _Indent, Separator);
							_Output.Write ("			new Step () {{Value = new {1} (), \n{0}", _Indent, Step.Id);
							_Output.Write ("				Title = {1}, Description =\n{0}", _Indent, Step.Tag.Quoted());
							_Output.Write ("		", _Indent);
							_Indent = _Indent + "\t";
							_Indent = _Indent + "\t";
							MakeTextList (Step.Entries);
							_Indent = _Indent.Remove (0,1);
							_Indent = _Indent.Remove (0,1);
							_Output.Write ("}}", _Indent);
							}
						}
					_Output.Write ("}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		public override bool Dispatch (int Step) {{\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			switch (Step) {{\n{0}", _Indent);
					 int StepNumber = 0;
					foreach  (var Entry in Wizard.Entries) {
						 var Step = Entry as Step;
						if (  Step != null ) {
							_Output.Write ("				case {1} : return {2}.Dispatch(this);\n{0}", _Indent, StepNumber++, Step.Id.Label());
							}
						}
					_Output.Write ("				}}\n{0}", _Indent);
					_Output.Write ("			return false;\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/*\n{0}", _Indent);
				_Output.Write ("	* Backing object class declarations\n{0}", _Indent);
				_Output.Write ("	*/\n{0}", _Indent);
				foreach  (var Object in GUI.Objects)  {
					_Output.Write ("	public partial class {1} ", _Indent, Object.Id);
					if (  (Object.ParentObject == null) ) {
						_Output.Write (": Object ", _Indent);
						} else {
						_Output.Write (": {1} ", _Indent, Object.ParentObject.Id);
						}
					_Output.Write ("{{\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					DeclareFields (Object.Entries);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		public override List<ObjectEntry> Entries {{\n{0}", _Indent);
					_Output.Write ("            get {{ return _Entries; }}\n{0}", _Indent);
					_Output.Write ("            set {{ _Entries = value; }}\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		List<ObjectEntry> _Entries = new List<ObjectEntry> {{\n{0}", _Indent);
					 Separator =new Separator (",");
					 MakeWidgets (Object, Separator);
					_Output.Write ("			}} ;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Create a list containing all the current children.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <returns></returns>\n{0}", _Indent);
					_Output.Write ("        public override List<Goedel.Trojan.Object> GetChildren() {{\n{0}", _Indent);
					if (  (Object.ParentObject == null) ) {
						_Output.Write ("			var Result = base.GetChildren();\n{0}", _Indent);
						} else {
						_Output.Write ("            var Result = new List<Goedel.Trojan.Object>();\n{0}", _Indent);
						}
					foreach  (var Entry in Object.Entries) {
						switch (Entry._Tag ()) {
							case GUISchemaType.List: {
							  List List = (List) Entry; 
							_Output.Write ("			if ({1}.Value != null) {{\n{0}", _Indent, List.FieldName);
							_Output.Write ("				foreach (var Entry in {1}.Value) {{\n{0}", _Indent, List.FieldName);
							_Output.Write ("					Result.Add (Entry);\n{0}", _Indent);
							_Output.Write ("					}}\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
							break; }
							case GUISchemaType.Set: {
							  Set Set = (Set) Entry; 
							_Output.Write ("			if ({1}.Value != null) {{\n{0}", _Indent, Set.FieldName);
							_Output.Write ("				foreach (var Entry in {1}.Value) {{\n{0}", _Indent, Set.FieldName);
							_Output.Write ("					Result.Add (Entry);\n{0}", _Indent);
							_Output.Write ("					}}\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			return Result;\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		//  MakeTextList
		//

			 public void MakeTextList (List<_Choice> Entries) {
			 var Separator = new Separator (",");
			_Output.Write ("		new List<string> {{", _Indent);
			foreach  (var Entry in Entries) {
				 var Text = Entry as Text;
				if (  Text != null ) {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			{1} ", _Indent, Text.Data.Quoted());
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			}}", _Indent);
			 }
		
		

		//
		// MakeMenu
		//
		public void MakeMenu (Menu Menu) {
			_Output.Write ("		List<MenuEntry> _Entries = new List<MenuEntry> {{", _Indent);
			 var Separator = new Separator (",");
			foreach  (var Entry in Menu.Entries) {
				switch (Entry._Tag ()) {
					case GUISchemaType.Menu: {
					  Menu SubMenu = (Menu) Entry; 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new SubMenu () {{\n{0}", _Indent);
					_Output.Write ("				Id =\"{1}\",  \n{0}", _Indent, SubMenu.Id);
					_Output.Write ("				Label = \"{1}\", \n{0}", _Indent, SubMenu.Tag);
					_Output.Write ("				Sub = new {1}() }}", _Indent, SubMenu.Id);
					break; }
					case GUISchemaType.Action: {
					  Action Action = (Action) Entry; 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new MenuEntry () {{ \n{0}", _Indent);
					_Output.Write ("				Id =\"{1}\",  \n{0}", _Indent, Action.Id);
					_Output.Write ("				Label = \"{1}\" }}", _Indent, Action.Tag);
					break; }
					case GUISchemaType.Divider: { 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new MenuDivider () ", _Indent);
				break; }
					}
				}
			_Output.Write ("}};\n{0}", _Indent);
			}
		

		//
		//  MakeWidgets
		//

			 public void MakeWidgets (_Choice Choice, Separator Separator) {
			 List<_Choice> Entries = null;
			 var Object = Choice as Object;
			if (  (Object != null) ) {
				 Entries = Object.Entries;
				if (  (Object.ParentObject != null) ) {
					 MakeWidgets (Object.ParentObject, Separator);
					}
				} else if (  (Choice as Enumerate != null)) {
				 var Enumerate = Choice as Enumerate;
				 Entries = Enumerate.Entries;
				} else {
				 var Option = Choice as Option;
				 Entries = Option.Entries;
				}
			foreach  (var Entry in Entries) {
				if (  (Entry as Command != null)  ) {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectCommand {{\n{0}", _Indent);
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					_Output.Write ("						Label = {1}}}", _Indent, Entry.FieldTag.Quoted());
					} else if (  (Entry as Inherit != null) ) {
					} else if (  (Entry as Action != null) ) {
					 var Action = Entry as Action;
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectAction {{\n{0}", _Indent);
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					if (  (Entry.Text != null) ) {
						_Output.Write ("						Text = {1},\n{0}", _Indent, Entry.Text.Quoted());
						}
					_Output.Write ("						Label = {1}}}", _Indent, Action.Tag.Quoted());
					} else if (  (Entry as Text != null) ) {
					 var Text = Entry as Text;
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectText {{\n{0}", _Indent);
					_Output.Write ("						Text = \"{1}\"\n{0}", _Indent, Text);
					_Output.Write ("						}}", _Indent);
					} else if (  (Entry as Enumerate != null) ) {
					 var Enumerate = Entry as Enumerate;
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectFieldEnumerate {{\n{0}", _Indent);
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					_Output.Write ("						Label = \"{1}\",\n{0}", _Indent, Entry.FieldTag);
					_Output.Write ("						Entries = new List<ObjectEntry> {{", _Indent);
					 var SubSeparator = new Separator (",");
					foreach  (var RadioEntry in Enumerate.Entries) {
						_Output.Write ("{1}\n{0}", _Indent, SubSeparator);
						_Output.Write ("				new ObjectFieldRadio {{\n{0}", _Indent);
						_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, RadioEntry.FieldName);
						_Output.Write ("						Label = \"{1}\",\n{0}", _Indent, RadioEntry.FieldTag);
						_Output.Write ("						SelectionValue = (int) Enum{1}.{2}  }} ", _Indent, Entry.FieldName, RadioEntry.FieldName);
						}
					_Output.Write ("							}}\n{0}", _Indent);
					_Output.Write ("						}}", _Indent);
					} else if (  (Entry as Item != null) ) {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectFieldItem {{\n{0}", _Indent);
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					_Output.Write ("						Label = \"{1}\",\n{0}", _Indent, Entry.FieldTag);
					_Output.Write ("						Value = new {1} () \n{0}", _Indent, Entry.FieldType);
					_Output.Write ("						}}", _Indent);
					} else if (  (Entry as Option != null) ) {
					 var Option = Entry as Option;
					_Output.Write ("{1}	\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectFieldOption {{\n{0}", _Indent);
					_Output.Write ("						Id = \"{1}\",  \n{0}", _Indent, Entry.FieldName);
					_Output.Write ("						Label = \"{1}\",\n{0}", _Indent, Entry.FieldTag);
					if (  (Entry.Output) ) {
						_Output.Write ("						ReadOnly = true,\n{0}", _Indent);
						}
					_Output.Write ("						Entries = new List<ObjectEntry> {{", _Indent);
					_Indent = _Indent + "\t";
					 var SubSeparator = new Separator (",");
					  MakeWidgets (Option, SubSeparator);
					_Indent = _Indent.Remove (0,1);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("							}}\n{0}", _Indent);
					_Output.Write ("						}}", _Indent);
					} else if (  (Entry as Output != null) ) {
					} else {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new ObjectField{1} {{Id = \"{2}\", \n{0}", _Indent, Entry.WidgetType, Entry.FieldName);
					// Do not need to check if these are permitted here as Goedel syntax check does that.
					if (  (Entry as List != null) ) {
						_Output.Write ("						Prototype = new {1} (),\n{0}", _Indent, (Entry as List).Of);
						}
					if (  (Entry.Output) ) {
						_Output.Write ("						ReadOnly = true,\n{0}", _Indent);
						}
					if (  (Entry.Tip != null) ) {
						_Output.Write ("						Tip = {1},\n{0}", _Indent, Entry.Tip.Quoted());
						}
					if (  (Entry.IsSlider) ) {
						_Output.Write ("						Mode = FieldModeInteger.Slider,\n{0}", _Indent);
						}
					if (  (Entry.Range != null) ) {
						_Output.Write ("						Minimum = {1},\n{0}", _Indent, Entry.Range.Lower);
						_Output.Write ("						Maximum = {1},\n{0}", _Indent, Entry.Range.Upper);
						_Output.Write ("						Step = {1},\n{0}", _Indent, Entry.Range.Step);
						}
					if (  (Entry.Length != null) ) {
						_Output.Write ("						Length = {1},\n{0}", _Indent, Entry.Length);
						}
					_Output.Write ("						Label = \"{1}\" // {2}\n{0}", _Indent, Entry.FieldTag, Entry.FieldIndex);
					_Output.Write ("					    }}", _Indent);
					}
				}
			 }
		
		

		//
		// MakeFieldArray
		//
		public void MakeFieldArray (Object Object) {
			if (  (Object.ParentObject != null) ) {
				MakeFieldArray (Object.ParentObject);
				}
			foreach  (var Entry in Object.Entries) {
				if (  ((Entry.FieldNumber >= 0)) ) {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new FieldValue{1} ()  /* {2} {3} */ ", _Indent, Entry.WidgetType, Entry.FieldName, Entry.FieldNumber);
					}
				}
			}
		

		//
		// DeclareFields
		//
		public void DeclareFields (List<_Choice> Entries) {
			foreach  (var Entry in Entries) {
				if (  (Entry.FieldType != null) ) {
					if (  (Entry as Enumerate != null)  ) {
						_Output.Write ("		public enum {1} {{", _Indent, Entry.FieldType);
						 Separator = new Separator (",");
						foreach  (var EnumEntry in (Entry as Enumerate).Entries) {
							switch (EnumEntry._Tag ()) {
								case GUISchemaType.Radio: {
								  Radio Radio = (Radio) EnumEntry; 
								_Output.Write ("{1}\n{0}", _Indent, Separator);
								_Output.Write ("			{1}  /* {2} */\n{0}", _Indent, Radio.Id, Radio.Tag);
							break; }
								}
							}
						_Output.Write ("			}};\n{0}", _Indent);
						_Output.Write ("		public ObjectFieldEnumerate {1}  {{\n{0}", _Indent, Entry.FieldName);
						_Output.Write ("			get {{\n{0}", _Indent);
						_Output.Write ("				return {1};\n{0}", _Indent, Entry.FieldIndex);
						_Output.Write ("				}}\n{0}", _Indent);
						_Output.Write ("			}}\n{0}", _Indent);
						} else {
						_Output.Write ("		/// <summary>\n{0}", _Indent);
						_Output.Write ("        /// {1}\n{0}", _Indent, Entry.FieldName);
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						if (  (Entry as Item != null) ) {
							_Output.Write ("		public {1} {2} {{\n{0}", _Indent, Entry.FieldType, Entry.FieldName);
							_Output.Write ("			get {{\n{0}", _Indent);
							_Output.Write ("				return ({1}) ({2}).Value;\n{0}", _Indent, Entry.FieldType, Entry.FieldIndex);
							_Output.Write ("				}}\n{0}", _Indent);
							} else if (  (Entry as Step != null)) {
							_Output.Write ("		public {1} {2} {{\n{0}", _Indent, Entry.FieldType, Entry.FieldName);
							_Output.Write ("			get {{\n{0}", _Indent);
							_Output.Write ("				return ({1}) {2};\n{0}", _Indent, Entry.FieldType, Entry.FieldIndex);
							_Output.Write ("				}}\n{0}", _Indent);
							} else {
							_Output.Write ("		public ObjectField{1} {2} {{\n{0}", _Indent, Entry.WidgetType, Entry.FieldName);
							_Output.Write ("			get {{\n{0}", _Indent);
							_Output.Write ("				return {1};\n{0}", _Indent, Entry.FieldIndex);
							_Output.Write ("				}}\n{0}", _Indent);
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("			}}\n{0}", _Indent);
						_Output.Write ("			\n{0}", _Indent);
						}
					}
				switch (Entry._Tag ()) {
					case GUISchemaType.Item: { 
					break; }
					case GUISchemaType.List: { 
					break; }
					case GUISchemaType.Set: { 
					break; }
					case GUISchemaType.Option: {
					  Option Option = (Option) Entry; 
					
					DeclareFields (Option.Entries);
				break; }
					}
				}
			}
		}
	}
