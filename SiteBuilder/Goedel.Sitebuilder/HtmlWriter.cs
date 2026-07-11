using Goedel.Registry;

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace Goedel.Sitebuilder;



/// <summary>
/// Resource entry on page.
/// </summary>
/// <param name="Uri">The resource locator.</param>
/// <param name="Type">The resource type</param>
/// <param name="Integrity">Optional integrity specifier.</param>
public record Resource(
            string Uri,
            string Type,
            string? Integrity = null) {
    }

/// <summary>Script entry on page.</summary>
/// <param name="Uri">URI to load the script from</param>
/// <param name="Type">The resource type</param>
/// <param name="Integrity">Optional integrity specifier.</param>
public record Script(
            string Uri,
            string Type,
            string? Integrity = null) : Resource(Uri, Type, Integrity) {
    }

/// <summary>
/// Resource entry on page.
/// </summary>
/// <param name="Uri">The resource locator.</param>
/// <param name="Type">The resource type</param>
/// <param name="Integrity">Optional integrity specifier.</param>
public record Stylesheet(
            string Uri,
            string Type,
            string? Integrity = null) : Resource(Uri, Type, Integrity) {
    }

/// <summary>Element</summary>
/// <param name="Tag">Tag name</param>
/// <param name="ClassAttribute">Attribute</param>
public record Element(string Tag, string ClassAttribute=null) {
    }

/// <summary>The document types</summary>
public enum DocumentType {
    /// <summary>XHTML document type.</summary>
    XHTML=0
    }


/// <summary>Write HTML output</summary>
public class HtmlWriter {
    
    /// <summary>If true, indent the output for ease of reading.</summary>
    public bool Indent { get; set; } = true;
    
    /// <summary>Output writer.</summary>
    protected TextWriter TextWriter { get; set; }


    Stack<Element> Elements = [];

    /// <summary>Document preamble document type.</summary>
    public string[] DocumentTypes = [
        "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">"
        ];

    /// <summary>Constructor, returns a new instance bound to the writer <paramref name="textWriter"/></summary>
    /// <param name="textWriter">The output stream.</param>
    public HtmlWriter(
            TextWriter textWriter
            ) {
        TextWriter = textWriter;
        }
    void StartLine() {
        if (Indent) {
            for (var i = 0; i < Elements.Count; i++) {
                TextWriter.Write("  ");
                }
            }
        }
    void StartElement(string tag) {
        StartLine();
        TextWriter.Write($"<{tag}");
        }

    void WriteAttributes(string[] attributes) {
        for (var i= 0; i+1 < attributes.Length; i+=2) {
            if (attributes[i + 1] is not null) {
                WriteAttribute(attributes[i], attributes[i + 1]);
                }
            }
        }

    void WriteAttribute(string tag, string value) {
        TextWriter.Write(" ");
        TextWriter.Write(tag);
        TextWriter.Write("=\"");
        TextWriter.Write(value);
        TextWriter.Write("\"");
        }

    string EnclosingClass(string classId) {
        classId = classId.Replace(".", "");

        var array = Elements.ToArray();

        for (var i = 0; i < Elements.Count; i++) {
            var classAttribute = array[i].ClassAttribute;
            if (classAttribute != null) {
                return classAttribute + " " + classId;
                }
            }
        return classId;

        }
    //string NormalizeId(string id) => id.Replace(".", "");

    ///// <summary>Start class</summary>
    ///// <param name="tag">Tag</param>
    ///// <param name="classId">Identifier</param>
    ///// <param name="attributes">List of {tag, value} attributes</param>
    ///// <returns></returns>
    //public int OpenClassNew(string tag, string classId, params string[] attributes) {


    //    StartElement(tag);
    //    Elements.Push(new(tag, classId));
    //    WriteAttribute("class", classId);
    //    WriteAttributes(attributes);
    //    TextWriter.WriteLine(">");

    //    return Elements.Count - 1;

    //    }

    /// <summary>Start class</summary>
    /// <param name="tag">Tag</param>
    /// <param name="classId">Identifier</param>
    /// <param name="attributes">List of {tag, value} attributes</param>
    /// <returns></returns>
    public int OpenClass(string tag, string classId, params string[] attributes) {

        var classAttr = EnclosingClass(classId);


        StartElement(tag);
        Elements.Push(new(tag, classAttr));
        WriteAttribute("class", classAttr);
        WriteAttributes(attributes);
        TextWriter.WriteLine(">");

        return Elements.Count - 1;

        }

    ///// <summary>Close class.</summary>
    //public void CloseClass() => Close();



    /// <summary>
    /// Start an element <paramref name="tag"/> with attribute value pairs from
    /// <paramref name="attributes"/>.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <param name="attributes">The attributes.</param>
    /// <returns>The stack position.</returns>
    public int Open(string tag, params string[] attributes) {
        StartElement(tag);
        Elements.Push(new(tag));
        WriteAttributes(attributes);
        TextWriter.WriteLine(">");

        return Elements.Count-1;
        }

    /// <summary>
    /// Close the immediately preceding tag. If <paramref name="position"/> is
    /// specified, the value is checked against the corresponding Open.
    /// </summary>
    /// <param name="position">Expected stack depth.</param>
    public void Close(int position = -1) {
        (position < 0 | position == Elements.Count - 1).AssertTrue(NestingIncorrect.Throw);
        var tag = Elements.Pop();
        StartLine();
        TextWriter.WriteLine($"</{tag.Tag}>");
        }

    /// <summary>HTML element, with tag <paramref name="tag"/> and attributes <paramref name="attributes"/></summary>
    /// <param name="tag">The tag</param>
    /// <param name="attributes">A list of {tag, value} pairs defining attributes.</param>
    /// <returns></returns>
    public int Element(string tag, params string[]? attributes) {
        StartElement(tag);
        WriteAttributes(attributes);
        TextWriter.WriteLine("/>");
        return Elements.Count - 1;
        }

    /// <summary>HTML element, with tag <paramref name="tag"/> and attributes 
    /// <paramref name="attributes"/> that declares itself to be of class
    /// <paramref name="classId"/></summary>
    /// <param name="tag">The tag</param>
    /// <param name="attributes">A list of {tag, value} pairs defining attributes.</param>
    /// <param name="classId">The value of the class attribute.</param>
    /// <returns></returns>
    public int ElementClass(string tag, string classId, params string[]? attributes) {
        var classAttr = EnclosingClass(classId);

        StartElement(tag);
        WriteAttribute("class", classAttr);
        WriteAttributes(attributes);
        TextWriter.WriteLine("/>");
        return Elements.Count - 1;
        }

    /// <summary>Write a text block <paramref name="text"/> wrapped in an element
    /// <paramref name="tag"/> with attributes <paramref name="attributes"/></summary>
    /// <param name="text">Text to write.</param>
    /// <param name="tag">The wrapper element class.</param>
    /// <param name="attributes">A list of {tag, value} pairs defining attributes.</param>
    public void Text(string text, string tag, params string[]? attributes) {
        StartElement(tag);
        WriteAttributes(attributes);
        TextWriter.Write(">");
        Text(text);
        TextWriter.WriteLine($"</{tag}>");
        }

    //public void TextVerbatim(string text, string tag, params string[]? attributes) {
    //    StartElement(tag);
    //    WriteAttributes(attributes);
    //    TextWriter.Write(">");
    //    Text(text);
    //    TextWriter.WriteLine($"</{tag}>");
    //    }

    /// <summary>Write a text block <paramref name="text"/> wrapped in an element
    /// <paramref name="tag"/> with attributes <paramref name="attributes"/></summary>
    /// <param name="text">Text to write.</param>
    /// <param name="tag">The wrapper element class.</param>
    /// <param name="attributes">A list of {tag, value} pairs defining attributes.</param>
    /// <param name="classId">The value of the class attribute.</param>
    public void TextClass(string text, string classId, string tag, params string[]? attributes) {
        var classAttr = EnclosingClass(classId);

        StartElement(tag);
        WriteAttribute("class", classAttr);
        WriteAttributes(attributes);
        TextWriter.Write(">");
        Text(text);
        TextWriter.WriteLine($"</{tag}>");
        }

    /// <summary>
    /// Write the specified textg to the stream as an escaped HTML string.
    /// </summary>
    /// <param name="text">The textg to write.</param>
    public void Text(string text) {


        TextWriter.Write(HttpUtility.HtmlEncode( text));
        }


    /// <summary>
    /// Write the specified textg to the stream as an escaped HTML string.
    /// </summary>
    /// <param name="text">The textg to write.</param>
    public void TextVerbatim(string text) {


        TextWriter.Write(text);
        }


    int positionMain;

    /// <summary>Write out the document head part with the document type, title, etc.</summary>
    /// <param name="title">The document title.</param>
    /// <param name="faviCon">Page icon.</param>
    /// <param name="docType">The document type.</param>
    /// <param name="language">The document language.</param>
    public void Head(
                string title,
                Resource faviCon,
                DocumentType docType = DocumentType.XHTML, 
                string language = "en") {
        TextWriter.WriteLine(DocumentTypes[(int)docType]);
        Open("html", "lang", language);
        positionMain = Open("head");
        Element("meta", "charset", "utf-8");
        Text(title, "title");
        if (faviCon is not null) {
            Element("link", "rel", "icon", "type", faviCon.Type, "href", faviCon.Uri);
            }
        }

    /// <summary>Begin writing the document body.</summary>
    public void Body() {
        Close(positionMain);
        positionMain = Open("body");
        }

    /// <summary>Finish writing the document, append the footer.</summary>
    public void Finish() {
        Close(positionMain);
        Close(0);
        }

    /// <summary>Write out the resources at the start or end of the document.</summary>
    /// <param name="resources">The resources to write</param>
    public void Reources(List<Resource>? resources) {
        foreach (var resource in resources.IfEnumerable()) {
            switch (resource) {
                case Stylesheet stylesheet: {
                    Element("link", "rel", "stylesheet", "type", resource.Type, "href", resource.Uri);
                    break;
                    }
                case Script script: {
                    Text("", "script", "type", resource.Type, "src", resource.Uri, "integrity", resource.Integrity);
                    break;
                    }
                }
            }
        }

    //public void EndReources(List<Resource>? resources) {
    //    foreach (var resource in resources.IfEnumerable()) {
    //        switch (resource) {
    //            case Stylesheet stylesheet: {
    //                Element("link", "rel", "stylesheet", "type", resource.Type, "href", resource.Uri);
    //                break;
    //                }
    //            case Script script: {
    //                Text("", "script", "type", resource.Type, "src", resource.Uri, "integrity", resource.Integrity);
    //                break;
    //                }
    //            }
    //        }
    //    }


    }
