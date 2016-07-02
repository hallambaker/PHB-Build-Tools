using System;
using System.Collections.Generic;

namespace Goedel.Trojan {

    public enum FieldModeInteger  {
        Entry = 1,
        Slider = 2
        }

    public enum FieldModeString {
        Entry = 1,
        Secret = 2,
        Paragraph = 3,
        RichText = 4
        }


    public class ObjectFieldBoolean : ObjectField {

        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public bool Value;

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public bool Test;

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }
        }

    public class ObjectFieldInteger : ObjectField {

        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public int Value = 0;

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public int Test;

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }

        public int Maximum = Int32.MaxValue;
        public int Minimum = Int32.MinValue;
        public int Step = 1;
        public int Digits = 0;
        public int Length = 12;

        public FieldModeInteger Mode = FieldModeInteger.Entry;
        public bool IsValid = true;



        }

    public class ObjectFieldString : ObjectField {
        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public string Value;

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public string Test;

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }

        public int LengthMaximum = Int32.MaxValue;
        public int Length = 32;
        public FieldModeString Mode ;

        }

    public class ObjectFieldSecret : ObjectFieldString {
        }

    public class ObjectFieldDateTime : ObjectField {
        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public DateTime Value;

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public DateTime Test;

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }

        }


    public class ObjectFieldSet : ObjectField {
        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public List<Object> Value;

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public List<Object> Test;

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }

        }

    public class ObjectFieldChooser : ObjectField {
        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public List<string> Value = new List<string>();

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public List<string> Test = new List<string>();

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }

        public List<string> Options { get; set; }
        }

    public class ObjectFieldList : ObjectField {
        public Object Prototype;

        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public List<Object> Value = new List<Object> ();

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// </summary>
        public List<Object> Test = new List<Object>();

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            Value = Test;
            }
        }


    public class ObjectFieldItem : ObjectField {
        /// <summary>
        /// The value of the field after validity checking.
        /// </summary>
        public Object Value;

        /// <summary>
        /// The value of the field on which validity checks are made.
        /// This is identical to Value for this item.
        /// </summary>
        public Object Test { get { return Value; } set { Value = value; } }


        /// <summary>
        /// The list of sub fields in the option set.
        /// </summary>
        public List<ObjectEntry> Entries { get; set; }


        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            }



        }

    public class ObjectFieldOption : ObjectField {

        public bool Value;

        /// <summary>
        /// The list of sub fields in the option set.
        /// </summary>
        public List<ObjectEntry> Entries { get; set; }


        /// <summary>
        /// The value of the field on which validity checks are made.
        /// This is identical to Value for this item.
        /// </summary>
        public bool Test { get { return Value; } set { Value = value; } }

        /// <summary>
        /// Copy Test to Value.
        /// </summary>
        public override void Apply() {
            }

        }

    public class ObjectFieldEnumerate : ObjectFieldInteger {

        /// <summary>
        /// The enumeration fields
        /// </summary>
        public List<ObjectEntry> Entries { get; set; }
        }

    public class ObjectFieldRadio : ObjectFieldBoolean {

        /// <summary>
        /// The value that the output key will be set to if the selection is chosen
        /// </summary>
        public int SelectionValue;
        }


    /*
     * Currently unused, for future use.
     */
    public class ObjectFieldFont : ObjectFieldBoolean {
        }
    public class ObjectFieldColor : ObjectFieldBoolean {
        }
    public class ObjectFieldDate : ObjectFieldDateTime {
        }
    public class ObjectFieldTime : ObjectFieldDateTime {
        }
    public class ObjectFieldFile: ObjectFieldString {
        }
    }