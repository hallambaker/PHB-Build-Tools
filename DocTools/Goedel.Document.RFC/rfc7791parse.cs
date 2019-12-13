using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Goedel.Document.RFC;

namespace Goedel.Document.RFC {
    public class Rfc7991Parse {

        Goedel.Document.RFC.Document Document;
        TextReader TextReader;

        public static void Parse(string File, Goedel.Document.RFC.Document Document) {
            using (FileReader FileReader = new FileReader(File)) {
                Parse(FileReader, Document);
                }
            }

        public static void Parse(TextReader TextReader, Goedel.Document.RFC.Document Document) => new Rfc7991Parse(TextReader, Document);


        public Rfc7991Parse(TextReader TextReader, Goedel.Document.RFC.Document Document) {
            this.TextReader = TextReader;
            this.Document = Document;

            Parse ();
            }


        public void Parse() {

            XmlRootAttribute xRoot = new XmlRootAttribute() {
                ElementName = "rfc"
                };
            //xRoot.Namespace = "http://tempuri.org/rfc2629";
            //xRoot.IsNullable = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://tempuri.org/rfc2629");

            XmlSerializer XmlSerializer = new XmlSerializer (typeof (rfc));

            rfc rfc = (rfc) XmlSerializer.Deserialize (TextReader);
            Document.Number = rfc.number;
            Document.Obsoletes = rfc.obsoletes;
            Document.Updates = rfc.updates;
            Document.Category = rfc.category;
            Document.Consensus = rfc.consensus.ToString();
            Document.SeriesNumber = rfc.seriesNo;
            Document.Ipr = rfc.ipr;
            Document.IprExtract = rfc.iprExtract;
            Document.SubmissionType = rfc.submissionType.ToString();
            Document.Docname = rfc.docName;

            Document.Version = null; // is part of the Docname in rfc2629 format

            if (rfc.front != null) {
                var front = rfc.front;
                Document.Title = front.title?.Value;
                Document.TitleAbrrev = front.title?.abbrev;
                Document.TitleAscii = front.title?.ascii;

                Document.SeriesInfos = MakeSeriesInfo(front.seriesInfo);

                Document.Authors = MakeAuthors(front.author);
                Document.Day = rfc.front.date.day;
                Document.Month = rfc.front.date.month;
                Document.Year = rfc.front.date.year;
                Document.Area = MakeString(front.area);
                Document.Workgroup = MakeString(front.workgroup);
                Document.Keywords = MakeKeywords(front.keyword);

                Document.Abstract = MakeTextBlocks(front.@abstract.Items);
                Document.Note = MakeTextBlocks(front.note);
                if (front.boilerplate != null) {
                    Document.Boilerplate = MakeSections(front.boilerplate.section, 2);
                    }
                }

            Document.Middle = MakeSections(rfc.middle.section, 1);

            if (rfc.back != null) {
                var back = rfc.back;

                Document.Back = MakeSections(back.section, 1);
                MakeCatalog(Document.Catalog, back.references);
                }
            }


        List<string> MakeString (asciitext[] Text) {
            if (Text == null) {
                return null;
                }

            var Result = new List<string>();
            foreach (var T in Text) {
                Result.Add(T.Value);
                }
            return Result;
            }


        string MakeString (string[] s) {

            if (s == null) { return null; }
            if (s.Length == 0) {return null; }
            return s[0];
            }

        List<string> MakeKeywords(asciitext[] keyword) {
            List<string> Result = new List<string>();

            if (keyword != null) {
                foreach (var s in keyword) {
                    Result.Add(s.Value);
                    }
                }

            return Result;
            }


        List<Author> MakeAuthors(author[] authors) {
            List<Author> Result = new List<Author> ();

            foreach (author author in authors) {
                Author Author = new Author() {
                    Initials = author.initials,
                    Name = author.fullname
                    };

                if (author.organization != null) {
                    Author.Organization = author.organization.Value;
                    Author.OrganizationAbbrev = author.organization.abbrev;
                    }
              
                Author.Surname = author.surname;

                if (author.address != null) {
                    Author.Phone = author.address.phone?.Value;
                    Author.URI = author.address.uri?.Value;
                    Author.Email = author.address.email?.Value;
                    Author.City = GetAddressAttribute(author.address.postal, "city");
                    Author.Code = GetAddressAttribute(author.address.postal, "code");
                    Author.Country = GetAddressAttribute(author.address.postal, "country");
                    Author.Street = GetAddressAttribute(author.address.postal, "street");
                    }

                Result.Add (Author);
                }

            return Result;
            }


        Reference MakeReference(reference reference) {
            Reference Result = new Reference ();

            if (reference.front != null) {
                Result.Title = reference.front.title.Value;
                Result.Abbrev = reference.front.title.abbrev;
                if (reference.front.author != null) {
                    Result.Authors = MakeAuthors (reference.front.author);
                    }
                if (reference.front.date != null) {
                    Result.Day = reference.front.date.day;
                    Result.Month = reference.front.date.month;
                    Result.Year = reference.front.date.year;
                    }
                Result.Area = MakeString(reference.front.area);
                Result.Workgroup = MakeString(reference.front.workgroup);
                Result.Keywords = MakeKeywords (reference.front.keyword);
                // do nothing with the note field
                }

            Result.SeriesInfos = MakeSeriesInfo (reference.Items);
            Result.Formats = MakeFormats (reference.Items);


            Result.GeneratedID = reference.anchor;
            Result.Target = reference.target;

            return Result;
            }

        List<SeriesInfo> MakeSeriesInfo (seriesInfo[] seriesInfos) {
            List<SeriesInfo> ListSeriesInfo = new List<SeriesInfo>();

            if (seriesInfos != null) {
                foreach (var seriesInfo in seriesInfos) {

                    SeriesInfo SeriesInfo = new SeriesInfo();
                    ListSeriesInfo.Add(SeriesInfo);
                    SeriesInfo.Name = seriesInfo.name;
                    SeriesInfo.Value = seriesInfo.value;
                    break;
                    }
                }
            return ListSeriesInfo;

            }

        List<SeriesInfo> MakeSeriesInfo(object[] seriesInfos) {
            List<SeriesInfo> ListSeriesInfo = new List<SeriesInfo> ();

            if (seriesInfos != null) {
                foreach (var obj in seriesInfos) {
                    switch (obj) {
                        case seriesInfo seriesInfo: {
                            SeriesInfo SeriesInfo = new SeriesInfo();
                            ListSeriesInfo.Add(SeriesInfo);
                            SeriesInfo.Name = seriesInfo.name;
                            SeriesInfo.Value = seriesInfo.value;
                            break;
                            }
                        }
                    }
                }


            return ListSeriesInfo;
            }

        List<Format> MakeFormats (object[] formats) {
            List<Format> ListFormats = new List<Format>();

            if (formats != null) {
                foreach (var obj in formats) {

                    switch (obj) {
                        case format format: {
                            Format Format = new Format();
                            ListFormats.Add(Format);
                            Format.Octets = format.octets;
                            Format.Target = format.target;
                            Format.Type = format.type;
                            break;
                            }
                        }
                    }
                }
            return ListFormats;
            }



        void MakeCatalog (Catalog Catalog, references[] referencesArray) {
            if (referencesArray != null) {
                foreach (references references in referencesArray) {
                    References References = new References();
                    Catalog.ReferenceSections.Add(References);
                    References.Title = references.title;

                    if (references.Items != null) {
                        foreach (var obj in references.Items) {
                            switch (obj) {

                                case reference reference:

                                    References.Entries.Add(MakeReference(reference));
                                    break;
                                }
                            }
                        }
                    }
                }
            }


        void FillTextBlock (object[] Items, List<TextBlock> TextBlocks) {
            foreach (object o in Items) {
                if (o.GetType() == typeof(figure)) {
                    AddFigureBlock(TextBlocks, (figure)o);
                    }
                else if (o.GetType() == typeof(iref)) {
                    AddIndex(TextBlocks, (iref)o);
                    }
                else if (o.GetType() == typeof(t)) {
                    AddListBlocks(TextBlocks, (t)o);
                    }
                else if (o.GetType() == typeof(texttable)) {
                    AddTableBlock(TextBlocks, (texttable)o);
                    }

                // ToDo: add in all new textblock like things
                }
            }

        List<TextBlock> MakeTextBlocks (note[] Notes) {
            if (Notes == null) {
                return null;
                }

            List<TextBlock> Result = new List<TextBlock>();

            foreach (var Note in Notes) {
                FillTextBlock(Note.Items, Result);
                }
            return Result;
            }


        List<TextBlock> MakeTextBlocks(object[] Items) {
            List<TextBlock> Result = new List<TextBlock>();

            FillTextBlock(Items, Result);

            return Result;
            }

        List<TextBlock> MakeTextBlocks (section section) {
            List<TextBlock> Result = new List<TextBlock>();

            FillTextBlock(section.Items, Result);

            return Result;
            }


        void MakeTextBlockT(TextBlockSequenceBuilder builder, t source) {
            var block = new P() {
                Segments = new List<Markdown.TextSegment>()
                };

            builder.AddBlock(block, block.Segments);
            AddTextBlocks(builder, source.Items, source.ItemsElementName);
            }

        void MakeTextBlock (TextBlockSequenceBuilder builder, ol source) {
            builder.ListLevel++;
            if (source.li != null) {

                if (!Int32.TryParse(source.start, out int index)) {
                    index = 1;
                    }


                //var listBuilder = new TextBlockSequenceBuilder();
                //block.Items = listBuilder.Blocks;
                    foreach (var li in source.li) {
                    var blockLi = new LI() {
                        Level = builder.ListLevel,
                        Index = index,
                        Format = source.type,
                        Segments = new List<Markdown.TextSegment>(),
                        Type = BlockType.Symbol
                        };

                    builder.AddBlock(blockLi, blockLi.Segments);
                    AddTextBlocks(builder, li.Items, li.ItemsElementName);
                    }
                }
            builder.ListLevel--;

            }

        void MakeTextBlock(TextBlockSequenceBuilder builder, ul source) {
            builder.ListLevel++;
            if (source.li != null) {
                //var listBuilder = new TextBlockSequenceBuilder();
                //block.Items = listBuilder.Blocks;
                foreach (var li in source.li) {
                    var blockLi = new LI() {
                        Level = builder.ListLevel,
                        Segments = new List<Markdown.TextSegment>(),
                        Type = BlockType.Ordered
                        };

                    builder.AddBlock(blockLi, blockLi.Segments);
                    AddTextBlocks(builder, li.Items, li.ItemsElementName);
                    }
                }
            builder.ListLevel--;
            }

        void MakeTextBlock(TextBlockSequenceBuilder builder, dl source) {
            builder.ListLevel++;
            if (source.Items != null) {
                //var listBuilder = new TextBlockSequenceBuilder();
                //block.Items = listBuilder.Blocks;

                var index = 0;
                foreach (var li in source.Items) {
                    var lit = li as listItemType;
                    var tag = source.ItemsElementName[index++];
                    BlockType blockType = BlockType.Paragraph;
                    switch (tag) {
                        case ItemsChoiceTypeDL.dt: {
                            blockType = BlockType.Term;
                            break;
                            }
                        case ItemsChoiceTypeDL.dd: {
                            blockType = BlockType.Data;
                            break;
                            }
                        }

                    var blockLi = new LI() {
                        Level = builder.ListLevel,
                        Segments = new List<Markdown.TextSegment>(),
                        Type = blockType
                        };

                    builder.AddBlock(blockLi, blockLi.Segments);
                    AddTextBlocks(builder, lit.Items, lit.ItemsElementName);
                    }
                }
            builder.ListLevel--;
            // see above
            }

        void MakeTextBlock(TextBlockSequenceBuilder builder, list source) {
            builder.ListLevel++;
            builder.ListLevel--;
            // see above
            }
        void MakeTextBlock(TextBlockSequenceBuilder builder, table source) {

            var block = new Table() {
                };

            // ToDo: name, iref, anchor

            MakeTableRow(block, source.thead);
            foreach (var row in source.tbody) {
                MakeTableRow(block, row);
                }
            MakeTableRow(block, source.tfoot);

            builder.AddBlock(block, null);

            // see above
            }


        void MakeTableRow(Table table, tableRowsType trtype) {
            if (trtype == null) {
                return;
                }
            if (trtype.tr == null) {
                return;
                }
            var builder = new TextBlockSequenceBuilder();
            foreach (var trs in trtype.tr) {
                foreach (var item in trs.Items) {
                    MakeTableCell(builder, item);
                    }
                }

            }

        void MakeTableCell(TextBlockSequenceBuilder builder, tableCellType cellType) {
            // anchor, colspan, rowspan, align 

            AddTextBlocks(builder, cellType.Items, cellType.ItemsElementName);
            }


        void MakeTextBlock(TextBlockSequenceBuilder builder, texttable source) {
            if (source.ttcol == null) {
                return; // elide empty tables
                }
            var cols = source.ttcol.Length;
            if (cols <= 0) {
                return; // elide headerless tables
                }

            foreach (var ttcol in source.ttcol) {

                // create table element with text ttcol.Value
                }

            int count = 0;
            foreach (var c in source.c) {
                if (count == 0) {
                    // make a new row for the table here.
                    }
                count = (count + 1) % cols;
                // make text run with c.Items, c.ItemsElementName

                }


            // see above
            }
        void MakeTextBlock(TextBlockSequenceBuilder builder, blockquote source) {
            var block = new P() {
                Segments = new List<Markdown.TextSegment>()
                };

            builder.AddBlock(block, block.Segments);
            AddTextBlocks(builder, source.Items, source.ItemsElementName);
            }
        void MakeTextBlock(TextBlockSequenceBuilder builder, sourcecode source) {
            var block = new PRE(source.Value, source.anchor) {
                // name?
                // type?
                // src ?
                };

            builder.AddBlock(block, block.Segments);
            // here need to include the source text or the link...
            }

        void MakeTextBlock(TextBlockSequenceBuilder builder, artwork source) {
            var block = new PRE("TBS", source.anchor) {
                // name?
                // type?
                // src ?
                };

            builder.AddBlock(block, block.Segments);
            // here need to include the source text or the link...
            }
        void MakeTextBlock(TextBlockSequenceBuilder builder, aside source) {
            AddTextBlocks(builder, source.Items, source.ItemsElementName);
            }

        void MakeTextBlock(TextBlockSequenceBuilder builder, figure source) {
            // see above

            var block = new Figure(null, source.anchor) {
                // name?
                // iref?
                // preamble?
                // postamble?
                };

            foreach (var item in source.Items) {
                switch (item) {
                    case artwork artwork: {
                        break;
                        }
                    case sourcecode sourcecode: {
                        break;
                        }
                    }

                }
            builder.AddBlock(block, null);


            }


        void AddTextBlocks(TextBlockSequenceBuilder builder, object[] Items, ItemsChoiceTextRun[] Tags) {


            var index = 0;
            foreach (var item in Items) {
                if (item is string s) {
                    Console.WriteLine($"{item}");
                    builder.AddText(s); 
                    }
                else {
                    var tag = Tags[index++];

                    switch (tag) {
                        case ItemsChoiceTextRun.t: {
                            MakeTextBlockT(builder, item as t);
                            break;
                            }

                        // These tags contain text runs which may recurse.
                        case ItemsChoiceTextRun.em:
                        case ItemsChoiceTextRun.strong:
                        case ItemsChoiceTextRun.sub:
                        case ItemsChoiceTextRun.sup:
                        case ItemsChoiceTextRun.tt: {
                            var textrun = item as textrun;
                            var opener = builder.OpenTextRun(tag);
                            AddTextBlocks(builder, textrun.Items, textrun.ItemsElementName);
                            builder.CloseTextRun(opener);
                            break;
                            }

                        case ItemsChoiceTextRun.bcp14: {
                            var opener = builder.OpenTextRun(tag);
                            builder.AddText((item as Bcp14String).Text);
                            builder.CloseTextRun(opener);
                            break;
                            }


                        case ItemsChoiceTextRun.spanx: {
                            var opener = builder.OpenTextRun(item as spanx);
                            builder.AddText((item as spanx).Text);
                            builder.CloseTextRun(opener);
                            break;
                            }

                        case ItemsChoiceTextRun.eref: {
                            var eref = item as eref;
                            var opener = builder.OpenTextRun(tag, "target", eref.target);
                            builder.AddText(eref.Text);
                            builder.CloseTextRun(opener);
                            break;
                            }


                        case ItemsChoiceTextRun.relref: {
                            var relref = item as relref;
                            var opener = builder.OpenTextRun(tag,
                                "target", relref.target,
                                "displayFormat", relref.displayFormat.ToString(),
                                "section", relref.section,
                                "relative", relref.relative);
                            builder.AddText(relref.Text);
                            builder.CloseTextRun(opener);
                            break;
                            }

                        case ItemsChoiceTextRun.xref: {
                            var xref = item as xref;
                            var opener = builder.OpenTextRun(tag,
                                "target", xref.target,
                                "displayFormat", xref.format.ToString());
                            builder.AddText(xref.Text);
                            builder.CloseTextRun(opener);
                            break;
                            }

                        // cref items are comments and may contain textruns, ref, relref or xref content
                        case ItemsChoiceTextRun.cref: {
                            var cref = item as cref;
                            var opener = builder.OpenTextRun(tag,
                                "anchor", cref.anchor,
                                "source", cref.source,
                                "display", cref.display.ToString());
                            AddTextBlocks(builder, cref.Items, cref.ItemsElementName);
                            builder.CloseTextRun(opener);
                            break;
                            }

                        // Entries that do not contain content
                        case ItemsChoiceTextRun.iref: {
                            var iref = item as iref;
                            builder.TextEmpty(tag, "item", iref.item, "subitem", iref.subitem);
                            break;
                            }


                        // List like entities
                        case ItemsChoiceTextRun.ol: {
                            MakeTextBlock(builder, item as ol);
                            break;
                            }

                        case ItemsChoiceTextRun.ul: {
                            MakeTextBlock(builder, item as ul);
                            break;
                            }

                        case ItemsChoiceTextRun.dl: {
                            MakeTextBlock(builder, item as dl);
                            break;
                            }

                        case ItemsChoiceTextRun.list: {
                            MakeTextBlock(builder, item as list);
                            break;
                            }


                        // Entries yet to be implemented

                        // Table like entities
                        case ItemsChoiceTextRun.table: {
                            MakeTextBlock(builder, item as table);
                            break;
                            }
                        case ItemsChoiceTextRun.texttable: {
                            MakeTextBlock(builder, item as texttable);
                            break;
                            }


                        case ItemsChoiceTextRun.blockquote: {
                            MakeTextBlock(builder, item as blockquote);
                            break;
                            }

                        case ItemsChoiceTextRun.sourcecode: {
                            MakeTextBlock(builder, item as sourcecode);
                            break;
                            }
                        case ItemsChoiceTextRun.figure: {
                            MakeTextBlock(builder, item as figure);
                            break;
                            }
                        case ItemsChoiceTextRun.artwork: {
                            MakeTextBlock(builder, item as artwork);
                            break;
                            }
                        case ItemsChoiceTextRun.aside: {
                            MakeTextBlock(builder, item as aside);
                            break;
                            }

                        case ItemsChoiceTextRun.vspace: {
                            // Vspace is deprecated and we simply elide occurrences.
                            break;
                            }
                        }
                    }
                }
            }



        List<Section> MakeSections(section[] sections, int level) {
            if (level > 6) {
                throw new Exception("Levels nested too deeply, maximum is 6.");
                }

            List<Section> Result = new List<Section>();
            if (sections != null) {
                foreach (section section in sections) {
                    var title = section.title;
                    if (section.name != null) {
                        title = MakeString(section.name.Items);
                        }

                    var outSection = new Section(title, section.anchor);

                    if (section.Items != null) {
                        var builder = new TextBlockSequenceBuilder();
                        outSection.TextBlocks = builder.Blocks;
                        AddTextBlocks(builder, section.Items, section.ItemsElementName);
                        }


                    // Recurse
                    if (section.section1 != null) {
                        outSection.Subsections = MakeSections(section.section1, level + 1);
                        }

                    Result.Add(outSection);
                    }
                }
            return Result;
            }


        ////////////////////////
        // Above is mostly stable

        void AddText(ref string s1, string s2) => s1 = (s1 == null) ? s2 : s1 + s2;


        void MakeP(List<TextBlock> Parent, ref string  Text, ref string ID) {
            if (Text != null) {
                P P = new P (Text, ID);
                ID = null; Text = null;
                Parent.Add (P);
                }
            }

        void MakeListItem(List<TextBlock> Parent, BlockType ListItem,
                        ref string Text, ref string ID, ref string hangtext, int level, ref int Index) {

            if (ListItem == BlockType.Definitions) {
                if (hangtext != null) {
                    LI LI = new LI(hangtext, ID, BlockType.Term, level);
                    ID = null; hangtext = null;
                    Parent.Add(LI);
                    }
                if (Text != null) {
                    LI LI = new LI(Text, ID, BlockType.Data, level);
                    ID = null; Text = null;
                    Parent.Add(LI);
                    }
                }
            else {

                if (Text != null) {
                    LI LI = new LI(Text, ID, ListItem, level, Index++);
                    ID = null; Text = null;
                    Parent.Add(LI);
                    }
                }
            }



        // a single t block can have a series of nested paragraphs.
        void AddListBlocks(List<TextBlock> Parent, t t) {
            if (t.Items != null) {
                string ID = t.anchor;
                string Text = null;

                foreach (object o in t.Items) {
                    if (o.GetType() == typeof (string)) {
                        AddText (ref Text, (string) o);
                        }
                    else if (o.GetType() == typeof(list)) {
                        MakeP (Parent, ref Text, ref ID);
                        //Console.Write("Got list !");
                        AddListBlocks (Parent, (list) o, 0);
                        }
                    else if (o.GetType() == typeof(figure)) {
                        MakeP (Parent, ref Text, ref ID);
                        AddFigureBlock (Parent, (figure) o);
                        }

                    // cref eref iref spanx vspace xref
                    }

                MakeP (Parent, ref Text, ref ID);
                }

            }


        void AddListBlocks(List<TextBlock> Parent, list list, int level) {
            BlockType ListItem;
            int Index = 1;

            if ((list.style == null) || (list.style == "symbols")) {
                ListItem = BlockType.Symbol;
                }
            else if (list.style == "numbers") {
                ListItem = BlockType.Ordered;
                }
            else if (list.style == "hanging") {
                ListItem = BlockType.Definitions;
                }
            else {
                throw new Exception ("List type not supported [" + list.style + "]");
                }

            if ((list.t == null) || list.t.Length == 0) {
                return;
                }
            foreach (t t in list.t) {
                string ID = t.anchor;
                string HangText = t.hangText;
                string Text = null;

                foreach (object o in t.Items) {
                    if (o.GetType() == typeof (string)) {
                        AddText (ref Text, (string) o);
                        }
                    else if (o.GetType() == typeof(list)) {
                        MakeListItem (Parent, ListItem, ref Text, ref ID, ref HangText, level, ref Index);
                        AddListBlocks (Parent, (list) o, level +1);
                        }
                    else if (o.GetType() == typeof(figure)) {
                        MakeListItem (Parent, ListItem, ref Text, ref ID, ref HangText, level, ref Index);
                        AddFigureBlock (Parent, (figure) o);
                        }

                    // cref eref iref spanx vspace xref
                    }

                MakeListItem (Parent, ListItem, ref Text, ref ID, ref HangText, level, ref Index);
                }
            }


        void AddPre (List<TextBlock> Parent, System.Xml.XmlNode[] Texts, string Anchor) {
            //foreach (var Text in Texts) {
            //    PRE PRE = new PRE(Text, Anchor);
            //    Parent.Add(PRE);
            //    }
            }


        void AddFigureBlock(List<TextBlock> Parent, figure figure) {
            foreach (var Item in figure.Items) {
                switch (Item) {
                    case artwork artwork: {
                        AddPre(Parent, artwork.Any, figure.anchor);
                        break;
                        }
                    case sourcecode sourcecode: {
                        break;
                        }
                    }

                }
            }

        void AddTableBlock(List<TextBlock> Parent, texttable texttable) {
            Table Table = new Table() {
                GeneratedID = texttable.anchor
                };
            
            TableRow TableRow = new TableRow();

            if (texttable.ttcol != null) {
                foreach (ttcol ttcol in texttable.ttcol) {
                    TableData item = new TableData() {
                        IsHeading = true,
                        Text = ttcol.Value
                        };
                    TableRow.Data.Add(item);
                    }
                }

            Table.MaxRow = TableRow.Data.Count;
            Table.Rows.Add (TableRow);

            if (texttable.c != null) {
                int col = Table.MaxRow;
                foreach (c c in texttable.c) {
                    TableData item = new TableData() {
                        IsHeading = false,
                        // Text = MakeString(c.Items, c.Text)
                        };

                    if (col >= Table.MaxRow) {
                        col = 0;
                        TableRow = new TableRow();
                        Table.Rows.Add(TableRow);
                        }
                    col++;
                    TableRow.Data.Add(item);
                    }
                }
            
            Parent.Add (Table);

            }

        string MakeString(object[] items) {
            string Result = "";

            foreach (var item in items) {
                if (item is string s) {
                    Result += s;
                    }
                }

            return Result;
            }

        string MakeString(object[] items, string[] text) {
            string Result = "";

            if (text != null) {
                foreach (string s in text) {
                    Result += s;
                    }
                }
            return Result;
            }


        // NYI
        // Lowest priority last

        void AddIndex(List<TextBlock> Parent, iref iref) {
            }


        string GetAddressAttribute(postal postal, string Tag) => null;

        }
    }
