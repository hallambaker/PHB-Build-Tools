using System;
using System.Collections.Generic;
using System.Text;

namespace Goedel.Tool.RFCTool {
    public partial class Document {
        public string           Title ="";
        public string           Abrrev = "";
        public string           Docname = "";
        public string           Version ="";

        public string FirstAuthor {
            get {
                if (Authors.Count == 0) return "";
                string [] Parts = Authors[0].Name.Split (null);
                return Parts [Parts.Length - 1];
                }
            }

        public string           FullDocName {
            get { return Version == null ? Docname : Docname + "-" + Version;}}

        public List<Author>     Authors = new List<Author>();

        public string           Year;
        public string           Month;
        public string           Day;


        public string Date { get { return ToDate(Day, Month, Year); } }

        public string           Ipr;
        public string           Area;
        public string           Workgroup;

        public string           Publisher = "Internet Engineering Task Force (IETF)";
        public List<string>     Keywords = new List<string>();
        // These have to be set up
        public string           ID1 = "Internet-Draft";
        public string           Status = "Standards Track";
        
        DateTime DocDate { get { return DateTime.Parse (Date);}}
        DateTime Expiring { get { return DocDate.AddDays(185); } }

        public string Expires { get { 
            return ToDate(Expiring.Day.ToString(), 
                    Months [Expiring.Month], Expiring.Year.ToString()); } }

        public string           Number;
        public string           Category;
        public string           Updates;
        public string           Obsoletes;
        public string           SeriesNumber;

        public Catalog          Catalog = new Catalog();

        public List<TextBlock>  Abstract = new List<TextBlock>();
        public List<Section>    Middle = new List<Section>();
        public List<Section>    Back=new List<Section>();

        //public List<Reference>   References = new List<Reference>();
        //public List<Reference>   Normative = new List<Reference>();
        //public List<Reference>   NonNormative = new List<Reference>();


        static string [] Months = {null, "January", "February", "March", "April", "May", "June", 
                               "July", "August", "September", "October", "November", "December"};

        static string ToDate (string Day, string Month, string Year) {
            string Format = "{1} {0}, {2}";
            return String.Format (Format, Day, Month, Year);
            }

        public Document() {
            DateTime now = DateTime.Now;
            Year = now.Year.ToString();
            Month = Months[now.Month];
            Day = now.Day.ToString();
            }

        public Document(string InputFile, string Format)
            : this() {

            //using (TextReader TextReader = new StreamReader(InputFile)) {
            if (Format == null || (Format.ToLower() == "html" | Format.ToLower() == "html2rfc")) {
                using (FileReader FileReader = new FileReader(InputFile)) {

                    NewParse NewParse = new NewParse(FileReader, this);
                    }
                }
            else if (Format.ToLower() == "xml" | Format.ToLower() == "xml2rfc"
                    | Format.ToLower() == "rfc2629") {
                using (FileReader FileReader = new FileReader(InputFile)) {

                    Rfc2629Parse Rfc2629Parse = new Rfc2629Parse (FileReader, this);
                    }
                }
            else {
                throw new Exception ("Format not recognized");
                }

            MakeAutomatics();
            }

        public void MakeAutomatics() {
            Catalog.AddDefaultSources();
            //Console.WriteLine();
            //foreach (Citation Citation in Catalog.Citations) {
            //Console.WriteLine("Citation Label {0} Normative {1}",
            //    Citation.Label, Citation.Normative);

            //    }
            Catalog.ResolveAll (this);  // Resolve any unresolved sources

            AddReferences();
            NumberSections();
            AddAuthors();
            }

        private static int CompareReferences(Reference First, Reference Second) {
            return string.Compare(First.Label, Second.Label);
            }

        public void AddReferences() {
            if ((Catalog.Normative.Count == 0) & (Catalog.Informative.Count == 0) &
                Catalog.ReferenceSections.Count == 0) return; // nothing to do
            Section References = new Section("References", "References");
            References.Automatic = true;
            Middle.Add(References);


            foreach (References RefSection in Catalog.ReferenceSections) {
                if (RefSection.Entries.Count > 0) {
                    RefSection.Entries.Sort(CompareReferences);
                    Section Sub = new Section(RefSection.Title, "NormativeReferences");
                    References.Subsections.Add(Sub);
                    foreach (Reference Reference in RefSection.Entries) {
                        Sub.TextBlocks.Add(Reference);
                        }
                    }
                }

            // 
            // These should be removed by making the normative and informative sections 
            // merely the first two default items in the references lists

            Catalog.Normative.Sort(CompareReferences);
            Catalog.Informative.Sort(CompareReferences);

            if (Catalog.Normative.Count > 0) {
                Section Sub = new Section("Normative References", "NormativeReferences");
                References.Subsections.Add(Sub);
                foreach (Reference Reference in Catalog.Normative) {
                    Sub.TextBlocks.Add(Reference);
                    }

                }
            if (Catalog.Informative.Count > 0) {
                Section Sub = new Section("Informative References", "InformativeReferences");
                References.Subsections.Add(Sub);
                foreach (Reference Reference in Catalog.Informative) {
                    Sub.TextBlocks.Add(Reference);
                    }
                }
            }


        public void AddAuthors() {
            string Head = Authors.Count == 1 ? "Author's Address" : "Authors' Addresses";
            Section Sub = new Section(Head, "AuthorsAddresses");
            Sub.Automatic = true;

            Sub.Number = "";
            foreach (Author Author in Authors) {
                Sub.TextBlocks.Add(Author);
                }
            Back.Add (Sub);
            }

        public void NumberSection(Section Section, int Number, int Level, string prefix, 
            string Punctuation, string idprefix) {
            Section.Level = Level;
            Section.Number = prefix + Punctuation;
            if (Section.ID == null) Section.ID = idprefix;

            int Index = 1;
            foreach (Section S in Section.Subsections) {
                string IS = Index.ToString();
                NumberSection(S, Index, Level + 1, prefix + Punctuation + IS, "." , 
                    idprefix + "_" + IS);
                Index++;
                }
            }


        public void NumberSections() {
            int Index = 1;
            foreach (Section S in Middle) {
                string IS = Index.ToString() ;
                NumberSection(S, Index, 1, IS, ".", "Section_" + IS);
                Index++;
                }
            Index = 0;
            foreach (Section S in Back) {
                string Letter = "" + (char) ('A' + Index);
                string IS = "Appendix " + Letter;
                NumberSection(S, Index, 1, IS, ":", "Appendix_" + Letter);
                Index++;
                }
            }

        public bool CheckNits() {
            bool Result = true;

            if (Authors.Count > 5) {
                ReportNit ("Too many authors, maximum is 5");
                }
            return Result; 
            }


        public virtual void ReportNit (string Nit) {
            Console.Write (Nit);
            }

        public static String [] StatusOfThisMemo = {
            "This Internet-Draft is submitted in full conformance with the " +
            "provisions of BCP 78 and BCP 79.",

            "Internet-Drafts are working documents of the Internet Engineering " +
            "Task Force (IETF).  Note that other groups may also distribute " +
            "working documents as Internet-Drafts.  The list of current Internet-" +
            "Drafts is at http://datatracker.ietf.org/drafts/current/.",

            "Internet-Drafts are draft documents valid for a maximum of six months " +
            "and may be updated, replaced, or obsoleted by other documents at any " +
            "time.  It is inappropriate to use Internet-Drafts as reference " +
            "material or to cite them other than as \"work in progress.\""         
            };

        public static string CopyrightDate = 
            "Copyright (c) {0} IETF Trust and the persons identified as the " +
            "document authors.  All rights reserved.";

        public static string [] CopyrightTerms = {
            "This document is subject to BCP 78 and the IETF Trust's Legal " +
            "Provisions Relating to IETF Documents " +
            "(http://trustee.ietf.org/license-info) in effect on the date of " +
            "publication of this document. Please review these documents " +
            "carefully, as they describe your rights and restrictions with respect " +
            "to this document. Code Components extracted from this document must " +
            "include Simplified BSD License text as described in Section 4.e of " +
            "the Trust Legal Provisions and are provided without warranty as " +
            "described in the Simplified BSD License."
           };

        public string CopyrightNotice {
            get { return String.Format (CopyrightDate, Year); }
            }
        }

    public class Author : TextBlock {
        public string                  Name;
        public string Initials;
        public string Surname;
        public string FirstName;
        public string                  Organization;
        public string                  OrganizationAbbrev;
        public string                  Street;
        public string                  City;
        public string                  Code;
        public string                  Country;
        public string                  Phone;
        public string                  Email;
        public string                  URI;
        }

    public class Section {
        public string                  Heading;
        public string                  ID;
        public string                  Number;
        public int                      Page=-1;
        public int                      Line=-1;
        public int Level;
        public List<TextBlock>         TextBlocks;
        public List<Section>           Subsections;

        public bool Automatic = false;

        public Section() : this (null, null) {
            }

        public Section(string Heading, string ID) {
            this.Heading = Heading; this.ID = ID;
            TextBlocks = new List<TextBlock>();
            Subsections = new List<Section>();
            }

        }


    public abstract class TextBlock {
        public string                  ID;
        public int                      Line, Position;
        public int                      Page;
        }

    public class P : TextBlock {
        public List <Text>              Chunks = new List<Text>();
        public string Text { get { return _Text; } }
        public string                   _Text;

        public P(string Text, string ID) {
            this.ID = ID;
            this._Text = Text;
            }

        public P(List <Text> Chunks, string ID) {
            this.ID = ID;
            this.Chunks = Chunks;
            }

        public void AddText(Text Text) {
            Chunks.Add (Text);
            }

        public void AddText(string Data) {
            Text Text = new Text (TextType.Plain);
            Text.Data = Data;
            AddText (Text);
            }

        public void AddText(TextType TextType, string Data, string Target, string Title) {
            Text Text = new Text (TextType);
            Text.Data = Data; Text.Target = Target; Text.Title = Title;
            AddText(Text);
            }

        public void AddIndex(string Index, string SubIndex) {
            Text Text = new Text (TextType.Index);
            Text.Index = Index; Text.SubIndex = SubIndex;
            AddText(Text);
            }

        public string GetText() {
            return null;
            }

        }

    public class PRE : P  {

        public PRE (string Text, string ID) : base (Text, ID){

            }
        }

    public enum ListItem {
        Ordered, Symbol, Definitions, Term, Data
        }

    public class LI : P {
        public ListItem         Type;
        public int              Level;
        public int              Index;

        public LI (string Text, string ID, ListItem Type, int Level) : 
                    base (Text, ID){
            this.Type = Type; this.Level = Level;
            }


        public LI (string Text, string ID, ListItem Type, int Level, int Index) : 
                    this (Text, ID, Type, Level){
            this.Index = Index;
            }
        }


    public class ListBlock : LI {

        public List<TextBlock> Items = new List<TextBlock> ();
        public ListBlock(string Text, string ID, ListItem Type, int Level) : 
                base (null, ID, Type, Level) {
            }
        }


    public class Table : TextBlock {
        public int MaxRow = 0;
        public List<int>                   Percent = new List<int>();
        public List<int>                   Width = new List<int>();
        public List<TableRow>              Rows = new List<TableRow>();
        }

    public class TableRow : TextBlock {
        public List <TableData>            Data = new List<TableData>();
        }

    public class TableData : TextBlock {
        public bool                        IsHeading;
        public string               Text;
        }





    public enum TextType {
        Plain,

        CrossRef,
        ExternalRef,
        Index,
        Comment,

        Emphasis,
        Strong,
        //Code,
        Keyboard,

        Definition,
        Citation,
        Normative

        }

    public class Text {
        public TextType            Type;
        public string              Data;        // What goes in the text
        public string              Target;      // Target of hypertext link
        public string              Title;       // For mouseover popup text
        public string              Index;
        public string              SubIndex;
        public bool                ReplaceText;
        public Text(TextType TextType) {
            Type = TextType;
            }
        }

    public class SeriesInfo {
        public string               Name;
        public string               Value;
        }

    public class Format {
        public string               Type;
        public string               Octets;
        public string               Target;
        }

    public class Reference : TextBlock {
        public string              Label;

        public string               Target;

        public string               Title;
        public string               Abbrev;
        public string               Version; // not needed?
        public string               Area;
        public string               Workgroup;
        public List<Author>         Authors = new List<Author>();
        public string               Year;
        public string               Month;
        public string               Day;
        public List<string>         Keywords = new List<string>();
        public List<string>         Abstract = new List<string>();
        public List<SeriesInfo>     SeriesInfos = new List<SeriesInfo>();
        public List<Format>         Formats = new List<Format>();
        public List<string>         Annotation = new List<string>();
        }


    public enum RequirementLevel {
        MUST,
        SHOULD,
        RECOMMENDED,
        MAY
        }

    public class Requirement {
        public RequirementLevel    Level;
        public string              Text;
        }
    }
