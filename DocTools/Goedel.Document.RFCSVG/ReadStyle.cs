
//  This file was automatically generated at 8/29/2023 3:15:43 PM
//   
//  Changes to this file may be overwritten without warning
//  
//  Generator:  fsrgen version 3.0.0.865
//      Goedel Script Version : 0.1   Generated 
//      Goedel Schema Version : 0.1   Generated
//  
//      Copyright : © 2015-2021
//  
//  Build Platform: Win32NT 10.0.22621.0
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.FSR;


// Goedel.Document.RFCSVG
namespace Goedel.Document.RFCSVG;


// Prototypes for the actions. These must be implemented in 
// the plus class

/*
public partial class ReadStyle {
    public virtual void Reset (int c) {
		}
    public virtual void AddLabel (int c) {
		}
    public virtual void Null (int c) {
		}
    public virtual void AddTag (int c) {
		}
    public virtual void AddValue (int c) {
		}
    public virtual void CompleteValue (int c) {
		}
    public virtual void Abort (int c) {
		}
	}
*/

public partial class ReadStyle : global::Goedel.FSR.Lexer {
    /// <summary>
    /// Create and initialize a lexical analyzer.
    /// </summary>
    /// <param name="Reader">The input source.</param>
    public  ReadStyle(LexReader Reader) : base (Reader) {
        }

    /// <summary>
    /// Create and initialize a lexical analyzer.
    /// </summary>
    /// <param name="Stream">The input source.</param>
    public  ReadStyle(Stream Stream) : base(new LexReader(Stream)) {
        }

    /// <summary>
    /// Create and initialize a lexical analyzer.
    /// </summary>
    /// <param name="TextReader">The input source.</param>
    public  ReadStyle(TextReader TextReader) : base(new LexReader(TextReader)) {
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
		/// <summary>Label</summary>
        Label = 1,
		/// <summary>LabelWS</summary>
        LabelWS = 2,
		/// <summary>StartStyle</summary>
        StartStyle = 3,
		/// <summary>Tag</summary>
        Tag = 4,
		/// <summary>TagWS</summary>
        TagWS = 5,
		/// <summary>EndTag</summary>
        EndTag = 6,
		/// <summary>Value</summary>
        Value = 7,
		/// <summary>EndValue</summary>
        EndValue = 8,
		/// <summary>EndStyle</summary>
        EndStyle = 9,
		/// <summary>Fail</summary>
        Fail = 10
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
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 , 1 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		1 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 2 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 3 , 4 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
		0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 5 , 0 , 6 , 0 , 0 };

	readonly static short [,]  Compressed_Transitions  = new short [,]  {
		{1 , 0 , 0 , 1 , 1 , 1 , 1 },
		{1 , 2 , 1 , 1 , 1 , 3 , 1 },
		{10 , 2 , 10 , 10 , 10 , 3 , 10 },
		{4 , 3 , 4 , 4 , 4 , 4 , 9 },
		{4 , 5 , 4 , 6 , 4 , 4 , 9 },
		{10 , 2 , 10 , 6 , 10 , 10 , 9 },
		{7 , 6 , 7 , 7 , 7 , 7 , 9 },
		{7 , 7 , 7 , 7 , 8 , 7 , 9 },
		{4 , 3 , 4 , 4 , 4 , 4 , 9 },
		{-2 , -2 , -2 , -2 , -2 , -2 , -2 },
		{-2 , -2 , -2 , -2 , -2 , -2 , -2 }
	};


	readonly static Token [] Tokens = new Token [] {
		Token.Empty,
		Token.Invalid,
		Token.Invalid,
		Token.Invalid,
		Token.Invalid,
		Token.Invalid,
		Token.Invalid,
		Token.Invalid,
		Token.Invalid,
		Token.End,
		Token.Invalid
		};

	/// <summary>Generated initialization method, is called automatically 
	/// the FSR to reset </summary>
    public override void Init () =>
        Actions = new ActionDelegate[] {
			Reset,
			AddLabel,
			Null,
			Null,
			AddTag,
			Null,
			Null,
			AddValue,
			CompleteValue,
			CompleteValue,
			Abort
		};
	}


