

using Goedel.IO;

using System.IO;

namespace Shell.Annotate;

/// <summary>
/// The shall class
/// </summary>
public partial class Shell {


    //static Shell() => BlockParserMarkDown.Register();



    /// <summary>
    /// Process a file
    /// </summary>
    /// <param name="Options">Command line options</param>
    public override void SingleFile(SingleFile Options) {

        var annotations = new List<IAnnotation>() {
            new Annotation("Alice", "identifiers", "Is intended status Informational?", "query"),
            new Annotation("PHB", "abstract", "Abstract needs reviewing to check it covers current scope", "review"),
            new Annotation("Bob", "title", "Draft should describe relation to RFC6920 ni", "issue"),
            new Annotation("PHB", "title", "Need to mention key gen stuff in introduction", "issue"),
            new Annotation("PHB", "title", "Explain relation to OpenPGP fingerprints better", "issue"),
            new Annotation("Alice", "section-1-1", "What if a UDF were mistaken for a PGP fingerprint?", "query"),
            new Annotation("PHB", "section-3_2-2", "Digest values intentionally use characters outsid hexadecimal values?", "missing"),
            new Annotation("PHB", "section-1_1-6", "Drop like email addresses", "style")
            };


        var input = Options.InputFile.Value;
        var output = Options.OutputFile.Value;

        var document = new BlockDocument();
        Rfc7991Parse.Parse(input, document);

        document.MakeAutomatics();

        using (var writer = output.OpenTextWriterNew()) {

            Html2AnnotateOut Html2RFCOut = new(writer) {
                Annotations = annotations
                };
            Html2RFCOut.Write(document);

            }
        }
   

    }


