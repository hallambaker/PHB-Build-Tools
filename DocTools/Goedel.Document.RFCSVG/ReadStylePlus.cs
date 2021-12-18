using System;
using System.Text;
using System.Collections.Generic;
using Goedel.Utilities;
using Goedel.FSR;
using System.IO;

namespace Goedel.Document.RFCSVG {

    /*
     * Recoginze the following as tokens:
     * 
     * Start:       Label(Item) = Item [, Item]  <CR>
     * End:         EndLabel <CR>
     * TagValue:    Item = Item
     */


    public partial class ReadStyle {


        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Stream">The input source.</param>
        public ReadStyle(string text) : this(new StringReader(text)) {
            }

        //public override int GetTokenInt(int StartState) {
        //    StateInt = StartState;

        //    //Note that we call Reset() after setting the initial state
        //    //This allows a reset method to take different actions depending
        //    //on which type of data is being parsed.
        //    Reset();

        //    bool Going = Reader.Get();
        //    while (Going) {

        //        //Console.Write(Reader.LastChar);

        //        int c = Reader.LastInt;
        //        int ct = ((c >= 0) & (c < CharacterMappings.Length)) ?
        //            CharacterMappings[c] : 0;

        //        //Console.WriteLine("  {0} {1} {2}", StateInt, c, ct);

        //        NextState = CompressedTransitions[StateInt, ct];

        //        if (NextState >= 0) {
        //            ActionDelegate Action = Actions[(int)NextState];
        //            Action(Reader.LastInt);
        //            Going = Reader.Get();
        //            StateInt = NextState;
        //            }
        //        else {
        //            Going = false;
        //            Reader.UnGet();
        //            }
        //        }

        //    return StateInt;
        //    }

        StringBuilder classBuilder = new();
        StringBuilder valueBuilder = new();
        StringBuilder tagBuilder = new();

        public string Class => classBuilder.ToString();
        public string Value => valueBuilder.ToString();
        public string Tag => tagBuilder.ToString();

        public List<TagValue> TagValues;

        void Reset(int c) {
            TagValues = new List<TagValue>();
            classBuilder.Clear();
            valueBuilder.Clear();
            tagBuilder.Clear();
            }
        void AddLabel(int c) => classBuilder.Append((char)c);

        void AddTag(int c) => tagBuilder.Append((char)c);

        void AddValue(int c) => valueBuilder.Append((char)c);


        void CompleteValue(int c) {
            TagValues.Add(new TagValue(Tag, Value));
            valueBuilder.Clear();
            tagBuilder.Clear();
            }

        void Abort(int c) {
            }


        void Null(int c) {
            }

        }


    public struct TagValue {
        public string Value;
        public string Tag;

        public TagValue(string tag, string value) {
            Value = value;
            Tag = tag;
            }
        }

    }
