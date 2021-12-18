using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Document.RFC {
    public partial class Writers {

        /// <summary>
        /// Write RFC Document out in HTML to file.
        /// </summary>
        /// <param name="OutputFile">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteHTML(string OutputFile, Document Document) {
            using TextWriter TextWriter = new StreamWriter(OutputFile, false, Encoding.UTF8);
            WriteHTML(TextWriter, Document);
            }




        /// <summary>
        /// Write RFC Document out in HTML to text writer.
        /// </summary>
        /// <param name="TextWriter">Output</param>
        /// <param name="Document">Document to write</param>
        public static void WriteHTML(TextWriter TextWriter, Document Document) {

            Html2RFCOut Html2RFCOut = new(TextWriter);
            //Xml2RFCOut Xml2RFCOut = new Xml2RFCOut(Console.Out);
            Html2RFCOut.Write(Document);

            }

        }
    }
