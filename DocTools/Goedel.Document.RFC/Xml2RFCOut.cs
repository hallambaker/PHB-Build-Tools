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

            WriteEmptyTagNL("seriesInfo", null, "name", document.SeriesInfo.Name,
                 "value", document.SeriesInfo.Value, "stream", document.SeriesInfo.Stream);
            if (document.SeriesInfo.DOI != null) {
                WriteEmptyTag("seriesInfo", null, "name", "DOI", document.SeriesInfo.DOI);
                }
            if (document.SeriesInfo.STD != null) {
                WriteEmptyTag("seriesInfo", null, "name", "STD", document.SeriesInfo.STD);
                }

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

            // ToDo: note



            // boilerplate element is filled by the prep tool - ignore.


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
            textWriter.Write("/>");
            }

        void WriteEmptyTagNL(string Tag, params string[] Attributes) {
            WriteEmptyTag(Tag, Attributes);
            textWriter.WriteLine();
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
        // ToDo: Implement recursive lists
        // All the attributes expressed in the first item in a list are transfered.

        List<BlockType> listItems = new List<BlockType>();
        int listPointer = -1;

        void OpenList(BlockType listItem, LI lI) {
            //TextWriter.Write(Start);
            listPointer++;

            if (listItems.Count < (listPointer + 1)) {
                listItems.Add(listItem);

                }
            else {
                listItems[listPointer] = listItem;
                }
            switch (listItems[listPointer]) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: {
                    WriteStartTagNL("dl",
                        "anchor", lI.EnclosingAnchorID,
                        "hanging", lI.Format,
                        "spacing", lI.Spacing);
                    break;
                    }
                case BlockType.Ordered: {
                    WriteStartTagNL("ol",
                        "anchor", lI.EnclosingAnchorID,
                        "group", lI.Group,
                        "spacing", lI.Spacing,
                        "start", lI.Index.ToString(),
                        "type", lI.Format);
                    break;
                    }
                case BlockType.Symbol: {
                    WriteStartTagNL("ul",
                        "anchor", lI.EnclosingAnchorID,
                        "empty", lI.Empty,
                        "spacing", lI.Spacing);
                    break;
                    }
                }

            }

        void CloseList() {

            switch (listItems[listPointer]) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: {
                    WriteEndTagNL("dl");
                    break;
                    }
                case BlockType.Ordered: {
                    WriteEndTagNL("ol");
                    break;
                    } 
                case BlockType.Symbol: {
                    WriteEndTagNL("ul");
                    break;
                    }
                }

            listPointer--;
            }

        void SetListLevel(int Level, BlockType ListItem, LI LI) {
            if (Level < listPointer) {
                while (Level < listPointer) {
                    CloseList();
                    }
                }
            if (Level < 0) {
                return;
                }

            if (Level > listPointer) {
                while (Level > listPointer) {
                    OpenList(ListItem, LI);
                    }
                return;
                }


            // Level == ListPointer 
            if ((listItems[listPointer] == ListItem) |
                (listItems[listPointer] == BlockType.Term & ListItem == BlockType.Data)) {
                return;
                }
            CloseList();
            OpenList(ListItem, LI);
            }

        void WriteBlock(LI lI) {

            SetListLevel(lI.Level-1, lI.Type, lI);

            switch (lI.Type) {
                case BlockType.Data: {
                    Write("dd", lI.Segments, "anchor", lI.AnchorID);
                    break;
                    }
                case BlockType.Term: {
                    Write("dt", lI.Segments, "anchor", lI.AnchorID);
                    break;
                    }
                case BlockType.Ordered:
                case BlockType.Symbol: {
                    Write("li", lI.Segments, "anchor", lI.AnchorID);
                    break;
                    }
                }
            }

        void ListLast() {
            SetListLevel(-1, BlockType.Data, null);
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


        public void Write (
                    string tag, 
                    List<GM.TextSegment> segments, 
                    params string[] attributes) {
            WriteStartTag(tag, attributes);

            if (segments != null) {
                foreach (var Segment in segments) {
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



        void WriteIrefs(List<string> irefs) {
            if (irefs == null || irefs.Count == 0) {
                return;
                }

            foreach (var iref in irefs) {
                WriteEmptyTagNL("iref", null, "item", iref);
                }

            }
        void WriteRow(List<TableRow> rows, bool head, string tag) {
            if (rows == null || rows.Count == 0) {
                return;
                }

            var elementTag = head ? "th" : "td";
            foreach (var row in rows) {
                WriteStartTagNL("tr");
                foreach (var td in row.Data) {
                    WriteStartTagNL(elementTag,
                        "anchor", td.AnchorID,
                        "colspan", td.ColSpan.ToString(),
                        "rowspan", td.RowSpan.ToString());

                    WriteTextBlocks(td.Blocks);
                    WriteEndTagNL(elementTag);
                    }
                WriteEndTagNL("tr");
                }
            
            }


        void WriteBlock(Table block) {
            WriteStartTag("table", "anchor", block.AnchorID);
            WriteIrefs(block.Irefs);
            WriteRow(block.Head, true, "thead");
            foreach (var body in block.Body) {
                WriteRow(body, false, "tbody");
                }
            WriteRow(block.Foot, false, "tfoot");
            WriteEndTag("table");
            }


        void WriteBlock(PRE block) {
            if (block.GeneratedID != null && block.AnchorID != "") {
                WriteStartTag("figure", "anchor", block.AnchorID, "suppress-title", "true");
                }
            else {
                WriteStartTagNL("figure");
                }
            WriteStartTagNL("artwork");

            textWriter.Write("<![CDATA[");
            WritePRE(block.Segments);
            //TextWriter.Write(PRE.Text);
            textWriter.Write("]]>");

            WriteEndTagNL("artwork");
            WriteEndTagNL("figure");
            }
        void WriteBlock(Figure block) {
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
            textWriter.Write(block.Caption);
            WriteEndTagNL("postamble");
            WriteEndTagNL("figure");
            
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
                        WriteBlock(LI);
                        break;
                        }
                    case PRE PRE: {
                        ListLast();
                        WriteBlock(PRE);
                        break;
                        }
                    case P P: {
                        ListLast();
                        Write("t", P.Segments);
                        break;
                        }
                    case Table Table: {
                        ListLast();
                        WriteBlock(Table);

                        break;
                        }
                    case Figure Figure: {
                        ListLast();
                        WriteBlock(Figure);
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
