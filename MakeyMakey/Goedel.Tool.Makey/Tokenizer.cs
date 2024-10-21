
//  This file was automatically generated at 10/21/2024 1:37:39 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  fsrgen version 3.0.0.865
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2021
//  
//  Build Platform: Win32NT 10.0.22631.0
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.FSR;


// Goedel.Tool.Makey
namespace Goedel.Tool.Makey;


// Prototypes for the actions. These must be implemented in 
// the plus class

/*
public partial class Tokenizer {
    public virtual void Reset (int c) {
		}
    public virtual void AddCurrent (int c) {
		}
    public virtual void GotTag (int c) {
		}
    public virtual void GotStartTag (int c) {
		}
    public virtual void Ignore (int c) {
		}
    public virtual void GotItem (int c) {
		}
    public virtual void StartFinalize (int c) {
		}
    public virtual void EndFinalize (int c) {
		}
    public virtual void TagValueFinalize (int c) {
		}
    public virtual void Abort (int c) {
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
		/// <summary>Initial</summary>
        Initial = 0,
		/// <summary>Begin</summary>
        Begin = 1,
		/// <summary>GetTagValue0</summary>
        GetTagValue0 = 2,
		/// <summary>GetTagValue</summary>
        GetTagValue = 3,
		/// <summary>HaveOpen</summary>
        HaveOpen = 4,
		/// <summary>StartKey1</summary>
        StartKey1 = 5,
		/// <summary>StartParam1</summary>
        StartParam1 = 6,
		/// <summary>StartParam2</summary>
        StartParam2 = 7,
		/// <summary>StartItem1</summary>
        StartItem1 = 8,
		/// <summary>StartItem2</summary>
        StartItem2 = 9,
		/// <summary>StartItem3</summary>
        StartItem3 = 10,
		/// <summary>StartComplete</summary>
        StartComplete = 11,
		/// <summary>EndComplete</summary>
        EndComplete = 12,
		/// <summary>TagValueComplete</summary>
        TagValueComplete = 13,
		/// <summary>LineComplete</summary>
        LineComplete = 14,
		/// <summary>Fail</summary>
        Fail = 15
		};

	/// <summary>Token Types</summary>
	public enum Token {
		/// <summary>Could not find a valid token.</summary>
		INVALID = -1,
		/// <summary>Empty</summary>
        Empty = 0,
		/// <summary>Start</summary>
        Start = 1,
		/// <summary>End</summary>
        End = 2,
		/// <summary>Line</summary>
        Line = 3,
		/// <summary>TagValue</summary>
        TagValue = 4,
		/// <summary>Invalid</summary>
        Invalid = 5
		};


	/// <summary>Mapping of characters to character groups</summary>
	readonly static byte [] Character_Mapping   =  new byte [] {
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 , 2 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		1 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 3 , 4 , 0 , 0 , 5 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 6 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 };

	readonly static short [,]  Compressed_Transitions  = new short [,]  {
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


	readonly static Token [] Tokens = new Token [] {
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

	/// <summary>Generated initialization method, is called automatically 
	/// the FSR to reset </summary>
    public override void Init () =>
        Actions = new ActionDelegate[] {
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


