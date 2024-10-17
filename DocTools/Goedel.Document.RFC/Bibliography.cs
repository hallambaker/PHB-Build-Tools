using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace Goedel.Document.RFC;  

/// <summary>
/// Represents a link to a citation of material to be matched or that has 
/// been matched in the cache or an external source.
/// </summary>
public class Citation {

    /// <summary>
    /// The citation label, e.g. 'RFC1984'
    /// </summary>
    public string Label;

    /// <summary>
    /// If true, citation is listed as normative. Note that if a citation is 
    /// cited normatively and informatively in the same document it is
    /// normative.
    /// </summary>
    public bool Normative {
        get => _Normative;
        set => _Normative = value | _Normative;  // once true, always true
        }

    private bool _Normative;

    /// <summary>
    /// Set true when a label has been successfully matched in an external library or
    /// the cache.
    /// </summary>
    public bool Resolved = false;

    /// <summary>
    /// The result of resolving the match (used to store in the archive).
    /// </summary>
    public string Result = null;

    /// <summary>
    /// The URI that resulted in the match.
    /// </summary>
    public string Uri = null;

    /// <summary>
    /// The parsed reference.
    /// </summary>
    public Reference Reference = null;

    /// <summary>
    /// Create a citation reference to be matched.
    /// </summary>
    /// <param name="Label">The label spcified in the text.</param>
    /// <param name="Normative">If true, citation is normative.</param>
    public Citation(string Label, bool Normative) {
        this.Label = Label;
        this.Normative = Normative;
        }
    }

/// <summary>
/// A list of references. A document will typically have a list of normative 
/// references and a list of informative references.
/// </summary>
public class References {
    /// <summary>
    /// The title for this list of references.
    /// </summary>
    public string Title;

    /// <summary>
    /// The list of references.
    /// </summary>
    public List<Reference> Entries = new();
    }


public class Catalog {
    public List<Citation> Citations = new();
    public List<Reference> References = new();

    public List<Reference> Normative = new();
    public List<Reference> Informative = new();

    public List<References> ReferenceSections = new();

    List<Source> Sources = new();

    public List<string> Caches = new() ;

    public void AddSource(string Prefix, string UriPattern) {
        Source Source = new(Prefix, UriPattern);
        Sources.Add(Source);
        }

    /// <summary>
    /// Register the default set of citation libraries.
    /// </summary>
    public void AddDefaultSources() {
        AddSource("RFC-", "http://xml.resource.org/public/rfc/bibxml/reference.RFC.#D4#.xml");
        AddSource("RFC", "http://xml.resource.org/public/rfc/bibxml/reference.RFC.#D4#.xml");
        AddSource("DRAFT-", "http://xml.resource.org/public/rfc/bibxml3/reference.I-D.#.xml");
        AddSource("draft-", "https://xml2rfc.tools.ietf.org/public/rfc/bibxml3/reference.I-D.#.xml");
        //AddSource("draft-", "http://xml.resource.org/public/rfc/bibxml3/reference.I-D.#.xml");
        AddSource("I-D.", "http://xml.resource.org/public/rfc/bibxml3/reference.I-D.#.xml");
        AddSource("W3C.", "http://xml.resource.org/public/rfc/bibxml4/reference.W3C.#.xml");
        AddSource("XEP-", "http://xmpp.org/extensions/refs/reference.XSF.XEP-#D4#.xml");
        AddSource("3GPP.", "http://xml.resource.org/public/rfc/bibxml5/reference.3GPP.#.xml");
        AddSource("ANSI.", "http://xml.resource.org/public/rfc/bibxml2/reference.ANSI.#.xml");
        AddSource("CCITT.", "http://xml.resource.org/public/rfc/bibxml2/reference.CCITT.#.xml");
        AddSource("FIPS.", "http://xml.resource.org/public/rfc/bibxml2/reference.FIPS.#.xml");
        AddSource("IEEE.", "http://xml.resource.org/public/rfc/bibxml2/reference.IEEE.#.xml");
        AddSource("ISO.", "http://xml.resource.org/public/rfc/bibxml2/reference.ISO.#.xml");
        AddSource("ITU.", "http://xml.resource.org/public/rfc/bibxml2/reference.ITU.#.xml");
        AddSource("NIST.", "http://xml.resource.org/public/rfc/bibxml2/reference.NIST.#.xml");
        AddSource("OASIS.", "http://xml.resource.org/public/rfc/bibxml2/reference.OASIS.#.xml");
        AddSource("PKCS.", "http://xml.resource.org/public/rfc/bibxml2/reference.PKCS.#.xml");
        }

    public const string DraftURI = "https://xml2rfc.tools.ietf.org/public/rfc/bibxml3/reference.I-D.{0}.xml";


    public void AddCitation(string Label) => AddCitation(Label, true);


    public string GetCitation(string Text) => GetCitation(Text, false);

    public string GetCitation(string Text, bool StripNL) {
        int State = 0;
        string Label = null;
        string Result = "";
        string Tag = null;
        bool Normative = false;
        foreach (char c in Text) {
            if (StripNL & ((c == '\n')| (c == '\r')) ) {
                }
            else if (State == 0) {
                if (c == '[') {
                    State = 1; Label = "";
                    }
                else {
                    Result += c;
                    }
                }
            else if (State == 1) {
                if (c == '!') {
                    Normative = true; State = 2; Label = "";
                    }
                else if (c == '~') {
                    Normative = false; State = 2; Label = "";
                    }
                else if (c == '.') {
                    State = 0; Result += '[';
                    }
                else {
                    Result += '[';
                    Result += c;
                    State = 0;
                    }
                }
            else if (State == 2) {
                if (c == ']') {
                    State = 0;
                    AddCitation(Label, Normative);
                    Result = Result + "[" + Label + "]";
                    }
                else if (c == '/') {
                    State = 3; Tag = "";
                    }
                else {
                    Label += c;
                    }
                }
            else if (State == 3) {
                if (c == ']') {
                    State = 0;
                    AddCitation(Label, Normative);
                    Result = Result + "[" + Tag + "]";
                    }
                else if (c == '/') {
                    State = 3; Tag = "";
                    }
                else {
                    Tag += c;
                    }
                }

            }
        return Result;
        }

    public Citation FindCitation(string Label) {
        foreach (Citation Citation in Citations) {
            if (Citation.Label == Label) {
                return Citation;
                }
            }

        return null;
        }

    public void AddCitation(string Label, bool Normative) {

        Citation Citation = FindCitation(Label);

        if (Citation != null) {
            Citation.Normative |= Normative;
            }
        else {
            Citations.Add(new Citation(Label, Normative));
            }
        }

    public void ResolveAll(Source Source, BlockDocument Document) {
        foreach (Citation Citation in Citations) {
            if (!Citation.Resolved) {
                Source.Resolve(Citation, Document);

                if (Citation.Resolved) {
                    if (Citation.Normative) {
                        Normative.Add(Citation.Reference);
                        }
                    else {
                        Informative.Add(Citation.Reference);
                        }
                    }

                }
            }
        }

    public void ResolveAll(BlockDocument Document) {
        foreach (Citation Citation in Citations) {
            Reference Reference = FindReference(Citation.Label);
            if (Reference != null) {
                Citation.Resolved = true;
                if (Citation.Normative) {
                    Normative.Add(Reference);
                    }
                else {
                    Informative.Add(Reference);
                    }
                }
            }

        foreach (Source Source in Sources) {
            ResolveAll(Source, Document);
            }

        foreach (Citation Citation in Citations) {
            Reference Reference = FindReference(Citation.Label);
            if (!Citation.Resolved) {
                Reference = new Reference() {
                    GeneratedID = Citation.Label,
                    Title = "[Reference Not Found!]"
                    };
                Author Author = new() {
                    Surname = "",
                    Initials = "",
                    Organization = ""
                    };
                Reference.Authors.Add (Author);
                if (Citation.Normative) {
                    Normative.Add(Reference);
                    }
                else {
                    Informative.Add(Reference);
                    }
                }
            }

        foreach (string File in Caches) {
            AppendResolved (File);
            }

        }

    public void AppendResolved(string FileName) {
        DateTime Now = DateTime.Now;
        using StreamWriter Stream = File.AppendText(FileName);
        foreach (Citation Citation in Citations) {
            if (Citation.Uri != null) {
                Stream.WriteLine("<!-- {0} Added {1} -->", Citation.Label, Now.ToString("R"));
                Stream.WriteLine("<!-- Source: {0} -->", Citation.Uri);
                Stream.WriteLine(Citation.Result);
                }
            }
        }

    public string ForceReferenceID = null;
    public void AddReference (Reference Reference) {
        Reference.GeneratedID = ForceReferenceID ?? Reference.GeneratedID;
        if (FindReference (Reference.GeneratedID) == null) {
            References.Add (Reference);
            }
        }

    public Reference FindReference(string Label) {
        foreach (Reference Reference in References) {
            if (Reference.GeneratedID == Label) {
                return Reference;
                }
            }

        return null;
        }
    }

public class Source {
    public string Prefix;
    public string UriPattern;
    public string UriStart;
    public string UriEnd;
    public string UriFormat = null;

    public Source(string Prefix, string UriPattern) {
        this.UriPattern = UriPattern;
        this.Prefix = Prefix;

        int Index = UriPattern.IndexOf('#');
        if (Index < 0) {
            UriStart = UriPattern;
            UriEnd = "";
            }
        else {
            UriStart = UriPattern.Remove(Index);
            UriEnd = UriPattern.Substring(Index + 1);

            Index = UriEnd.IndexOf('#');
            if (Index > 0) {
                UriFormat = UriEnd.Remove(Index);
                UriEnd = UriEnd.Substring(Index + 1);
                }
            }
        }


    // Remove the poxy XML Declarations that serve no other purpose than to screw things up
    public string StripDeclaration(String Text) {
        if (Text.StartsWith("<?xml")) {
            int Index = Text.IndexOf("?>");
            return Text.Substring(Index + 2);
            }
        else {
            return Text;
            }
        }

#pragma warning disable IDE0044 // Add readonly modifier
    static WebClient WebClient = new();
#pragma warning restore IDE0044 // Add readonly modifier



    public static string GetDraftVersion (string DocName) {
        try {
            var Uri = String.Format(Catalog.DraftURI, DocName.Substring(6));
            string Result = WebClient.DownloadString(Uri);
            var Document = new BlockDocument();
            new NewParse(Result, Document);
            var References = Document?.Catalog?.References;
            if (References == null | References.Count < 1) {
                return "??";
                }
            var Number = Int32.Parse(References[0].Version) + 1;
            return (Number.ToString ("D2"));
            }

        catch {
            return "00";
            }


        }


    public void Resolve(Citation Citation, BlockDocument Document) {
        if (Citation.Resolved) {
            return;
            }

        string Uri = GetUri(Citation.Label);
        if (Uri == null) {
            return;
            }

        //Console.WriteLine("{0}  {1}", Citation.Label, Uri);

        try {
            Document.Catalog.ForceReferenceID = Citation.Label;

            string Result = WebClient.DownloadString(Uri);
            Citation.Result = StripDeclaration(Result);
            Citation.Resolved = Result != null;
            Citation.Uri = Uri;
            //Console.WriteLine(Result);
            //Console.WriteLine();

            // This is horribly hack but will do for now.
            // Should really rewrite the parser to separate
            // the references parser from the document parser
            new NewParse(Result, Document);

            // if this blows up it is because the label specified in the citation does not
            // match the label followed to retrieve it.
            //
            // For Internet drafts only labels of the form [!I-D.hallambaker-udf] actually work.
            var Reference = Document.Catalog.FindReference(Citation.Label);
            Citation.Reference = Reference;

            if (Reference != null) {
                Citation.Resolved = true;
                }
            }
        catch {
            Console.WriteLine("$04-Not Found {0}", Citation.Label);
            }
        finally {
            Document.Catalog.ForceReferenceID = null;
            }

        //Console.WriteLine();
        }




    string GetUri(string Label) {

        //Console.WriteLine("Check {0} / {1}", Prefix, Label);
        if (!Label.StartsWith(Prefix)) {
            return null;
            }

        try {

            string Sufix = Label.Substring(Prefix.Length);

            if (UriFormat == null || UriFormat.Length == 0) {
                }
            else if (UriFormat[0] == 'D') {
                int Value = Convert.ToInt32(Sufix);
                Sufix = Value.ToString(UriFormat);
                }
            else {
                Sufix = String.Format(UriFormat, Sufix);
                }
            return UriStart + Sufix + UriEnd;
            }
        catch {
            return null;
            }
        }
    }
