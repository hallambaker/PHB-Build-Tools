﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Resolvers;
using System.IO;
using System.Text;
using Goedel.Tool.RFCTool;

namespace Goedel.Tool.RFCTool {
    // New Parser because the old one was getting incomprehensible
    // Read the whole file into memory and process from the resulting tree
    public class NewParse {
        private abstract class XML {
            public abstract string TextValue { get; }
            //public abstract List<Text> ToTextList { get; }
            }

        //private class Attribute {
        //    public string Tag;
        //    public string Value;
        //    }

        public class Processing {
            public string Tag;
            public string File;
            public string Format;
            public string Cache;

            private bool IsAlpha(char c) {
                if ((c >= 'a' & c <= 'z') | (c >= 'A' & c <= 'Z')) return true;
                return false;
                }

            public Processing(string Tag, string Text) {
                this.Tag = Tag.ToLower();
                this.File = null;
                this.Format = null;

                int State = 0;
                string T = "", V = "";
                foreach (char c in Text) {
                    if (State == 0) {
                        if (IsAlpha(c)) {
                            T += c;
                            State = 1;
                            }
                        }
                    else if (State == 1) {
                        if (IsAlpha(c)) {
                            T += c;
                            }
                        else if (c == '=') {
                            State = 2;
                            }
                        }
                    else if (State == 2) {
                        if (IsAlpha(c)) {
                            T += c;
                            }
                        if (c == '\"') {
                            V = "";
                            State = 3;
                            }
                        }
                    else if (State == 3) {

                        if (c == '\"') {
                            if (T.ToLower() == "file") {
                                File = V;
                                }
                            else if (T.ToLower() == "format") {
                                Format = V;
                                }
                            else if (T.ToLower() == "cache") {
                                Cache = V;
                                }
                            V = "";
                            T = "";
                            State = 0;
                            }
                        else {
                            V += c;
                            }
                        }
                    }
                }
            }


        private class Element : XML {
            public static string StripSpaces(string Input) {
                string Result = "";
                bool TextSpace = false;
                foreach (char c in Input) {
                    if ((c == ' ') | (c == '\n') | (c == '\t')) {
                        if (TextSpace) {
                            TextSpace = false;
                            Result += ' ';
                            }
                        }
                    else if (c == 160) {
                        Result += " ";
                        }
                    else {
                        Result += c;
                        TextSpace = true;
                        }
                    }
                return Result;
                }


            public override string TextValue {
                get {
                    string Result = "";
                    foreach (XML XML in Contents) {
                        Result += XML.TextValue;
                        }
                    return Result;
                    }
                }

            public string FilteredText {
                get {
                    List<Text> TextList = this.ToTextList;

                    return StripSpaces(TextValue);
                    }
                }


            enum TextMode {
                Literal,
                StripSpace,
                Normative
                }


            private void TextListAdd (ref List<Text> TextList, List<XML> XMLList, TextMode TextMode, TextType TextType) {

                    foreach (XML XML in XMLList) {
                        if (XML.GetType() == typeof(Element)) {
                            Element Element = (Element)XML;

                            switch (Element.Tag.ToLower()) {
                                case "span": {
                                        break;
                                        }
                                case "a": {
                                        break;
                                        }
                                case "dfn": {
                                        TextListAdd (ref TextList, Element.Contents, TextMode, TextType.Definition);
                                        break;
                                        }
                                case "code":
                                case "kbd":
                                case "var": {
                                        TextListAdd (ref TextList, Element.Contents, TextMode, TextType.Keyboard);
                                        break;
                                        }
                                case "strong": {
                                        TextListAdd (ref TextList, Element.Contents, TextMode, TextType.Strong);
                                        break;
                                        }
                                case "em": {
                                        TextListAdd (ref TextList, Element.Contents, TextMode, TextType.Emphasis);
                                        break;
                                        }
                                default: {
                                        break;
                                        }
                                }

                            }
                        else if (XML.GetType() == typeof(Content)) {
                            Content Content = (Content)XML;
                            if (Content.ContentType == ContentType.Text) {
                                Text Text = new Text (TextType);
                                //Text.Data =  SpaceStrip ? StripSpaces (Content.Text) : Content.Text;


                                // the microparser for [] goes here
                                }
                            }
                        }
                }

            public List<Text> ToTextList {
                get {
                    List<Text> TextList = new List<Text>();
                    TextListAdd(ref TextList, Contents, 
                        (Tag == "pre") ? TextMode.Literal : TextMode.StripSpace, TextType.Plain);
                    return TextList;
                    }
                }

            public string Tag;
            public List<XML> Contents = new List<XML>();

            public String Id = null;
            public String Class = null;
            public String Href = null;

            public String Initials = null;
            public String Surname = null;
            public String Fullname = null;
            public String Abbrev = null;

            public String Day = null;
            public String Month = null;
            public String Year = null;

            public String Name = null;
            public String Value = null;
            public String Type = null;
            public String Octets = null;
            public String Target = null;

            public String Anchor = null;
            }

        enum ContentType {
            Text, Whitespace, Entity, Cdata
            }

        private class Content : XML {

            public override string TextValue {
                get {
                    return Text;
                    }
                }

            public string Text;
            public ContentType ContentType;

            public Content(string Text, ContentType ContentType) {
                this.Text = Text;
                this.ContentType = ContentType;
                }
            }


        TextReader TextReader;
        Document Document;
        XmlReader XmlReader;

        Element XMLRoot;
        Element Root = new Element();

        public static void Parse(string File, Document Document) {
            using (FileReader FileReader = new FileReader(File)) {
                Parse(FileReader, Document);
                }
            }

        public static void Parse(TextReader TextReader, Document Document) {
            var NewParse = new NewParse(TextReader, Document);
            }

        public NewParse(TextReader FileReader, Document Document) {
            this.TextReader = FileReader;
            this.Document = Document;

            XmlPreloadedResolver XmlPreloadedResolver = new XmlPreloadedResolver();

            XmlReaderSettings XmlReaderSettings = new XmlReaderSettings();
            XmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
            XmlReaderSettings.IgnoreWhitespace = true;
            XmlReaderSettings.IgnoreComments = true;
            XmlReaderSettings.CheckCharacters = false;
            XmlReaderSettings.XmlResolver = XmlPreloadedResolver;
            XmlReader = XmlReader.Create(FileReader, XmlReaderSettings);

            ReadStream (XmlReader);
            MakeDocument(XMLRoot);

            }


        public NewParse(String Reference, Document Document) {
            StringReader StringReader = new StringReader (Reference);
            this.Document = Document;
            ParseReferences (StringReader);
            }

        public NewParse(Processing Processing, Document Document) {

            using (FileReader BibFileReader = new FileReader(Processing.File)) {
                this.Document = Document;

                ParseReferences (BibFileReader);
                }
            }

        void ParseReferences(TextReader TextReader) {
                XmlPreloadedResolver XmlPreloadedResolver = new XmlPreloadedResolver();

                XmlReaderSettings XmlReaderSettings = new XmlReaderSettings();
                XmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
                XmlReaderSettings.IgnoreWhitespace = true;
                XmlReaderSettings.IgnoreComments = true;
                XmlReaderSettings.CheckCharacters = false;
                XmlReaderSettings.XmlResolver = XmlPreloadedResolver;
                XmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
                XmlReader = XmlReader.Create(TextReader, XmlReaderSettings);

                ReadStream(XmlReader);

                MakeReferences (Root.Contents );
            }

        void MakeDocument(Element Root) {
            switch (Root.Tag) {
                case "html": MakeHtmlDocument(Root); break;
                case "rfc": MakeXmlDocument(Root); break;
                }
            }

        void MakeHtmlDocument(Element Root) {

            int BodyIndex = 0;
            int FrontIndex = 0;
            int MiddleIndex;

            Element Body, Front, Middle;

            // find body element
            Body = GetNextElement(Root.Contents, "body", ref BodyIndex);
            if (Body == null) throw new Exception("No HTML Body");

            Front = GetNextElement(Body.Contents, "h1", ref FrontIndex);
            if (Body == null) throw new Exception("No H1 Title Element");

            MiddleIndex = FrontIndex + 1;
            Middle = GetNextElement(Body.Contents, "h1", ref MiddleIndex);
            if (Body == null) throw new Exception("No H1 Start of Document Element");

            MakeHTMLFront(Body.Contents, FrontIndex, MiddleIndex);
            MakeHTMLMiddle(Body.Contents, MiddleIndex);
            }


        P MakePBlock(Element Element) {
            P P = new P(Element.Value, Element.Id);


            return P;
            }

        void MakeHTMLFront(List<XML> Contents, int Start, int End) {

            // The first element is always <H1>
            Element Element = (Element)Contents[Start];
            Document.Title = Element.FilteredText;


            for (int Index = Start + 1; Index < End; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element = (Element)Contents[Index];
                    string FilteredText = Document.Catalog.GetCitation (Element.FilteredText);

                    switch (Element.Tag.ToLower()) {
                        case "p": {
                                if (FilteredText != null && FilteredText != "") {
                                    Document.Abstract.Add(new P(FilteredText, Element.Id));
                                    }
                                break;
                                }
                        case "dl": {
                                MakeProperties(Element.Contents);
                                break;
                                }
                        }
                    }
                }
            }

        void MakeProperties(List<XML> Contents) {
            string Tag = "";

            for (int Index = 0; Index < Contents.Count; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)Contents[Index];
                    string FilteredText = Document.Catalog.GetCitation (Element.FilteredText);

                    switch (Element.Tag.ToLower()) {
                        case "dt": {
                                Tag = FilteredText.ToLower();
                                break;
                                }
                        case "dd": {
                                bool DocProperty = SetDocumentProperty(Tag, FilteredText);
                                if (!DocProperty & Document.Authors.Count > 0) {
                                    SetAuthorProperty(Document.Authors[Document.Authors.Count - 1],
                                        Tag, FilteredText);
                                    }
                                break;
                                }
                        }
                    }
                }
            }

        bool Back = false;

        Section[] Sections = new Section[6];
        int SectionLevel = 0;
        Section CurrentSection;
        static string BackPrefix = "Appendix:";
        Section SetSection(Element Element) {
            int Level = HeadingLevel(Element.Tag);
            string Title = Element.FilteredText;

            if (Level == 0 & Title.StartsWith(BackPrefix)) {
                Back = true;
                Title = Title.Substring(BackPrefix.Length);
                }

            CurrentSection = new Section(
                Document.Catalog.GetCitation (Title), Element.Id);

            if (Level == 0) {
                
                if (Back) {
                    Document.Back.Add(CurrentSection);
                    }
                else {
                    Document.Middle.Add(CurrentSection);
                    }
                }
            else {
                Sections[Level - 1].Subsections.Add(CurrentSection);
                }
            SectionLevel = Level;
            Sections[Level] = CurrentSection;

            return CurrentSection;
            }

        int HeadingLevel(string Tag) {
            if (Tag == "h1") return 0;
            if (Tag == "h2") return 1;
            if (Tag == "h3") return 2;
            if (Tag == "h4") return 3;
            if (Tag == "h5") return 4;
            if (Tag == "h6") return 5;
            return -1;
            }

        void MakeHTMLMiddle(List<XML> Contents, int Start) {
            for (int Index = Start; Index < Contents.Count; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)Contents[Index];
                    string FilteredText = Document.Catalog.GetCitation (Element.FilteredText);

                    switch (Element.Tag.ToLower()) {
                        case "h1":
                        case "h2":
                        case "h3":
                        case "h4":
                        case "h5":
                        case "h6": {
                                SetSection(Element);
                                break;
                                }
                        case "p": {
                                CurrentSection.TextBlocks.Add(
                                    new P(FilteredText, Element.Id));
                                break;
                                }
                        case "pre": {
                                CurrentSection.TextBlocks.Add(
                                    new PRE(Element.TextValue, Element.Id));
                                break;
                                }
                        case "ol": {
                                MakeList(Element.Contents, ListItem.Ordered, 0);
                                break;
                                }
                        case "ul": {
                                MakeList(Element.Contents, ListItem.Symbol, 0);
                                break;
                                }
                        case "dl": {
                                MakeList(Element.Contents, ListItem.Definitions, 0);
                                break;
                                }
                        case "table": {
                                CurrentSection.TextBlocks.Add(MakeTable(Element.Contents));
                                break;
                                }
                        default: {
                                break;
                                }
                        }
                    }
                }
            }

        void MakeReferences(List<XML> Contents) {
            for (int Index = 0; Index < Contents.Count; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)Contents[Index];
                    switch (Element.Tag.ToLower()) {
                        case "reference": {
                                MakeReference(Element);
                                break;
                                }
                        }
                    }
                }
            }

        void MakeFront(Reference Reference, Element Element) {
            for (int Index = 0; Index < Element.Contents.Count; Index++) {
                if (Element.Contents[Index].GetType() == typeof(Element)) {
                    SetReferenceProperty(Reference, (Element)Element.Contents[Index]);
                    }
                }
            }

        void MakeReference(Element Element) {
            Reference Reference = new Reference();
            Reference.ID = Element.Anchor;

            for (int Index = 0; Index < Element.Contents.Count; Index++) {
                if (Element.Contents[Index].GetType() == typeof(Element)) {
                    Element L1 = (Element)Element.Contents[Index];

                    switch (L1.Tag.ToLower()) {
                        case "front": {
                                MakeFront(Reference, L1);
                                break;
                                }
                        case "seriesinfo": {
                                SeriesInfo SeriesInfo = new SeriesInfo();
                                SeriesInfo.Name = L1.Name;
                                SeriesInfo.Value = L1.Value;
                                Reference.SeriesInfos.Add(SeriesInfo);
                                break;
                                }
                        case "format": {
                                Format Format = new Format();
                                Format.Type = L1.Type;
                                Format.Octets = L1.Octets;
                                Format.Target = L1.Target;
                                Reference.Formats.Add(Format);
                                break;
                                }
                        }
                    }
                }
            Document.Catalog.AddReference (Reference);
            }


        void MakeList(List<XML> Contents, ListItem Type, int Level) {

            for (int Index = 0; Index < Contents.Count; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)Contents[Index];
                    string FilteredText = Element.FilteredText;
                    LI LI;

                    switch (Element.Tag.ToLower()) {
                        case "li": {
                                LI = new LI(FilteredText, Element.Id, Type, Level);
                                CurrentSection.TextBlocks.Add(LI);
                                break;
                                }
                        case "dt": {
                                LI = new LI(FilteredText, Element.Id, ListItem.Term, Level);
                                CurrentSection.TextBlocks.Add(LI);
                                break;
                                }
                        case "dd": {
                                LI = new LI(FilteredText, Element.Id, ListItem.Data, Level);
                                CurrentSection.TextBlocks.Add(LI);
                                break;
                                }

                        // Recursive lists 
                        case "ol": {
                                MakeList(Element.Contents, ListItem.Ordered, Level + 1);
                                break;
                                }
                        case "ul": {
                                MakeList(Element.Contents, ListItem.Symbol, Level + 1);
                                break;
                                }
                        case "dl": {
                                MakeList(Element.Contents, ListItem.Definitions, Level + 1);
                                break;
                                }

                        }
                    }
                }
            }

        Table MakeTable(List<XML> Contents) {
            Table Table = new Table();

            for (int Index = 0; Index < Contents.Count; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)Contents[Index];

                    switch (Element.Tag.ToLower()) {
                        case "tr": {
                                Table.Rows.Add(MakeTableRow(Table, Element.Contents));
                                break;
                                }
                        }
                    }
                }
            return Table;
            }

        TableRow MakeTableRow(Table Table, List<XML> Contents) {
            TableRow TableRow = new TableRow();

            for (int Index = 0; Index < Contents.Count; Index++) {
                if (Contents[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)Contents[Index];
                    string FilteredText = Document.Catalog.GetCitation (Element.FilteredText);

                    switch (Element.Tag.ToLower()) {
                        case "th": {
                                TableData TableData = new TableData();
                                TableData.IsHeading = true;
                                TableData.Text = FilteredText;
                                TableData.ID = Element.Id;
                                TableRow.Data.Add(TableData);
                                break;
                                }
                        case "td": {
                                TableData TableData = new TableData();
                                TableData.IsHeading = false;
                                TableData.Text = FilteredText;
                                TableData.ID = Element.Id;
                                TableRow.Data.Add(TableData);
                                break;
                                }
                        }
                    }
                }
            Table.MaxRow = TableRow.Data.Count > Table.MaxRow ?
                TableRow.Data.Count : Table.MaxRow;

            return TableRow;
            }


        void MakeXmlDocument(Element Root) {
            throw new Exception("Not Yet Implemented");
            }


        Element GetNextElement(List<XML> XMLs, string Tag, ref int Index) {
            for (; Index < XMLs.Count; ) {
                if (XMLs[Index].GetType() == typeof(Element)) {
                    Element Element = (Element)XMLs[Index];
                    if (Element.Tag.ToLower() == Tag) {
                        return Element;
                        }
                    }
                Index++; // Only increment pointer after the check
                }
            return null;

            }

        void Dump(Element Element, int Level) {


            Console.Write("".PadLeft(Level * 2) + Element.Tag);
            if (Element.Class != null) Console.Write(" Class={0}", Element.Class);
            if (Element.Id != null) Console.Write(" Class={0}", Element.Id);
            if (Element.Href != null) Console.Write(" Class={0}", Element.Href);
            Console.WriteLine();

            foreach (XML XML in Element.Contents) {
                if (XML.GetType() == typeof(Element)) {
                    Dump((Element)XML, Level + 1);
                    }
                if (XML.GetType() == typeof(Content)) {
                    Content Content = (Content)XML;
                    Console.WriteLine("[[" + Content.Text + "]]");
                    }
                }
            }


        class StackItem {
            public Element Element;
            }

        class Stack {
            public List<StackItem> Data = new List<StackItem>();
            int Pointer = 0;

            public StackItem Top { get { return Data[Pointer - 1]; } }
            public StackItem FirstIn { get { return Data[0]; } }

            public Element TopElement {
                get { return Top.Element; }
                set { Top.Element = value; }
                }


            public StackItem Push() {
                StackItem StackItem = new StackItem();
                if (Pointer < Data.Count) {
                    Data[Pointer] = StackItem;
                    }
                else {
                    Data.Add(StackItem);
                    }

                Pointer++;

                return StackItem;
                }

            public void Pop() {
                Pointer--;
                }

            public void Dump() {

                for (int i = 0; i < Pointer; i++) {
                    Console.Write("<{0}>", Data[i].Element.Tag);
                    }
                Console.WriteLine();
                }

            }

        public void ReadStream(XmlReader XmlReader) {
            Stack Stack = new Stack();


            Stack.Push();
            Stack.Top.Element = Root;

            XmlReader.Read();
            while (!XmlReader.EOF) {
                bool ReadNext = true;

                //Stack.Dump();


                switch (XmlReader.NodeType) {
                    case XmlNodeType.Element:
                        //Console.WriteLine("<{0}>", XmlReader.Name);
                        Element Parent = Stack.Top.Element;

                        Stack.Push();
                        Stack.Top.Element = new Element();
                        Stack.Top.Element.Tag = XmlReader.Name;
                        Parent.Contents.Add(Stack.Top.Element);
                        bool IsEmpty = XmlReader.IsEmptyElement;
                        //XmlWriter.WriteStartElement(XmlReader.Name);
                        if (XmlReader.HasAttributes) {
                            while (XmlReader.MoveToNextAttribute()) {
                                SetElementAttribute(Stack.Top.Element, XmlReader.Name, XmlReader.Value);
                                //Console.Write(" {0}=\"{1}\"", XmlReader.Name, XmlReader.Value);
                                //XmlWriter.WriteAttributeString(XmlReader.Name, XmlReader.Value);
                                }
                            }
                        if (IsEmpty) {
                            Stack.Pop();
                            //Console.WriteLine("</{0}>", XmlReader.Name);
                            }

                        break;

                    case XmlNodeType.EndElement:
                        Stack.Pop();
                        //Console.WriteLine("</{0}>", XmlReader.Name);
                        break;

                    case XmlNodeType.Text:
                        Stack.Top.Element.Contents.Add(
                            new Content(XmlReader.Value, ContentType.Text));
                        break;

                    case XmlNodeType.Whitespace:
                        Stack.Top.Element.Contents.Add(
                            new Content(XmlReader.Value, ContentType.Whitespace));
                        break;

                    case XmlNodeType.CDATA:
                        Stack.Top.Element.Contents.Add(
                            new Content(XmlReader.Value, ContentType.Cdata));
                        break;

                    case XmlNodeType.EntityReference:
                        Stack.Top.Element.Contents.Add(
                            new Content(XmlReader.Value, ContentType.Entity));
                        break;

                    case XmlNodeType.ProcessingInstruction:
                        //Console.Write("<?{0} {1}?>", XmlReader.Name, XmlReader.Value);
                        Processing Processing = new Processing(XmlReader.Name, XmlReader.Value);

                        if (Processing.Tag == "include") {
                            //FileReader.Include(Processing.File);
                            }
                        if (XmlReader.Name.ToLower() == "bibliography") {
                            NewParse BibParse = new NewParse (Processing, Document);
                            if (Processing.Cache != null) {
                                Document.Catalog.Caches.Add (Processing.File);
                                }
                            }
                        break;

                    case XmlNodeType.Comment: break;
                    case XmlNodeType.XmlDeclaration: break;
                    case XmlNodeType.Document: break;
                    case XmlNodeType.DocumentType: break;
                    default: break;
                    }
                if (ReadNext) {
                    XmlReader.Read();
                    }
                }
            // The first element in the stack is the root element.
            foreach (XML XML in Root.Contents) {
                if (XML.GetType() == typeof(Element)) {
                    XMLRoot = (Element)((Root.Contents.Count == 0) ? null : XML);
                    }
                }
            }

        private void SetElementAttribute(Element Element, string Tag, string Value) {
            switch (Tag.ToLower()) {
                case "id": Element.Id = Value; break;
                case "class": Element.Class = Value; break;
                case "href": Element.Href = Value; break;

                case "initials": Element.Initials = Value; break;
                case "surname": Element.Surname = Value; break;
                case "fullname": Element.Fullname = Value; break;
                case "abbrev": Element.Abbrev = Value; break;

                case "day": Element.Day = Value; break;
                case "month": Element.Month = Value; break;
                case "year": Element.Year = Value; break;

                case "name": Element.Name = Value; break;
                case "value": Element.Value = Value; break;
                case "type": Element.Type = Value; break;
                case "octets": Element.Octets = Value; break;
                case "target": Element.Target = Value; break;
                case "anchor": Element.Anchor = Value; break;
                }

            }


        private bool SetDocumentProperty(string Tag, string Value) {
            //Title, Abrrev, Docname, Version, Year, Month, Day, Ipr, Area, Workgroup, Keyword, Abstract,
            //title, abrrev, docname, version, year, month, day, ipr, area, workgroup, keyword, abstract,

            switch (Tag.ToLower()) {
                case "abrrev": Document.Abrrev = Value; break;
                case "docname": Document.Docname = Value; break;
                case "version": Document.Version = Value; break;
                case "year": Document.Year = Value; break;
                case "month": Document.Month = Value; break;
                case "day": Document.Day = Value; break;
                case "ipr": Document.Ipr = Value; break;
                case "area": Document.Area = Value; break;
                case "workgroup": Document.Workgroup = Value; break;
                case "keyword": Document.Keywords.Add(Value); break;

                case "number": Document.Number = Value; break;
                case "category": Document.Category = Value; break;
                case "updates": Document.Updates = Value; break;
                case "obsoletes": Document.Obsoletes = Value; break;
                case "seriesNo": Document.SeriesNumber = Value; break;

                case "author": {
                        Author Author = new Author();
                        Author.Name = Value;
                        Document.Authors.Add(Author);
                        break;
                        }
                default:
                    //Console.WriteLine ("Tag not found [{0}]", Tag);
                    return false;
                }
            return true;
            }

        private bool SetAuthorProperty(Author Author, string Tag, string Value) {
            switch (Tag.ToLower()) {
                    case "surname": Author.Surname = Value; break;
                    case "initials": Author.Initials = Value; break;
                case "organization": Author.Organization = Value; break;
                case "street": Author.Street = Value; break;
                case "city": Author.City = Value; break;
                case "code": Author.Code = Value; break;
                case "country": Author.Country = Value; break;
                case "phone": Author.Phone = Value; break;
                case "email": Author.Email = Value; break;
                case "uri": Author.URI = Value; break;
                default:
                    return false;
                }
            return true;
            }


        private void SetAuthorProperty(Author Author, List<XML> Elements) {
            foreach (XML XML in Elements) {
                if (XML.GetType() == typeof(Element)) {
                    Element Element = (Element)XML;
                    string Value = Element.FilteredText;
                    switch (Element.Tag.ToLower()) {
                        case "organization": Author.Organization = Value; break;
                        case "postal": SetAuthorProperty(Author, Element.Contents); break;
                        case "street": Author.Street = Value; break;
                        case "city": Author.City = Value; break;
                        case "code": Author.Code = Value; break;
                        case "country": Author.Country = Value; break;
                        case "phone": Author.Phone = Value; break;
                        case "email": Author.Email = Value; break;
                        case "uri": Author.URI = Value; break;
                        }
                    }
                }
            }

        private bool SetReferenceProperty(Reference Reference, Element Element) {
            string Value = Element.FilteredText;
            string Tag = Element.Tag;
            switch (Tag.ToLower()) {
                case "title": Reference.Title = Value; break;
                case "author": {
                        Author Author = new Author();
                        Author.Name = Element.Fullname;
                        Author.Initials = Element.Initials;
                        Author.Surname = Element.Surname;
                        SetAuthorProperty(Author, Element.Contents);
                        Reference.Authors.Add(Author);
                        break;
                        }
                case "version": Reference.Version = Value; break;
                case "date":
                    Reference.Year = Element.Year;
                    Reference.Month = Element.Month;
                    Reference.Day = Element.Day; break;

                case "area": Reference.Area = Value; break;
                case "workgroup": Reference.Workgroup = Value; break;
                case "keyword": Reference.Keywords.Add(Value); break;
                default: return false;
                }
            return true;
            }
        }
    }
