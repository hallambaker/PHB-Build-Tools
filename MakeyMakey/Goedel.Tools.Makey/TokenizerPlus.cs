using System;
using System.Text;
using System.Collections.Generic;
//using Goedel.Registry;
using Goedel.FSR;

namespace Goedel.Tool.Makey {

    /*
     * Recoginze the following as tokens:
     * 
     * Start:       Label(Item) = Item [, Item]  <CR>
     * End:         EndLabel <CR>
     * TagValue:    Item = Item
     */


    public partial class Tokenizer {

        private static T IfExists<T> (List<T> List, int Index) {
            if (List == null) return default(T);
            return (List.Count > Index) ? List [Index] : default(T);
            }



        public override int GetTokenInt(int StartState) {
            StateInt = StartState;

            //Note that we call Reset() after setting the initial state
            //This allows a reset method to take different actions depending
            //on which type of data is being parsed.
            Reset();

            bool Going = Reader.Get();
            while (Going) {

                //Console.Write(Reader.LastChar);

                int c = Reader.LastInt;
                int ct = ((c >= 0) & (c < CharacterMappings.Length)) ?
                    CharacterMappings[c] : 0;

                //Console.WriteLine("  {0} {1} {2}", StateInt, c, ct);

                NextState = CompressedTransitions[StateInt, ct];

                if (NextState >= 0) {
                    Action Action = Actions[(int)NextState];
                    Action(Reader.LastInt);
                    Going = Reader.Get();
                    StateInt = NextState;
                    }
                else {
                    Going = false;
                    Reader.UnGet();
                    }
                }

            return StateInt;
            }


        Accumulate Accumulate = new Accumulate();

        public string Tag { get; set; }
        public string Key { get; set; }
        public List<string> Values { get; set; } 

        public string Value { get { return Value0; }  }
        public string Value0 { get { return IfExists(Values, 0); } }
        public string Value1 { get { return IfExists(Values, 1); } }
        public string Value2 { get { return IfExists(Values, 2); } }

        /// <summary>
        /// Reset all buffers and output values for the next token.
        /// </summary>
        public override void Reset () {
            Values = new List<string>();
            Tag = null;
            Clear();
            }

        /// <summary>
        /// Reset all buffers.
        /// </summary>
        void Clear() {
            Accumulate.Clear();
            }

        /// <summary>
        /// Get the value of the current item and clear the input buffers.
        /// </summary>
        string CurrentItem() {
            return Accumulate.CurrentItem();
            }

        //Add data to the current input stripping leading and trailing spaces
        void AddCurrent(int c) {
            Accumulate.AddCurrent(c);
            }


        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void Reset(int c) {
            Reset();
            }

        /// <summary>
        /// Complete a start token. This has the pattern 
        /// Label( {Item} ) = {Item} [, {Item}]
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void StartFinalize(int c) {
            Values.Add(CurrentItem());
            //Console.WriteLine("Start: |{0}|{1}|", Tag, Key);
            foreach(var Val in Values) {
                //Console.WriteLine ("    |{0}|", Val);
                }
            }

        /// <summary>
        /// Complete a TagValue token. This is any line that does not
        /// match the Start token pattern that matches the pattern
        /// {Item} = {Item}
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void TagValueFinalize(int c) {
            Values.Add(CurrentItem());
            //Console.WriteLine("Tag: |{0}| = |{1}|", Tag, Value);
            }

        /// <summary>
        /// Complete an End token This is any line that does not
        /// match either the Start or TagValue tokens
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void EndFinalize(int c) {

            Tag = CurrentItem();

            if (String.Compare (Tag, 0, "End", 0, 3) != 0) {
                NextState = (int)State.LineComplete;
                //Console.WriteLine("Line: {0}", Tag);
                }
            else {
                //Console.WriteLine("End: {0}", Tag);
                }

            }



        /// <summary>
        /// Abort the current scan and ignore the data.
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void Abort(int c) {

            }


        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void Ignore(int c) {
            }




        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void GotTag(int c) {
            Tag = CurrentItem();
            }




        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void GotStartTag(int c) {
            Key = CurrentItem();
            }


        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="c">Character that was read</param>
        public virtual void GotItem(int c) {
            Values.Add(CurrentItem());
            }

        }
    }
