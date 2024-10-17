using System;
using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using GM = Goedel.Document.Markdown;
using Goedel.IO;

namespace Goedel.Document.OpenXML {
    public partial class BlockParseWord : GM.BlockParser {
        WordprocessingDocument Source;
        MainDocumentPart SourceMain;
        DocumentFormat.OpenXml.Wordprocessing.Document SourceDocument;
        Body SourceBody;


        public static void Register() {
            var ParseRegistryEntry = new GM.ParseRegistryEntry() {
                Include = Include,
                Parse = Parse
                };
            GM.ParseRegistry.Register(".docx", ParseRegistryEntry);
            }

        public static GM.MarkdownDocument Parse(string FileName, GM.TagCatalog TagCatalog) {

            var Parser = new BlockParseWord(FileName, TagCatalog);


            return Parser.Document;
            }

        public static GM.MarkdownDocument Parse(byte[] Data, GM.TagCatalog TagCatalog)
            {

            var Parser = new BlockParseWord(Data, TagCatalog);


            return Parser.Document;
            }

        public static bool Include(string FileName, GM.TagCatalog TagCatalog,
                        GM.MarkdownDocument Document) => true;
        public static bool Img(string FileName, GM.TagCatalog TagCatalog,
                        GM.MarkdownDocument Document) => true;


        GM.CatalogEntry CatalogEntryDefault;
        GM.CatalogEntry CatalogEntryPreformatted;
        GM.CatalogEntry CatalogEntryTable;
        GM.CatalogEntry CatalogEntryTableRow;
        GM.CatalogEntry CatalogEntryTableCell;

        public BlockParseWord(string FileName, GM.TagCatalog TagCatalog) {
            FileName.OpenReadToEnd(out var data);
            var stream = new MemoryStream(data);

            Source = WordprocessingDocument.Open(stream, false);

            InitBlockParseWord (Source, TagCatalog);
            }

        public BlockParseWord(byte [] Data, GM.TagCatalog TagCatalog) {
            var Stream = new MemoryStream(Data);
            Source = WordprocessingDocument.Open(Stream, false);

            InitBlockParseWord(Source, TagCatalog);
            }

        public  void InitBlockParseWord(WordprocessingDocument Source, GM.TagCatalog TagCatalog) {
            this.TagCatalog = TagCatalog;
            CatalogEntryDefault = TagCatalog.Find("p");
            CatalogEntryPreformatted = TagCatalog.Find("pre");
            CatalogEntryTable = TagCatalog.Find("table");
            CatalogEntryTableRow = TagCatalog.Find("tablerow");
            CatalogEntryTableCell = TagCatalog.Find("tablecell");

            SourceMain = Source.MainDocumentPart;
            SourceDocument = SourceMain.Document;
            SourceBody = SourceDocument.Body;

            // Here process document styles to catalog dictionary
            CompileDictionary(SourceMain);
            Document = new GM.MarkdownDocument();

            foreach (var Child in SourceBody.ChildElements) {
                ParseChild(Child);
                }
            }

        Dictionary<string, GM.CatalogEntry> StyleDictionary = 
                new();


        public void CompileDictionary (MainDocumentPart SourceMain) {
            var StyleDefinitions = SourceMain.StyleDefinitionsPart;
            var Styles = StyleDefinitions.Styles;

            foreach (var Style in Styles.ChildElements) {
                GetCatalogEntry(Style as Style);
                }
            }

        void GetCatalogEntry(Style Style) {
            if (Style == null) {
                return;
                }

            var CatalogEntry = TagCatalog.Find(Style.StyleId);

            if (CatalogEntry == null) {
                if (Style.StyleParagraphProperties != null) {
                    var Properties = Style.StyleParagraphProperties;

                    if (Properties.OutlineLevel != null) {
                        // we have a heading here.
                        var Found = int.TryParse(Properties.OutlineLevel.Val, out var Level);

                        if (Found & Level >= 0 & Level < 6) {
                            string Tag = "h" + (Level+1).ToString();
                            CatalogEntry = TagCatalog.Find(Tag);

                            //Console.WriteLine("[{0} -> {1}]", Style.StyleId, Tag);
                            }
                        }
                    }
                }


            // Default to normal paragraph
            if (CatalogEntry == null) {
                CatalogEntry = CatalogEntryDefault;
                }

            StyleDictionary.Add(Style.StyleId, CatalogEntry);
            }

        GM.CatalogEntry GetCatalogEntry (string StyleID) {
            StyleDictionary.TryGetValue(StyleID, out var Result);

            return Result ?? CatalogEntryDefault;
            }

        public void ParseChild (OpenXmlElement Child) {
            switch (Child) {
                case Paragraph Paragraph: ParseParagraph(Paragraph); return;
                case Table Table: ParseTable(Table); return;
                }
            }

        // This rather peculiar arrangement required because these are not lists,
        // they are enumerations of a hidden structure.
        void DumpRun (OpenXmlElement Element, string Indent) {

            Console.WriteLine("{0}Type {1} Children={2}, Text=<{3}>", Indent, Element.GetType(),
                        Element.HasChildren, Element.InnerText);
            if (Element.HasChildren) {
                foreach (var Child in Element) {
                    DumpRun(Child, Indent + "  ");
                    }
                }

            }



        public void ParseParagraph (Paragraph Paragraph) {

            var Properties = Paragraph.ParagraphProperties;

            var StyleId = Properties?.ParagraphStyleId;
            var StyleVal = StyleId!= null ? StyleId.Val.ToString(): "p";

            var CatalogEntry = GetCatalogEntry(StyleVal);

            if (CatalogEntry.ElementType == GM.ElementType.Meta) {

                ParseMeta(CatalogEntry, Paragraph.InnerText);
                }
            else if (CatalogEntry.ElementType == GM.ElementType.Block) {
                if (StyleVal.ToLower() == "pre") {
                    ParsePreformatted(Paragraph);
                    }
                else {
                    ParseParagraph(Paragraph, CatalogEntry);
                    }
                }
            }

        string GetHyperlink (string Tag) {
            foreach (var Entry in Source.MainDocumentPart.HyperlinkRelationships) {
                if (Tag == Entry.Id) {
                    return Entry.Uri.OriginalString;
                    }
                }
            

            return null;
            }

        void Add (GM.MarkNewParagraph Lexer, Hyperlink Hyperlink) {
            var HREF = GetHyperlink(Hyperlink.Id);
            var Attributes = new List <GM.TagValue>{ new GM.TagValue ("a","http://whatever")};
            Lexer.SegmentFull("a", Attributes, Hyperlink.InnerText);
            }

        struct TextProperty {
            public bool Open;
            public bool Close;
            public bool Value;

            public bool Set (bool New) {
                if (New == Value) {
                    Open = false; Close = false; Value = New; return true;
                    }
                if (New) {
                    Open = true; Close = false; Value = New; return true;
                    }
                Open = false; Close = true; Value = New; return false;
                }
            }

        TextProperty Bold = new();
        TextProperty Italic = new();
        TextProperty Underline = new();
        TextProperty Subscript = new();
        TextProperty Superscript = new();

        void Add (GM.MarkNewParagraph Lexer, Run Run) {

            //Console.WriteLine($"Run {Run.InnerText}");

            if (Lexer.XMLTagMode) {
                Lexer.Push(Run.InnerText);
                return;
                }

            var Changed = false;

            var RunProperties = Run.RunProperties;
            if (RunProperties != null) {

                // We do not process style inside a character style. It is just too complicated.
                if (RunProperties.RunStyle != null) {
                    Lexer.PushEnd();
                    Lexer.SegmentFull(RunProperties.RunStyle.Val, null, Run.InnerText);
                    Lexer.PushStart();
                    return;
                    }

                // Check to see what styles have changed.
                Changed = Bold.Set(RunProperties.Bold != null);
                Changed |= Italic.Set(RunProperties.Italic != null);
                Changed |= Underline.Set(RunProperties.Underline != null);

                var Valign = Run.RunProperties.VerticalTextAlignment;
                if (Valign != null) {
                    Changed |= Subscript.Set(Valign.Val == VerticalPositionValues.Subscript);
                    Changed |= Superscript.Set(Valign.Val == VerticalPositionValues.Superscript);
                    }
                else {
                    Changed |= Subscript.Set(false);
                    Changed |= Superscript.Set(false);
                    }
                }
            else {
                // All annotations must have reset.
                Changed = Bold.Set(false);
                Changed |= Italic.Set(false);
                Changed |= Underline.Set(false);
                Changed |= Subscript.Set(false);
                Changed |= Superscript.Set(false);
                }



            // Close all contexts first
            if (Bold.Close) {
                PopStack(Lexer, "b");
                }
            if (Italic.Close) {
                PopStack(Lexer, "i");
                }
            if (Underline.Close) {
                PopStack(Lexer, "tt");
                }
            if (Subscript.Close) {
                PopStack(Lexer, "sub");
                }
            if (Superscript.Close) {
                PopStack(Lexer, "sup");
                }

            //if (Changed) {
            //    Lexer.MakeSegment();
            //    }

            // Now open any new ones
            if (Bold.Open) {
                PushStack(Lexer, "b");
                }
            if (Italic.Open) {
                PushStack(Lexer, "i");
                }
            if (Underline.Open) {
                PushStack(Lexer, "tt");
                }
            if (Subscript.Open) {
                PushStack(Lexer, "sub");
                }
            if (Superscript.Open) {
                PushStack(Lexer, "sup");
                }

            if (Lexer.StackAnnotation.Count > 0) {
                Lexer.SegmentText(Run.InnerText);
                }
            else {
                Lexer.Push(Run.InnerText);
                }
            }


        void PushStack (GM.MarkNewParagraph Lexer, string tag) {
            Lexer.MakeSegment();
            var Segment = Lexer.SegmentStart(tag);
            Lexer.StackAnnotation.Push(Segment);
            }

        void PopStack (GM.MarkNewParagraph Lexer, string Tag) {
            Lexer.MakeSegment();
            Lexer.PopAnnotation(Tag);
            }

        public void ParseParagraph (Paragraph Paragraph, GM.CatalogEntry CatalogEntry) {
            CurrentBlock = null;

            CurrentCatalogEntry = CatalogEntry;
            StartNewParagraph();

            var Lexer = new GM.MarkNewParagraph(CurrentBlock);
            foreach (var Child in Paragraph.ChildElements) {
                switch (Child) {

                    case Hyperlink Hyperlink: {
                        Add(Lexer, Hyperlink);
                        break;
                        }

                    case Run Run: {
                        Add(Lexer, Run);
                        break;
                        }
                    default: {
                        break;
                        }
                    }
                }
            Lexer.PushEnd ();
            }



        public void ParseTable (Table Table) {
            var TableOut = GM.Block.MakeBlock(CatalogEntryTable, null);
            Document.Blocks.Add(TableOut); // add to the document
            CurrentBlock = null;   // Force text following table into new paragraph.

            foreach (var Row in Table.ChildElements) {
                switch (Row) {
                    case TableRow TableRow :{
                        var Open = TableOut.AddSegmentOpen(CatalogEntryTableRow, null);
                        foreach (var Cell in TableRow.ChildElements) {
                            switch (Cell) {
                                case TableCell TableCell: {
                                    var OpenCell = TableOut.AddSegmentOpen(CatalogEntryTableCell, null);
                                    TableOut.AddSegmentText (TableCell.InnerText);
                                    TableOut.AddSegmentClose(OpenCell);
                                    break;
                                    }
                                }
                            }
                        TableOut.AddSegmentClose(Open);
                        break;
                        }
                    }
                }
            }


        public void ParseMeta (GM.CatalogEntry CatalogEntry, string Text) {
            CurrentCatalogEntry = CatalogEntryDefault;

            if (CatalogEntry.Key == "meta") {
                ProcessParagraphText(Text);
                }
            else {

                var TagValue = new GM.TagValue(CatalogEntry.Key, null);
                var Attributes = new List<GM.TagValue> { TagValue};

                var Meta = new GM.Meta(CatalogEntry, Attributes) {
                    Text = Text
                    };

                Document.MetaDataAdd(CatalogEntry, Meta);
                }
            
            }

        public void ParsePreformatted (Paragraph Paragraph) {
            bool Break = true;
            if (CurrentCatalogEntry != CatalogEntryPreformatted) {
                CurrentBlock = null;
                Break = false;
                CurrentCatalogEntry = CatalogEntryPreformatted;
                }

            StartNewParagraph();
            CurrentCatalogEntry = CatalogEntryPreformatted;

            var Text = ParseParagraph(Break, Paragraph);
            
            CurrentBlock.AddSegmentText(Text);
            }


        public string ParseParagraph (bool Break, Paragraph Paragraph) {
            var StringWriter = new StringWriter();

            if (Break) {
                StringWriter.Write("\n");
                }

            foreach (var Child in Paragraph.ChildElements) {

                switch (Child) {
                    case Run ChildRun: {
                        foreach (var ChildPart in ChildRun) {
                            if (ChildPart as Text != null) {
                                StringWriter.Write((ChildPart as Text).Text);
                                }
                            if (ChildPart as Break != null) {
                                StringWriter.Write("\n");
                                }
                            }
                        break;
                        }
                    }

                }

            var Result = StringWriter.ToString();
            return Result;
            }

        }

    }
