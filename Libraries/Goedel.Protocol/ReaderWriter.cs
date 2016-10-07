﻿//   Copyright © 2015 by Comodo Group Inc.
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
using System.Collections.Generic;
using System.IO;
using System.Text;




namespace Goedel.Protocol {

    public abstract class CharacterStream {
        public long Count = 0;
        public abstract bool EOF { get; }

        public abstract char LookNext();
        public abstract char GetNext();

        }

    public abstract class BufferedCharacterStream : CharacterStream {

        public abstract void Mark();

        public abstract void Restore();
        }

    public class StringCharacterStream : BufferedCharacterStream {
        string Source;
        int Position = 0;
        public override bool EOF { get { return Position >= Source.Length; } }

        public StringCharacterStream(string Source) {
            this.Source = Source;
            }

        public override char LookNext() {
            return Source[Position];
            }

        public override char GetNext() {
            return Source[Position++];
            }


        int MarkPosition = -1;
        public override void Mark() {
            MarkPosition = Position;
            }

        public override void Restore() {
            if (MarkPosition >= 0) {
                Position = MarkPosition;
                }
            }

        }

    public class TextCharacterTextStream : CharacterStream {
        TextReader Source;
        bool _EOF;
        public override bool EOF { get { return _EOF; } }

        public TextCharacterTextStream(TextReader Source) {
            this.Source = Source;
            _EOF = false;
            }

        public override char LookNext() {
            var Char = Source.Peek();
            if (Char < 0) {
                _EOF = true;
                return (char)0;
                }
            return (char)Char;
            }

        public override char GetNext() {
            var Char = Source.Read();
            if (Char < 0) {
                _EOF = true;
                return (char)0;
                }
            return (char)Char;
            }

        }


    public abstract class CharacterTextStream : BufferedCharacterStream {
        byte[] Source;
        long Position;

        protected long MarkPosition = -1;
        protected int MarkBuffer;
        protected int Buffer = -1;

        protected bool _EOF = false;
        public override bool EOF { get { return _EOF; } }

        protected abstract int ReadChar();


        public override char LookNext() {
            Peek();
            return (char)Buffer;
            }

        public override char GetNext() {
            Peek();
            Buffer = -1;
            return (char)Buffer;
            }

        void Peek() {
            if (Buffer > 0) {
                return;
                }

            var Byte1 = ReadChar();
            if (_EOF) {
                return;
                }

            if (Byte1 >= 0xf0) {
                // 4 byte sequence
                var Byte2 = ReadChar();
                var Byte3 = ReadChar();
                var Byte4 = ReadChar();
                Buffer = ((Byte1 & 0x1f) << 15) | ((Byte2 & 0x3f) << 12) |
                    ((Byte3 & 0x1f) << 6) | (Byte4 & 0x3f);
                }
            else if (Byte1 >= 0xe0) {
                // 3 byte sequence
                var Byte2 = ReadChar();
                var Byte3 = ReadChar();
                Buffer = ((Byte1 & 0x1f) << 12) | ((Byte2 & 0x3f) << 6) |
                    (Byte3 & 0x1f);
                }
            else if (Byte1 >= 0xc0) {
                // 2 byte sequence
                var Byte2 = ReadChar();
                Buffer = ((Byte1 & 0x1f) << 6) | (Byte2 & 0x3f);
                }
            else {
                Buffer = Byte1;
                }

            }

        }



    public class DataCharacterTextStream : CharacterTextStream {
        byte[] Source;
        long Position;

        public override void Mark() {
            MarkPosition = Position;
            MarkBuffer = Buffer;
            }

        public override void Restore() {
            if (MarkPosition >= 0) {
                Position = MarkPosition;
                Buffer = MarkBuffer;
                }
            }


        public DataCharacterTextStream(byte[] Source) {
            this.Source = Source;
            Position = 0;
            }

        protected override int ReadChar() {
            if (Position >= Source.Length) {
                _EOF = true;
                return -1;
                }
            var C1 = Source[Position++];
            return C1;
            }
 
        }



    public abstract class Reader {
        protected CharacterStream Input;

        protected char LookNext() {
            return Input.LookNext();
            }

        protected char GetNext() {
            return Input.GetNext();
            }

        protected bool EOF { get { return Input.EOF; } }

        protected void SetReader(TextReader InputIn) {
            Input = new TextCharacterTextStream(InputIn);
            }

        protected void SetReader(string InputIn) {
            Input = new StringCharacterStream(InputIn);
            }


        public Reader(string BufferIn) {
            SetReader(BufferIn);
            }

        public Reader() {
            }



        public Reader(TextReader InputIn) {
            SetReader(InputIn);
            }

        abstract public bool StartObject();
        abstract public void EndObject();
        abstract public bool NextObject();

        abstract public string ReadToken();

        abstract public int ReadInteger32();
        abstract public long ReadInteger64();
        abstract public bool ReadBoolean();
        abstract public byte[] ReadBinary();
        abstract public string ReadString();
        abstract public DateTime ReadDateTime();
        abstract public bool StartArray();
        abstract public bool NextArray();
        }


    public abstract class Writer {

        protected StreamBuffer Output;

        public byte[] GetBytes {
            get { return Output.GetBytes; }
            }

        abstract public void WriteToken(string Tag, int Indent);

        abstract public void WriteInteger32(int Data);
        abstract public void WriteInteger64(long Data);
        abstract public void WriteFloat32(float Data);
        abstract public void WriteFloat64(double Data);
        abstract public void WriteBoolean(bool Data);

        abstract public void WriteString(string Data);
        abstract public void WriteBinary(byte[] Data);
        abstract public void WriteDateTime(DateTime Data);

        // Mark the start, middle and end of array elements
        abstract public void WriteArrayStart();
        abstract public void WriteArraySeparator(ref bool first);
        abstract public void WriteArrayEnd();

        // Mark the start, middle and end of object elements
        abstract public void WriteObjectStart();
        abstract public void WriteObjectSeparator(ref bool first);
        abstract public void WriteObjectEnd();
        }
    }
   