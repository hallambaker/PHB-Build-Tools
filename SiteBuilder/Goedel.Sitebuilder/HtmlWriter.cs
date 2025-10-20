using Goedel.Registry;

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace Goedel.Sitebuilder;


/// <summary>
/// 
/// </summary>
/// <param name="Uri">The resource locator.</param>
/// <param name="Type">The icon type</param>
public record Resource(
            string Uri,
            string Type,
            string? Integrity = null) {
    }

public record Script(
            string Uri,
            string Type,
            string? Integrity = null) : Resource(Uri, Type, Integrity) {
    }

public record Stylesheet(
            string Uri,
            string Type,
            string? Integrity = null) : Resource(Uri, Type, Integrity) {
    }


public record Element(string Tag, string ClassAttribute=null) {
    }


public enum DocumentType {
    XHTML=0
    }



public class HtmlWriter {
    
    public bool Indent { get; set; } = true;
    
    protected TextWriter TextWriter { get; set; }


    Stack<Element> Elements = [];


    public string[] DocumentTypes = [
        "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">"
        ];

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

    public int OpenClassNew(string tag, string classId, params string[] attributes) {


        StartElement(tag);
        Elements.Push(new(tag, classId));
        WriteAttribute("class", classId);
        WriteAttributes(attributes);
        TextWriter.WriteLine(">");

        return Elements.Count - 1;

        }


    public int OpenClass(string tag, string classId, params string[] attributes) {

        var classAttr = EnclosingClass(classId);


        StartElement(tag);
        Elements.Push(new(tag, classAttr));
        WriteAttribute("class", classAttr);
        WriteAttributes(attributes);
        TextWriter.WriteLine(">");

        return Elements.Count - 1;

        }

    public void CloseClass() => Close();



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

    public int Element(string tag, params string[]? attributes) {
        StartElement(tag);
        WriteAttributes(attributes);
        TextWriter.WriteLine("/>");
        return Elements.Count - 1;
        }

    public int ElementClass(string tag, string classId, params string[]? attributes) {
        var classAttr = EnclosingClass(classId);

        StartElement(tag);
        WriteAttribute("class", classAttr);
        WriteAttributes(attributes);
        TextWriter.WriteLine("/>");
        return Elements.Count - 1;
        }

    public void Text(string text, string tag, params string[]? attributes) {
        StartElement(tag);
        WriteAttributes(attributes);
        TextWriter.Write(">");
        Text(text);
        TextWriter.WriteLine($"</{tag}>");
        }

    public void TextClass(string text, string classId, string tag, params string[]? attributes) {
        var classAttr = EnclosingClass(classId);

        StartElement(tag);
        WriteAttribute("class", classAttr);
        WriteAttributes(attributes);
        TextWriter.Write(">");
        Text(text);
        TextWriter.WriteLine($"</{tag}>");
        }

    public void Text(string text) {
        TextWriter.Write(text);
        }


    int positionMain;
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
    public void Body() {
        Close(positionMain);
        positionMain = Open("body");
        }
    public void Finish() {
        Close(positionMain);
        Close(0);
        }

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

    public void EndReources(List<Resource>? resources) {
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


    }
