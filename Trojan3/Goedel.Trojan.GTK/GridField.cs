//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Goedel.Trojan.GTK {
//    class GridField : Gtk.Grid {

//        //List<FormField> FormFields = new List<FormField>();

//        public GridField (Object Object, List<ObjectEntry> ObjectEntries) {

//            int Row = 0;

//            foreach (var Entry in ObjectEntries) {
//                if (Entry as ObjectField != null) {
//                    var Label = new Gtk.Label(Entry.Label) {
//                        Halign = Gtk.Align.Start,
//                        Valign = Gtk.Align.Start
//                        };
//                    var Widget = MakeFieldWidget(Object, Entry);

//                    if (Widget != null) {
//                        Widget.SetAlignment(Gtk.Align.Start, Gtk.Align.Start);
//                        }

//                    Attach(Label, 0, Row, 2, 1);
//                    AttachNextTo(Widget, Label, Gtk.PositionType.Right, 1, 1);

//                    Row++;
//                    }
//                else if (Entry as ObjectOption != null) {
//                    //var OptionField = new FormFieldOption(this, Object, Entry as ObjectOption);
//                    }
//                else if (Entry as ObjectCommand != null) {

//                    }
//                }

//            }


//        private Gtk.Widget MakeFieldWidget(Object Object, ObjectEntry Entry) {
//            var ObjectField = Entry as ObjectField;
//            if (ObjectField == null) return null;

//            FormField FormField = null;
//            switch (ObjectField.Type) {
//                case WidgetType.String: {
//                    FormField = new FormFieldString(Object, ObjectField);
//                    break;
//                    }
//                case WidgetType.Integer: {
//                    FormField = new FormFieldInteger(Object, ObjectField);
//                    break;
//                    }
//                case WidgetType.Secret: {
//                    FormField = new FormFieldSecret(Object, ObjectField);
//                    break;
//                    }
//                case WidgetType.Boolean: {
//                    FormField = new FormFieldBoolean(Object, ObjectField);
//                    break;
//                    }

//                case WidgetType.Set: {
//                    //FormField = new FormField (Object, ObjectField);
//                    break;
//                    }
//                case WidgetType.List: {
//                    //FormField = new FormField (Object, ObjectField);
//                    break;
//                    }
//                case WidgetType.DateTime: {
//                    //FormField = new FormField (Object, ObjectField);
//                    break;
//                    }
//                }

//            if (FormField != null) {
//                FormFields.Add(FormField);
//                }

//            return FormField.Widget;
//            }


//        }
//    }
