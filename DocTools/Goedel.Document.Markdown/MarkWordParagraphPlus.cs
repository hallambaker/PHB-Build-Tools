using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Goedel.Document.Markdown {
    public partial class MarkNewParagraph {

        List<string> Normative= new() {
            "MUST", "SHALL", "SHOULD", "REQUIRED", "RECOMMENDED", "MAY", "OPTIONAL",
            "MUST NOT", "SHALL NOT", "SHOULD NOT"
                };
        int Negatable = 3;
        int Index = -1;

        //Block Block;
        public List<TextSegment> Segments;

        public MarkNewParagraph (Block Block) {
            //this.Block = Block;
            Block.Segments ??= new List<TextSegment>();
            Segments = Block.Segments;
            }

        public MarkNewParagraph(List<TextSegment> Segments = null) => this.Segments = Segments ?? new List<TextSegment>();

        StringBuilder Buffer = new();
        StringBuilder Upper = new();
        StringBuilder Extra = new();

        public List<TagValue> Attributes = new();
        public string Tag;
        public TagValue Element;

        

        void Reset (int c) {
            Buffer.Clear();
            Upper.Clear();
            Extra.Clear();
            Element = null;
            Tag = null;
            Attributes = new List<TagValue>();
            }

        public override void PushEnd (string Text = null) {
            if (Text != null) {
                Push(Text);
                }
            CheckNormative();
            MakeSegment();
            PopAnnotationAll();
            }

        public virtual void AddText (int c) {
            CheckNormative();

            //Buffer.Append(Upper.ToString());
            //Upper.Clear();
            Buffer.Append((char)c);
            }

        public virtual void AddUpper(int c) => Upper.Append((char)c);

        public virtual void AddUpperSpace (int c) {
            CheckNormative(true);

            if (Index >= 0) {
                Upper.Append((char)c);
                }
            else {
                Buffer.Append((char)c);
                }
            }

        //void Complete () {

        //    }

        void CheckNormative (bool IsSpace=false) {
            if (Upper.Length == 0) {
                return;
                }
            var Text = Upper.ToString();
            Upper.Clear();

            if (Index >= 0) {
                if (Text == " NOT") {
                    MakeBCP14(Normative[Index], true);
                    return;
                    }
                MakeBCP14(Normative[Index], false);
                }

            Index = Normative.IndexOf(Text);
            if (Index == -1) {
                Buffer.Append(Text);
                return;    
                }

            if ((Index >= Negatable) | !IsSpace) {
                MakeBCP14(Normative[Index], false);
                }

            }


        public void MakeSegment () {
            Buffer.Append(Upper.ToString());
            Upper.Clear();

            if (Buffer.Length > 0) {
                SegmentText(Buffer.ToString());
                Buffer.Clear();
                }
            }

        void MakeBCP14 (string Text, bool Not) {
            MakeSegment();
            var Attributes = new List<TagValue> { new TagValue() { Tag = "bcp14" } };
            if (Not) {
                SegmentFull("bcp14", Attributes, Text+" NOT");
                }
            else {
                SegmentFull("bcp14", Attributes, Text);
                }
            Index = -1;
            }

        public void SegmentFull (string Tag, List<TagValue> Attributes, string Text = null) {
            var Start = SegmentStart(Tag, Attributes);
            Start.IsEmpty = Text == null;
            if (Text != null) {
                SegmentText(Text);
                }
            SegmentEnd(Start);
            }



        // Action routines.

        public virtual void Null(int c) => Extra.Append((char)c);

        public virtual void GotEscape(int c) => Extra.Append((char)c);

        public virtual void AddeText (int i) {
            char c = (char)i;
            if (!((c == '&') | (c == '<') | (c == '>') | (c == '=') |
                (c == '*') | (c == '#') | (c == ':'))) {
                Buffer.Append(Extra.ToString());
                }
            Extra.Clear();
            Buffer.Append((char)c);
            }

        public virtual void StartTag (int c) {
            if (Element != null) {
                Attributes.Add(Element);
                }
            Element = new TagValue() {
                Tag = "" + (char)c
                };
            Extra.Append((char)c);
            }

        public virtual void AddTag (int c) {
            Element.Tag += (char)c;
            Extra.Append((char)c);
            }

        public virtual void AddCloseTag (int c) {
            char cc = (char)c;

            if (Tag == null) {
                Tag = cc.ToString();
                }
            else {
                Tag += cc;
                }
            Extra.Append((char)c);
            }

        public virtual void AddValue (int c) {
            Element.Value += (char)c;
            Extra.Append((char)c);
            }

        public Token CurrentToken  => Tokens[NextState]; 

        public virtual void ElementEnd (int c) {
            if (Element != null) {
                Attributes.Add(Element);
                }

            MakeSegment();
            if (CurrentToken == Token.Open) {
                var Segment = SegmentStart(Attributes[0].Tag, Attributes);
                StackAnnotation.Push(Segment);
                XMLTagMode = true;
                }
            else if (CurrentToken == Token.Close) {
                PopAnnotation(Tag);
                }
            else if (CurrentToken == Token.Empty) {
                var Segment = SegmentStart(Attributes[0].Tag, Attributes);
                Segment.IsEmpty = true;
                SegmentEnd(Segment);
                }
            Extra.Clear();
            }


        public bool XMLTagMode = false;

        public Stack<TextSegmentOpen> StackAnnotation = new();

        

        public void PopAnnotationAll () {
            while (PopAnnotation() != null) {
                }
            }

        // Utility function for managing the Annotation stack
        // Utility function for managing the Annotation stack
        public TextSegmentOpen PopAnnotation () {
            if (StackAnnotation.Count <= 0) {
                XMLTagMode = false;
                return null;
                }
            var Segment = StackAnnotation.Pop();
            XMLTagMode = StackAnnotation.Count > 0;
            SegmentEnd(Segment);
            return Segment;
            }

        public void PopAnnotation (string Key) {
            TextSegmentOpen Top;
            do {
                Top = PopAnnotation();
                if (Top == null) {
                    return;
                    }
                } while (Top.Tag != Key);
            }


        // Convenience routines to add segments.
        public TextSegmentOpen SegmentStart (string Tag, List<TagValue> Attributes = null) {
            if (Attributes == null) {
                Attributes = new List<TagValue> { new TagValue(Tag, null) };
                }

            var Start = new TextSegmentOpen() {
                Tag = Tag,
                Attributes = Attributes
                };
            Segments.Add(Start);
            return Start;
            }

        public void SegmentText(string Text) => Segments.Add(new TextSegmentText(Text));

        public void SegmentEnd (TextSegmentOpen Start) {
            var End = new TextSegmentClose(Start);
            Segments.Add(End);
            }

        }
    }
