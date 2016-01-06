using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Linq;
using System.Reflection;
using Goedel.Registry;
using Goedel.Trojan;

namespace Goedel.Trojan {

    public partial class Generator {
        string Directory;


        public Generator(string Directory) {
            this.Directory = Directory;
            }

        public void GenerateGUI(GUI GUI) {
            GUI.Normalize(null);

            var Wizards = GUI.Entries.OfType<Wizard>();

            foreach (var Wizard in Wizards) {
                GenerateWizard(Wizard);
                }
            }


        public void GenerateWizard(Wizard Wizard) {
            var Dialogs = Wizard.Entries.OfType<Dialog>();

            string FileStem = Directory + "\\" + Wizard.Id.Label;
            string FileXaml = FileStem + ".xaml";
            string FileCS = FileStem + ".xaml.cs";

            using (Stream outputStream =
                        new FileStream(FileXaml, FileMode.Create, FileAccess.Write)) {
                using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

                    var Script = new GenerateWpf(OutputWriter);

                    Script.GenerateXAML(Wizard);
                    }
                }

            using (Stream outputStream =
                        new FileStream(FileCS, FileMode.Create, FileAccess.Write)) {
                using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

                    var Script = new GenerateWpf(OutputWriter);

                    Script.GenerateCS(Wizard);
                    }
                }

            foreach (var Dialog in Dialogs) {
                GenerateDialog(Dialog);
                }
            }


        public void GenerateDialog(Dialog Dialog) {
            string FileStem = Directory + "\\" + Dialog.Id.Label;
            string FileXaml = FileStem + ".xaml";
            string FileCS = FileStem + ".xaml.cs";


            using (Stream outputStream =
                        new FileStream(FileXaml, FileMode.Create, FileAccess.Write)) {
                using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

                    var Script = new GenerateWpf(OutputWriter);

                    Script.GenerateXAML(Dialog);
                    }
                }

            using (Stream outputStream =
                        new FileStream(FileCS, FileMode.Create, FileAccess.Write)) {
                using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {

                    var Script = new GenerateWpf(OutputWriter);

                    Script.GenerateCS(Dialog);
                    }
                }

            }
        }
    }
