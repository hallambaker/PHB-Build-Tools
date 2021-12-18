using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Document.Markdown;
using Goedel.Registry;

namespace BridgeLib {
    public class Configure {

        public static Stream StreamFromString(string s) {
            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
            }

        public static TagCatalog GetTagCatalog(string filename) {

            var Parse = new Goedel.Document.Markdown.Tags.MarkSchema();

            if (filename == null) {
                using Stream infile = StreamFromString(Constants.Value);
                var Schema = new Lexer("INTERNAL");
                Schema.Process(infile, Parse);
                }
            else {
                using Stream infile =
                            new FileStream(filename, FileMode.Open, FileAccess.Read);
                var Schema = new Lexer(filename);
                Schema.Process(infile, Parse);
                }
            return new TagCatalog(Parse);
            }
        }
    }
