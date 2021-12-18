using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Goedel.Document.RFC {


    /// <summary>
    /// Output class performing nroff style page formatting.
    /// </summary>
    public class PageWriter {
        TextWriter TextWriter;
        public int MaxCol;
        public int MaxLine;

        static int HeaderMargin = 2;
        static int FooterMargin = 5;

        public int LastLine => MaxLine - FooterMargin;

        public string HeaderLeft;
        public string HeaderCenter;
        public string HeaderRight;

        public string FooterLeft;
        public string FooterCenter;
        public string FooterRight;

        public int HeaderLine = 1;
        public int FooterLine => MaxLine - 1;

        public int MustFillLine => (MaxLine - FooterMargin - 10);

        public int Line;
        public int Page;

        public bool AddFormFeed = true;

        public string[] Lines;
        public int BreakLine = 1;

        // To avoid making a club header, we buffer the header here before writing it out

        public bool AllowBreak = false;

        public int BlankLines = 0;

        public void AddBlank(int Lines) => BlankLines = Lines > BlankLines ? Lines : BlankLines;

        public void AddBreak(int Lines) {
            AddBlank(Lines);
            BreakLine = Line;
            }

        public void DoBlank() {
            for (int i = 0; i < BlankLines; i++) {
                //TextWriter.WriteLine();
                AdvanceLine();
                }
            BlankLines = 0;
            }


        public List<string> Heading = new();

        /// <summary>
        /// Create a new instance using the specified TextWriter as output
        /// and the default page length (58) and width (72);
        /// </summary>
        /// <param name="TextWriter">The output stream</param>
        public PageWriter(TextWriter TextWriter)
            : this(TextWriter, 58, 72) {
            }

        /// <summary>
        /// Create a new instance using the specified TextWriter as output
        /// and the specified page length and width ;
        /// </summary>
        /// <param name="TextWriter">The output stream</param>
        /// <param name="MaxLine">The page length</param>
        /// <param name="MaxCol">The page width</param>
        public PageWriter(TextWriter TextWriter, int MaxLine, int MaxCol) {
            this.TextWriter = TextWriter;
            this.MaxLine = MaxLine;
            this.MaxCol = MaxCol;
            Lines = new string[MaxLine];
            Page = 1;
            Line = 0;
            }

        static string Padding(int chars) => "".PadLeft(chars);

        static string FlushLeft(ref string Source, int Left, int Right) {
            string Text = Consume(ref Source, Right - Left);
            if (Text == "") {
                return "";
                }

            //var Result = Padding(Left) + Text;
            //if (Result.Length > 72) {

            //    }

            return Padding(Left) + Text.Trim();
            }
        static string Centered(ref string Source, int Left, int Right) {
            string Text = Consume(ref Source, Right - Left);
            if (Text == "") {
                return "";
                }
            return Padding(Left + ((Right - Text.Length) / 2)) + Text.Trim();
            }
        static string FlushRight(ref string Source, int Left, int Right) {
            string Text = Consume(ref Source, Right - Left);
            if (Text == "") {
                return "";
                }
            return Padding(Left + Right - Text.Length) + Text.Trim();
            }

        static bool IsWhitespace(char c) {
            if (c == ' ') {
                return true;
                }
            return false;
            }

        public static string Consume(ref string Source, int Max) {
            string Result = "";
            string WhiteSpace = "";
            string Buffered = "";

            int State = 0;
            bool Scan = true;
            int Index;
            int LastConsumed = 0;

            if (Source == null) {
                Source = "";
                }


            for (Index = 0; (Index < Source.Length) & Scan; Index++) {
                char c = Source[Index];
                if (State == 0) {
                    if (!IsWhitespace(c)) {
                        Result += c;
                        LastConsumed = Index + 1;
                        State = 1;
                        }
                    }
                else if (State == 1) {
                    if (IsWhitespace(c)) {
                        LastConsumed = Index;
                        Result += Buffered;
                        Buffered = "";
                        WhiteSpace = " ";
                        State = 2;
                        }
                    if (c == '-') {
                        if ((Result.Length + Buffered.Length) < Max) {
                            Result += Buffered + c;
                            Buffered = "";
                            LastConsumed = Index + 1;
                            }
                        else {
                            //Console.WriteLine ("Caught {0}", Result);
                            }
                        }
                    else {
                        Buffered += c;
                        }
                    }
                else if (State == 2) {
                    if (IsWhitespace(c)) {
                        LastConsumed = Index;
                        WhiteSpace += " ";
                        }
                    else {
                        Result += WhiteSpace;
                        WhiteSpace = "";
                        Buffered = c.ToString();
                        State = 1;
                        }

                    }
                Scan = (Result.Length + Buffered.Length) <= Max;
                }

            if ((Result.Length + Buffered.Length) <= Max) {
                Result += Buffered;
                LastConsumed = Index;
                }



            // Remove the characters that we used
            Source = Source.Remove(0, LastConsumed);

            return Result;
            }

        void AdvanceLine() {
            Line++;
            if (Line >= LastLine) {
                WritePage();
                }
            }


        // Write a line of text 

        public void WriteLine() => AdvanceLine();
        public void WriteLine(string Text) {
            //TextWriter.WriteLine (Text);


            Lines[Line] += Text;
            //if (AllowBreak) {
            //    BreakLine = CurrentLine;
            //    }

            AdvanceLine();
            }


        // This is the routine all writes will be channelled through

        public enum Format {
            Heading, FlushLeft, FlushRight, Centered
            }

        public void WriteTOC(string Heading, int First, int Left, string Page) {
            if (Page.Length % 2 > 0) {
                Page = " " + Page;
                }
            int Right = MaxCol - Page.Length;
            string Text = "".PadLeft(First) + Consume(ref Heading, Right - First);

            DoBlank();

            while (Heading.Length > 0) {
                WriteLine(Text);
                Text = "".PadLeft(Left) + Consume(ref Heading, Right - Left);
                }
            Text += " ";
            if ((Text.Length % 2) == 1) {
                Text += " ";
                }
            while (Text.Length < Right) {
                Text += ". ";
                }
            Text += Page;
            WriteLine(Text);
            //Console.WriteLine (Text.Length);
            }

        public void WritePreformated(String Text, int First, int Left,
            int Above, int Below) {

            if (Text == null) {
                Console.WriteLine("Null Preformatted");
                return;
                }


            AddBlank(Above);
            DoBlank();

            string CurrentLine = "";

            bool ForceOutput = Line < MustFillLine;

            foreach (char c in Text) {
                if (c == '\n') {
                    WriteLine("".PadLeft (First) + CurrentLine);
                    CurrentLine = "";
                    if (ForceOutput) {
                        BreakLine = Line;
                        }
                    }
                else {
                    CurrentLine += c;
                    }
                }
            if (CurrentLine.Length > 0) {
                WriteLine("".PadLeft (First) + CurrentLine);
                }


            AddBlank(Below);
            return;
            }

        public void Write(string Source, int First, int Left, Format Format, 
                        int Above, int Below) {

            if (Source == "") {
                return; // nothing to write
                }

            AddBlank(Above);
            DoBlank();

            int Right = MaxCol - (Lines[Line] == null ? 0 : Lines[Line].Length);
            string Text = null;
            switch (Format) {
                case Format.Heading: Text = FlushLeft(ref Source, First, Right); break;
                case Format.FlushLeft: Text = FlushLeft(ref Source, First, Right); break;
                case Format.FlushRight: Text = FlushRight(ref Source, First, Right); break;
                case Format.Centered: Text = Centered(ref Source, First, Right); break;
                }

            if (Text == "") {
                return; // nothing to write
                }

            //Console.WriteLine($"[{Text}]");
            WriteLine(Text);

            while (Source.Length > 0) {
                switch (Format) {
                    case Format.Heading: Text = FlushLeft(ref Source, Left, Right); break;
                    case Format.FlushLeft: Text = FlushLeft(ref Source, Left, Right); break;
                    case Format.FlushRight: Text = FlushRight(ref Source, Left, Right); break;
                    case Format.Centered: Text = Centered(ref Source, Left, Right); break;
                    }
                if (Text != "") {
                    WriteLine(Text);
                    }
                if (Format == Format.FlushLeft) {
                    BreakLine = Line; // always allow break on second line in paragraphs
                    }
                }

            AddBlank(Below);
            }

        string MakeBanner(string Left, string Center, string Right) {
            string Result = Left + " ";

            int Remaining = MaxCol - Result.Length - Right.Length - 2;

            if (Remaining < 0) {
                throw new Exception("Header or Footer won't fit");
                }

            int PadLeft = (MaxCol - Center.Length) / 2;
            if (PadLeft > Result.Length) {
                Result += "".PadLeft(PadLeft - Result.Length) + Center;
                }
            else {
                Result += Center;
                }

            Result += "".PadLeft(MaxCol - Result.Length - Right.Length - 1);

            return Result + " " + Right;
            }

        string MakeHeader() => MakeBanner(HeaderLeft, HeaderCenter, HeaderRight);

        string MakeFooter() => MakeBanner(FooterLeft, FooterCenter, String.Format("[Page {0}]", Page));


        public void NewPage() {
            //WriteLine (MakeHeader ());
            //WriteLine (MakeFooter ());
            int i;
            int ClubLines = Line - BreakLine - 1;

            for (i = 0; i < HeaderMargin; i++) {
                Lines[i] = "";
                }
            for (int j = 0; j < ClubLines ; j++) {
                Lines[i++] = Lines [BreakLine + j +1];
                }

            for (;i < Lines.Length; i++) {
                Lines[i] = "";
                }

            Lines[HeaderLine - 1] = MakeHeader();
            Line = HeaderMargin + ClubLines;
            BreakLine = -1;
            Page++;
            }

        // The only method allows to write to the TextWriter
        public void LastPage() {
            WritePage(FooterLine);
            TextWriter.Flush ();
            }

        public void WritePage() => WritePage(Lines.Length);

        public void WritePage(int WriteLines) {
            Lines[FooterLine - 1] = MakeFooter();

            if (BreakLine < 0) {
                BreakLine = LastLine;
                }
            //if (BreakLine < MustFillLine) {
            //    BreakLine = CurrentLine;
            //    }

            for (int i=0; i<WriteLines; i++) {
                string s = Lines[i];

                if (s != null && s.Length > 72) {
                    Console.WriteLine ("Line too long {1}\n{0}", s, s.Length);
                    }

                if (i > BreakLine & i < LastLine & BreakLine > HeaderMargin 
                        & i < Line ) {
                    TextWriter.WriteLine();
                    }
                else if (s != null) {
                    TextWriter.WriteLine(s);
                    }
                else {
                    TextWriter.WriteLine();
                    }
                }
            if (AddFormFeed & (WriteLines == Lines.Length)) {
                TextWriter.Write ("\f");
                }
            TextWriter.Flush();

            NewPage();

            }

        }
    }
