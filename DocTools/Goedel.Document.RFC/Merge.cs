using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Goedel.Document.RFC {

    public partial class Writers {
        public static void Merge (string InputFile, string OutputFile) {
            Translate Translate = new();

            Encoding UTF8WithoutBOM = new UTF8Encoding (false);

            using XmlTextReader XmlTextReader = new(InputFile);
            using XmlTextWriter XmlTextWriter = new(OutputFile, UTF8WithoutBOM);

            //XmlTextReader.DtdProcessing = DtdProcessing.Ignore;

            Translate.ReadStream(XmlTextReader, XmlTextWriter, true);
            }
        }


    class Translate {
        public void ReadStream(XmlReader XmlReader, XmlWriter XmlWriter, bool StartDocument) {
            
            XmlReader.Read();
            while (!XmlReader.EOF) {
                bool ReadNext = true;
                switch (XmlReader.NodeType) {
                    case XmlNodeType.Element:
                        bool IsEmpty = XmlReader.IsEmptyElement;
                        //Console.Write("<{0}", XmlReader.Name);
                        XmlWriter.WriteStartElement(XmlReader.Name);
                        if (XmlReader.HasAttributes) {
                            while (XmlReader.MoveToNextAttribute()) {
                                //Console.Write(" {0}=\"{1}\"", XmlReader.Name, XmlReader.Value);
                                XmlWriter.WriteAttributeString(XmlReader.Name, XmlReader.Value);
                                }
                            }
                        if (IsEmpty) {
                            XmlWriter.WriteEndElement();
                            //Console.Write("/");
                            }
                        //Console.Write(">");
                        // process attributes
                        break;
                    case XmlNodeType.Text:
                        //Console.Write(XmlReader.Value);
                        XmlWriter.WriteString (XmlReader.Value);
                        break;
                    case XmlNodeType.Whitespace:
                        //Console.Write(XmlReader.Value);
                        XmlWriter.WriteWhitespace (XmlReader.Value);
                        break;
                    case XmlNodeType.CDATA:
                        //Console.Write("<![CDATA[{0}]]>", XmlReader.Value);
                        XmlWriter.WriteCData (XmlReader.Value);
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        //Console.Write("<?{0} {1}?>", XmlReader.Name, XmlReader.Value);
                        if (XmlReader.Name == "include") {
                            ProcessInclude(XmlReader.Value, XmlWriter);
                            }
                        else {
                            XmlWriter.WriteProcessingInstruction(XmlReader.Name, XmlReader.Value);
                            }
                        break;
                    case XmlNodeType.Comment:
                        //Console.Write("<!--{0}-->", XmlReader.Value);
                        XmlWriter.WriteComment (XmlReader.Value);
                        break;
                    case XmlNodeType.XmlDeclaration:
                        if (StartDocument) {
                            //Console.Write("<?xml version='1.0' encoding=\"utf-8\"?>");
                            XmlWriter.WriteStartDocument();
                            }
                        break;
                    case XmlNodeType.Document:
                        break;
                    case XmlNodeType.DocumentType:
                        string Name = XmlReader.Name, PUBLIC=null, SYSTEM=null, Subset = XmlReader.Value;
                        //Console.Write("<!DOCTYPE {0} [{1}]>", XmlReader.Name, XmlReader.ToString());
                        if (XmlReader.HasAttributes) {
                            while (XmlReader.MoveToNextAttribute()) {
                                //Console.Write(" {0}=\"{1}\"", XmlReader.Name, XmlReader.Value);
                                if (XmlReader.Name == "SYSTEM") {
                                    SYSTEM = XmlReader.Value;
                                    }
                                if (XmlReader.Name == "PUBLIC") {
                                    PUBLIC = XmlReader.Value;
                                    }
                                }
                            }
                        XmlWriter.WriteDocType (Name, PUBLIC, SYSTEM, Subset);
                        break;
                    case XmlNodeType.EntityReference:
                        //Console.Write(XmlReader.Name);
                        XmlWriter.WriteEntityRef (XmlReader.Name);
                        break;
                    case XmlNodeType.EndElement:
                        //Console.Write("</{0}>", XmlReader.Name);
                        XmlWriter.WriteEndElement ();
                        break;
                    default:
                        //Console.WriteLine ("*******************");
                        break;
                    }
                if (ReadNext) {
                    XmlReader.Read ();
                    }
                }
            if (StartDocument) {
                XmlWriter.WriteEndDocument();
                }
            }


        public void ProcessInclude(string String, XmlWriter XmlWriter) {
            string AttributeXML = GetAttributeValue ("xml", String);
            string AttributeMIME = GetAttributeValue ("mime", String);
            string AttributeSection = GetAttributeValue ("section", String);

            //Console.WriteLine ("####Process Include xml={0} mime={1} section={2}", 
            //        AttributeXML, AttributeMIME, AttributeSection);

            if (AttributeXML != null) {
                using XmlTextReader XmlTextReader = new(AttributeXML);
                ReadStream(XmlTextReader, XmlWriter, false); // recursively process the file
                }
            if (AttributeMIME != null) {
                throw new Exception ("MIME include not yet implemented");
                }
            }

        private bool IsWhitespace(char c) => ((c == ' ') | (c == '\t') | (c == '\r') | (c == 'n'));

        private bool IsTag(char c) => ((c >= 'a') & (c <= 'z'));

        private string GetAttributeValue(string Tag, string String) {
            string result = null;
            int state = 0;
            bool match = false;
            int Index = 0;

            foreach (char c in String) {
                switch (state) {
                    case 0:
                        if (IsTag(c)) {
                            state = 1; Index = 1; match = c == Tag [0];
                            }
                        else if (!IsWhitespace(c)) {
                            throw new Exception ("Parse error in include");
                            }
                        break;
                    case 1:
                        if (IsTag(c)) {
                            match &= (c == Tag [Index++]);
                            }
                        else if (IsWhitespace(c)) {
                            match = (match & Index == Tag.Length);
                            state = 2;
                            }
                        else if (c == '=') {
                            match = (match & Index == Tag.Length);
                            state = 3;
                            }
                        else {
                            throw new Exception ("Parse error in include");
                            }
                        break;
                    case 2:
                        if (IsWhitespace(c)) {
                            state = 2;
                            }
                        else if (c == '=') {
                            state = 3;
                            }
                        else {
                            throw new Exception ("Parse error in include");
                            }
                        break;
                    case 3:
                        if (IsWhitespace(c)) {
                            state = 3;
                            }
                        else if (c == '"') {
                            state = 4;
                            }
                        else {
                            throw new Exception ("Parse error in include");
                            }
                        break;
                    case 4:
                        if (c == '"') {
                        if (match) {
                            return result;
                            }
                            state = 0;
                            }
                        else if (match) {
                            result += c;
                            }
                        break;
                    }
                }


            return null;
            }
        }
    }
