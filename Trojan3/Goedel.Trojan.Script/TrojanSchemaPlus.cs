using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Goedel.Registry;

namespace Goedel.Trojan.Script {


    partial class GUISchema {
        public static List<GUI> GUIs = new List<GUI>();
        }


    partial class _Choice {
        public GUI GUI;

        public string CommentSummary = "TBS";

        public string Accessor = null;

        public string WidgetType = null;
        public string FieldName = null;
        public string FieldType = null;
        public string FieldTag = null;

        public bool Output = false;
        public List<string> Tip = null;
        public List<string> Text = null;

        public int FieldNumber = -1;
        public int SubFieldNumber = -1;
        public string FieldIndex = null;

        public bool SimpleField = false;

        public _Choice Parent;
        public Object ParentObject = null;
        public Wizard ParentWizard = null;



        /// <summary>
        /// Set the properties from the entries list.
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Entries"></param>

        protected void SetFieldEntries(_Choice Parent, List <_Choice> Entries) {

            SetEntries(Parent);
            SimpleField = true;

            foreach (var Entry in Entries) {
                if (Entry as Output != null) {
                    Output = true;
                    }
                if (Entry as Tip != null) {
                    this.Tip = (Entry as Tip).Data;
                    }
                }
            }

        protected void SetEntries(_Choice Parent) {
            this.Parent = Parent;
            ParentObject = Parent as Object;

            if (Parent as Wizard != null) {
                return; // Ignore text entries in a Wizard.
                }

            if (ParentObject == null) {
                // we are a sub list

                ParentObject = Parent.ParentObject;
                FieldNumber = ++ParentObject.SubFieldNumber;

                Accessor = "((ObjectField" + WidgetType + ")"+
                    Parent.Accessor + ".Entries[" + FieldNumber.ToString() + "])";
                }

            else {
                // bare accessor
                FieldNumber = ++ParentObject.FieldNumber;

                Accessor = "((ObjectField" + WidgetType + ")Entries[" +
                    FieldNumber.ToString() + "])";
                }


            FieldIndex = Accessor ;
            }

        protected void SetStepEntries(_Choice Parent) {
            this.Parent = Parent;
            ParentWizard = Parent as Wizard;


            // bare accessor
            FieldNumber = ++ParentWizard.FieldNumber;

            Accessor = "Steps[" +
                FieldNumber.ToString() + "]";

            FieldIndex = Accessor + ".Value";
            }

        }


    partial class GUI {
        public List<Command> Commands = new List<Command>();
        public List<Window> Windows = new List<Window>();
        public List<Menu> Menus = new List<Menu>();
        public List<Object> Objects = new List<Object>();
        public List<Wizard> Wizards = new List<Wizard>();
        public About About = null;

        public override void Init(_Choice Parent) {
            base.GUI = this;
            GUISchema.GUIs.Add(this);

            }

        }



    partial class Window {

        public Menu Menu; 

        public override void Init(_Choice Parent) {
            GUI = Parent.GUI;
            GUI.Windows.Add(this);

            foreach (var Entry in Entries) {
                var ThreePane = Entry as ThreePane;
                if (ThreePane != null) {
                    Menu = ThreePane.Menu.Definition as Menu;
                    }

                }

            }

        }


    partial class Wizard {

        public override void Init(_Choice Parent) {
            GUI = Parent.GUI;
            GUI.Wizards.Add(this);
            }

        }

    partial class About {

        public override void Init(_Choice Parent) {
            GUI = Parent.GUI;
            GUI.About = this;
            }

        }


    partial class Menu {


        public override void Init(_Choice Parent) {
            GUI = Parent.GUI;
            GUI.Menus.Add(this);
            }

        }

    partial class Command {
        public string Parameter = null;

        public override void Init(_Choice Parent) {
            GUI = Parent.GUI;
            GUI.Commands.Add(this);

            FieldTag = Tag;
            FieldName = Id.ToString();

            if (Parent as Object != null) {
                ParentObject = Parent as Object;
                Parameter = ParentObject.Id.ToString();
                }

            foreach (var Choice in Entries) {
                if (Choice as Select != null) {
                    var Select = Choice as Select;
                    Parameter = Select.Id.ToString();
                    }
                }

            CommentSummary = "Stub method for " + FieldName + "command." +
                    "Override with application implementation." ;
            }

        }

    partial class Action {
        public string Parameter = null;
        public string Tag = "";
        public Command Definition;

        public override void Init(_Choice Parent) {
            Definition = Id.Definition as Command;
            if (Definition as Command != null) {
                Tag = Definition.Tag;
                }

            GUI = Parent.GUI;

            FieldTag = "none";
            FieldName = Id.ToString();

            foreach (var Entry in Entries) {
                if (Entry as Tip != null) {
                    this.Tip = (Entry as Tip).Data;
                    }
                if (Entry as Text != null) {
                    this.Text = (Entry as Text).Data;
                    }
                }

            if (Parent as Object != null) {
                ParentObject = Parent as Object;
                Parameter = ParentObject.Id.ToString();
                }
            }

        }


    partial class Object {

        public override void Init(_Choice Parent) {
            GUI = Parent.GUI;
            GUI.Objects.Add(this);

            foreach (var Choice in Entries) {
                if (Choice as Inherit != null) {
                    var Inherit = Choice as Inherit;
                    ParentObject = Inherit.Id.Definition as Object;
                    FieldNumber = ParentObject.FieldNumber;
                    }
                }
            }

        }

    partial class Item {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = Of.ToString();
            WidgetType = "Item";
            SetEntries(Parent);

            }
        }

    partial class List {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            //FieldType = "List<" + Of.ToString() + ">";
            FieldType = "List<Goedel.Trojan.Object>";
            WidgetType = "List";
            SetEntries(Parent);
            }
        }

    partial class Set {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            //FieldType = "List<" + Of.ToString() + ">";
            FieldType = "List<Goedel.Trojan.Object>";
            WidgetType = "Set";
            SetEntries(Parent);
            }
        }


    partial class DateTime {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "DateTime";
            WidgetType = "DateTime";
            SetFieldEntries(Parent, Entries);
            }
        }

    partial class Chooser {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "List<string>";
            WidgetType = "Chooser";
            SetFieldEntries(Parent, Entries);
            }
        }

    partial class String {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "string";
            WidgetType = "String";
            SetFieldEntries(Parent, Entries);
            }
        }

    partial class Secret {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "string";
            WidgetType = "Secret";
            SetFieldEntries(Parent, Entries);
            }
        }

    partial class Integer {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "int";
            WidgetType = "Integer";
            SetFieldEntries(Parent, Entries);
            }
        }

    partial class Boolean {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "bool";
            WidgetType = "Boolean";
            SetFieldEntries(Parent, Entries);
            }
        }

    partial class Option {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "bool";
            WidgetType = "Option";
            SetEntries(Parent);
            }
        }


    partial class Enumerate {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "Enum" + Id.ToString();
            WidgetType = "Enumerate";
            SetEntries(Parent);
            }
        }

    partial class Radio {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = "bool";
            WidgetType = "Radio";
            SetEntries(Parent);
            }
        }

    partial class Step {
        public override void Init(_Choice Parent) {
            FieldTag = Tag;
            FieldName = Id.ToString();
            FieldType = Id.ToString();
            WidgetType = null;
            SetStepEntries(Parent);
            }
        }


    partial class Text {

        public override void Init(_Choice Parent) {
            FieldType = null;
            SetEntries(Parent);
            }

        public override string ToString() {
            var Buffer = new StringBuilder();
            var Space = false;
            foreach (var Segment in Data) {
                if (Space) {
                    Buffer.Append(" ");
                    }
                Space = true;
                Buffer.Append(Segment);
                }

            return Buffer.ToString();
            }
        }

    }
