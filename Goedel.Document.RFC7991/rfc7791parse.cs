using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Goedel.Tool.RFCTool;

namespace Goedel.Document.RFC7991 {
    public class Rfc7991Parse {

        Goedel.Tool.RFCTool.Document Document;
        TextReader TextReader;

        public static void Parse(string File, Goedel.Tool.RFCTool.Document Document) {
            using (FileReader FileReader = new FileReader(File)) {
                Parse(FileReader, Document);
                }
            }

        public static void Parse(TextReader TextReader, Goedel.Tool.RFCTool.Document Document) {
            new Rfc7991Parse(TextReader, Document);
            }

        
        public Rfc7991Parse(TextReader TextReader, Goedel.Tool.RFCTool.Document Document) {
            this.TextReader = TextReader;
            this.Document = Document;

            Parse ();
            }


        public void Parse() {

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "rfc";
            //xRoot.Namespace = "http://tempuri.org/rfc2629";
            //xRoot.IsNullable = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://tempuri.org/rfc2629");

            XmlSerializer XmlSerializer = new XmlSerializer (typeof (rfc), "http://tempuri.org/rfc7991");

            rfc rfc = (rfc) XmlSerializer.Deserialize (TextReader);

            Document.Abrrev = rfc.front.title.abbrev;
            Document.Abstract = MakeTextBlocks (rfc.front.@abstract.t);
            Document.Area = MakeString(rfc.front.area);
            Document.Authors = MakeAuthors (rfc.front.author);
            Document.Back = MakeSections (rfc.back.section, 1);
            MakeCatalog (Document.Catalog, rfc.back.references);
            Document.Category = MakeString (rfc.category, rfc.categorySpecified);
            Document.Day = rfc.front.date.day;
            Document.Docname = rfc.docName;
            Document.Ipr = MakeString(rfc.ipr, rfc.iprSpecified);
            Document.Keywords = MakeKeywords (rfc.front.keyword);
            Document.Middle =  MakeSections (rfc.middle, 1);
            Document.Month = rfc.front.date.month;
            Document.Number = rfc.number;
            Document.Obsoletes = rfc.obsoletes;
            Document.SeriesNumber = rfc.seriesNo;
            Document.Title = rfc.front.title.Value;
            Document.Updates = rfc.updates;
            Document.Version = null; // is part of the Docname in rfc2629 format
            Document.Workgroup = MakeString(rfc.front.workgroup);
            Document.Year = rfc.front.date.year;


            // These should probably be eliminated by proper handling of the
            // ID type tags

            //Document.Publisher = "Fix this";
            //Document.ID1 = "Internet-Draft";
            //Document.Status = "Standards Track";
            }


        string MakeString (string[] s) {

            if (s == null) { return null; }
            if (s.Length == 0) {return null; }
            return s[0];
            }

        List<string> MakeKeywords(string[] keyword) {
            List<string> Result = new List<string>();

            if (keyword != null) {
                foreach (string s in keyword) {
                    Result.Add(s);
                    }
                }

            return Result;
            }



        string MakeString(rfcCategory enumeration, bool specified) {
            if (!specified) { return null; }
            switch (enumeration) {
                case rfcCategory.std: return "std";
                case rfcCategory.bcp: return "bcp";
                case rfcCategory.info: return "info";
                case rfcCategory.exp: return "exp";
                case rfcCategory.historic: return "historic";
                default: return null;
                }
            }

        string MakeString(rfcIpr enumeration, bool specified) {
            if (!specified) { return null; }
            switch (enumeration) {
                    case rfcIpr.full2026: return "full2026";
                    case rfcIpr.noDerivativeWorks2026: return "noDerivativeWorks2026";
                    case rfcIpr.none: return "none";
                    case rfcIpr.full3667: return "full3667";
                    case rfcIpr.noModification3667: return "noModification3667";
                    case rfcIpr.noDerivatives3667: return "noDerivatives3667";
                    case rfcIpr.full3978: return "full3978";
                    case rfcIpr.noModification3978: return "noModification3978";
                    case rfcIpr.noDerivatives3978: return "noDerivatives3978";
                    case rfcIpr.trust200811: return "trust200811";
                    case rfcIpr.noModificationTrust200811: return "noModificationTrust200811";
                    case rfcIpr.noDerivativesTrust200811: return "noDerivativesTrust200811";
                    case rfcIpr.trust200902: return "trust200902";
                    case rfcIpr.noModificationTrust200902: return "noModificationTrust200902";
                    case rfcIpr.noDerivativesTrust200902: return "noDerivativesTrust200902";
                    case rfcIpr.pre5378Trust200902: return "pre5378Trust200902";
                default : return null;
                }
            }

        string MakeString(rfcSubmissionType enumeration, bool specified) {
            if (!specified) { return null; }
            switch (enumeration) {
                case rfcSubmissionType.IETF: return "IETF";
                case rfcSubmissionType.independent: return "independent";
                default: return null;
                }
            }




        List<Author> MakeAuthors(author[] authors) {
            List<Author> Result = new List<Author> ();

            foreach (author author in authors) {
                Author Author = new Author ();
                Author.Initials = author.initials;
                Author.Name = author.fullname;

                if (author.organization != null) {
                    Author.Organization = author.organization.Value;
                    Author.OrganizationAbbrev = author.organization.abbrev;
                    }
              
                Author.Surname = author.surname;

                if (author.address != null) {
                    Author.Phone = author.address.phone;
                    Author.URI = author.address.uri;
                    Author.Email = author.address.email;
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

            Result.SeriesInfos = MakeSeriesInfo (reference.seriesInfo);
            Result.Formats = MakeFormats (reference.format);


            Result.ID = reference.anchor;
            Result.Target = reference.target;

            //Result.Version = reference.


            //front front
            // seriesInfo[] seriesInfo
            // format[] format 
            // annotation[] annotation
            //  string anchor
            // string target

            return Result;
            }

        List<SeriesInfo> MakeSeriesInfo(seriesInfo []seriesInfos) {
            List<SeriesInfo> ListSeriesInfo = new List<SeriesInfo> ();

            if (seriesInfos != null) {
                foreach (seriesInfo seriesInfo in seriesInfos) {
                    SeriesInfo SeriesInfo = new SeriesInfo();
                    ListSeriesInfo.Add(SeriesInfo);
                    SeriesInfo.Name = seriesInfo.name;
                    SeriesInfo.Value = seriesInfo.value;
                    }
                }


            return ListSeriesInfo;
            }

        List<Format> MakeFormats(format []formats) {
            List<Format> ListFormats = new List<Format> ();

            if (formats != null) {
                foreach (format format in formats) {
                    Format Format = new Format();
                    ListFormats.Add(Format);
                    Format.Octets = format.octets;
                    Format.Target = format.target;
                    Format.Type = format.type;
                    }
                }
            return ListFormats;
            }



        void MakeCatalog(Catalog Catalog, references[] referencesArray) {
            if (referencesArray != null) {
                foreach (references references in referencesArray) {
                    References References = new References();
                    Catalog.ReferenceSections.Add(References);
                    References.Title = references.title;

                    foreach (reference reference in references.reference) {
                        References.Entries.Add(MakeReference(reference));
                        }
                    }
                }
            }




        List<TextBlock> MakeTextBlocks(t[] ts) {
            List<TextBlock> Result = new List<TextBlock>();

            foreach (t t in ts) {
                AddListBlocks (Result, t);
                }


            return Result;
            }

        List<Section> MakeSections(section[] sections, int level) {
            if (level > 6) {
                throw new Exception("Levels nested too deeply, maximum is 6.");
                }

            List<Section> Result = new List<Section>();
            if (sections != null) {
                foreach (section section in sections) {
                    Section Section = new Section(section.title, section.anchor);
                    if (section.Items != null) {
                        foreach (object o in section.Items) {
                            if (o.GetType() == typeof(figure)) {
                                AddFigureBlock(Section.TextBlocks, (figure)o);
                                }
                            else if (o.GetType() == typeof(iref)) {
                                AddIndex(Section.TextBlocks, (iref)o);
                                }
                            else if (o.GetType() == typeof(t)) {
                                AddListBlocks(Section.TextBlocks, (t)o);
                                }
                            else if (o.GetType() == typeof(texttable)) {
                                AddTableBlock(Section.TextBlocks, (texttable)o);
                                }
                            }
                        }

                    // Recurse
                    if (section.section1 != null) {
                        Section.Subsections = MakeSections(section.section1, level + 1);
                        }

                    Result.Add(Section);
                    }
                }
            return Result;
            }


        ////////////////////////
        // Above is mostly stable

        void AddText(ref string s1, string s2) {
            s1 = (s1 == null) ? s2 : s1 + s2;
            }


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
                        Console.Write("Got list !");
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



        void AddFigureBlock(List<TextBlock> Parent, figure figure) {
            if (figure.artwork != null && figure.artwork.Value != null) {
                PRE PRE = new PRE(figure.artwork.Value, figure.anchor);
                Parent.Add (PRE);
                }
            }

        void AddTableBlock(List<TextBlock> Parent, texttable texttable) {
            Table Table = new Table ();

            Table.ID = texttable.anchor;
            
            TableRow TableRow = new TableRow();
            foreach (ttcol ttcol in texttable.ttcol) {
                TableData item = new TableData ();
                item.IsHeading = true;
                item.Text = ttcol.Value;
                TableRow.Data.Add (item);
                }

            Table.MaxRow = TableRow.Data.Count;
            Table.Rows.Add (TableRow);

            int col = Table.MaxRow;
            foreach (c c in texttable.c) {
                TableData item = new TableData ();
                item.IsHeading = false;
                item.Text = MakeString (c.Items, c.Text);

                if (col >= Table.MaxRow) {
                    col = 0;
                    TableRow = new TableRow();
                    Table.Rows.Add (TableRow);
                    }
                col++;
                TableRow.Data.Add (item);
                }
            
            Parent.Add (Table);

            }


        string MakeString(object [] items, string[] text) {
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


        string GetAddressAttribute(postal postal, string Tag) {
            return null;
            }

        }
    }
