using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {
    public partial class FormWidgetAction : Gtk.Button {
        ObjectAction ObjectEntry;
        Object Object;
        int Row;
        GridForm GridForm;


        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetAction(Object Object, ObjectAction ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            this.GridForm = GridForm;
            this.Object = Object;
            Clicked += OnClick;

            Label = ObjectEntry.Label;
            Row = GridForm.AttachActionField(ObjectEntry, this);
            }


        public void OnClick(object sender, EventArgs args) {
            Object.Model.Selected = Object;
            Object.Model.Dispatch(ObjectEntry.Id);
            }
        }
    }
