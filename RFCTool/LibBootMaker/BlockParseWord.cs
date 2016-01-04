//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;



//namespace Goedel.MarkLib {
//    public partial class BlockParser {

//        public void ParseWord() {
//            Document.Blocks = new List<Block>();
//            Document.MetaData = new Dictionary<string, List<Meta>>();
//            Document.Headings = new List<Heading>();


//            foreach (var Paragraph in Document.Paragraphs) {
//                var CatalogEntry = Paragraph.CatalogEntry;
//                CurrentCatalogEntry = CatalogEntry;

//                if (CatalogEntry.ElementType == ElementType.Meta) {
//                    Meta Meta = new Meta(CatalogEntry, Paragraph.Attributes);
//                    Document.MetaDataAdd(CatalogEntry, Meta);
//                    CurrentBlock = Meta;
//                    }
//                else if (CatalogEntry.ElementType == ElementType.Block) {
//                    CheckLayoutWrapper(CatalogEntry);
//                    MakeEnclosure(CurrentCatalogEntry);
//                    CurrentBlock = Block.MakeBlock(CatalogEntry, Paragraph.Attributes);
//                    Document.Blocks.Add(CurrentBlock);
//                    }
//                else if (CatalogEntry.ElementType == ElementType.Item) {
//                    MakeParagraphIfNull();
//                    CurrentBlock.AddSegmentEmpty(CatalogEntry, Paragraph.Attributes);
//                    }
//                else {

//                    Console.WriteLine("Aggh!");
//                    }

//                var Trimmed = Paragraph.Text.Trim();
//                var Reader = new StringReader(Trimmed);
//                MarkDownParagraph Lexer = new MarkDownParagraph(Reader);

//                var Token = Lexer.GetToken();
//                var LastAttributes = Lexer.Attributes;

//                while (Token != MarkDownParagraph.Token.INVALID) {
//                    ////Console.WriteLine("   {0} : {1}", Token, Lexer.Data);
//                    //foreach (var Tag in Lexer.Attributes) {
//                    //    Console.WriteLine("      {0} = {1}", Tag.Tag, Tag.Value);
//                    //    }
//                    ProcessText(Token, Lexer.Data, Lexer.Attributes, Lexer.Tag);
//                    Token = Lexer.GetToken();
//                    }

//                ProcessText(Token, null, null, null);
//                PopAnnotationAll();

//                }

//            FinishParse();
//            }


//        }
//    }
