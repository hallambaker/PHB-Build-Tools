using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.IO;
using Goedel.Utilities;

namespace Goedel.Document.RFC {

    public class Base64Writer : TextWriter {
        TextWriter Output;

        public Base64Writer (TextWriter Output) {
            Write("Teeeeeeeeeeeeeeeeeeeeeeeeeeeeest!!!");
            WriteLine("YESSSSSSSSSSSSSSSSSSSSS!!!");
            this.Output = Output;
            }

        public override Encoding Encoding => Output.Encoding;

        public override void Write(string value) => base.Write(value);

        public override void Write(char value) => base.Write(value);
        }

    public class XMLEmbed {

        public static void Embed (string Filename, TextWriter TextWriter) {
            var TextReader = Filename.OpenTextReader();
            Embed(TextReader, TextWriter);
            }

        public static void EmbedBase64 (string Filename, TextWriter TextWriter, string attributes) {
            try {
                var TextReader = Filename.OpenTextReader();
                var Output = new StringWriter();
                Embed(TextReader, Output);

                var Base64 = Convert.ToBase64String(Output.ToString().ToUTF8());
                TextWriter.Write("<img ");
                if (attributes != null) {
                    TextWriter.Write(" ");
                    TextWriter.Write(attributes);
                    TextWriter.Write(" ");
                    }
                TextWriter.Write("src=\"data:image/svg+xml;base64,");
                TextWriter.Write(Base64);
                TextWriter.Write("\">");
                }
            catch {
                // file not found
                }
            }

        public static void FaviconBase64 (string Filename, TextWriter TextWriter) {
            try {
                Filename.OpenReadToEnd(out var Data);

                var Base64 = Convert.ToBase64String(Data);
                TextWriter.Write("<link rel=\"icon\" href=\"data:image/svg+xml;base64,");
                TextWriter.Write(Base64);
                TextWriter.Write("\">");
                }
            catch {
                // file not found
                }
            }

        public static void Embed (TextReader TextReader, TextWriter TextWriter) {

            var XmlReaderSettings = new XmlReaderSettings() {
                CheckCharacters = false,
                CloseInput = false,
                ConformanceLevel = ConformanceLevel.Auto,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = false,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = false,
                ValidationType = ValidationType.None
                };

            var XmlWriterSettings = new XmlWriterSettings() {
                CloseOutput = false,
                ConformanceLevel = ConformanceLevel.Fragment,
                Indent = true,
                IndentChars = "  ",
                WriteEndDocumentOnClose = true
                };


            using var XmlReader = XmlTextReader.Create(TextReader, XmlReaderSettings);
            using var XmlWriter = XmlTextWriter.Create(TextWriter, XmlWriterSettings);
            Embed(XmlReader, XmlWriter);

            }


        public static void Embed (XmlReader XMLReader, XmlWriter XmlWriter) {

            //XMLReader.MoveToContent();

            while (XMLReader.Read()) {
                switch (XMLReader.NodeType) {
                    case XmlNodeType.Document: {
                        break;
                        }

                    case XmlNodeType.Element: {
                        XmlWriter.WriteStartElement (XMLReader.Prefix, XMLReader.LocalName, XMLReader.NamespaceURI);
                        if (XMLReader.HasAttributes) {
                            XmlWriter.WriteAttributes(XMLReader, true);
                            }
                        if (XMLReader.IsEmptyElement) {
                            XmlWriter.WriteEndElement();
                            }
                        break;
                        }
                    case XmlNodeType.EndElement: {
                        XmlWriter.WriteEndElement();
                        break;
                        }

                    case XmlNodeType.EntityReference: {
                        XmlWriter.WriteEntityRef(XMLReader.Name);
                        break;
                        }
                    case XmlNodeType.Text: {
                        XmlWriter.WriteString(XMLReader.Value);
                        break;
                        }
                    case XmlNodeType.CDATA: {
                        XmlWriter.WriteCData(XMLReader.Value);
                        break;
                        }
                    case XmlNodeType.Comment: {
                        XmlWriter.WriteComment(XMLReader.Value);
                        break;
                        }
                    case XmlNodeType.SignificantWhitespace:
                    case XmlNodeType.Whitespace: {
                        XmlWriter.WriteString(XMLReader.Value);
                        break;
                        }
                    // Don't think we need these.

                    case XmlNodeType.ProcessingInstruction: {
                        break;
                        }
                    case XmlNodeType.XmlDeclaration: {
                        break;
                        }
                    case XmlNodeType.DocumentType: {
                        break;
                        }
                    }


                }


            XmlWriter.Flush();
            }





        }
    }
