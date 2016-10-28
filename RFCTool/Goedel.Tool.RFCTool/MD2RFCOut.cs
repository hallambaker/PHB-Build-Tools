using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Tool.RFCTool {

    public partial class Writers {

        /// <summary>
        /// Write RFC Document out in Markdown Format to file.
        /// </summary>
        /// <param name="OutputFile">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteMD(string OutputFile, Document Document) {
            using (var TextWriter = new StreamWriter(OutputFile, false, Encoding.UTF8)) {
                WriteMD(TextWriter, Document);
                }
            }

        /// <summary>
        /// Write RFC Document out in Markdown Format to text writer.
        /// </summary>
        /// <param name="TextWriter">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteMD(TextWriter TextWriter, Document Document) {
            var Writer = new WriteMD(TextWriter);
            Writer.Write(Document);
            }

        }

    public class WriteMD {
        TextWriter TextWriter;
        int Width = 72;

        public WriteMD(TextWriter TextWriter) {
            this.TextWriter = TextWriter;
            }

        public void WriteLine() {
            TextWriter.WriteLine();
            }

        bool IsWhite(char c) {
            if (c == ' ') return true;
            if (c == '\t') return true;
            return false;
            }

        public void WriteParagraph(string Text) {
            WriteLine();

            string Buffer = "";
            string WBuffer = "";
            int State = 0;
            int Position = 0;

            foreach (char c in Text) {
                if (State == 0) {
                    if (IsWhite(c)) {
                        State = 1;
                        TextWriter.Write(WBuffer);
                        TextWriter.Write(Buffer);
                        Buffer = "";
                        WBuffer = " ";
                        }
                    else {
                        Buffer = Buffer + c;
                        }
                    }
                else {
                    if (IsWhite(c)) {
                        if (State == 1) {
                            WBuffer = WBuffer + " ";
                            }
                        }
                    else {
                        State = 0;
                        Buffer = "" + c;
                        }

                    }

                // See if we should wrap the line
                Position++;
                if (Position > Width) {
                    WriteLine();
                    Position = 0;
                    WBuffer = "";
                    if (IsWhite(c)) {
                        State = 2;  // discard any spaces from this point
                        }
                    }
                }

            TextWriter.Write(WBuffer);
            TextWriter.WriteLine(Buffer);
            }

        public void WriteParagraphPre(string Text) {
            WriteLine();
            TextWriter.WriteLine("~~~~");
            TextWriter.WriteLine(Text);
            TextWriter.WriteLine("~~~~");
            }

        public void WriteHeading(string Text, int Level) {
            WriteLine();
            for (var i = 0; i < Level; i++) {
                TextWriter.Write("#");
                }
            TextWriter.WriteLine(Text);
            }

        // Convenience routines for formatting tags
        public void WriteMetaStart(string Tag, string Value) {
            TextWriter.Write("<");
            TextWriter.Write(Tag);
            //if (Value != null) {
            //    TextWriter.Write("=");
            //    TextWriter.Write(Value);
            //    }
            }

        // for flag attributes where the tag is a boolean
        public void WriteMetaAttribute(string Value) {
            if (Value != null) {
                TextWriter.Write(" ");
                TextWriter.Write(Value);
                }
            }

        public void WriteMetaData(string Value) {
            TextWriter.Write(">");
            if (Value != null) {
                TextWriter.Write(Value);
                }
            }

        // For tag=value type attributes
        public void WriteMetaAttribute(string Tag, string Value) {
            TextWriter.Write(" ");
            TextWriter.Write(Tag);
            if (Value != null) {
                TextWriter.Write("#");
                TextWriter.Write(Value);
                }
            }
        public void WriteMetaEnd() {
            TextWriter.WriteLine();
            }

        public void WriteMeta(string Tag, string Value) {
            WriteMeta(Tag, Value, 0);
            }

        public void WriteMeta(string Tag, string Value, int Indent) {
            if (Value != null) {
                for (var i = 0; i < Indent; i++) {
                    TextWriter.Write("    ");
                    }
                WriteMetaStart(Tag, Value);
                WriteMetaData(Value);
                WriteMetaEnd();
                }
            }


        // Write out the document
        public void Write(Document Document) {
            TextWriter.Write("// This file was converted using RFCTool");
            WriteLine();

            WriteMeta("ietf",       Document.Docname);
            WriteMeta("title", Document.Title);
            WriteMeta("abbrev", Document.Abrrev);
            WriteMeta("version", Document.Version);
            WriteLine();

            WriteMeta("ipr", Document.Ipr);
            WriteMeta("area", Document.Area);
            WriteMeta("workgroup", Document.Workgroup);
            WriteMeta("publisher", Document.Publisher);
            WriteMeta("status", Document.Status);
            WriteLine();

            WriteMeta("number", Document.Number);
            WriteMeta("category", Document.Category);
            WriteMeta("updates", Document.Updates);
            WriteMeta("obsoletes", Document.Obsoletes);
            WriteMeta("seriesnumber", Document.SeriesNumber);

            WriteAuthors(Document.Authors);
            WriteLine();

            foreach (var Keyword in Document.Keywords) {
                WriteMeta("keyword", Keyword);
                }


            if (Document.Abstract.Count > 0) {
                WriteHeading("Abstract", 1);
                foreach (TextBlock TextBlock in Document.Abstract) {
                    if (TextBlock.GetType() == typeof(P)) {
                        P P = (P)TextBlock;
                        WriteParagraph(P.Text);
                        }
                    }
                }

            WriteSections(Document.Middle, 1);
            WriteSections(Document.Back, 1);
            }


        public void WriteSections(List<Section> Sections, int Level) {
            foreach (Section Section in Sections) {
                if (!Section.Automatic) {
                    if (Section.Heading != null) {
                        WriteHeading(Section.Heading, Level);
                        }

                    foreach (TextBlock TextBlock in Section.TextBlocks) {
                        if (TextBlock.GetType() == typeof(LI)) {

                            }

                        if (TextBlock.GetType() == typeof(P)) {
                            P P = (P)TextBlock;
                            WriteParagraph(P.Text);
                            }

                        if (TextBlock.GetType() == typeof(PRE)) {
                            PRE PRE = (PRE)TextBlock;
                            WriteParagraphPre(PRE.Text);
                            }

                        if (TextBlock.GetType() == typeof(Table)) {
                            //Table Table = (Table)TextBlock;
                            //WriteStartTag("table", "id", Table.ID);

                            //for (int Row = 0; Row < Table.Rows.Count; Row++) {
                            //    TableRow TableRow = Table.Rows[Row];
                            //    int Col = 0;
                            //    string Tag = Row == 0 ? "th" : "td";
                            //    WriteStartTag("tr");
                            //    for (; Col < TableRow.Data.Count; Col++) {
                            //        WriteValueTag(Tag, TableRow.Data[Col].Text);
                            //        }
                            //    for (; Col < Table.MaxRow; Col++) {
                            //        WriteValueTag(Tag, "");
                            //        }
                            //    WriteEndTag("tr");
                            //    }

                            //WriteEndTag("table ");
                            }
                        }
                    WriteSections(Section.Subsections, Level + 1);
                    }
                }
            }

        void WriteAuthors(List<Author> Authors) {
            foreach (Author Author in Authors) {
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

        }
    }
