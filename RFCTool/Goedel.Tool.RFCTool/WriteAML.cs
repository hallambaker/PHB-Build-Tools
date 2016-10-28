using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Goedel.Registry;
using Goedel.Utilities;

namespace Goedel.Tool.RFCTool {
    public partial class Writers {

        /// <summary>
        /// Write RFC Document out in MAML to file.
        /// </summary>
        /// <param name="OutputFile">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteAML(string OutputFile, Document Document) {
            using (TextWriter TextWriter = new StreamWriter(OutputFile, false, Encoding.UTF8)) {
                WriteAML(TextWriter, Document);
                }
            }



        /// <summary>
        /// Write document to specified output stream
        /// </summary>
        /// <param name="TextWriter">The output stream to write to</param>
        /// <param name="Document">The document to write</param>
        public static void WriteAML(TextWriter TextWriter, Document Document) {
            // Format document to place line numbers
            WriteAML WriteAML = new WriteAML(TextWriter);
            WriteAML.Write(Document);


            }
        }


    public class WriteAML {
        TextWriter TextWriter;
        XMLTextWriterPlus XMLOutput;
        Document Document;


        public WriteAML(TextWriter TextWriter) {
            this.TextWriter = TextWriter;
            XMLOutput = new XMLTextWriterAML(TextWriter);
            }

        string Id = "f49b1505-1206-4ffe-8f23-43f42f61e4bd";
        string Version = "1";
        string ContentType = "developerConceptualDocument";

        string xmlns = "http://ddue.schemas.microsoft.com/authoring/2003/5";
        string xmlns_xlink = "http://www.w3.org/1999/xlink";

        public void Write (Document Document) {
            this.Document = Document;
            var Stack = XMLOutput.Stack;

            XMLOutput.Start("topic", "id", Id, "revisionNumber", Version);
            XMLOutput.Start(ContentType, "xmlns", xmlns, "xmlns:xlink", xmlns_xlink);

            XMLOutput.Comment("The Abstract");
            XMLOutput.Start("introduction");
            XMLOutput.OpenBlock();
            Write(Document.Abstract);
            XMLOutput.CloseBlock();
            XMLOutput.End();

            XMLOutput.Comment("The Body");
            Write(Document.Middle);
            Write(Document.Back);

            XMLOutput.Comment("References");
            XMLOutput.Start("relatedTopics");
            XMLOutput.End();

            XMLOutput.End();
            XMLOutput.End();

            Assert.True(Stack == XMLOutput.Stack, Internal.Throw);
            }

        public void Write(List<Section> Sections) {
            if (Sections.Count ==0) {
                return;
                }
            foreach (var Section in Sections) {
                Write(Section);
                }
            }

        public void Write(Section Section) {
            var Stack = XMLOutput.Stack;

            XMLOutput.Start("section");
            XMLOutput.WriteElement("title", Section.Heading);
            XMLOutput.Start("content");

            Write(Section.TextBlocks);
            XMLOutput.End();

            if (Section.Subsections.Count > 0) {
                XMLOutput.Start("sections");
                Write(Section.Subsections);
                XMLOutput.End();
                }

            XMLOutput.End();

            Assert.True(Stack == XMLOutput.Stack, Internal.Throw);
            }


        BlockType LastBlock;
        int PendingClose;
        bool NewBlockType;

        public void Write(List<TextBlock> Texts) {
            var Stack = XMLOutput.Stack;

            //XMLOutput.OpenBlock();
            //foreach (var Text in Texts) {
            //    XMLOutput.SetTextBlock(Text);
            //    Write(Text);
            //    LastBlock = Text;
            //    }

            //XMLOutput.CloseBlock();

            XMLOutput.OpenBlock();

            LastBlock = BlockType.Null;
            PendingClose = 0;
            foreach (var Text in Texts) {
                NewBlockType = Text.BlockType != LastBlock;
                if (NewBlockType) {
                    CheckPending();
                    }

                XMLOutput.SetTextBlock(Text);
                Write(Text);
                LastBlock = Text.BlockType;
                }
            CheckPending();

            XMLOutput.CloseBlock();

            Assert.True(Stack == XMLOutput.Stack, Internal.Throw);
            }


        void CheckPending () {
            for (var i = 0; i < PendingClose; i++) {
                XMLOutput.End();
                }
            PendingClose = 0;
            }

        public void Write(TextBlock Text) {
            if (Text as PRE != null) {
                Write(Text as PRE);
                return;
                }
            if (Text as LI != null) {
                Write(Text as LI);
                return;
                }
            if (Text as P != null) {
                Write(Text as P);
                return;
                }
            }

        public void Write(P Text) {
            XMLOutput.Start("para");
            XMLOutput.Write(Text.Text);
            XMLOutput.End();
            }

        public void Write(PRE Text) {
            XMLOutput.Start("code", "language", "none");
            XMLOutput.WriteVerbatim(Text.Text);
            XMLOutput.End();
            }

        public void Write(LI Text) {
            if (Text.Type == BlockType.Term) {
                XMLOutput.Start("definedTerm");
                XMLOutput.Write(Text.Text);
                XMLOutput.End();
                }
            else if (Text.Type == BlockType.Data) {
                if (NewBlockType) {
                    XMLOutput.Start("definition");
                    PendingClose++;
                    }
                XMLOutput.Start("para");
                XMLOutput.Write(Text.Text);
                XMLOutput.End();
                }
            else if (Text.Type == BlockType.Ordered) {
                XMLOutput.Start("listItem");
                XMLOutput.Start("para");
                XMLOutput.Write(Text.Text);
                XMLOutput.End();
                XMLOutput.End();
                }
            else if (Text.Type == BlockType.Symbol) {
                XMLOutput.Start("para");
                XMLOutput.Write(Text.Text);
                XMLOutput.End();
                }
            else {
                XMLOutput.Comment("Not implemented");
                }

            }


        public void Write(Table Text) {
            XMLOutput.Comment("No tables yet");
            }
        }


    /// <summary>
    /// XML Writer customized to output the open and close tags used in the AML format.
    /// </summary>
    class XMLTextWriterAML : XMLTextWriterPlus {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="TextWriter">The output stream.</param>
        public XMLTextWriterAML(TextWriter TextWriter) : base(TextWriter) {
            }

        /// <summary>
        /// Begin a block containing either Paragraph, Definition or List content.
        /// </summary>
        /// <param name="Mode">The type of content for this block</param>
        protected override void Open(Mode Mode) {
            Console.WriteLine("++++{0}", Mode);
            if (Mode == Mode.Definition) {
                Start("definitionTable");
                }

            }

        /// <summary>
        /// End a block containing either Paragraph, Definition or List content.
        /// </summary>
        /// <param name="Mode">The type of content for this block</param>
        protected override void Close(Mode Mode) {
            Console.WriteLine("----{0}", Mode);
            if (Mode == Mode.Definition) {
                End();
                }
            }

        /// <summary>
        /// Begin a list level
        /// </summary>
        /// <param name="ListItem">The type of list</param>
        protected override void OpenList(BlockType ListItem) {
            Console.WriteLine("++++{0}", ListItem);
            if (ListItem == BlockType.Symbol) {
                Start("list", "class", "bullet");
                }
            else if (ListItem == BlockType.Ordered) {
                Start("list", "class", "ordered");
                }
            }

        /// <summary>
        /// End a list level
        /// </summary>
        /// <param name="ListItem">The type of list</param>
        protected override void CloseList(BlockType ListItem) {
            Console.WriteLine("----{0}", ListItem);
            if (ListItem == BlockType.Symbol) {
                End();
                }
            else if (ListItem == BlockType.Ordered) {
                End();
                }
            }
        }


    }
