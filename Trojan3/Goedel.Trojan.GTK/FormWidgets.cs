using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {
    public interface IFormWidget {

        void Test();

        void Apply();

        void Fill();

        }

    public class GridForm : Gtk.Grid {
        public List<IFormWidget> Widgets;


        public int Row = 0;

        public GridForm() {
            }

        public GridForm(Object Object, List<ObjectEntry> Entries) {
            Attach(Object, Entries);
            }

        // Attach a widget in the standard alignment
        public void AttachEntryField(ObjectEntry ObjectEntry, Gtk.Widget Widget) {
            var Label = new Gtk.Label(ObjectEntry.Label) {
                Halign = Gtk.Align.Start,
                Valign = Gtk.Align.Start
                };
            Attach(Label, 0, Row, 1, 1);
            Attach(Widget, 1, Row, 1, 1);
            Row++;
            Widgets.Add(Widget as IFormWidget);
            }

        public void Attach(Object Object, List<ObjectEntry> Entries) {
            foreach (var Entry in Entries) {
                if (Entry as ObjectFieldBoolean != null) {
                    var New = new FormWidgetBoolean(Object,
                        (Entry as ObjectFieldBoolean), this);
                    }
                else if (Entry as ObjectFieldInteger != null) {
                    var New = new FormWidgetInteger(Object,
                        (Entry as ObjectFieldInteger), this);
                    }
                else if (Entry as ObjectFieldString != null) {
                    var New = new FormWidgetString(Object,
                        (Entry as ObjectFieldString), this);
                    }
                else if (Entry as ObjectFieldSecret != null) {
                    var New = new FormWidgetSecret(Object,
                        (Entry as ObjectFieldSecret), this);
                    }
                else if (Entry as ObjectFieldSet != null) {
                    var New = new FormWidgetSet(Object,
                        (Entry as ObjectFieldSet), this);
                    }
                else if (Entry as ObjectFieldChooser != null) {
                    var New = new FormWidgetChooser(Object,
                        (Entry as ObjectFieldChooser), this);
                    }
                else if (Entry as ObjectFieldList != null) {
                    var New = new FormWidgetList(Object,
                        (Entry as ObjectFieldList), this);
                    }
                else if (Entry as ObjectFieldItem != null) {
                    var New = new FormWidgetItem(Object,
                        (Entry as ObjectFieldItem), this);
                    }
                else if (Entry as ObjectFieldEnumerate != null) {
                    var New = new FormWidgetEnumerate(Object,
                        (Entry as ObjectFieldEnumerate), this);
                    }
                else if (Entry as ObjectFieldRadio != null) {
                    var New = new FormWidgetRadio(Object,
                        (Entry as ObjectFieldRadio), this);
                    }
                else if (Entry as ObjectFieldFont != null) {
                    var New = new FormWidgetFont(Object,
                        (Entry as ObjectFieldFont), this);
                    }
                else if (Entry as ObjectFieldColor != null) {
                    var New = new FormWidgetColor(Object,
                        (Entry as ObjectFieldColor), this);
                    }
                else if (Entry as ObjectFieldDate != null) {
                    var New = new FormWidgetDate(Object,
                        (Entry as ObjectFieldDate), this);
                    }
                else if (Entry as ObjectFieldTime != null) {
                    var New = new FormWidgetTime(Object,
                        (Entry as ObjectFieldTime), this);
                    }
                else if (Entry as ObjectFieldFile != null) {
                    var New = new FormWidgetFile(Object,
                        (Entry as ObjectFieldFile), this);
                    }
                }
            }


        public void Test() {
            foreach (var Entry in Widgets) {
                Entry.Test();
                }
            }

        public void Apply() {
            foreach (var Entry in Widgets) {
                Entry.Apply();
                }
            }

        public void Fill() {
            foreach (var Entry in Widgets) {
                Entry.Fill();
                }
            }

        }


    public class FormWidgetBoolean : Gtk.CheckButton, IFormWidget {
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
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetInteger : Gtk.Entry, IFormWidget {
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
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            try {
                ObjectEntry.Test = Convert.ToInt32(Text);
                ObjectEntry.Valid = true;
                }
            catch {
                ObjectEntry.Valid = false;
                }
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {

            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {

            }

        }
    public class FormWidgetString : Gtk.Entry, IFormWidget {
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
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Text;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            ObjectEntry.Value = Text;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Text = ObjectEntry.Value;
            }

        }
    public class FormWidgetSecret : Gtk.Entry, IFormWidget {
        ObjectFieldSecret ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetSecret(Object Object, ObjectFieldSecret ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Visibility = false;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Text;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            ObjectEntry.Value = Text;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Text = ObjectEntry.Value;
            }

        }

    /// <summary>
    /// As currently defined, this widget is really only suited to output
    /// </summary>
    public class FormWidgetDateTime : Gtk.Entry, IFormWidget {
        ObjectFieldDateTime ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetDateTime(Object Object, ObjectFieldDateTime ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {

            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Text = ObjectEntry.Value.ToString();
            }

        }


    public class FormWidgetSet : Gtk.CheckButton, IFormWidget {
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
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {

            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {

            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {

            }

        }
    public class FormWidgetChooser : Gtk.CheckButton, IFormWidget {
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
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetList : Gtk.CheckButton, IFormWidget {
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
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetItem : Gtk.CheckButton, IFormWidget {
        ObjectFieldItem ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetItem(Object Object, ObjectFieldItem ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetOption : Gtk.CheckButton, IFormWidget {
        ObjectFieldOption ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetOption(Object Object, ObjectFieldOption ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetEnumerate : Gtk.CheckButton, IFormWidget {
        ObjectFieldEnumerate ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetEnumerate(Object Object, ObjectFieldEnumerate ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetRadio : Gtk.CheckButton, IFormWidget {
        ObjectFieldRadio ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetRadio(Object Object, ObjectFieldRadio ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetDate : Gtk.CheckButton, IFormWidget {
        ObjectFieldDate ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetDate(Object Object, ObjectFieldDate ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }
    public class FormWidgetTime : Gtk.CheckButton, IFormWidget {
        ObjectFieldTime ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetTime(Object Object, ObjectFieldTime ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }

    public class FormWidgetFont : Gtk.CheckButton, IFormWidget {
        ObjectFieldFont ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetFont(Object Object, ObjectFieldFont ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }

    public class FormWidgetColor : Gtk.CheckButton, IFormWidget {
        ObjectFieldColor ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetColor(Object Object, ObjectFieldColor ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }

    public class FormWidgetFile : Gtk.CheckButton, IFormWidget {
        ObjectFieldFile ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetFile(Object Object, ObjectFieldFile ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            GridForm.AttachEntryField(ObjectEntry, this);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            //ObjectEntry.Value = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public void Fill() {
            //Active = ObjectEntry.Value;
            }

        }

    }
