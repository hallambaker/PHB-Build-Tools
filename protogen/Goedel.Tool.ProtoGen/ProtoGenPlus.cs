using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Goedel.Registry;
using Goedel.Utilities;

namespace Goedel.Tool.ProtoGen; 
public partial class _Choice {
    public TypeCase AssignedTypeCase = TypeCase.Default;
    public virtual TypeCase TypeCase => AssignedTypeCase == TypeCase.Default ? Parent.TypeCase : AssignedTypeCase;
    public _Choice Parent = null;
    public bool Normalized = false;
    public string Default = null;
    public string DefaultC => Default == null ? "NULL" : "\"" + Default + "\"";
    public bool Multiple = false;
    public bool Dictionary = false;
    public bool Generic { get; set; } = false;
    public bool Enumerated = false;
    public bool Required = false;
    public bool TypeTag { get; set; } = false;
    public string? TypeElement { get; set; } = null;

    public string RequiredC => Required ? "TRUE" : "FALSE";

    public string ID { get; set; }  = null;
    public string XID { get; set; } = null;

    public string TypeC = null;
    public string TypeJ = null;
    public bool ByValue = true;

    public bool Parameterized = false;
    public List<string> Description = new();

    public string ThisInherits = ": global::Goedel.Protocol.JsonObject";

    // Classes that don't inherit internally are children of JsonObject
    // by default.
    public string ExternParent = "JSONObject";

    public int CountChildren = 0;
    public int LengthBits = 32;


    public string VaryCS(string type) =>
            Dictionary ? $"Dictionary<string,{type}>" : Multiple ? $"List<{type}>" : $"{type}";
    //Dictionary ? $"Dictionary<string,{type}>?" : Multiple ? $"List<{type}>?" : $"{type}?";
    public string VaryProperty(string type) =>
            Dictionary ? $"PropertyDictionary{type}" : Multiple ? $"PropertyList{type}" : $"Property{type}";

    //Dictionary? $"PropertyDictionary{type}" : Multiple? $"PropertyList{type}" : $"Property{type}";


    public virtual string PropertyName => "Property";
    public virtual string TypeCS => "";

    public virtual void Normalize() {
        }

    public static List<_Choice> InheritEntries(List<_Choice> Input) {
        

        List<_Choice> Result = new();

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

                if (Parent?.GetType() == typeof(Message)) {
                    Message Cast = (Message)Parent;
                    Inheritance = Cast.AllEntries;
                    }
                else if (Parent?.GetType() == typeof(Structure)) {
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

    public static List<_Choice> InheritEntriesUnsorted(_Choice structure, List<_Choice> input) {

        List<_Choice> Result = new();

        foreach (_Choice Entry in input) {
            Entry.Normalize();
            }

        foreach (_Choice Entry in input) {
            if (Entry.GetType() == typeof(Inherits)) {
                Inherits Inherits = (Inherits)Entry;
                List<_Choice> Inheritance = null;

                _Choice Parent = Inherits.Ref.Definition;

                Assert.AssertNotNull(Parent, UndefinedParent.Throw, 
                    new UndefinedParentError() {Class = structure.ID, Inherits= Inherits.Ref.Label});

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


        switch (TypeCase) {
            case TypeCase.CamelCase: {
                var first = char.ToLowerInvariant(ID[0]);
                ID = first + ID[1..];
                break;
                }
            case TypeCase.SnakeCase: {
                var builder = new StringBuilder();

                var first = true;
                foreach (var c in ID) {
                    if (char.IsUpper(c)) {
                        if (!first) {
                            builder.Append('_');
                            }
                        builder.Append(char.ToLowerInvariant(c));
                        }
                    else {
                        builder.Append(c);
                        }
                    first = false;
                    }

                ID = builder.ToString();
                break;
                }
            }

        //if (TypeCase == TypeCase.CamelCase) {
        //    var first = Char.ToLowerInvariant(ID[0]);
        //    ID = first + ID[1..];
        //    //ID = Id.Label;
        //    }
        //if (TypeCase == TypeCase.SnakeCase) {
        //    var first = Char.ToLowerInvariant(ID[0]);
        //    ID = first + ID[1..];
        //    //ID = Id.Label;
        //    }

        foreach (_Choice entry in Options) {
            entry.Normalize();

            switch (entry) {
                case Description description: {
                    foreach (var Text in description.Text1) {
                        Description.Add(Text);
                        }
                    break;
                    }
                case Generic generic: {
                    Generic = true;
                    break;
                    }
                case Multiple multiple: {
                    Multiple = true;
                    break;
                    }
                case Dictionary multiple: {
                    Dictionary = true;
                    break;
                    }
                case Enumerated enumerated: {
                    Enumerated = true;
                    Multiple = true;
                    break;
                    }
                case Required required: {
                    Required = true;
                    break;
                    }
                case TypeTag typetag: {
                    TypeTag = true;
                    TypeElement ??= ID;

                    Parent.TypeTag = true;
                    Parent.TypeElement ??= ID;
                    break;
                    }
                case LengthBits lengthBits: {
                    LengthBits = lengthBits.Bits;
                    break;
                    }
                case Default defaultValue: {
                    Default = defaultValue.Default;
                    break;
                    }
                case Tag tag: {
                    XID = ID;
                    ID = tag.Text;
                    break;
                    }

                }


            }
        }

    }


public enum TypeCase {
    Default,
    Direct,
    CamelCase,
    PascalCase,
    SnakeCase
    }

public partial class Protocol {

    public override TypeCase TypeCase => AssignedTypeCase;

    public List<Structure> Structures { get; set; } = [];

    public override void Normalize() {

        if (Normalized) {
            return;
            }
        ID = Id.ToString ();
        foreach (_Choice entry in Entries) {
            switch (entry) {
                //case CamelCase: {
                //    AssignedTypeCase = TypeCase.CamelCase;
                //    break;
                //    }
                //case PascalCase: {
                //    AssignedTypeCase = TypeCase.PascalCase;
                //    break;

                //    }
                //case SnakeCase: {
                //    AssignedTypeCase = TypeCase.SnakeCase;
                //    break;

                //    }
                case Message message: {
                    message.Normalize();
                    Structures.Add(message.AsStructure());
                    break;
                    }
                case Inherits inherits: {
                    ThisInherits = ": " + inherits.Ref.ToString();
                    break;
                    }
                case Structure structure: {
                    structure.Normalize();
                    Structures.Add(structure);
                    break;
                    }
                }


            }


        Normalized = true;
        }

    }


public interface IStructure {
    public ID<_Choice> IId { get; }
    public List<_Choice> IEntries { get; }


    public bool IParameterized { get; }

    public string ID { get; }

    public bool Generic { get; }
    public bool TypeTag { get; }
    public string TypeElement { get; }
    }

public partial class Message : IStructure {
    public ID<_Choice> IId => Id;
    public List<_Choice> IEntries => Entries;

    public bool IParameterized => Parameterized;


    public List<_Choice> AllEntries = null;
    public List<_Choice> AllEntriesUnsorted = null;

    public override void Normalize() {

        if (Normalized) {
            return;
            }
        ID = Id.ToString();
        AllEntries = _Choice.InheritEntries(Entries);
        AllEntriesUnsorted = _Choice.InheritEntriesUnsorted(this, Entries);
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
        Structure Structure = new() {
            AllEntries = AllEntries,
            AllEntriesUnsorted = AllEntriesUnsorted,
            Entries = Entries,
            Id = Id
            };
        Structure.Normalize();

        return Structure;
        }
    }

public partial class Structure : IStructure {
    public ID<_Choice> IId => Id;
    public List<_Choice> IEntries => Entries;
    public bool IParameterized => Parameterized;

    public bool IsMessage = false;
    public List<_Choice> AllEntries = null;
    public List<_Choice> AllEntriesUnsorted = null;
    public override void Normalize() {
        if (Id.ToString() == "Jwks") {
            }

        if (Normalized) {
            return;
            }

        ID = Id.ToString ();
        AllEntries = InheritEntries(Entries);
        AllEntriesUnsorted = InheritEntriesUnsorted(this, Entries);

        foreach (_Choice entry in AllEntries) {
            switch (entry) {
                case Tag tag: {
                    XID = Id.ToString();
                    ID = tag.Text;
                    break;
                    }

                case Generic generic: {
                    Generic = true;
                    break;
                    }
                //case PascalCase: {
                //    AssignedTypeCase = TypeCase.PascalCase;
                //    break;

                //    }
                //case SnakeCase: {
                //    AssignedTypeCase = TypeCase.SnakeCase;
                //    break;

                //    }
                }
            if (entry.TypeC != null) {
                CountChildren++;
                }
            //if (entry.GetType() == typeof(Param)) {

            //    }
            }

        Normalized = true;
        }

    }

public partial class Boolean {

    public override string PropertyName => VaryProperty("Boolean");

    public override string TypeCS => VaryCS("bool");


    //public override string PropertyName => 
    //            Vary( "PropertyListBoolean", "PropertyBoolean", "PropertyDictionaryBoolean");
    //public override string TypeCS => 
    //            Vary("List<bool>?" , "bool?", "Dictionary<string,bool>?");
    
    
    ////Multiple ? "List<bool>?" : "bool?";

    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "boolean";
        TypeJ = "Boolean";
        Normalized = true;
        }
    }

public partial class Integer {

    public override string PropertyName => LengthBits > 32 ? VaryProperty("Integer64") : VaryProperty("Integer32");

    public override string TypeCS => LengthBits > 32 ? VaryCS("long") : VaryCS("int");


    //public override string PropertyName => LengthBits > 32 ? 
    //    Vary("PropertyListInteger64", "PropertyInteger64", "PropertyDictionaryInteger64") :
    //    Vary("PropertyListInteger32", "PropertyInteger32", "PropertyDictionaryInteger32");

    ////(Multiple ? "PropertyListInteger64" : "PropertyInteger64") :
    ////    (Multiple ? "PropertyListInteger32" : "PropertyInteger32");
    //public override string TypeCS => LengthBits > 32 ? 
    //    Vary("List<long>?" , "long?", "PropertyDictionaryBoolean") :
    //    Vary("List<int>?" , "int?", "PropertyDictionaryBoolean"); 
    
    ////(Multiple ? "List<long>?" : "long?") :
    ////            (Multiple ? "List<int>?" : "int?");
    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "long long";
        TypeJ = "Int64";
        Normalized = true;
        }
    }
public partial class Decimal {
    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "long long";
        TypeJ = "Decimal64";
        Normalized = true;
        }
    }
public partial class Float {

    public override string PropertyName => VaryProperty("Real64");

    public override string TypeCS => VaryCS("double");

    //public override string PropertyName =>
    //    Vary("PropertyListReal64", "PropertyReal64", "PropertyDictionaryReal64");
    ////Multiple ? "PropertyListReal64" : "PropertyReal64";
    //public override string TypeCS =>
    //    Vary("List<double>?", "double?", "Dictionary<string,double>");

    ////Multiple ? "List<double>?" : "double?";
    public override void Normalize() {

        if (Normalized) {
            return;
            }
        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "double";
        TypeJ = "Real64";
        Normalized = true;
        }
    }
public partial class Binary {
    public override string PropertyName => VaryProperty("Binary");

    public override string TypeCS => VaryCS("byte[]");

    public override void Normalize() {

        if (Normalized) {
            return;
            }

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

        if (Normalized) {
            return;
            }

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

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "JSON_String";
        TypeJ = "String";
        ByValue = false;
        Normalized = true;
        }
    }
public partial class String {

    public override string PropertyName => TypeTag? "PropertyStringTag" : VaryProperty("String");

    public override string TypeCS => TypeTag ? "string" : VaryCS("string");


    //public override string PropertyName => 
    //    Vary("PropertyListBoolean", "PropertyBoolean", "PropertyDictionaryBoolean"); 
    
    
    
    //Multiple ? "PropertyListString" : "PropertyString";
    //public override string TypeCS => 
    //    Vary("PropertyListBoolean", "PropertyBoolean", "PropertyDictionaryBoolean"); 
    
    
    //Multiple ? "List<string>?" : "string?";
    public override void Normalize() {

        if (Normalized) {
            return;
            }

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

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "JSON_String";
        TypeJ = "String";
        ByValue = false;
        Normalized = true;
        }
    }
public partial class DateTime {

    public override string PropertyName => VaryProperty("DateTime");

    public override string TypeCS => VaryCS("DateTime");




    //public override string PropertyName =>
    //    Vary("PropertyListBoolean", "PropertyBoolean", "PropertyDictionaryBoolean"); 
    
    
    //Multiple ? "PropertyListDateTime" : "PropertyDateTime";
    //public override string TypeCS =>
    //    Vary("PropertyListBoolean", "PropertyBoolean", "PropertyDictionaryBoolean"); 
    
    //Multiple ? "List<DateTime>?" : "DateTime?";
    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        TypeC = "JSON_DateTime";
        TypeJ = "DateTime";
        ByValue = false;
        Normalized = true;
        }
    }
public partial class Struct {


    public string BaseType => Type.Label;
    public override string PropertyName => VaryProperty("Struct");

    public override string TypeCS => VaryCS(BaseType);
    public string TypeCSCons => TypeCS;

    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        Normalized = true;
        }
    }
public partial class TStruct {

    public string BaseType => Type.Label;
    public override string PropertyName => VaryProperty("Struct");

    public override string TypeCS => VaryCS(BaseType);
    public string TypeCSCons => TypeCS;

    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        Normalized = true;
        }
    }

public partial class GStruct {

    public string BaseType => $"{GType.Label}<{Type.Label}>";

    public string MainFieldName => $"{GType.Label}{Id.Label}";

    public string SubFieldName => $"{Id.Label}";

    public override string PropertyName => VaryProperty("GStruct");

    public override string TypeCS => VaryCS(BaseType);
    public string SubTypeCS => VaryCS(Type.Label);

    public string TypeCSCons => TypeCS;

    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = MainFieldName;
        SetOptions(Options);
        Normalized = true;
        }
    }



public partial class Tagged {
    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
        SetOptions(Options);
        Normalized = true;
        }
    }



