//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using Goedel.Document.Markdown;
//using Goedel.Document.Markdown.Tags;
//using Goedel.Document.OpenXML;
//using Goedel.Registry;

//namespace Shell.Bootmaker;

//class Program {

//    // Bug: The breadcrumbs are being incorrectly set, it goes master->Level2 not master->Level1->Level2

//    // ToDo: Work out how to do a decent image closeup in Bootstrap

//    // ToDo: Re-enable use of Visio? Word? Powerpoint? handlers


//    //static void Main(string[] args) {
//    //    //string InPath = @"C:\Users\Phillip\Google Drive\hallambaker.com\Professional\Architecture\";
//    //    //string OutPath = @"O:\hallambaker";

//    //    //Visio1.DoStuff(InPath + "Internet.vsd", OutPath);

//    //    //Excel1.DoStuff(InPath + "TestExcell.xlsx", OutPath);

//    //    //PowerPoint1.DoStuff(InPath + "Test.pptx", OutPath);   

//    //    var TagCatalog = ReadCatalog();
//    //    TestWeb(TagCatalog);
//    //    //TestParse();

//    //    //TestNew(TagCatalog);
//    //    }

//    static Program() => BlockParserMarkDown.Register();


//    static TagCatalog ReadCatalog() {
//        string inputfile = "TagDefinitions.mdsd";
//        var Parse = new Goedel.Document.Markdown.Tags.MarkSchema();

//        using (Stream infile =
//                    new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

//            Lexer Schema = new(inputfile);
//            Schema.Process(infile, Parse);
//            }

//        var TagCatalog1 = new TagCatalog(Parse);
//        //TagCatalog1.DocumentProcess = Goedel.WordLib.Dispatch.Process;
//        //TagCatalog1.Process = Goedel.Document.Office.Dispatch.Process;

//        return TagCatalog1;
//        }

//    static void TestNew(TagCatalog TagCatalog) {

//        var Document = new Document("Test.md");
//        var BlockParser = new BlockParserMarkDown() {
//            TagCatalog = TagCatalog,
//            Document = Document
//            };
//        BlockParser.Parse();

//        Dump(Document);

//        var FormatHTML = new FormatHTML(TagCatalog);


//        DefaultStyle(FormatHTML);
//        FormatHTML.MakeHTML(Document, @"C:\Users\Phillip\Code\Applications\SpecTools\BootMaker\Mark");

//        }


//    static void Dump(Document Document) {
//        Console.WriteLine("META DATA");

//        foreach (var Index in Document.MetaData) {
//            Console.WriteLine("    {0}", Index.Key);
//            foreach (var Attribute in Index.Value) {
//                DumpAttributes(Attribute.Attributes);
//                Console.WriteLine("        {0}", Attribute.Text);
//                }

//            }

//        foreach (var Paragraph in Document.Blocks) {
//            DumpAttributes(Paragraph.Attributes);
//            Console.WriteLine("        {0}", Paragraph.Text);

//            //foreach (var Segment in Paragraph.Segments) {
//            //    Console.WriteLine("     [{0}]", Segment.Text);
//            //    }

//            }
//        }

//    static void DumpAttributes(List<TagValue> Attributes) {
//        if (Attributes == null) {
//            return;
//            }
//        foreach (var TV in Attributes) {
//            Console.Write(" {0}={1}", TV.Tag, TV.Value);
//            }
//        Console.WriteLine();
//        }


//    static void TestParse() {
//        Console.WriteLine("Should just be text");
//        MarkDownParagraph.TestParse("");
//        MarkDownParagraph.TestParse("&");
//        MarkDownParagraph.TestParse("& &");
//        MarkDownParagraph.TestParse("Terms&Conditions");
//        MarkDownParagraph.TestParse("<");
//        MarkDownParagraph.TestParse("< <");
//        MarkDownParagraph.TestParse("<test");
//        MarkDownParagraph.TestParse("<test=");
//        MarkDownParagraph.TestParse("<test=test");

//        Console.WriteLine("Should be escape converions");
//        MarkDownParagraph.TestParse("&&");
//        MarkDownParagraph.TestParse("Test &&");
//        MarkDownParagraph.TestParse("Test&&Check");
//        MarkDownParagraph.TestParse("&<");
//        MarkDownParagraph.TestParse("&>");
//        MarkDownParagraph.TestParse("&=");
//        MarkDownParagraph.TestParse("&<tag>");
//        MarkDownParagraph.TestParse("&</>");

//        Console.WriteLine("Should be open elements");
//        MarkDownParagraph.TestParse("<tag>");
//        MarkDownParagraph.TestParse("SomeText<tag>");
//        MarkDownParagraph.TestParse("<tag=value>");
//        MarkDownParagraph.TestParse("<tag =value>");
//        MarkDownParagraph.TestParse("<tag= value>");
//        MarkDownParagraph.TestParse("<tag=value >");
//        MarkDownParagraph.TestParse("<tag=value tag2>");
//        MarkDownParagraph.TestParse("<tag=value tag2 >");
//        MarkDownParagraph.TestParse("<tag=value tag2=>");
//        MarkDownParagraph.TestParse("<tag=value tag2= > ");
//        MarkDownParagraph.TestParse("<tag=value tag2=value2 > ");
//        MarkDownParagraph.TestParse(@"<tag=value tag2=""value2"" > ");
//        MarkDownParagraph.TestParse(@"<tag=value tag2="""" > ");

//        Console.WriteLine("Should be empty elements");
//        MarkDownParagraph.TestParse("<tag/>");
//        MarkDownParagraph.TestParse("SomeText<tag/> ");
//        MarkDownParagraph.TestParse("<tag=value/> ");
//        MarkDownParagraph.TestParse("<tag =value/> ");
//        MarkDownParagraph.TestParse("<tag= value/> ");
//        MarkDownParagraph.TestParse(@"<tag=value tag2=""value2"" /> ");
//        MarkDownParagraph.TestParse(@"<tag=value tag2="""" /> ");

//        Console.WriteLine("Should be close elements");
//        MarkDownParagraph.TestParse("</>");
//        MarkDownParagraph.TestParse("</tag>");
//        MarkDownParagraph.TestParse("</tag >");
//        MarkDownParagraph.TestParse("/tag> ");
//        }




//    static void TestWeb(TagCatalog TagCatalog) {

//        string InPath = @"C:\Users\Phillip\Google Drive\hallambaker.com";
//        //string OutPath = @"C:\Users\Phillip\hallambaker.com";
//        string OutPath = @"O:\hallambaker";
//        //string OutPath = @"O:\www\";

//        var DocumentSet = new DocumentSet(InPath, TagCatalog);
//        var FormatHTML = new FormatHTML(TagCatalog);
//        DefaultStyle(FormatHTML);

//        FormatHTML.MakeFiles(DocumentSet, OutPath);


//        }

//    static void DefaultStyle(FormatHTML FormatHTML) {


//        FormatHTML.Header = new string[] {
//                @"<meta charset=""utf-8"">",
//                @"<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">",
//                @"<meta name=""viewport"" content=""width=device-width, initial-scale=1"">",
//                @"<link rel=""stylesheet"" href=""/Bootstrap/3.3.1/css/bootstrap.min.css"">",
//                @"<link rel=""stylesheet"" href=""/Bootstrap/3.3.1/css/bootstrap-theme.min.css"">"
//                };
//        FormatHTML.Trailer = new string[] {
//                @"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js""></script>",
//                @"<script src=""/Bootstrap/3.3.1/css/Bootstrap.min.js""></script>"
//                };

//        FormatHTML.NavStart = new string[] {
//                @"<nav class=""navbar navbar-default"">",
//                @"  <div class=""container-fluid"">"
//                };
//        FormatHTML.NavEnd = new string[] {
//                "  </div>",
//                "</nav>"
//                };
//        FormatHTML.NavRoot = new string[] {
//                @"    <div class=""navbar-header"">",
//                @"        <a class=""navbar-brand"" href=""{0}"">{1}</a>",
//                @"    </div>"
//                };
//        FormatHTML.NavParent = new string[] {
//                @"    <div class=""navbar-header"">",
//                @"        <a class=""navbar-brand"" href=""{0}"">{1}</a>",
//                @"    </div>"
//                };
//        FormatHTML.NavEntryStart = new string[] {
//                @"    <div>",
//                @"      <ul class=""nav navbar-nav"">",
//                };
//        FormatHTML.NavEntryEnd = new string[] {
//                @"      </ul>",
//                @"    </div>"
//                };
//        FormatHTML.NavEntry = new string[] {
//                @"    <li><a href=""{0}"">{1}</a></li>"
//                };
//        FormatHTML.NavEntryActive = new string[] {
//                @"    <li class=""active""><a href=""."">{1}</a></li>"
//                };
//        FormatHTML.ParagraphsStart = new string[] {
//                @"<div class=""container"">",
//                 @"<div class=""row"">",
//                };
//        FormatHTML.ParagraphsEnd = new string[] {
//                @"</div>",
//                @"</div>"
//                };



//        }
//    }
