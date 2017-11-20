//   Copyright © 2015 by Default Deny Security Inc.
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Goedel.Registry;
using Goedel.Command;
using Goedel.Tool.Script;

namespace GoedelShell {
    //public partial class ID {
    //    public override void Default(string TextIn) {
    //        if (Text == null) {
    //            Text = TextIn;
    //            }
    //        }
    //    }

    public partial class GoedelShell {

        private string DefaultFile (NewFile Entry, string Default) {
            if (Entry.Text != null) {
                return Entry.Text;
                }
            return Path.GetFileNameWithoutExtension(Default) + "." + Entry.Extension;
            }

        public override void About(About Options) {
            FileTools.About();
            }

        public override void Generate(Generate Options) {
            string inputfile = Options.InputFile.Text;
            string outputfile = DefaultFile(Options.OutputFile, inputfile);

            if (outputfile == null) {
                outputfile = Path.GetFileNameWithoutExtension(inputfile) +
                    "." + Options.OutputFile.Extension;
                }
            if (Options.Lazy.Value & FileTools.UpToDate(inputfile, outputfile)) {
                return;
                }

            Console.WriteLine("Process file {0} to {1}", inputfile, outputfile);

            using (Stream scriptfile =
                new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {
                using (Stream outputStream =
                    new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {
                    using (TextWriter outputText = new StreamWriter(outputStream)) {
                        Goedel.Tool.Script.Script Script = new Goedel.Tool.Script.Script();
                        Script.Process(scriptfile, inputfile, outputText);
                        }
                    }
                }
            }


        public override void Wrap(Wrap Options) {
            string inputfile = Options.InputFile.Text;
            string csfile = DefaultFile(Options.CS, inputfile);


            if (Options.Lazy.Value & FileTools.UpToDate(inputfile, csfile)) {
                return;
                }

            Console.WriteLine("Process file {0} to {1}", inputfile, csfile);

            using (Stream scriptfile =
                new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {
                using (var inText = new StreamReader(scriptfile)) {
                    using (Stream outputStream =
                        new FileStream(csfile, FileMode.Create, FileAccess.Write)) {
                        using (TextWriter outputText = new StreamWriter(outputStream)) {
                            DoWrap.CS(inText, outputText,
                                Options.Namespace.Value, Options.Class.Text, Options.Variable.Text);
                            }
                        }
                    }
                }
            }

        }
    }
