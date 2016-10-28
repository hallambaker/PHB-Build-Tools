using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Tool.RFCTool {
    public class Xml2RFCOut {
        TextWriter TextWriter;

        public Xml2RFCOut(TextWriter TextWriter) {
            this.TextWriter = TextWriter;
            }

        string XMLEscape(string In) {
            string Result = "";

            foreach (char c in In) {
                switch (c) {
                    case '<': Result += "&lt;"; break;
                    case '>': Result += "&gt;"; break;
                    case '&': Result += "&amp;"; break;
                    case (char)160: Result += "&nbsp;"; break;
                    default: Result += c; break;
                    }
                }

            return Result;
            }

        string XMLAttributeEscape(string In) {
            string Result = "";

            foreach (char c in In) {
                switch (c) {
                    case '<': Result += "&lt;"; break;
                    case '>': Result += "&gt;"; break;
                    case '&': Result += "&amp;"; break;
                    case '\"': Result += "&quot;"; break;
                    case (char)160: Result += "&nbsp;"; break;
                    default: Result += c; break;
                    }
                }

            return Result;
            }

        bool NotNull(params string[] Strings) {
            foreach (string S in Strings) {
                if (S != null) return true;
                }
            return false;
            }


        void WriteIfValueTag(string Tag, params string[] Attributes) {
            if (Attributes.Length > 0 && Attributes[0] != null) {
                WriteValueTag(Tag, Attributes);
                }
            }

        void WriteValueTag(string Tag, params string[] Attributes) {
            TextWriter.Write("<{0}", Tag);
            string Value = (Attributes.Length > 0) ? Attributes[0] : null;

            for (int i = 1; i < (Attributes.Length - 1); i += 2) {
                if (Attributes[i + 1] != null) {
                    TextWriter.Write(" {0}=\"{1}\"", Attributes[i], XMLAttributeEscape(Attributes[i + 1]));
                    }
                }
            if (Value != null && Value.Length > 0) {
                TextWriter.WriteLine(">{1}</{0}>", Tag, XMLEscape(Value));
                }
            else {
                TextWriter.WriteLine("/>");
                }
            }

        void WriteStartTag(string Tag, params string[] Attributes) {
            TextWriter.Write("<{0}", Tag);

            for (int i = 0; i < (Attributes.Length - 1); i += 2) {
                if (Attributes[i + 1] != null) {
                    TextWriter.Write(" {0}=\"{1}\"", Attributes[i], XMLAttributeEscape(Attributes[i + 1]));
                    }
                }
            TextWriter.WriteLine(">");
            }

        void WriteEndTag(string Tag) {
            TextWriter.WriteLine("</{0}>", Tag);
            }

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
                case BlockType.Data: TextWriter.WriteLine("<t><list style=\"hanging\">"); return;
                case BlockType.Ordered: TextWriter.WriteLine("<t><list style=\"numbers\">"); return;
                case BlockType.Symbol: TextWriter.WriteLine("<t><list style=\"symbols\">"); return;
                }

            }

        void CloseList() {

            switch (ListItems[ListPointer]) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: TextWriter.WriteLine("</list></t>"); break;
                case BlockType.Ordered: TextWriter.WriteLine("</list></t>"); break;
                case BlockType.Symbol: TextWriter.WriteLine("</list></t>"); break;
                }

            ListPointer--;
            }

        void SetListLevel(int Level, BlockType ListItem) {
            if (Level < ListPointer) {
                while (Level < ListPointer) {
                    CloseList();
                    }
                }
            if (Level < 0) return;

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

        string WrapNull(string Text) {
            return Text == null ? "" : Text;
            }

        string HangText = null;
        void ListLevel(LI LI) {

            if (HangText != null & LI.Type != BlockType.Data) {
                WriteValueTag("t", " ", "hangText", HangText);
                HangText = null;
                }

            SetListLevel(LI.Level, LI.Type);

            switch (LI.Type) {
                case BlockType.Data: WriteValueTag("t", LI.Text,
                   "hangText", WrapNull(HangText)); HangText = null; break;
                case BlockType.Term: HangText = LI.Text; break;
                case BlockType.Ordered:
                case BlockType.Symbol:
                    WriteValueTag("t", LI.Text); break;
                }
            }

        void ListLast() {
            if (HangText != null) {
                WriteValueTag("t", " ", "hangText", HangText);
                HangText = null;
                }

            SetListLevel(-1, BlockType.Data);
            }

        public void WriteSections(List<Section> Sections) {
            foreach (Section Section in Sections) {
                if (!Section.Automatic) {

                    WriteStartTag("section", "title", Section.Heading, "anchor", Section.ID);
                    foreach (TextBlock TextBlock in Section.TextBlocks) {
                        if (TextBlock.GetType() == typeof(LI)) {
                            LI LI = (LI)TextBlock;
                            ListLevel(LI);
                            }
                        else {
                            ListLast();
                            }

                        if (TextBlock.GetType() == typeof(P)) {
                            P P = (P)TextBlock;
                            WriteValueTag("t", P.Text);
                            }

                        if (TextBlock.GetType() == typeof(PRE)) {
                            PRE PRE = (PRE)TextBlock;
                            if (PRE.ID != null && PRE.ID != "") {
                                WriteStartTag("figure", "anchor", PRE.ID);
                                }
                            else {
                                WriteStartTag("figure");
                                }
                            WriteStartTag("artwork");

                            TextWriter.Write("<![CDATA[");
                            TextWriter.Write(PRE.Text);
                            TextWriter.Write("]]>");

                            WriteEndTag("artwork");
                            WriteEndTag("figure");
                            }

                        if (TextBlock.GetType() == typeof(Table)) {
                            Table Table = (Table)TextBlock;
                            WriteStartTag("texttable ", "anchor", Table.ID);

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

                            WriteEndTag("texttable ");
                            }
                        }
                    ListLast();

                    WriteSections(Section.Subsections);
                    WriteEndTag("section");
                    }
                }
            }

        void WriteReferences(List<Reference> References) {
            foreach (Reference Reference in References) {
                WriteStartTag("reference", "anchor", Reference.ID);
                WriteStartTag("front");
                WriteIfValueTag("title", Reference.Title);
                WriteAuthors(Reference.Authors);
                WriteValueTag("date", null, "day", Reference.Day, "month", Reference.Month,
                        "year", Reference.Year);
                foreach (string Keyword in Reference.Keywords) {
                    WriteValueTag("keyword", Keyword);
                    }
                if (Reference.Abstract.Count > 0) {
                    WriteStartTag("abstract");
                    foreach (string S in Reference.Abstract) {
                        WriteValueTag("t", S);
                        }
                    WriteEndTag("abstract");
                    }
                WriteEndTag("front");
                foreach (SeriesInfo SeriesInfo in Reference.SeriesInfos) {
                    WriteValueTag("seriesInfo", null, "name", SeriesInfo.Name, 
                        "value", SeriesInfo.Value);
                    }
                foreach (Format Format in Reference.Formats) {
                    WriteValueTag("format", null, "type", Format.Type, 
                        "target", Format.Target, "octets", Format.Octets);
                    }
                WriteEndTag("reference");
                }
            }


        void WriteAuthors(List<Author> Authors) {
            foreach (Author Author in Authors) {
                WriteStartTag("author", "fullname", Author.Name, "initials",
                    Author.Initials, "surname", Author.Surname);
                WriteIfValueTag("organization", Author.Organization);
                WriteStartTag("address");
                if (NotNull(Author.Street, Author.City, Author.Code, Author.Country)) {
                    WriteStartTag("postal");
                    WriteIfValueTag("street", Author.Street);
                    WriteIfValueTag("city", Author.City);
                    WriteIfValueTag("code", Author.Code);
                    WriteIfValueTag("country", Author.Country);
                    WriteEndTag("postal");
                    }
                WriteIfValueTag("phone", Author.Phone);
                WriteIfValueTag("email", Author.Email);
                WriteIfValueTag("uri", Author.URI);
                WriteEndTag("address");
                WriteEndTag("author");
                }
            }
        public void Write(Document Document) {

            TextWriter.WriteLine("<?xml version='1.0'?>");
            TextWriter.WriteLine("<!DOCTYPE rfc SYSTEM 'rfc2629.dtd'>");

            string Category = "info";
            switch (Document.Category.ToLower()) {
                case "standards track":
                case "standard":
                case "std":
                    Category = "std"; break;
                case "informational":
                case "info":
                    Category = "info"; break;
                case "experimental":
                case "exp":
                    Category = "exp"; break;
                case "best current practice":
                case "bcp":
                    Category = "bcp"; break;
                case "historic":
                    Category = "historic"; break;
                }


            WriteStartTag("rfc", 
                "ipr", Document.Ipr, 
                "docName", Document.FullDocName,
                "number", Document.Number,
                "obsoletes", Document.Obsoletes,
                "category", Category,
                "seriesNo", Document.SeriesNumber
                );

            // The default set of processing instructions
            TextWriter.WriteLine(@"<?rfc toc=""yes""?>  ");
            TextWriter.WriteLine(@"<?rfc symrefs=""yes""?>  ");
            TextWriter.WriteLine(@"<?rfc sortrefs=""yes""?>  ");
            TextWriter.WriteLine(@"<?rfc compact=""yes""?>  ");
            TextWriter.WriteLine(@"<?rfc subcompact=""no""?>  ");

            WriteStartTag("front");
            WriteValueTag("title", Document.Title, "abbrev", Document.Abrrev);
            WriteAuthors(Document.Authors);

            WriteValueTag("date", null, "day", Document.Day, "month", Document.Month,
                "year", Document.Year);
            WriteValueTag("area", Document.Area);
            WriteValueTag("workgroup", Document.Workgroup);
            foreach (string Keyword in Document.Keywords) {
                WriteValueTag("keyword", Keyword);
                }

            if (Document.Abstract.Count > 0) {
                WriteStartTag("abstract");
                foreach (TextBlock TextBlock in Document.Abstract) {
                    if (TextBlock.GetType() == typeof(P)) {
                        P P = (P)TextBlock;
                        WriteValueTag("t", P.Text);
                        }
                    }
                WriteEndTag("abstract");
                }
            WriteEndTag("front");
            WriteStartTag("middle");
            WriteSections(Document.Middle);
            WriteEndTag("middle");

            WriteStartTag("back");

            if (Document.Catalog.Normative.Count > 0) {
                WriteStartTag("references", "title", "Normative References");
                WriteReferences(Document.Catalog.Normative);
                WriteEndTag("references");
                }

            if (Document.Catalog.Informative.Count > 0) {
                WriteStartTag("references", "title", "Informative References");
                WriteReferences(Document.Catalog.Informative);
                WriteEndTag("references");
                }

            WriteSections(Document.Back);
            WriteEndTag("back");

            WriteEndTag("rfc");
            }
        }
    }
