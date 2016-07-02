using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    /// <summary>
    /// Option class. Entry fields appear when option is selected.
    /// </summary>
    public partial class FormWidgetOption : Widget {
        ObjectFieldOption ObjectEntry;
        Gtk.CheckButton Current;
        bool Raised = false;
        GridForm Grid;

        public override Gtk.Widget WidgetTip { get { return Current; } }


        /// <summary>
        /// The object field that this object maps to.
        /// </summary>
        public override ObjectField ObjectField { get { return ObjectEntry; } }

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetOption(Object Object, ObjectFieldOption ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.CheckButton();

            Grid = new GridForm();
            Grid.Attach(Object, ObjectEntry.Entries);

            Current.Active = ObjectEntry.Value;
            Current.Toggled += ChangedEvent;

            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachCheckboxGrid(this, ObjectEntry, Current, Grid);
            ShowOrHide();

            }

        public void ShowOrHide() {
            if (Current.Active) {
                if (!Raised) {
                    GridForm.Attach(Grid, 1, Row, 2, 1);
                    GridForm.ShowAll();
                    Raised = true;
                    }
                }
            else {
                if (Raised) {
                    GridForm.Remove(Grid);
                    Raised = false;
                    }
                }
            }


        private static void ChangedEvent(object Sender, EventArgs E) {
            var FormWidgetOption = Sender as FormWidgetOption;

            if (FormWidgetOption == null) return;

            Console.WriteLine("Toggle!!!");

            FormWidgetOption.ShowOrHide();
            FormWidgetOption.UserChangedValue = true;
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            ObjectEntry.Test = Current.Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            Current.Active = ObjectEntry.Value;

            _Changed = false;
            }

        }


    /// <summary>
    /// Sub structure
    /// </summary>
    public partial class FormWidgetItem : Widget {
        ObjectFieldItem ObjectEntry;
        GridForm Current;

        public override Gtk.Widget WidgetTip { get { return Current; } }


        /// <summary>
        /// The object field that this object maps to.
        /// </summary>
        public override ObjectField ObjectField { get { return ObjectEntry; } }


        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetItem(Object Object, ObjectFieldItem ObjectEntry,
                    GridForm GridForm) {

            Current = new GridForm();

            this.ObjectEntry = ObjectEntry;
            Current.Attach(ObjectEntry.Value, ObjectEntry.Value.Entries);
            this.GridForm = GridForm;
            Row = GridForm.AttachGrid(this, ObjectEntry, Current);
            Current.ShowAll();
            }

        /// <summary>
        /// Copy values from widget to model test field
        /// </summary>
        public override void Test() {
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            }

        }

    /// <summary>
    /// Enumeration widget with radio buttons. Presents output as an integer enumeration value.
    /// </summary>
    public partial class FormWidgetEnumerate : Widget {
        ObjectFieldEnumerate ObjectEntry;
        List<RadioButton> Buttons = new List<RadioButton>();
        GridForm Current;

        public override Gtk.Widget WidgetTip { get { return Current; } }


        /// <summary>
        /// The object field that this object maps to.
        /// </summary>
        public override ObjectField ObjectField { get { return ObjectEntry; } }


        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetEnumerate(Object Object, ObjectFieldEnumerate ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new GridForm();

            int RadioRow = 0;
            RadioButton Button = null;

            //var Label = new Gtk.Label(ObjectEntry.Label);
            //Attach(Label, 0, RadioRow++, 1, 1);

            foreach (var Entry in ObjectEntry.Entries) {
                var Radio = Entry as ObjectFieldRadio;
                if (Radio != null) {
                    if (Button == null) {
                        Button = new RadioButton(this, Radio.SelectionValue, Radio.Label);
                        Button.Active = true;
                        Buttons.Add(Button);
                        }
                    else {
                        Button = new RadioButton(Button, this, Radio.SelectionValue, Radio.Label);
                        Buttons.Add(Button);
                        }
                    Current.Attach(Button, 0, RadioRow++, 1, 1);
                    }
                }

            this.GridForm = GridForm;
            Row = GridForm.AttachGrid(this, ObjectEntry, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            ObjectEntry.Value = -1;
            for (int i = 0; i < Buttons.Count; i++) {
                if (Buttons[i].Active) {
                    ObjectEntry.Value = i;
                    }
                }
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            for (int i = 0; i < Buttons.Count; i++) {
                if (ObjectEntry.Value == i) {
                    Buttons[i].Active = true;
                    }
                else {
                    Buttons[i].Active = false;
                    }
                }
            _Changed = false;
            }


        private class RadioButton : Gtk.RadioButton {
            FormWidgetEnumerate FormWidgetEnumerate;
            int Index;

            public RadioButton(FormWidgetEnumerate FormWidgetEnumerate, int Index, string Text) : base (Text) {
                this.FormWidgetEnumerate = FormWidgetEnumerate;
                this.Index = Index;
                Clicked += OnClicked;
                }

            public RadioButton(RadioButton Group, FormWidgetEnumerate FormWidgetEnumerate, int Index, string Text) : base(Group, Text) {
                this.FormWidgetEnumerate = FormWidgetEnumerate;
                this.Index = Index;
                Clicked += OnClicked;
                }

            private void OnClicked(object Sender, EventArgs E) {
                FormWidgetEnumerate.ObjectEntry.Test = Index;
                }

            }

        }

    }
