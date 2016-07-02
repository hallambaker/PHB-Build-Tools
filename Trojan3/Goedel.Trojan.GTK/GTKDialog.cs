using System;
using System.Collections.Generic;
using Goedel.Trojan;
using Gtk;

namespace Goedel.Trojan.GTK {


    /// <summary>
    /// Present a modal dialog box.
    /// </summary>
    public class GTKDialog : Gtk.Window {

        Grid GridMain = null;
        NavigateGrid GridNavigate = null;
        GridForm GridField = null;
        Object Object;

        /// <summary>
        /// Free up all the resources used.
        /// </summary>
        ~GTKDialog() {
            if (GridMain != null) GridMain.Destroy();
            if (GridField != null) GridField.Destroy();
            if (GridNavigate != null) GridNavigate.Destroy();
            }

        /// <summary>
        /// Construct a modal dialog box for the specified object with the usual
        /// action buttons.
        /// </summary>
        /// <param name="Object">The object to be bound to the dialog.</param>
        public GTKDialog(Object Object) : base (Object.Title) {
            this.Object = Object;

            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);
            DeleteEvent += OnDeleteEvent;

            GridMain = new Grid();
            Add(GridMain);

            GridField = new GridForm(Object, Object.Entries);
            GridNavigate = new NavigateGrid(this);

            GridMain.Attach(GridField, 0, 0, 1, 1);
            GridMain.Attach(GridNavigate, 0, 1, 1, 1);

            ShowAll();
            }

        public void OnDeleteEvent(object sender, EventArgs args) {
            ActionCancel();
            }


        enum ButtonType {
            OK = 0, Cancel = 1, Apply = 2
            }
        readonly static string[] ButtonLabels = new string[] { "OK", "Cancel", "Apply" };


        private class NavigateGrid : Grid {
            public NavigateGrid(GTKDialog GTKDialog) {
                var ButtonOK = new DialogButton(GTKDialog, ButtonType.OK);
                var ButtonCancel = new DialogButton(GTKDialog, ButtonType.Cancel);
                var ButtonApply = new DialogButton(GTKDialog, ButtonType.Apply);

                Attach(ButtonOK, 1, 0, 1, 1);
                Attach(ButtonCancel, 2, 0, 1, 1);
                Attach(ButtonApply, 3, 0, 1, 1);
                }
            }

        private class DialogButton : Button {
            GTKDialog GTKDialog;
            ButtonType ButtonType;

            public DialogButton(GTKDialog GTKDialog, ButtonType ButtonType) {
                this.GTKDialog = GTKDialog;
                this.ButtonType = ButtonType;
                Label = ButtonLabels[(int)ButtonType];

                Clicked += OnClicked;
                
                }


            public void OnClicked(object sender, EventArgs args) {
                switch (ButtonType) {
                    case ButtonType.OK: {
                        GTKDialog.ActionOK();
                        break;
                        }
                    case ButtonType.Cancel: {
                        GTKDialog.ActionCancel();
                        break;
                        }
                    case ButtonType.Apply: {
                        GTKDialog.ActionApply();
                        break;
                        }
                    }
                }
            }

        internal void ActionOK () {
            if (GridField.Valid()) {
                GridField.Fill();
                Object.Dispatch();
                Destroy();
                }
            }

        internal void ActionCancel() {
            Destroy();
            }

        internal void ActionApply() {
            if (GridField.Valid()) {
                GridField.Fill();
                Object.Dispatch();
                }
            }

        }
    }
