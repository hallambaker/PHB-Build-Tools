﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.FSR {

    public class LexReader : IDisposable {
        Stream Stream = null;
        TextReader TextReader = null;
        bool Pending = false;

        public string FilePath = null;

        public bool EOF = false;
        public int LastInt;
        public char LastChar {
            get { return LastInt > 0 ? (char)LastInt : '.'; }
            }

        protected LexReader() {
            }

        // Public constructors

        public LexReader(Stream Stream) {
            Init(Stream);
            }

        public LexReader(TextReader TextReader) {
            Init(TextReader);
            }

        protected void Init(Stream Stream) {
            this.Stream = Stream;
            Init(new StreamReader(Stream));
            }

        protected void Init(TextReader TextReader) {
            this.TextReader = TextReader;
            }

        public virtual void Dispose() {
            if (Stream != null) Stream.Dispose();
            if (TextReader != null) TextReader.Dispose();
            }

        public virtual bool Get() {
            if (Pending & !EOF) {
                Pending = false;
                return true;
                }
            LastInt = TextReader.Read();
            if (LastInt < 0) {
                EOF = true;
                return false;
                }
            else if (LastInt == '\r') {
                LastInt = TextReader.Read();
                }

            return true;
            }

        public virtual void UnGet() {
            Pending = true;
            }

        }

    public class StringReader : LexReader {
        string Data;
        int Count;

        public StringReader(string Data) {
            this.Data = Data;
            Count = 0;
            }


        public override void Dispose() {
            //if (Stream != null) Stream.Dispose();
            //if (TextReader != null) TextReader.Dispose();
            }

        public override bool Get() {
            if (Count < Data.Length) {
                LastInt = (int)Data[Count];
                Count++;
                return true;
                }
            else {
                EOF = true;
                return false;
                }
            }

        public override void UnGet() {
            if (Count > 0) {
                Count--;
                }
            }
        }

    }