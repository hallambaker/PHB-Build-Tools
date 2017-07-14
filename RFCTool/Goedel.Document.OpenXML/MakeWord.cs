using System;
using System.Collections.Generic;
// To install DocumentFormat.OpenXml, run the following command in the Package Manager Console
// PM> Install-Package DocumentFormat.OpenXml

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HT = Goedel.Tool.RFCTool;

namespace Goedel.Document.OpenXML {
    public partial class MakeWord {
        WordprocessingDocument Target;
        MainDocumentPart TargetMain;
        DocumentFormat.OpenXml.Wordprocessing.Document TargetDocument;
        Body TargetBody;
        StyleDefinitionsPart TargetStyles;

        const string StyleID_title = "title";
        const string StyleID_h1 = "h1";
        const string StyleID_h2 = "h2";
        const string StyleID_h3 = "h3";
        const string StyleID_h4 = "h4";
        const string StyleID_h5 = "h5";
        const string StyleID_h6 = "h6";

        const string StyleID_pre = "pre";
        const string StyleID_meta = "meta";
        const string StyleID_abbrev = "abbrev";
        const string StyleID_dt = "dt";
        const string StyleID_dd = "dd";
        const string StyleID_li = "li";
        const string StyleID_ni = "ni";

        Goedel.Tool.RFCTool.Document Source;

        private void DumpNumber (StyleParagraphProperties st1) {
            if (st1 == null) {
                return;
                }
            var np = st1.NumberingProperties;
            if (np == null) {
                return;
                }
            var nid = np.NumberingId;
            var nins = np.Inserted;
            if (nid == null) {
                return;
                }
            }



        private MakeWord (string Filename, Goedel.Tool.RFCTool.Document Source) {
            this.Source = Source;

            //ReadTest();


            using (var wordDocument =
                WordprocessingDocument.Create(Filename, WordprocessingDocumentType.Document)) {
                this.Target = wordDocument;
                Generate();
                }
            }

        public static MakeWord FromHTML2RFC (string Filename, Goedel.Tool.RFCTool.Document Source) {
            return new MakeWord(Filename, Source);
            }


        private void Generate () {
            TargetMain = Target.AddMainDocumentPart();
            TargetDocument = new DocumentFormat.OpenXml.Wordprocessing.Document();
            TargetMain.Document = TargetDocument;
            TargetBody = TargetDocument.AppendChild(new Body());

            AddStyles();
            AddText();
            }

        private void AddText () {
            WriteParagraph("title", Source.Title);
            WriteParagraph("abbrev", Source.TitleAbrrev);

            WriteMeta("ietf", Source.Docname);
            WriteMeta("version", Source.Version);
            WriteMeta("ipr", Source.Ipr);
            WriteMeta("area", Source.Area);
            WriteMeta("workgroup", Source.Workgroup);
            //WriteMeta("publisher", Source.Publisher);
            //WriteMeta("status", Source.Status);

            WriteMeta("number", Source.Number);
            WriteMeta("category", Source.Category);
            WriteMeta("updates", Source.Updates);
            WriteMeta("obsoletes", Source.Obsoletes);
            WriteMeta("seriesnumber", Source.SeriesNumber);

            WriteAuthors(Source.Authors);

            foreach (var Keyword in Source.Keywords) {
                WriteMeta("keyword", Keyword);
                }

            if (Source.Abstract.Count > 0) {
                WriteHeading("Abstract", 1);
                foreach (HT.TextBlock TextBlock in Source.Abstract) {
                    if (TextBlock.GetType() == typeof(HT.P)) {
                        var P = (HT.P)TextBlock;
                        WriteParagraph(P.Text);
                        }
                    }
                }
            WriteSections(Source.Middle, 1);
            WriteSections(Source.Back, 1);
            }

        public void WriteSections (List<HT.Section> Sections, int Level) {
            foreach (var Section in Sections) {
                if (!Section.Automatic) {
                    if (Section.Heading != null) {
                        WriteHeading(Section.Heading, Level);
                        }

                    foreach (var TextBlock in Section.TextBlocks) {
                        if (TextBlock.GetType() == typeof(HT.LI)) {
                            var LI = (HT.LI)TextBlock;
                            if (LI.Type == HT.BlockType.Symbol) {
                                WriteParagraph(StyleID_li, LI.Text);
                                }
                            else if (LI.Type == HT.BlockType.Ordered) {
                                WriteParagraph(StyleID_ni, LI.Text);
                                }
                            else if (LI.Type == HT.BlockType.Term) {
                                WriteParagraph(StyleID_dt, LI.Text);
                                }
                            else if (LI.Type == HT.BlockType.Data) {
                                WriteParagraph(StyleID_dd, LI.Text);
                                }
                            }

                        if (TextBlock.GetType() == typeof(HT.P)) {
                            var P = (HT.P)TextBlock;
                            WriteParagraph(P.Text);
                            }

                        if (TextBlock.GetType() == typeof(HT.PRE)) {
                            var PRE = (HT.PRE)TextBlock;
                            WriteParagraph(StyleID_pre, PRE.Text);
                            }

                        if (TextBlock.GetType() == typeof(HT.Table)) {
                            WriteTable(TextBlock as HT.Table);
                            }
                        }
                    WriteSections(Section.Subsections, Level + 1);
                    }
                }
            }

        void WriteTable (HT.Table TableData) {
            var Table = new Table();
            foreach (var Row in TableData.Rows) {
                var tr = new TableRow();
                foreach (var Cell in Row.Data) {
                    var tc = new TableCell();
                    tc.Append(new Paragraph(
                        new Run(
                            new Text(Cell.Text))));
                    tc.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                    tr.Append(tc);
                    }
                Table.Append(tr);
                }
            TargetBody.Append(Table);
            }


        void WriteAuthors (List<HT.Author> Authors) {
            foreach (var Author in Authors) {
                WriteMeta("author", Author.Name);
                WriteMeta("initials", Author.Initials, 1);
                WriteMeta("organization", Author.Organization, 1);
                WriteMeta("surname", Author.Surname, 1);
                WriteMeta("phone", Author.Phone, 1);
                WriteMeta("email", Author.Email, 1);
                WriteMeta("uri", Author.URI, 1);
                WriteMeta("street", Author.Street, 1);
                WriteMeta("city", Author.City, 1);
                WriteMeta("code", Author.Code, 1);
                WriteMeta("country", Author.Country, 1);
                }
            }

        string[] Headings = {
            StyleID_title,
            StyleID_h1,
            StyleID_h2,
            StyleID_h3,
            StyleID_h4,
            StyleID_h5,
            StyleID_h6,
            };

        private void WriteHeading (string Text, int Level) {
            Level = Level > 6 ? 6 : Level;
            var P = StartParagraph(Headings[Level]);
            var Run = P.AppendChild(new Run());
            Run.AppendChild(new Text(Text));
            }


        private void WriteParagraph (string Style, string Text) {
            var P = StartParagraph(Style);
            var Run = P.AppendChild(new Run());
            Run.AppendChild(new Text(Text));
            }

        private void WriteParagraph (string Text) {
            var P = StartParagraph("Normal");
            var Run = P.AppendChild(new Run());
            Run.AppendChild(new Text(Text));
            }

        private void WriteMeta (string Tag, List<string> Texts) {
            if (Texts == null) {
                return;
                }
            foreach (var Text in Texts) {
                WriteMeta(Tag, Text, 0);
                }
            }
        private void WriteMeta (string Tag, string Text) {
            WriteMeta(Tag, Text, 0);
            }

        private void WriteMeta (string Tag, string Text, int Indent) {
            if (Text == null) { return; }

            var P = StartParagraph(StyleID_meta);
            var Run = P.AppendChild(new Run());
            Run.AppendChild(new Text("<" + Tag + ">" + Text));
            }

        private Paragraph StartParagraph (string Style) {

            var P = TargetBody.AppendChild(new Paragraph());
            var PPs = MakeParagraphProperties(P);
            PPs.ParagraphStyleId = new ParagraphStyleId() {
                Val = Style
                };

            return P;
            }

        private ParagraphProperties MakeParagraphProperties (Paragraph p) {
            var PL = p.Elements<ParagraphProperties>().GetEnumerator();

            if (PL.Current != null) {
                return PL.Current;
                }

            var PP = new ParagraphProperties();
            p.PrependChild<ParagraphProperties>(PP);
            return PP;
            }


        private void AddStyles () {

            var NumberingDefinitionsPart =
                TargetDocument.MainDocumentPart.AddNewPart<NumberingDefinitionsPart>();
            GeneratePartContent(NumberingDefinitionsPart);

            AddStylesPartToPackage();

            AddHeaderStyle(StyleID_title, "Title", 56, false, false, 0);
            AddHeaderStyle(StyleID_h1, "Heading 1", 32, false, false, 1);
            AddHeaderStyle(StyleID_h2, "Heading 2", 26, false, false, 2);
            AddHeaderStyle(StyleID_h3, "Heading 3", 24, false, false, 3);
            AddHeaderStyle(StyleID_h4, "Heading 4", 22, false, false, 4);
            AddHeaderStyle(StyleID_h5, "Heading 5", 22, true, false, 5);
            AddHeaderStyle(StyleID_h6, "Heading 6", 20, true, false, 6);

            AddNewStyle(StyleID_pre, "Preformatted", true, 22, 0);
            AddNewStyle(StyleID_abbrev, "Abbreviation", false, 24, 0);
            AddNewStyle(StyleID_dt, "Defined Term", false, 24, 1);
            AddNewStyle(StyleID_dd, "Definition", false, 24, 2);
            //AddNewStyle(StyleID_li, "Bullet", false, 22, 2);
            AddNewStyle(StyleID_ni, "Numbered", false, 22, 2);

            AddNewStyle(StyleID_meta, "Meta", "Courier New", 22, null,
                    false, false, 0, 0, true);


            var Style = MakeStyle(StyleID_li);
            Style.StyleParagraphProperties.NumberingProperties =
                    new NumberingProperties() {
                        NumberingId = new NumberingId() {
                            Val = 1
                            }
                        };

            Style = MakeStyle(StyleID_ni);
            Style.StyleParagraphProperties.NumberingProperties =
                    new NumberingProperties() {
                        NumberingId = new NumberingId() {
                            Val = 3
                            }
                        };

            Style = MakeStyle(StyleID_dt);
            Style.StyleRunProperties.Bold = new Bold();
            Style.StyleParagraphProperties.Indentation = new Indentation() {
                Left = "360"
                };

            Style = MakeStyle(StyleID_dd);
            Style.StyleParagraphProperties.Indentation = new Indentation() {
                Left = "720"
                };

            }


        }
    }
