using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Document.Test;
using Goedel.Utilities;
namespace RunDocs {
    class Program {
        static void Main (string[] args) {
            Goedel.IO.Debug.Initialize();

            var UnitTest1 = new UnitTest1();
            UnitTest1.TestWordLexerXML();
            }
        }
    }
