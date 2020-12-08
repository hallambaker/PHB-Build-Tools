using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Document.Markdown {
    public class Writer : IDisposable {
        protected Stream Stream = null;
        protected TextWriter TextWriter = null;

        // Public constructors
        public Writer(FileInfo FileInfo) => Init(FileInfo);

        public Writer(string FilePath) => Init(FilePath);

        // The real constructors
        protected void Init (FileInfo FileInfo) {
            string FilePath = FileInfo.DirectoryName + @"\" + FileInfo.Name;
            Init(FilePath);
            }

        protected void Init(string FilePath) => Init(new FileStream(FilePath, FileMode.Create, FileAccess.Write));

        protected void Init (Stream Stream) {
            this.Stream = Stream;
            Init(new StreamWriter(Stream));
            }

        protected void Init(TextWriter TextWriter) => this.TextWriter = TextWriter;


        public virtual void Dispose () {
            if (TextWriter != null) {
                TextWriter.Dispose();
                }

            if (Stream != null) {
                Stream.Dispose();
                }
            }

        public Encoding Encoding => TextWriter.Encoding;

        // Formatting convenience routines

        public void WriteEscape(string format) {
            foreach (char c in format) {
                switch (c) {
                    case '&': TextWriter.Write("&amp;"); break;
                    case '<': TextWriter.Write("&lt;"); break;
                    case '>': TextWriter.Write("&gt;"); break;
                    default: TextWriter.Write(c); break;
                    }
                }
            }


        public void Write(string format) => TextWriter.Write(format);
        public void Write(string format, Object arg) => TextWriter.Write(format, arg);
        public void Write(string format, params Object[] arg) => TextWriter.Write(format, arg);

        public void WriteLine() => TextWriter.WriteLine();

        public void WriteLine(string format) => TextWriter.WriteLine(format);
        public void WriteLine(string format, Object arg) => TextWriter.WriteLine(format, arg);
        public void WriteLine(string format, params Object[] arg) => TextWriter.WriteLine(format, arg);

        public void WriteLine (string[] formats) {
            foreach (var format in formats) {
                TextWriter.WriteLine(format);
                }
            }
        public void WriteLine (string[] formats, params Object[] arg) {
            foreach (var format in formats) {
                TextWriter.WriteLine(format, arg);
                }
            }
        }

    public class HTMLWriter : Writer {


        // Public constructors
        public HTMLWriter (FileInfo FileInfo) : base (FileInfo) {
            }

        public HTMLWriter (string FilePath) : base (FilePath) {
            }

        public void StartHead () {
            WriteLine("<!DOCTYPE html>");
            WriteLine ("<html lang=\"en\">");
            WriteLine ("<head>");
            }

        public void StartBody () {
            WriteLine ("</head>");
            WriteLine ("<body>");
            }

        public override void Dispose () {
            WriteLine ("</body>");
            WriteLine ("</html>");
            base.Dispose();
            }

        public void WriteLine(string Prefix, string[] Lines) {
            foreach (var Line in Lines) {
                Write (Prefix);
                WriteLine(Line);
                }

            }
        }
    }
