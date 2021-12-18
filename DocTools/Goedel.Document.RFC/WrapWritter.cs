using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Goedel.Utilities;

namespace Goedel.Document.RFC {
    public partial class WrapWriter : Disposable {

        public TextWriter TextWriter;
        public int MaxCol;

        int col = 0;
        int space = 0;
        int start = 0;

        StringBuilder builder = new();
        StringBuilder spaces = new();

        protected override void Disposing() {
            TextWriter.Write(builder.ToString());
            }

        public WrapWriter(TextWriter textWriter, int maxCol = 69) {
            TextWriter = textWriter;
            MaxCol = maxCol;
            }

        public void Write(string Text) {
            foreach (var c in Text) {
                AddChar(c);
                }


            }

        void AddChar(char c) {
            if (c == ' ') {
                spaces.Append(c);
                space++;
                if (builder.Length == 0) {
                    start++;
                    }
                }
            else if (c == '\n') {
                WriteLine();
                start = 0;
                }
            else {
                if ((col + space) >= MaxCol) {
                    WriteLine();
                    col = start + 4;
                    for (var i = 0; i < col; i++) {
                        builder.Append(' ');
                        }
                    }
                else {
                    builder.Append(spaces.ToString());
                    col += space;
                    }
                spaces.Clear();
                space = 0;
                builder.Append(c.ToHTMLEntity());
                col++;
                }


            }


        void WriteLine() {
            TextWriter.WriteLine(builder.ToString());
            //Console.WriteLine ($"Build length {builder.Length}");
            builder.Clear();
            spaces.Clear();
            space = 0;
            col = 0;
            }

        }
    }
