﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.FSR;
using Goedel.IO;

namespace Goedel.Document.Markdown {

    /// <summary>
    /// The block parser for markdown.
    /// </summary>
    public partial class BlockParserMarkDown : BlockParser {

        public static void Register () {
            var ParseRegistryEntry = new ParseRegistryEntry() {
                Include = Include,
                Parse = Parse
                };

            ParseRegistry.Register(".md", ParseRegistryEntry);
            }

        /// <summary>
        /// Static method for complete document parse.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TagCatalog"></param>
        /// <returns></returns>
        public static Document Parse (string FileName, TagCatalog TagCatalog) {
            var TextReader = new StreamReader(FileName);
            return Parse(TextReader, TagCatalog);
            }

        /// <summary>
        /// Static method for complete document parse.
        /// </summary>
        /// <param name="TextReader"></param>
        /// <param name="TagCatalog"></param>
        /// <returns></returns>
        public static Document Parse (TextReader TextReader, TagCatalog TagCatalog) {

            var Reader = new LexReader(TextReader);
            var Document = new Document();

            Document.Parse(Reader);
            var BlockParser = new BlockParserMarkDown() {
                TagCatalog = TagCatalog,
                Document = Document
                };
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
        public static bool Include (string FileName, TagCatalog TagCatalog,
                        Document Document) {
            var Reader = new LexReader(FileName.OpenTextReader());

            var Included = new Document();
            Included.Parse(Reader);

            var BlockParser = new BlockParserMarkDown() {
                TagCatalog = TagCatalog,
                Document = Document
                };
            BlockParser.ParseMarkDown(Included.Paragraphs);

            return true;
            }


        public void Parse () {
            ParseMarkDown(Document.Paragraphs);
            FinishParse();
            }

        /// <summary>
        /// Partial parse function, does not perform cleanup.
        /// </summary>
        /// <param name="Paragraphs"></param>
        public void ParseMarkDown (List<Paragraph> Paragraphs) {

            foreach (var Paragraph in Paragraphs) {
                AddBlocks(Paragraph);
                }

            }

        /// <summary>
        /// Convert a MarkDown Paragraph into a series of blocks, metadata entries,
        /// etc.
        /// </summary>
        /// <param name="Paragraph"></param>
        void AddBlocks (Paragraph Paragraph) {
            FinishBlock();

            CurrentCatalogEntry = GetCatalogEntry(Paragraph.BlockType, Paragraph.Level);
            if (CurrentCatalogEntry.ElementType == ElementType.Block) {
                MakeElement(CurrentCatalogEntry, null, false);
                }

            ProcessParagraphText(Paragraph.Text);
            }



        // should push this out to the tag catalog itself.
        CatalogEntry GetCatalogEntry (BlockType BlockType, int Level) {
            switch (BlockType) {
                case BlockType.Preformatted: return TagCatalog.FindDefault("pre");
                case BlockType.Heading: return TagCatalog.Defaults[Level];
                case BlockType.Bullet: {
                    return TagCatalog.FindDefault("li");
                    }
                case BlockType.Numbered: return TagCatalog.FindDefault("nli");
                case BlockType.DefinedData: return TagCatalog.FindDefault("dd");
                case BlockType.DefinedTerm: return TagCatalog.FindDefault("dt");
                case BlockType.Paragraph: return TagCatalog.Defaults[0];
                default: return TagCatalog.Defaults[0];
                }
            }

        }
    }