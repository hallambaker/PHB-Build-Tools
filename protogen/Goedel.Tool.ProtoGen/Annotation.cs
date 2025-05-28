using System;
using System.Collections.Generic;
using System.Text;
using Goedel.Registry;
using Goedel.Utilities;
using Goedel.Protocol;

// TODO: Should add a summary of each object/message into the markdown documentation output


namespace Goedel.Tool.ProtoGen;

public class AnnotateSchema {
    public ProtoStruct Parse { get; }

    Protocol Protocol { get; }

    public AnnotateSchema (string schemaFilename) {
        Parse = new ProtoStruct() {
            };
        using (Stream infile =
            new FileStream(schemaFilename, FileMode.Open, FileAccess.Read)) {
            Lexer Schema = new(schemaFilename);
            Schema.Process(infile, Parse);
            }

        Parse.Complete();

        Protocol = Parse.Top[0] as Protocol;
        }


    public void DocumentProperties(
                    TextWriter output,
                string structureName,
                List<string> properties = null,
                JsonObject example = null
                    ) {

        var include = properties is null ? null : new HashSet<string>();
        if (properties is not null) {
            foreach (var property in properties) {
                include.Add(property);
                }
            }



        var structure = GetStructure(Protocol, structureName);
        DescribeStructure(Protocol, structure, example, output, include);

        }


    public void DocumentStructure(
                TextWriter output,
                string structureName,
                List<string> properties = null,
                JsonObject example = null,
                bool writeHeading = true
                ) {


        var include = properties is null ? null : new HashSet<string>();
        if (properties is not null) {
            foreach (var property in properties) {
                include.Add(property);
                }
            }

        var structure = GetStructure(Protocol, structureName);
        var (count, substructure) = CountProperties(structure, include);

        var article = count == 1 ? "property" : "properties";

        if (writeHeading) {
            output.WriteLine($"### {structure.ID}");
            output.WriteLine();
            DescribeSubStructure(structure, output);
            }
        DescribeStructure(Protocol, structure, example, output, include);



        }



    public static void DocumentStructure(
                    string schemaFilename,
                    JsonObject example,
                    TextWriter output,
                    string? baseTag = null,
                    ISet<string>? include = null) => DocumentStructure (schemaFilename,
                        example, output, [baseTag], include);


    public static void DocumentStructure(
                    string schemaFilename,
                    JsonObject example,
                    TextWriter output,
                    List<string>? baseTags,
                    ISet<string>? include = null) {

        var annotate = new AnnotateSchema(schemaFilename);



        foreach (var tag in baseTags) {
            var baseTag = tag ?? example._Binding.Tag;
            var structure = GetStructure(annotate.Protocol, baseTag);
            annotate.DescribeStructure(annotate.Protocol, structure, example, output, include);
            }
        }



    public void DescribeStructure(
                Protocol protocol, 
                Structure structure,
                JsonObject example,
                TextWriter output,
                ISet<string>? include = null,
                bool isSub = false) {
        foreach (var entry in structure.Entries) {
            if (include == null || include.Contains(entry.ID)) {
                Structure substructure = null;
                bool externStructure = false;

                string typeDeclarator = null;
                List<_Choice> options = null;

                switch (entry) {
                    case Boolean typeString: {
                        typeDeclarator = "Boolean";
                        options = typeString.Options;
                        break;
                        }
                    case Integer typeInt: {
                        typeDeclarator = "Number";
                        options = typeInt.Options;
                        break;
                        }
                    case Float typeFloat: {
                        typeDeclarator = "Number";
                        options = typeFloat.Options;
                        break;
                        }
                    case String typeString: {
                        typeDeclarator = "String";
                        options = typeString.Options;
                        break;
                        }
                    case Struct typeStruct: {
                        typeDeclarator = typeStruct.BaseType;
                        options = typeStruct.Options;

                        substructure = GetStructure(protocol, typeDeclarator);
                        externStructure = substructure is null;
                        break;
                        }
                    case DateTime typeDate: {
                        typeDeclarator = "UTCDateTime";
                        options = typeDate.Options;
                        break;
                        }

                    case Section section: {
                        output.WriteLine($"## {section.Title}");
                        output.WriteLine();
                        var descriptions = GetDescription(section.Entries);
                        foreach (var line in descriptions) {
                            output.WriteLine(line);
                            }
                        output.WriteLine();
                        break;
                        }

                    }

                if (typeDeclarator is not null) {
                    if (!isSub) {
                        output.WriteLine($"### {entry.ID}");
                        output.WriteLine();
                        }

                    output.WriteLine(MakeType(entry, typeDeclarator));
                    var descriptions = GetDescription(options);
                    foreach (var line in descriptions) {
                        output.WriteLine(line);
                        }

                    if (substructure is not null) {
                        DescribeSubStructure(substructure, output);
                        DescribeStructure(protocol, substructure, example, output, null, true);
                        }
                    if (externStructure) {
                        output.WriteLine($"The {typeDeclarator} object is defined in [TBS].");
                        }

                    if (!isSub & example != null) {
                        output.WriteLine();
                        output.Write("~~~~");
                        output.WriteLine(GetExample(example, entry.ID));
                        output.WriteLine("~~~~");
                        output.WriteLine();
                        }
                    }
                }
            }

        }

    static (int, string) CountProperties(Structure structure, ISet<string>? include = null) {
        var count = 0;
        string superclass = null;
        foreach (var entry in structure.Entries) {
            if (include == null || include.Contains(entry.ID)) {
                switch (entry) {
                    case Boolean:
                    case Integer:
                    case Float:
                    case String:
                    case Struct:
                    case DateTime: {
                        count++;
                        break;
                        }
                    case Inherits typeInherits: {
                        superclass = typeInherits.Ref.Label;
                        break;
                        }
                    }
                }
            }

        return (count, superclass);
        }

    static void DescribeSubStructure(Structure structure,
                    TextWriter output) {
        var (count, superclass) = CountProperties (structure);

        if (superclass is null) {
            if (count == 0) {
                output.WriteLine($"{Indefinite(structure.ID)} object has no properties.");
                }
            else if (count == 1) {
                output.WriteLine($"{Indefinite(structure.ID)} object has the following property.");
                }
            else {
                output.WriteLine($"{Indefinite(structure.ID)} object has the following properties.");
                }
            output.WriteLine();
            }
        else {
            if (count == 0) {
                output.WriteLine($"{Indefinite(structure.ID)} object has all the properties of the " +
                    $"{superclass} data type.");
                }
            else if (count == 1) {
                output.WriteLine($"{Indefinite(structure.ID)} object has all the properties of the " +
                    $"{superclass} data type, with the following additional definition:");
                }
            else {
                output.WriteLine($"{Indefinite(structure.ID)} object has all the properties of the " +
                    $"{superclass} data type, with the following additional definitions:");
                }
            output.WriteLine();
            }



        }

    static string Indefinite(string word) => ("aeiouAEIOU".Contains(word[0]) ? "An " : "A ") + word;




    static Structure GetStructure(Protocol protocol, string tag) {
        foreach (var entry in protocol.Entries) {
            if (entry is Structure structure) {

                if (structure.ID == tag) {
                    return structure;
                    }
                }
            }
        return null;
        }

    static string GetExample(JsonObject jsonObject, string tag) {
        var builder = new MemoryStream();
        var writer = new JsonWriter(builder);

        var binding = jsonObject._Binding;

        if (binding.AllProperties.TryGetValue(tag, out var property)) {


            writer.WriteToken(tag, 0);



            property.Serialize(jsonObject, writer);

            }
        return builder.ToArray().ToUTF8();
        }


    static List<string> GetDescription(List<_Choice> options) {
        var result = new List<string>();
        foreach (var option in options) {
            if (option is Description description) {
                var para = "";
                foreach (var line in description.Text1) {
                    para = para + line + "\n";
                    }
                result.Add(para);
                }
            }

        return result;
        }

    static string MakeType(_Choice entry, string type) {
        var builder = new StringBuilder();
        builder.Append("\"");
        builder.Append(entry.ID);
        builder.Append("\": ");

        if (entry.Dictionary) {
            builder.Append($"String[{type}]");
            }
        else if (entry.Multiple) {
            builder.Append($"{type}[]");
            }
        else {
            builder.Append(type);
            }

        builder.Append(entry.Required ? " (mandatory)" : " (optional)");


        return builder.ToString();

        }




    }


public interface IEntries {
    public List<_Choice> _Choices { get;}
    }
public partial class ProtoStruct : Parser {
    bool HaveRun = false;


    virtual public void Complete() {
        if (!HaveRun) {
            HaveRun = true;
            // Set all the parents first to avoide weirdness due to inheritance
            foreach (ProtoGen._Choice Entry in Top) {
                Entry.SetParent(null);
                }

            foreach (ProtoGen._Choice Entry in Top) {
                Entry.Complete(null);
                }
            }
        }

    }

public abstract partial class _Choice {
    public bool             IsAbstract = false;
    public _Choice          Superclass = null;
    public List<_Choice>    Subclasses = new() ;


    public virtual void SetParent(_Choice parent) {
        Parent = parent;

        if (this is IEntries entries) {
            foreach (_Choice entry in entries._Choices) {

                entry.SetParent(this);

                switch (entry) {
                    case CamelCase: {
                        AssignedTypeCase = TypeCase.CamelCase;
                        break;
                        }
                    case PascalCase: {
                        AssignedTypeCase = TypeCase.PascalCase;
                        break;
                        }
                    case SnakeCase: {
                        AssignedTypeCase = TypeCase.SnakeCase;
                        break;
                        }
                    }
                }

            }

        }


    virtual public void Complete(_Choice parent) {
        //Console.WriteLine ("Completing");
        Parent = parent;
        Normalize();
        }

    static public void Complete(List<_Choice> Entries, _Choice parent) {
        foreach (_Choice Entry in Entries) {
            Entry.Complete(parent);
            }
        }
    }


public partial class Protocol : IEntries {

    public List<_Choice> _Choices => Entries;

    public override void  Complete(_Choice parent) {
        //Console.WriteLine ("Completing {0}", Id.ToString());
        Parent = parent;
        Normalize();
        foreach (_Choice Entry in Entries) {
            Entry.Complete(this);
            }
        }
    }

public partial class Transaction : IEntries {
    public List<_Choice> _Choices => Entries;

    public override void Complete(_Choice parent) {
        //Console.WriteLine ("Completing Transaction {0}", Id.ToString());
        Parent = parent;
        Complete(Entries, parent);
        }
    }

public partial class Class : _Choice {

    }


public partial class Message : IEntries {
    public List<_Choice> _Choices => Entries;
    public override void  Complete(_Choice parent) {
        //Console.WriteLine ("Completing Message {0}", Id.ToString());

        foreach (_Choice Entry in Entries) {
            Entry.Parent = this;
            if (Entry._Tag() == ProtoStructType.Inherits) {
                Inherits Inherits = (Inherits) Entry;
                Superclass = (Message) (Inherits.Ref.ID.Object) ;
                Superclass.Subclasses.Add (this);
                Inherits.Ref.Object = this; 
                }
            if (Entry._Tag() == ProtoStructType.Abstract) {
                IsAbstract = true;
                }
            }
        }
    }

public partial class Structure : IEntries {

    public List<_Choice> _Choices => Entries;
    public override void  Complete(_Choice parent) {
        //Console.WriteLine ("Completing Structure {0}", Id.ToString());

        foreach (_Choice Entry in Entries) {
            Entry.Parent = this;
            if (Entry._Tag() == ProtoStructType.Inherits) {
                Inherits Inherits = (Inherits) Entry;
                Superclass =(Inherits.Ref.ID.Object) as Structure;
                Superclass?.Subclasses.Add (this); // If this is a superclass, add it.
                Inherits.Ref.Object = this; 
                }
            if (Entry._Tag() == ProtoStructType.Abstract) {
                IsAbstract = true;
                }
            }
        }
    }


