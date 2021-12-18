using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Goedel.Document.Markdown;
using Goedel.Registry;

namespace Shell.Bootmaker;

/// <summary>
/// The shall class
/// </summary>
public partial class Shell {


    static Shell() => BlockParserMarkDown.Register();


    /// <summary>
    /// Process a whole site
    /// </summary>
    /// <param name="Options">Command line options</param>
    public override void Site(Site Options) {
        var TagCatalog = ReadCatalog();
        ProcessWeb(TagCatalog, Options.InputDir.Value, Options.OutputDir.Value);

        }

    /// <summary>
    /// Process a file
    /// </summary>
    /// <param name="Options">Command line options</param>
    public override void SingleFile(SingleFile Options) => base.SingleFile(Options);


    static TagCatalog ReadCatalog() {
        string inputfile = "TagDefinitions.mdsd";
        var Parse = new Goedel.Document.Markdown.Tags.MarkSchema();

        using (Stream infile =
                    new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

            Lexer Schema = new(inputfile);
            Schema.Process(infile, Parse);
            }

        var TagCatalog1 = new TagCatalog(Parse) {
            Process = ProcessCopyFile
            };

        return TagCatalog1;
        }


    static readonly Dictionary<string, bool> CopyExtensions = new() {
            { ".png", true},
            { ".jpg", true},
            { ".svg", true},
            { ".txt", true},
            { ".config", true}
            };

    static void ProcessCopyFile(string InPath, string OutPath) {
        var Extension = Path.GetExtension(InPath);
        if (!CopyExtensions.ContainsKey(Extension.ToLower())) {
            return;
            }

        var OutFile = Path.Combine(OutPath, Path.GetFileName(InPath));

        System.IO.File.Copy(InPath, OutFile, true);
        }


    static void ProcessWeb(TagCatalog TagCatalog, string InPath, string OutPath) {
        var DocumentSet = new DocumentSet(InPath, TagCatalog);
        DocumentSet.MakeFiles(OutPath);
        }

    static void DefaultStyle(FormatHTML FormatHTML) {

        FormatHTML.Header = new string[] {
                @"<meta charset=""utf-8"">",
                @"<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">",
                @"<meta name=""viewport"" content=""width=device-width, initial-scale=1"">",
                @"<link rel=""stylesheet"" href=""/js/bootstrap.min.css"">",
                @"<link rel=""stylesheet"" href=""/Bootstrap/3.3.1/css/bootstrap-theme.min.css"">"
                };

        FormatHTML.Trailer = new string[] {
                @"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js""></script>",
                @"<script src=""/Bootstrap/3.3.1/css/Bootstrap.min.js""></script>"
                };

        FormatHTML.NavStart = new string[] {
                @"<nav class=""navbar navbar-default"">",
                @"  <div class=""container-fluid"">"
                };

        FormatHTML.NavEnd = new string[] {
                "  </div>",
                "</nav>"
                };

        FormatHTML.NavRoot = new string[] {
                @"    <div class=""navbar-header"">",
                @"        <a class=""navbar-brand"" href=""{0}"">{1}</a>",
                @"    </div>"
                };

        FormatHTML.NavParent = new string[] {
                @"    <div class=""navbar-header"">",
                @"        <a class=""navbar-brand"" href=""{0}"">{1}</a>",
                @"    </div>"
                };

        FormatHTML.NavEntryStart = new string[] {
                @"    <div>",
                @"      <ul class=""nav navbar-nav"">",
                };

        FormatHTML.NavEntryEnd = new string[] {
                @"      </ul>",
                @"    </div>"
                };

        FormatHTML.NavEntry = new string[] {
                @"    <li><a href=""{0}"">{1}</a></li>"
                };

        FormatHTML.NavEntryActive = new string[] {
                @"    <li class=""active""><a href=""."">{1}</a></li>"
                };

        FormatHTML.ParagraphsStart = new string[] {
                @"<div class=""container"">",
                 @"<div class=""row"">",
                };

        FormatHTML.ParagraphsEnd = new string[] {
                @"</div>",
                @"</div>"
                };

        }
    }
