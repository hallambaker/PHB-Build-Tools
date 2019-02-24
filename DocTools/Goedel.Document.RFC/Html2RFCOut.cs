using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;
using Goedel.IO;
using Goedel.Document.RFC;
using Goedel.Document.Markdown;

namespace Goedel.Document.RFC {
    public partial class Html2RFCOut : XMLTextWriter {
        public bool Strict = false;

        TextWriter TextWriter;



        /// <summary>
        /// Constructor, this is a subclass of XMLTextWriter
        /// </summary>
        /// <param name="TextWriter"></param>
        public Html2RFCOut(TextWriter TextWriter) : base (TextWriter, false) {
            this.TextWriter = TextWriter;
            TextWriter.NewLine = "\n";
            TextWriter.WriteLine("<!DOCTYPE html>");
            }

        public string Pilcrow = "&para;"; // "¶"


        readonly string[] AttributesLinkLicense = new string[] {
            "href", "https://trustee.ietf.org/trust-legal-provisions.html", "rel", "license" }; 

        readonly string[] Stylesheets = {
            //"https://rfc-format.github.io/draft-iab-rfc-css-bis/xml2rfc.css",  // ToDo: - final stylesheet
            "rfc-local.css"
            };

        public string MainStylesheet = "xml2rfc.css";


        // ToDo: Should incorporate the stylesheet and linked SVGs into the output as one file.

        DateTime Prepared = DateTime.UtcNow;
        DateTime Rendered = DateTime.UtcNow;
        Document Document;

        public void Write (Document Document) {
            this.Document = Document;
            ListLevel = new ListLevel() { OpenListItem = OpenListItem, CloseListItem = CloseListItem };


            Start("html");
            Start("head");
            WriteElementEmpty("meta", "class", "RFC", "lang", "en");
            WriteElementEmpty("meta", "charset", "utf-8");
            WriteElementEmpty("meta", "content", "text/html", "http-equiv", "Content-Type");
            WriteElementEmpty("meta", "name", "viewport", "content", "width=device-width, initial-scale=1");
            // ToDo: Keywords

            if (Document.Keywords != null && Document.Keywords.Count > 0) {
                var KeyWordList = new StringBuilder();
                foreach (var Keyword in Document.Keywords) {
                    if (KeyWordList.Length > 0) {
                        KeyWordList.Append(",");
                        }
                    KeyWordList.Append(Keyword.Trim());
                    }
                WriteElementEmpty("meta", "name", "keywords", "content", KeyWordList.ToString());
                }

            // ToDo: Description
            if (Document.Abstract.Count > 0) {
                var DescriptionBuilder = new StringBuilder();
                foreach (var TextBlock in Document.Abstract) {
                    switch (TextBlock) {
                        case P P: {
                            DescriptionBuilder.Append(P.Text);
                            break;
                            }
                        }
                    }
                WriteElementEmpty("meta", "name", "description", "content", DescriptionBuilder.ToString());
                }

            foreach (var Author in Document.Authors) {

                WriteElementEmpty("meta", "name", "author", "content", Author.Name.Trim());
                }

            WriteElement("title", Document.Title);
            WriteElementEmpty("link", AttributesLinkLicense); // ToDo: - other licenses

            WriteStyle(MainStylesheet);

            foreach (var Stylesheet in Stylesheets) {
                WriteElementEmpty("link", "href", Stylesheet, "rel", "stylesheet", "type", "text/css");
                }
            End();

            if (!Strict) {
                if (Document.EmbedSVG == 0) {
                    XMLEmbed.FaviconBase64("favicon.png", TextWriter);
                    }
                else {
                    WriteElementEmpty("link", "href", "favicon.png", "rel", "icon");
                    }
                }


            Start("body");

            // ToDo: Does not currently support RFC ears.
            Start("table", "class", "ears");
            Start("thead");
            Start("tr");
            WriteElement("td", Document.SeriesText, "class", "left");
            WriteElement("td", Document.TitleAbrrev, "class", "center");
            WriteElement("td", Document.Month + " " + Document.Year, "class", "right");
            End();
            End();
            Start("tfoot");
            Start("tr");
            WriteElement("td", Document.FirstAuthor, "class", "left");
            WriteElement("td", "Expires " + Document.Expires, "class", "center");
            WriteElement("td", "[Page]", "class", "right");
            End();
            End();
            End();

            WriteIdentifiers(Document);
            Start("h1", "id", "title");
            Write(Document.Title.Trim());

            foreach (var SeriesInfo in Document.SeriesInfos) {
                WriteElement("br");
                WriteElement("span", SeriesInfo.FullDocName, "id", SeriesInfo.Stream+"-file", "class", "filename");
                }
            End();

            WriteAbstract(Document);
            WriteSections(Document.Boilerplate, 0, true);
            if (Document.TocInclude) {
                WriteToc(Document);
                }
            
            WriteSections(Document.Middle, 0);
            WriteReferences(Document.Catalog);

            WriteSections(Document.Back, 0);


            WriteAuthors(Document.AuthorSectionTitle, Document.Authors);
            WriteColophon();

            End();
            }


        //Tagging and bagging paragraph blocks
        void StartSection (Section Section) {
            Start("section", "id", Section.SetableID);
            if (Section.GeneratedID != null) {
                Start(HeadTag(Section.Level), "id", Section.GeneratedID);
                WriteElement("a", Section.Number, "class", "selfRef", "href", "#" + Section.GeneratedID);
                }
            else {
                Start(HeadTag(Section.Level));
                }
            WriteElement("a", Section.Heading, "class", "selfRef", "href", "#" + Section.SetableID);
            End();
            }
        //Level, Section.Number + " " + Section.Heading, Section.ID, Section.SectionID

        //Tagging and bagging paragraph blocks
        void StartSection (int Level, string Heading, string SectionId, string Numbered = null) {
            Start("section", "id", SectionId);
            if (Numbered != null) {
                Start(HeadTag(Level), "id", Numbered);
                }
            else {
                Start(HeadTag(Level));
                }
            WriteElement("a", Heading, "class", "selfRef", "href", "#" + SectionId);
            End();
            }

        void EndSection () {
            End();
            Write();
            }


        public void WriteParagraph(P P) => WriteBlock(P, "p");


        void WritePRE (List<TextSegment> Segments) {
            foreach (var Segment in Segments) {
                switch (Segment) {
                    case TextSegmentText TextSegmentText:
                        Write(TextSegmentText.Text, false, false);
                        break;
                    }
                }
            }

        public void WritePre (PRE P) {
            Start("pre", true, false, "id", P.GeneratedID);
            // ToDo: write ID
            WritePRE(P.Segments);
            //Write(P.Text.Trim(), false, false);
            WriteElement("a", false, false, Pilcrow, "class", "pilcrow", "href", "#" + P.GeneratedID);

            End(false, true);
            }

        public void WriteTable (Table Table) {
            if (Table.Rows.Count == 0) {
                return; // no rows so suppress outpout
                }

            Start ("table", "id", Table.GeneratedID);
            Start("thead");
            Start("tr");
            foreach (var Data in Table.Rows[0].Data) {
                WriteElement("td", Data.Text);
                }
            End();
            End();
            Start("tbody");
            for (var i = 1; i < Table.Rows.Count; i++) {
                Start("tr");
                foreach (var Data in Table.Rows[i].Data) {
                    WriteElement("td", Data.Text);
                    }
                End();
                }
            End();
            End();

            }
        // Replacements for the automatic sections
        public void WriteAbstract (Document Document) {
            StartSection(1, "Abstract", "abstract");

            foreach (var TextBlock in Document.Abstract) {
                switch (TextBlock) {
                    case P P: {
                        WriteParagraph(P);
                        break;
                        }
                    }
                }

            EndSection();
            }

        public void WriteToc (Document Document) {
            StartSection(1, "Table of Contents", "toc");

            Start("nav", "class", "toc");
            WriteToc(Document.Middle);
            WriteToc(Document.Back);

            if (Document.TofInclude & Document.TableOfFigures.Count > 0) {
                Start("h3", "id", "tof");
                WriteElement("a", "Table of Figures", "class", "selfRef", "href", "#" + "tof");
                End();

                Start("ul", "class", "toc");
                foreach (var Figure in Document.TableOfFigures) {
                    Start("li", "class", "toc");
                    WriteElement("a", true, false, Figure.SectionText, "href", "#" + Figure.GeneratedID);
                    Output.Write(": ");
                    WriteElement("a", false, true, Figure.Caption, "href", "#" + Figure.GeneratedID);
                    End();
                    }
                End();
                }

            if (Document.TofInclude & Document.TableOfTables.Count > 0) {
                Start("h3", "id", "tof");
                WriteElement("a", "Table of FigureTables", "class", "selfRef", "href", "#" + "tof");
                End();

                Start("ul", "class", "toc");
                foreach (var Table in Document.TableOfTables) {
                    Start("li", "class", "toc");
                    WriteElement("a", true, false, Table.SectionText, "href", "#" + Table.GeneratedID);
                    Output.Write(": ");
                    WriteElement("a", false, true, Table.Caption, "href", "#" + Table.GeneratedID);
                    End();
                    }
                End();
                }

            End();
            EndSection();
            }

        public void WriteToc (List<Section> Sections) {
            if (Sections.Count == 0) {
                return;
                }
            Start("ul", "class", "toc");
            foreach (var Section in Sections) {
                WriteToc(Section);
                }
            End();
            }

        public void WriteToc (Section Section) {
            Start("li", "class", "toc");
            if (!Section.SuppressNumbering) {
                WriteElement("a", true, false, Section.Number, "href", "#" + Section.GeneratedID);
                }
            WriteElement("a", false, true, Section.Heading, "href", "#" + Section.SetableID);
            WriteToc(Section.Subsections);
            End();
            }


        public void WriteStatus (Document Document) {
            StartSection(1, "Status of this Memo", "n-status-of-this-memo");
            EndSection();
            }

        public void WriteCopyright (Document Document) {
            StartSection(1, "Copyright Notice", "n-copyright-notice");
            EndSection();
            }

        public void WriteDate (DateTime DateTime, string Class) {
            var Numeric = DateTime.ToString("yyyy-MM-dd");
            var Text = DateTime.ToString("d MMMMM yyyy");
            WriteElement("time", Text, "class", Class, "datetime", Numeric);
            }

        public void WriteIdentifiers (Document Document) {
            EndLine();
            Start("dl", "id", "identifiers");

            WriteElement("dt", "Stream:");
            WriteElement("dd", Document.StreamText, "class", "stream");

            if (Document.WorkgroupText != null) {
                WriteElement("dt", "Workgroup:");
                WriteElement("dd", Document.WorkgroupText, "class", "workgroup");
                }

            WriteElement("dt", "Series:");
            WriteElement("dd", Document.SeriesText, "class", "series");

            WriteElement("dt", "Status:");
            WriteElement("dd", Document.StatusText, "class", "status");

            WriteElement("dt", "Published:");
            Start("dd");
            WriteDate(Document.DocDate, "published");
            End();

            if (Document.IsDraft) {
                WriteElement("dt", "Expires");
                Start("dd");
                WriteDate(Document.Expiring, "expires");
                End();
                }
            
            WriteElement("dt", Document.Authors.Count > 0 ? "Authors:" : "Author");
            foreach (var Author in Document.Authors) {
                Start("dd", "class", "authors");
                WriteSpan("author-name", Author.Name);
                if (Author.Organization != null) {
                    StartLine();
                    Output.Write("(");
                    WriteInlineElement("span", Author.Organization.Trim(), "class", "org");
                    Output.Write(")");
                    EndLine();
                    }
                End();
                }

            End();
            }

        // -----------------------

        ListLevel ListLevel;
        void OpenListItem (BlockType ListItem) {
            switch (ListItem) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: {
                    Start("dl", "id", ListLevel.ID + "-", "class", "nohang");
                    return;
                    }
                case BlockType.Ordered: Start("ol", "id", ListLevel.ID + "-"); return;
                case BlockType.Symbol: Start("ul", "id", ListLevel.ID + "-"); return;
                }
            }

        void CloseListItem (BlockType ListItem) {
            switch (ListItem) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data:
                case BlockType.Ordered:
                case BlockType.Symbol: End(); break;
                }
            }

        string WrapNull (string Text) => Text ?? "";

        void ListItem (LI LI) {
            ListLevel.SetListLevel(LI.Level-1, LI.Type, LI.GeneratedID);

            switch (LI.Type) {
                case BlockType.Data: {
                    WriteBlock(LI, "dd");
                    break;
                    }
                case BlockType.Term: {
                    WriteBlock(LI, "dt");
                    break;
                    }
                case BlockType.Ordered: {
                    WriteBlock(LI, "li");
                    break;
                    }
                case BlockType.Symbol: {
                    WriteBlock(LI, "li");
                    break;
                    }
                }
            }

        void ListLast() => ListLevel.ListLast();

        public void WriteLinkSVG (string Filename, string Element, string Attribute) {

            switch (Document.EmbedSVG) {
                case 0: {       // default base64 encode
                    XMLEmbed.EmbedBase64(Filename, Output);
                    break;
                    }
                case 1: {       // embed SVG
                    XMLEmbed.Embed(Filename, Output);
                    break;
                    }
                case 2: {       // link to external file
                    WriteElement(Element, (string)null, Attribute, Filename);
                    break;
                    }
                }




            }

        public void WriteStyle (string Filename) {

            if (!Document.EmbedStylesheet) { // just write the link out
                WriteElementEmpty("link", "href", Filename, "rel", "stylesheet", "type", "text/css");
                return;
                }

            Start("style", "type", "text/css");
            var Text = Filename.OpenReadToEnd();
            Output.Write(Text);
            End();
            }


        //----------------------

        // Write out the sections
        public void WriteSections (List<Section> Sections, int Level, bool Always = false) {
            foreach (Section Section in Sections) {
                if (Always | !Section.Automatic) {
                    StartSection(Section);
                    foreach (TextBlock TextBlock in Section.TextBlocks) {
                        switch (TextBlock) {
                            case LI LI: {
                                ListItem(LI);
                                break;
                                }
                            default: {
                                ListLast();
                                switch (TextBlock) {
                                    case PRE PRE: {
                                        WritePre(PRE);
                                        break;
                                        }
                                    case P P: {
                                        WriteParagraph(P);
                                        break;
                                        }
                                    case Table Table: {
                                        WriteTable(Table);
                                        break;
                                        }
                                    case Figure Figure: {
                                        WriteFigure(Figure);
                                        break;
                                        }
                                    }
                                break;
                                }

                            }
                        }

                    EndSection();
                    WriteSections(Section.Subsections, Level + 1);
                    }
                }
            }
         
        void WriteFigureCaption (Figure Figure) {
            if (Figure.Caption == null) {
                return;
                }

            Start("figcaption");

            WriteElement("a", Figure.SectionText, "href", Figure.GeneratedID);
            Output.Write(":");
            WriteElement("a", Figure.Caption, "href", Figure.SetableID, "id", Figure.SetableID, "class", "selfRef");
            
            End();
            }


        void WriteFigure (Figure Figure) {
            Start("figure", "id", Figure.FigureID);
            Start("div", "class", "artwork art-svg", "id", Figure.GeneratedID);

            WriteLinkSVG(Figure.Filename, "img", "src");

            WriteElement("a", Pilcrow, "class", "pilcrow", "href", "#" + Figure.GeneratedID);
            End();
            WriteFigureCaption(Figure);

            End();
            }


        void WriteReferences (Catalog Catalog) {
            if (Catalog.Informative.Count + Catalog.Normative.Count <= 0) {
                return;
                }
            StartSection(0, "References", "n-references");
            WriteReference(Catalog.Normative, "Normative References", "n-normative");
            WriteReference(Catalog.Informative, "Informative References", "n-informative");
            EndSection();
            }

        void WriteReference (List<Reference> References, string Heading, string Id) {
            if (References.Count > 0) {
                StartSection(1, Heading, Id);
                Start("dl", "class", "reference");
                foreach (var Reference in References) {
                    WriteReference(Reference);
                    }
                End();
                EndSection();
                }
            }

        void WriteReference (Reference Reference) {
            WriteElement("dt", "[" +Reference.GeneratedID.Trim() + "]" , "id", Reference.GeneratedID);
            Start("dd");
            WriteAuthorList(Reference.Authors);

            // Title, complicated as it is enclodes in double quotes.
            StartLine();
            Output.Write("\"");
            WriteInlineElement("span", Reference.Title, "class", "refTitle");
            Output.Write("\"");
            EndLine();

            // series infos here
            foreach (SeriesInfo SeriesInfo in Reference.SeriesInfos) {
                StartSpan("refSeries");
                WriteSpan("refSeriesName", SeriesInfo.Name);
                WriteSpan("refSeriesValue", SeriesInfo.Value);
                End();
                }

            // formats
            foreach (Format Format in Reference.Formats) {
                StartLine();
                Output.Write("&lt;");
                WriteInlineElement("a", Format.Target, "class", "refTarget");
                Output.Write("&gt;");
                EndLine();
                }

            // Publication date
            GetTime(Reference.Day, Reference.Month, Reference.Year, out var Numeric, out var Text);
            WriteElement("time", Text, "class", "refDate", "datetime", Numeric);

            End();
            }

        void GetTime (string Day, string Month, string Year, out string Numeric, out string Text) {
            var NumericBuilder = new StringBuilder();
            var TextBuilder = new StringBuilder();

            if (Year != null) {
                NumericBuilder.Append(Year);
                }
            if (Month != null) {
                NumericBuilder.Append("-");
                var Index = Months.IndexOf(Month.Substring(0, 3).ToLower()) + 1;
                NumericBuilder.Append(Index.ToString("D2"));
                }
            if (Day != null) {
                NumericBuilder.Append("-");
                NumericBuilder.Append(Day);
                TextBuilder.Append(Day);
                TextBuilder.Append(" ");
                }
            if (Month != null) {
                TextBuilder.Append(Month);
                TextBuilder.Append(" ");
                }
            if (Year != null) {
                TextBuilder.Append(Year);
                }

            Numeric = NumericBuilder.ToString();
            Text = TextBuilder.ToString();
            }

        static List<string> Months = new List<string>() {
                "jan", "feb", "mar",  "apr", "may", "jun",  "jul", "aug", "sep",  "oct", "nov", "dec"
                };
            


        void WriteAuthorList (List<Author> Authors) {
            var Separate = false;
            foreach (var Author in Authors) {
                if (Separate) {
                    WriteElement("span", "and");
                    }
                Separate = true;
                WriteSpan("refAuthor", Author.Name);
                }
            }

        void WriteSpan(string Class, string Text) => WriteElementIfTrim("span", Text, "class", Class);

        void StartSpan(string Class) => Start("span", "Class", Class);

        void WriteColophon () {
            // The colophon
            var PreparedString = Prepared.ToString("yyyy-MM-dd");
            var RenderedString = Rendered.ToString("yyyy-MM-dd");

            Start("div", "class", "docInfo");
            Start("span", "class", "prepared");
            Write("Prepared: ");
            WriteElement("time", PreparedString, "datetime", PreparedString);
            End();
            Start("span", "class", "rendered");
            Write("Rendered: ");
            WriteElement("time", RenderedString, "datetime", RenderedString);
            End();
            End();
            Write();

            // Link to the script
            WriteElement("script", "", "src", "xml2rfc.js", "type", "text/javascript");

            End();

            }

        static string HeadTag (int Level) {
            switch (Level) {
                case 0: return "h2";
                case 1: return "h3";
                case 2: return "h4";
                case 3: return "h5";
                }
            return "h6";
            }



        void WriteAuthors (string Heading, List<Author> Authors) {
            // ToDo: use &nbsp; as spaces - need to rewrite element handler.
            StartSection(0, Heading, "n-authors");
            WriteElement("hr", (string) null, "class", "addr");

            foreach (var Author in Authors) {
                Start("address", "class", "vcard");

                Start("div", "class", "nameRole");
                WriteElementIfTrim("span", Author.Name, "class", "fn");
                // ToDo: process non existent Editor role here.
                End();

                WriteElementIfTrim("div", Author.Organization, "class", "org");
                //ToDo: ascii/nonAscii organizations.

                if (NotNull(Author.Street, Author.City, Author.Code, Author.Country)) {
                    Start("div", "class", "adr");
                    WriteElementIfTrim("div", Author.Street, "class", "street-address");
                    if (NotNull(Author.City, Author.Code)) {
                        Start("div", "class", "city-region-code");
                        WriteElementIfTrim("span", Author.City, "class", "city");
                        //ToDo: WriteElementIf("span", Author.Region, "class", "region");
                        WriteElementIfTrim("span", Author.Code, "class", "postal-code");
                        End();
                        }
                    WriteElementIfTrim("div", Author.Country, "class", "country-name");
                    End();
                    }
                if (NotNull(Author.Phone, Author.Email, Author.URI)) {
                    Start("div");
                    WriteContact("Email:", Author.Email, "email", "mailto:");
                    WriteContact("Phone:", Author.Phone, "tel", "tel:", "VOICE");
                    WriteContact("URI:", Author.URI, "url");
                    End();
                    }
                End();
                }
            EndSection();
            }

        void WriteContact (string Tag, string Value, string Class, string Prefix = "", string Type = null) {
            if (Value == null) {
                return;
                }
            WriteElement("span", Tag);
            WriteElement("a", Value, "class", Class, "href", Prefix + Value);
            WriteElementIf("span", Type, "class", "type");
            }

        bool NotNull (params string[] Strings) {
            foreach (string S in Strings) {
                if (S != null) {
                    return true;
                    }
                }
            return false;
            }


        enum Annotations {
            Strong, Emphasis, Code, Comment, Superscript, Subscript, Norm

            }
        }

    }
