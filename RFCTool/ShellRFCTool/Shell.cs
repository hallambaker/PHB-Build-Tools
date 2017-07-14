using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Goedel.Command;
using Goedel.Tool.RFCTool;
using MakeRFC;
using Goedel.Document.OpenXML;
using GM=Goedel.Document.Markdown;

namespace MakeRFC {
    public partial class Shell {




        public override void RFC (RFC Options) {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;



            string inputfile = Options.InputFile.Text;
            //string bibliography = Options.Bibliography.Text;
            string cache = Options.Cache.Text;

            string htmlfile = Options.HTML.Value;
            string xmlfile = Options.XML.Value;
            string txtfile = Options.TXT.Value;
            string mdfile = Options.MD.Value;
            string docfile = Options.DOC.Value;
            string amlfile = Options.AML.Value;

            string catalog = Options.Catalog.ByDefault ? null : Options.Catalog.Text;
            var TagCatalog = BridgeLib.Configure.GetTagCatalog(catalog);

            bool DoWork = !Options.Lazy.Value;
            if (htmlfile != null) { DoWork = DoWork | !FileTools.UpToDate(inputfile, htmlfile); }
            if (xmlfile != null) { DoWork = DoWork | !FileTools.UpToDate(inputfile, xmlfile); }
            if (txtfile != null) { DoWork = DoWork | !FileTools.UpToDate(inputfile, txtfile); }

            if (!DoWork) { return; }

            string Format = Options.InputFormat.Text;
            if (Options.InputFormat.Text == null) {
                Format = Path.GetExtension(inputfile);
                }
            else {
                Format = "." + Options.InputFormat.Text;
                }

            var Document = new Document();

            // Add in any cache specified on the command line
            if (cache != null) {
                Document.Catalog.Caches.Add(cache);
                var Processing = new NewParse.Processing("bibliography", "") {
                    File = cache
                    };
                new NewParse(Processing, Document);

                }

            var Import = new Import (TagCatalog);

            GM.BlockParserMarkDown.Register();
            BlockParseWord.Register();

            switch (Format.ToLower()) {
                case ".html":
                case ".html2rfc": {
                    NewParse.Parse(inputfile, Document);
                    break;
                    }

                case ".xml":
                case ".xml2rfc":
                case ".rfc2629":
                case ".rfc7991": {
                    Rfc7991Parse.Parse(inputfile, Document);
                    break;
                    }

                case ".doc":
                case ".docx": {
                    //var Import = new Import(catalog);
                    Import.WordParse(inputfile, Document);
                    break;
                    }

                case ".md": {
                    //var Import = new Import(catalog);
                    Import.MDParse(inputfile, Document);
                    break;
                    }
                }


            Document.MakeAutomatics();

            if (htmlfile != null) { Goedel.Tool.RFCTool.Writers.WriteHTML(htmlfile, Document); }
            if (xmlfile != null) { Goedel.Tool.RFCTool.Writers.WriteXML(xmlfile, Document); }
            if (txtfile != null) { Goedel.Tool.RFCTool.Writers.WriteTXT(txtfile, Document); }
            if (mdfile != null) { Goedel.Tool.RFCTool.Writers.WriteMD(mdfile, Document); }
            if (docfile != null) { Goedel.Document.OpenXML.MakeWord.FromHTML2RFC(docfile, Document); }
            if (amlfile != null) { Goedel.Tool.RFCTool.Writers.WriteAML(amlfile, Document); }
            }

        public override void Template (Template Options) {
            string ID = Options.Identifier.Text;

            string htmlfile = Options.HTML.DefaultFile(ID);
            string xmlfile = Options.XML.DefaultFile(ID);
            string mdfile = Options.MD.DefaultFile(ID);
            string docfile = Options.DOC.DefaultFile(ID);

            var Document = NewTemplate.Fill(ID);

            if (htmlfile != null) { Goedel.Tool.RFCTool.Writers.WriteHTML(htmlfile, Document); }
            if (xmlfile != null) { Goedel.Tool.RFCTool.Writers.WriteXML(xmlfile, Document); }
            if (docfile != null) { Goedel.Document.OpenXML.MakeWord.FromHTML2RFC(docfile, Document); }
            if (mdfile != null) { Goedel.Tool.RFCTool.Writers.WriteMD(mdfile, Document); }
            }
        }
    }




