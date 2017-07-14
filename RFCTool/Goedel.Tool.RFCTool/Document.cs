using System;
using System.Collections.Generic;
using System.Text;
using GM = Goedel.Document.Markdown;

namespace Goedel.Tool.RFCTool {
    public partial class Document {

        public GM.Document Source = null;

        // Constants for inside the text
        //public string           Publisher = "Internet Engineering Task Force (IETF)";
        //public string           ID1 = "Internet-Draft";
        //public string           Status = "Standards Track";


        // Attributes from tag <RFC>
        public string Number;
        public string Obsoletes;
        public string Updates;
        public string Category;
        public string Consensus;
        public string SeriesNumber;
        public string Ipr;
        public string IprExtract;
        public string SubmissionType; // see seriesinfo
        public string Docname = "";

        // I see no reason at all to allow these to be varied. The TOC is
        // for the benefit of the reader, not the writer.
        public bool SortRefs = true;
        public bool Symrefs = true;
        public bool TocInclude = true;
        public int TocDepth = 3;
        public bool IndexInclude = false;

        public string Version = "";
        public string Scripts = "Common,Latin";
        public string ExpiresDate = null;

        public List<Link> Links = new List<Link>();

        // Collected attributes and elements from <front>
        public string Title = "";
        public string TitleAbrrev = "";
        public string TitleAscii = "";
        public List<Author> Authors = new List<Author>();

        public string Year;
        public string Month;
        public string Day;

        public List<string> Area = new List<string>();      // UNUSED
        public List<string> Workgroup = new List<string>();
        public List<string> Keywords = new List<string>();
        public List<TextBlock> Abstract = new List<TextBlock>();
        public List<TextBlock> Note = new List<TextBlock>();
        public List<Section> Boilerplate = new List<Section>();
        public List<Section> Middle = new List<Section>();
        public List<Section> Back = new List<Section>();
        public List<SeriesInfo> SeriesInfos = new List<SeriesInfo>();

        // Calculated attributes
        public DateTime DocDate { get => DateTime.Parse(Date); }
        public DateTime Expiring { get => DocDate.AddDays(185); }
        public string Expires {
            get => ToDate(Expiring.Day.ToString(), Months[Expiring.Month], Expiring.Year.ToString());
            }
        public string FullDocName {
            get => Version == null ? Docname : Docname + "-" + Version;
            }
        public string Date { get => ToDate(Day, Month, Year); }
        public string FirstAuthor {
            get {
                if (Authors.Count == 0) {
                    return "";
                    }
                string[] Parts = Authors[0].Name.Trim().Split(null);
                return Parts[Parts.Length - 1];
                }
            }
        public bool IsConsensus { get => Consensus == "yes" | Consensus == "true"; }
        public bool IsDraft { get => Series == "draft"; }


        public SeriesInfo SeriesInfo { get => SeriesInfos.Count > 0 ? SeriesInfos[0] : null; }
        public string Stream { get => SeriesInfo?.Stream ?? "ietf"; }
        public string Status { get => SeriesInfo?.Status ?? "standard"; }
        public string Series { get => SeriesInfo?.Name ?? "draft"; }


        public string WorkgroupText {
            get => Workgroup?.Count > 0 ? Workgroup[0] : null;
            } // ToDo: concatenate working groups       
        public string StreamText;
        public string SeriesText;
        public string StatusText;

        public Catalog          Catalog = new Catalog();

        // End data from the schema


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

                    new NewParse(FileReader, this);
                    }
                }
            else if (Format.ToLower() == "xml" | Format.ToLower() == "xml2rfc"
                    | Format.ToLower() == "rfc2629") {
                using (FileReader FileReader = new FileReader(InputFile)) {

                    new Rfc7991Parse (FileReader, this);
                    }
                }
            else {
                throw new Exception ("Format not recognized");
                }

            MakeAutomatics();
            }

        public void MakeAutomatics() {
            AutoSetVersion();
            RFCEditorBoilerplate.Set(this);
            Catalog.AddDefaultSources();
            Catalog.ResolveAll (this);  // Resolve any unresolved sources
            AddReferences();
            NumberSections();
            AddAuthors();
            }



        void AutoSetVersion () {
            if (!IsDraft | (Version != null & Version != "")) {
                return; // Already set
                }
            Version = RFCTool.Source.GetDraftVersion (Docname);
            // Docname

            }



        private static int CompareReferences(Reference First, Reference Second) {
            return string.Compare(First.Label, Second.Label);
            }

        public void AddReferences () {
            if ((Catalog.Normative.Count == 0) & (Catalog.Informative.Count == 0) &
                Catalog.ReferenceSections.Count == 0) {
                return;
                }// nothing to do
            Section References = new Section("References", "References") {
                Automatic = true,
                SuppressNumbering = true
                };
            Middle.Add(References);

            // 
            // These should be removed by making the normative and informative sections 
            // merely the first two default items in the references lists

            Catalog.Normative.Sort(CompareReferences);
            Catalog.Informative.Sort(CompareReferences);

            if (Catalog.Normative.Count > 0) {
                Section Sub = new Section("Normative References", "NormativeReferences") {
                    SuppressNumbering = true
                    };
                References.Subsections.Add(Sub);
                foreach (Reference Reference in Catalog.Normative) {
                    Sub.TextBlocks.Add(Reference);
                    }

                }
            if (Catalog.Informative.Count > 0) {
                Section Sub = new Section("Informative References", "InformativeReferences") {
                    SuppressNumbering = true
                    };
                References.Subsections.Add(Sub);
                foreach (Reference Reference in Catalog.Informative) {
                    Sub.TextBlocks.Add(Reference);
                    }
                }
            }


        public void AddAuthors() {
            string Head = Authors.Count == 1 ? "Author's Address" : "Authors' Addresses";
            Section Sub = new Section(Head, "AuthorsAddresses") {
                Automatic = true
                };

            Sub.Number = "";
            foreach (Author Author in Authors) {
                Sub.TextBlocks.Add(Author);
                }
            Back.Add (Sub);
            }

        public void NumberSection (Section Section, 
                int Number, int Level, string prefix,
                string Punctuation, string idprefix, string sectionprefix) {
            Section.Level = Level;
            
            Section.Number = prefix + Punctuation;
            Section.SectionID = "n-" + GetAnchor(Section.Heading);
            Section.NumericID = "s-" + prefix;
            if (Section.ID == null) {
                Section.ID = idprefix;
                }


            int Index = 1;
            foreach (Section S in Section.Subsections) {
                string IS = Index.ToString();
                NumberSection(S, Index, Level + 1, prefix + Punctuation + IS, "." , 
                    idprefix + "_" + IS, sectionprefix);
                Index++;
                }

            Index = 1;
            foreach (TextBlock Text in Section.TextBlocks) {
                if (Text as Reference == null) {
                    string IS = Index.ToString();
                    Text.ID = Section.NumericID + "-" + IS;
                    }
                }
            }


        public void NumberSections() {
            int Index = 1;
            foreach (Section S in Middle) {
                string IS = Index.ToString() ;
                NumberSection(S, Index, 1, IS, ".", "s-" + IS, GetAnchor(S.Heading));
                Index++;
                }
            Index = 0;
            foreach (Section S in Back) {
                string Letter = "" + (char) ('A' + Index);
                string IS = "Appendix " + Letter;
                NumberSection(S, Index, 1, IS, ":", "a-" + Letter, GetAnchor(S.Heading));
                Index++;
                }
            }


        string GetAnchor (string Text) => Text.Replace(" ", "-").ToLower();


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
        }


    public class Link {
        public string Href;
        public string Class;
        }

    public class Author : TextBlock {

        public override BlockType BlockType { get => BlockType.Author; }

        public string Name;
        public string Initials;
        public string Surname;
        public string FirstName;
        public string Role;
        public string Organization;
        public string OrganizationAbbrev;
        public string OrganizationAscii;
        public string Street;
        public string City;
        public string Region;
        public string Code;
        public string Country;
        public string Phone;
        public string Email;
        public string URI;
        }

    public class Section {
        public string                   Heading;
        public string                   ID;
        public string                   SectionID;
        public string                   NumericID;

        string _Number = "";
        public string Number {
            get => _Number;
            set { if (!SuppressNumbering) { _Number = value; } }
            }

        public int                      Page=-1;
        public int                      Line=-1;
        public int Level;
        public List<TextBlock>         TextBlocks;
        public List<Section>           Subsections;

        public bool Automatic = false;
        public bool RemoveInRFC = false;
        public bool SuppressNumbering = false;

        public Section() : this (null, null) {
            }

        public Section(string Heading, string ID) {
            this.Heading = Heading; this.ID = ID;
            TextBlocks = new List<TextBlock>();
            Subsections = new List<Section>();
            }

        }

    /// <summary>Text Block Types</summary>
    public enum BlockType {
        /// <summary>Formatted paragraph</summary>
        Paragraph,
        /// <summary>Verbatim paragraph</summary>
        Verbatim,
        /// <summary>Numbered list</summary>
        Ordered,
        /// <summary>Bullet List</summary>
        Symbol,
        /// <summary>Definitions</summary>
        Definitions,
        /// <summary>Defined Term</summary>
        Term,
        /// <summary>Definition</summary>
        Data,
        /// <summary>Table</summary>
        Table,
        /// <summary>Table Row</summary>
        TableRow,
        /// <summary>Table Data</summary>
        TableData,
        /// <summary>Reference</summary>
        Reference,
        /// <summary>Author</summary>
        Author,
        /// <summary>Figure</summary
        Figure,
        /// <summary>Artwork</summary
        Artwork,
        /// <summary>BlockQuote</summary
        BlockQuote,
        /// <summary>BlocSourceCodekQuote</summary
        SourceCode,
        /// <summary>Null type</summary
        Null
        }

    public abstract class TextBlock {
        public string ID;  // The id used in <p>, <h2>, <h3>, etc.
        public string SectionID;
        public string NumericID;
        public int Line, Position;
        public int Page;

        public abstract BlockType BlockType {get;}

        }


    public class Figure : TextBlock {
        public override BlockType BlockType { get => BlockType.Figure; }

        public string Filename;

        public Figure (string Filename, string ID) {
            this.ID = ID;
            this.Filename = Filename;
            }
        }


    public class P : TextBlock {
        public List <Text>              Chunks = new List<Text>();
        public string Text { get => _Text; } 
        public string                   _Text;

        public override BlockType BlockType { get => BlockType.Paragraph; } 

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
            Text Text = new Text(TextType.Plain) {
                Data = Data
                };
            AddText (Text);
            }

        public void AddText(TextType TextType, string Data, string Target, string Title) {
            Text Text = new Text(TextType) {
                Data = Data,
                Target = Target,
                Title = Title
                };
            AddText(Text);
            }

        public void AddIndex(string Index, string SubIndex) {
            Text Text = new Text(TextType.Index) {
                Index = Index,
                SubIndex = SubIndex
                };
            AddText(Text);
            }

        public string GetText() {
            return null;
            }

        }

    public class PRE : P  {
        public override BlockType BlockType { get => BlockType.Verbatim; } 

        public PRE (string Text, string ID) : base (Text, ID){

            }
        }



    public class LI : P {
        public BlockType         Type;
        public int              Level;
        public int              Index;
        public override BlockType BlockType { get => Type; } 

        public LI (string Text, string ID, BlockType Type, int Level) : 
                    base (Text, ID){
            this.Type = Type; this.Level = Level;
            }


        public LI (string Text, string ID, BlockType Type, int Level, int Index) : 
                    this (Text, ID, Type, Level){
            this.Index = Index;
            }
        }


    public class ListBlock : LI {

        public List<TextBlock> Items = new List<TextBlock> ();
        public ListBlock(string Text, string ID, BlockType Type, int Level) : 
                base (null, ID, Type, Level) {
            }
        }


    public class Table : TextBlock {
        public override BlockType BlockType { get => BlockType.Table; } 

        public int MaxRow = 0;
        public List<int>                   Percent = new List<int>();
        public List<int>                   Width = new List<int>();
        public List<TableRow>              Rows = new List<TableRow>();
        }

    public class TableRow : TextBlock {
        public override BlockType BlockType { get => BlockType.TableRow; } 
        public List <TableData>            Data = new List<TableData>();
        }

    public class TableData : TextBlock {
        public override BlockType BlockType { get => BlockType.TableData; } 
        public bool                        IsHeading;
        public string               Text;
        }




    /// <summary>
    /// Types of text occurring within paragraph blocks.
    /// </summary>
    public enum TextType {
        /// <summary>Standard text type</summary>
        Plain,
        /// <summary>Cross reference</summary>
        CrossRef,
        /// <summary>Reference to external data</summary>
        ExternalRef,
        /// <summary>Index term</summary>
        Index,
        /// <summary>Comment</summary>
        Comment,

        /// <summary>Typically italics</summary>
        Emphasis,
        /// <summary>Typically bold</summary>
        Strong,
        /// <summary>Identifiers, code, etc.</summary>
        Code,
        /// <summary>Human input</summary>
        Keyboard,

        /// <summary>Definition of term</summary>
        Definition,
        /// <summary>Citation of reference</summary>
        Citation,
        /// <summary>Normative text</summary>
        Normative,


        /// <summary>Null type</summary>
        Null
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
        public string AsciiName;
        public string AsciiValue;
        public string Name;         // series draft/rfc/w3c
        public string Value;        // identifier in series 822 or dreaft-hallambaker-fred-00
        public string Status;       // standard/informational/experimental/bcp/fyi/full-standard
        public string Stream;       // IETF/IAB/IRTF/independent


        public string Version {
            get {
                if (Name == "rfc") {
                    return "";
                    }
                var Result = Value.Substring(Value.Length - 2);
                return Result;
                }
            }
        }

    public class Format {
        public string               Type;
        public string               Octets;
        public string               Target;
        }

    public class Reference : TextBlock {
        public override BlockType BlockType { get => BlockType.Reference; }

        public string Label;

        public string Target;

        public string Title;
        public string Abbrev;

        public List<string> Area;
        public List<string> Workgroup;
        public List<Author> Authors = new List<Author>();
        public string Year;
        public string Month;
        public string Day;
        public List<string> Keywords = new List<string>();
        public List<string> Abstract = new List<string>();
        public List<SeriesInfo> SeriesInfos = new List<SeriesInfo>();
        public List<Format> Formats = new List<Format>();
        public List<string> Annotation = new List<string>();


        public string Version {
            get {
                if (SeriesInfos == null || SeriesInfos?.Count == 0) {
                    return "??";
                    }
                return SeriesInfos[0].Version;
                }
            }

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
