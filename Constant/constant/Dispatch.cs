using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Goedel.Registry;
using Goedel.IO;
using Goedel.Tool.Constant ;

namespace Goedel.Shell.Constant;
public partial class ConstantShell {

    /// <summary>
    /// Generate the output files as directed by <paramref name="Options"/>
    /// </summary>
    /// <param name="Options">The specified options.</param>
    public override void Generate(Generate Options) {
        var inputfile = Options.InputFile.Text;
        var outputfile = Options.OutputFile.DefaultFrom(inputfile);
        var Parse = new Goedel.Tool.Constant.Constant();

        using (Stream infile =
                    new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

            Lexer Schema = new(inputfile);

            Schema.Process(infile, Parse);
            }

        Parse.Init();

        if (Options.MarkDown.Value) {
            Parse.Markdown();

            }
        else {
            using (var OutputWriter = outputfile.OpenTextWriterNew()) {
                var Script = new Goedel.Tool.Constant.Generate() {
                    _Output = OutputWriter
                    };
                Script.GenerateCS(Parse);
                }

            }


        }

    } // class ConstantShell

