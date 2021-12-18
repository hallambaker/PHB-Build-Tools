using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.FSR;

namespace Goedel.Document.Markdown {

    public class TagValue {
        public string Tag = "";
        public string Value = "";

        public TagValue() {
            }
        public TagValue(string Tag, string Value) {
            this.Tag = Tag;
            this.Value = Value;
            }
        public static List<TagValue> EmptyAttributes = new();
        }


    public partial class MarkDownParagraph {

        string Buffer;     // Text known to be text
        string Extra;      // Text that might be text or an element
        public string Data  => Buffer + Extra; 
            

        public TagValue Element;
        public string Tag;

        public List<TagValue> Attributes;

        void ToBuffer(int c) {
            if ((c == 147) | (c == 148) | (c == 8220) | (c == 8221)) {
                c = '\"';
                }
            else if ((c == 145) | (c == 146) | (c == 8216) | (c == 8217)) {
                c = '\'';
                }

            if (c > 127) {
                Console.WriteLine("Outside RFC values");
                }

            if (c != '\r') {
                Buffer = Buffer + Extra + (char)c;
                }
            else {
                Buffer += Extra;
                }
            Extra = "";
            }
        void ToExtra(int c) => Extra += (char)c;

        public override void Reset() {
            Buffer = "";
            Extra = "";
            Element = null;
            Tag = null;
            Attributes = new List<TagValue>();
            }

        // Transitions
        public virtual void Reset(int c) => Reset();

        public virtual void Null(int c) => ToExtra(c);

        public virtual void AddText(int c) => ToBuffer(c);

        public virtual void GotEscape(int c) => ToExtra(c);

        public virtual void AddeText(int i) {
            char c = (char)i;
            if ((c == '&') | (c == '<') | (c == '>') | (c == '=') |
                (c == '*') | (c == '#') | (c == ':')) {
                Extra = "";     // Suppress the escape character
                }
            ToBuffer(c);
            }

        public virtual void StartTag(int c) {
            if (Element != null) {
                Attributes.Add(Element);
                }
            Element = new TagValue() {
                Tag = "" + (char)c
                };
            ToExtra(c);
            }

        public virtual void AddTag(int c) {
            Element.Tag += (char)c;
            ToExtra(c);
            }

        public virtual void AddCloseTag(int c) {
            char cc = (char)c;

            if (Tag == null) {
                Tag = cc.ToString();
                }
            else {
                Tag += cc;
                }
            ToExtra(c);
            }

        public virtual void AddValue(int c) {
            Element.Value += (char)c;
            ToExtra(c);
            }

        public virtual void ElementEnd(int c) {
            if (Element != null) {
                Attributes.Add(Element);
                }
            Extra = "";
            }

        public static void TestParse(string Data) {
            Console.WriteLine(Data);

            var Reader = new System.IO.StringReader(Data);
            MarkDownParagraph Lexer = new(Reader);

            var Token = Lexer.GetToken();

            while (Token != MarkDownParagraph.Token.INVALID) {
                Console.WriteLine("   {0} : {1}", Token, Lexer.Data);
                foreach (var Tag in Lexer.Attributes) {
                    Console.WriteLine("      {0} = {1}", Tag.Tag, Tag.Value);
                    }
                Token = Lexer.GetToken();
                }
            }
        }
    }
