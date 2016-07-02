using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {

    public class FormWidgetText : Gtk.Label {
        ObjectText ObjectEntry;

        /// <summary>
        /// Construct a checkbox widget and label and attach to associated grid.
        /// </summary>
        /// <param name="Object">Data model.</param>
        /// <param name="ObjectEntry">Field description.</param>
        /// <param name="GridForm">Grid in which to display item.</param>
        public FormWidgetText(Object Object, ObjectText ObjectEntry,
                    GridForm GridForm) {
            this.ObjectEntry = ObjectEntry;
            Text = ObjectEntry.Text;
            Wrap = true;
            GridForm.AttachText(ObjectEntry, this);
            }
        }

    public class ErrorLabel : Gtk.Label {

        public ErrorLabel(GridForm GridForm, int Row, string Text) {
            this.Text = Text; // set the output text.
            GridForm.AttachError(this, Row);
            }


        public static void Raise(ref ErrorLabel ErrorLabel, GridForm GridForm, int Row,
                string Text) {

            if (ErrorLabel != null) {
                ErrorLabel.Destroy();
                }

            ErrorLabel = new ErrorLabel(GridForm, Row, Text);

            }

        }

    public class GridForm : Gtk.Grid {
        public List<IFieldBinding> Widgets = new List<IFieldBinding>();
        public Object Object;

        int Row = 0;

        public GridForm() {
            }

        public GridForm(Object Object, List<ObjectEntry> Entries) {
            this.Object = Object;
            Attach(Object, Entries);
            }

        // Attach error label to existing row
        public void AttachError(ErrorLabel ErrorLabel, int Row) {
            Attach(ErrorLabel, 3, Row, 1, 1);
            ErrorLabel.Show();
            }

        // Attach a widget in the standard alignment
        public int AttachText(ObjectEntry ObjectEntry, Gtk.Widget Widget) {
            Attach(Widget, 0, Row, 3, 1);

            return Row++;
            }

        // Attach a widget in the standard alignment
        public int AttachEntryField(Widget TrojanWidget, Gtk.Widget Widget) {
            var Label = new Gtk.Label(TrojanWidget.ObjectField.Label) {
                Halign = Gtk.Align.Start,
                Valign = Gtk.Align.Start
                };
            Attach(Label, 0, Row, 2, 1);
            Attach(Widget, 2, Row, 1, 1);
            Widgets.Add(TrojanWidget as IFieldBinding);

            return Row++;
            }

        // Attach a widget in the standard alignment
        public int AttachActionField(ObjectAction ObjectEntry, Gtk.Widget Widget) {
            var Label = new Gtk.Label(ObjectEntry.Text) {
                Halign = Gtk.Align.Start,
                Valign = Gtk.Align.Start
                };
            Attach(Label, 2, Row, 1, 1);
            Attach(Widget, 1, Row, 1, 1);

            return Row++;
            }


        /// <summary>
        /// Attach an 'options style' widget with a checkbox that causes
        /// the dependent content to be enabled/disabled.
        /// </summary>
        /// <param name="ObjectEntry"></param>
        /// <param name="Widget"></param>
        /// <param name="Grid"></param>
        public int AttachCheckboxGrid(Widget TrojanWidget, 
                    ObjectEntry ObjectEntry, Gtk.Widget Widget, GridForm Grid) {
            Widgets.Add(TrojanWidget as IFieldBinding);

            var Label = new Gtk.Label(ObjectEntry.Label) {
                Halign = Gtk.Align.Start,
                Valign = Gtk.Align.Start
                };
            Attach(Label, 0, Row, 1, 1);
            Attach(Widget, 1, Row, 1, 1);
            Row++;

            return Row++;
            
            }


        /// <summary>
        /// Attach a labeled subgrid such as an enumeration set or a sub-item
        /// </summary>
        /// <param name="ObjectEntry"></param>
        /// <param name="Grid"></param>
        public int AttachGrid(Widget TrojanWidget, ObjectEntry ObjectEntry, GridForm Grid) {
            Widgets.Add(TrojanWidget as IFieldBinding);
            var Label = new Gtk.Label(ObjectEntry.Label) {
                Halign = Gtk.Align.Start,
                Valign = Gtk.Align.Start
                };
            Attach(Label, 0, Row, 1, 1);
            Row++;
            Attach(Grid, 1, Row, 2, 1);
            return Row++;
            }





        public void Attach(Object Object, List<ObjectEntry> Entries) {


            foreach (var Entry in Entries) {
                IFieldBinding New = null;


                if (Entry as ObjectText != null) {
                    var NewText = new FormWidgetText(Object,
                        (Entry as ObjectText), this);
                    }
                else if (Entry as ObjectAction != null) {
                    var NewAction = new FormWidgetAction(Object,
                        (Entry as ObjectAction), this);
                    }

                else if (Entry as ObjectFieldEnumerate != null) {
                    New = new FormWidgetEnumerate(Object,
                       (Entry as ObjectFieldEnumerate), this);
                    }

                else if (Entry as ObjectFieldBoolean != null) {
                    New = new FormWidgetBoolean(Object,
                        (Entry as ObjectFieldBoolean), this);
                    }
                else if (Entry as ObjectFieldInteger != null) {
                    New = new FormWidgetInteger(Object,
                        (Entry as ObjectFieldInteger), this);
                    }
                else if (Entry as ObjectFieldString != null) {
                    New = new FormWidgetString(Object,
                        (Entry as ObjectFieldString), this);
                    }
                else if (Entry as ObjectFieldSecret != null) {
                    New = new FormWidgetSecret(Object,
                        (Entry as ObjectFieldSecret), this);
                    }
                else if (Entry as ObjectFieldOption != null) {
                    New = new FormWidgetOption(Object,
                        (Entry as ObjectFieldOption), this);
                    }
                else if (Entry as ObjectFieldSet != null) {
                    New = new FormWidgetSet(Object,
                        (Entry as ObjectFieldSet), this);
                    }
                else if (Entry as ObjectFieldChooser != null) {
                    New = new FormWidgetChooser(Object,
                        (Entry as ObjectFieldChooser), this);
                    }
                else if (Entry as ObjectFieldList != null) {
                     New = new FormWidgetList(Object,
                        (Entry as ObjectFieldList), this);
                    }
                else if (Entry as ObjectFieldItem != null) {
                    New = new FormWidgetItem(Object,
                        (Entry as ObjectFieldItem), this);
                    }

                else if (Entry as ObjectFieldFont != null) {
                     New = new FormWidgetFont(Object,
                        (Entry as ObjectFieldFont), this);
                    }
                else if (Entry as ObjectFieldColor != null) {
                     New = new FormWidgetColor(Object,
                        (Entry as ObjectFieldColor), this);
                    }
                else if (Entry as ObjectFieldDate != null) {
                     New = new FormWidgetDate(Object,
                        (Entry as ObjectFieldDate), this);
                    }
                else if (Entry as ObjectFieldTime != null) {
                     New = new FormWidgetTime(Object,
                        (Entry as ObjectFieldTime), this);
                    }
                else if (Entry as ObjectFieldFile != null) {
                     New = new FormWidgetFile(Object,
                        (Entry as ObjectFieldFile), this);
                    }
                else {
                    Console.WriteLine("Ooops");
                    }

                if (New != null) {
                    var ObjectField = Entry as ObjectField;
                    New.ReadOnly = ObjectField.ReadOnly;
                    New.Tip = ObjectField.Tip;
                    ObjectField.FieldBindings.Add(New);

                    }


                }
            }


        /// <summary>
        /// Copy entry widget values into Values[1] and return the
        /// result from Object.Valid.
        /// </summary>
        /// <returns></returns>
        public virtual bool Valid() {
            foreach (var Entry in Widgets) {
                Entry.Test();
                }

            return Object.Valid();
            }

        /// <summary>
        /// Copy widget values into Values[1] and check for validity.
        /// If invalid, return false, otherwise copy values to Value
        /// and call Dispatch.
        /// </summary>
        /// <returns></returns>
        public virtual bool Apply() {
            if (!Valid()) {
                return false;
                }

            foreach (var Entry in Widgets) {
                Entry.Apply();
                }

            return Object.Dispatch(); ;
            }

        /// <summary>
        /// Copy values from Value to Widget.
        /// </summary>
        public virtual void Fill() {
            foreach (var Entry in Widgets) {
                Entry.Fill();
                }
            }

        }

    }
