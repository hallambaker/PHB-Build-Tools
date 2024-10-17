using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Document.RFC {
    public partial class Writers {

        public static void WriteXML(string OutputFile, BlockDocument Document) {
            using TextWriter TextWriter = new StreamWriter(OutputFile, false, Encoding.ASCII);
            WriteXML(TextWriter, Document);
            }

        public static void WriteXML(TextWriter TextWriter, BlockDocument Document) {

            Xml2RFCOut Xml2RFCOut = new(TextWriter);
            Xml2RFCOut.Write(Document);

            }
        }
    }
