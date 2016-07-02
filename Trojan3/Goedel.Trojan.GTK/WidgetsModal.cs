using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    /// <summary>
    /// As currently defined, this widget is really only suited to output
    /// </summary>
    public partial class FormWidgetDateTime : Widget {
        ObjectFieldDateTime ObjectEntry;
        Gtk.Entry Current; 

        Gtk.HBox HBox;
        Gtk.Button Button;
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
        public FormWidgetDateTime(Object Object, ObjectFieldDateTime ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Fill();
            this.GridForm = GridForm;
            Current = new Gtk.Entry();

            Button = new Gtk.Button("Browse");

            HBox = new Gtk.HBox();
            HBox.PackStart(Current, true, false, 0);
            HBox.PackEnd(Button, true, false, 0);

            GridForm.AttachEntryField(this, HBox);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            //ObjectEntry.Test = Text;
            }


        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            Current.Text = ObjectEntry.Value.ToString() ;
            _Changed = false;
            }



        }


    public partial class FormWidgetDate : Widget {
        ObjectFieldDate ObjectEntry;
        Gtk.Entry Current;
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
        public FormWidgetDate(Object Object, ObjectFieldDate ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.Entry();
            Fill();
            this.GridForm = GridForm;
            GridForm.AttachEntryField(this, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            //ObjectEntry.Test = Active;
            }

        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            Current.Text = ObjectEntry.Value.ToString(); ;
            _Changed = false;
            }


        }
    public partial class FormWidgetTime : Widget {
        ObjectFieldTime ObjectEntry;
        Gtk.Entry Current;
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
        public FormWidgetTime(Object Object, ObjectFieldTime ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.Entry();
            Fill();
            this.GridForm = GridForm;
            GridForm.AttachEntryField(this, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            //ObjectEntry.Test = Active;
            }
        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            //Active = ObjectEntry.Value;
            _Changed = false;
            }

        }

    public partial class FormWidgetFont : Widget {
        ObjectFieldFont ObjectEntry;
        Gtk.Entry Current;
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
        public FormWidgetFont(Object Object, ObjectFieldFont ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.Entry();
            Fill();
            this.GridForm = GridForm;
            GridForm.AttachEntryField(this, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            //ObjectEntry.Test = Active;
            }


        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            //Active = ObjectEntry.Value;
            _Changed = false;
            }

        }

    public partial class FormWidgetColor : Widget {
        ObjectFieldColor ObjectEntry;
        Gtk.Entry Current;
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
        public FormWidgetColor(Object Object, ObjectFieldColor ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.Entry();
            Fill();
            this.GridForm = GridForm;
            GridForm.AttachEntryField(this, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            //ObjectEntry.Test = Active;
            }


        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            //Active = ObjectEntry.Value;
            _Changed = false;
            }

        }

    public partial class FormWidgetFile : Widget {
        ObjectFieldFile ObjectEntry;
        Gtk.Entry Current;
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
        public FormWidgetFile(Object Object, ObjectFieldFile ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.Entry();
            Fill();
            this.GridForm = GridForm;
            GridForm.AttachEntryField(this, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            //ObjectEntry.Test = Active;
            }



        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            //Active = ObjectEntry.Value;
            _Changed = false;
            }

        }

    }
