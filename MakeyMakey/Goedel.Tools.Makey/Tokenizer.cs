
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.FSR;


// Goedel.Tool.Makey
namespace Goedel.Tool.Makey{


	// Prototypes for the actions. These must be implemented in 
	// the plus class

	/*
	public partial class Tokenizer {
        public virtual void Reset (char c) {
			}
        public virtual void AddCurrent (char c) {
			}
        public virtual void GotTag (char c) {
			}
        public virtual void GotStartTag (char c) {
			}
        public virtual void Ignore (char c) {
			}
        public virtual void GotItem (char c) {
			}
        public virtual void StartFinalize (char c) {
			}
        public virtual void EndFinalize (char c) {
			}
        public virtual void TagValueFinalize (char c) {
			}
        public virtual void Abort (char c) {
			}
		}
	*/

	public partial class Tokenizer : global::Goedel.FSR.Lexer {
       /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Reader">The input source.</param>
        public  Tokenizer(LexReader Reader) : base (Reader) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Stream">The input source.</param>
        public  Tokenizer(Stream Stream) : base(new LexReader(Stream)) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="TextReader">The input source.</param>
        public  Tokenizer(TextReader TextReader) : base(new LexReader(TextReader)) {
            }

        /// <summary>
        /// Maps characters to character sets
        /// </summary>
        public override byte[] CharacterMappings { get { return Character_Mapping; }  }

        /// <summary>
        /// State transitions in response to character set
        /// </summary>
        public override short[,] CompressedTransitions { get { return Compressed_Transitions; } }

        /// <summary>
        /// Get the next token from the stream
        /// </summary>
        /// <param name="StartState">The initial starting state</param>
        /// <returns>The token detected or -1 if an error occurred</returns>
        public Token GetToken(State StartState) {
            return Tokens [GetTokenInt((int)StartState)];
            }

        /// <summary>
        /// Get the next token from the stream
        /// </summary>
        /// <returns>The token detected or -1 if an error occurred</returns>
        public Token GetToken () {
            return GetToken (0);
            }

		public enum State {
            Initial = 0,
            Begin = 1,
            GetTagValue0 = 2,
            GetTagValue = 3,
            HaveOpen = 4,
            StartKey1 = 5,
            StartParam1 = 6,
            StartParam2 = 7,
            StartItem1 = 8,
            StartItem2 = 9,
            StartItem3 = 10,
            StartComplete = 11,
            EndComplete = 12,
            TagValueComplete = 13,
            LineComplete = 14,
            Fail = 15
			};

		public enum Token {
			INVALID = -1,
            Empty = 0,
            Start = 1,
            End = 2,
            Line = 3,
            TagValue = 4,
            Invalid = 5
			};



		//#define Goedel.Tool.Makey_Action__Count  10
		//#define Goedel.Tool.Makey_Token__Count  6
		//#define Goedel.Tool.Makey_State__Count  16


		static byte [] Character_Mapping   =  new byte [] {
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 , 2 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			1 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 3 , 4 , 0 , 0 , 5 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 6 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0   };

		static short [,]  Compressed_Transitions  = new short [,]  {
			{1 , 0 , 0 , 1 , 1 , 1 , 1 },
			{1 , 1 , 12 , 4 , 1 , 1 , 2 },
			{3 , 3 , 13 , 3 , 3 , 3 , 3 },
			{3 , 3 , 13 , 3 , 3 , 3 , 3 },
			{5 , 5 , 15 , 5 , 6 , 5 , 5 },
			{5 , 5 , 5 , 5 , 6 , 5 , 5 },
			{-2 , 7 , -2 , -2 , -2 , -2 , 8 },
			{-2 , 7 , -2 , -2 , -2 , -2 , 8 },
			{9 , 9 , 11 , 9 , 9 , 10 , 9 },
			{9 , 9 , 11 , 9 , 9 , 10 , 9 },
			{9 , 9 , 11 , 9 , 9 , 9 , 9 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 }
		};


		static Token [] Tokens = new Token [] {
			Token.Empty,
			Token.End,
			Token.TagValue,
			Token.TagValue,
			Token.Invalid,
			Token.Start,
			Token.Start,
			Token.Start,
			Token.Start,
			Token.Start,
			Token.Start,
			Token.Start,
			Token.End,
			Token.TagValue,
			Token.Line,
			Token.Invalid
			};

        public override void Init () {
            Actions = new Action[] {
				Reset,
				AddCurrent,
				GotTag,
				AddCurrent,
				GotTag,
				AddCurrent,
				GotStartTag,
				Ignore,
				Ignore,
				AddCurrent,
				GotItem,
				StartFinalize,
				EndFinalize,
				TagValueFinalize,
				Ignore,
				Abort
				};
			}
		}
	}

