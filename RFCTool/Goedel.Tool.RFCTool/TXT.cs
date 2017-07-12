using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Goedel.Tool.RFCTool {
    public partial class Writers {

        /// <summary>
        /// Write document to specified output stream
        /// </summary>
        /// <param name="OutputFile">The output file to write to</param>
        /// <param name="Document">The document to write</param>
        public static void WriteTXT(string OutputFile, Document Document) {
            // Format document to place line numbers
            WriteTXT WriteTXT = new WriteTXT();
            WriteTXT.Write(Document);

            // Redo from the beginning with line numbers
            WriteTXT = new WriteTXT(OutputFile);
            WriteTXT.Write(Document);
            }

        /// <summary>
        /// Write document to specified output stream
        /// </summary>
        /// <param name="TextWriter">The output stream to write to</param>
        /// <param name="Document">The document to write</param>
        public static void WriteTXT(TextWriter TextWriter, Document Document)
            {
            // Format document to place line numbers
            WriteTXT WriteTXT = new WriteTXT();
            WriteTXT.Write(Document);

            // Redo from the beginning with line numbers
            WriteTXT = new WriteTXT(TextWriter);
            WriteTXT.Write(Document);
            }


        }


    public class WriteTXT {

        TextWriter TextWriter;
        PageWriter PageWriter;

        public WriteTXT() {
            TextWriter =StreamWriter.Null;  // Was Console.Out
            PageWriter = new PageWriter(TextWriter) {
                AddFormFeed = false
                };
            }

        public WriteTXT(string OutputFile) {
            TextWriter = new StreamWriter(OutputFile, false, Encoding.ASCII);
            PageWriter = new PageWriter(TextWriter);
            }

        public WriteTXT(TextWriter TextWriter) {
            PageWriter = new PageWriter(TextWriter);
            }

        public void WriteHeading(Section Section, string Heading) {
            // Going to have to feed through the CurrentSection to the Pagewriter to 
            // fill in the Page number
            //TextWriter.WriteLine (Heading);

            PageWriter.Write(Heading, 0, 3, PageWriter.Format.Heading, 1, 1);

            }

        public void WriteParagraph(string Text) {
            //TextWriter.WriteLine (Text);
            PageWriter.Write(Text, 3, 3, PageWriter.Format.FlushLeft, 1, 1);
            }

        public void WritePre(PRE PRE) {
            //PageWriter.Write("[[Insert Preformatted]]", 3, 3, PageWriter.Format.FlushLeft, 1, 1);

            PageWriter.WritePreformated (PRE.Text, 3, 3, 1, 1);
            }


        public void WriteRow(Table Table, TableRow Row, int [] Widths, string Rule1, string Rule2) {

            string[] Cols= new string [Table.MaxRow];

            for (int i = 0; i < Table.MaxRow; i++) {
                Cols[i] = Row.Data[i].Text;
                }

            int Lines = 0;
            bool HaveRowData = true;
            while (HaveRowData) {
                HaveRowData = false;
                for (int i = 0; i < Table.MaxRow; i++) {
                    string Chunk = PageWriter.Consume(ref Cols[i], Widths[i]);
                    HaveRowData = HaveRowData | (Chunk.Length > 0); }
                if (HaveRowData) {
                    Lines++;
                    }
                }

            if ((PageWriter.Line + Lines + 2)> PageWriter.LastLine) {
                PageWriter.WritePage ();
                PageWriter.WriteLine();
                PageWriter.WriteLine(Rule1);
                WriteRow (Table, Table.Rows[0], Widths, Rule1, Rule2);
                PageWriter.WriteLine(Rule1);
                }

            for (int i = 0; i < Table.MaxRow; i++) {
                Cols[i] = Row.Data[i].Text;
                }

            HaveRowData = true;
            while (HaveRowData) {
                HaveRowData = false;
                string Line = "   |";
                for (int i = 0; i < Table.MaxRow; i++) {
                    string Chunk = PageWriter.Consume(ref Cols[i], Widths[i]);
                    HaveRowData = HaveRowData | (Chunk.Length > 0);
                    Line = Line + " " + Chunk + "".PadLeft(Widths[i] - Chunk.Length) + " |";
                    }
                if (HaveRowData) PageWriter.WriteLine(Line);
                }


            }

        public void WriteTable(Table Table) {
            //PageWriter.Write("[[Insert Table]]", 3, 3, PageWriter.Format.FlushLeft, 1, 1);

            PageWriter.AddBlank(1);
            PageWriter.DoBlank();

            int RowSpace = PageWriter.MaxCol - 4;

            int PerRow = RowSpace / Table.MaxRow;


            int[] Widths = new int[Table.MaxRow];
            for (int i = 0; i < Table.MaxRow; i++) {
                Widths[i] = 0;
                }

            int VariableRows = 0;
            foreach (TableRow Row in Table.Rows) {
                for (int i = 0; i < Table.MaxRow; i++) {
                    if (Widths[i] < 0) {
                        }
                    else if (Row.Data[i].Text.Length > PerRow) {
                        Widths[i] = -1;
                        VariableRows++;
                        }
                    else {
                        Widths[i] = Widths[i] > Row.Data[i].Text.Length ?
                            Widths[i] : Row.Data[i].Text.Length;
                        }
                    }
                }

            for (int i = 0; i < Table.MaxRow; i++) {
                if (Widths[i] >= 0) {
                    RowSpace -= Widths[i] + 3;
                    }
                }

            if (VariableRows > 0) {
                PerRow = RowSpace / VariableRows;
                int Extra = RowSpace - (PerRow * VariableRows);
                for (int i = 0; i < Table.MaxRow; i++) {
                    if (Widths[i] < 0) {
                        Widths[i] = PerRow + (Extra > 0 ? 1 : 0) - 3;
                        Extra--;
                        }
                    }
                }
            string Rule1 = "   +", Rule2 = "   |";
            for (int i = 0; i < Table.MaxRow; i++) {
                Rule1 += "".PadLeft (Widths[i] + 2, '-') + "+";
                Rule2 += "".PadLeft (Widths[i] + 2, ' ') + "|";
                }

            //string Rule1 = "   +" + "".PadLeft(PageWriter.MaxCol - 5, '-') + "+";
            //string Rule2 = "   |" + "".PadLeft(PageWriter.MaxCol - 5, '-') + "|";

            //string[] Cols = new String[Table.MaxRow];

            PageWriter.WriteLine(Rule1);
            WriteRow(Table, Table.Rows[0], Widths, Rule1, Rule2);
            PageWriter.WriteLine(Rule1);

            for (int i = 1; i < Table.Rows.Count; i++) {              
                WriteRow(Table, Table.Rows[i], Widths, Rule1, Rule2);
                PageWriter.WriteLine(i+1 == Table.Rows.Count ? Rule1 : Rule2);
                PageWriter.AddBreak(0);
                }



            PageWriter.AddBlank(1);
            }

        public void WriteParagraphs(List<TextBlock> Texts) {
            foreach (TextBlock TextBlock in Texts) {
                if (TextBlock.GetType() == typeof(P)) {
                    P P = (P)TextBlock;
                    //TextWriter.WriteLine(P.Text);
                    //PageWriter.WriteLeft (P.Text, 4, 4, 71);

                    PageWriter.Write(P.Text, 3, 3, PageWriter.Format.FlushLeft, 1, 1);
                    }

                if (TextBlock.GetType() == typeof(PRE)) {
                    PRE PRE = (PRE)TextBlock;
                    WritePre(PRE);
                    }

                if (TextBlock.GetType() == typeof(Table)) {
                    Table Table = (Table)TextBlock;
                    //TextWriter.WriteLine(P.Text);
                    //PageWriter.WriteLeft (P.Text, 4, 4, 71);
                    WriteTable(Table);
                    }

                if (TextBlock.GetType() == typeof(LI)) {
                    LI LI = (LI)TextBlock;
                    int First = 0, Left = 0, Above = 0, Below = 0; string Text = LI.Text; bool Break = true;
                    if (LI.Type == BlockType.Symbol) {
                        PageWriter.AllowBreak = true;
                        First = 6 + (LI.Level * 3);
                        Left = First + 3;
                        Text = "*  " + Text;
                        Above = 1; Below = 1;
                        }
                    if (LI.Type == BlockType.Ordered) {
                        PageWriter.AllowBreak = true;
                        First = 6 + (LI.Level * 3);
                        Left = First + 3;
                        Text = LI.Index.ToString() + ") " + Text;
                        Above = 1; Below = 1;
                        }
                    if (LI.Type == BlockType.Term) {
                        PageWriter.AllowBreak = false;
                        First = 6 + (LI.Level * 3);
                        Left = First + 3;
                        Above = 1;
                        Break = false; // suppress page break between defined term and definition
                        }
                    if (LI.Type == BlockType.Data) {
                        PageWriter.AllowBreak = true;
                        First = 9 + (LI.Level * 3);
                        Left = First;
                        Below = 1;
                        }
                    //PageWriter.WriteLeft (Text, First, Left, 71, Above, Below);

                    PageWriter.Write(Text, First, Left, PageWriter.Format.FlushLeft, Above, Below);
                    if (Break) {
                        PageWriter.AddBreak(0) ;
                        }
                    }

                if (TextBlock.GetType() == typeof(Author)) {
                    Author Author = (Author)TextBlock;
                    PageWriter.AddBlank(1);
                    if (Author.Name != null) {
                        PageWriter.Write(Author.Name, 3, 3, PageWriter.Format.FlushLeft, 0, 0);
                        }
                    if (Author.Organization != null) {
                        PageWriter.Write(Author.Organization, 3, 3, PageWriter.Format.FlushLeft, 0, 0);
                        }
                    if (Author.Email != null) {
                        PageWriter.Write(Author.Email, 3, 3, PageWriter.Format.FlushLeft, 1, 0);
                        }
                    PageWriter.AddBreak(1);
                    }

                if (TextBlock.GetType() == typeof(Reference)) {
                    Reference Reference = (Reference) TextBlock;

                    WriteReference(Reference);

                    }
                }
            }

        string MakePadding(int length) {
            return (length <= 0) ? "" : "".PadLeft (length);
            }


        bool PushComma = false;
        private string DeNullifyString (string Text, string Pre, string Post) {
            if (Text == null) return "";
            if (Text.Length == 0) return "";
            if (PushComma) {
                PushComma = false;
                return "," + Pre + Text + Post;
                }
            return Pre + Text + Post;
            }

        private string DeNullifyString (string Text, string Pre) {
            return DeNullifyString (Text, Pre, "");
            }

        private string DeNullifyString(string Text) {
            return DeNullifyString(Text, "", "");
            }

        // [RFC2200]	Postel, J., "Internet Official Protocol Standards", 
        // RFC 2200, STD 1, June 1997.
        public void WriteReference(Reference Reference) {
            string Line = "[" + Reference.ID + "]  "
                + MakePadding (7 - Reference.ID.Length);
            foreach (Author Author in Reference.Authors) {
                Line += DeNullifyString (Author.Surname, "");
                Line += DeNullifyString (Author.Initials, ", ");
                PushComma = true;
                }

            PushComma = true;
            Line += DeNullifyString (Reference.Title, " \"", "\"");

            foreach (SeriesInfo SeriesInfo in Reference.SeriesInfos) {
                PushComma = true;
                Line += DeNullifyString (SeriesInfo.Name, " ");
                Line += DeNullifyString (SeriesInfo.Value, " ");
                }
            
            PushComma = true;
            Line += DeNullifyString (Reference.Day, " ");
            Line += DeNullifyString (Reference.Month, " ");
            Line += DeNullifyString (Reference.Year, " ");
            Line += ".";
            PageWriter.Write(Line, 3, 14, PageWriter.Format.FlushLeft, 1, 1);
            }

        public void WriteParagraphs(string[] Texts) {
            foreach (string s in Texts) {
                PageWriter.Write(s, 3, 3, PageWriter.Format.FlushLeft, 1, 1);
                }
            PageWriter.AddBreak(0) ; // always allow breaks after paragraphs
            }

        public void WriteLeft(string Text) {
            //TextWriter.WriteLine (Text);
            PageWriter.Write(DeNullifyString(Text), 0, 0, PageWriter.Format.FlushLeft, 0, 0);
            }
        public void WriteRight(string Text) {
            //TextWriter.WriteLine (Text);
            PageWriter.Write(DeNullifyString(Text), 0, 0, PageWriter.Format.FlushRight, 0, 0);
            }
        public void WriteCenter(string Text, int Above, int Below) {
            PageWriter.Write(DeNullifyString(Text), 0, 0, PageWriter.Format.Centered, Above, Below);
            }

        
        // The Mast heading has a unique format. Since this will never extend over 
        // more than half a page, we special case it by moving the line pointer 
        // on the PageWriter

        int LeftMastLines = 0;

        public void MastLeft() {
            }
        public void MastRight() {
            LeftMastLines = PageWriter.Line;
            PageWriter.Line =0;
            }

        public void MastEnd() {
            PageWriter.Line = PageWriter.Line > LeftMastLines ? 
                PageWriter.Line : LeftMastLines;
            }

        public void WriteTOC(List<Section> Sections) {
            
            foreach (Section Section in Sections) {
                int First ;
                int Left ;
                string Heading ;

                if (Section.Level > 0) {
                    First = Section.Level * 3;
                    Left = First + Section.Number.Length + 3;
                    Heading = Section.Number + "  " + Section.Heading;
                    }
                else {
                    First = 3;
                    Left = 3;
                    Heading = Section.Heading;
                    }
                PageWriter.WriteTOC(Heading, First, Left, Section.Page.ToString());
                PageWriter.AddBreak(0) ;

                //TextWriter.WriteLine(CurrentSection.Number + " " + CurrentSection.Heading +
                //    "..." + CurrentSection.Page);
                if (Section.Level < 3) {
                    WriteTOC(Section.Subsections);
                    }

                }
            }

        public void WriteSections(List<Section> Sections) {

            foreach (Section Section in Sections) {
                WriteHeading(Section, Section.Number + " " + Section.Heading);
                Section.Page = PageWriter.Page;
                Section.Line = PageWriter.Line;
                if (Section.TextBlocks.Count > 0) {
                    WriteParagraphs(Section.TextBlocks);
                    }
                WriteSections(Section.Subsections);
                PageWriter.AddBreak(0) ; // always allow break after a section
                }
            }

        public void Write(Document Document) {

            PageWriter.FooterLeft=Document.FirstAuthor;
            PageWriter.FooterCenter = "Expires " + Document.Expires;

            PageWriter.HeaderLeft = "Internet-Draft";
            PageWriter.HeaderCenter = Document.TitleAbrrev;
            
            PageWriter.HeaderRight = Document.Month + " " + Document.Year;

            MastLeft();

            WriteLeft("Internet Engineering Task Force (IETF)");
            if (Document.Number != null) {
                WriteLeft("Request for Comments" + Document.Number);
                if (Document.Updates != null) {
                WriteLeft("Updates: " + Document.Updates);
                    }
                if (Document.Obsoletes != null) {
                    WriteLeft("Obsoletes: " + Document.Obsoletes);
                    }
                WriteLeft("Category: " + Document.Category);
                }
            else {
                WriteLeft("INTERNET-DRAFT");
                WriteLeft("Intended Status: " + Document.Category);
                if (Document.Updates != null) {
                WriteLeft("Updates: " + Document.Updates + "(if approved)");
                    }
                if (Document.Obsoletes != null) {
                    WriteLeft("Obsoletes: " + Document.Obsoletes + "(if approved)");
                    }
                WriteLeft("Expires: " + Document.Expires);
                if (Document.SeriesNumber != null) {
                    WriteLeft("ISSN: " + Document.SeriesNumber);
                    }
                }

            MastRight();

            foreach (Author Author in Document.Authors) {
                WriteRight(Author.Name);
                WriteRight(Author.Organization);
                }
            // The date format is totally illogical but the Americans run the organization
            // and make hissy fits rather than follow international standards like year-month-day
            WriteRight(String.Format("{0} {1}, {2}",
                        Document.Month, Document.Day, Document.Year));

            MastEnd();

            WriteCenter(Document.Title, 2, 0);
            WriteCenter(Document.FullDocName, 0, 1);

            WriteHeading(null, "Abstract");
            WriteParagraphs(Document.Abstract);

            WriteHeading(null, "Status of This Memo");
            WriteParagraphs(Document.StatusOfThisMemo);


            WriteHeading(null, "Copyright Notice");
            WriteParagraph(Document.CopyrightNotice);
            WriteParagraphs(Document.CopyrightTerms);

            // Always start TOC on a new page
            PageWriter.WritePage();

            // Table of Contents here
            WriteHeading(null, "Table of Contents");
            PageWriter.AddBlank (1);
            WriteTOC(Document.Middle);
            WriteTOC(Document.Back);
            PageWriter.WritePage();

            // Sections here
            WriteSections(Document.Middle);
            WriteSections(Document.Back);

            // Ensure last page gets written
            PageWriter.LastPage();
            }
        }



    }
