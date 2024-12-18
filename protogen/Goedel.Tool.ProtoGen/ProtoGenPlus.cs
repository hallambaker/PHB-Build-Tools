﻿using System;
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
    public bool Enumerated = false;
    public bool Required = false;

    public string RequiredC => Required ? "TRUE" : "FALSE";

    public string ID = null;

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
                case Multiple multiple: {
                    Multiple = true;
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
                case LengthBits lengthBits: {
                    LengthBits = lengthBits.Bits;
                    break;
                    }
                case Default defaultValue: {
                    Default = defaultValue.Default;
                    break;
                    }
                case Tag tag: {
                    ID = tag.Text;
                    break;
                    }
                //case CamelCase : {
                //    AssignedTypeCase = TypeCase.CamelCase;
                //    break;
                //    }
                //case PascalCase : {
                //    AssignedTypeCase = TypeCase.PascalCase;
                //    break;
                //    }
                //case SnakeCase : {
                //    AssignedTypeCase = TypeCase.SnakeCase;
                //    break;
                //    }
                }



            //if (entry.GetType() == typeof(Description)) {
            //    foreach (var Text in (entry as Description).Text1) {
            //        Description.Add(Text);
            //        }
            //    }
            //if (entry.GetType() == typeof(Multiple)) {

            //    }
            //if (entry.GetType() == typeof(Enumerated)) {
            //    Enumerated = true;
            //    Multiple = true;
            //    }
            //if (entry.GetType() == typeof(Required)) {
            //    Required = true;
            //    }
            //if (entry.GetType() == typeof(LengthBits)) {
            //    LengthBits = ((LengthBits)entry).Bits;
            //    }
            //if (entry.GetType() == typeof(Default)) {
            //    Default = ((Default)entry).Default;
            //    }
            //if (entry.GetType() == typeof(Tag)) {
            //    var Tag = entry as Tag;
            //    ID = Tag.Text;
            //    }

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

public partial class Message {
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

public partial class Structure {
    public bool IsMessage = false;
    public List<_Choice> AllEntries = null;
    public List<_Choice> AllEntriesUnsorted = null;
    public override void Normalize() {


        if (Normalized) {
            return;
            }

        ID = Id.ToString ();
        AllEntries = InheritEntries(Entries);
        AllEntriesUnsorted = InheritEntriesUnsorted(this, Entries);

        foreach (_Choice entry in AllEntries) {
            //switch (entry) {
            //    case CamelCase: {
            //        AssignedTypeCase = TypeCase.CamelCase;
            //        break;
            //        }
            //    case PascalCase: {
            //        AssignedTypeCase = TypeCase.PascalCase;
            //        break;

            //        }
            //    case SnakeCase: {
            //        AssignedTypeCase = TypeCase.SnakeCase;
            //        break;

            //        }
            //    }
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

    public override string PropertyName => Multiple ? "PropertyListBoolean" : "PropertyBoolean";
    public override string TypeCS => Multiple ? "List<bool>?" : "bool?";

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

    public override string PropertyName => LengthBits > 32 ? (Multiple ? "PropertyListInteger64" : "PropertyInteger64") :
        (Multiple ? "PropertyListInteger32" : "PropertyInteger32");
    public override string TypeCS => LengthBits > 32 ? (Multiple ? "List<long>?" : "long?") :
                (Multiple ? "List<int>?" : "int?");
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
    public override string PropertyName => Multiple ? "PropertyListReal64" : "PropertyReal64";
    public override string TypeCS => Multiple ? "List<double>?" : "double?";
    public override void Normalize() {

        if (Normalized) {
            return;
            }

        SetOptions(Options);
        TypeC = "double";
        TypeJ = "Real64";
        Normalized = true;
        }
    }
public partial class Binary {
    public override string PropertyName => Multiple ? "PropertyListBinary" : "PropertyBinary";
    public override string TypeCS => Multiple ? "List<byte[]>?" : "byte[]?";
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

    public override string PropertyName => Multiple ? "PropertyListString" : "PropertyString";
    public override string TypeCS => Multiple ? "List<string>?" : "string?";
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
    public override string PropertyName => Multiple ? "PropertyListDateTime" : "PropertyDateTime";
    public override string TypeCS => Multiple ? "List<DateTime>?" : "DateTime?";
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

    public override string PropertyName => Multiple ? "PropertyListStruct" : "PropertyStruct";

    public string BaseType => Type.Label;
    public override string TypeCS => Multiple ? $"List<{BaseType}>?" : $"{BaseType}?";
    public  string TypeCSCons => Multiple ? $"List<{BaseType}>" : $"{BaseType}";


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

    public override string PropertyName => Multiple ? "PropertyListStruct" : "PropertyStruct";

    public string BaseType => Type.Label;
    public override string TypeCS => Multiple ? $"List<{BaseType}>?" : $"{BaseType}?";
    public string TypeCSCons => Multiple ? $"List<{BaseType}>" : $"{BaseType}";


    public override void Normalize() {

        if (Normalized) {
            return;
            }

        ID = Id.ToString();
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



