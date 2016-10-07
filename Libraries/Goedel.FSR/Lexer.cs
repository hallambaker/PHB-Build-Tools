using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Goedel.Utilities;

namespace Goedel.FSR {



    public class Accumulate {
        StringBuilder Current = new StringBuilder();
        StringBuilder WhiteSpace = new StringBuilder();


        bool HaveCharacter = false;
        bool StartQuote = false;

        /// <summary>
        /// Reset all buffers.
        /// </summary>
        public void Clear() {
            Current.Clear();
            WhiteSpace.Clear();
            HaveCharacter = false;
            }

        //Add data to the current input stripping leading and trailing spaces
        public void AddCurrent(int c) {
            var Character = c.ToASCII();
            if (Character == ' ' | Character == '\t') {
                if (HaveCharacter) {
                    WhiteSpace.Append(Character);
                    }
                }
            else {
                if (!HaveCharacter & Character == '\"') {
                    StartQuote = true;
                    }
                else if (!(StartQuote & Character == '\"')) {
                    Current.Append(WhiteSpace.ToString());
                    WhiteSpace.Clear();
                    Current.Append(Character);
                    }
                HaveCharacter = true;
                }
            }

        /// <summary>
        /// Get the value of the current item and clear the input buffers.
        /// </summary>
        public string CurrentItem() {

            var Value = Current.ToString();
            Clear();
            return Value;

            }
        }


    // The Generic Lexer Class
    public abstract class Lexer {
        public abstract byte[] CharacterMappings { get; }
        public abstract short[,] CompressedTransitions { get; }
        public Action[] Actions;
        public int StateInt;
        public int NextState = -1;

        /// <summary>
        /// Get the next token calling all token actions while the
        /// stream is read.
        /// </summary>
        /// <param name="StartState">The initial state</param>
        /// <returns>The value of the token recognized.</returns>
        public virtual int GetTokenInt(int StartState) {
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

        public delegate void Action(int c);

        public abstract void Init();

        protected LexReader Reader;
        public Lexer (LexReader Reader) : this () {
            this.Reader = Reader;
            }

        public Lexer() {
            Init();
            }

        /// <summary>
        /// Reset method. Is called at the start of each
        /// call to GetTokenInt.
        /// </summary>
        public virtual void Reset() {
            }

        }

    }