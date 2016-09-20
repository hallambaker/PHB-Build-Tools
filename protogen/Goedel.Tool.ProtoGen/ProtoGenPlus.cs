using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace Goedel.Tool.ProtoGen {
    public partial class _Choice {
        public bool Normalized = false;
        public string Default = null;
        public string DefaultC {
            get { return Default == null ? "NULL" : "\"" + Default + "\""; }
            }
        public bool Multiple = false;
        public bool Required = false;

        public string RequiredC {
            get { return Required ? "TRUE" : "FALSE"; }
            }

        public string ID = null;

        public string TypeC = null;
        public string TypeJ = null;
        public bool ByValue = true;

        public bool Parameterized = false;

        public string ThisInherits = ": global::Goedel.Protocol.JSONObject";

        // Classes that don't inherit internally are children of JSONObject
        // by default.
        public string ExternParent = "JSONObject";

        public int CountChildren = 0;

        public virtual void Normalize() {
            }

        public static List<_Choice> InheritEntries(List<_Choice> Input) {
            

            List<_Choice> Result = new List<_Choice>();

            foreach (_Choice Entry in Input) {
                Entry.Normalize();
                }

            foreach (_Choice Entry in Input) {
                if (Entry.GetType() == typeof(Inherits)) {
                    Inherits Inherits = (Inherits)Entry;
                    List<_Choice> Inheritance = null;

                    _Choice Parent = Inherits.Ref.Definition;
                    if (Parent != null) {
                        Parent.Normalize();
                        }

                    if (Parent.GetType() == typeof(Message)) {
                        Message Cast = (Message)Parent;
                        Inheritance = Cast.AllEntries;
                        }
                    else if (Parent.GetType() == typeof(Structure)) {
                        Structure Cast = (Structure)Parent;
                        Inheritance = Cast.AllEntries;
                        }

                    if (Inheritance != null) {
                        foreach (_Choice Inherit in Inheritance) {
                            Result.Add(Inherit);
                            }
                        }
                    }
                if (Entry.GetType() == typeof(Extern)) {
                    var Extern = Entry as Extern;
                    Entry.ExternParent = Extern.ToString();
                    }

                }

            foreach (_Choice Entry in Input) {
                if (Entry.GetType() != typeof(Inherits)) {
                    Result.Add(Entry);
                    }
                }

            return Result;
            }

        public static List<_Choice> InheritEntriesUnsorted(List<_Choice> Input) {

            List<_Choice> Result = new List<_Choice>();

            foreach (_Choice Entry in Input) {
                Entry.Normalize();
                }

            foreach (_Choice Entry in Input) {
                if (Entry.GetType() == typeof(Inherits)) {
                    Inherits Inherits = (Inherits)Entry;
                    List<_Choice> Inheritance = null;

                    _Choice Parent = Inherits.Ref.Definition;
                    Parent.Normalize();

                    if (Parent.GetType() == typeof(Message)) {
                        Message Cast = (Message)Parent;
                        Inheritance = Cast.AllEntriesUnsorted;
                        }
                    else if (Parent.GetType() == typeof(Structure)) {
                        Structure Cast = (Structure)Parent;
                        Inheritance = Cast.AllEntriesUnsorted;
                        }

                    if (Inheritance != null) {
                        foreach (_Choice Inherit in Inheritance) {
                            Result.Add(Inherit);
                            }
                        }
                    }
                else {
                    Result.Add(Entry);
                    }
                }


            return Result;
            }

        public void SetOptions(List<_Choice> Options) {
            foreach (_Choice Entry in Options) {
                Entry.Normalize();
                if (Entry.GetType() == typeof(Multiple)) {
                    Multiple = true;
                    }
                if (Entry.GetType() == typeof(Required)) {
                    Required = true;
                    }
                if (Entry.GetType() == typeof(Default)) {
                    Default = ((Default)Entry).Default;
                    }
                if (Entry.GetType() == typeof(Tag)) {
                    var Tag = Entry as Tag;
                    ID = Tag.Text;
                    }

                }
            }

        }

    public partial class Protocol {

        public List <Structure> Structures = new List<Structure> ();

        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString ();
            foreach (_Choice Entry in Entries) {
                if (Entry.GetType() == typeof(Message)) {
                    ((Message)Entry).Normalize();

                    Structures.Add (((Message)Entry).AsStructure());
                    }
                else if (Entry.GetType() == typeof(Inherits)) {
                    ThisInherits = ": " + (Entry as Inherits).Ref.ToString();
                    }
                else if (Entry.GetType() == typeof(Structure)) {
                    ((Structure)Entry).Normalize();
                    Structures.Add (((Structure)Entry));
                    }
                }

            Normalized = true;
            }

        }

    public partial class Message {
        public List<_Choice> AllEntries = null;
        public List<_Choice> AllEntriesUnsorted = null;

        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            AllEntries = _Choice.InheritEntries(Entries);
            AllEntriesUnsorted = _Choice.InheritEntriesUnsorted(Entries);
            foreach (_Choice Entry in AllEntries) {
                if (Entry.TypeC != null) {
                    CountChildren++;

                    }
                if (Entry.GetType() == typeof(Param)) {
                    Parameterized = true;
                    }
                }

            Normalized = true;
            }


        public Structure AsStructure() {
            Structure Structure = new Structure ();

            Structure.AllEntries = AllEntries;
            Structure.AllEntriesUnsorted = AllEntriesUnsorted;
            Structure.Entries = Entries;
            Structure.Id = Id;
            Structure.Normalize ();

            return Structure;
            }
        }

    public partial class Structure {
        public bool IsMessage = false;
        public List<_Choice> AllEntries = null;
        public List<_Choice> AllEntriesUnsorted = null;
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString ();
            AllEntries = _Choice.InheritEntries(Entries);

            AllEntriesUnsorted = _Choice.InheritEntriesUnsorted(Entries);

            foreach (_Choice Entry in AllEntries) {
                if (Entry.TypeC != null) {
                    CountChildren++;
                    }
                if (Entry.GetType() == typeof(Param)) {
                    //Parameterized = true;
                    }
                }

            Normalized = true;
            }

        }

    public partial class Boolean {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "boolean";
            TypeJ = "Boolean";
            Normalized = true;
            }
        }

    public partial class Integer {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "long long";
            TypeJ = "Int64";
            Normalized = true;
            }
        }
    public partial class Decimal {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "long long";
            TypeJ = "Decimal64";
            Normalized = true;
            }
        }
    public partial class Float {
        public override void Normalize() {
            if (Normalized) return;
            SetOptions(Options);
            TypeC = "double";
            TypeJ = "Real64";
            Normalized = true;
            }
        }
    public partial class Binary {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "JSON_Binary";
            TypeJ = "Binary";
            ByValue = false;
            Normalized = true;
            }
        }
    public partial class Label {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "JSON_String";
            TypeJ = "String";
            ByValue = false;
            Normalized = true;
            }
        }
    public partial class Name {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "JSON_String";
            TypeJ = "String";
            ByValue = false;
            Normalized = true;
            }
        }
    public partial class String {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "JSON_String";
            TypeJ = "String";
            ByValue = false;
            Normalized = true;
            }
        }
    public partial class URI {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "JSON_String";
            TypeJ = "String";
            ByValue = false;
            Normalized = true;
            }
        }
    public partial class DateTime {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            TypeC = "JSON_DateTime";
            TypeJ = "DateTime";
            ByValue = false;
            Normalized = true;
            }
        }
    public partial class Struct {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            Normalized = true;
            }
        }
    public partial class TStruct {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            Normalized = true;
            }
        }

    public partial class Tagged {
        public override void Normalize() {
            if (Normalized) return;
            ID = Id.ToString();
            SetOptions(Options);
            Normalized = true;
            }
        }


    }
