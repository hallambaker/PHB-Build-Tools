using System;
using System.Collections.Generic;

namespace Goedel.Trojan {




    public class ObjectFieldBoolean : ObjectField {
        public bool Test { get; set; }
        public bool Value {get; set; }
        }

    public class ObjectFieldInteger : ObjectField {
        public int Test { get; set; }
        public int Value { get; set; }
        public bool Valid { get; set; }
        }

    public class ObjectFieldString : ObjectField {
        public string Test { get; set; }
        public string Value { get; set; }
        }

    public class ObjectFieldSecret : ObjectFieldString {
        }

    public class ObjectFieldDateTime : ObjectField {
        public DateTime Test { get; set; }
        public DateTime Value { get; set; }
        }


    public class ObjectFieldSet : ObjectField {
        public List<Object> Test { get; set; }
        public List<Object> Value { get; set; }

        }

    public class ObjectFieldChooser : ObjectField {
        public List<string> Test { get; set; }
        public List<string> Value { get; set; }
        public List<string> Options { get; set; }
        }

    public class ObjectFieldList : ObjectField {
        public List<Object> Test { get; set; }
        public List<Object> Value { get; set; }
        }


    public class ObjectFieldItem : ObjectField {
        public Object Test { get; set; }
        public Object Value { get; set; }
        }

    public class ObjectFieldOption : ObjectField {
        public bool Test { get; set; }
        public bool Value { get; set; }
        public List<ObjectEntry> Entries { get; set; }
        }

    public class ObjectFieldEnumerate : ObjectField {
        public int Test { get; set; }
        public int Value { get; set; }
        public List<ObjectEntry> Entries { get; set; }
        }

    public class ObjectFieldRadio : ObjectFieldBoolean {
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