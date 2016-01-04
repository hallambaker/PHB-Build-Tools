using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Goedel.MarkLib {

    public abstract class BlockParser {
        protected Document Document;
        protected TagCatalog TagCatalog;
        protected CatalogEntry CurrentCatalogEntry = null;
        protected Block CurrentBlock = null;


        /// <summary>
        /// Final cleanup. Pop remaining enclosures off the stack and set the
        /// values of Title and ShortTitle from the metadata values (if any).
        /// </summary>
        protected void FinishParse() {

            PopLayoutAll();

            List<Meta> MetaTags;
            if (Document.MetaDataLookup("title", out MetaTags)) {
                Document.Title = MetaTags[0].Text;
                Document.ShortTitle = Document.Title;
                }
            if (Document.MetaDataLookup("short", out MetaTags)) {
                Document.ShortTitle = MetaTags[0].Text;
                }
            }

        // We have the following types of markup
        // 
        // Meta         - applies to the whole document
        // Layout       - applies to multiple paragraph blocks
        // Block        - applies to a complete block (e.g. heading)
        // Annotation   - applies to a part of a block

        protected Stack<Block> StackLayout = new Stack<Block>();
        protected Stack<TextSegmentOpen> StackAnnotation = new Stack<TextSegmentOpen>();

        // Reads in one input paragraph chunk at a time and performs
        // block level parsing

        protected bool MatchInStack(Stack<Block> Stack, CatalogEntry Key) {
            foreach (var T in Stack) {
                if (T.CatalogEntry == Key) return true;
                }
            return false;
            }

        protected bool MatchInStack(Stack<TextSegmentOpen> Stack, CatalogEntry Key) {
            foreach (var T in Stack) {
                if (T.CatalogEntry == Key) return true;
                }
            return false;
            }

        protected void PopAnnotationAll() {
            while (PopAnnotation() != null) {
                }
            }

        // Utility function for managing the Annotation stack
        protected TextSegmentOpen PopAnnotation() {
            if (StackAnnotation.Count <= 0) return null;
            var Top = StackAnnotation.Pop();
            //var Close = new Close(Top);
            //Document.Blocks.Add(Close);

            CurrentBlock.AddSegmentClose(Top);
            return Top;
            }

        protected void PopAnnotation(CatalogEntry Key) {
            if (!MatchInStack(StackAnnotation, Key)) {
                return;
                }
            var Top = PopAnnotation();
            while (Top.CatalogEntry != Key) {
                Top = PopAnnotation();
                }
            }


        // Utility function for managing the Layout stack
        protected void PopLayoutAll() {

            while (PopLayout() != null) {
                }
            }

        protected Block PopLayout() {
            if (StackLayout.Count <= 0) return null;
            var Top = StackLayout.Pop();
            var Close = new Close(Top);
            Document.Blocks.Add(Close);
            return Top;
            }

        protected void PopLayout(CatalogEntry Key) {
            if (!MatchInStack(StackLayout, Key)) {
                return;
                }
            var Top = PopLayout();
            while (Top.CatalogEntry != Key) {
                Top = PopLayout();
                }
            }

        protected void PurgeStack(CatalogEntry CatalogEntry) {
            while (StackLayout.Count > 0) {
                var Top = StackLayout.Peek();
                // Can we find a Wrapper statement?

                if (Top.CatalogEntry.Wrappers.Count == 0) {
                    return;
                    }

                foreach (var Allowed in Top.CatalogEntry.Wrappers) {
                    if (Allowed == CatalogEntry) {
                        return;
                        }
                    }

                PopLayout();
                }
            }

        protected void MakeEnclosure(CatalogEntry CatalogEntry) {
            PurgeStack(CatalogEntry);


            if (CatalogEntry.Enclosure == null) return;
            if (!MatchInStack(StackLayout, CatalogEntry.Enclosure)) {
                MakeLayout(CatalogEntry.Enclosure, null);
                CurrentBlock = null;
                }
            }


        protected void CheckLayoutWrapper(CatalogEntry CatalogEntry) {
            while (StackLayout.Count > 0) {
                var Top = StackLayout.Peek();

                foreach (var Match in CatalogEntry.Enclosures) {
                    if (Top.CatalogEntry == Match) return;
                    }
                PopLayout();
                }
            }

        protected void MakeLayout(CatalogEntry CatalogEntry, List<TagValue> Attributes) {
            // here we need to check up on the state of the stack

            CheckLayoutWrapper(CatalogEntry);
            MakeEnclosure(CatalogEntry);

            CurrentBlock = Block.MakeBlock(CatalogEntry, Attributes);
            Document.Blocks.Add(CurrentBlock);
            StackLayout.Push(CurrentBlock);
            }


        protected void MakeParagraphIfNull() {
            MakeEnclosure(CurrentCatalogEntry);

            if (CurrentBlock == null) {
                CurrentBlock = new Paragraph();
                CurrentBlock.CatalogEntry = CurrentCatalogEntry;
                Document.Blocks.Add(CurrentBlock);
                }
            CurrentCatalogEntry = TagCatalog.Defaults[0];
            }


        /// <summary>
        /// Process text chunks one at a time and write out the corresponding
        /// annotation blocks.
        /// </summary>
        /// <param name="Text">Text string input.</param>
        protected void ProcessParagraphText(string Text) {
            var Reader = new StringReader(Text);
            MarkDownParagraph Lexer = new MarkDownParagraph(Reader);

            var Token = Lexer.GetToken();
            var LastAttributes = Lexer.Attributes;

            while (Token != MarkDownParagraph.Token.INVALID) {
                ////Console.WriteLine("   {0} : {1}", Token, Lexer.Data);
                //foreach (var Tag in Lexer.Attributes) {
                //    Console.WriteLine("      {0} = {1}", Tag.Tag, Tag.Value);
                //    }
                ProcessText(Token, Lexer.Data, Lexer.Attributes, Lexer.Tag);
                Token = Lexer.GetToken();
                }

            ProcessText(Token, null, null, null);
            PopAnnotationAll();
            }

        void ProcessText(MarkDownParagraph.Token Token, string Text,
                        List<TagValue> Attributes, string Tag) {

            ProcessTextStart(Text);

            if (Token == MarkDownParagraph.Token.INVALID) {
                PopAnnotation(null);
                return;
                }

            var CatalogEntry = ProcessTextTag(Attributes, Tag);
            if (CatalogEntry == null) {
                return;
                }

            ProcessTextCatalogEntry(Token, CatalogEntry, Attributes);
            }

        void ProcessTextStart(string Text) {
            // Process the text part

            if ((Text != null) && Text.Length > 0) {
                MakeParagraphIfNull();
                CurrentBlock.AddSegmentText(Text);
                }
            }
        CatalogEntry ProcessTextTag(List<TagValue> Attributes, string Tag) {
            // Process the tag part
            //
            // TagCatalog.Find returns null if the Attributes are empty,
            // Otherwise a Catalog entry or 'unknown' is returned.

            var CatalogEntry = TagCatalog.Find(Attributes);
            if (CatalogEntry == null) {
                CatalogEntry = TagCatalog.Find(Tag);
                }
            return CatalogEntry;
            }

        protected void ProcessTextCatalogEntry(MarkDownParagraph.Token Token,
                    CatalogEntry CatalogEntry, List<TagValue> Attributes) {
            // Layout and annotation are always content tags
            if ((Token == MarkDownParagraph.Token.Open) |
                 (Token == MarkDownParagraph.Token.Empty)) {

                // Layout tag always means start a new block
                if (CatalogEntry.ElementType == ElementType.Layout) {
                    MakeLayout(CatalogEntry, Attributes);
                    CurrentBlock = null;
                    }

                // Annotation tag is markup within a block
                else if (CatalogEntry.ElementType == ElementType.Annotation) {
                    MakeParagraphIfNull();
                    var Segment = CurrentBlock.AddSegmentOpen(CatalogEntry, Attributes);
                    StackAnnotation.Push(Segment);
                    }
                }

            // Meta, Block and Item tags never have close tags they create exactly one block
            if (CatalogEntry.Key == "include") {
                Console.WriteLine("Read file {0}", Attributes[0].Value);


                ParseRegistry.Include(Attributes[0].Value, TagCatalog, Document);
                }

            else if (CatalogEntry.ElementType == ElementType.Meta) {
                Meta Meta = new Meta(CatalogEntry, Attributes);
                Document.MetaDataAdd(CatalogEntry, Meta);
                CurrentBlock = Meta;

                }
            else if (CatalogEntry.ElementType == ElementType.Block) {
                CurrentBlock = Block.MakeBlock(CatalogEntry, Attributes);
                Document.Blocks.Add(CurrentBlock);
                }
            else if (CatalogEntry.ElementType == ElementType.Item) {
                MakeParagraphIfNull();
                CurrentBlock.AddSegmentEmpty(CatalogEntry, Attributes);
                }

            // Layout and annotation require explicit close tags
            if ((Token == MarkDownParagraph.Token.Close) |
                 (Token == MarkDownParagraph.Token.Empty)) {
                if (CatalogEntry.ElementType == ElementType.Layout) {
                    PopLayout(CatalogEntry);
                    }

                else if (CatalogEntry.ElementType == ElementType.Annotation) {
                    PopAnnotation(CatalogEntry);
                    }
                }
            }



        }

    /// <summary>
    /// The block parser for markdown.
    /// </summary>
    public partial class BlockParserMarkDown : BlockParser {

        public BlockParserMarkDown(TagCatalog TagCatalog, Document Document) {
            this.TagCatalog = TagCatalog;
            this.Document = Document;
            }

        public static void Register() {
            var ParseRegistryEntry = new ParseRegistryEntry();
            ParseRegistryEntry.Include = Include;
            ParseRegistryEntry.Parse = Parse;

            ParseRegistry.Register(".md", ParseRegistryEntry);
            }

        /// <summary>
        /// Top level parse function. 
        /// </summary>
        public void Parse() {
            ParseMarkDown(Document.Paragraphs);
            FinishParse();
            }

        /// <summary>
        /// Partial parse function, does not perform cleanup.
        /// </summary>
        /// <param name="Paragraphs"></param>
        public void ParseMarkDown(List<Paragraph> Paragraphs) {

            foreach (var Paragraph in Paragraphs) {
                AddBlocks(Paragraph);
                }

            }

        /// <summary>
        /// Static method for complete document parse.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TagCatalog"></param>
        /// <returns></returns>
        public static Document Parse(string FileName, TagCatalog TagCatalog) {
            var TextReader = new StreamReader(FileName);
            return Parse(TextReader, TagCatalog);
            }

        /// <summary>
        /// Static method for complete document parse.
        /// </summary>
        /// <param name="TextReader"></param>
        /// <param name="TagCatalog"></param>
        /// <returns></returns>
        public static Document Parse(TextReader TextReader, TagCatalog TagCatalog) {

            var Reader = new Reader(TextReader);
            var Document = new Document();

            Document.Parse(Reader);
            var BlockParser = new BlockParserMarkDown(TagCatalog, Document);
            BlockParser.Parse();

            return Document;
            }


        /// <summary>
        /// Static method for parsing included file.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TagCatalog"></param>
        /// <param name="Document"></param>
        /// <returns></returns>
        public static bool Include(string FileName, TagCatalog TagCatalog, 
                        Document Document) {
            var Reader = new Reader(FileName);

            var Included = new Document();
            Included.Parse(Reader);

            var BlockParser = new BlockParserMarkDown(TagCatalog, Document);
            BlockParser.ParseMarkDown(Included.Paragraphs);

            return true;
            }




        /// <summary>
        /// Convert a MarkDown Paragraph into a series of blocks, metadata entries,
        /// etc.
        /// </summary>
        /// <param name="Paragraph"></param>
        void AddBlocks(Paragraph Paragraph) {

            // Reset the current block at the start of a new input paragraph
            CurrentBlock = null;

            if (Paragraph.BlockType == BlockType.Preformatted) {
                CurrentCatalogEntry = TagCatalog.FindDefault("pre");
                MakeParagraphIfNull();
                CurrentBlock.AddSegmentText(Paragraph.Text);

                return;
                }

            // Here we set up default paragraph styles using the built in 
            // Catalog Entries.
            if (Paragraph.BlockType == BlockType.Heading) {
                CurrentCatalogEntry = TagCatalog.Defaults[Paragraph.Level];
                }
            else if (Paragraph.BlockType == BlockType.Bullet) {
                CurrentCatalogEntry = TagCatalog.FindDefault("li");
                }
            else if (Paragraph.BlockType == BlockType.Numbered) {
                CurrentCatalogEntry = TagCatalog.FindDefault("nli");
                }
            else if (Paragraph.BlockType == BlockType.DefinedData) {
                CurrentCatalogEntry = TagCatalog.FindDefault("dd");
                }
            else if (Paragraph.BlockType == BlockType.DefinedTerm) {
                CurrentCatalogEntry = TagCatalog.FindDefault("dt");
                }


            else if (Paragraph.BlockType == BlockType.Tagged) {
                CurrentCatalogEntry = Paragraph.CatalogEntry;
                Console.WriteLine("   KeyNotFoundException {0}", CurrentCatalogEntry.Key);
                ProcessTextCatalogEntry(MarkDownParagraph.Token.Open,
                        Paragraph.CatalogEntry, Paragraph.Attributes);
                }
            else if (Paragraph.BlockType == BlockType.Paragraph) {
                CurrentCatalogEntry = TagCatalog.Defaults[0];
                }
            else {
                CurrentCatalogEntry = TagCatalog.Defaults[0];
                }

            ProcessParagraphText(Paragraph.Text);

            }

        }
    }
