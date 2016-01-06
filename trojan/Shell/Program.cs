using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Linq;
using System.Reflection;
using Goedel.Registry;
using Goedel.Trojan;

namespace Goedel.Trojan {
    public partial class CommandShell {
        public override void Generate(Generate Options) {
            var Parse = GUISchema.Parse(Options.InputFile.Text, Options);

            var Generator = new Generator(Options.OutputFile.Text);

            foreach (var GUI in Parse.Top) {
                Generator.GenerateGUI(GUI as GUI);
                }

            }
        }
    
    }