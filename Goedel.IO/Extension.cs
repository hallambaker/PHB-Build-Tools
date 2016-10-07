using System;
using System.IO;
using System.Collections.Generic;

namespace Goedel.IO {
    public  static partial class Extension {


        public static FileStream OpenFileRead(this string Filename) {
            return new FileStream(Filename, FileMode.Open, FileAccess.Read);
            }

        public static FileStream OpenFileReadShared(this string Filename) {
            return new FileStream(Filename, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite);
            }

        public static TextReader OpenTextReader(this FileStream FileStream) {
            return new StreamReader(FileStream);
            }

        public static TextReader OpenTextReader (this string Filename) {
            var FileStream = Filename.OpenFileRead();
            return new StreamReader(FileStream);
            }


        public static FileStream OpenFileNew(this string Filename) {
            return new FileStream(Filename, FileMode.Create, FileAccess.Write);
            }

        public static FileStream OpenFileWrite(this string Filename) {
            return new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write);
            }

        public static FileStream OpenFileAppend(this string Filename) {
            return new FileStream(Filename, FileMode.Append, FileAccess.Write);
            }

        public static FileStream OpenFileAppendShare(this string Filename) {
            return new FileStream(Filename, FileMode.Append, FileAccess.Write,
                FileShare.Read);
            }

        public static TextWriter OpenTextWriter(this FileStream FileStream) {
            return new StreamWriter(FileStream);
            }

        public static TextWriter OpenTextWriter(this string Filename) {
            var FileStream = Filename.OpenFileRead();
            return new StreamWriter(FileStream);
            }

        }
    }
