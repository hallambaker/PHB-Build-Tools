using System.IO;
using Goedel.Document.RFC;
using MakeRFC;
using GM = Goedel.Document.Markdown;


namespace Goedel.Document.RFCToolBinding {

    /// <summary>
    /// This class provides entry points into the Goedel.Document.* classes
    /// for use in the VSIX integration tools.
    /// Thhe entry points provided accept files in any of the supported 
    /// input file formats and emit them in a specific output file format.
    /// </summary>
    public class BindingRFC {

        /// <summary>
        /// Process document to produce output in plaintext RFC format.
        /// </summary>
        /// <param name="FileName">The original document file name.</param>
        /// <param name="Reader">TextReader object containing document source.</param>
        /// <param name="Writer">Destination to write document to.</param>
        public void Process2TXT(string FileName, TextReader Reader, TextWriter Writer) {
            var Document = ReadDocument(FileName, Reader);

            if (Document != null) {
                Goedel.Document.RFC.Writers.WriteTXT(Writer, Document);
                }
            }

        /// <summary>
        /// Process document to produce output in plaintext RFC format.
        /// </summary>
        /// <param name="FileName">The original document file name.</param>
        /// <param name="Reader">TextReader object containing document source.</param>
        /// <param name="Writer">Destination to write document to.</param>
        public void Process2XML(string FileName, TextReader Reader, TextWriter Writer) {
            var Document = ReadDocument(FileName, Reader);

            if (Document != null) {
                Goedel.Document.RFC.Writers.WriteXML(Writer, Document);
                }
            }

        /// <summary>
        /// Process document to produce output in plaintext RFC format.
        /// </summary>
        /// <param name="FileName">The original document file name.</param>
        /// <param name="Reader">TextReader object containing document source.</param>
        /// <param name="Writer">Destination to write document to.</param>
        public void Process2HTML(string FileName, TextReader Reader, TextWriter Writer) {
            var Document = ReadDocument(FileName, Reader);

            if (Document != null) {
                Goedel.Document.RFC.Writers.WriteHTML(Writer, Document);
                }
            }

        /// <summary>
        /// Process document to produce output in plaintext RFC format.
        /// </summary>
        /// <param name="FileName">The original document file name.</param>
        /// <param name="Reader">TextReader object containing document source.</param>
        /// <param name="Writer">Destination to write document to.</param>
        public void Process2MD(string FileName, TextReader Reader, TextWriter Writer) {
            var Document = ReadDocument(FileName, Reader);

            if (Document != null) {
                Goedel.Document.RFC.Writers.WriteMD(Writer, Document);
                }
            }



        /// <summary>
        /// Process document to produce output in MAML format
        /// </summary>
        /// <param name="FileName">The original document file name.</param>
        /// <param name="Reader">TextReader object containing document source.</param>
        /// <param name="Writer">Destination to write document to.</param>
        public void Process2AML(string FileName, TextReader Reader, TextWriter Writer) {
            var Document = ReadDocument(FileName, Reader);

            if (Document != null) {
                Goedel.Document.RFC.Writers.WriteAML(Writer, Document);
                }
            }


        /// <summary>
        /// Read document according to input format indicated by extension.
        /// </summary>
        /// <param name="FileName">The original document file name.</param>
        /// <param name="Reader">TextReader object containing document source.</param>
        /// <returns></returns>
        public Goedel.Document.RFC.Document ReadDocument(string FileName, TextReader Reader) {
            // Use the default tag catalog (for now, may support a property page some time
            var TagCatalog = BridgeLib.Configure.GetTagCatalog(null);
            var Document = new Goedel.Document.RFC.Document();

            var Extension = Path.GetExtension(FileName).ToLower();

            switch (Extension) {
                case ".md": {
                        var Source = GM.BlockParserMarkDown.Parse(Reader, TagCatalog);
                        ConverterRFC.Convert(Source, Document);
                        return Document;
                        }
                case ".docx": {
                        // Till we have the ability to read raw files
                        //var Source = BlockParseWord.Parse(Reader, TagCatalog);
                        //ConverterRFC.Convert(Source, Document);
                        return null;
                        }
                //case ".xml": {
                //        Rfc2629Parse.Parse(Reader, Document);
                //        return null;
                //        }
                //case ".html": {
                //        NewParse.Parse(Reader, Document);
                //        return null;
                //        }
                }
            return null;
            }


        }


    }
