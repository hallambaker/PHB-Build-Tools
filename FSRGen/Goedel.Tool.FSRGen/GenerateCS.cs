// Script Syntax Version:  1.0

//  Copyright ©  2017 by 
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.FSRGen {
	public partial class Generate : global::Goedel.Registry.Script {

		

		//
		// GenerateCS
		//
		public void GenerateCS (FSRSchema FSRSchema) {
			 FSRSchema.Complete ();
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using System.IO;\n{0}", _Indent);
			_Output.Write ("using System.Text;\n{0}", _Indent);
			_Output.Write ("using Goedel.FSR;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Item in FSRSchema.Top) {
				switch (Item._Tag ()) {
					case FSRSchemaType.FSR: {
					  FSR FSR = (FSR) Item; 
					
					 bool first = true;
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// {1}\n{0}", _Indent, FSR.Id);
					_Output.Write ("namespace {1}{{\n{0}", _Indent, FSR.Id);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	// Prototypes for the actions. These must be implemented in \n{0}", _Indent);
					_Output.Write ("	// the plus class\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	/*\n{0}", _Indent);
					_Output.Write ("	public partial class {1} {{\n{0}", _Indent, FSR.Prefix);
					foreach  (Action Action in FSR.Actions) {
						_Output.Write ("        public virtual void {1} (int c) {{\n{0}", _Indent, Action.Tag);
						_Output.Write ("			}}\n{0}", _Indent);
						}
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("	*/\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	public partial class {1} : global::Goedel.FSR.Lexer {{\n{0}", _Indent, FSR.Prefix);
					_Output.Write ("       /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Create and initialize a lexical analyzer.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"Reader\">The input source.</param>\n{0}", _Indent);
					_Output.Write ("        public  {1}(LexReader Reader) : base (Reader) {{\n{0}", _Indent, FSR.Prefix);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Create and initialize a lexical analyzer.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"Stream\">The input source.</param>\n{0}", _Indent);
					_Output.Write ("        public  {1}(Stream Stream) : base(new LexReader(Stream)) {{\n{0}", _Indent, FSR.Prefix);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Create and initialize a lexical analyzer.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"TextReader\">The input source.</param>\n{0}", _Indent);
					_Output.Write ("        public  {1}(TextReader TextReader) : base(new LexReader(TextReader)) {{\n{0}", _Indent, FSR.Prefix);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Maps characters to character sets\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        public override byte[] CharacterMappings  => Character_Mapping; \n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// State transitions in response to character set\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        public override short[,] CompressedTransitions  => Compressed_Transitions; \n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Get the next token from the stream\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"StartState\">The initial starting state</param>\n{0}", _Indent);
					_Output.Write ("        /// <returns>The token detected or -1 if an error occurred</returns>\n{0}", _Indent);
					_Output.Write ("        public Token GetToken(State StartState) => Tokens [GetTokenInt((int)StartState)];\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Get the next token from the stream\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <returns>The token detected or -1 if an error occurred</returns>\n{0}", _Indent);
					_Output.Write ("        public Token GetToken () => GetToken (0);\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>State types</summary>\n{0}", _Indent);
					_Output.Write ("		public enum State {{\n{0}", _Indent);
					foreach  (State State in FSR.States) {
						if (  (first)  ) {
							 first = false;
							} else {
							_Output.Write (",\n{0}", _Indent);
							}
						_Output.Write ("			/// <summary>{1}</summary>\n{0}", _Indent, State.Id);
						_Output.Write ("            {1} = {2}", _Indent, State.Id, State.Index);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>Token Types</summary>\n{0}", _Indent);
					_Output.Write ("		public enum Token {{\n{0}", _Indent);
					_Output.Write ("			/// <summary>Could not find a valid token.</summary>\n{0}", _Indent);
					_Output.Write ("			INVALID = -1", _Indent);
					foreach  (Token Token in FSR.Tokens) {
						_Output.Write (",\n{0}", _Indent);
						_Output.Write ("			/// <summary>{1}</summary>\n{0}", _Indent, Token.Tag);
						_Output.Write ("            {1} = {2}", _Indent, Token.Tag, Token.Index);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>Mapping of characters to character groups</summary>\n{0}", _Indent);
					_Output.Write ("		static byte [] Character_Mapping   =  new byte [] {{\n{0}", _Indent);
					_Output.Write ("			", _Indent);
					for  (int j = 0; j < FSR.MaxChar; j++) {
						if (  (j >0) ) {
							_Output.Write (", ", _Indent);
							if (  ((j%16) == 0) ) {
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("			", _Indent);
								}
							}
						_Output.Write ("{1} ", _Indent, FSR.MappingTable[j]);
						}
					_Output.Write ("  }};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		static short [,]  Compressed_Transitions  = new short [,]  {{\n{0}", _Indent);
					for  (int i = 0; i < FSR.States.Count; i++)  {
						if (  (i >0) ) {
							_Output.Write (",\n{0}", _Indent);
							}
						_Output.Write ("			{{", _Indent);
						for  (int j = 0; j < FSR.MaxMap; j++) {
							if (  (j >0) ) {
								_Output.Write (", ", _Indent);
								if (  ((j%16) == 0) ) {
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("     ", _Indent);
									}
								}
							_Output.Write ("{1} ", _Indent, FSR.CompressedTable[i,j]);
							}
						_Output.Write ("}}", _Indent);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		static Token [] Tokens = new Token [] {{\n{0}", _Indent);
					
					 bool comma = false;
					foreach  (var State in FSR.States) {
						if (  (comma) ) {
							_Output.Write (",\n{0}", _Indent);
							}
						 comma = true;
						_Output.Write ("			Token.{1}", _Indent, State.Token);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>Generated initialization method, is called automatically \n{0}", _Indent);
					_Output.Write ("		/// the FSR to reset </summary>\n{0}", _Indent);
					_Output.Write ("        public override void Init () =>\n{0}", _Indent);
					_Output.Write ("            Actions = new ActionDelegate[] {{\n{0}", _Indent);
					
					  comma = false;
					foreach  (var State in FSR.States) {
						if (  (comma) ) {
							_Output.Write (",\n{0}", _Indent);
							}
						 comma = true;
						_Output.Write ("				{1}", _Indent, State.Action);
						}
					_Output.Write ("			}};\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			}
		}
	}
