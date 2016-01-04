
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


// Goedel.MarkLib
namespace Goedel.MarkLib {

	// Prototypes for the actions. These must be implemented in 
	// the plus class

	/*
	public partial class MarkDownLex {
        public virtual void Reset (char c) {
			}
        public virtual void Ignore (char c) {
			}
        public virtual void AddText (char c) {
			}
        public virtual void StartPre (char c) {
			}
        public virtual void AddPre (char c) {
			}
        public virtual void PreClose (char c) {
			}
        public virtual void CountWhite (char c) {
			}
        public virtual void AddSpace (char c) {
			}
        public virtual void WasHeading1 (char c) {
			}
        public virtual void WasHeading2 (char c) {
			}
        public virtual void AddHeading (char c) {
			}
        public virtual void DefinedTerm (char c) {
			}
        public virtual void DefinedData (char c) {
			}
        public virtual void AddNumbered (char c) {
			}
        public virtual void AddBullet (char c) {
			}
		}
	*/

	public partial class MarkDownLex {

		public delegate void Action (int c);

		public enum State {
            BlockStart = 0,
            Comment1 = 1,
            Comment2 = 2,
            Pre1 = 3,
            Pre2 = 4,
            PreCR = 5,
            InPre = 6,
            PreAdd = 7,
            PreOut1 = 8,
            PreOut2 = 9,
            PreOut3 = 10,
            PreOut4 = 11,
            NotPre = 12,
            WhiteSpace = 13,
            Text = 14,
            TextSpace = 15,
            TextMoreSpace = 16,
            TextCR = 17,
            TextCRSpace = 18,
            TextH1 = 19,
            TextH2 = 20,
            Heading = 21,
            Defined = 22,
            Defined2 = 23,
            Number = 24,
            Numbered = 25,
            Bullet = 26,
            End = 27
			};

		public enum Token {
			INVALID = -1,
            Empty = 0,
            Block = 1
			};



		//#define Goedel.MarkLib_Action__Count  15
		//#define Goedel.MarkLib_Token__Count  2
		//#define Goedel.MarkLib_State__Count  28


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

        public Action[] Actions;
        public void Init () {
            Actions = new Action[] {
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
				Ignore
				};
			}
		}
	}

