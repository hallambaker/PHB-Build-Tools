using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Utilities;
using GM=Goedel.Document.Markdown;

namespace Goedel.Document.RFC {

    public partial class Writers {

        /// <summary>
        /// Write RFC Document out in Markdown Format to file.
        /// </summary>
        /// <param name="OutputFile">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteMD(string OutputFile, BlockDocument Document) {
            using var TextWriter = new StreamWriter(OutputFile, false, Encoding.UTF8);
            WriteMD(TextWriter, Document);
            }

        /// <summary>
        /// Write RFC Document out in Markdown Format to text writer.
        /// </summary>
        /// <param name="TextWriter">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteMD(TextWriter TextWriter, BlockDocument Document) {
            var Writer = new WriteMD(TextWriter);
            Writer.Write(Document);
            }

        }

    public class WriteMD : MakeFormat {
        TextWriter TextWriter;
        int Width = 72;

        public WriteMD(TextWriter TextWriter) => this.TextWriter = TextWriter;

        public void WriteLine() => TextWriter.WriteLine();

        bool IsWhite (char c) => (c == ' ') | (c == '\t');
           

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
                        Buffer += c;
                        }
                    }
                else {
                    if (IsWhite(c)) {
                        if (State == 1) {
                            WBuffer += " ";
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


        void OpenListItem (BlockType ListItem) {
            switch (ListItem) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: TextWriter.WriteLine("<dl>"); ; return;
                case BlockType.Ordered: TextWriter.WriteLine("<ol>"); ; return;
                case BlockType.Symbol: TextWriter.WriteLine("<ul>"); ; return;
                }
            }

        void CloseListItem (BlockType ListItem) {
            switch (ListItem) {
                case BlockType.Definitions:
                case BlockType.Term:
                case BlockType.Data: TextWriter.WriteLine("</dl>"); ; return;
                case BlockType.Ordered: TextWriter.WriteLine("</ol>"); ; return;
                case BlockType.Symbol: TextWriter.WriteLine("</ul>"); ; return; ;
                }
            }


        public void WriteParagraphLI (LI LI) {
            WriteLine();
            ListLevel.SetListLevel(LI.Level-1, LI.Type, LI.GeneratedID);
            switch (LI.Type) {
                case BlockType.Data: {
                    Write(LI.Segments, "<dd>");
                    break;
                    }
                case BlockType.Term: {
                    Write(LI.Segments, "<dt>");
                    break;
                    }
                case BlockType.Ordered: 
                case BlockType.Symbol: {
                    Write(LI.Segments, "<li>");
                    break;
                    }
                }
            }


        public void WriteTable (Table Table) {

            throw new NYI(); // disable for now

            //TextWriter.WriteLine( "<table={0}>", Table.GeneratedID);
            //TextWriter.WriteLine("<thead>");
            //TextWriter.WriteLine("<tr>");
            //foreach (var Data in Table.Body[0].Data) {
            //    TextWriter.WriteLine("<td>{0}", Data.Text);
            //    }
            //TextWriter.WriteLine("<tbody>");
            //TextWriter.WriteLine("<tr>");
            //for (var i = 1; i < Table.Body.Count; i++) {
            //    TextWriter.WriteLine("<tr>");
            //    foreach (var Data in Table.Body[i].Data) {
            //        TextWriter.WriteLine("<td>{0}", Data.Text);
            //        }
            //    }
            //TextWriter.WriteLine("</table>");
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
        public void WriteMetaEnd() => TextWriter.WriteLine();


        public override void MakeMeta(string Tag, string Value, int Indent=0) {
            if (Value != null) {
                for (var i = 0; i < Indent; i++) {
                    TextWriter.Write("    ");
                    }
                WriteMetaStart(Tag, Value);
                WriteMetaData(Value);
                WriteMetaEnd();
                }
            }

        ListLevel ListLevel;

        // Write out the document
        public void Write(BlockDocument Document) {
            ListLevel = new ListLevel() { OpenListItem = OpenListItem, CloseListItem = CloseListItem };

            TextWriter.Write("// This file was converted using RFCTool");
            WriteLine();

            WriteHeader(Document);

            WriteLine();

            if (Document.Abstract.Count > 0) {
                WriteHeading("Abstract", 1);
                foreach (TextBlock TextBlock in Document.Abstract) {
                    if (TextBlock.GetType() == typeof(P)) {
                        P P = (P)TextBlock;
                        WriteP(P);
                        // ToDo - write out proper formatter here.
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
                            WriteParagraphLI(TextBlock as LI);
                            }
                        else {
                            ListLevel.ListLast();
                            if (TextBlock.GetType() == typeof(P)) {
                                P P = (P)TextBlock;
                                WriteP(P);
                                }

                            if (TextBlock.GetType() == typeof(PRE)) {
                                PRE PRE = (PRE)TextBlock;
                                WriteParagraphPre(PRE.TextSegments);
                                }

                            if (TextBlock.GetType() == typeof(Table)) {
                                WriteTable(TextBlock as Table);
                                }
                            
                            }
                        }

                    ListLevel.ListLast();

                    WriteSections(Section.Subsections, Level + 1);
                    }
                }
            }


        public void Write (List<GM.TextSegment> Segments, string Tag=null) {
            var Buffer = new StringBuilder();

            if (Segments == null) {
                return;
                }
            if (Tag != null) {
                Buffer.Append(Tag);
                }

            foreach (var Segment in Segments) {
                switch (Segment) {
                    case GM.TextSegmentText Text: {
                        Buffer.Append(Text.Text);
                        break;
                        }
                    case GM.TextSegmentOpen Open: {
                        Buffer.Append("<");
                        bool First = true;
                        foreach (var Attribute in Open.Attributes) {
                            if (!First) {
                                Buffer.Append(" ");
                                }
                            First = true;
                            Buffer.Append(Attribute.Tag);
                            if (Attribute.Value != null) {
                                Buffer.Append("=\"");
                                Buffer.Append(Attribute.Value);
                                Buffer.Append("\"");
                                }
                            }
                        if (Open.IsEmpty) {
                            Buffer.Append("/");
                            }
                        Buffer.Append(">");
                        //Write(Text);
                        break;
                        }
                    case GM.TextSegmentClose Text: {
                        var Open = Text.Open;
                        if (!Open.IsEmpty) {
                            Buffer.Append("</");
                            Buffer.Append(Open.Tag);
                            Buffer.Append(">");
                            }

                        //Write(Text);
                        break;
                        }
                    case GM.TextSegmentEmpty Text: {
                        //WriteElement(Text.Tag);
                        break;
                        }
                    }
                }
            TextWriter.WriteLine(Buffer.ToString().Wrap());

            }


        public void WriteP (P P) {
            TextWriter.WriteLine();
            if (P == null) {
                return;
                }

            Write(P.Segments);
            }
        }
    }
