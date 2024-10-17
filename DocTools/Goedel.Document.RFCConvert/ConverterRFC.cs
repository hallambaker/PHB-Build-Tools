using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Document.RFC;
using GM = Goedel.Document.Markdown;
using Goedel.Registry;
using Goedel.Utilities;

namespace MakeRFC {
    public class ConverterRFC {
        GM.MarkdownDocument source;
        Goedel.Document.RFC.BlockDocument target;

        enum BlockState {
            Title,
            Abstract,
            Front,
            Back
            }
        BlockState state = BlockState.Title;

        // Convenience routine as a static function
        public static ConverterRFC Convert(GM.MarkdownDocument source, Goedel.Document.RFC.BlockDocument target) => new(source, target);

        // Create a converter
        public ConverterRFC(GM.MarkdownDocument Source, Goedel.Document.RFC.BlockDocument target) {
            this.source = Source;
            this.target = target;
            target.Source = Source;
            Convert();
            }

        void Convert() {
            // Pull in metadata from the catalog
            target.Title = source.MetaDataGetString("title", "Title");
            target.TitleAbrrev = source.MetaDataGetString("abbrev", target.Title);
            target.Docname = source.MetaDataGetString("series", "ietf-draft-TBS"); // ToDo : make proper SeriesInfo

            target.Year = source.MetaDataGetString("year", target.Year);
            target.Month = source.MetaDataGetString("month", target.Month);
            target.Day = source.MetaDataGetString("day", target.Day);

            target.Ipr = source.MetaDataGetString("ipr", null);
            target.Area = source.MetaDataGetStrings("area", null);
            target.Workgroup = source.MetaDataGetStrings("workgroup", null);

            target.Number = source.MetaDataGetString("number", null);
            target.Category = source.MetaDataGetString("category", null);
            target.Updates = source.MetaDataGetString("updates", null);
            target.Obsoletes = source.MetaDataGetString("obsoletes", null);
            target.SeriesNumber = source.MetaDataGetString("seriesnumber", null);

            target.Also = source.MetaDataGetString("also", null);

            var haveKeywords = source.MetaDataLookup("keyword", out var Metas);
            if (haveKeywords) {
                foreach (var Meta in Metas) {
                    if (Meta.Text != null) {
                        target.Keywords.Add(Meta.Text);
                        }
                    }
                }

            var haveSeries = source.MetaDataLookup("series", out Metas);
            if (haveSeries) {
                foreach (var Meta in Metas) {
                    var SeriesInfo = new SeriesInfo() {
                        DocName = Meta.Text
                        };
                    FillMetaString(Meta, "stream", ref SeriesInfo.Stream);
                    FillMetaString(Meta, "status", ref SeriesInfo.Status);
                    FillMetaString(Meta, "version", ref SeriesInfo.Version);
                    target.SeriesInfos.Add(SeriesInfo);
                    }
                }
            else {
                target.Docname = "ietf-draft-TBS";
                }

            var haveLayout = source.MetaDataLookup("layout", out Metas);
            if (haveLayout) {
                foreach (var Meta in Metas) {
                    FillMetaAttributeBool(Meta, "sortrefs", ref target.SortRefs);
                    FillMetaAttributeBool(Meta, "symrefs", ref target.Symrefs);
                    FillMetaAttributeBool(Meta, "toc", ref target.TocInclude);
                    FillMetaAttributeBool(Meta, "tocinclude", ref target.TocInclude);
                    FillMetaAttributeBool(Meta, "tof", ref target.TofInclude);
                    FillMetaAttributeBool(Meta, "tot", ref target.TotInclude);
                    FillMetaAttributeBool(Meta, "ton", ref target.TonInclude);
                    FillMetaAttributeBool(Meta, "index", ref target.IndexInclude);
                    FillMetaAttributeBool(Meta, "embedstyle", ref target.EmbedStylesheet);
                    FillMetaAttributeInt(Meta, "embedsvg", ref target.EmbedSVG);
                    FillMetaAttributeInt(Meta, "tocdepth", ref target.TocDepth);

                    }
                }


            var haveAuthors = source.MetaDataLookup("author", out Metas);
            if (haveAuthors) {
                foreach (var Meta in Metas) {
                    var Author = new Goedel.Document.RFC.Author() {
                        Name = Meta.Text
                        };
                    // add author attributes here

                    FillMetaString(Meta, "initials", ref Author.Initials);
                    FillMetaString(Meta, "surname", ref Author.Surname);
                    FillMetaString(Meta, "organization", ref Author.Organization);
                    FillMetaString(Meta, "organizationabbrev", ref Author.OrganizationAbbrev);
                    FillMetaString(Meta, "street", ref Author.Street);
                    FillMetaString(Meta, "city", ref Author.City);
                    FillMetaString(Meta, "code", ref Author.Code);
                    FillMetaString(Meta, "country", ref Author.Country);
                    FillMetaString(Meta, "phone", ref Author.Phone);
                    FillMetaString(Meta, "email", ref Author.Email);
                    FillMetaString(Meta, "uri", ref Author.URI);

                    target.Authors.Add(Author);
                    }
                }

            // Fill in the lists Abstract, Middle and Back from the block stream

            var currentText = target.Abstract;
            var currentPart = target.Middle;
            var sectionStack = new Stack<Section>();
            Section CurrentSection = null;


            LI enclosingDt = null;

            //var blocknum = 0;
            foreach (var block in source.Blocks) {
                //Console.WriteLine($"Block {blocknum++}");


                if ((block.GetType() == typeof(Goedel.Document.Markdown.Layout)) |
                    (block.GetType() == typeof(Goedel.Document.Markdown.Close))){
                    enclosingDt = null;

                    if (block.CatalogEntry.Key =="table") {
                        currentText.Add(ParseTable(block));
                        }

                    }
                else if (block.CatalogEntry.Level > 0) {
                    enclosingDt = null;

                    //Console.WriteLine("    Heading");
                    if ((block.CatalogEntry.Key == "appendix") &
                            (state != BlockState.Back)) {
                        state = BlockState.Back;
                        currentPart = target.Back;
                        // Reset the section stack
                        sectionStack = new Stack<Section>();
                        }

                    if (state == BlockState.Title) {
                        //Ignore the first section heading
                        state = BlockState.Abstract;
                        }
                    else {
                        // Put a new section onto the stack at the desired level
                        CurrentSection = AddSection(sectionStack, currentPart,
                                block.CatalogEntry.Level);
                        //CurrentSection.Heading = Target.Catalog.GetCitation(Block.Text, true);
                        MakeSegments(CurrentSection, block);
                        currentText = CurrentSection.TextBlocks;
                        }
                    }
                else {
                    if (state == BlockState.Title) {
                        state = BlockState.Abstract;
                        }

                    string FilteredText = target.Catalog.GetCitation(block.Text, true);

                    switch (block.CatalogEntry.Key) {
                        case "li": {
                            enclosingDt = null;


                            var textBlock = new LI() {
                                //Chunks = MakeChunks(Block.Segments),
                                Type = BlockType.Symbol,
                                Level = 1
                                };
                            textBlock.Segments = ReadSegments(textBlock, block.Segments);


                            currentText.Add(textBlock);
                            break;
                            }
                        case "nli": 
                        case "ni" : {
                            enclosingDt = null;
                            var textBlock = new LI() {
                                Type = BlockType.Ordered,
                                Level = 1
                                };
                            textBlock.Segments = ReadSegments(textBlock, block.Segments);
                            currentText.Add(textBlock);
                            break;
                            }
                        case "dt": {
                            var textBlock = new LI() {
                                Type = BlockType.Term,
                                Level = 1,
                                Content = new List<TextBlock>()
                                };
                            textBlock.Segments = ReadSegments(textBlock, block.Segments);
                            currentText.Add(textBlock);
                            enclosingDt = textBlock;
                            break;
                            }
                        case "dd": {
                            if (enclosingDt == null) {
                                enclosingDt = new LI() {
                                    Type = BlockType.Term,
                                    Level = 1,
                                    Content = new List<TextBlock>()
                                    };
                                currentText.Add(enclosingDt);
                                }
                            var TextBlock = new P();
                            TextBlock.Segments = ReadSegments(TextBlock, block.Segments);
                            enclosingDt.Content.Add(TextBlock);

                            break;
                            }
                        case "pre": {
                            enclosingDt = null;
                            var TextBlock = new Goedel.Document.RFC.PRE(Preformat(block.Text), "");

                            if (block.Attributes != null) {
                                if (block.Attributes[0].Value != null) {
                                    TextBlock.Language = block.Attributes[0].Value;
                                    }
                                }

                            currentText.Add(TextBlock);
                            break;
                            }
                        case "figuresvg":
                        case "imgref": {
                            enclosingDt = null;
                            if (block?.Attributes.Count > 0) {
                                var ID = GetID(block);
                                var width = block.AttributeValue("width");
                                var Figure = new Figure(block.Attributes[0].Value, ID) {
                                    Caption = block.Text,
                                    Width = width
                                    };
                                currentText.Add(Figure);
                                }
                            break;
                            }
                        default: {
                            enclosingDt = null;
                            if (state != BlockState.Abstract | block.BlockType != GM.BlockType.Meta) {
                                var textBlock = new P();
                                textBlock.Segments = ReadSegments(textBlock, block.Segments);
                                currentText.Add(textBlock);
                                }
                            break;
                            }
                        }
                    }
                }

            }

        void MakeSegments (Section Section, GM.Block Block) {
            var builder = new StringBuilder();

            foreach (var segment in Block.Segments) {
                switch (segment) {
                    case GM.TextSegmentText Text: {
                        builder.Append(Text.Text);
                        break;
                        }
                    case GM.TextSegmentOpen Text: {
                        if (Text.Attributes?[0].Tag == "id" | Text.Attributes?[0].Tag == "anchor") {
                            Section.SetableID = Text.Attributes?[0].Value;
                            }

                        if (Text.Attributes.Count > 0) {
                            
                            }

                        break;
                        }
                    }
                }
            Section.Heading = builder.ToString();
            Section.Segments = Block.Segments;
            }



        List<GM.TextSegment> ReadSegments (TextBlock parent, List<GM.TextSegment> segments) {

            foreach (var segment in segments) {
                switch (segment) {
                    case GM.TextSegmentOpen text: {
                        switch (text.Tag) {
                            case "info": {
                                AddReference(text, false);
                                break;
                                }
                            case "norm": {
                                AddReference(text, true);
                                break;
                                }
                            case "anchor":
                            case "id": {
                                if (text.Attributes != null && text.Attributes?.Count > 0) {
                                    parent.AnchorID = text.Attributes[0].Value;
                                    }
                                break;
                                }
                            //case "iref": {

                            //    break;
                            //    }
                            //case "xref": {

                            //    break;
                            //    }
                            case "a":
                            case "eref": {

                                break;
                                }
                            }
                        break;
                        }

                    }
                }

            return segments;
            }


        void AddReference (GM.TextSegmentOpen text, bool normative) {
            var ID = text.Attributes?[0].Value;

            text.Text = "[" + ID + "]";

            target.Catalog.AddCitation (ID, normative);
            }

        string GetID (GM.Block block) {
            foreach (var Attribute in block.Attributes) {
                if (Attribute.Tag == "id") {
                    return Attribute.Value;
                    }
                if (Attribute.Tag == "anchor") {
                    return Attribute.Value;
                    }
                }
            return null;
            }


        string Preformat(string In) {
            string Out = In.Replace('\v', '\n');
            return Out.Replace('\r', '\n');
            }

        Table ParseTable (GM.Block block) {
            var Table = new Table();
            TableRow row = null;
            TableData data = null;
            int rowCount = 0;


            foreach (var Segment in block.Segments) {
                switch (Segment) {
                    case GM.TextSegmentOpen Open: {
                        if (Open.CatalogEntry.Key == "tablerow") {
                            row = new TableRow();
                            Table.Body ??= new List<List<TableRow>>();
                            if (Table.Head.Count == 0) {
                                Table.Head.Add(row);
                                }
                            else {
                                if (Table.Body.Count == 0) {
                                    Table.Body.Add(new List<TableRow>());
                                    }
                                Table.Body[0].Add(row);
                                }
                            rowCount++;
                            }
                        if (Open.CatalogEntry.Key == "tablecell") {
                            data = new TableData();
                            row.Data.Add(data);
                            Table.MaxRow = row.Data.Count > Table.MaxRow ? row.Data.Count : Table.MaxRow;
                            }
                        break;
                        }
                    case GM.TextSegmentText Text: {
                        if (data != null) {
                            data.Text = Text.Text;
                            }
                        break;
                        }
                    case GM.TextSegmentClose Close: {
                        break;
                        }
                    }


                }

            return Table;

            }

        void FillMetaAttributeInt (GM.Meta Meta, string Tag, ref int Value) {
            foreach (var Attribute in Meta.Attributes) {
                if (Attribute.Tag.ToLower() == Tag) {
                    Int32.TryParse(Attribute.Value, out Value);
                    return;
                    }
                }
            }

        void FillMetaAttributeBool (GM.Meta Meta, string Tag, ref bool Value) {
            foreach (var Attribute in Meta.Attributes) {
                if (Attribute.Tag.ToLower() == Tag) {
                    Value = Attribute.Value.ToLower() != "false";
                    return;
                    }
                }
            }

        void FillMetaString(GM.Meta Meta, string Tag, ref string Value) {
            if (Meta.Children.TryGetValue(Tag, out var Metas)) {
                Value = Metas[0].Text;
                }
            }

        Section AddSection(Stack<Section> Stack, List<Section> Top, int Level) {
            //Console.WriteLine("Level {0}", Level);


            // If Level is 1 then we always make a new section
            if (Level == 1) {
                var Section = new Section();
                Stack.Clear();
                Stack.Push(Section);
                Top.Add(Section);
                return Section;
                }

            // Make sure we have a starter on the stack even if a
            // document starts with a heading 2
            if (Stack.Count == 0) {
                var Section = new Section();
                Stack.Push(Section);
                Top.Add(Section);
                }

            // New heading is lower level so pop the stack
            while (Level <= Stack.Count) {
                Stack.Pop();
                }

            while (Level > Stack.Count) {
                var Section = new Section();
                var Last = Stack.Peek();
                Last.Subsections.Add(Section);
                Stack.Push(Section);
                }

            return Stack.Peek();
            }

        }

    }
