using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goedel.Document.RFC {
    public class SourceFile {
        //string          Filename;
        //int             Start;
        //int             End;
        //int             Line;
        //int             Column;
        public List<SourceFile> SourceFiles = new();
        }


    public class FileReader : TextReader {
        StringWriter StringWriter = new();
        
        string      Buffer = null;
        int         Pointer = 0;

        public SourceFile SourceFile;
        public SourceFile CurrentSourceFile = null;

        public FileReader(string FileName) {
            Include(FileName);

            //Console.WriteLine("-----------------------------");
            //Console.WriteLine("-----------------------------");

            Buffer = StringWriter.ToString();
            }


        public override int Peek() {
            if (Pointer >= Buffer.Length) {
                return -1;
                }
            return (int)Buffer[Pointer];
            }

        public override int Read() {
            if (Pointer >= Buffer.Length) {
                return -1;
                }
            //Console.Write (Buffer[Pointer]);

            return (int)Buffer[Pointer++];
            }



        public void Include(string FileName) {
            using StreamReader StreamReader = new(FileName);
            ReadStream(StreamReader);
            }

        public void IncludeXML(string FileName) {
            using StreamReader StreamReader = new(FileName);
            ReadStreamXML(StreamReader);
            }


        enum Tokens {
            Left = 0,
            Right = 1,
            Quote = 2,
            Question = 3,
            WS = 4,
            Alpha = 5,
            Equals = 6,
            Any = 6
            }

        enum States {
            Start = 0,
            Left = 1,
            Tag = 2,
            Space = 3,
            Attribute = 4,
            Equals = 5,
            Quote = 6,
            Value = 7,
            Right = 8
            }

        private Tokens GetToken(int t) {
            if (t < 0) {
                return Tokens.Any;
                }
            char c = (char)t;

            switch (c) {
                case '<': return Tokens.Left;
                case '>': return Tokens.Right;
                case '?': return Tokens.Question;
                case '=': return Tokens.Equals;
                case '\'':
                case '\"': return Tokens.Quote;
                case ' ':
                case '\t':
                case '\r': return Tokens.WS;
                }

            if ((c >= 'a') & (c <= 'z')) {
                return Tokens.Alpha;
                }
            if ((c >= 'A') & (c <= 'Z')) {
                return Tokens.Alpha;
                }
            return Tokens.Any;
            }

        private int[,] NextState = 
            {
            //    <   >   '   ?  WS  az   =   .
                { 1,  0,  0,  0,  0,  0,  0,  0},  // 0 Start
                { 0,  0,  0,  2,  0,  0,  0,  0},  // 1 Left
                { 0,  0,  0,  0,  3,  2,  0,  0},  // 2 Tag
                { 0,  0,  0,  8,  3,  4,  0,  0},  // 3 Space
                { 0,  0,  0,  0,  0,  4,  5,  0},  // 4 Attribute
                { 0,  0,  7,  0,  6,  0,  0,  0},  // 5 Equals
                { 0,  0,  7,  0,  6,  0,  0,  0},  // 6 Quote
                { 7,  7,  3,  7,  7,  7,  7,  7},  // 7 Value
                { 0,  0,  0,  0,  0,  0,  0,  0}   // 8 Right
                                     };


        class AttributeValue {
            public string Attribute = "";
            public string Value = "";
            }

        private void ReadStreamXML(StreamReader StreamReader) {
            string Processing = "";

            while (!StreamReader.EndOfStream) {
                int c = StreamReader.Read();
                char ch = (char)c;
                if (ch == '<') {
                    Processing += "&lt;";
                    }
                else if (ch == '>') {
                    Processing += "&gt;";
                    }
                else {
                    Processing += ch;
                    }
                }
            StringWriter.Write(Processing);

            //Console.Write (Processing);

            }

        private void ReadStream(StreamReader StreamReader) {
            States State = States.Start;
            string Tag = "";
            List<AttributeValue> AttributeValues = null;
            AttributeValue AttributeValue = null;

            string Processing = "";

            while (!StreamReader.EndOfStream) {
                int c = StreamReader.Read();
                char ch = (char)c;
                Tokens T = GetToken(c);

                States Next = (States)NextState[(int)State, (int)T];

                //if (((int)State > 0) | (((int)Next) > 0)) {
                //    Console.Write((char)c);
                //    }

                if (Next == States.Tag) {
                    if (State == States.Tag) {
                        Tag += ch;
                        }
                    else {
                        Tag = "";
                        AttributeValues = new List<AttributeValue>();
                        }
                    }
                if (Next == States.Attribute) {
                    if (State != States.Attribute) {
                        AttributeValue = new AttributeValue();
                        AttributeValues.Add(AttributeValue);
                        }
                    AttributeValue.Attribute += ch;
                    }
                else if (State == States.Attribute) {
                    }
                if ((State == States.Value) & (Next == States.Value)) {
                    AttributeValue.Value += ch;
                    }
                if (State == States.Right) {
                    Process(Tag, AttributeValues);
                    }

                if (Next == States.Start) {
                    if (State != States.Start) {
                        StringWriter.Write(Processing);
                        Processing = "";
                        }
                    StringWriter.Write(ch);
                    }
                else {
                    Processing += ch;
                    }

                State = Next;
                }
            }

        private string GetValue(List<AttributeValue> AttributeValues, string Attribute) {
            foreach (AttributeValue AttributeValue in AttributeValues) {
                if (AttributeValue.Attribute == Attribute) {
                    return AttributeValue.Value;
                    }
                }
            return null;
            }

        private void Process(string Tag, List<AttributeValue> AttributeValues) {

            //Console.Write("    {0}", Tag);
            //for (int i = 0; i < AttributeValues.Count; i++) {
            //    Console.Write("  {0}={1}", AttributeValues[i].Attribute,
            //        AttributeValues[i].Value);
            //    }
            //Console.WriteLine();

            if (Tag == "include") {
                string Filename = GetValue (AttributeValues, "file");
                if (Filename != null) {
                    Include (Filename);
                    }

                Filename = GetValue (AttributeValues, "xml");
                if (Filename != null) {
                    IncludeXML (Filename);
                    }
                }
            }
        }


    public class FileReader2 : TextReader {

        private class StreamHandle {
            public string FileName;
            public int LineNumber;
            public int LinePosition;
            public StreamReader StreamReader;

            public StreamHandle(string FileName) {
                this.FileName = FileName;
                StreamReader = new StreamReader(FileName);
                LineNumber = 1;
                LinePosition = 0;
                }
            }
        
        List<StreamHandle> StreamHandles = new();
        StreamHandle CurrentHandle => StreamHandles[StreamHandles.Count - 1];

        public int LineNumber => CurrentHandle.LineNumber;
        public int LinePosition => CurrentHandle.LinePosition;
        public string FileName => CurrentHandle.FileName;

        public string position => String.Format("Line {0} Position {1} of {2}",
                    LineNumber, LinePosition, FileName);

        public FileReader2(string FileName) => Include(FileName);

        private bool CheckEOS() {
            if (StreamHandles.Count == 0) {
                return true;
                }

            if (CurrentHandle.StreamReader.EndOfStream) {
                StreamHandles.RemoveAt(StreamHandles.Count - 1);
                }
            return StreamHandles.Count == 0;
            }

        public override int Peek() {
            if (CheckEOS()) {
                return -1;
                }
            throw new Exception ("Internal XML Parser error");
            }

        public override int Read() {
            if (CheckEOS()) {
                return -1;
                }

            int c = CurrentHandle.StreamReader.Read();
            if (c == '\n') {
                CurrentHandle.LineNumber++;
                CurrentHandle.LinePosition = 0;
                }
            else {
                CurrentHandle.LinePosition++;
                }

            if (c > 1) {
                //Console.Write((char) c);
                }

            return c;
            }

        public void Include(string FileName) {
            StreamHandle StreamHandle = new(FileName);
            StreamHandles.Add(StreamHandle);
            }

        }
    }
