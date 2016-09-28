using System;
using System.IO;
using System.Collections.Generic;

using Goedel.Registry;
using Goedel.Tool.Makey;

namespace Goedel.Shell.Makey {
    public partial class MakeyShell {


        /// <summary>
        /// Convert project file
        /// </summary>
        /// <param name="Options">Command line parameters</param>
        public override void Project(Project Options) {


            string inputfile = Options.InputFile.Text;
            string outputfile = Options?.OutputFile?.Text ?? "makefile";

            if (outputfile == null) {
                outputfile = Path.GetFileNameWithoutExtension(inputfile) +
                    "." + Options.OutputFile.Extension;
                }
            if (Options.Lazy.IsSet & FileTools.UpToDate(inputfile, outputfile)) {
                return;
                }

            Console.WriteLine("Process file {0} to {1}", inputfile, outputfile);

            VSProject Project;
            using (Stream scriptfile =
                new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                using (var TextReader = new StreamReader(scriptfile)) {

                    Project = new VSProject(TextReader);
                    }
                }

            using (Stream outputStream =
                new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
                using (TextWriter outputText = new StreamWriter(outputStream)) {
                    var Generate = new Generate(outputText);

                    Generate.GenerateMakefile(Project);
                    }

                }
            }
        }
    }
