
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
	public partial class MarkDownParagraph {
        public virtual void Reset (char c) {
			}
        public virtual void AddText (char c) {
			}
        public virtual void GotEscape (char c) {
			}
        public virtual void AddeText (char c) {
			}
        public virtual void StartTag (char c) {
			}
        public virtual void AddTag (char c) {
			}
        public virtual void AddValue (char c) {
			}
        public virtual void AddCloseTag (char c) {
			}
		}
	*/

	public partial class MarkDownParagraph : global::Goedel.FSR.Lexer {
       /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Reader">The input source.</param>
        public  MarkDownParagraph(LexReader Reader) : base (Reader) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Stream">The input source.</param>
        public  MarkDownParagraph(Stream Stream) : base(new LexReader(Stream)) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="TextReader">The input source.</param>
        public  MarkDownParagraph(TextReader TextReader) : base(new LexReader(TextReader)) {
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

		/// <summary>State types</summary>
		public enum State {
			/// <summary>Start</summary>
            Start = 0,
			/// <summary>ReadText</summary>
            ReadText = 1,
			/// <summary>Escape</summary>
            Escape = 2,
			/// <summary>eText</summary>
            eText = 3,
			/// <summary>ElementStart</summary>
            ElementStart = 4,
			/// <summary>NewElementTag</summary>
            NewElementTag = 5,
			/// <summary>ElementTag</summary>
            ElementTag = 6,
			/// <summary>ElementWS</summary>
            ElementWS = 7,
			/// <summary>ElementWaitValue</summary>
            ElementWaitValue = 8,
			/// <summary>ElementValue</summary>
            ElementValue = 9,
			/// <summary>ElementWaitQuotedValue</summary>
            ElementWaitQuotedValue = 10,
			/// <summary>ElementQuotedValue</summary>
            ElementQuotedValue = 11,
			/// <summary>ElementEnd</summary>
            ElementEnd = 12,
			/// <summary>ElementEmpty</summary>
            ElementEmpty = 13,
			/// <summary>ElementEmptyEnd</summary>
            ElementEmptyEnd = 14,
			/// <summary>ElementCloseStart</summary>
            ElementCloseStart = 15,
			/// <summary>ElementClose</summary>
            ElementClose = 16,
			/// <summary>ElementEndClose</summary>
            ElementEndClose = 17
			};

		/// <summary>Token Types</summary>
		public enum Token {
			/// <summary>Could not find a valid token.</summary>
			INVALID = -1,
			/// <summary>Null</summary>
            Null = 0,
			/// <summary>Text</summary>
            Text = 1,
			/// <summary>Open</summary>
            Open = 2,
			/// <summary>Close</summary>
            Close = 3,
			/// <summary>Empty</summary>
            Empty = 4
			};


		/// <summary>Mapping of characters to character groups</summary>
		static byte [] Character_Mapping   =  new byte [] {
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 , 0 , 0 , 0 , 0 , 0 , 0 , 
			0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
			1 , 0 , 2 , 0 , 0 , 0 , 3 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 4 , 
			5 , 5 , 5 , 5 , 5 , 5 , 5 , 5 , 5 , 5 , 0 , 0 , 6 , 7 , 8 , 0 , 
			0 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 
			9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 0 , 0 , 0 , 0 , 0 , 
			0 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 
			9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 9 , 0 , 0 , 0 , 0 , 0   };

		static short [,]  Compressed_Transitions  = new short [,]  {
			{1 , 1 , 1 , 2 , 1 , 1 , 4 , 1 , 1 , 1 },
			{1 , 1 , 1 , 2 , 1 , 1 , 4 , 1 , 1 , 1 },
			{3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 },
			{1 , 1 , 1 , 2 , 1 , 1 , -1 , 1 , 1 , 1 },
			{1 , 1 , 1 , 1 , 15 , 1 , 3 , 1 , 1 , 5 },
			{1 , 7 , 1 , 1 , 13 , 6 , 1 , 8 , 12 , 6 },
			{1 , 7 , 1 , 1 , 13 , 6 , 1 , 8 , 12 , 6 },
			{1 , 7 , 1 , 1 , 13 , 1 , 1 , 8 , 12 , 5 },
			{9 , 8 , 10 , 9 , 13 , 9 , 9 , 9 , 12 , 9 },
			{9 , 7 , 9 , 9 , 13 , 9 , 9 , 9 , 12 , 9 },
			{11 , 11 , 7 , 11 , 11 , 11 , 11 , 11 , 11 , 11 },
			{11 , 11 , 7 , 11 , 11 , 11 , 11 , 11 , 11 , 11 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{1 , 1 , 1 , 2 , 1 , 1 , 4 , 1 , 14 , 1 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , 17 , 16 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , 17 , 16 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 }
		};


		static Token [] Tokens = new Token [] {
			Token.Null,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Text,
			Token.Open,
			Token.Text,
			Token.Empty,
			Token.Text,
			Token.Text,
			Token.Close
			};

		/// <summary>Generated initialization method, is called automatically 
		/// the FSR to reset </summary>
        public override void Init () {
            Actions = new Action[] {
				Reset,
				AddText,
				GotEscape,
				AddeText,
				Null,
				StartTag,
				AddTag,
				Null,
				Null,
				AddValue,
				Null,
				AddValue,
				ElementEnd,
				Null,
				ElementEnd,
				Null,
				AddCloseTag,
				ElementEnd
				};
			}
		}
	}

