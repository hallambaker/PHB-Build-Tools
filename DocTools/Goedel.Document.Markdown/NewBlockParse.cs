using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.FSR;
using Goedel.IO;

namespace Goedel.Document.Markdown;

// BUG: When parsing using .mdsd files, empty elements occuring immediately inside layout elements are ignored
// e.g. <xl><x4><img="foo.png></xl> produces no output.


/// <summary>
/// The block parser for markdown.
/// </summary>
public partial class BlockParserMarkDown : BlockParser {

    public static void Register() {
        var ParseRegistryEntry = new ParseRegistryEntry() {
            Include = Include,
            Parse = Parse
            };

        ParseRegistry.Register(".md", ParseRegistryEntry);
        ParseRegistry.Register(".mdx", ParseRegistryEntry);
        }

    /// <summary>
    /// Static method for complete document parse.
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="TagCatalog"></param>
    /// <returns></returns>
    public static MarkdownDocument Parse(string FileName, TagCatalog TagCatalog) {
        var TextReader = new StreamReader(FileName);
        return Parse(TextReader, TagCatalog);
        }

    /// <summary>
    /// Static method for complete document parse.
    /// </summary>
    /// <param name="TextReader"></param>
    /// <param name="TagCatalog"></param>
    /// <returns></returns>
    public static MarkdownDocument Parse(TextReader TextReader, TagCatalog TagCatalog) {

        var Reader = new LexReader(TextReader);
        var Document = new MarkdownDocument();

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
    public static bool Include(string FileName, TagCatalog TagCatalog,
                    MarkdownDocument Document) {
        CatalogEntry CatalogEntryTable = TagCatalog.Find("table");
        CatalogEntry CatalogEntryTableRow = TagCatalog.Find("tablerow");
        CatalogEntry CatalogEntryTableCell = TagCatalog.Find("tablecell");

        var documentBlocks = Document.Blocks;
        var newBlocks = new List<Block>();
        Document.Blocks = newBlocks;
        // todo expand path here
        var FilePath = Path.Combine(Document.Path, FileName);

        var Reader = new LexReader(FilePath.OpenTextReader());

        var Included = new MarkdownDocument();
        Included.Parse(Reader);

        var BlockParser = new BlockParserMarkDown() {
            TagCatalog = TagCatalog,
            Document = Document
            };
        BlockParser.ParseMarkDown(Included.Paragraphs);

        Block table = null;
        TextSegmentOpen tableRow = null;
        foreach (var block in newBlocks) {


            switch (block.CatalogEntry.Key) {
                case "table": {
                        if (block is Layout) {
                            table = Block.MakeBlock(CatalogEntryTable, null);
                            documentBlocks.Add(table);
                            }
                        break;
                        }
                case "tablerow":
                case "tr": {
                        if (block is Layout) {
                            if (table != null) {
                                if (tableRow != null) {
                                    table.AddSegmentClose(tableRow);
                                    }

                                tableRow = table.AddSegmentOpen(CatalogEntryTableRow, null);
                                }
                            }
                        break;
                        }
                case "th":
                case "td":
                case "tablecell": {
                        if (tableRow != null) {
                            var OpenCell = table.AddSegmentOpen(CatalogEntryTableCell, null);
                            table.AddSegmentText(block.Text);
                            table.AddSegmentClose(OpenCell);
                            }

                        break;
                        }
                default: {
                        if (tableRow != null) {
                            table.AddSegmentClose(tableRow);
                            tableRow = null;
                            }

                        table = null;

                        documentBlocks.Add(block);
                        break;
                        }
                }
            }
        if (tableRow != null) {
            table.AddSegmentClose(tableRow);
            tableRow = null;
            }

        Document.Blocks = documentBlocks;

        return true;
        }


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
    /// Convert a MarkDown Paragraph into a series of blocks, metadata entries,
    /// etc.
    /// </summary>
    /// <param name="Paragraph"></param>
    void AddBlocks(Paragraph Paragraph) {
        FinishBlock();

        CurrentCatalogEntry = GetCatalogEntry(Paragraph.BlockType, Paragraph.Level);
        if (CurrentCatalogEntry.ElementType == ElementType.Block) {
            MakeElement(CurrentCatalogEntry, null, false);
            }

        ProcessParagraphText(Paragraph.Text);
        }



    // should push this out to the tag catalog itself.
    CatalogEntry GetCatalogEntry(BlockType BlockType, int Level) {
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
