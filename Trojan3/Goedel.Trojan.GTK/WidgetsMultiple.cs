using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    public partial class FormWidgetChooser : Widget {
        ObjectFieldChooser ObjectEntry;
        Gtk.ComboBox Current = new Gtk.ComboBox();

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
        public FormWidgetChooser(Object Object, ObjectFieldChooser ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;

            if (ObjectEntry.Options != null) {
                Current = new Gtk.ComboBox(ObjectEntry.Options.ToArray());
                }
            else {
                Current = new Gtk.ComboBox(new string[] { "Empty" });
                }
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(this, Current);
            }


        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            }



        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {

            _Changed = false;
            }



        }


    public partial class FormWidgetList : Widget {
        ObjectFieldList ObjectEntry;
        Gtk.TreeView Current ;
        ListView ListView;


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
        public FormWidgetList(Object Object, ObjectFieldList ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;

            Current = new Gtk.TreeView();
            ListView = new ListView(Current, ObjectEntry.Prototype, ObjectEntry.Value);

            // Copy the data into the widget
            Fill();

            // Make the display widget.
            this.GridForm = GridForm;

            Row = GridForm.AttachEntryField(this, Current);
            }

        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            }



        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {
            ListView.BindData();
            _Changed = false;
            }

        }


    public partial class FormWidgetSet : Widget {
        ObjectFieldSet ObjectEntry;
        Gtk.TreeView Current;

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
        public FormWidgetSet(Object Object, ObjectFieldSet ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Current = new Gtk.TreeView();
            Fill();
            this.GridForm = GridForm;
            Row = GridForm.AttachEntryField(this, Current);
            }



        /// <summary>
        /// Copy values from widget to model test field.
        /// </summary>
        public override void Test() {
            }



        /// <summary>
        /// Copy value from model value field to widget.
        /// </summary>
        public override void Fill() {

            _Changed = false;
            }

        }



    }
