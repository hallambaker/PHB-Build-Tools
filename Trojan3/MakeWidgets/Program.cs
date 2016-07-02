using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MakeWidgets {
    class Program {
        static void Main(string[] args) {
            string FileName = args.Length > 0 ? args[0] : "Mixin.cs";

            using (var TextWriter = new StreamWriter(FileName)) {
                var GenerateGTK = new GenerateGTK(TextWriter);

                GenerateGTK.GenerateCS("Test");
                }
            }
        }
    }
