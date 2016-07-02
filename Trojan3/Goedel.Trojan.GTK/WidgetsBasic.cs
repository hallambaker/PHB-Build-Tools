using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    public partial class FormWidgetBoolean : Widget {
        ObjectFieldBoolean ObjectEntry;
        Gtk.CheckButton Current;

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
        public FormWidgetBoolean(Object Object, ObjectFieldBoolean ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.CheckButton();

            WidgetsAll.Add(Current);

            Fill();
            this.GridForm = GridForm;

            Current.Toggled += ChangedEvent;
            Row = GridForm.AttachEntryField(this, Current);
            }

        private static void ChangedEvent(object Sender, EventArgs E) {
            var FormWidgetBoolean = Sender as FormWidgetBoolean;
            if (FormWidgetBoolean == null) return;
            FormWidgetBoolean.UserChangedValue = true;
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


    public partial class FormWidgetInteger : Widget {
        ObjectFieldInteger ObjectEntry;

        Gtk.Widget Current;
        Gtk.Entry Entry = null;
        Gtk.HScale HScale = null;
        Gtk.Adjustment Range = null;
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
        public FormWidgetInteger(Object Object, ObjectFieldInteger ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;

            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(this, Current);
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {

            try {
                if (ObjectEntry.Mode == FieldModeInteger.Entry) {
                    ObjectEntry.Test = Convert.ToInt32(Entry.Text);
                    }
                else {
                    ObjectEntry.Test = Convert.ToInt32(HScale.Value);
                    }
                ObjectEntry.IsValid = true;
                }
            catch {
                ObjectEntry.IsValid = false;
                }
            }


        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            if (ObjectEntry.Mode == FieldModeInteger.Entry) {
                if (Entry == null) {
                    Entry = new Gtk.Entry();
                    WidgetsAll.Add(Entry);
                    }
                Entry.Text = ObjectEntry.Value.ToString();
                Entry.WidthChars = ObjectEntry.Length;
                Entry.Hexpand = false;
                Current = Entry;
                }
            else {
                if (Range == null) {
                    Range = new Gtk.Adjustment(ObjectEntry.Value, ObjectEntry.Minimum,
                    ObjectEntry.Maximum, ObjectEntry.Step, ObjectEntry.Step, ObjectEntry.Step);
                    }
                else {
                    Range.Lower = ObjectEntry.Minimum;
                    Range.Upper = ObjectEntry.Maximum;
                    Range.StepIncrement = ObjectEntry.Step;
                    }

                HScale = HScale != null ? HScale : new Gtk.HScale(Range);
                HScale.DrawValue = true;
                HScale.Digits = ObjectEntry.Digits;
                HScale.Value = ObjectEntry.Value;
                Current = HScale;
                WidgetsAll.Add(HScale);
                }
            _Changed = false;
            }

        }



    public partial class FormWidgetString : Widget {
        ObjectFieldString ObjectEntry;
        protected Gtk.Entry Current;

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
        public FormWidgetString(Object Object, ObjectFieldString ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.Entry();
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(this, Current);
            WidgetsAll.Add(Current);
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            ObjectEntry.Test = Current.Text;
            }


        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            Current.Text = ObjectEntry.Value;
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
            Current.Visibility = false;
            }
        }


    }