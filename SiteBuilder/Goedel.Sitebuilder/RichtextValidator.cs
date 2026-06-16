using System.Xml;
using System.Xml.Linq;

namespace Goedel.Sitebuilder;

/// <summary>Result of performing validation.</summary>
public enum RichetextResult {

    Valid,

    Invalid,

    Empty

    }


public record TextMarkup {

    public Dictionary<string, TextBlock> Block = [];
    public Dictionary<string, TextDecoration> Decoration = [];

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

public record TextBlock(
    string Tag,
    List<TextBlock> Children = null,
    List<string> Attributes = null) {
    }


public record TextDecoration(
    string Tag,
    List<string> Attributes = null) {
    }



public class RichtextValidator {


    public bool IsBlank = true;

    public List<string> Images = [];

    static TextMarkup PostMarkup { get; }

    XmlReader XmlReader { get; }

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

    RichtextValidator (string text) : this ( XmlReader.Create (text)) { 
        
        
        
        }

    RichtextValidator(XmlReader reader) {

        XmlReader = reader;

        }

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


    private TextBlock Contains(TextBlock parent, string tag) {
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


    public static RichetextResult Validate(string text) {

        //var textreader = new StringReader(text);
        //var settings = new XmlReaderSettings() {
        //    ConformanceLevel = ConformanceLevel.Fragment
        //    };

        NameTable nt = new NameTable();
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);

        //Create the XmlParserContext.
        XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);



        var reader = new XmlTextReader(text, XmlNodeType.Element, context);

        var validator = new RichtextValidator(reader);

        return validator.Validate();
        }

    }
