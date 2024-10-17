using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GM = Goedel.Document.Markdown;
using Goedel.Utilities;
using Goedel.Document.RFCSVG;

namespace Goedel.Document.RFC;

/// <summary>
/// A document in internal representation.
/// </summary>
public partial class BlockDocument {

    ///<summary>The source document (if markdown)</summary> 
    public GM.MarkdownDocument Source = null;

    ///<summary>The time the document was processed.</summary> 
    public DateTime PrepTime = DateTime.Now;

    ///<summary>Dictionary mapping cross reference strings to targets.</summary> 
    public Dictionary<string, XRefTarget> XRefDictionary = new();

    ///<summary>Set of assigned identifiers.</summary> 
    public SortedSet<string> AssignedIDs = new();


    // Attributes from tag <RFC>


    ///<summary>RFC attribute, the RFC number</summary> 
    public string Number;

    ///<summary>RFC attribute, the document replaced by this one </summary> 
    public string Obsoletes;

    ///<summary>RFC attribute, the document updated by this one</summary> 
    public string Updates;

    ///<summary>RFC attribute, the category.</summary> 
    public string Category;

    ///<summary>RFC attribute, the consensus value.</summary> 
    public string Consensus;

    ///<summary>RFC attribute, the series number</summary> 
    public string SeriesNumber;

    ///<summary>RFC attribute, the IPR identifier</summary> 
    public string Ipr;

    ///<summary>RFC attribute, the IPR description</summary> 
    public string IprExtract;
    ///<summary>RFC attribute, the submission type</summary> 
    public string SubmissionType; // see seriesinfo

    ///<summary>RFC attribute, the document name</summary> 
    public string Docname = "";

    // I see no reason at all to allow these to be varied. The TOC is
    // for the benefit of the reader, not the writer.
    public int TocDepth = 3;

    public bool SortRefs = true;
    public bool Symrefs = true;
    public bool TocInclude = true;
    public bool TofInclude = true;
    public bool TotInclude = true;
    public bool TonInclude = true;
    public bool IndexInclude = false;
    public bool EmbedStylesheet = true;
    public int EmbedSVG = 0;                // Default, embed as data: uri


    public string Scripts = "Common,Latin";
    public string ExpiresDate = null;

    public List<Link> Links = new();

    // Collected attributes and elements from <front>
    public string TitleFull => IsDraft ? Title : FullDocName + ": " + Title;

    public string Title = "";
    public string TitleAbrrev = "";
    public string TitleAscii = "";
    public List<Author> Authors = new();

    public string Year;
    public string Month;
    public string Day;

    public List<string> Area = new();      // UNUSED
    public List<string> Workgroup = new();
    public List<string> Keywords = new();
    public List<TextBlock> Abstract = new();
    public List<List<TextBlock>> Note = new();
    public List<Section> Boilerplate = new();
    public List<Section> Middle = new();
    public List<Section> Back = new();
    public List<SeriesInfo> SeriesInfos = new();


    public string Also = null;
    public string WorkgroupCombined;
    public string AreaCombined;

    // Calculated attributes
    public DateTime DocDate => DateTime.Parse(Date);
    public DateTime Expiring => DocDate.AddDays(185);
    public string Expires => ToDate(Expiring.Day.ToString(), Months[Expiring.Month], Expiring.Year.ToString());

    public string FullDocName => IsDraft ?
        (Version == null ? Docname : Docname + "-" + Version) : "RFC" + SeriesInfo.Value;


    public string Date => ToDate(Day, Month, Year);
    public string FirstAuthor {
        get {
            if (Authors.Count == 0) {
                return "";
                }
            string[] Parts = Authors[0].Name.Trim().Split(null);
            return Parts[Parts.Length - 1];
            }
        }
    public bool IsConsensus => Consensus == "yes" | Consensus == "true";
    public bool IsDraft => Series == "Internet-Draft" | Series == "draft";

    public SeriesInfo SeriesInfo;
    public string Stream => SeriesInfo?.Stream ?? "ietf";
    public string Status => Category ?? SeriesInfo?.Status ?? "standard";
    public string Series => SeriesInfo?.Name ?? "draft";
    public string Version {
        get => SeriesInfo?.AutoVersion;
        set { }
        }

    public string WorkgroupText => Workgroup?.Count > 0 ? Workgroup[0] : null; // ToDo: concatenate working groups       

    public StringSet StreamTexts;
    public StringSet SeriesTexts;
    public StringSet StatusTexts;


    public string StreamText;
    public string SeriesText;
    public string StatusText;

    public Catalog Catalog = new();

    // End data from the schema



    public List<Figure> TableOfFigures = new();
    public List<Table> TableOfTables = new();


    static string[] Months = {null, "January", "February", "March", "April", "May", "June",
                               "July", "August", "September", "October", "November", "December"};

    static string ToDate(string Day, string Month, string Year) {
        string Format = "{1} {0}, {2}";
        return String.Format(Format, Day, Month, Year);
        }

    public BlockDocument() {
        DateTime now = DateTime.Now;
        Year = now.Year.ToString();
        Month = Months[now.Month];
        Day = now.Day.ToString();
        }

    public BlockDocument(string InputFile, string Format)
        : this() {

        if (Format == null || (Format.ToLower() == "html" | Format.ToLower() == "html2rfc")) {
            using FileReader FileReader = new(InputFile);
            new NewParse(FileReader, this);
            }
        else if (Format.ToLower() == "xml" | Format.ToLower() == "xml2rfc"
                | Format.ToLower() == "rfc2629") {
            using FileReader FileReader = new(InputFile);
            new Rfc7991Parse(FileReader, this);
            }
        else {
            throw new Exception("Format not recognized");
            }

        MakeAutomatics();
        }

    /// <summary>
    /// Perform all automated expansions of data to inline, etc. sections.
    /// </summary>
    public void MakeAutomatics() {
        ParseSeriesInfos();
        RFCEditorBoilerplate.Set(this);
        Catalog.AddDefaultSources();
        Catalog.ResolveAll(this);  // Resolve any unresolved sources
        AddReferences();
        NumberSections();
        AddAuthors();
        }

    /// <summary>
    /// Convert information from the various locations successive schema versions put 
    /// it to a canonical form.
    /// </summary>
    public void ParseSeriesInfos() {
        SeriesInfo = new SeriesInfo();

        foreach (var seriesInfo in SeriesInfos) {
            switch (seriesInfo.Name.ToLower()) {
                case "rfc": {
                        SeriesInfo.Name = "RFC";
                        SeriesInfo.Value = seriesInfo.Value;
                        break;
                        }
                case "draft":
                case "internet-draft": {
                        SeriesInfo.Name = "Internet-Draft";
                        SeriesInfo.Value = seriesInfo.Value;
                        break;
                        }
                case "doi": {
                        SeriesInfo.DOI = seriesInfo.Value;
                        break;
                        }
                }
            SeriesInfo.Status ??= seriesInfo.Status?.ToLower();
            SeriesInfo.Stream ??= seriesInfo.Stream;
            }

        SeriesInfo.Status ??= Category;
        SeriesInfo.Stream ??= SubmissionType;

        switch (SeriesInfo.Stream?.ToLower()) {

            case "iab": {
                    SeriesInfo.Stream = "IAB";
                    break;
                    }
            case "irtf": {
                    SeriesInfo.Stream = "IRTF";
                    break;
                    }
            case "independent": {
                    SeriesInfo.Stream = "independent";
                    break;
                    }
            default: {
                    SeriesInfo.Stream = "IETF";
                    break;
                    }
            }

        switch (SeriesInfo.Status?.ToLower()) {
            case "standard":
            case "std": {
                    SeriesInfo.Status = "std";
                    break;
                    }
            case "bcp": {
                    SeriesInfo.Status = "bcp";
                    break;
                    }
            case "fyi":
            case "informational":
            case "info": {
                    SeriesInfo.Status = "info";
                    break;
                    }
            case "experimental":
            case "exp": {
                    SeriesInfo.Status = "exp";
                    break;
                    }
            case "his":
            case "historic": {
                    SeriesInfo.Status = "historic";
                    break;
                    }
            }


        }


    private static int CompareReferences(Reference First, Reference Second) => string.Compare(First.Label, Second.Label);

    public void AddReferences() {
        if ((Catalog.Normative.Count == 0) & (Catalog.Informative.Count == 0) &
            Catalog.ReferenceSections.Count == 0) {
            return;
            }// nothing to do
        Section References = new("References", "n-references") {
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
            Section Sub = new("Normative References", "n-normative") {
                SuppressNumbering = true,
                };
            References.Subsections.Add(Sub);
            foreach (Reference Reference in Catalog.Normative) {
                Sub.TextBlocks.Add(Reference);
                }

            }
        if (Catalog.Informative.Count > 0) {
            Section Sub = new("Informative References", "n-informative") {
                SuppressNumbering = true
                };
            References.Subsections.Add(Sub);
            foreach (Reference Reference in Catalog.Informative) {
                Sub.TextBlocks.Add(Reference);
                }
            }
        }

    public string AuthorSectionTitle;

    public void AddAuthors() {
        AuthorSectionTitle = Authors.Count == 1 ? "Author's Address" : "Authors' Addresses";
        Section Sub = new(AuthorSectionTitle, "n-authors") {
            Automatic = true
            };

        Sub.Number = "";
        foreach (Author Author in Authors) {
            Sub.TextBlocks.Add(Author);
            }
        Back.Add(Sub);
        }


    public void NumberTextBlocks(string NumericID, string sectionNumber, ref List<TextBlock> TextBlocks) {

        // ToDo: keep the blocks but reset the list with a new one with explicit 
        //    nesting of list items.

        var Index = 0;

        if (TextBlocks != null) {
            foreach (TextBlock Text in TextBlocks) {
                if (Text as Reference == null) {
                    Index++;
                    string IS = Index.ToString();
                    Text.GeneratedID = NumericID + "-" + IS;
                    Text.NumericID = IS;
                    Text.SectionId = sectionNumber;
                    }

                if (Text as Figure != null) {
                    var Figure = Text as Figure;
                    TableOfFigures.Add(Text as Figure);
                    Figure.NumericID = TableOfFigures.Count.ToString();
                    Figure.GeneratedID = Figure.AnchorID ?? "n-" + GetAnchor(Figure.Caption);
                    }
                if (Text.AnchorID != null) {
                    var target = new XRefTarget() {
                        TextBlock = Text
                        };
                    XRefDictionary.AddSafe(Text.AnchorID, target);
                    }
                }
            }
        }


    string UniqueifyID(string id) {

        if (AssignedIDs.Contains(id)) {
            var baseid = id;
            for (var i = 0; AssignedIDs.Contains(id); i++) {
                id = baseid + "-" + i.ToString();
                }
            }
        AssignedIDs.Add(id);

        return id;
        }

    /// <summary>
    /// Number a section
    /// </summary>
    /// <param name="section">The section to number</param>
    /// <param name="number">The number relative to its peers</param>
    /// <param name="level">The depth (for cutting the TOC)</param>
    /// <param name="numericIDPrefix">Prefix for the numeric ID</param>
    /// <param name="TextID">The text representation of Number</param>
    /// <param name="textSuffix">Suffix to put after number for display</param>

    /// <param name="textPrefix">Prefix to put in front of user for display</param>
    /// <param name="numeric">If true the number is turned into a numeric section ID</param>
    public void NumberSection(
            Section section,
        int number,
        int level,
        string numericIDPrefix,
        string textSuffix,
        string textNumber = "",
        string textPrefix = "",
        bool numeric = true) {

        section.Level = level;
        var numberAsText = numeric ? number.ToString() : "" + ('A' + number);
        textNumber = textNumber + numberAsText + textSuffix;

        section.Number = textSuffix != null ? textPrefix + textNumber : "";

        section.SetableID ??= "n-" + GetAnchor(section.Heading);      // For H1, H2, H3, etc.

        section.SetableID = UniqueifyID(section.SetableID);



        section.GeneratedID = numericIDPrefix + numberAsText;                      // For the toc ref and sub paras

        //if (Section.GeneratedID == null) {
        //    Section.GeneratedID = NumericIDPrefix;
        //    }

        // Do this before we number subsections or else the figure numbers will turn out weird.
        NumberTextBlocks(section.GeneratedID, textNumber, ref section.TextBlocks);

        int index = 1;
        foreach (Section s in section.Subsections) {
            string IS = index.ToString();
            NumberSection(s, index, level + 1,
            section.GeneratedID + "_", ".", textNumber: textNumber);
            index++;
            }



        if (section.SetableID != null) {
            var target = new XRefTarget() {
                Section = section
                };
            XRefDictionary.AddSafe(section.SetableID, target);
            }
        }


    public void NumberSections() {
        NumberTextBlocks("section-abstract", "Abstract", ref Abstract);
        //NumberTextBlocks("s-note", ref Note);

        int Index = 1;

        foreach (Section S in Boilerplate) {
            NumberSection(S, Index, 1, "section-boilerplate-", null);
            Index++;
            }

        Index = 1;

        foreach (Section S in Middle) {
            NumberSection(S, Index, 1, "section-", ".");
            Index++;
            }
        //Index = 0;
        foreach (Section S in Back) {
            NumberSection(S, Index, 1, "section-", ":", textPrefix: "Appendix ", numeric: false);
            Index++;
            }
        }


    string GetAnchor(string Text) => Text == null ? "undefined" : Clean(Text);


    static string Clean(string text) {
        var builder = new StringBuilder();
        text = text.ToLower();
        foreach (var c in text) {
            if ((c >= 'A' & c <= 'Z') | (c >= 'a' & c <= 'z') | (c >= '0' & c <= '9')) {
                builder.Append(c);
                }
            else if (c == ' ') {
                builder.Append('-');
                }
            }

        return builder.ToString();

        }

    public bool CheckNits() {
        bool Result = true;

        if (Authors.Count > 5) {
            ReportNit("Too many authors, maximum is 5");
            }
        return Result;
        }

    public virtual void ReportNit(string Nit) => Console.Write(Nit);


    public XRefTarget AddAnchor(string xref, Section section) {
        if (!XRefDictionary.TryGetValue(xref, out var xRefTarget)) {
            xRefTarget = new XRefTarget();
            XRefDictionary.Add(xref, xRefTarget);
            }
        xRefTarget.Section = section;
        return xRefTarget;
        }

    public XRefTarget AddAnchor(string xref, TextBlock textBlock) {
        if (!XRefDictionary.TryGetValue(xref, out var xRefTarget)) {
            xRefTarget = new XRefTarget();
            XRefDictionary.Add(xref, xRefTarget);
            }
        xRefTarget.TextBlock = textBlock;
        return xRefTarget;
        }

    public XRefTarget AddXref(string xref) {
        if (!XRefDictionary.TryGetValue(xref, out var xRefTarget)) {
            xRefTarget = new XRefTarget();
            XRefDictionary.Add(xref, xRefTarget);
            }
        return xRefTarget;
        }

    }


public class Link {
    public string Href;
    public string Class;
    public string Rel;
    }

public class Author : TextBlock {
    public override string SectionText => null;
    public override BlockType BlockType => BlockType.Author;

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
    public string Heading;
    public List<GM.TextSegment> Segments;

    public string GeneratedID;
    public string SetableID = null;
    //public string                   NumericID;

    string _Number = "";
    public string Number {
        get => _Number;
        set { if (!SuppressNumbering) { _Number = value; } }
        }

    public int Page = -1;
    public int Line = -1;
    public int Level;
    public List<TextBlock> TextBlocks;
    public List<Section> Subsections;

    public bool Automatic = false;
    public bool RemoveInRFC = false;
    public bool SuppressNumbering = false;

    public Section() : this(null, null) {
        }

    public Section(string Heading, string ID) {
        this.Heading = Heading;
        this.SetableID = ID;
        TextBlocks = new List<TextBlock>();
        Subsections = new List<Section>();
        }

    }

/// <summary>
/// Text block class, parent for all text blocks
/// </summary>
public abstract class TextBlock {
    ///<summary>The block type</summary> 
    public abstract BlockType BlockType { get; }

    ///<summary>The anchor text.</summary> 
    public string AnchorText => GeneratedID;

    ///<summary>Generated identifier.</summary> 
    public string GeneratedID;  // The id used in <p>, <h2>, <h3>, etc.
    string anchorID;
    public string AnchorID {
        get => anchorID == "" ? null : anchorID;
        set => anchorID = value;
        }

    public string SectionId = "tbs";
    public string NumericID = "tbs";

    public string Align;

    // These are the identifiers to use in future.
    public string PnID = null; // probablyu collapse to GeneratedID

    public abstract string SectionText { get; }
    public int Line, Position;
    public int Page;



    public List<GM.TextSegment> Irefs;

    public List<GM.TextSegment> BlockName;
    }

/// <summary>
/// Text block containing a figure.
/// </summary>
public class Figure : TextBlock {

    ///<inheritdoc/>
    public override BlockType BlockType => BlockType.Figure;
    public override string SectionText => "Figure " + NumericID;
    public string FigureID => "f-" + NumericID;

    public List<GM.TextSegment> Preamble;
    public List<GM.TextSegment> Postamble;

    public string Caption;

    public string Width;
    public string Height;

    public string Filename;

    public List<PRE> Content = new();
    public string Type;

    public SvgDocument SvgDocument = null;

    public Figure(string filename, string id, string type = "svg") {
        AnchorID = id;
        Filename = filename;
        Type = type;

        if (filename != null) {
            SvgDocument = new SvgDocument(filename);
            }

        }

    public void SaveContent(TextWriter output) {
        SvgDocument?.Save(output);
        }

    }


public class P : TextBlock {
    public override string SectionText => "Paragraph " + NumericID;
    public List<GM.TextSegment> Segments;


    public string Text => GetText();
    public override BlockType BlockType => BlockType.Paragraph;

    public P() {
        }


    public P(string Text, string ID) {
        this.AnchorID = ID;
        Segments ??= new List<GM.TextSegment>();
        Segments.Add(new GM.TextSegmentText(Text));
        }

    public string GetText() {
        var Buffer = new StringBuilder();

        foreach (var Segment in Segments) {
            switch (Segment) {
                case GM.TextSegmentText Text: {
                        Buffer.Append(Text.Text.XMLEscapeRFCBullies());
                        break;
                        }
                }
            }
        return Buffer.ToString();
        }

    }

public class PRE : P {

    public string Element;

    public string Language;
    public string Filename;
    public string OutputFile;
    public string Alt;

    public override BlockType BlockType => BlockType.Verbatim;
    public PRE() : base() {

        }
    public PRE(string Text, string ID) : base(Text, ID) {

        }

    public string TextSegments => FromSegments(Segments);

    string FromSegments(List<GM.TextSegment> Segments) {
        var Builder = new StringBuilder();

        foreach (var Segment in Segments) {
            switch (Segment) {
                case GM.TextSegmentText TextSegmentText:
                    Builder.Append(TextSegmentText.Text);
                    break;
                }
            }

        return Builder.ToString();
        }

    }



public class LI : P {
    public BlockType Type;
    public int Level;
    public int Index;

    public string Format;
    public string Group;
    public string Spacing;

    public string EnclosingAnchorID;
    public List<TextBlock> Content;

    public string Empty => Format == "empty" ? "true" : null;

    public override BlockType BlockType => Type;

    public LI(string Text, string ID, BlockType Type, int Level) :
                base(Text, ID) {
        this.Type = Type; this.Level = Level;
        }

    public LI() {
        }


    public LI(string Text, string ID, BlockType Type, int Level, int Index) :
                this(Text, ID, Type, Level) => this.Index = Index;
    }


public class ListBlock : LI {

    public List<TextBlock> Items = new();


    public string ListType;
    public string ListStart;
    public string ListGroup;
    public string ListSpacing;

    public ListBlock() {
        }

    public ListBlock(string Text, string ID, BlockType Type, int Level) :
            base(null, ID, Type, Level) {
        }
    }


public class Table : TextBlock {
    public override string SectionText => "Table " + NumericID;
    public override BlockType BlockType => BlockType.Table;
    public string Caption;

    public int MaxRow = 0;
    public List<int> Percent = new();
    public List<int> Width = new();
    public List<TableRow> Head = new();
    public List<List<TableRow>> Body = new();
    public List<TableRow> Foot = new();
    }

public class TableRow : TextBlock {
    public override string SectionText => null;
    public override BlockType BlockType => BlockType.TableRow;
    public List<TableData> Data = new();
    }

public class TableData : TextBlock {
    public override string SectionText => null;
    public override BlockType BlockType => BlockType.TableData;
    public bool IsHeading;
    public string Text;

    // the good stuff
    public int RowSpan;
    public int ColSpan;
    public List<TextBlock> Blocks;

    }


public class Text {
    public string Data;        // What goes in the text
    public string Title;       // For mouseover popup text
    public string Id; // For anything requiring an anchor to this element.
    }


public class SeriesInfo {

    ///<summary>The name of the series. Valid values are "RFC",
    ///"Internet-Draft", and "W3c".</summary>
    public string Name;

    ///<summary>identifier in series 822 or dreaft-hallambaker-fred-00</summary>
    public string Value;

    ///<summary>The status of this document.  The currently known values are
    ///"standard", "informational", "experimental", "bcp", "fyi", and
    ///"full-standard".  The RFC Series Editor may change this list in the
    ///future.</summary>
    public string Status;

    ///<summary>   The stream (as described in [RFC7841]) that originated the document.
    ///Valid values are "IETF", "IAB", "IRTF", "independent".
    ///</summary>
    public string Stream;



    public string AsciiName;
    public string AsciiValue;

    public string FullName {
        get {
            switch (Name) {
                case "RFC": return "RFC" + Value;
                case "Internet-Draft": return Value;
                }
            return "unknown";
            }
        }



    ///<summary>The DOI identifier</summary>
    public string DOI;
    ///<summary>The STD identifier</summary>
    public string STD;
    ///<summary>The document version if explicitly specified.</summary>
    public string ExplicitVersion = null;

    ///<summary>The explicitly specified version.</summary>
    public string Version = null;


    public string AutoVersion {
        get => Version ?? GetNextVersion();
        set {
            Version = value; ExplicitVersion = value;
            }
        }

    string GetNextVersion() {
        if (Name != "Internet-Draft") {
            return "";
            }
        Version = Goedel.Document.RFC.Source.GetDraftVersion(Value);
        return Version;
        }


    public string FullDocName {
        get {
            if (Name == "draft") { return Value + "-" + AutoVersion; }
            return "RFC" + Value;
            }
        }

    public string DocName {
        set {
            if (value.StartsWith("draft")) {
                Name = "draft";
                Value = value;
                }
            else if (value.StartsWith("rfc")) {
                Name = "RFC";
                Value = value.Substring(3);
                }
            else if (value.StartsWith("w3c")) {
                Name = "W3C";
                Value = value.Substring(3);
                }
            }
        }

    public string ParsedVersion {
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
    public string Type;
    public string Octets;
    public string Target;
    }

public class Reference : TextBlock {
    public override string SectionText => null;
    public override BlockType BlockType => BlockType.Reference;

    public string Label;

    public string Target;

    public string Title;
    public string Abbrev;

    public List<string> Area;
    public List<string> Workgroup;
    public List<Author> Authors = new();
    public string Year;
    public string Month;
    public string Day;
    public List<string> Keywords = new();
    public List<string> Abstract = new();
    public List<SeriesInfo> SeriesInfos = new();
    public List<Format> Formats = new();
    public List<string> Annotation = new();


    public string Version {
        get {
            if (SeriesInfos == null || SeriesInfos?.Count == 0) {
                return "??";
                }
            return SeriesInfos[0].ParsedVersion;
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
    public RequirementLevel Level;
    public string Text;
    }


/// <summary>
/// This text builder should probably be attached to the XML parser since that
/// is what it is specialized to.
/// </summary>
public class TextBlockSequenceBuilder {
    public List<TextBlock> Blocks = new();
    public TextBlock Block => Blocks.Count == 0 ? null : Blocks[Blocks.Count - 1];

    public List<Markdown.TextSegment> Segments;


    public int AsideLevel = 0;
    public int ListLevel = 0;

    public TextBlockSequenceBuilder() {
        }

    public void AddBlock(TextBlock block, List<Markdown.TextSegment> segments) {
        Blocks.Add(block);
        Segments = segments;

        }
    public void AddText(string text) {
        if (text != null) {
            var textSegment = new Markdown.TextSegmentText(text);
            if (Segments == null) {
                var block = new P() {
                    Segments = new List<GM.TextSegment>()
                    };
                AddBlock(block, block.Segments);

                }


            Segments.Add(textSegment);
            }
        }

    public Markdown.TextSegmentOpen OpenTextRun(spanx spanx) {
        switch (spanx.style) {
            case "emph": {
                    return OpenTextRun("em");
                    }
            case "strong": {
                    return OpenTextRun("strong");
                    }
            case "verb": {
                    return OpenTextRun("tt");
                    }
            }
        return OpenTextRun("");
        }

    public Markdown.TextSegmentOpen OpenTextRun(ItemsChoiceTextRun tag, params string[] attributes) {
        switch (tag) {
            case ItemsChoiceTextRun.bcp14: {
                    return OpenTextRun("bcp14", attributes);
                    }

            case ItemsChoiceTextRun.em: {
                    return OpenTextRun("em", attributes);
                    }
            case ItemsChoiceTextRun.strong: {
                    return OpenTextRun("strong", attributes);
                    }
            case ItemsChoiceTextRun.tt: {
                    return OpenTextRun("tt", attributes);
                    }
            case ItemsChoiceTextRun.sub: {
                    return OpenTextRun("sub", attributes);
                    }
            case ItemsChoiceTextRun.sup: {
                    return OpenTextRun("sup", attributes);
                    }


            case ItemsChoiceTextRun.eref: {
                    return OpenTextRun("eref", attributes);
                    }
            case ItemsChoiceTextRun.relref: {
                    return OpenTextRun("relref", attributes);
                    }
            case ItemsChoiceTextRun.xref: {
                    return OpenTextRun("xref", attributes);
                    }
            case ItemsChoiceTextRun.cref: {
                    return OpenTextRun("cref", attributes);
                    }

            }
        return OpenTextRun("");
        }


    public Markdown.TextSegmentOpen OpenTextRun(string tag, params string[] attributes) {
        var textSegment = new Markdown.TextSegmentOpen() {
            Tag = tag
            };
        Segments.Add(textSegment);
        return textSegment;
        }

    public void CloseTextRun(Markdown.TextSegmentOpen opener) {

        var textSegment = new Markdown.TextSegmentClose(opener);
        Segments.Add(textSegment);
        }


    public void TextEmpty(ItemsChoiceTextRun tag, params string[] attributes) {
        switch (tag) {
            case ItemsChoiceTextRun.iref: {
                    TextEmpty("iref", attributes);
                    break;
                    }

            }
        var textSegment = new Markdown.TextSegmentEmpty();
        Segments.Add(textSegment);
        }
    public void TextEmpty(string tag, params string[] attributes) {

        var textSegment = new Markdown.TextSegmentEmpty();
        Segments.Add(textSegment);
        }
    }
