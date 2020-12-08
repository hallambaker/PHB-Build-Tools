
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.FSR;


// Goedel.Document.Markdown
namespace Goedel.Document.Markdown{


	// Prototypes for the actions. These must be implemented in 
	// the plus class

	/*
	public partial class MarkDownLex {
        public virtual void Reset (int c) {
			}
        public virtual void Ignore (int c) {
			}
        public virtual void AddText (int c) {
			}
        public virtual void StartPre (int c) {
			}
        public virtual void AddPre (int c) {
			}
        public virtual void PreClose (int c) {
			}
        public virtual void CountWhite (int c) {
			}
        public virtual void AddSpace (int c) {
			}
        public virtual void WasHeading1 (int c) {
			}
        public virtual void WasHeading2 (int c) {
			}
        public virtual void AddHeading (int c) {
			}
        public virtual void DefinedTerm (int c) {
			}
        public virtual void DefinedData (int c) {
			}
        public virtual void AddNumbered (int c) {
			}
        public virtual void AddBullet (int c) {
			}
		}
	*/

	public partial class MarkDownLex : global::Goedel.FSR.Lexer {
       /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Reader">The input source.</param>
        public  MarkDownLex(LexReader Reader) : base (Reader) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Stream">The input source.</param>
        public  MarkDownLex(Stream Stream) : base(new LexReader(Stream)) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="TextReader">The input source.</param>
        public  MarkDownLex(TextReader TextReader) : base(new LexReader(TextReader)) {
            }

        /// <summary>
        /// Maps characters to character sets
        /// </summary>
        public override byte[] CharacterMappings  => Character_Mapping; 

        /// <summary>
        /// State transitions in response to character set
        /// </summary>
        public override short[,] CompressedTransitions  => Compressed_Transitions; 

        /// <summary>
        /// Get the next token from the stream
        /// </summary>
        /// <param name="StartState">The initial starting state</param>
        /// <returns>The token detected or -1 if an error occurred</returns>
        public Token GetToken(State StartState) => Tokens [GetTokenInt((int)StartState)];


        /// <summary>
        /// Get the next token from the stream
        /// </summary>
        /// <returns>The token detected or -1 if an error occurred</returns>
        public Token GetToken () => GetToken (0);

		/// <summary>State types</summary>
		public enum State {
			/// <summary>BlockStart</summary>
            BlockStart = 0,
			/// <summary>Comment1</summary>
            Comment1 = 1,
			/// <summary>Comment2</summary>
            Comment2 = 2,
			/// <summary>Pre1</summary>
            Pre1 = 3,
			/// <summary>Pre2</summary>
            Pre2 = 4,
			/// <summary>PreCR</summary>
            PreCR = 5,
			/// <summary>InPre</summary>
            InPre = 6,
			/// <summary>PreAdd</summary>
            PreAdd = 7,
			/// <summary>PreOut1</summary>
            PreOut1 = 8,
			/// <summary>PreOut2</summary>
            PreOut2 = 9,
			/// <summary>PreOut3</summary>
            PreOut3 = 10,
			/// <summary>PreOut4</summary>
            PreOut4 = 11,
			/// <summary>NotPre</summary>
            NotPre = 12,
			/// <summary>WhiteSpace</summary>
            WhiteSpace = 13,
			/// <summary>Text</summary>
            Text = 14,
			/// <summary>TextSpace</summary>
            TextSpace = 15,
			/// <summary>TextMoreSpace</summary>
            TextMoreSpace = 16,
			/// <summary>TextCR</summary>
            TextCR = 17,
			/// <summary>TextCRSpace</summary>
            TextCRSpace = 18,
			/// <summary>TextH1</summary>
            TextH1 = 19,
			/// <summary>TextH2</summary>
            TextH2 = 20,
			/// <summary>Heading</summary>
            Heading = 21,
			/// <summary>Defined</summary>
            Defined = 22,
			/// <summary>Defined2</summary>
            Defined2 = 23,
			/// <summary>Number</summary>
            Number = 24,
			/// <summary>Numbered</summary>
            Numbered = 25,
			/// <summary>Bullet</summary>
            Bullet = 26,
			/// <summary>End</summary>
            End = 27
			};

		/// <summary>Token Types</summary>
		public enum Token {
			/// <summary>Could not find a valid token.</summary>
			INVALID = -1,
			/// <summary>Empty</summary>
            Empty = 0,
			/// <summary>Block</summary>
            Block = 1
			};


		/// <summary>Mapping of characters to character groups</summary>
		static byte [] Character_Mapping   =  new byte [] {
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 , 2 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			1 , 0 , 0 , 3 , 0 , 0 , 0 , 0 , 0 , 0 , 4 , 0 , 0 , 5 , 6 , 7 , 
			8 , 8 , 8 , 8 , 8 , 8 , 8 , 8 , 8 , 8 , 9 , 0 , 0 , 10 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 11 , 0   };

		static short [,]  Compressed_Transitions  = new short [,]  {
			{14 , 13 , 0 , 21 , 26 , 14 , 14 , 1 , 24 , 22 , 14 , 3 },
			{14 , 14 , 14 , 14 , 14 , 14 , 14 , 2 , 14 , 14 , 14 , 14 },
			{14 , 14 , 0 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 4 },
			{14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 5 },
			{5 , 5 , 6 , 5 , 5 , 5 , 5 , 5 , 5 , 5 , 5 , 5 },
			{7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 9 },
			{7 , 7 , 8 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 },
			{7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 9 },
			{7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 10 },
			{7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 7 , 11 },
			{11 , 11 , 27 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 },
			{14 , 15 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 13 , 14 , 21 , 26 , 14 , 14 , 1 , 24 , 22 , 14 , 14 },
			{14 , 15 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 16 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 16 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 18 , 27 , -1 , -1 , 20 , 14 , 14 , 14 , -1 , 19 , 14 },
			{14 , 18 , 27 , 14 , -1 , 20 , 14 , 14 , -1 , -1 , 19 , 14 },
			{14 , 14 , 27 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 19 , 14 },
			{14 , 14 , 27 , 14 , 14 , 20 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 14 , 17 , 21 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 14 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 23 , 14 , 14 },
			{14 , 14 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 23 , 14 , 14 },
			{14 , 14 , 17 , 14 , 14 , 14 , 25 , 14 , 24 , 14 , 14 , 14 },
			{14 , 14 , 17 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 14 , 17 , 14 , 26 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 }
		};


		static Token [] Tokens = new Token [] {
			Token.Empty,
			Token.Empty,
			Token.Empty,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Empty,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block,
			Token.Block
			};

		/// <summary>Generated initialization method, is called automatically 
		/// the FSR to reset </summary>
        public override void Init () =>
            Actions = new ActionDelegate[] {
				Reset,
				Ignore,
				Ignore,
				AddText,
				AddText,
				AddText,
				StartPre,
				AddPre,
				PreClose,
				PreClose,
				PreClose,
				Ignore,
				Ignore,
				CountWhite,
				AddText,
				AddText,
				Ignore,
				AddSpace,
				Ignore,
				WasHeading1,
				WasHeading2,
				AddHeading,
				DefinedTerm,
				DefinedData,
				AddNumbered,
				AddNumbered,
				AddBullet,
				Ignore			};
		}
	}

