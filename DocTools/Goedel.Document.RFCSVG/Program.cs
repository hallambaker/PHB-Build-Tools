using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Goedel.Utilities;
using Goedel.IO;
namespace Goedel.Document.RFCSVG {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            var svgDocument = new SvgDocument("ActivationAccount.svg");

            var output = "Redacted.svg".OpenTextWriterNew();
            svgDocument.Save(output);


            }
        }


    class SvgDocument {
        public XmlDocument Source;
        public XmlDocument Target;
        public XmlElement Root;

        public string Prefix => "svg";
        public string Namespace => "http://foo/";

        public SvgDocument(XmlDocument source) => Redact(source);


        public Dictionary<string, List<TagValue>> Style = new Dictionary<string, List<TagValue>>();


        public SvgDocument(string input) {
            var source = new XmlDocument();
            source.Load(input);
            Redact(source);
            }

        public void Save(TextWriter output) {
            using var xmlOutput = new XmlTextWriter(output);
            xmlOutput.Formatting = Formatting.Indented;
            Save(xmlOutput);
            }

        public void Save(XmlWriter output) => Root.WriteTo(output);


        public XmlDocument Redact(XmlDocument source) {
            Source = source; // might as well keep
            Target = new XmlDocument(); // the output



            //Target.Prefix = Prefix;
            //Target.NamespaceURI = Namespace;


            //Root = Target.CreateElement("svg");

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


            Root = MakeElement(Source.DocumentElement);

            Root.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            Root.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");


            //foreach (var child in .ChildNodes) {
            //    if (child is XmlElement element) {
            //        var newChild = MakeElement(element);
            //        if (newChild != null) {
            //            Root.AppendChild(newChild);
            //            }
            //        }


            //    }


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


        XmlElement MakeElement(XmlElement sourceElement) {
            // Should we suppress?
            if (!Elem.Elements.TryGetValue(sourceElement.Name, out var _)) {
                return null;
                }

            // create the element and attach redacted attributes
            var targetElement = Target.CreateElement(sourceElement.Name);
            foreach (var item in sourceElement.Attributes) {
                if (item is XmlAttribute attribute) {
                    if (attribute.Name == "class") {
                        if (Style.TryGetValue(attribute.Value, out var styleValue)) {
                            foreach (var tagvalue in styleValue) {
                                targetElement.SetAttribute(tagvalue.Tag, tagvalue.Value);
                                }
                            }
                        }
                    else {

                        var (name, value) = Attr.Constrain(attribute);
                        if (value != null) {
                            targetElement.SetAttribute(name, value);
                            }
                        }
                    }
                }

            // attach the child nodes
            foreach (var child in sourceElement.ChildNodes) {
                if (child is XmlElement element) {
                    var newChild = MakeElement(element);
                    if (newChild != null) {
                        targetElement.AppendChild(newChild);
                        }
                    }
                else if (child is XmlText text) {
                    var newText = Target.CreateTextNode(text.InnerText);
                    targetElement.AppendChild(newText);
                    }
                }

            return targetElement;
            }



        }

    public class Elem {

        public string Name;

        static Elem[] elements = new Elem[] {
            new Elem ("desc"),
            new Elem ("svgTitle"),
            new Elem ("path"),
            new Elem ("rect"),
            new Elem ("circle"),
            new Elem ("line"),
            new Elem ("ellipse"),
            new Elem ("polyline"),
            new Elem ("polygon"),
            new Elem ("solidColor"),
            new Elem ("textArea"),
            new Elem ("linearGradient"),
            new Elem ("radialGradient"),
            new Elem ("g"),
            new Elem ("defs"),
            new Elem ("use"),
            new Elem ("a"),
            new Elem ("tspan"),
            new Elem ("text"),
            new Elem ("svg")
            };

        public static Dictionary<string, Elem> Elements;

        static Elem() {
            Elements = new Dictionary<string, Elem>();

            foreach (var attr in elements) {
                Elements.Add(attr.Name, attr);
                }

            }

        Elem(string name) => Name = name;

        }
    public class Attr {

        readonly static string[] rfcColor = new string[] {
            "none", "black", "white", "#000000", "#FFFFFF", "#ffffff", "inherit"};
        

        public readonly static Attr[] GroupMain = new Attr[] {
            new Attr ("fill-opacity"),
            new Attr ("stroke-opacity"),
            new Attr ("fill", rfcColor),
            new Attr ("fill-rule", "inherit", "nonzero", "evenodd" ),
            new Attr ("stroke", rfcColor),
            new Attr ("stroke-dasharray"),
            new Attr ("stroke-dashoffset"),
            new Attr ("stroke-linecap", "butt", "round", "square", "inherit"),
            new Attr ("stroke-linejoin", "miter", "round", "bevel", "inherit"),
            new Attr ("stroke-miterlimit"),
            new Attr ("stroke-width"),
            new Attr ("color", rfcColor),
            new Attr ("color-rendering", "auto", "optimizeSpeed", "optimizeQuality", "inherit"),
            new Attr ("vector-effect", "none", "non-scaling-stroke", "inherit"),
            new Attr ("direction", "ltr", "rtl", "inherit"),
            new Attr ("unicode-bidi", "normal", "embed", "bidi-override", "inherit"),
            new Attr ("solid-color", rfcColor),
            new Attr ("solid-opacity"),
            new Attr ("display-align","auto", "before", "center", "after", "inherit"),
            new Attr ("line-increment"),
            new Attr ("stop-color", rfcColor),
            new Attr ("stop-opacity"),
            new Attr ("font-family"),
            new Attr ("font-size"),
            new Attr ("font-style", "normal", "italic", "oblique", "inherit"),

            new Attr ("font-variant", "normal", "small-caps", "inherit"),
            new Attr ("font-weight","normal", "bold", "bolder","lighter"),
            new Attr ("text-anchor", "start", "middle", "end", "inherit"),
            new Attr ("text-align", "start", "center", "end", "inherit"),
            new Attr ("class"),
            new Attr ("role"),
            new Attr ("rel"),
            new Attr ("rev"),
            new Attr ("typeof"),
            new Attr ("content"),
            new Attr ("datatype"),
            new Attr ("resource"),
            new Attr ("about"),
            new Attr ("property"),
            new Attr ("x"),
            new Attr ("y"),
            new Attr ("rx"),
            new Attr ("ry"),
            new Attr ("width"),
            new Attr ("height"),
            new Attr ("preserveAspectRatio"),
            new Attr ("viewBox"),
            new Attr ("zoomAndPan", "disable"),
            new Attr ("transform"),
            new Attr ("style"),
            new Attr ("d"),
            new Attr ("visibility", "visible" , "hidden" , "collapse" , "inherit"),
            

            // problematic will likely be thrown out
            //new Attr ("dx"),
            //new Attr ("dy"),


            new Attr ("id")
            };


        static Dictionary<string, Attr> mainAttributes;
        static Attr() {
            mainAttributes = new Dictionary<string, Attr>();

            foreach (var attr in GroupMain) {
                mainAttributes.Add(attr.Name, attr);
                }

            }

        public string Name;
        public string[] Allowed;

        public Attr(string name) {
            Name = name;
            }
        public Attr(string name, params string[] allowed) {
            Name = name;
            Allowed = allowed;
            }

        public static int DefaultedAttributes = 0;
        public static int SuppressedAttributes = 0;

        public static (string, string) Constrain(
                    XmlAttribute attribute) {
            //if (attribute.Name == "class") {
            //    if (style.TryGetValue(attribute.Value, out var styleValue)) {
            //        //return ("style", styleValue);
            //        throw new NYI("Style not found");

            //        }
            //    else {
            //        throw new NYI("Style not found");
            //        }

            //    }
            if (mainAttributes.TryGetValue(attribute.Name, out var attr)) {
                if (attr.Allowed == null || attr.Allowed.Length == 0) {
                    return (attribute.Name, attribute.Value);
                    }
                foreach (var allow in attr.Allowed) {
                    if (allow == attribute.Value) {
                        return (attribute.Name, attribute.Value);
                        }
                    }
                DefaultedAttributes++;
                return (attribute.Name, attr.Allowed[0]); 

                }
            else {
                SuppressedAttributes++;
                return (null, null);
                }
            }


        }

    }
