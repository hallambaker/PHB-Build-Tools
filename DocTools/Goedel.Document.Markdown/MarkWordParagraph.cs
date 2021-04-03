
//  This file was automatically generated at 4/3/2021 1:04:53 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  fsrgen version 3.0.0.596
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2019
//  
//  Build Platform: Win32NT 10.0.18362.0
//  
//  
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
	public partial class MarkNewParagraph {
        public virtual void Reset (int c) {
			}
        public virtual void AddUpper (int c) {
			}
        public virtual void AddUpperSpace (int c) {
			}
        public virtual void AddText (int c) {
			}
        public virtual void GotEscape (int c) {
			}
        public virtual void AddeText (int c) {
			}
        public virtual void StartTag (int c) {
			}
        public virtual void AddTag (int c) {
			}
        public virtual void AddValue (int c) {
			}
        public virtual void AddCloseTag (int c) {
			}
		}
	*/

	public partial class MarkNewParagraph : global::Goedel.FSR.Lexer {
       /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Reader">The input source.</param>
        public  MarkNewParagraph(LexReader Reader) : base (Reader) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="Stream">The input source.</param>
        public  MarkNewParagraph(Stream Stream) : base(new LexReader(Stream)) {
            }

        /// <summary>
        /// Create and initialize a lexical analyzer.
        /// </summary>
        /// <param name="TextReader">The input source.</param>
        public  MarkNewParagraph(TextReader TextReader) : base(new LexReader(TextReader)) {
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
			/// <summary>Start</summary>
            Start = 0,
			/// <summary>UpperText</summary>
            UpperText = 1,
			/// <summary>UpperSpace</summary>
            UpperSpace = 2,
			/// <summary>AnyText</summary>
            AnyText = 3,
			/// <summary>AnySpace</summary>
            AnySpace = 4,
			/// <summary>Escape</summary>
            Escape = 5,
			/// <summary>eText</summary>
            eText = 6,
			/// <summary>ElementStart</summary>
            ElementStart = 7,
			/// <summary>NewElementTag</summary>
            NewElementTag = 8,
			/// <summary>ElementTag</summary>
            ElementTag = 9,
			/// <summary>ElementWS</summary>
            ElementWS = 10,
			/// <summary>ElementWaitValue</summary>
            ElementWaitValue = 11,
			/// <summary>ElementValue</summary>
            ElementValue = 12,
			/// <summary>ElementWaitQuotedValue</summary>
            ElementWaitQuotedValue = 13,
			/// <summary>ElementQuotedValue</summary>
            ElementQuotedValue = 14,
			/// <summary>ElementEnd</summary>
            ElementEnd = 15,
			/// <summary>ElementEmpty</summary>
            ElementEmpty = 16,
			/// <summary>ElementEmptyEnd</summary>
            ElementEmptyEnd = 17,
			/// <summary>ElementCloseStart</summary>
            ElementCloseStart = 18,
			/// <summary>ElementClose</summary>
            ElementClose = 19,
			/// <summary>ElementEndClose</summary>
            ElementEndClose = 20
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
			2 , 0 , 3 , 0 , 0 , 0 , 4 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 5 , 
			6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 0 , 0 , 7 , 8 , 9 , 0 , 
			0 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 
			10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 10 , 0 , 0 , 0 , 0 , 0 , 
			0 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 
			11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 11 , 0 , 0 , 0 , 0 , 0   };

		static short [,]  Compressed_Transitions  = new short [,]  {
			{3 , 3 , 3 , 3 , 5 , 3 , 3 , 7 , 3 , 3 , 1 , 3 },
			{3 , 3 , 2 , 3 , 5 , 3 , 3 , 7 , 3 , 3 , 1 , 3 },
			{4 , 4 , 4 , 4 , 5 , 4 , 3 , 7 , 4 , 4 , 1 , 3 },
			{4 , 4 , 4 , 4 , 5 , 4 , 3 , 7 , 4 , 4 , 4 , 3 },
			{4 , 4 , 4 , 4 , 5 , 4 , 3 , 7 , 4 , 4 , 1 , 3 },
			{6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 , 6 },
			{3 , 3 , 3 , 3 , 5 , 3 , 3 , -1 , 3 , 3 , 3 , 3 },
			{3 , 3 , 3 , 3 , 3 , 18 , 3 , 6 , 3 , 3 , 8 , 8 },
			{3 , 10 , 10 , 3 , 3 , 16 , 9 , 3 , 11 , 15 , 9 , 9 },
			{3 , 10 , 10 , 3 , 3 , 16 , 9 , 3 , 11 , 15 , 9 , 9 },
			{3 , 10 , 10 , 3 , 3 , 16 , 3 , 3 , 11 , 15 , 8 , 8 },
			{12 , 11 , 11 , 13 , 12 , 16 , 12 , 12 , 12 , 15 , 12 , 12 },
			{12 , 10 , 10 , 12 , 12 , 16 , 12 , 12 , 12 , 15 , 12 , 12 },
			{14 , 14 , 14 , 10 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{14 , 14 , 14 , 10 , 14 , 14 , 14 , 14 , 14 , 14 , 14 , 14 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{3 , 3 , 3 , 3 , 5 , 3 , 3 , 7 , 3 , 17 , 3 , 3 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , 20 , 19 , 19 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , 20 , 19 , 19 },
			{-2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 , -2 }
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
        public override void Init () =>
            Actions = new ActionDelegate[] {
				Reset,
				AddUpper,
				AddUpperSpace,
				AddText,
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
				ElementEnd			};
		}
	}

