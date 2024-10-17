using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Goedel.Command;
using Goedel.Document.RFC;
using MakeRFC;
using Goedel.Document.OpenXML;
using GM=Goedel.Document.Markdown;

namespace MakeRFC {
    public partial class Shell {




        public override void RFC (RFC Options) {
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
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

            DoWork |= Options.Auto.Value;

            if (htmlfile != null) { DoWork |= !FileTools.UpToDate(inputfile, htmlfile); }
            if (xmlfile != null) { DoWork |= !FileTools.UpToDate(inputfile, xmlfile); }
            if (txtfile != null) { DoWork |= !FileTools.UpToDate(inputfile, txtfile); }

            if (!DoWork) { return; }

            string Format = Options.InputFormat.Text;
            if (Options.InputFormat.Text == null) {
                Format = Path.GetExtension(inputfile);
                }
            else {
                Format = "." + Options.InputFormat.Text;
                }

            var Document = new BlockDocument();

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

            if (Options.Auto.Value) {
                Writers.WriteHTML(Document.Docname + ".html", Document);
                Writers.WriteHTML(Document.FullDocName + ".html", Document);
                Writers.WriteTXT(Document.FullDocName + ".txt", Document);
                Writers.WriteXML(Document.FullDocName + ".xml", Document);
                }


            if (htmlfile != null) { Goedel.Document.RFC.Writers.WriteHTML(htmlfile, Document); }
            if (xmlfile != null) { Goedel.Document.RFC.Writers.WriteXML(xmlfile, Document); }
            if (txtfile != null) { Goedel.Document.RFC.Writers.WriteTXT(txtfile, Document); }
            if (mdfile != null) { Goedel.Document.RFC.Writers.WriteMD(mdfile, Document); }
            if (docfile != null) { Goedel.Document.OpenXML.MakeWord.FromHTML2RFC(docfile, Document); }
            if (amlfile != null) { Goedel.Document.RFC.Writers.WriteAML(amlfile, Document); }
            }

        public override void Template (Template Options) {
            string ID = Options.Identifier.Text;

            string htmlfile = Options.HTML.DefaultFile(ID);
            string xmlfile = Options.XML.DefaultFile(ID);
            string mdfile = Options.MD.DefaultFile(ID);
            string docfile = Options.DOC.DefaultFile(ID);

            var Document = NewTemplate.Fill(ID);

            if (htmlfile != null) { Goedel.Document.RFC.Writers.WriteHTML(htmlfile, Document); }
            if (xmlfile != null) { Goedel.Document.RFC.Writers.WriteXML(xmlfile, Document); }
            if (docfile != null) { Goedel.Document.OpenXML.MakeWord.FromHTML2RFC(docfile, Document); }
            if (mdfile != null) { Goedel.Document.RFC.Writers.WriteMD(mdfile, Document); }
            }
        }
    }




