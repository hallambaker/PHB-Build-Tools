using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HTML2RFC;
using GM = Goedel.MarkLib;
using Goedel.Registry;

namespace MakeRFC {
    public class ConverterRFC {
        GM.Document Source;
        HTML2RFC.Document Target;

        enum BlockState {
            Title,
            Abstract,
            Front,
            Back
            }
        BlockState State = BlockState.Title;

        // Convenience routine as a static function
        public static void Convert(GM.Document Source, HTML2RFC.Document Target) {
            var Converter = new ConverterRFC(Source, Target);
            }

        // Create a converter
        public ConverterRFC(GM.Document Source, HTML2RFC.Document Target) {
            this.Source = Source;
            this.Target = Target;
            Convert();
            }

        void Convert() {
            // Pull in metadata from the catalog
            Target.Title = Source.MetaDataGetString("title", "Title");
            Target.Abrrev = Source.MetaDataGetString("abbrev", Target.Title);
            Target.Docname = Source.MetaDataGetString("ietf", "ietf-draft-TBS");
            Target.Version = Source.MetaDataGetString("version", "00");

            Target.Year = Source.MetaDataGetString("year", Target.Year);
            Target.Month = Source.MetaDataGetString("month", Target.Month);
            Target.Day = Source.MetaDataGetString("day", Target.Day);

            Target.Ipr = Source.MetaDataGetString("ipr", null);
            Target.Area = Source.MetaDataGetString("area", null);
            Target.Workgroup = Source.MetaDataGetString("workgroup", null);
            Target.Publisher = Source.MetaDataGetString("publisher", Target.Publisher);
            Target.Status = Source.MetaDataGetString("status", Target.Status);

            Target.Number = Source.MetaDataGetString("number", null);
            Target.Category = Source.MetaDataGetString("category", null);
            Target.Updates = Source.MetaDataGetString("updates", null);
            Target.Obsoletes = Source.MetaDataGetString("obsoletes", null);
            Target.SeriesNumber = Source.MetaDataGetString("seriesnumber", null);

            List<GM.Meta> Metas;
            var HaveKeywords = Source.MetaDataLookup("keyword", out Metas);
            if (HaveKeywords) {
                foreach (var Meta in Metas) {
                    if (Meta.Text != null) {
                        Target.Keywords.Add(Meta.Text);
                        }
                    }
                }

            var HaveAuthors = Source.MetaDataLookup("author", out Metas);
            if (HaveAuthors) {
                foreach (var Meta in Metas) {
                    var Author = new HTML2RFC.Author();
                    Author.Name = Meta.Text;
                    // add author attributes here

                    FillAuthor(Meta, "initials", ref Author.Initials);
                    FillAuthor(Meta, "firstname", ref Author.Surname);
                    FillAuthor(Meta, "lastname", ref Author.LastName);
                    FillAuthor(Meta, "organization", ref Author.Organization);
                    FillAuthor(Meta, "organizationabbrev", ref Author.OrganizationAbbrev);
                    FillAuthor(Meta, "street", ref Author.Street);
                    FillAuthor(Meta, "city", ref Author.City);
                    FillAuthor(Meta, "code", ref Author.Code);
                    FillAuthor(Meta, "country", ref Author.Country);
                    FillAuthor(Meta, "phone", ref Author.Phone);
                    FillAuthor(Meta, "email", ref Author.Email);
                    FillAuthor(Meta, "uri", ref Author.URI);

                    Target.Authors.Add(Author);
                    }
                }


            // Thes have multiple values which makes them a bit more complex
            //Target.Keywords = new List<string> { "TBS" };
            //Target.Authors = ;



            // Fill in the lists Abstract, Middle and Back from the block stream

            var CurrentText = Target.Abstract;
            var CurrentPart = Target.Middle;
            var SectionStack = new Stack<Section>();
            Section CurrentSection = null;

            foreach (var Block in Source.Blocks) {
                //Console.WriteLine("Block {0}", Block.BlockType);


                if ((Block.GetType() == typeof(Goedel.MarkLib.Layout)) |
                    (Block.GetType() == typeof(Goedel.MarkLib.Close))){
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
                        CurrentSection.Heading = Target.Catalog.GetCitation(Block.Text, true);
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
                            var TextBlock = new HTML2RFC.LI(FilteredText, "", ListItem.Symbol, 1);
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "nli": 
                        case "ni" : {
                            var TextBlock = new HTML2RFC.LI(FilteredText, "", ListItem.Ordered, 1);
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "dt": {
                            var TextBlock = new HTML2RFC.LI(FilteredText, "", ListItem.Term, 1);
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "dd": {
                            var TextBlock = new HTML2RFC.LI(FilteredText, "", ListItem.Data, 1);
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        case "pre": {
                            var TextBlock = new HTML2RFC.PRE(Preformat(Block.Text), "");
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        default: {
                            var TextBlock = new HTML2RFC.P(FilteredText, "");
                            CurrentText.Add(TextBlock);
                            break;
                            }
                        }
                    }
                }

            }
        string Preformat(string In) {
            string Out = In.Replace('\v', '\n');



            return Out.Replace('\r', '\n');
            }

        void FillAuthor(GM.Meta Meta, string Tag, ref string Value) {
            List<GM.Meta> Metas;

            if (Meta.Children.TryGetValue(Tag, out Metas)) {
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
