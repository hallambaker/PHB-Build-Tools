using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Goedel.Registry;
using Goedel.Tool.RFCTool;
using MakeRFC;
using OpenXML;
using GM=Goedel.MarkLib;

namespace MakeRFC {
    public partial class Shell {




        public override void HTML(HTML Options) {
            string inputfile = Options.InputFile.Text;
            string bibliography = Options.Bibliography.Text;
            string cache = Options.Cache.Text;

            string htmlfile = Options.HTML.DefaultFile(inputfile);
            string xmlfile = Options.XML.DefaultFile(inputfile);
            string txtfile = Options.TXT.DefaultFile(inputfile);
            string mdfile = Options.MD.DefaultFile(inputfile);
            string docfile = Options.DOC.DefaultFile(inputfile);

            string catalog = Options.Catalog.Text;
            var TagCatalog = BridgeLib.Configure.GetTagCatalog(catalog);

            bool DoWork = !Options.Lazy.IsSet;
            if (htmlfile!= null) DoWork = DoWork | !FileTools.UpToDate(inputfile, htmlfile);
            if (xmlfile!= null) DoWork = DoWork | !FileTools.UpToDate(inputfile, xmlfile);
            if (txtfile!= null) DoWork = DoWork | !FileTools.UpToDate(inputfile, txtfile);

            if (!DoWork) return;

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
                var Processing = new NewParse.Processing("bibliography", "");
                Processing.File = cache;
                var BibParse = new NewParse(Processing, Document);
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
                case ".rfc2629": {
                    Rfc2629Parse.Parse(inputfile, Document);
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

            if (htmlfile!= null) Goedel.Tool.RFCTool.HTML2RFC.WriteHTML (htmlfile, Document);
            if (xmlfile!= null) Goedel.Tool.RFCTool.HTML2RFC.WriteXML (xmlfile, Document);
            if (txtfile!= null) Goedel.Tool.RFCTool.HTML2RFC.WriteTXT (txtfile, Document);
            if (mdfile != null) Goedel.Tool.RFCTool.HTML2RFC.WriteMD(mdfile, Document);
            if (docfile != null) OpenXML.MakeWord.FromHTML2RFC(docfile, Document);
            }

        public override void Template(Template Options) {
            string ID = Options.Identifier.Text;

            string htmlfile = Options.HTML.DefaultFile(ID);
            string xmlfile = Options.XML.DefaultFile(ID);
            string mdfile = Options.MD.DefaultFile(ID);
            string docfile = Options.DOC.DefaultFile(ID);

            var Document = NewTemplate.Fill(ID);

            if (htmlfile != null) Goedel.Tool.RFCTool.HTML2RFC.WriteHTML(htmlfile, Document);
            if (xmlfile != null) Goedel.Tool.RFCTool.HTML2RFC.WriteXML(xmlfile, Document);
            if (docfile != null) OpenXML.MakeWord.FromHTML2RFC(docfile, Document);
            if (mdfile != null) Goedel.Tool.RFCTool.HTML2RFC.WriteMD(mdfile, Document);
            }
        }
    }




