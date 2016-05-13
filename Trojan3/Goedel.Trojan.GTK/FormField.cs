//using System;
//using System.Collections.Generic;
//using Goedel.Trojan;
//using Gtk;

//namespace Goedel.Trojan.GTK {
//    public abstract class FormField : Widget {
//        public Object Object { get; set; }
//        public ObjectField ObjectField { get; set; }
//        public virtual Gtk.Widget Widget { get; set; }



//        bool _EditMode = false;
//        public virtual bool EditMode {
//            get {
//                return _EditMode;
//                }

//            set {
//                _EditMode = value;
//                }
//            }


//        public FormField(Object Object, ObjectEntry ObjectEntry) {
//            this.Object = Object;
//            this.ObjectField = ObjectEntry as ObjectField;
//            }

//        public abstract void Apply();
//        public abstract void Cancel();
//        }


//    public class FormFieldOption : FormField {
//        bool _EditMode = false;
//        public override bool EditMode {
//            get {
//                return _EditMode;
//                }
//            set {
//                _EditMode = value;
//                // enable the value here
//                }
//            }

//        protected Gtk.Grid _Widget;
//        Gtk.CheckButton Select;


//        public FormFieldOption(EntryForm EntryForm, Object Object, ObjectOption ObjectOption) :
//             base (Object, ObjectOption) {

//            _Widget = new Grid();
//            Select = new CheckButton();

//            _Widget.Attach(Select, 0, 0, 1, 1);

//            Gtk.Widget Last = Select;
//            EntryForm.FillGrid(_Widget, Object, ObjectOption.Entries);
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }



//    public abstract class FormFieldEntry : FormField {
//        bool _EditMode = false;
//        public override bool EditMode {
//            get {
//                return _EditMode;
//                }
//            set {
//                _EditMode = value;
//                _Widget.IsEditable = value;
//                }
//            }

//        protected Gtk.Entry _Widget;
//        public override Gtk.Widget Widget {
//            get { return _Widget; }
//            }

//        public FormFieldEntry(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            _Widget = new Gtk.Entry(FieldValueAsString);
//            }

//        public override void Apply() {
//            FieldValueAsString = _Widget.Text;
//            }

//        public override void Cancel() {
//            _Widget.Text = FieldValueAsString;
//            }


//        protected virtual string FieldValueAsString {
//            get { return Object.GetField(ObjectField.Index) as string; }
//            set { Object.SetField(ObjectField.Index, value); }
//            }

//        }

//    public class FormFieldString : FormFieldEntry {
//        public FormFieldString(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            var Value = Object.GetField(ObjectField.Index) as string;
//            _Widget = new Gtk.Entry(Value);
//            }
//        }

//    public class FormFieldSecret : FormFieldEntry {
//        public FormFieldSecret(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            var Value = Object.GetField(ObjectField.Index) as string;
//            _Widget = new Gtk.Entry(Value) {
//                Visibility = false
//                };
//            }
//        }

//    public class FormFieldInteger : FormFieldEntry {
//        public FormFieldInteger(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            var Value = Object.GetField(ObjectField.Index) as int?;
//            var Text = Value.ToString();
//            _Widget = new Gtk.Entry(Text);
//            }

//        protected override string FieldValueAsString {
//            get {
//                var Value = Object.GetField(ObjectField.Index) as int?;
//                return Value.ToString();
//                }
//            set {
//                var Value = (int?)Convert.ToInt32(value);
//                Object.SetField(ObjectField.Index, Value);
//                }
//            }
//        }

//    public class FormFieldBoolean : FormField {

//        bool _EditMode = false;
//        public override bool EditMode {
//            get {
//                return _EditMode;
//                }
//            set {
//                _EditMode = value;
//                _Widget.Sensitive = value;
//                }
//            }

//        protected Gtk.CheckButton _Widget;
//        public override Gtk.Widget Widget {
//            get { return _Widget; }
//            }

//        public FormFieldBoolean(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            _Widget = new Gtk.CheckButton();
//            Cancel();
//            }

//        public override void Apply() {
//            //Object.SetField(ObjectField.Index, _Widget.Active);
//            }

//        public override void Cancel() {
//            //var Value = Object.GetField(ObjectField.Index) as bool?;
//            //_Widget.Active = Value == null ? false : (bool)Value;
//            }
//        }


//    public class FormFieldSet : FormField {
//        public FormFieldSet(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }

//    public class FormFieldList : FormField {
//        public FormFieldList(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }


//    public class FormFieldDate : FormField {
//        public FormFieldDate(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }

//    public class FormFieldDateTime : FormField {
//        public FormFieldDateTime(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }





//    public class FormFieldCommand : FormField {
//        public FormFieldCommand(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }






//    /*
//     * For future use
//     */

//    public class FormFieldFont : FormField {
//        public FormFieldFont(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }

//    public class FormFieldColour : FormField {
//        public FormFieldColour(Object Object, ObjectField ObjectField) :
//                    base(Object, ObjectField) {
//            }

//        public override void Apply() { }
//        public override void Cancel() { }
//        }

//    }
