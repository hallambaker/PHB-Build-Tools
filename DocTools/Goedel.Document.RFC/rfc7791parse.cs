using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Goedel.Document.RFC;
using Goedel.Utilities;

namespace Goedel.Document.RFC;

/// <summary>
/// Parse input in RFC 77991 (XML2RFCv3) format and render as a series of blocks.
/// </summary>
public class Rfc7991Parse {

    Goedel.Document.RFC.BlockDocument Document;
    TextReader TextReader;

    public static void Parse(string File, Goedel.Document.RFC.BlockDocument Document) {
        using FileReader FileReader = new(File);
        Parse(FileReader, Document);
        }

    public static void Parse(TextReader TextReader, Goedel.Document.RFC.BlockDocument Document) => new Rfc7991Parse(TextReader, Document);


    public Rfc7991Parse(TextReader TextReader, Goedel.Document.RFC.BlockDocument Document) {
        this.TextReader = TextReader;
        this.Document = Document;

        Parse();
        }


    public void Parse() {

        //XmlRootAttribute xRoot = new XmlRootAttribute() {
        //    ElementName = "rfc"
        //    };
        //xRoot.Namespace = "http://tempuri.org/rfc2629";
        //xRoot.IsNullable = true;

        //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //ns.Add("", "http://tempuri.org/rfc2629");

        var XmlSerializer = new XmlSerializer(typeof(rfc));

        rfc rfc = (rfc)XmlSerializer.Deserialize(TextReader);
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

            Document.Abstract = MakeTextBlocks(front.@abstract.Items, front.@abstract.ItemsElementName);
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


    List<string> MakeString(asciitext[] Text) {
        if (Text == null) {
            return null;
            }

        var Result = new List<string>();
        foreach (var T in Text) {
            Result.Add(T.Value);
            }
        return Result;
        }


    List<string> MakeKeywords(asciitext[] keyword) {
        List<string> Result = new();

        if (keyword != null) {
            foreach (var s in keyword) {
                Result.Add(s.Value);
                }
            }

        return Result;
        }

    #region // authors, references

    List<Author> MakeAuthors(author[] authors) {
        List<Author> Result = new();

        foreach (author author in authors) {
            Author Author = new() {
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

            Result.Add(Author);
            }

        return Result;
        }


    Reference MakeReference(reference reference) {
        Reference Result = new();

        if (reference.front != null) {
            Result.Title = reference.front.title.Value;
            Result.Abbrev = reference.front.title.abbrev;
            if (reference.front.author != null) {
                Result.Authors = MakeAuthors(reference.front.author);
                }
            if (reference.front.date != null) {
                Result.Day = reference.front.date.day;
                Result.Month = reference.front.date.month;
                Result.Year = reference.front.date.year;
                }
            Result.Area = MakeString(reference.front.area);
            Result.Workgroup = MakeString(reference.front.workgroup);
            Result.Keywords = MakeKeywords(reference.front.keyword);
            // do nothing with the note field
            }

        Result.SeriesInfos = MakeSeriesInfo(reference.Items);
        Result.Formats = MakeFormats(reference.Items);


        Result.GeneratedID = reference.anchor;
        Result.Target = reference.target;

        return Result;
        }



    List<SeriesInfo> MakeSeriesInfo(seriesInfo[] seriesInfos) {
        List<SeriesInfo> ListSeriesInfo = new();

        if (seriesInfos != null) {
            foreach (var seriesInfo in seriesInfos) {

                SeriesInfo SeriesInfo = new();
                ListSeriesInfo.Add(SeriesInfo);
                SeriesInfo.Name = seriesInfo.name;
                SeriesInfo.Value = seriesInfo.value;
                break;
                }
            }
        return ListSeriesInfo;

        }

    List<SeriesInfo> MakeSeriesInfo(object[] seriesInfos) {
        var ListSeriesInfo = new List<SeriesInfo>();

        if (seriesInfos != null) {
            foreach (var obj in seriesInfos) {
                switch (obj) {
                    case seriesInfo seriesInfo: {
                            SeriesInfo SeriesInfo = new();
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

    List<Format> MakeFormats(object[] formats) {
        var ListFormats = new List<Format>();

        if (formats != null) {
            foreach (var obj in formats) {

                switch (obj) {
                    case format format: {
                            Format Format = new();
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


    #endregion

    List<TextBlock> MakeTextBlocks(object[] items, ItemsChoiceTextRun[] tags) {
        if (items == null) {
            return null;
            }

        var builder = new TextBlockSequenceBuilder();
        AddTextBlocks(builder, items, tags);

        return builder.Blocks;
        }


    void MakeCatalog(Catalog Catalog, references[] referencesArray) {
        if (referencesArray != null) {
            foreach (references references in referencesArray) {
                References References = new();
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

    List<List<TextBlock>> MakeTextBlocks(note[] Notes) {
        if (Notes == null) {
            return null;
            }

        var result = new List<List<TextBlock>>();

        foreach (var Note in Notes) {

            var blocks = MakeTextBlocks(Note.Items, Note.ItemsElementName);
            result.Add(blocks);

            }
        return result;
        }



    void MakeTextBlockT(TextBlockSequenceBuilder builder, t source) {
        var block = new P() {
            Segments = new List<Markdown.TextSegment>()
            };

        builder.AddBlock(block, block.Segments);
        AddTextBlocks(builder, source.Items, source.ItemsElementName);
        }

    void MakeTextBlock(TextBlockSequenceBuilder builder, ol source) {
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
                    Type = BlockType.Ordered
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
                    Type = BlockType.Symbol
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
            AnchorID = source.anchor
            };

        if (source.iref != null) {
            block.Irefs = MakeIrefs(source.iref);
            }

        block.Head = MakeTableRow(source.thead);
        foreach (var row in source.tbody) {
            block.Body.Add(MakeTableRow(row));
            }
        block.Foot = MakeTableRow(source.tfoot);

        builder.AddBlock(block, null);

        // see above
        }


    List<TableRow> MakeTableRow(tableRowsType trtype) {
        if (trtype == null) {
            return null;
            }
        if (trtype.tr == null) {
            return null;
            }
        var result = new List<TableRow>();

        if (trtype.tr != null) {
            foreach (var trs in trtype.tr) {
                var dataList = new List<TableData>();
                var tableRow = new TableRow() {
                    AnchorID = trtype.anchor,
                    Data = dataList
                    };
                result.Add(tableRow);
                if (trs.Items != null) {
                    foreach (var item in trs.Items) {
                        dataList.Add(MakeTableCell(item));
                        }
                    }
                }
            }

        return result;
        }

    int A2Int(string text, int def = 1) {
        if (!Int32.TryParse(text, out int index)) {
            return def;
            }
        return index;
        }

    TableData MakeTableCell(tableCellType cellType) {
        // anchor, colspan, rowspan, align 

        var blocks = MakeTextBlocks(cellType.Items, cellType.ItemsElementName);

        var result = new TableData() {
            AnchorID = cellType.anchor,
            RowSpan = A2Int(cellType.rowspan),
            ColSpan = A2Int(cellType.colspan),
            Blocks = blocks
            };


        return result;
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

    void MakeTextBlock(TextBlockSequenceBuilder builder, aside source) {
        builder.AsideLevel++;
        AddTextBlocks(builder, source.Items, source.ItemsElementName);
        builder.AsideLevel--;
        }




    void MakeTextBlock(TextBlockSequenceBuilder builder, sourcecode source) =>
        builder.AddBlock(MakeTextBlock(source), null);

    List<Markdown.TextSegment> MakeName(name name) {
        return null;
        }

    List<Markdown.TextSegment> MakeIrefs(iref[] irefs) {
        return null;
        }


    PRE MakeTextBlock(sourcecode source) {
        var block = new PRE(source.Value, source.anchor) {
            Element = "sourcecode",
            Filename = source.src,
            Language = source.type,
            OutputFile = source.name
            };

        return block;
        }

    void MakeTextBlock(TextBlockSequenceBuilder builder, artwork source) =>
        builder.AddBlock(MakeTextBlock(source), null);

    PRE MakeTextBlock(artwork source) {
        var block = new PRE(source.Text, source.anchor) {
            Element = "artwork",
            Filename = source.src
            };
        return block;

        }

    void MakeTextBlock(TextBlockSequenceBuilder builder, figure source) {
        // see above

        var block = new Figure(null, source.anchor) {
            AnchorID = source.anchor,
            Width = source.width,
            Height = source.height
            };

        if (source.name != null) {
            block.BlockName = MakeName(source.name);
            }
        if (source.iref != null) {
            block.Irefs = MakeIrefs(source.iref);
            }


        if (source.preamble != null) {
            block.Preamble = MakeTextSegments(
                source.preamble.Items, source.preamble.ItemsElementName);
            }
        if (source.postamble != null) {
            block.Postamble = MakeTextSegments(
                source.postamble.Items, source.postamble.ItemsElementName);
            }

        foreach (var item in source.Items) {
            switch (item) {
                case artwork artwork: {
                        if (artwork.type == "svg") {
                            }
                        else {
                            block.Content.Add(MakeTextBlock(artwork));
                            }
                        break;
                        }
                case sourcecode sourcecode: {
                        block.Content.Add(MakeTextBlock(sourcecode));
                        break;
                        }
                }

            }
        builder.AddBlock(block, null);


        }


    List<Markdown.TextSegment> MakeTextSegments(object[] Items, ItemsChoiceTextRun[] Tags) {
        return null;
        }

    void AddTextBlocks(TextBlockSequenceBuilder builder, object[] Items, ItemsChoiceTextRun[] Tags) {
        if (Items == null) {
            return;
            }

        var index = 0;
        foreach (var item in Items) {
            if (item is string s) {
                //Console.WriteLine($"{item}");
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

        List<Section> Result = new();
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

    // ToDo: this should be a text run so that it can have indexes etc.
    string MakeString(object[] items) {
        string Result = "";

        foreach (var item in items) {
            if (item is string s) {
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
