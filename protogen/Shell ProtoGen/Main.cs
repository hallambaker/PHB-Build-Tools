using System;
using System.IO;
using System.Collections;
using System.Text;
using Goedel.Registry;
using ProtoGen;



namespace ProtoGenShell {
    public partial class ProtoGenShell {

        public override void About(About Options) {
            FileTools.About ();
            }

        public override void Generate(Generate Options) {
            string inputfile = Options.InputFile.Text;
            //string outputfile = Options.OutputFile.Text;

            Console.WriteLine("Process file {0}", inputfile);

            ProtoStruct Parse = new ProtoStruct();

            using (System.IO.Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process  (infile, Parse);
                Parse.Complete ();
                }


            if (Options.RFC2XML.TagText != null) {
                string OutputPath = Options.RFC2XML.Text;
                // need to do the defaulting thing here
                using (Stream outputStream =
                        new FileStream(OutputPath, FileMode.Create, FileAccess.Write)) {
                    using (TextWriter OutputWriter = 
                            new StreamWriter(outputStream, Encoding.UTF8)) {
                        ProtoGen.Generate Generate = new ProtoGen.Generate(OutputWriter);
                        Generate.GenerateRFC2XML(Parse);
                        }
                    }
                }

            if (Options.HTML.TagText != null) {
                string OutputPath = Options.HTML.Text;
                using (Stream outputStream =
                        new FileStream(OutputPath, FileMode.Create, FileAccess.Write)) {
                    using (TextWriter OutputWriter = 
                            new StreamWriter(outputStream, Encoding.UTF8)) {
                        ProtoGen.Generate Generate = new ProtoGen.Generate(OutputWriter);
                        Generate.GenerateHTML(Parse);
                        }
                    }
                }

            if (Options.CS.TagText != null) {
                string OutputPath = Options.CS.Text;
                using (Stream outputStream2 =
                        new FileStream(OutputPath, FileMode.Create, FileAccess.Write)) {
                    using (TextWriter OutputWriter = new StreamWriter(outputStream2, Encoding.UTF8)) {

                        ProtoGen.Generate Generate = new ProtoGen.Generate(OutputWriter);

                        Generate.GenerateCS(Parse);
                        }
                    }
                }

            }
        }
    }
