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
    List<TextBlock> Children = null,
    List<string> Attributes = null) {
    }

/// <summary>Describes a text decoration.</summary>
/// <param name="Tag">The tag</param>
/// <param name="Attributes">The permitted attributes.</param>
public record TextDecoration(
    string Tag,
    List<string> Attributes = null) {
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

    /// <summary>Static constructor.</summary>
    static RichtextValidator() {

        PostMarkup = new TextMarkup(
            [new ("h1"), new("h2"), new("h3"), new("p"), 
            new("ul", [new("li")]), 
            new("ol", [new("li")]),

            new("blockquote")], 
            [new ("strong"), new("em"), new("sub"), new("sup"),
                new("img", Attributes:["src"]),
                new("a", ["href", "rel", "target"]),
                new("pre", ["data-language"])]);


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

        TextDecoration? decoration = null;

        while (XmlReader.Read()) {
            switch (XmlReader.NodeType) {
                case XmlNodeType.Element: {
                    //Console.WriteLine("Start Element {0}", XmlReader.Name);
                    switch (state) {
                        case 0: {
                            if (!textMarkup.Block.TryGetValue(XmlReader.Name, out block)){
                                return RichetextResult.Invalid;
                                }
                            state = 1;
                            break;
                            }
                        case 1: {
                            inner = Contains(block, XmlReader.Name);
                            if (inner is not null) {
                                state = 2;
                                break;
                                }
                            if (!textMarkup.Decoration.TryGetValue(XmlReader.Name, out decoration)) {
                                return RichetextResult.Invalid;
                                }
                            state = 3;
                            break;
                            }
                        }
                    break;
                    }
                case XmlNodeType.Text: {
                    IsBlank |= !String.IsNullOrWhiteSpace(XmlReader.Value);
                    break;
                    }
                case XmlNodeType.EndElement: {
                    //Console.WriteLine("End Element {0}", XmlReader.Name);
                    switch (state) {
                        case 0: {
                            return RichetextResult.Invalid;
                            }
                        case 1: {
                            if (XmlReader.Name != block.Tag) {
                                return RichetextResult.Invalid;
                                }
                            state = 0;
                            break;
                            }
                        case 2: {
                            if (XmlReader.Name != inner.Tag) {
                                return RichetextResult.Invalid;
                                }
                            state = 1;
                            break;
                            }
                        case 3: {
                            if (XmlReader.Name != decoration.Tag) {
                                return RichetextResult.Invalid;
                                }
                            state = 1;
                            break;
                            }
                        case 4: {
                            if (XmlReader.Name != decoration.Tag) {
                                return RichetextResult.Invalid;
                                }
                            state = 2;
                            break;
                            }
                        }
                    break;
                    }
                }

            }

        return RichetextResult.Valid;
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
    /// <returns>The validation result.</returns>
    public static RichetextResult Validate(string text) {

        //var textreader = new StringReader(text);
        //var settings = new XmlReaderSettings() {
        //    ConformanceLevel = ConformanceLevel.Fragment
        //    };

        NameTable nt = new();
        XmlNamespaceManager nsmgr = new(nt);

        //Create the XmlParserContext.
        XmlParserContext context = new(null, nsmgr, null, XmlSpace.None);



        var reader = new XmlTextReader(text, XmlNodeType.Element, context);

        var validator = new RichtextValidator(reader);

        return validator.Validate();
        }

    }
