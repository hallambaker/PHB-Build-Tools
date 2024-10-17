using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Document.RFC {
    public partial class Writers {

        /// <summary>
        /// Write RFC Document out in HTML to file.
        /// </summary>
        /// <param name="outputFile">Output</param>
        /// <param name="document">Document to write</param>
        public static void WriteHTML(string outputFile, BlockDocument document) {
            using TextWriter TextWriter = new StreamWriter(outputFile, false, Encoding.UTF8);
            WriteHTML(TextWriter, document);
            }




        /// <summary>
        /// Write RFC Document out in HTML to text writer.
        /// </summary>
        /// <param name="textWriter">Output</param>
        /// <param name="document">Document to write</param>
        public static void WriteHTML(TextWriter textWriter, BlockDocument document) {

            Html2RFCOut Html2RFCOut = new(textWriter);
            //Xml2RFCOut Xml2RFCOut = new Xml2RFCOut(Console.Out);
            Html2RFCOut.Write(document);

            }


        /// <summary>
        /// Write RFC Document out in HTML to text writer.
        /// </summary>
        /// <param name="textWriter">Output</param>
        /// <param name="document">Document to write</param>
        public static void WriteHTMLAnnotated(TextWriter textWriter, BlockDocument document) {

            Html2AnnotateOut Html2RFCOut = new(textWriter);
            //Xml2RFCOut Xml2RFCOut = new Xml2RFCOut(Console.Out);
            Html2RFCOut.Write(document);

            }

        }
    }
