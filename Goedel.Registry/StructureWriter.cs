using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Registry {

    public enum OutputFormat {
        Goedel,
        XML,
        JSON
        }

    public abstract class  StructureWriter {

        /// <summary>
        /// Indent is the character string that will be written out to indent 
        /// blocks of code. Default is four spaces but can be set to two spaces, 
        /// a tab character or other text as required.
        /// </summary>
        public string Indent = "    ";

        protected TextWriter TextWriter;
        protected int Level = 0;
        protected bool StartOfLine;
        bool First = true;

        /// <summary>
        /// Called to start the line
        /// </summary>
        protected void StartLine() {
            StartOfLine = true;
            if (First) {
                First = false;
                return;
                }
            TextWriter.WriteLine ();
            for (int i = 0; i < Level; i++) {
                TextWriter.Write (Indent);
                }
            }

        public static StructureWriter GetStructureWriter (TextWriter TextWriterIn, OutputFormat OutputFormat) {
            switch (OutputFormat) {
                case Goedel.Registry.OutputFormat.Goedel:
                    return new IndentWriter(TextWriterIn);
                case Goedel.Registry.OutputFormat.XML:
                    return new XMLWriter(TextWriterIn);
                default:
                    return null;
                }

            }

        protected StructureWriter() {
            }

        public StructureWriter(TextWriter TextWriterIn) {
            TextWriter = TextWriterIn;
            }

        /// <summary>
        /// Called at the start of the document.
        /// </summary>
        /// <param name="Tag">Encoding specific document preamble.</param>
        public abstract void StartDocument (string Tag);
        public void StartDocument() {
            StartDocument (null);
            }
        /// <summary>
        /// Called at the end of the document;
        /// </summary>
        /// <param name="Tag">Encoding specific .</param>
        public abstract void EndDocument (string Tag);
        public void EndDocument() {
            EndDocument (null);
            }
        public abstract void StartList (string Tag);
        public abstract void EndList (string Tag);
        public abstract void StartElement (string Tag);
        public abstract void EndElement (string Tag);

        public abstract void WriteId (string Tag, string Data);
        public abstract void WriteAttribute (string Tag, string Data);
        public abstract void WriteAttribute (string Tag, int Data);
        public abstract void WriteAttribute (string Tag, float Data);
        }


    public class IndentWriter : StructureWriter {

        void Space() {
            if (StartOfLine) {
                StartOfLine = false;
                return;
                }
            TextWriter.Write(" ");
            }
        void Write(string Data) {
            Space();
            TextWriter.Write(Data);
            }

        public IndentWriter(TextWriter TextWriterIn) {
            TextWriter = TextWriterIn;
            }

        
        public override void StartDocument(string Tag) {
            }
        public override void EndDocument(string Tag) {
            TextWriter.WriteLine();
            TextWriter.Flush();
            }

        public override void StartList(string Tag) {
            Level++;
            }
        public override void EndList(string Tag) { }
        public override void StartElement(string Tag) {
            StartLine();
            Write(Tag);
            }
        public override void EndElement(string Tag) { }
        public override void WriteId(string Tag, string Data) {
            Write(Data);
            }
        public override void WriteAttribute(string Tag, string Data) {
            Write("\"" + Data + "\"");
            }
        public override void WriteAttribute(string Tag, int Data) {
            Write(Convert.ToString(Data));
            }
        public override void WriteAttribute(string Tag, float Data) {
            Write(Convert.ToString(Data));
            }
        }

    public class XMLWriter : StructureWriter {

        public XMLWriter (TextWriter TextWriterIn) {
            TextWriter = TextWriterIn;
            }

        /// <summary>
        /// Starts the document.
        /// </summary>
        /// <param name="Tag">XML declaration string, if null a default value is used.</param>
        public override void StartDocument(string Tag) {
            if (Tag == null) {
                TextWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\" ?>");
                }
            else {
                TextWriter.WriteLine(Tag);
                }
            }
        public override void EndDocument(string Tag) {
            TextWriter.WriteLine();
            TextWriter.Flush();
            }

        public override void StartList(string Tag) { }
        public override void EndList(string Tag) { }
        public override void StartElement (string Tag) {}
        public override void EndElement (string Tag) {}
        public override void WriteId (string Tag, string Data) {}
        public override void WriteAttribute (string Tag, string Data) {}
        public override void WriteAttribute (string Tag, int Data) {}
        public override void WriteAttribute (string Tag, float Data) {}
        }
    }
