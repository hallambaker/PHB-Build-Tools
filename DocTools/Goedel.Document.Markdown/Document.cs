using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Goedel.FSR;
using Goedel.IO;

namespace Goedel.Document.Markdown {
    public class DocumentSet {

        public string Name = ".";
        public string PartPath = "";

        public DocumentSet Root;
        public DocumentSet Parent;

        public List<MarkdownDocument> Documents = new();
        public List<DocumentSet> Directories = new();

        public List<Resource> Resources = new();
        public TagCatalog TagCatalog;

        // build the top level
        public DocumentSet(string Path, TagCatalog TagCatalog) {
            Root = this;
            Parent = null;
            this.TagCatalog = TagCatalog;

            AddDirectory(Path);
            }

        public DocumentSet(DocumentSet Parent, DirectoryInfo DirectoryInfo, string PartPath) {
            this.Parent = Parent;
            this.Root = Parent.Root;
            this.TagCatalog = Parent.TagCatalog;
            this.PartPath = PartPath;
            AddDirectory(Parent, DirectoryInfo, PartPath);
            }

        public void AddDirectory(string Path) {

            var DirectoryInfo = new DirectoryInfo(Path);
            AddDirectory(Parent, DirectoryInfo, "");
            }


        public void AddDirectory(DocumentSet Parent, DirectoryInfo DirectoryInfo, string PartPath) {
        Console.WriteLine("Path {0} [{1}]", DirectoryInfo.FullName, PartPath);
            Name = DirectoryInfo.Name;

            var EnumerateFiles = DirectoryInfo.EnumerateFiles();
            var EnumerateDirectories = DirectoryInfo.EnumerateDirectories();

            foreach (var FileInfo in EnumerateFiles) {
                string Extension = Path.GetExtension(FileInfo.Name).ToLower();

                if (Extension == ".md") {
                    var Document = new MarkdownDocument(this, FileInfo, TagCatalog);

                    if (Document.IsIndex & (Document.ShortTitle != null)) {
                        Name = Document.ShortTitle;
                        }
                    Documents.Add(Document);
                    }

                else {
                    //Only processing Markdown right now.
                    MarkdownDocument Document = null;

                    if (TagCatalog.DocumentProcess != null) {
                        Document = TagCatalog.DocumentProcess(this, FileInfo, TagCatalog);
                        }

                    if (Document != null) {
                        Documents.Add(Document);
                        }
                    else {
                        var Resource = new Resource(this, FileInfo);
                        Resources.Add(Resource);
                        }
                    }

                }

            foreach (var SubDirectoryInfo in EnumerateDirectories) {
                Directories.Add(new DocumentSet(this, SubDirectoryInfo,
                            PartPath + @"\" + SubDirectoryInfo.Name));
                }
            }

        public void MakeFiles(string RootPath) {
            string TargetDir = RootPath + PartPath;

            Console.WriteLine("Make files in {0}", TargetDir);
            if (!Directory.Exists(TargetDir)) {
                Directory.CreateDirectory(TargetDir);
                }

            foreach (var Document in Documents) {
                TagCatalog.DefaultFormat.MakeHTML(Document, TargetDir);
                }

            foreach (var Resource in Resources) {
                if (TagCatalog?.Process != null) {
                    TagCatalog?.Process(Resource.FullName, TargetDir);
                    }
                }

            foreach (var Dir in Directories) {
                Dir.MakeFiles(RootPath);
                }
            }



        }

    public enum BlockType {
        Paragraph       = 0,
        Heading         = 1,
        DefinedTerm     = 2,
        DefinedData     = 3,
        Bullet          = 4,
        Numbered        = 5,
        Meta            = 6,
        Preformatted    = 7
        }






    public abstract class TextSegment {
        string tag;

        public CatalogEntry CatalogEntry = null;
        public string Tag {
            get => tag ?? CatalogEntry?.Key;
            set => tag = Normalize(value);
            }

        public List<TagValue> Attributes = null;

        public TextSegment() {
            }

        public TextSegment(CatalogEntry catalogEntry, List<TagValue> attributes) {
            Attributes = attributes;
            CatalogEntry = catalogEntry;
            }


        public string AttributeValue(string tag) {
            if (Attributes == null) {
                return null;
                }

            foreach (var tagValue in Attributes) {
                if (tagValue.Tag == tag) {
                    return tagValue.Value;
                    }
                }

            return null;
            }


        string Normalize(string Tag) {

            switch (Tag.ToLower()) {
                case "i": {
                    return "em";
                    }
                case "b": {
                    return "strong";
                    }

                case "a":
                case "norm":
                case "info": {
                    return Tag.ToLower();
                    }

                default: {
                    return Tag;
                    }
                }

            }
        }


    public class TextSegmentText : TextSegment {
        public string Text;


        public TextSegmentText(string Text) => this.Text = Text;



        }

    public class TextSegmentOpen : TextSegment {
        public TextSegment Close = null;
        public string Text;
        public bool IsEmpty = false;
        public bool IsInvisible = false;
        public TextSegmentOpen () {
            }

        public TextSegmentOpen(CatalogEntry CatalogEntry, List<TagValue> Attributes)
            : base(CatalogEntry, Attributes) {
            }
        }

    public class TextSegmentEmpty : TextSegment {
        public TextSegmentEmpty() {
            }

        public TextSegmentEmpty(CatalogEntry CatalogEntry, List<TagValue> Attributes)
            : base(CatalogEntry, Attributes) {
            }
        }

    public class TextSegmentClose : TextSegment {
        public TextSegmentOpen Open = null;
        public TextSegmentClose(TextSegmentOpen Open){
            if (Open != null) {
                Open.Close = this;
                this.Open = Open;
                this.CatalogEntry = Open.CatalogEntry;
                }
            }
        }
    public partial class Resource {
        FileInfo FileInfo;
        public string Name = "";
        public string Path = "";

        public DocumentSet Root = null;
        public DocumentSet Parent = null;

        public string Extension;

        public string FullName => FileInfo.FullName; 

        public Resource() {
            }

        public Resource(FileInfo FileInfo) {
            this.FileInfo = FileInfo;
            Path = FileInfo.DirectoryName;
            Name = FileInfo.Name;
            Extension = System.IO.Path.GetExtension(Name).ToLower();
            }

        public Resource(DocumentSet Parent, FileInfo FileInfo)
            : this(FileInfo) {
            if (Parent != null) {
                this.Root = Parent.Root;
                this.Parent = Parent;
                }
            }

        }

    public partial class Block {
        public BlockType BlockType;
        public string Text = "";

        public List<TagValue> Attributes;
        public List<TextSegment> Segments = new();
        public CatalogEntry CatalogEntry = null;

        public override string ToString() => CatalogEntry?.ToString() ?? "Unknown block";


        public string AttributeValue(string key) {
            key = key.ToLower();
            if (Attributes != null) {
                foreach (var attribute in Attributes) {
                    if (attribute.Tag.ToLower() == key) {
                        return attribute.Value;
                        }
                    }
                }
            return null;
            }

        public TextSegmentOpen AddSegmentOpen(CatalogEntry CatalogEntry, List<TagValue> Attributes) {
            var TextSegment = new TextSegmentOpen(CatalogEntry, Attributes);
            Segments.Add(TextSegment);
            return TextSegment;
            }

        public TextSegmentClose AddSegmentClose(TextSegmentOpen Open) {
            var TextSegment = new TextSegmentClose(Open);
            Segments.Add(TextSegment);
            return TextSegment;
            }

        public TextSegmentText AddSegmentText(string Text) {
            this.Text += Text;
            var TextSegment = new TextSegmentText(Text);
            Segments.Add(TextSegment);
            return TextSegment;
            }

        public TextSegmentEmpty AddSegmentEmpty(CatalogEntry CatalogEntry, List<TagValue> Attributes) {
            var TextSegment = new TextSegmentEmpty(CatalogEntry, Attributes);
            Segments.Add(TextSegment);
            return TextSegment;
            }

        public bool Match(string Key) {
            if (CatalogEntry == null) {
                return false;
                }
            return CatalogEntry.Key == Key;
            }

        public void Format(HTMLWriter HTMLWriter) {
            }

        public static Block MakeBlock(CatalogEntry CatalogEntry,
                    List<TagValue> Attributes) {
            Block NewBlock = null;
            // Locate the tag in the catalog

            if (CatalogEntry == null) {
                NewBlock = new Paragraph();
                }
            else {
                if (CatalogEntry.ElementType == ElementType.Meta) {
                    NewBlock = new Meta();
                    }
                if (CatalogEntry.ElementType == ElementType.Layout) {
                    NewBlock = new Layout();
                    }
                if (CatalogEntry.ElementType == ElementType.Block) {
                    NewBlock = new Paragraph();
                    }
                }

            if (NewBlock == null) { return null; }

            // Initialize the common parameters
            NewBlock.CatalogEntry = CatalogEntry;
            NewBlock.Attributes = Attributes;

            return NewBlock;
            }

        }

    public partial class Meta : Block {
        public Dictionary<string, List<Meta>> Children;
        
        public Meta() {
            }
        
        public Meta(CatalogEntry CatalogEntry, List<TagValue> Attributes) {
            this.CatalogEntry = CatalogEntry;
            this.Attributes = Attributes;
            }

        }

    public partial class Layout : Block {
        }

    public partial class Close : Block {
        public Block Parent;

        public Close(Block Parent) {
            this.Parent = Parent;
            this.CatalogEntry = Parent.CatalogEntry;
            }
        }

    public partial class Heading : Block {

        public List<Heading> SubHeadings;
        }

    public partial class ListItem : Block {

        public List<Heading> SubHeadings;
        }


    public partial class RawParagraph : Block {
        }

    public partial class Paragraph : Block {
        public string Tag = "";

        public int Level = 1;

         public Paragraph(MarkDownLex Lexer) {
            BlockType = Lexer.BlockType;
            Level = Lexer.Level;
            Text = Lexer.Text;
            }

        public Paragraph() {
            BlockType = BlockType.Paragraph;
            Level = 0;
            }

        }


    /// <summary>
    /// A Document Resource.
    /// </summary>
    public partial class MarkdownDocument : Resource  {

        public Dictionary<string, List<Meta>> MetaData = new();
        public List<Heading> Headings;
        public List<Paragraph> Paragraphs = new();
        public List<Block> Blocks = new();

        public string Title = "";
        public string ShortTitle = null;

        public string SourceFormat = "Markdown";

        public string Link;

        public bool IsIndex = false;


        public MarkdownDocument() {
            }

        public MarkdownDocument(string file) {
            Name = file;
            Link = System.IO.Path.GetFileNameWithoutExtension(Name) + ".html";

            using var Reader = new LexReader(file.OpenTextReader());
            Parse(Reader);
            }

        public MarkdownDocument(FileInfo FileInfo) :
            base(FileInfo) {
            }

        public MarkdownDocument(DocumentSet Parent, FileInfo FileInfo) :
            base(Parent, FileInfo) {
            }


        public MarkdownDocument(FileInfo FileInfo, TagCatalog TagCatalog) :
            base(FileInfo) => Init(FileInfo, TagCatalog);

        public MarkdownDocument(Stream Stream, TagCatalog TagCatalog)
            : base() {
            using var Reader = new LexReader(Stream);
            Init(Reader, TagCatalog);
            }

        public MarkdownDocument(DocumentSet Parent, FileInfo FileInfo, TagCatalog TagCatalog) : 
                    base (Parent, FileInfo) {

            Link = System.IO.Path.GetFileNameWithoutExtension(Name) + ".html";
            if (Name == "index.md") {
                IsIndex = true;
                }

            Init(FileInfo, TagCatalog);
            }

        private void Init(FileInfo FileInfo, TagCatalog TagCatalog) {
            using var Reader = new LexReader(FileInfo.FullName.OpenTextReader());
            Init(Reader, TagCatalog);
            }


        private void Init(LexReader Reader, TagCatalog TagCatalog) {
            Parse(Reader);  // Pre parse the source file into blocks

            var BlockParser = new BlockParserMarkDown() {
                TagCatalog = TagCatalog,
                Document = this
                };
            BlockParser.Parse(); // Convert blocks into paragraph entries.
            }


        public List<string> MetaDataGetStrings (string Key, List<string> Default) {
            var Found = MetaDataLookup(Key, out var Items);
            if (!Found) {
                return Default;
                }

            var Result = new List<string>();
            foreach (var Item in Items) {
                Result.Add(Item.Text);
                }

            return Result;
            }


        public string MetaDataGetString(string Key, string Default) {

            var Found = MetaDataLookup(Key, out var Items);

            return Found ? Items[0].Text : Default;
            }


        public bool MetaDataLookup(CatalogEntry Key, out List<Meta> Items) => MetaDataLookup(Key.Key, out Items);

        public bool MetaDataLookup(string Key, out List<Meta> Items) => MetaData.TryGetValue(Key, out Items);

        public List<Meta> MetaDataAdd(CatalogEntry Key, Meta Item) {
            if (Key.Parent == null) {
                return MetaDataAdd(MetaData, Key.Key, Item);
                }
            else {
                if (!MetaData.TryGetValue(Key.Parent.Key, out var Parent)) {
                    Parent = new List<Meta>();
                    MetaData.Add(Key.Parent.Key, Parent);
                    Parent.Add(new Meta());
                    }
                var Last = Parent[Parent.Count - 1];
                if (Last.Children == null) {
                Last.Children = new Dictionary<string, List<Meta>>();
                    }

                return MetaDataAdd(Last.Children, Key.Key, Item);
                }

            }

        public List<Meta> MetaDataAdd(Dictionary<string, List<Meta>> MetaData,
                        string Key, Meta Item) {
            var Found = MetaDataLookup(Key, out var Items);
            if (!Found) {
                Items = new List<Meta>();
                MetaData.Add(Key, Items);
                }
            Items.Add(Item);
            return Items;
            }



        }
    }
