using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM=Goedel.Document.Markdown;
using Goedel.Utilities;

namespace Goedel.Document.RFC {
    public class Xml2RFCOut {
        TextWriter textWriter;

        public Xml2RFCOut(TextWriter TextWriter) => this.textWriter = TextWriter;
        #region // Dcoument
        Document document;

        public void Write(Document document) {
            this.document = document;

            textWriter.WriteLine("<?xml version='1.0' encoding='utf-8'?>");

            WriteStartTag("rfc",
                "xmlns:xi", "http://www.w3.org/2001/XInclude",
                "category", Prep(document.Category),
                "consensus", Prep(document.Consensus),
                "docName", Prep(document.FullDocName),
                "indexInclude", Prep(document.IndexInclude),
                "ipr", Prep(document.Ipr),
                //iprExtract
                "number", Prep(document.Number),
                "obsoletes", Prep(document.Obsoletes),
                //prepTime
                "scripts", Prep(document.Scripts),
                "sortRefs", Prep(document.SortRefs),
                "submissionType", Prep(document.SubmissionType),
                "symRefs", Prep(document.Symrefs),
                "tocDepth", document.TocDepth.ToString(),
                "tocInclude", Prep(document.TocInclude),
                "updates", Prep(document.Updates),
                "version", "3",
                "xml:lang", "en"
                );


            MakeFront(document);
            MakeMiddle(document);
            MakeBack(document);



            WriteEndTagNL("rfc");
            }

        void MakeFront(Document document) {
            WriteStartTagNL("front");

            WriteValueTag("title", document.Title, "abbrev", document.TitleAbrrev);
            // series info here.
            WriteAuthors(document.Authors);
            WriteValueTag("date", null, "day", document.Day, "month", document.Month,
                "year", document.Year);

            WriteValueTag("area", document.AreaCombined);
            WriteValueTag("workgroup", document.WorkgroupCombined);

            foreach (string Keyword in document.Keywords) {
                WriteValueTag("keyword", Keyword);
                }

            if (document.Abstract.Count > 0) {
                WriteStartTagNL("abstract");
                WriteTextBlocks(document.Abstract);
                WriteEndTagNL("abstract");
                }

            // note
            // boilerplate

            WriteEndTagNL("front");
            }

        void MakeMiddle(Document document) {
            WriteStartTagNL("middle");
            WriteSections(document.Middle);
            WriteEndTagNL("middle");
            }
        void MakeBack(Document document) {
            WriteStartTagNL("back");

            if (document.Catalog.Normative.Count > 0) {
                WriteStartTag("references", "title", "Normative References");
                WriteReferences(document.Catalog.Normative);
                WriteEndTagNL("references");
                }

            if (document.Catalog.Informative.Count > 0) {
                WriteStartTag("references", "title", "Informative References");
                WriteReferences(document.Catalog.Informative);
                WriteEndTagNL("references");
                }

            WriteSections(document.Back);
            WriteEndTagNL("back");
            }

        string Prep(bool value) => value ? "true" : "false";

        string Prep(string value) => value == "" ? null : value;

        #endregion


        #region // utility functions
        bool NotNull(params string[] Strings) {
            foreach (string S in Strings) {
                if (S != null) {
                    return true;
                    }
                }
            return false;
            }


        void WriteIfValueTag(string Tag, params string[] Attributes) {
            if (Attributes.Length > 0 && Attributes[0] != null) {
                WriteValueTag(Tag, Attributes);
                }
            }
        void WriteValueTag(string Tag, List<string> Values) {
            foreach (var Value in Values) {
                WriteStartTagNL(Tag);
                textWriter.Write(Value.Trim());
                WriteEndTagNL(Tag);
                }
            }

        void WriteEmptyTag(string Tag, params string[] Attributes) {
            textWriter.Write("<{0}", Tag);
            for (int i = 1; i < (Attributes.Length - 1); i += 2) {
                if (Attributes[i + 1] != null) {
                    textWriter.Write(" {0}=\"{1}\"", Attributes[i], Attributes[i + 1].Trim().XMLAttributeEscape());
                    }
                }
            textWriter.Write(">");
            }

        void WriteValueTag(string Tag, params string[] Attributes) {
            textWriter.Write("<{0}", Tag);
            string Value = (Attributes.Length > 0) ? Attributes[0] : null;

            for (int i = 1; i < (Attributes.Length - 1); i += 2) {
                if (Attributes[i + 1] != null) {
                    textWriter.Write(" {0}=\"{1}\"", Attributes[i], Attributes[i + 1].Trim().XMLAttributeEscape());
                    }
                }
            if (Value != null && Value.Length > 0) {
                textWriter.WriteLine(">{1}</{0}>", Tag, Value.Trim().XMLEscapeStrict());
                }
            else {
                textWriter.WriteLine("/>");
                }
            }

        void WriteStartTagNL(string Tag, params string[] Attributes) {
            textWriter.Write("<{0}", Tag);

            for (int i = 0; i < (Attributes.Length - 1); i += 2) {
                if (Attributes[i + 1] != null) {
                    textWriter.Write(" {0}=\"{1}\"", Attributes[i], Attributes[i + 1].Trim().XMLAttributeEscape());
                    }
                }
            textWriter.WriteLine(">");
            }

        void WriteStartTag(string Tag, params string[] Attributes) {
            textWriter.Write("<{0}", Tag);

            for (int i = 0; i < (Attributes.Length - 1); i += 2) {
                if (Attributes[i + 1] != null) {
                    textWriter.Write(" {0}=\"{1}\"", Attributes[i], Attributes[i + 1].Trim().XMLAttributeEscape());
                    }
                }
            textWriter.Write(">");
            }

        void WriteStartTag(string Tag, List<GM.TagValue> Attributes) {
            textWriter.Write("<{0}", Tag);
            if (Attributes != null) {
                foreach (var attribute in Attributes) {
                    textWriter.Write(" {0}=\"{1}\"", attribute.Tag, attribute.Value.Trim().XMLAttributeEscape());
                    }
                }
            textWriter.Write(">");
            }


        void WriteEndTagNL(string Tag) => textWriter.WriteLine("</{0}>", Tag);
        void WriteEndTag(string Tag) => textWriter.Write("</{0}>", Tag);

        

        #endregion
        #region // List functions


        List<BlockType> ListItems = new List<BlockType>();
        int ListPointer = -1;

        void OpenList(BlockType ListItem) {
            //TextWriter.Write(Start);
            ListPointer++;

            if (ListItems.Count < (ListPointer + 1)) {
                ListItems.Add(ListItem);

                }
            else {
                ListItems[ListPointer] = ListItem;
                }
            switch (ListItems[ListPointer]) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: textWriter.WriteLine("<dl>"); return;
                case BlockType.Ordered: textWriter.WriteLine("<ol>"); return;
                case BlockType.Symbol: textWriter.WriteLine("<ul>"); return;
                }

            }

        void CloseList() {

            switch (ListItems[ListPointer]) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: textWriter.WriteLine("</dl>"); break;
                case BlockType.Ordered: textWriter.WriteLine("</ol>"); break;
                case BlockType.Symbol: textWriter.WriteLine("</ul>"); break;
                }

            ListPointer--;
            }

        void SetListLevel(int Level, BlockType ListItem) {
            if (Level < ListPointer) {
                while (Level < ListPointer) {
                    CloseList();
                    }
                }
            if (Level < 0) {
                return;
                }

            if (Level > ListPointer) {
                while (Level > ListPointer) {
                    OpenList(ListItem);
                    }
                return;
                }


            // Level == ListPointer 
            if ((ListItems[ListPointer] == ListItem) |
                (ListItems[ListPointer] == BlockType.Term & ListItem == BlockType.Data)) {
                return;
                }
            CloseList();
            OpenList(ListItem);
            }

        void ListLevel(LI LI) {

            SetListLevel(LI.Level-1, LI.Type);

            switch (LI.Type) {
                case BlockType.Data: {
                    Write("dd", LI.Segments);
                    break;
                    }
                case BlockType.Term: {
                    Write("dt", LI.Segments);
                    break;
                    }
                case BlockType.Ordered:
                case BlockType.Symbol: {
                    Write("li", LI.Segments);
                    break;
                    }
                }
            }

        void ListLast() {
            SetListLevel(-1, BlockType.Data);
            }

        #endregion
        #region // Text segment output
        public void Write (GM.TextSegmentOpen Open) {
            switch (Open.Tag) {

                case "bcp14":
                case "em":
                case "strong":
                case "tt":
                case "sub":
                case "sup":
                case "eref":
                case "relref":
                case "xref":
                case "cref": {
                    WriteStartTag(Open.Tag, Open.Attributes);
                    break;
                    }

                case "norm":
                case "info":
                case "a": {
                    // need to manage these here!

                    break;
                    }
                }
            }

        public void Write (GM.TextSegmentClose Close) {
            WriteEndTag(Close.Open.Tag);
            }

        public void Write(GM.TextSegmentEmpty Text) {
            switch (Text.Tag) {
                case "iref": {
                    WriteEmptyTag(Text.Tag);
                    break;
                    }
                }
            }


        public void Write (string tag, List<GM.TextSegment> Segments) {
            WriteStartTag(tag);

            if (Segments != null) {
                foreach (var Segment in Segments) {
                    switch (Segment) {
                        case GM.TextSegmentText Text: {
                            textWriter.Write(Text.Text.XMLEscapeStrict());
                            break;
                            }
                        case GM.TextSegmentOpen Text: {
                            Write(Text);
                            break;
                            }
                        case GM.TextSegmentClose Text: {
                            Write(Text);
                            break;
                            }
                        case GM.TextSegmentEmpty Text: {
                            Write(Text);
                            break;
                            }
                        }
                    }
                }
            WriteEndTagNL(tag);
            }
        #endregion
        #region // Write sections

        void WritePRE (List<GM.TextSegment> Segments) {
            foreach (var Segment in Segments) {
                switch (Segment) {
                    case GM.TextSegmentText TextSegmentText:
                        textWriter.Write(TextSegmentText.Text);
                        break;
                    }
                }
            }

        void WriteBlock (P block) {
            }
        void WriteBlock(PRE block) {
            }
        void WriteBlock(Figure block) {
            }
        void WriteBlock(LI block) {
            }
        void WriteBlock(Table block) {
            }

        public void WriteSections(List<Section> Sections) {
            foreach (Section Section in Sections) {
                if (!Section.Automatic) {

                    WriteStartTag("section", "title", Section.Heading, "anchor", Section.GeneratedID);
                    WriteTextBlocks(Section.TextBlocks);
                    ListLast();

                    WriteSections(Section.Subsections);
                    WriteEndTagNL("section");
                    }
                }
            }


        public void WriteTextBlocks(List<TextBlock> TextBlocks) {
            foreach (var TextBlock in TextBlocks) {
                switch (TextBlock) {
                    case LI LI: {
                        ListLevel(LI);
                        break;
                        }
                    case PRE PRE: {
                        ListLast();
                        if (PRE.GeneratedID != null && PRE.GeneratedID != "") {
                            WriteStartTag("figure", "anchor", PRE.GeneratedID, "suppress-title", "true");
                            }
                        else {
                            WriteStartTagNL("figure");
                            }
                        WriteStartTagNL("artwork");

                        textWriter.Write("<![CDATA[");
                        WritePRE(PRE.Segments);
                        //TextWriter.Write(PRE.Text);
                        textWriter.Write("]]>");

                        WriteEndTagNL("artwork");
                        WriteEndTagNL("figure");
                        break;
                        }
                    case P P: {
                        ListLast();
                        Write("t", P.Segments);
                        break;
                        }
                    case Table Table: {
                        ListLast();
                        WriteStartTag("texttable ", "anchor", Table.GeneratedID);

                        for (int Row = 0; Row < Table.Rows.Count; Row++) {
                            TableRow TableRow = Table.Rows[Row];
                            int Col = 0;
                            string Tag = Row == 0 ? "ttcol" : "c";

                            for (; Col < TableRow.Data.Count; Col++) {
                                WriteValueTag(Tag, TableRow.Data[Col].Text);
                                }
                            for (; Col < Table.MaxRow; Col++) {
                                WriteValueTag(Tag, "");
                                }
                            }

                        WriteEndTagNL("texttable ");
                        break;
                        }
                    case Figure Figure: {
                        ListLast();
                        WriteStartTagNL("figure");
                        WriteStartTagNL("preamble");
                        textWriter.Write("[[This figure is not viewable in this format.");
                        if (document.Also != null) {
                            textWriter.Write(" The figure is available at <eref target=\"");
                            textWriter.Write(document.Also);
                            textWriter.Write("\">");
                            textWriter.Write(document.Also);
                            textWriter.Write("</eref>.");
                            }
                        textWriter.Write("]]");
                        WriteEndTagNL("preamble");
                        WriteStartTagNL("artwork");
                        WriteEndTagNL("artwork");
                        WriteStartTagNL("postamble");
                        textWriter.Write(Figure.Caption);
                        WriteEndTagNL("postamble");
                        WriteEndTagNL("figure");
                        break;
                        }
                    }


                }
            }

        #endregion
        #region // References, Authors
        void WriteReferences(List<Reference> References) {
            foreach (Reference Reference in References) {
                WriteStartTag("reference", "anchor", Reference.GeneratedID);
                WriteStartTagNL("front");
                WriteIfValueTag("title", Reference.Title);
                WriteAuthors(Reference.Authors);
                WriteValueTag("date", null, "day", Reference.Day, "month", Reference.Month,
                        "year", Reference.Year);
                foreach (string Keyword in Reference.Keywords) {
                    WriteValueTag("keyword", Keyword);
                    }
                if (Reference.Abstract.Count > 0) {
                    WriteStartTagNL("abstract");
                    foreach (string S in Reference.Abstract) {
                        WriteValueTag("t", S);
                        }
                    WriteEndTagNL("abstract");
                    }
                WriteEndTagNL("front");
                foreach (SeriesInfo SeriesInfo in Reference.SeriesInfos) {
                    WriteValueTag("seriesInfo", null, "name", SeriesInfo.Name, 
                        "value", SeriesInfo.Value);
                    }
                foreach (Format Format in Reference.Formats) {
                    WriteValueTag("format", null, "type", Format.Type, 
                        "target", Format.Target, "octets", Format.Octets);
                    }
                WriteEndTagNL("reference");
                }
            }


        void WriteAuthors(List<Author> Authors) {
            foreach (Author Author in Authors) {
                WriteStartTag("author", "fullname", Author.Name, "initials",
                    Author.Initials, "surname", Author.Surname);
                WriteIfValueTag("organization", Author.Organization);
                WriteStartTagNL("address");
                if (NotNull(Author.Street, Author.City, Author.Code, Author.Country)) {
                    WriteStartTagNL("postal");
                    WriteIfValueTag("street", Author.Street);
                    WriteIfValueTag("city", Author.City);
                    WriteIfValueTag("code", Author.Code);
                    WriteIfValueTag("country", Author.Country);
                    WriteEndTagNL("postal");
                    }
                WriteIfValueTag("phone", Author.Phone);
                WriteIfValueTag("email", Author.Email);
                WriteIfValueTag("uri", Author.URI);
                WriteEndTagNL("address");
                WriteEndTagNL("author");
                }
            }
        #endregion
        }
    }
