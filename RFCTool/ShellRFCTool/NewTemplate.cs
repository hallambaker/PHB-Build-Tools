using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Goedel.Registry;
using Goedel.Tool.RFCTool;
using MakeRFC;
using BridgeLib;
using GM = Goedel.Document.Markdown;

namespace MakeRFC {
    public class NewTemplate {

        public static Document Fill(string ID) {
            // Use the built in tag catalog
            var TagCatalog = BridgeLib.Configure.GetTagCatalog(null);

            // Use the built in document, expanding the identifier field

            var IDStem = Path.GetFileNameWithoutExtension(ID);
            string Expanded = System.String.Format(Constants.Template, IDStem);
            var Stream = Configure.StreamFromString(Expanded);

            // Parse the expanded document to a MarkDown parse tree
            var Source = new GM.Document(Stream, TagCatalog);

            // Create an RFC document from the Markdown parse tree
            var Document = new Document();
            ConverterRFC.Convert(Source, Document);
            return Document;
            }
        }
    }
