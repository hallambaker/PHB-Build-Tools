using System;
using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using GM = Goedel.Document.Markdown;

namespace OpenXML {
    public partial class BlockParseWord : GM.BlockParser {
        WordprocessingDocument Source;
        MainDocumentPart SourceMain;
        Document SourceDocument;
        Body SourceBody;


        public static void Register() {
            var ParseRegistryEntry = new GM.ParseRegistryEntry();
            ParseRegistryEntry.Include = Include;
            ParseRegistryEntry.Parse = Parse;

            GM.ParseRegistry.Register(".docx", ParseRegistryEntry);
            }

        public static GM.Document Parse(string FileName, GM.TagCatalog TagCatalog) {

            var Parser = new BlockParseWord(FileName, TagCatalog);


            return Parser.Document;
            }

        public static GM.Document Parse(byte[] Data, GM.TagCatalog TagCatalog)
            {

            var Parser = new BlockParseWord(Data, TagCatalog);


            return Parser.Document;
            }

        public static bool Include(string FileName, GM.TagCatalog TagCatalog,
                        GM.Document Document) {


            return true;
            }

        GM.CatalogEntry CatalogEntryDefault;
        GM.CatalogEntry CatalogEntryPreformatted;

        public BlockParseWord(string FileName, GM.TagCatalog TagCatalog) {
            Source = WordprocessingDocument.Open(FileName, false);

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

            SourceMain = Source.MainDocumentPart;
            SourceDocument = SourceMain.Document;
            SourceBody = SourceDocument.Body;

            // Here process document styles to catalog dictionary
            CompileDictionary(SourceMain);
            Document = new GM.Document();

            foreach (var Child in SourceBody.ChildElements) {
                ParseChild(Child);
                }
            }

        Dictionary<string, GM.CatalogEntry> StyleDictionary = 
                new Dictionary<string, GM.CatalogEntry>();


        public void CompileDictionary (MainDocumentPart SourceMain) {
            var StyleDefinitions = SourceMain.StyleDefinitionsPart;
            var Styles = StyleDefinitions.Styles;

            foreach (var Style in Styles.ChildElements) {
                GetCatalogEntry(Style as Style);
                }
            }

        void GetCatalogEntry(Style Style) {
            if (Style == null) return;

            var CatalogEntry = TagCatalog.Find(Style.StyleId);


            if (CatalogEntry == null) {
                if (Style.StyleParagraphProperties != null) {
                    var Properties = Style.StyleParagraphProperties;

                    if (Properties.OutlineLevel != null) {
                        // we have a heading here.
                        int Level;
                        var Found = int.TryParse(Properties.OutlineLevel.Val, out Level);

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
            GM.CatalogEntry Result;
            StyleDictionary.TryGetValue(StyleID, out Result);

            return Result != null ? Result : CatalogEntryDefault;
            }

        public void ParseChild (OpenXmlElement Child) {
            var Paragraph = Child as Paragraph;
            if (Paragraph == null) return;

            //Console.WriteLine("Element {0}", Paragraph.InnerText);
            var Properties = Paragraph.ParagraphProperties;

            var StyleId = Properties != null ? Properties.ParagraphStyleId : null;
            var StyleVal = StyleId!= null ? StyleId.Val.ToString(): "p";

            //Console.WriteLine("   Style {0}", StyleVal);
            var CatalogEntry = GetCatalogEntry(StyleVal);

            if (CatalogEntry.ElementType == GM.ElementType.Meta) {

                ParseMeta(CatalogEntry, Paragraph.InnerText);
                }
            else if (CatalogEntry.ElementType == GM.ElementType.Block) {
                if (StyleVal.ToLower() == "pre") {
                    ParsePreformatted(Paragraph);
                    }
                else {
                    ParseParagraph(Paragraph.InnerText, CatalogEntry);
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

                var Meta = new GM.Meta(CatalogEntry, Attributes);
                Meta.Text = Text;

                Document.MetaDataAdd(CatalogEntry, Meta);
                }
            
            }

        public void ParseParagraph (string Text, GM.CatalogEntry CatalogEntry) {
            CurrentBlock = null;

            CurrentCatalogEntry = CatalogEntry;
            MakeParagraphIfNull();
            //CurrentBlock.AddSegmentText(Text);

            //Parse input to process meta tags, etc.
            ProcessParagraphText(Text);
            }

        public void ParsePreformatted (Paragraph Paragraph) {
            bool Break = true;
            if (CurrentCatalogEntry != CatalogEntryPreformatted) {
                CurrentBlock = null;
                Break = false;
                CurrentCatalogEntry = CatalogEntryPreformatted;
                }

            MakeParagraphIfNull();
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
                var ChildRun = Child as Run;

                if (ChildRun != null) {
                    foreach (var ChildPart in ChildRun) {
                        if (ChildPart as Text != null) {
                            StringWriter.Write((ChildPart as Text).Text);
                            }
                        if (ChildPart as Break != null) {
                            StringWriter.Write("\n");
                            }
                        }

                    }
                }

            var Result = StringWriter.ToString();
            //Console.Write(Result);
            return Result;
            }



        //public static void Create(string File, HTML2RFC.Document Target) {
        //    var ReadWord = new ReadWord(File);
        //    ReadWord.Process(Target);
        //    }

        //private void Process (HTML2RFC.Document Target) {
        //    this.Target = Target;
        //    }


        }

    }
