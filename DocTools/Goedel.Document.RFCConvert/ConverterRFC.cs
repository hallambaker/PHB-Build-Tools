using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Document.RFC;
using GM = Goedel.Document.Markdown;
using Goedel.Registry;

namespace MakeRFC {
    public class ConverterRFC {
        GM.Document Source;
        Goedel.Document.RFC.Document Target;

        enum BlockState {
            Title,
            Abstract,
            Front,
            Back
            }
        BlockState State = BlockState.Title;

        // Convenience routine as a static function
        public static ConverterRFC Convert(GM.Document Source, Goedel.Document.RFC.Document Target) => new ConverterRFC(Source, Target);

        // Create a converter
        public ConverterRFC(GM.Document Source, Goedel.Document.RFC.Document Target) {
            this.Source = Source;
            this.Target = Target;
            Target.Source = Source;
            Convert();
            }

        void Convert() {
            // Pull in metadata from the catalog
            Target.Title = Source.MetaDataGetString("title", "Title");
            Target.TitleAbrrev = Source.MetaDataGetString("abbrev", Target.Title);
            Target.Docname = Source.MetaDataGetString("series", "ietf-draft-TBS"); // ToDo : make proper SeriesInfo

            Target.Year = Source.MetaDataGetString("year", Target.Year);
            Target.Month = Source.MetaDataGetString("month", Target.Month);
            Target.Day = Source.MetaDataGetString("day", Target.Day);

            Target.Ipr = Source.MetaDataGetString("ipr", null);
            Target.Area = Source.MetaDataGetStrings("area", null);
            Target.Workgroup = Source.MetaDataGetStrings("workgroup", null);

            Target.Number = Source.MetaDataGetString("number", null);
            Target.Category = Source.MetaDataGetString("category", null);
            Target.Updates = Source.MetaDataGetString("updates", null);
            Target.Obsoletes = Source.MetaDataGetString("obsoletes", null);
            Target.SeriesNumber = Source.MetaDataGetString("seriesnumber", null);

            Target.Also = Source.MetaDataGetString("also", null);

            var HaveKeywords = Source.MetaDataLookup("keyword", out var Metas);
            if (HaveKeywords) {
                foreach (var Meta in Metas) {
                    if (Meta.Text != null) {
                        Target.Keywords.Add(Meta.Text);
                        }
                    }
                }

            var HaveSeries = Source.MetaDataLookup("series", out Metas);
            if (HaveSeries) {
                foreach (var Meta in Metas) {
                    var SeriesInfo = new SeriesInfo() {
                        DocName = Meta.Text
                        };
                    FillMetaString(Meta, "stream", ref SeriesInfo.Stream);
                    FillMetaString(Meta, "status", ref SeriesInfo.Status);
                    FillMetaString(Meta, "version", ref SeriesInfo._Version);
                    Target.SeriesInfos.Add(SeriesInfo);
                    }
                }
            else {
                Target.Docname = "ietf-draft-TBS";
                }

            var HaveLayout = Source.MetaDataLookup("layout", out Metas);
            if (HaveLayout) {
                foreach (var Meta in Metas) {
                    FillMetaAttributeBool(Meta, "sortrefs", ref Target.SortRefs);
                    FillMetaAttributeBool(Meta, "symrefs", ref Target.Symrefs);
                    FillMetaAttributeBool(Meta, "toc", ref Target.TocInclude);
                    FillMetaAttributeBool(Meta, "tocinclude", ref Target.TocInclude);
                    FillMetaAttributeBool(Meta, "tof", ref Target.TofInclude);
                    FillMetaAttributeBool(Meta, "tot", ref Target.TotInclude);
                    FillMetaAttributeBool(Meta, "ton", ref Target.TonInclude);
                    FillMetaAttributeBool(Meta, "index", ref Target.IndexInclude);
                    FillMetaAttributeBool(Meta, "embedstyle", ref Target.EmbedStylesheet);
                    FillMetaAttributeInt(Meta, "embedsvg", ref Target.EmbedSVG);
                    FillMetaAttributeInt(Meta, "tocdepth", ref Target.TocDepth);

                    }
                }


            var HaveAuthors = Source.MetaDataLookup("author", out Metas);
            if (HaveAuthors) {
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

                    Target.Authors.Add(Author);
                    }
                }

            // Fill in the lists Abstract, Middle and Back from the block stream

            var CurrentText = Target.Abstract;
            var CurrentPart = Target.Middle;
            var SectionStack = new Stack<Section>();
            Section CurrentSection = null;

            foreach (var Block in Source.Blocks) {
                //Console.WriteLine("Block {0}", Block.BlockType);


                if ((Block.GetType() == typeof(Goedel.Document.Markdown.Layout)) |
                    (Block.GetType() == typeof(Goedel.Document.Markdown.Close))){

                    if (Block.CatalogEntry.Key =="table") {
                        CurrentText.Add(ParseTable(Block));
                        }

                    }
                else if (Block.CatalogEntry.Level > 0) {
                    //Console.WriteLine("    Heading");
                    if ((Block.CatalogEntry.Key == "appendix") &
                            (State != BlockState.Back)) {
                        State = BlockState.Back;
                        CurrentPart = Target.Back;
                        // Reset the section stack
                        SectionStack = new Stack<Section>();
                        }

                    if (State == BlockState.Title) {
                        //Ignore the first section heading
                        State = BlockState.Abstract;
                        }
                    else {
                        // Put a new section onto the stack at the desired level
                        CurrentSection = AddSection(SectionStack, CurrentPart,
                                Block.CatalogEntry.Level);
                        //CurrentSection.Heading = Target.Catalog.GetCitation(Block.Text, true);
                        MakeSegments(CurrentSection, Block);
                        CurrentText = CurrentSection.TextBlocks;
                        }
                    }
                else {
                    if (State == BlockState.Title) {
                        State = BlockState.Abstract;
                        }

                    string FilteredText = Target.Catalog.GetCitation(Block.Text, true);

                    switch (Block.CatalogEntry.Key) {
                        case "li": {
                            var TextBlock = new LI() {
                                Segments = ReadSegments (Block.Segments),
                                //Chunks = MakeChunks(Block.Segments),
                                Type = BlockType.Symbol,
                                Level = 1
                                };
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "nli": 
                        case "ni" : {
                            var TextBlock = new LI() {
                                Segments = ReadSegments(Block.Segments),
                                //Chunks = MakeChunks(Block.Segments),
                                Type = BlockType.Ordered,
                                Level = 1
                                };
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "dt": {
                            var TextBlock = new LI() {
                                Segments = ReadSegments(Block.Segments),
                                //Chunks = MakeChunks(Block.Segments),
                                Type = BlockType.Term,
                                Level = 1
                                };
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "dd": {
                            var TextBlock = new LI() {
                                Segments = ReadSegments(Block.Segments),
                                //Chunks = MakeChunks(Block.Segments),
                                Type = BlockType.Data,
                                Level = 1
                                };
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "pre": {
                            var TextBlock = new Goedel.Document.RFC.PRE(Preformat(Block.Text), "");

                            if (Block.Attributes != null) {
                                if (Block.Attributes[0].Value != null) {
                                    TextBlock.Language = Block.Attributes[0].Value;
                                    }
                                }

                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "figuresvg":
                        case "imgref": {
                            if (Block?.Attributes.Count > 0) {
                                var ID = GetID(Block);
                                var Figure = new Figure(Block.Attributes[0].Value, ID) {
                                    Caption = Block.Text
                                    };
                                CurrentText.Add(Figure);
                                }
                            break;
                            }
                        default: {
                            if (State != BlockState.Abstract | Block.BlockType != GM.BlockType.Meta) {
                                var TextBlock = new P() {
                                    Segments = ReadSegments(Block.Segments),
                                    //Chunks = MakeChunks(Block.Segments),
                                    };
                                CurrentText.Add(TextBlock);
                                }
                            break;
                            }
                        }
                    }
                }

            }

        void MakeSegments (Section Section, GM.Block Block) {
            var Builder = new StringBuilder();

            foreach (var Segment in Block.Segments) {
                switch (Segment) {
                    case GM.TextSegmentText Text: {
                        Builder.Append(Text.Text);
                        break;
                        }
                    case GM.TextSegmentOpen Text: {
                        if (Text.Attributes?[0].Tag == "id") {
                            Section.GeneratedID = Text.Attributes?[0].Value;
                            }

                        if (Text.Attributes.Count > 0) {
                            
                            }

                        break;
                        }
                    }
                }
            Section.Heading = Builder.ToString();
            Section.Segments = Block.Segments;
            }



        List<GM.TextSegment> ReadSegments (List<GM.TextSegment> Segments) {

            foreach (var Segment in Segments) {
                switch (Segment) {
                    case GM.TextSegmentOpen Text: {
                        if (Text.Tag == "info") {
                            AddReference(Text, false);
                            }
                        else if (Text.Tag == "norm") {
                            AddReference(Text, true);
                            }
                        break;
                        }

                    }
                }

            return Segments;
            }


        void AddReference (GM.TextSegmentOpen Text, bool Normative) {
            var ID = Text.Attributes?[0].Value;

            Text.Text = "[" + ID + "]";

            Target.Catalog.AddCitation (ID, Normative);
            }

        string GetID (GM.Block Block) {
            foreach (var Attribute in Block.Attributes) {
                if (Attribute.Tag == "id") {
                    return Attribute.Value;
                    }
                }
            return null;
            }


        string Preformat(string In) {
            string Out = In.Replace('\v', '\n');
            return Out.Replace('\r', '\n');
            }

        Table ParseTable (GM.Block Block) {
            var Table = new Table ();
            TableRow Row = null;
            TableData Data = null;
            int RowCount = 0;

            foreach (var Segment in Block.Segments) {
                switch (Segment) {
                    case GM.TextSegmentOpen Open: {
                        if (Open.CatalogEntry.Key == "tablerow") {
                            Row = new TableRow();
                            Table.Rows.Add(Row);
                            RowCount++;
                            }
                        if (Open.CatalogEntry.Key == "tablecell") {
                            Data = new TableData();
                            Row.Data.Add(Data);
                            Table.MaxRow = Row.Data.Count > Table.MaxRow ? Row.Data.Count : Table.MaxRow;
                            }
                        break;
                        }
                    case GM.TextSegmentText Text: {
                        Data.Text = Text.Text;
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
