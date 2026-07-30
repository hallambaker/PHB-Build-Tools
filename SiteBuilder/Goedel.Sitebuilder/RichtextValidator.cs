using System.Xml;
using System.Xml.Linq;

namespace Goedel.Sitebuilder;

/// <summary>Result of performing validation.</summary>
public enum RichetextResult {

    /// <summary>Value is valid.</summary>
    Valid,

    /// <summary>Value was invalid.</summary>
    Invalid,

    /// <summary>Value was empty.</summary>
    Empty
    }


/// <summary>Markup description</summary>
public record TextMarkup {

    /// <summary>Block text definitions.</summary>
    public Dictionary<string, TextBlock> Block = [];

    /// <summary>Decoration definitions.</summary>
    public Dictionary<string, TextDecoration> Decoration = [];

    /// <summary>Constructor, return a new instance.</summary>
    /// <param name="blocks">Block text definitions.</param>
    /// <param name="decorations">Decoration definitions.</param>
    public TextMarkup(
        List<TextBlock> blocks,
        List<TextDecoration> decorations) {

        foreach (var block in blocks) {
            Block.Add (block.Tag, block);   
            }
        foreach (var decoration in decorations) {
            Decoration.Add(decoration.Tag, decoration);
            }
        }
    }

/// <summary>Describes a text block</summary>
/// <param name="Tag">The tag</param>
/// <param name="Attributes">The permitted attributes.</param>
/// <param name="Children">Permitted child blocks.</param>
public record TextBlock(
    string Tag,
    List<TextBlock>? Children = null,
    List<string>? Attributes = null) {
    }

/// <summary>Describes a text decoration.</summary>
/// <param name="Tag">The tag</param>
/// <param name="Attributes">The permitted attributes.</param>
public record TextDecoration(
    string Tag,
    List<string>? Attributes = null) {
    }


/// <summary>Rich text validation parser.</summary>
public class RichtextValidator {

    /// <summary>If true, the output is blank.</summary>
    public bool IsBlank = true;

    /// <summary>List of images in the item.</summary>
    public List<string> Images = [];

    /// <summary>The schema. to validate against.</summary>
    static TextMarkup PostMarkup { get; }

    /// <summary>The reader.</summary>
    XmlReader XmlReader { get; }

    /// <summary>String writer to collect text as it is canonicalized.</summary>
    StringWriter Writer { get; } = new();

    /// <summary>The canonicalized input.</summary>
    public string Canonical => Writer.ToString();


    /// <summary>Static constructor.</summary>
    static RichtextValidator() {

        PostMarkup = new TextMarkup(
            [new ("h1"), new("h2"), new("h3"), new("p"), 
            new("ul", [new("li")]), 
            new("ol", [new("li")]),
            new("pre", Attributes: ["data-language"]),
            new("blockquote")], 
            [new ("strong"), new("em"), new("sub"), new("sup"),
                new("img", Attributes:["src"]),
                new("a", ["href", "rel", "target"]),
                ]);


        }

    /// <summary>Constructor returning a validator over <paramref name="text"/></summary>
    /// <param name="text">The text to validate.</param>
    RichtextValidator (string text) : this ( XmlReader.Create (text)) { 
        }

    /// <summary>Constructor returning a validator over data read from <paramref name="reader"/></summary>
    /// <param name="reader">Reader to parse the text.</param>
    RichtextValidator(XmlReader reader) {

        XmlReader = reader;

        }

    /// <summary>Validate the input.</summary>
    /// <param name="textMarkup">The schema to validate against (optional, defaults to 
    /// the default schema).</param>
    /// <returns>The validation result.</returns>
    public RichetextResult Validate(TextMarkup? textMarkup = null) {
        textMarkup ??= PostMarkup;

        int state = 0;
        TextBlock? block = null;
        TextBlock? inner = null;

        int blocks = 0;
        int decorations = 0;

        TextDecoration? decoration = null;

        while (XmlReader.Read()) {



            switch (XmlReader.NodeType) {

                case XmlNodeType.Element: {
                    Console.WriteLine($"{XmlReader.Name} - {state} {blocks} {decorations} ");
                    switch (state) {
                        case 0: {
                            if (!textMarkup.Block.TryGetValue(XmlReader.Name, out block)) {
                                return RichetextResult.Invalid;
                                }
                            state = 1;
                            blocks++;

                            //Writer.Write('\r');
                            Writer.WriteLine();
                            WriteElement(block.Attributes);

                            break;
                            }
                        case 1: {
                            inner = Contains(block, XmlReader.Name);
                            if (inner is not null) {
                                blocks++;

                                //Writer.Write('\r');
                                Writer.WriteLine();
                                WriteElement(inner.Attributes);

                                break;
                                }
                            if (!textMarkup.Decoration.TryGetValue(XmlReader.Name, out decoration)) {
                                return RichetextResult.Invalid;
                                }
                            state = 2;
                            decorations++;

                            WriteElement(decoration.Attributes);
                            break;
                            }
                        case 2: {
                            if (!textMarkup.Decoration.TryGetValue(XmlReader.Name, out decoration)) {
                                return RichetextResult.Invalid;
                                }

                            decorations++;
                            WriteElement(decoration.Attributes);
                            break;
                            }
                        }

                    Console.WriteLine($"   -> {XmlReader.Name} - {state} {blocks} {decorations} ");
                    break;
                    }
                case XmlNodeType.Text: {
                    IsBlank |= !String.IsNullOrWhiteSpace(XmlReader.Value);


                    Writer.Write(XmlReader.Value);

                    break;
                    }
                case XmlNodeType.EndElement: {
                    Console.WriteLine($"/ {XmlReader.Name} - {state} {blocks} {decorations} ");

                    //Console.WriteLine("End Element {0}", XmlReader.Name);
                    switch (state) {
                        case 0: {
                            return RichetextResult.Invalid;
                            }
                        case 1: {
                            blocks--;
                            if (blocks == 0) {
                                state = 0;
                                }
                            break;
                            }
                        case 2: {
                            decorations--;
                            if (decorations == 0) {
                                state = 1;
                                }
                            }
                        break;
                        }

                    Writer.Write("</");
                    Writer.Write(XmlReader.Name);

                    // write attributes

                    Writer.Write('>');

                    Console.WriteLine($"  => /{XmlReader.Name} - {state} {blocks} {decorations} ");
                    break;
                    }

                case XmlNodeType.EntityReference: {
                    var output = XmlReader.Name switch {
                        "nbsp" => " ",
                        "lt" => "&lt;",
                        "gt" => "&gt;",
                        "amp" => "&amp;",
                        "quot" => "\"",
                        "apos" => "'",
                        _ => ""
                        };


                    Writer.Write(output);
                    break;
                    }
                }

            }

        return RichetextResult.Valid;
        }






    private void WriteElement(List<string> attributes) {
        Writer.Write('<');
        Writer.Write(XmlReader.Name);

        if (XmlReader.HasAttributes & attributes != null) {
            foreach (var attribute in attributes) {
                var value = XmlReader.GetAttribute(attribute);

                if (value != null) {

                    Writer.Write(' ');
                    Writer.Write(attribute);
                    Writer.Write(' ');
                    Writer.Write('"');
                    var escapedValue = HttpUtility.HtmlAttributeEncode(value);
                    Writer.Write(escapedValue);
                    Writer.Write('"');
                    }
                }
            }
        Writer.Write(XmlReader.IsEmptyElement ? "/>" : ">");

        }



    
    private static TextBlock Contains(TextBlock parent, string tag) {
        if (parent.Children is null) {
            return null;
            }


        foreach (var child in parent.Children) {
            if (child.Tag == tag) {
                return child;
                }
            }


        return null;
        }

    /// <summary>Validate the document <paramref name="text"/></summary>
    /// <param name="text">The document to validate.</param>
    /// <param name="validated">The canonicalized text.</param>
    /// <returns>The validation result.</returns>
    public static bool Validate(string text, out string? validated) {

        if (text is null) {
            validated = null;
            return false;
            }

        NameTable nt = new();
        XmlNamespaceManager nsmgr = new(nt);

        //Create the XmlParserContext.
        XmlParserContext context = new(null, nsmgr, null, XmlSpace.None);



        var reader = new XmlTextReader(text, XmlNodeType.Element, context);

        var validator = new RichtextValidator(reader);

        var result = validator.Validate();

        validated = result == RichetextResult.Valid ? validator.Canonical : null;


        return result == RichetextResult.Valid;
        }

    }
