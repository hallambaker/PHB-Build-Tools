using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Tool.RFCTool {
    public partial class HTML2RFC {

        public static void WriteXML(string OutputFile, Document Document) {
            using (TextWriter TextWriter = new StreamWriter(OutputFile, false, Encoding.ASCII)) {
                WriteXML(TextWriter, Document);
                }
            }

        public static void WriteXML(TextWriter TextWriter, Document Document) {

            Xml2RFCOut Xml2RFCOut = new Xml2RFCOut(TextWriter);
            Xml2RFCOut.Write(Document);

            }
        }
    }
