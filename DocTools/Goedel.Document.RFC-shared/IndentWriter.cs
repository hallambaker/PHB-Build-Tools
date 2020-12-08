using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Document.RFC {


    /// <summary>
    /// Variant of the streamwriter class supporting formatted output
    /// of XML.
    /// </summary>
    public class IndentWriter : StreamWriter {

        public int Indent { get; set; } = 0;

        public IndentWriter(string OutputFile) :
                base(OutputFile, false, Encoding.UTF8) => NewLine = "\n";

        void MakeIndent () {
            for (var i = 0; i < Indent; i++) {
                Write("  ");
                }
            }


        }
    }
