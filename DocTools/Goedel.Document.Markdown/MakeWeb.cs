using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Goedel.Document.Markdown {

    enum ListType {
        Bullet,
        Number,
        Definition,
        NULL
        }


    class TagStack {
        HTMLWriter HTMLWriter;
        Stack<ListType> Stack = new();

        public TagStack(HTMLWriter HTMLWriter) => this.HTMLWriter = HTMLWriter;

        public void Set(ListType ListType, int Level) {
            Level = Level > 6 ? 6 : Level; // Cap at six to prevent attacks

            if (Level == Stack.Count) {
                if (Level != 0) {
                    if (Stack.Peek() != ListType) {
                        // Right level, wrong list type
                        Pop();
                        Push(ListType);
                        }
                    }
                // Do Nothing, already at right level
                }
            else if (Level > Stack.Count) {
                while (Level > Stack.Count) {
                    Push(ListType);
                    }
                }
            else {
                while (Level < Stack.Count) {
                    Pop();
                    }
                }
            }

        public void Clear() => Set(ListType.NULL, 0);

        void Push(ListType ListType) {
            Stack.Push (ListType);
            switch (ListType) {
                case ListType.Bullet: {
                        HTMLWriter.WriteLine("  <ul>");
                        break;
                        }
                case ListType.Number: {
                        HTMLWriter.WriteLine("  <ol>");
                        break;
                        }
                case ListType.Definition: {
                        HTMLWriter.WriteLine("  <dl>");
                        break;
                        }
                }
            }

        void Pop() {
            if (Stack.Count <1) {
                return;
                }
            var ListType = Stack.Pop ();

            switch (ListType) {
                case ListType.Bullet: {
                        HTMLWriter.WriteLine("  </ul>");
                        break;
                        }
                case ListType.Number: {
                        HTMLWriter.WriteLine("  </ol>");
                        break;
                        }
                case ListType.Definition: {
                        HTMLWriter.WriteLine("  </dl>");
                        break;
                        }
                }
            }


        }


    public class FormatHTML {

        public string Language = "en";
        public string[] Header = {};
        public string[] Trailer = {};

        public string[] ParagraphsStart = { };
        public string[] ParagraphsEnd = { };


        public FormatHTML(Tags.Format Format) => ReadFormat(Format);

        public FormatHTML (TagCatalog TagCatalog) {

            
            }

        void ReadFormat (Tags.Format  Format) {
            foreach (var Item in Format.Entries.Where (x => x as Tags.Entry != null)) {
                var Entry = Item as Tags.Entry;

                switch (Entry.Id.ToString()) {
                    case "Header": {
                        Header = Bind(Entry);
                        break;
                        }
                    case "Trailer": {
                        Trailer = Bind(Entry);
                        break;
                        }
                    case "NavStart": {
                        NavStart = Bind(Entry);
                        break;
                        }
                    case "NavEnd": {
                        NavEnd = Bind(Entry);
                        break;
                        }
                    case "NavRoot": {
                        NavRoot = Bind(Entry);
                        break;
                        }
                    case "NavParent": {
                        NavParent = Bind(Entry);
                        break;
                        }
                    case "NavEntryStart": {
                        NavEntryStart = Bind(Entry);
                        break;
                        }
                    case "NavEntryEnd": {
                        NavEntryEnd = Bind(Entry);
                        break;
                        }
                    case "NavEntry": {
                        NavEntry = Bind(Entry);
                        break;
                        }
                    case "NavEntryActive": {
                        NavEntryActive = Bind(Entry);
                        break;
                        }
                    case "ParagraphsStart": {
                        ParagraphsStart = Bind(Entry);
                        break;
                        }
                    case "ParagraphsEnd": {
                        ParagraphsEnd = Bind(Entry);
                        break;
                        }
                    }

                }
            }


        string[] Bind(Tags.Entry Entry) => Entry.Strings.ToArray();


        public void MakeFiles (DocumentSet DocumentSet, string RootPath) {
            string TargetDir = RootPath + DocumentSet.PartPath;

            Console.WriteLine("Make files in {0}", TargetDir);
            if (!Directory.Exists(TargetDir)) {
                Directory.CreateDirectory(TargetDir);
                }


            foreach (var Document in DocumentSet.Documents) {
                MakeHTML(Document, TargetDir);
                }

            foreach (var Resource in DocumentSet.Resources) {
                DocumentSet.TagCatalog.Process (Resource.FullName, TargetDir);
                }

            foreach (var Dir in DocumentSet.Directories) {
                MakeFiles(Dir, RootPath);
                }
            }

        public void MakeHTML(MarkdownDocument Document, string TargetDir) {
            string TargetFile = TargetDir + @"\" + Document.Link;

            Console.WriteLine("Make file in {0}", TargetFile);
            if (Document.SourceFormat == "Word") {
                Console.WriteLine("From Word!");
                }


            using var HTMLWriter = new HTMLWriter(TargetFile);
            var TagStack = new TagStack(HTMLWriter);

            HTMLWriter.StartHead();
            HTMLWriter.WriteLine("  <title>{0}</title>", Document.Title);
            HTMLWriter.WriteLine("  ", Header);

            HTMLWriter.StartBody();

            if (Document.Root != null) {
                MakeNav(HTMLWriter, Document);
                }

            HTMLWriter.WriteLine("  ", ParagraphsStart);

            foreach (var Block in Document.Blocks) {
                if (Block.GetType() == typeof(Layout)) {

                    WriteStart(HTMLWriter, Block.CatalogEntry, Block.Attributes);
                    }
                else if (Block.GetType() == typeof(Close)) {
                    WriteEnd(HTMLWriter, Block.CatalogEntry);
                    HTMLWriter.WriteLine();
                    }
                else if (Block.GetType() == typeof(Paragraph)) {
                    WriteParagraph(HTMLWriter, (Paragraph)Block);
                    }
                else if (Block.GetType() == typeof(Heading)) {
                    WriteStart(HTMLWriter, Block.CatalogEntry, Block.Attributes);
                    WriteText(HTMLWriter, Block);
                    WriteEnd(HTMLWriter, Block.CatalogEntry);
                    }
                }


            TagStack.Clear();
            HTMLWriter.WriteLine("  ", ParagraphsEnd);
            foreach (var s in Trailer) {
                HTMLWriter.Write("  ");
                HTMLWriter.WriteLine(s);
                }
            }


        void WriteParagraph(HTMLWriter HTMLWriter, Paragraph Block) {
            WriteStart(HTMLWriter, Block.CatalogEntry, Block.Attributes);
            WriteText(HTMLWriter, Block);
            WriteEnd(HTMLWriter, Block.CatalogEntry);
            HTMLWriter.WriteLine();
            }


        void WriteStart(HTMLWriter HTMLWriter,
                    CatalogEntry CatalogEntry,
                    List<TagValue> Attributes) => WriteStart(HTMLWriter, CatalogEntry, Attributes, false);

        void WriteStart(HTMLWriter HTMLWriter,
                    CatalogEntry CatalogEntry,
                    List<TagValue> Attributes, bool Empty) {

            if (CatalogEntry.Start != null) {
                if ((Attributes != null) && (Attributes.Count > 0) &&
                        (Attributes[0].Value != null) && (Attributes[0].Value.Length > 0)) {
                    HTMLWriter.Write(CatalogEntry.Start1,
                        Attributes[0].Value);
                    }
                else {
                    HTMLWriter.Write(CatalogEntry.Start);
                    }
                }
            if (CatalogEntry.XMLTag != null) {
                HTMLWriter.Write("<");
                HTMLWriter.Write(CatalogEntry.XMLTag);

                foreach (var TV in CatalogEntry.XMLDefaults) {
                    HTMLWriter.Write(" ");
                    HTMLWriter.Write(TV.Tag);
                    HTMLWriter.Write("=\"");
                    HTMLWriter.Write(TV.Value);
                    HTMLWriter.Write("\"");
                    }

                if ((Attributes != null) && (Attributes.Count > 0)) {
                    if ((Attributes[0].Value != null) && (Attributes[0].Value.Length > 0)) {
                        HTMLWriter.Write(" ");
                        HTMLWriter.Write(CatalogEntry.XMLFirst);
                        HTMLWriter.Write("=\"");
                        HTMLWriter.Write(Attributes[0].Value);
                        HTMLWriter.Write("\"");
                        }
                    for (int i = 1; i < Attributes.Count; i++) {
                        HTMLWriter.Write(" ");
                        HTMLWriter.Write(Attributes[i].Tag);
                        HTMLWriter.Write("=\"");
                        HTMLWriter.Write(Attributes[i].Value);
                        HTMLWriter.Write("\"");
                        }
                    }

                if (Empty) {
                    HTMLWriter.Write("/>");
                    }
                else {
                    HTMLWriter.Write(">");
                    }
                }
            }

        void WriteEnd(HTMLWriter HTMLWriter, CatalogEntry CatalogEntry) {
            if (CatalogEntry.XMLTag != null) {
                HTMLWriter.Write("</");
                HTMLWriter.Write(CatalogEntry.XMLTag);
                HTMLWriter.Write(">");
                }
            if (CatalogEntry.End != null) {
                HTMLWriter.Write(CatalogEntry.End);
                }
            }

        void WriteText(HTMLWriter HTMLWriter, Block Block) {
            foreach (var Segment in Block.Segments) {
                if (Segment.GetType() == typeof(TextSegmentText)) {
                    var SegmentT = (TextSegmentText) Segment;
                    HTMLWriter.WriteEscape(SegmentT.Text);
                    }
                if (Segment.GetType() == typeof(TextSegmentOpen)) {
                    var SegmentT = (TextSegmentOpen) Segment;
                    WriteStart(HTMLWriter, SegmentT.CatalogEntry, SegmentT.Attributes);
                    }
                if (Segment.GetType() == typeof(TextSegmentEmpty)) {
                    var SegmentT = (TextSegmentEmpty) Segment;
                    WriteStart(HTMLWriter, SegmentT.CatalogEntry, SegmentT.Attributes, true);
                    //WriteEnd(HTMLWriter, SegmentT.CatalogEntry);
                    }
                if (Segment.GetType() == typeof(TextSegmentClose)) {
                    var SegmentT = (TextSegmentClose) Segment;
                    WriteEnd(HTMLWriter, SegmentT.CatalogEntry);
                    }                
                }
            }


        //public void MakeHTML_Old (Document Document, string TargetDir) {
        //    string TargetFile = TargetDir + @"\" + Document.Link;

        //    Console.WriteLine("Make file in {0}", TargetFile);

            

        //    using (var HTMLWriter = new HTMLWriter(TargetFile)) {
        //        var TagStack = new TagStack(HTMLWriter);

        //        HTMLWriter.StartHead();
        //        HTMLWriter.WriteLine("  <title>{0}</title>", Document.Title);
        //        HTMLWriter.WriteLine("  ", Header);

        //        HTMLWriter.StartBody();

        //        if (Document.Root != null) {
        //            MakeNav(HTMLWriter, Document);
        //            }

        //        HTMLWriter.WriteLine("  ", ParagraphsStart);

        //        foreach (var Paragraph in Document.Paragraphs) {
        //            if (Paragraph.BlockType == BlockType.Paragraph) {
        //                TagStack.Clear();
        //                HTMLWriter.WriteLine("  <p>{0}</p>", Paragraph.Text);
        //                }
        //            else if (Paragraph.BlockType == BlockType.Heading) {
        //                TagStack.Clear();
        //                int Level = Paragraph.Level < 6 ? Paragraph.Level + 1 : 6;
        //                HTMLWriter.WriteLine("  <h{1}>{0}</h{1}>", Paragraph.Text, Level);
        //                }
        //            else if (Paragraph.BlockType == BlockType.DefinedTerm) {
        //                TagStack.Set(ListType.Definition, Paragraph.Level);
        //                HTMLWriter.WriteLine("  <dt>{0}</dt>", Paragraph.Text);
        //                }
        //            else if (Paragraph.BlockType == BlockType.DefinedData) {
        //                TagStack.Set(ListType.Definition, Paragraph.Level);
        //                HTMLWriter.WriteLine("  <dd>{0}</dd>", Paragraph.Text);
        //                }
        //            else if (Paragraph.BlockType == BlockType.Bullet) {
        //                TagStack.Set(ListType.Bullet, Paragraph.Level);
        //                HTMLWriter.WriteLine("  <li>{0}</li>", Paragraph.Text);
        //                }
        //            else if (Paragraph.BlockType == BlockType.Numbered) {
        //                TagStack.Set(ListType.Number, Paragraph.Level);
        //                HTMLWriter.WriteLine("  <li>{0}</li>", Paragraph.Text);
        //                }
        //            HTMLWriter.WriteLine();
        //            }
        //        TagStack.Clear();
        //        HTMLWriter.WriteLine("  ", ParagraphsEnd);
        //        foreach (var s in Trailer) {
        //            HTMLWriter.Write("  ");
        //            HTMLWriter.WriteLine(s);
        //            }
        //        }
        //    }

        public string[] NavStart = {};
        public string[] NavEnd = {};
        public string[] NavRoot = {};
        public string[] NavParent = {};
        public string[] NavEntryStart = {};
        public string[] NavEntry = {};
        public string[] NavEntryActive = {};
        public string[] NavEntryEnd = {};

        public void MakeNav (HTMLWriter HTMLWriter, MarkdownDocument Document) {

            Console.WriteLine();
            Console.WriteLine("Make Navigator {0}", Document.Name);
            Console.WriteLine("Root: {0}", Document.Root.Name);

            HTMLWriter.WriteLine(NavStart);
            HTMLWriter.WriteLine(NavParent, "/", Document.Root.Name);

            if (Document.Parent != Document.Root) {
                Console.WriteLine("Parent: {0}", Document.Parent.Name);
                HTMLWriter.WriteLine(NavParent, Document.Parent.PartPath, Document.Parent.Name);
                }

            HTMLWriter.WriteLine(NavEntryStart);

            bool IsFlat = true;
            foreach (var DocumentSet in Document.Parent.Directories) {
                Console.WriteLine("Directory: {0}", DocumentSet.Name);
                HTMLWriter.WriteLine(NavEntry, DocumentSet.PartPath, DocumentSet.Name);
                IsFlat = false;
                }

            // If we can't find any sub directories then provide navigation 
            // for this level.
            if (IsFlat) {
                foreach (var iDocument in Document.Parent.Documents) {
                    Console.WriteLine("Directory: {0}", iDocument.Name);
                    if (iDocument.IsIndex) {
                        }
                    else if (iDocument == Document) {
                        HTMLWriter.WriteLine(NavEntryActive, iDocument.Link, iDocument.ShortTitle);
                        }
                    else {
                        HTMLWriter.WriteLine(NavEntry, iDocument.Link, iDocument.ShortTitle);
                        }
                    }
                }

            HTMLWriter.WriteLine(NavEntryEnd);
            HTMLWriter.WriteLine(NavEnd);

            }
        }
    }
