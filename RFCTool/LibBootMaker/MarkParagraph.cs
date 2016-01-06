
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


// Goedel.MarkLib
namespace Goedel.MarkLib {

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

	public partial class MarkDownParagraph {

		public delegate void Action (int c);

		public enum State {
            Start = 0,
            ReadText = 1,
            Escape = 2,
            eText = 3,
            ElementStart = 4,
            NewElementTag = 5,
            ElementTag = 6,
            ElementWS = 7,
            ElementWaitValue = 8,
            ElementValue = 9,
            ElementWaitQuotedValue = 10,
            ElementQuotedValue = 11,
            ElementEnd = 12,
            ElementEmpty = 13,
            ElementEmptyEnd = 14,
            ElementCloseStart = 15,
            ElementClose = 16,
            ElementEndClose = 17
			};

		public enum Token {
			INVALID = -1,
            Null = 0,
            Text = 1,
            Open = 2,
            Close = 3,
            Empty = 4
			};



		//#define Goedel.MarkLib_Action__Count  8
		//#define Goedel.MarkLib_Token__Count  5
		//#define Goedel.MarkLib_State__Count  18


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
			{1 , 1 , 1 , 1 , 15 , 1 , 1 , 1 , 1 , 5 },
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

        public Action[] Actions;
        public void Init () {
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

