using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Goedel.Tool.RFCTool;

namespace Goedel.Tool.RFCTool {
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

            XmlRootAttribute xRoot = new XmlRootAttribute() {
                ElementName = "rfc"
                };
            //xRoot.Namespace = "http://tempuri.org/rfc2629";
            //xRoot.IsNullable = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://tempuri.org/rfc2629");

            XmlSerializer XmlSerializer = new XmlSerializer (typeof (rfc), "http://tempuri.org/rfc7991");

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
                Document.Authors = MakeAuthors(front.author);
                Document.Day = rfc.front.date.day;
                Document.Month = rfc.front.date.month;
                Document.Year = rfc.front.date.year;
                Document.Area = MakeString(front.area);
                Document.Workgroup = MakeString(front.workgroup);
                Document.Keywords = MakeKeywords(front.keyword);

                Document.Abstract = MakeTextBlocks(front.@abstract.Items);
                Document.Note = MakeTextBlocks(front.note);
                Document.Boilerplate = MakeSections(front.boilerplate.section, 2);
                }

            Document.Middle = MakeSections(rfc.middle.section, 1);

            if (rfc.back != null) {
                var back = rfc.back;

                Document.Back = MakeSections(back.section, 1);
                MakeCatalog(Document.Catalog, back.references);
                }
            }


        List<string> MakeString (List<asciitext> Text) {
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

        List<string> MakeKeywords(List<asciitext> keyword) {
            List<string> Result = new List<string>();

            if (keyword != null) {
                foreach (var s in keyword) {
                    Result.Add(s.Value);
                    }
                }

            return Result;
            }


        List<Author> MakeAuthors(List<author> authors) {
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
                    Author.Phone = author.address.phone.Value;
                    Author.URI = author.address.uri.Value;
                    Author.Email = author.address.email.Value;
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


            Result.ID = reference.anchor;
            Result.Target = reference.target;

            return Result;
            }

        List<SeriesInfo> MakeSeriesInfo(List<object> seriesInfos) {
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

        List<Format> MakeFormats (List<object> formats) {
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



        void MakeCatalog (Catalog Catalog, List<references> referencesArray) {
            if (referencesArray != null) {
                foreach (references references in referencesArray) {
                    References References = new References();
                    Catalog.ReferenceSections.Add(References);
                    References.Title = references.title;

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


        void FillTextBlock (List<object> Items, List<TextBlock> TextBlocks) {
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

        List<TextBlock> MakeTextBlocks (List<note> Notes) {
            List<TextBlock> Result = new List<TextBlock>();

            foreach (var Note in Notes) {
                FillTextBlock(Note.Items, Result);
                }
            return Result;
            }


        List<TextBlock> MakeTextBlocks(List<object> Items) {
            List<TextBlock> Result = new List<TextBlock>();

            FillTextBlock(Items, Result);

            return Result;
            }

        List<TextBlock> MakeTextBlocks (section section) {
            List<TextBlock> Result = new List<TextBlock>();

            FillTextBlock(section.Items, Result);

            return Result;
            }


        List<Section> MakeSections(List<section> sections, int level) {
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

            if ((list.t == null) || list.t.Count == 0) {
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


        void AddPre (List<TextBlock> Parent, List<string> Texts, string Anchor) {
            foreach (var Text in Texts) {
                PRE PRE = new PRE(Text, Anchor);
                Parent.Add(PRE);
                }
            }


        void AddFigureBlock(List<TextBlock> Parent, figure figure) {
            foreach (var Item in figure.Items) {
                switch (Item) {
                    case artwork artwork: {
                        AddPre(Parent, artwork.Text, figure.anchor);
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
                ID = texttable.anchor
                };
            
            TableRow TableRow = new TableRow();
            foreach (ttcol ttcol in texttable.ttcol) {
                TableData item = new TableData() {
                    IsHeading = true,
                    Text = ttcol.Value
                    };
                TableRow.Data.Add (item);
                }

            Table.MaxRow = TableRow.Data.Count;
            Table.Rows.Add (TableRow);

            int col = Table.MaxRow;
            foreach (c c in texttable.c) {
                TableData item = new TableData() {
                    IsHeading = false,
                    Text = MakeString(c.Items,c.Text)
                    };

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


        string MakeString(List<object> items, List<string> text) {
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
