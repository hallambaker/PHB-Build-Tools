//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

//namespace Goedel.Document.Markdown {


//    public abstract class TopReader : IDisposable {


//        public TopReader() {
//            }


//        public abstract void Dispose();

//        public abstract bool Get();

//        public abstract void UnGet();

//        }



//    public class Reader : IDisposable {
//        Stream Stream = null;
//        TextReader TextReader = null;
//        bool Pending = false;

//        public bool EOF = false;
//        public int LastInt;
//        public char LastChar {
//            get { return LastInt > 0 ? (char)LastInt : '.'; }
//            }

//        protected Reader() {
//            }

//        // Public constructors
//        public Reader(FileInfo FileInfo) {
//            Init(FileInfo);
//            }

//        public Reader(string FilePath) {
//            Init(FilePath);
//            }

//        public Reader(Stream Stream) {
//            Init(Stream);
//            }

//        public Reader(TextReader TextReader) {
//            Init(TextReader);
//            }

//        // The real constructors
//        protected void Init(FileInfo FileInfo) {
//            string FilePath = FileInfo.DirectoryName + @"\" + FileInfo.Name;
//            Init(FilePath);
//            }

//        protected void Init(string FilePath) {
//            Init(new FileStream(FilePath, FileMode.Open, FileAccess.Read));
//            }

//        protected void Init(Stream Stream) {
//            this.Stream = Stream;
//            Init(new StreamReader(Stream));
//            }

//        protected void Init(TextReader TextReader) {
//            this.TextReader = TextReader;
//            }

//        public virtual void Dispose() {
//            if (Stream != null) Stream.Dispose();
//            if (TextReader != null) TextReader.Dispose();
//            }

//        public virtual bool Get() {
//            if (Pending & !EOF) {
//                Pending = false;
//                return true;
//                }
//            LastInt = TextReader.Read();
//            if (LastInt < 0) {
//                EOF = true;
//                return false;
//                }
//            else if (LastInt == '\r') {
//                LastInt = TextReader.Read();
//                }

//            return true;
//            }

//        public virtual void UnGet() {
//            Pending = true;
//            }

//        }

//    public class StringReader : Reader {
//        string Data;
//        int Count;

//        public StringReader(string Data) {
//            this.Data = Data;
//            Count = 0;
//            }


//        public override void Dispose() {
//            //if (Stream != null) Stream.Dispose();
//            //if (TextReader != null) TextReader.Dispose();
//            }

//        public override bool Get() {
//            if (Count < Data.Length) {
//                LastInt = (int)Data[Count];
//                Count++;
//                return true;
//                }
//            else {
//                EOF = true;
//                return false;
//                }
//            }

//        public override void UnGet() {
//            if (Count > 0) {
//                Count--;
//                }
//            }
//        }

//    }
