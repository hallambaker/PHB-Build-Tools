using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Goedel.Registry;
using Goedel.IO;

namespace Goedel.Tool.Constant {

    public partial class Constant {

        public void Markdown() {
            foreach (var entry in Top) {

                switch (entry) {
                    case Namespace @namespace: {
                        Namespace = @namespace;
                        break;
                        }
                    case File file: {
                        file.Markdown(this);
                        break;
                        }
                    }


                }





            }
        }
    public partial class File {


        public void Markdown(Constant constant) {

            var filename = constant.Class + Id.Label + ".md";
            using (var output = filename.OpenTextWriterNew()) {

                //output.WriteLine("~~~~");
                //output.WriteLine($"{filename}");


                bool codes = false;
                foreach (var entry in Entries) {

                    switch (entry) {
                        case Code codeEntry: {
                            codes = true;
                            break;
                            }
                        case String stringEntry: {
                            stringEntry.Markdown(output);
                            break;
                            }
                        case Function functionEntry: {
                            functionEntry.Markdown(output);
                            break;
                            }
                        case Enum enumEntry: {
                            enumEntry.Markdown(output);
                            break;
                            }
                        }
                    }
                if (codes) {
                    foreach (var entry in Entries) {
                        output.WriteLine($"<dl>");
                        switch (entry) {
                            case Code codeEntry: {
                                codeEntry.Markdown(output);
                                break;
                                }
                            }
                        output.WriteLine($"</dl>");
                        }
                    
                    }

                //output.WriteLine("~~~~");
                }
            }
        }

    public partial class _Choice {
        public void MarkdownTableHead(TextWriter output) {
            output.WriteLine($"<table>");
            //output.WriteLine($"<thead>");
            MarkdownRowStart(output);
            }
        public void MarkdownTableBody(TextWriter output) {
            MarkdownRowEnd(output);
            //output.WriteLine($"</thead>");
            //output.WriteLine($"<tbody>");
            }
        public void MarkdownTableEnd(TextWriter output) {
            //output.WriteLine($"</tbody>");
            output.WriteLine($"</table>");
            }
        public void MarkdownRowStart(TextWriter output) {
            output.Write($"<tr>");
            }
        public void MarkdownRowEnd(TextWriter output) {
            output.WriteLine($"</tr>");
            }

        public void MarkdownData(TextWriter output, string data) {
            output.Write($"<td>{data}</td>");
            }

        }

    public partial class Code {


        public void Markdown(TextWriter output) {
            output.WriteLine($"<dt>{Title}");
            output.WriteLine($"<dd>");
            foreach (var line in Description.Text) {
                output.WriteLine($"    {line}");
                }
            output.WriteLine($"</dd>");
            }

        }
    public partial class String {


        public void Markdown(TextWriter output) {
            output.WriteLine("String");
            }


        }
    public partial class Function {


        public void Markdown(TextWriter output) {
            output.WriteLine("Function");
            }


        }
    public partial class Enum {


        public void Markdown(TextWriter output) {
            if (UDF?.Count > 0) {
                MarkdownTableHead(output);
                MarkdownData(output, "Type ID");
                MarkdownData(output, "Initial");
                MarkdownData(output, "Algorithm");
                MarkdownTableBody(output);
                foreach (var udf in UDF) {
                    MarkdownRowStart(output);
                    MarkdownData(output, udf.Value.ToString());
                    MarkdownData(output, udf.Initial.ToString());
                    MarkdownData(output, udf.Text);
                    MarkdownRowEnd(output);
                    }
                MarkdownTableEnd(output);
                }

            if (Integer?.Count > 0) {
                MarkdownTableHead(output);
                MarkdownData(output, "Code");
                MarkdownData(output, "Algorithm");
                MarkdownData(output, "Description");

                MarkdownTableBody(output);
                foreach (var item in Integer) {
                    MarkdownRowStart(output);
                    if (item.Reserve.End == 0) {
                        MarkdownData(output, item.Value.ToString());

                        }
                    else {
                        MarkdownData(output, $"{item.Value}-{item.Reserve.End}");
                        }
                    MarkdownData(output, item.Id.Label);
                    MarkdownData(output, item.Title);
                    MarkdownRowEnd(output);
                    }
                MarkdownTableEnd(output);
                }
            }


        }
    public partial class UDF {

        public char Initial => (char)('A' + (Value >> 3));

        public string Text {
            get {
                if (Note?.Text != null) {
                    return Note.Text;
                    }
                if (Algorithm != null) {
                    return Compress.Bits > 0 ? Algorithm.Id.Label :
                        $"{Algorithm.Id.Label} ({Compress.Bits} compressed)";
                    }

                return null;
                }


            }
        }

    }