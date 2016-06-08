using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    public partial class FormWidgetBoolean : Gtk.CheckButton, IFieldBinding {
        ObjectFieldBoolean ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetBoolean(Object Object, ObjectFieldBoolean ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;

            Toggled += ChangedEvent;
            Row = GridForm.AttachEntryField(ObjectEntry, this);
            }

        private static void ChangedEvent(object Sender, EventArgs E) {
            var FormWidgetBoolean = Sender as FormWidgetBoolean;
            if (FormWidgetBoolean == null) return;
            FormWidgetBoolean.UserChangedValue = true;
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Active = ObjectEntry.Value;
            _Changed = false;
            }

        }


    public partial class FormWidgetInteger : Gtk.Entry, IFieldBinding {
        ObjectFieldInteger ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetInteger(Object Object, ObjectFieldInteger ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(ObjectEntry, this);
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            try {
                ObjectEntry.Test = Convert.ToInt32(Text);
                ObjectEntry.IsValid = true;
                }
            catch {
                ObjectEntry.IsValid = false;
                }
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Text = ObjectEntry.Value.ToString ();
            _Changed = false;
            }

        }

    public partial class FormWidgetString : Gtk.Entry, IFieldBinding {
        ObjectFieldString ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetString(Object Object, ObjectFieldString ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(ObjectEntry, this);
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Text;
            }


        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Text = ObjectEntry.Value;
            _Changed = false;
            }

        }


    /// <summary>
    /// A secret form widget field is exactly the same as a string field with the
    /// Visibility flag set to false.
    /// </summary>
    public partial class FormWidgetSecret : FormWidgetString {
        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetSecret(Object Object, ObjectFieldSecret ObjectEntry,
                    GridForm GridForm) : base(Object, ObjectEntry, GridForm) {
            Visibility = false;
            }
        }


    public partial class FormWidgetSet : Gtk.TreeView, IFieldBinding {
        ObjectFieldSet ObjectEntry;


        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetSet(Object Object, ObjectFieldSet ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(ObjectEntry, this);
            }



        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            }



        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {

            _Changed = false;
            }

        }

    public partial  class FormWidgetChooser : Gtk.ComboBox, IFieldBinding {
        ObjectFieldChooser ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetChooser(Object Object, ObjectFieldChooser ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(ObjectEntry, this);
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            }



        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            _Changed = false;
            }



        }

    public partial class FormWidgetList : Gtk.TreeView, IFieldBinding {
        ObjectFieldList ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetList(Object Object, ObjectFieldList ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            _Changed = false;
            }
        }


    }