using Goedel.Document.RFCx;
using Goedel.Utilities;

using System.Diagnostics.Eventing.Reader;

namespace Goedel.Document.RFC;

public class Annotation : IAnnotation {
    public string User { get; set; }
    public string Anchor { get; set; }

    public string Semantic { get; set; }

    public string Text { get; set; }

    public bool Written { get; set; } = false;


    public List<string> References { get; set; }

    public Annotation() {
        }

    public Annotation(string user, string anchor, string text, string semantic = null) {
        User = user;
        Anchor = anchor;
        Semantic = semantic;
        References = new List<string>();
        Text = text;
        }


    }


public class Html2AnnotateOut : Html2RFCOut {

    public List<IAnnotation> Annotations {
        get => annotations;
        init {
            annotations = value;
            foreach (var annotation in annotations) {
                if (DictionaryOfAnnotations.TryGetValue(annotation.Anchor, out var list)) {
                    list.Add(annotation);
                    }
                else {
                    list = new List<IAnnotation>() { annotation };
                    DictionaryOfAnnotations.Add(annotation.Anchor, list);
                    }
                }
            }
        }
    List<IAnnotation> annotations;
    
    public Dictionary<string, List<IAnnotation>> DictionaryOfAnnotations { get; init; } = [];

    public override string MainStylesheet => null;
    string annotationStyle = """
        <style>
        .palimpsestUser {
        text-decoration: underline;
        }
        .palimpsestSemantic {
        font-weight: bold;
        }
        .column {
            flex: 80%;
        }
        .column2 {
            flex: 20%;
        }
        .emptyrow {
            display: flex;
        }
        .filledrow {
            display: flex;
        }
        </style>
        """;


    public override bool Annotate => true;

    string currentAnchor { get; set; }  = null;
    List<IAnnotation> currentAnnotations;

    bool IsSection { get; set; } = false;

    /// <summary>
    /// Constructor, this is a subclass of XMLTextWriter
    /// </summary>
    /// <param name="TextWriter"></param>
    public Html2AnnotateOut(TextWriter TextWriter) : base(TextWriter) {
        }

    ///<inheritdoc/>
    public override void Write(BlockDocument Document) {
        this.Document = Document;
        ListLevel = new ListLevel() { OpenListItem = OpenListItem, CloseListItem = CloseListItem };

        Start("html");
        Start("head");
        WriteHead(Document);
        Output.WriteLine(annotationStyle);
        End();
        Start("body");


        //WriteElement("h1", "Annotated Document", "id", "idnum");

        WriteBody(Document);

        End();
        }


    ///<inheritdoc/>
    public override void StartBlock(string anchor, bool isSection=false) {
        currentAnchor.AssertNull(NYI.Throw);
        currentAnchor = anchor;
        IsSection = isSection;

        var haveEntries = DictionaryOfAnnotations.TryGetValue(anchor, out currentAnnotations);

        if (haveEntries) {
            Start("div", "class", "filledrow");
            }
        else {
            Start("div", "class", "emptyrow");
            }
        Start("div", "class", "column");
        }

    ///<inheritdoc/>
    public override void EndBlock() {


        End();
        Start("div", "class", "column2");

        WriteElement("p", $"{(IsSection ? Sect : Pilcrow)}{currentAnchor}");

        if (currentAnnotations != null) {
            foreach (var annotation in currentAnnotations) {
                WriteElement("p", $"<span class=\"palimpsestUser\">[{annotation.User}]</span> " +
                    $"<span class=\"palimpsestSemantic\">{annotation.Semantic}</span>: " +
                    $"{annotation.Text}");

                annotation.Written = true;
                }
            }

        End();
        End();

        currentAnchor = null;
        }




    }




