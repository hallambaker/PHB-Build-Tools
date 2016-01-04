using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.MarkLib;
using Word = Microsoft.Office.Interop.Word;

namespace Goedel.WordLib {
    public class WordDocument : Document {

        public WordDocument(DocumentSet Parent, FileInfo FileInfo) : 
                    base (Parent, FileInfo) {
            Link = System.IO.Path.GetFileNameWithoutExtension(Name) + ".html";
            SourceFormat = "Word";
            }


        public static WordDocument Create(DocumentSet Parent, FileInfo FileInfo,
                    TagCatalog TagCatalog) {
            var Application = new Word.Application();

            var Result = Create (Application, Parent, FileInfo, TagCatalog);

            object Save = Word.WdSaveOptions.wdDoNotSaveChanges;
            object missing =  System.Reflection.Missing.Value;
            Application.Quit(ref Save, ref missing, ref missing);

            return Result;
            }



        public static WordDocument Create(Word.Application Application,
                DocumentSet Parent, FileInfo FileInfo,
                    TagCatalog TagCatalog) {


            var SourceDocument = Application.Documents.Open(FileInfo.FullName, ReadOnly: true);
            var TargetDocument = new WordDocument(Parent, FileInfo);

            TargetDocument.Blocks = new List<Block>();
            TargetDocument.MetaData = new Dictionary<string, List<Meta>>();
            TargetDocument.Headings = new List<Heading>();

            foreach (Word.Paragraph Paragraph in SourceDocument.Paragraphs) {
                Word.Style Style = Paragraph.get_Style();

                //Console.WriteLine("Paragraph {0}:", Style.NameLocal);

                var CatalogEntry = TagCatalog.Find(Style.NameLocal);
                if (CatalogEntry == null) {
                    if (Style.BuiltIn) {
                        switch (Style.NameLocal) {
                            case "Heading 1": CatalogEntry = TagCatalog.Defaults[1]; break;
                            case "Heading 2": CatalogEntry = TagCatalog.Defaults[2]; break;
                            case "Heading 3": CatalogEntry = TagCatalog.Defaults[3]; break;
                            case "Heading 4": CatalogEntry = TagCatalog.Defaults[4]; break;
                            case "Heading 5": CatalogEntry = TagCatalog.Defaults[5]; break;
                            case "Heading 6": CatalogEntry = TagCatalog.Defaults[6]; break;
                            case "List Paragraph" :
                                CatalogEntry = TagCatalog.Find("li");
                                break;
                            }

                        }
                    }

                var TargetParagraph = new Paragraph();

                if (CatalogEntry != null) {
                    TargetParagraph.Attributes = new List<TagValue>();
                    var TagValue = new TagValue();
                    TagValue.Tag = Style.NameLocal;
                    TargetParagraph.Attributes.Add(TagValue);
                    }
                else {
                    CatalogEntry = TagCatalog.Defaults[0];
                    }

                // Have determined the block type and created a container. Now
                // add in the text.

                // This is a quick and dirty version that ignores the Word formatting.

                Word.Range Range = Paragraph.Range;
                var Text = Range.Text;

                //Console.WriteLine("    {0}", Text);

                // Word creates spaces between paragraphs by simply adding in an empty 
                // paragraph. So suppress these.
                if (Text != "\r") {
                    TargetParagraph.Tag = Style.NameLocal;
                    TargetParagraph.Text = Text;
                    TargetParagraph.BlockType = BlockType.Tagged;
                    TargetParagraph.CatalogEntry = CatalogEntry;
                    TargetDocument.Paragraphs.Add(TargetParagraph);
                    }
                }

            var BlockParser = new BlockParserMarkDown(TagCatalog, TargetDocument);
            BlockParser.Parse();

            return TargetDocument;
            }
        }
    }
