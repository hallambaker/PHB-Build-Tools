using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.Document.Markdown;


namespace Goedel.Document.OpenXML {

    class OpenWordDocument : Goedel.Document.Markdown.MarkdownDocument {

        /// <summary>
        /// Construct a reader for a complete file.
        /// </summary>
        /// <param name="FileInfo">The FileInfo handle of the file to read.</param>
        /// <param name="TagCatalog">The tag catalog to interpret style entries.</param>
        public OpenWordDocument(FileInfo FileInfo, TagCatalog TagCatalog) :
            base(FileInfo) => Parse(FileInfo.FullName, TagCatalog, this);

        /// <summary>
        /// Construct a reader for a partial file
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TagCatalog"></param>
        /// <param name="Document"></param>
        public OpenWordDocument(string FileName, TagCatalog TagCatalog,
                    Goedel.Document.Markdown.MarkdownDocument Document) => Parse(FileName, TagCatalog, Document);


        /// <summary>
        /// The actual parse routine. Reads out the Word file block by block,
        /// creating entries in Paragraphs and the metadata registries.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TagCatalog"></param>
        /// <param name="Document"></param>
        public static void Parse (string FileName, TagCatalog TagCatalog,
                    Goedel.Document.Markdown.MarkdownDocument Document) {



            }

        }
    }
