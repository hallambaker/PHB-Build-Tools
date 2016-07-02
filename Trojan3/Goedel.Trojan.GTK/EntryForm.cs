using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    public static class GTKExtensions {
        public static void SetAlignment(this Gtk.Widget Widget,
                Gtk.Align Halign, Gtk.Align Valign) {
            Widget.Halign = Halign;
            Widget.Valign = Valign;
            Widget.Hexpand = false;
            Widget.Vexpand = false;
            }

        }


    /// <summary>
    /// A form contains the following widgets:
    ///    1) An edit toggle. If selected, this permits the work area to be edited
    ///    2) The work area
    ///    3) Update / Cancel Buttons
    /// </summary>

    public class EntryForm : Gtk.Grid{

        public static uint PaddingFieldX = 20;
        public static uint PaddingFieldY = 5;
        public static uint PaddingForm = 5;

        Gtk.CheckButton Editable;
        //Gtk.Table Table;
        Gtk.HButtonBox _ActionBox = null;
        Gtk.Button BConfirm;
        Gtk.Button BApply;
        Gtk.Button BCancel;

        GridForm FieldsGrid;


        bool _EditMode = false;
        public bool EditMode {
            get {
                return _EditMode;
                }
            set {
                _EditMode = value;
                }
            }

        bool ActionBoxVisible = false;

        public EntryForm(Object Object) {
            Editable = new Gtk.CheckButton();
            Editable.Active = false;
            Editable.Toggled += SetEditable;

            FieldsGrid = new GridForm (Object, Object.Entries);



            //MainGrid = new Gtk.Grid();
            Editable.SetAlignment(Gtk.Align.Start, Gtk.Align.Start);
            Attach(Editable, 0, 0, 1, 1);

            FieldsGrid.SetAlignment(Gtk.Align.Start, Gtk.Align.Start);
            Attach(FieldsGrid, 0, 1, 1, 1);

            SetEditable();
            }

        void SetEditable (object Sender, EventArgs args) {
            SetEditable();
            }

        void SetEditable() {
            if (Editable.Active) {
                if (!ActionBoxVisible) {
                    Attach(ActionBox, 0, 2, 1, 1);
                    ActionBoxVisible = true;
                    }
                }
            else {
                if (ActionBoxVisible) {
                    Remove(ActionBox);
                    ActionBoxVisible = false;
                    }
                }
            ShowAll();
            }

        Gtk.HButtonBox ActionBox {
            get {
                if (_ActionBox == null) {
                    _ActionBox = new Gtk.HButtonBox();
                    BConfirm = new ButtonConfirm(this);
                    BApply = new ButtonApply(this);
                    BCancel = new ButtonCancel(this);
                    _ActionBox.Add(BConfirm);
                    _ActionBox.Add(BApply);
                    _ActionBox.Add(BCancel);
                    _ActionBox.SetAlignment(Gtk.Align.Center, Gtk.Align.Start);
                    }
                return _ActionBox;
                }
            }



        public bool Valid() {
            return FieldsGrid.Valid();
            }

        public void Apply() {
            FieldsGrid.Apply();
            }

        public void Fill() {
            FieldsGrid.Fill();
            }
        
        class ButtonConfirm : Gtk.Button {
            EntryForm EntryForm;
            public ButtonConfirm(EntryForm EntryForm) : base("Confirm") {
                this.EntryForm = EntryForm;
                Pressed += ClickedEventHandler;
                }

            void ClickedEventHandler(object sender, EventArgs e) {
                EntryForm.Apply();
                EntryForm.EditMode = false;
                }
            }

        class ButtonApply : Gtk.Button {
            EntryForm EntryForm;
            public ButtonApply(EntryForm EntryForm) : base("Apply") {
                this.EntryForm = EntryForm;
                Pressed += ClickedEventHandler;
                }

            void ClickedEventHandler(object sender, EventArgs e) {
                EntryForm.Apply();
                }
            }

        class ButtonCancel : Gtk.Button {
            EntryForm EntryForm;
            public ButtonCancel(EntryForm EntryForm) : base("Cancel") {
                this.EntryForm = EntryForm;
                Pressed += ClickedEventHandler;
                }

            void ClickedEventHandler(object sender, EventArgs e) {
                EntryForm.Fill();
                }
            }

        }






    }
