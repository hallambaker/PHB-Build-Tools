using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Goedel.Registry;
using Goedel.IO;

namespace Goedel.Shell.Constant {
    public partial class ConstantShell  {

        public override void Generate(Generate Options) {
            var inputfile = Options.InputFile.Text;
            var outputfile = Options.OutputFile.DefaultFrom(inputfile);
            var Parse = new Goedel.Tool.Constant.Constant() ;


            using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }

            Parse.Init();


            using (var OutputWriter = outputfile.OpenTextWriterNew()) {
                var Script = new Goedel.Tool.Constant.Generate() {
                    _Output = OutputWriter
                    };
                Script.GenerateCS(Parse);
                }


            if (Options.MarkDown.Value) {


                }

            }

        } // class ConstantShell
    }
