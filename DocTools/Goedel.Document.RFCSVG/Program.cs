using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections.Generic;
using Goedel.Utilities;
using Goedel.IO;
namespace Goedel.Document.RFCSVG;

//class Program {
//    static void Main(string[] args) {
//        Console.WriteLine("Hello World!");

//        var svgDocument = new SvgDocument("ActivationAccount.svg");

//        var output = "Redacted.svg".OpenTextWriterNew();
//        svgDocument.Save(output);


//        }
//    }
/// <summary>
/// The processing context for an SVG document. The context is bound to an SVG document when
/// instantiated and the corresponding profile constrained document computed. This may then
/// be written out as a standalone document or as an element incorporated into another document.
/// </summary>
public class SvgDocument {
    ///<summary>The parsed source document.</summary>
    public XmlDocument Source;

    ///<summary>The redacted target document as a parse tree.</summary>
    public XmlDocument Target;

    ///<summary>The root svg element.</summary>
    public XmlElement Root;

    ///<summary>The style sheet extracted from the source document.</summary>
    public Dictionary<string, List<TagValue>> Style = new();

    /// <summary>
    /// Constructor creating a redacted document from the parsed document <paramref name="source"/>.
    /// </summary>
    /// <param name="source">The source document to redact.</param>
    public SvgDocument(XmlDocument source) => Redact(source);


    public string Filename;

    /// <summary>
    /// Constructor creating a redacted document from the file name <paramref name="input"/>.
    /// </summary>
    /// <param name="input">Name of the file to be read.</param>
    public SvgDocument(string input) {
        Filename = input;
        var source = new XmlDocument();
        source.Load(input);
        Redact(source);
        }

    /// <summary>
    /// Save the redacted document data to the output stream <paramref name="output"/>
    /// with format specifier <paramref name="indent "/>.
    /// </summary>
    /// <param name="output">The output stream.</param>
    /// <param name="indent ">The formatting mode</param>
    public void Save(TextWriter output, bool indent = true) {


        // The following code is needed due to the lack of documentation on
        // how to set up XmlDocument namespaces. There is clearly a missing
        // method

        var settings = new XmlWriterSettings() {
            Indent = indent,
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
            ConformanceLevel = ConformanceLevel.Fragment,
            CloseOutput = false
            };
        using var xmlOutput = XmlWriter.Create(output, settings);

        output.Write("<svg ");

        foreach (var item in Root.Attributes) {
            if (item is XmlAttribute attribute) {
                output.Write($"{attribute.Name}=\"{attribute.Value}\" ");
                }
            }
        output.WriteLine(">");
        output.Flush();

        Root.WriteContentTo(xmlOutput);
        xmlOutput.Flush();

        output.WriteLine("</svg>");
        output.Flush();
        }

    /// <summary>
    /// Save the redacted document data to the XmlWriter <paramref name="output"/>.
    /// </summary>
    /// <param name="output">The output XmlWriter.</param>
    public void Save(XmlWriter output) {
        output.WriteStartElement("svg");
        foreach (var item in Root.Attributes) {
            if (item is XmlAttribute attribute) {
                output.WriteAttributeString(attribute.Name, attribute.Value);
                }
            }

        output.WriteFullEndElement();

        Root.WriteContentTo(output);

        output.WriteEndElement();
        }
    /// <summary>
    /// Redact the source document <paramref name="source"/>.
    /// </summary>
    /// <param name="source">The document to redact.</param>
    /// <returns>The redacted document.</returns>
    XmlDocument Redact(XmlDocument source) {
        Source = source; // might as well keep
                         //var namespaceManager = new XmlNamespaceManager();
                         //namespaceManager.AddNamespace(String.Empty, "http://www.w3.org/2000/svg");



        //XmlNamespace ns0 = "http://www.w3.org/2000/svg";

        Target = new XmlDocument(); // the output
                                    //Target.NamespaceURI = "";
        foreach (var child in Source.DocumentElement.ChildNodes) {
            if (child is XmlElement element) {
                if (element.Name == "style") {
                    foreach (var style in element.ChildNodes) {
                        if (style is XmlCDataSection dataSection) {
                            MakeStyle(dataSection.Data);
                            }
                        }
                    }
                }
            }

        var targetContext = new SvgContext(this);

        Root = MakeElement(targetContext, Source.DocumentElement).Element;

        Root.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
        //Root.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");

        return Target;
        }

    void MakeStyle(string text) {
        var readStyle = new ReadStyle(text);

        ReadStyle.Token token;
        do {
            token = readStyle.GetToken();
            if (token == ReadStyle.Token.End) {
                Style.Add(readStyle.Class, readStyle.TagValues);

                }
            } while (token > 0);



        }


    SvgContext MakeElement(SvgContext parentContext, XmlElement sourceElement) {
        // Should we suppress?
        if (!ElementProfile.Elements.TryGetValue(sourceElement.Name, out var elem)) {
            return null;
            }

        // create the element and attach redacted attributes
        var targetContext = elem.Process(parentContext, sourceElement);
        var targetElement = targetContext.Element;
        // attach the child nodes

        var wrapTextTspan = false;
        var testTextTspan = sourceElement.Name == "text";

        foreach (var child in sourceElement.ChildNodes) {
            if (child is XmlElement element) {
                var newChild = MakeElement(targetContext, element);
                if (newChild != null) {
                    targetElement.AppendChild(newChild.Element);
                    }
                if (testTextTspan & element.Name == "tspan") {
                    wrapTextTspan = true;
                    }

                }
            else if (child is XmlText text) {
                var newText = Target.CreateTextNode(text.InnerText.XMLEscapeRFCBullies());
                if (wrapTextTspan) {
                    var wrapper = Target.CreateElement("tspan");
                    wrapper.AppendChild(newText);
                    targetElement.AppendChild(wrapper);
                    }
                else {
                    targetElement.AppendChild(newText);
                    }
                }
            }

        return targetContext;
        }



    }

/// <summary>
/// Processing context for SVG elements. The class tracks the current X and Y position to
/// allow resolution of relative X and Y coordinates (dx, dy) to the absolute coordinates 
/// supported by SVG Tiny 1.2
/// </summary>
public class SvgContext {

    /// <summary>
    /// The redacted document under construction
    /// </summary>
    public SvgDocument Target;

    ///<summary>The enclosing context.</summary>
    public SvgContext Parent;

    ///<summary>The XML element</summary>
    public XmlElement Element;

    ///<summary>The element description.</summary>
    public ElementProfile Elem;

    ///<summary>The X position in pixels</summary>
    public double X;

    ///<summary>The Y position in pixels</summary>
    public double Y;

    ///<summary>The font size in pixels</summary>
    public double FontSize;

    /// <summary>
    /// Construct a root context from <paramref name="svgDocument"/>.
    /// </summary>
    /// <param name="svgDocument">The document processing context.</param>
    public SvgContext(SvgDocument svgDocument) {
        Target = svgDocument;
        }

    /// <summary>
    /// Construct a subordinate context to <paramref name="parent"/> for element <paramref name="element"/>
    /// with description <paramref name="elem"/>.
    /// </summary>
    /// <param name="parent">The parent element processing context.</param>
    /// <param name="element">The XML element.</param>
    /// <param name="elem">The element description.</param>
    public SvgContext(SvgContext parent, XmlElement element, ElementProfile elem) {
        Parent = parent;
        Element = element;
        Elem = elem;

        // Set the starting values of the X and Y position etc.
        Target = parent.Target;
        X = parent.X;
        Y = parent.Y;
        FontSize = parent.FontSize;
        }

    /// <summary>
    /// Adjust the frame of reference according to the coordinate and font size
    /// information in the tag <paramref name="tag"/> with value <paramref name="value"/>.
    /// </summary>
    /// <param name="tag">The attribute name.</param>
    /// <param name="value">The attribute value.</param>
    public void Attribute(ref string tag, ref string value) {

        switch (tag) {
            case "font-size": {
                    FontSize = ParseValueDimensioned(value, FontSize);
                    break;
                    }
            case "x": {
                    X = ParseValueDimensioned(value, X);
                    break;
                    }
            case "y": {
                    Y = ParseValueDimensioned(value, Y);
                    break;
                    }

            case "dx": {
                    X += ParseValueDimensioned(value, X);
                    tag = "x";
                    value = $"{X:0.########}";
                    break;
                    }
            case "dy": {
                    Y += ParseValueDimensioned(value, Y);
                    tag = "y";
                    value = $"{Y:0.########}";
                    break;
                    }

            }


        }

    public double UnitInch = 96;
    public double UnitPt => (UnitInch / 72);
    public double UnitPc => (UnitInch / 6);

    public double UnitCm => (UnitInch / 2.54);
    public double UnitMm => (UnitCm * 10);



    double ParseValueDimensioned(string value, double current) {

        var trim = value.Trim();

        if (trim.EndsWith("cm")) {
            return ParseValue(value, 2, UnitCm);
            }
        else if (trim.EndsWith("em")) {
            return ParseValue(value, 2, FontSize);
            }
        else if (trim.EndsWith("ex")) {
            return ParseValue(value, 2, FontSize / 2);
            }
        else if (trim.EndsWith("in")) {
            return ParseValue(value, 2, UnitInch);
            }
        else if (trim.EndsWith("mm")) {
            return ParseValue(value, 2, UnitMm);
            }
        else if (trim.EndsWith("pc")) {
            return ParseValue(value, 2, UnitPc);
            }
        else if (trim.EndsWith("pt")) {
            return ParseValue(value, 2, UnitPt);
            }
        else if (trim.EndsWith("px")) {
            return ParseValue(value, 2, 1);
            }
        else if (trim.EndsWith("%")) {
            return ParseValue(value, 1, current / 100);
            }
        else {
            return ParseValue(value, 0, 1);
            }
        }

    double ParseValue(string value, int right, double scale) {
        var left = value.Substring(0, value.Length - right);

        try {
            var stringValue = Convert.ToDouble(left);
            return stringValue * scale;
            }
        catch {
            return scale;
            }
        }

    /// <summary>
    /// Create a subordinate element and context.
    /// </summary>
    /// <param name="name">Name of the element to create.</param>
    /// <param name="elem">The element description.</param>
    /// <returns>The context wrapping the new element.</returns>
    public SvgContext CreateElement(string name, ElementProfile elem) {
        var element = Target.Target.CreateElement(name);

        var context = new SvgContext(this, element, elem);
        return context;

        }
    }


public delegate void ProcessAttributeDelegate(SvgContext context, string tag, string value);

/// <summary>
/// Describes an SVG element profile.
/// </summary>
public class ElementProfile {

    ///<summary>The element name.</summary>
    public string Name;

    ///<summary>Dictionary mappng attribute names to values.</summary>
    public Dictionary<string, AttributeProfile> Attributes = new();

    ProcessAttributeDelegate processAttribute;

    static ElementProfile[] ElementProfiles = new ElementProfile[] {
            new ElementProfile ("svg",            null, "width", "height", "viewBox", "preserveAspectRatio", "snapshotTime", "id", "role"),
            new ElementProfile ("g",              null, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor", "fill-rule", "label", "class", "id", "role", "fill", "transform"),
            new ElementProfile ("defs",           null, "id", "role", "fill"),
            new ElementProfile ("title",          null, "id", "role"),
            new ElementProfile ("desc",           null, "id", "role"),
            new ElementProfile ("a",              null, "id", "role", "fill", "transform"),
            new ElementProfile ("use",            null, "id", "role", "transform", "x", "y", "href", "xlink:href"),

            new ElementProfile ("rect",           ProcessAttributeStroke, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor", "fill-rule", "id", "role", "fill", "transform", "x", "y",
                                            "width", "height", "rx", "ry", "stroke-miterlimit"),
            new ElementProfile ("circle",         null, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor",  "fill-rule", "id", "role", "transform",  "x", "y","r", "fill"),
            new ElementProfile ("ellipse",        null, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor", "fill-rule", "id", "role", "transform", "x", "y","rx", "ry", "fill"),
            new ElementProfile ("line",           null, "id", "role", "fill", "transform",  "x1", "y1",  "x2", "y2"),
            new ElementProfile ("polyline",       null, "id", "role", "fill", "transform", "points"),
            new ElementProfile ("polygon",        null, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor", "fill-rule", "id", "role", "transform", "fill", "points"),
            new ElementProfile ("text",           null, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor", "fill-rule", "id", "role", "transform", "fill", "rotate", "space",  "x", "y"),
            new ElementProfile ("tspan",          null, "id", "role",  "x", "y", "fill"),
            new ElementProfile ("textArea",       null, "id", "role", "fill", "transform", "x", "y", "width", "height","auto"),
            new ElementProfile ("tbreak",         null, "id", "role"),

            new ElementProfile ("solidColor",     null, "id", "role", "fill"),
            new ElementProfile ("linearGradient", null, "id", "role", "gradientUnits", "x1", "y1", "x2", "y2"),
            new ElementProfile ("radialGradient", null, "id", "role", "gradientUnits", "cx", "cy", "r"),
            new ElementProfile ("stop",           null, "id", "role", "fill"),
            new ElementProfile ("path",           ProcessAttributeStroke, "font-family", "font-weight", "font-style", "font-variant", "direction", "unicode-bidi",
                                        "text-anchor",  "fill-rule", "id", "role", "fill", "transform", "d", "pathLength",
                                        "stroke-miterlimit")
            };

    ///<summary>Dictionary mapping element names to profiles.</summary>
    public static Dictionary<string, ElementProfile> Elements;


    static ElementProfile() {
        Elements = new Dictionary<string, ElementProfile>();

        foreach (var attr in ElementProfiles) {
            Elements.Add(attr.Name, attr);
            }

        }


    ElementProfile(string name, ProcessAttributeDelegate process = null, params string[] attributes) {
        Name = name;
        processAttribute = process ?? ProcessAttributeDefault;
        foreach (var attribute in attributes) {
            if (AttributeProfile.MainAttributes.TryGetValue(attribute, out var attr)) {
                Attributes.Add(attribute, attr);
                }
            else if (AttributeProfile.OptionalAttributes.TryGetValue(attribute, out attr)) {
                Attributes.Add(attribute, attr);
                }
            else {
                throw new NYI("Configuration error");
                }
            }
        }

    /// <summary>
    /// Process the source element data <paramref name="sourceElement"/> and return
    /// a new processing context for the redacted version in the context <paramref name="parent"/>.
    /// </summary>
    /// <param name="parent">The parent processing context.</param>
    /// <param name="sourceElement">The source element to process.</param>
    /// <returns>The processing context for the redacted version of the element</returns>
    public SvgContext Process(SvgContext parent, XmlElement sourceElement) {
        var targetElement = parent.CreateElement(sourceElement.Name, this);

        foreach (var item in sourceElement.Attributes) {
            if (item is XmlAttribute attribute) {
                if (attribute.Name == "class") {
                    if (parent.Target.Style.TryGetValue(attribute.Value, out var styleValue)) {
                        foreach (var tagvalue in styleValue) {
                            processAttribute(targetElement, tagvalue.Tag, tagvalue.Value);
                            }
                        }
                    else {
                        }
                    }
                else if (attribute.Name == "id") {
                    // just suppress these 
                    }
                else if (attribute.Name == "style") {
                    // just suppress these 
                    }
                else {
                    processAttribute(targetElement, attribute.Name, attribute.Value);
                    }
                }
            }

        return targetElement;
        }




    static void ProcessAttributeDefault(SvgContext context, string tag, string value) {

        context.Attribute(ref tag, ref value);

        var value2 = AttributeProfile.Constrain(tag, value);
        if (value2 != null) {
            context.Element.SetAttribute(tag, value2);
            return;
            }

        if (context.Elem.Attributes.TryGetValue(tag, out var attr)) {
            value2 = attr.Constrain(value);
            if (value2 != null) {
                context.Element.SetAttribute(tag, value2);
                return;
                }
            }
        }

    static void ProcessAttributeStroke(SvgContext context, string tag, string value) {
        if (tag == "stroke" & value == "none") {
            return;
            }
        ProcessAttributeDefault(context, tag, value);
        }

    }

/// <summary>
/// Describes an attribute profile.
/// </summary>
public class AttributeProfile {

    readonly static string[] rfcColor = new string[] {
            "none", "black", "white", "#000000", "#FFFFFF", "#ffffff", "inherit"};


    readonly static AttributeProfile[] groupMain = new AttributeProfile[] {
            new AttributeProfile ("fill-opacity"),
            new AttributeProfile ("stroke-opacity"),
            new AttributeProfile ("fill", rfcColor),
            new AttributeProfile ("fill-rule", "inherit", "nonzero", "evenodd" ),
            new AttributeProfile ("stroke", rfcColor),
            new AttributeProfile ("stroke-dasharray"),
            new AttributeProfile ("stroke-dashoffset"),
            new AttributeProfile ("stroke-linecap", "butt", "round", "square", "inherit"),
            new AttributeProfile ("stroke-linejoin", "miter", "round", "bevel", "inherit"),

            new AttributeProfile ("stroke-width"),
            new AttributeProfile ("color", rfcColor),
            new AttributeProfile ("color-rendering", "auto", "optimizeSpeed", "optimizeQuality", "inherit"),
            new AttributeProfile ("vector-effect", "none", "non-scaling-stroke", "inherit"),
            new AttributeProfile ("direction", "ltr", "rtl", "inherit"),
            new AttributeProfile ("unicode-bidi", "normal", "embed", "bidi-override", "inherit"),
            new AttributeProfile ("solid-color", rfcColor),
            new AttributeProfile ("solid-opacity"),
            new AttributeProfile ("display-align","auto", "before", "center", "after", "inherit"),
            new AttributeProfile ("line-increment"),
            new AttributeProfile ("stop-color", rfcColor),
            new AttributeProfile ("stop-opacity"),
            new AttributeProfile ("font-family", "sans-serif", "serif", "monospace"),
            new AttributeProfile ("font-size"),
            new AttributeProfile ("font-style", "normal", "italic", "oblique", "inherit"),

            new AttributeProfile ("font-variant", "normal", "small-caps", "inherit"),
            new AttributeProfile ("font-weight","normal", "bold", "bolder","lighter"),
            new AttributeProfile ("text-anchor", "start", "middle", "end", "inherit"),
            new AttributeProfile ("text-align", "start", "center", "end", "inherit"),
            new AttributeProfile ("class"),
            new AttributeProfile ("role"),
            new AttributeProfile ("rel"),
            new AttributeProfile ("rev"),
            new AttributeProfile ("typeof"),
            new AttributeProfile ("content"),
            new AttributeProfile ("datatype"),
            new AttributeProfile ("resource"),
            new AttributeProfile ("about"),
            new AttributeProfile ("property"),
            new AttributeProfile ("x"),
            new AttributeProfile ("y"),
            new AttributeProfile ("x1"),
            new AttributeProfile ("y1"),
            new AttributeProfile ("x2"),
            new AttributeProfile ("y2"),
            new AttributeProfile ("rx"),
            new AttributeProfile ("ry"),
            new AttributeProfile ("width"),
            new AttributeProfile ("height"),
            new AttributeProfile ("preserveAspectRatio"),
            new AttributeProfile ("viewBox"),
            new AttributeProfile ("zoomAndPan", "disable"),
            new AttributeProfile ("transform"),
            //new AttributeProfile ("style"),
            new AttributeProfile ("d"),
            //new AttributeProfile ("visibility", "visible" , "hidden" , "collapse" , "inherit"),
            new AttributeProfile ("snapshotTime"),
            new AttributeProfile ("label"),
            new AttributeProfile ("href"),
            new AttributeProfile ("xlink:href"),
            new AttributeProfile ("r"),
            new AttributeProfile ("points"),
            new AttributeProfile ("rotate"),
            new AttributeProfile ("gradientUnits"),
            new AttributeProfile ("cx"),
            new AttributeProfile ("cy"),
            new AttributeProfile ("space"),
            new AttributeProfile ("auto"),
            new AttributeProfile ("pathLength"), 
            // problematic will likely be thrown out
            new AttributeProfile ("dx"),
            new AttributeProfile ("dy"),


            new AttributeProfile ("id")
            };

    readonly static AttributeProfile[] groupOptional = new AttributeProfile[] {
            new AttributeProfile ("stroke-miterlimit"),
            };

    /// <summary>The attributes shared between all elements.</summary>
    public static Dictionary<string, AttributeProfile> MainAttributes;

    ///<summary>Optional elements only supported by specific attributes.</summary>
    public static Dictionary<string, AttributeProfile> OptionalAttributes;
    static AttributeProfile() {
        MainAttributes = new Dictionary<string, AttributeProfile>();
        OptionalAttributes = new Dictionary<string, AttributeProfile>();

        foreach (var attr in groupMain) {
            MainAttributes.Add(attr.Name, attr);
            }
        foreach (var attr in groupOptional) {
            OptionalAttributes.Add(attr.Name, attr);
            }
        }

    ///<summary>The attribute name.</summary>
    public string Name;

    ///<summary>The allowed attribute values.</summary>
    public string[] Allowed;

    /// <summary>
    /// Base constructor for attribute <paramref name="name"/>.
    /// </summary>
    /// <param name="name">Name of the attribute to create</param>
    public AttributeProfile(string name) {
        Name = name;
        }

    /// <summary>
    /// Base constructor for attribute <paramref name="name"/> with values constrained to 
    /// the set specified in <paramref name="allowed"/>.
    /// </summary>
    /// <param name="name">Name of the attribute to create</param>
    /// <param name="allowed"></param>
    public AttributeProfile(string name, params string[] allowed) {
        Name = name;
        Allowed = allowed;
        }

    ///<summary>Counter tracking the number of defaulted attributes.</summary>
    public static int DefaultedAttributes = 0;

    ///<summary>Counter tracking the bnumber of suppressed attributes.</summary>
    public static int SuppressedAttributes = 0;

    /// <summary>
    /// Constrain the value <paramref name="value"/> to the set
    /// permitted for the attribute named <paramref name="name"/> 
    /// </summary>
    /// <param name="name">The name of the attribute to constrain to.</param>
    /// <param name="value">The value to be constrained.</param>
    /// <returns>The constrained value.</returns>
    public static string Constrain(
                string name, string value) {

        if (MainAttributes.TryGetValue(name, out var attr)) {
            return attr.Constrain(value);
            }
        else {
            SuppressedAttributes++;
            return null;
            }
        }

    /// <summary>
    /// Constrain the value <paramref name="value"/> to the set
    /// permitted for the attribute this description describes.
    /// </summary>
    /// <param name="value">The value to be constrained.</param>
    /// <returns>The constrained value.</returns>
    public string Constrain(string value) {
        if (Allowed == null || Allowed.Length == 0) {
            return value;
            }
        foreach (var allow in Allowed) {
            if (allow == value) {
                return value;
                }
            }
        DefaultedAttributes++;
        return Allowed[0];

        }
    }
