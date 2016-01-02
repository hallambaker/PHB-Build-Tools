using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Reflection;
using Goedel.Registry;
using CommandP;

namespace CommandShell {
    public partial class CommandShell {

        public override void About(About Options) {
            FileTools.About ();
            }

        public override void Generate(Generate Options) {

            string inputfile = Options.InputFile.Text;

            // Default the file extension
            string outputfile = FileTools.DefaultOutput (inputfile, Options.OutputFile.Text, 
                Options.OutputFile.Extension);


            // Do nothing if the lazy flag is set and the output file is up to date
            if (Options.Lazy.IsSet & FileTools.UpToDate (inputfile, outputfile)) {
                return;
                }

            CommandParse Parse = new CommandParse();
            Parse.Main = Options.Main.IsSet;
            Parse.Builtins = Options.Builtins.IsSet;
            Parse.Catcher = Options.Catcher.IsSet;

            using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }

            using (Stream outputStream =
                        new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
                using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

                    GenerateCS GenerateCS = new GenerateCS(OutputWriter);

                    GenerateCS.Generate(Parse);
                    }
                }
            }
        }
    }
