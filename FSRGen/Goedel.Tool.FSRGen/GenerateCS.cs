// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Unknown by Unknown
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
// #xclass Goedel.Tool.FSRGen Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.FSRGen {
	public partial class Generate : global::Goedel.Registry.Script {

		// #method GenerateCS FSRSchema FSRSchema 
		

		//
		// GenerateCS
		//
		public void GenerateCS (FSRSchema FSRSchema) {
			// #% FSRSchema.Complete (); 
			 FSRSchema.Complete ();
			//  
			_Output.Write ("\n{0}", _Indent);
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using System.IO; 
			_Output.Write ("using System.IO;\n{0}", _Indent);
			// using System.Text; 
			_Output.Write ("using System.Text;\n{0}", _Indent);
			// using Goedel.FSR; 
			_Output.Write ("using Goedel.FSR;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Item in FSRSchema.Top) 
			foreach  (_Choice Item in FSRSchema.Top) {
				// #switchcast FSRSchemaType Item 
				switch (Item._Tag ()) {
					// #casecast FSR FSR 
					case FSRSchemaType.FSR: {
					  FSR FSR = (FSR) Item; 
					// #% bool first = true; 
					
					 bool first = true;
					//  
					_Output.Write ("\n{0}", _Indent);
					// // #{FSR.Id} 
					_Output.Write ("// {1}\n{0}", _Indent, FSR.Id);
					// namespace #{FSR.Id}{ 
					_Output.Write ("namespace {1}{{\n{0}", _Indent, FSR.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	// Prototypes for the actions. These must be implemented in  
					_Output.Write ("	// Prototypes for the actions. These must be implemented in \n{0}", _Indent);
					// 	// the plus class 
					_Output.Write ("	// the plus class\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	/* 
					_Output.Write ("	/*\n{0}", _Indent);
					// 	public partial class #{FSR.Prefix} { 
					_Output.Write ("	public partial class {1} {{\n{0}", _Indent, FSR.Prefix);
					// #foreach (Action Action in FSR.Actions) 
					foreach  (Action Action in FSR.Actions) {
						//         public virtual void #{Action.Tag} (char c) { 
						_Output.Write ("        public virtual void {1} (char c) {{\n{0}", _Indent, Action.Tag);
						// 			} 
						_Output.Write ("			}}\n{0}", _Indent);
						// #end foreach 
						}
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					// 	*/ 
					_Output.Write ("	*/\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public partial class #{FSR.Prefix} : global::Goedel.FSR.Lexer { 
					_Output.Write ("	public partial class {1} : global::Goedel.FSR.Lexer {{\n{0}", _Indent, FSR.Prefix);
					//        /// <summary> 
					_Output.Write ("       /// <summary>\n{0}", _Indent);
					//         /// Create and initialize a lexical analyzer. 
					_Output.Write ("        /// Create and initialize a lexical analyzer.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         /// <param name="Reader">The input source.</param> 
					_Output.Write ("        /// <param name=\"Reader\">The input source.</param>\n{0}", _Indent);
					//         public  #{FSR.Prefix}(LexReader Reader) : base (Reader) { 
					_Output.Write ("        public  {1}(LexReader Reader) : base (Reader) {{\n{0}", _Indent, FSR.Prefix);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Create and initialize a lexical analyzer. 
					_Output.Write ("        /// Create and initialize a lexical analyzer.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         /// <param name="Stream">The input source.</param> 
					_Output.Write ("        /// <param name=\"Stream\">The input source.</param>\n{0}", _Indent);
					//         public  #{FSR.Prefix}(Stream Stream) : base(new LexReader(Stream)) { 
					_Output.Write ("        public  {1}(Stream Stream) : base(new LexReader(Stream)) {{\n{0}", _Indent, FSR.Prefix);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Create and initialize a lexical analyzer. 
					_Output.Write ("        /// Create and initialize a lexical analyzer.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         /// <param name="TextReader">The input source.</param> 
					_Output.Write ("        /// <param name=\"TextReader\">The input source.</param>\n{0}", _Indent);
					//         public  #{FSR.Prefix}(TextReader TextReader) : base(new LexReader(TextReader)) { 
					_Output.Write ("        public  {1}(TextReader TextReader) : base(new LexReader(TextReader)) {{\n{0}", _Indent, FSR.Prefix);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Maps characters to character sets 
					_Output.Write ("        /// Maps characters to character sets\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         public override byte[] CharacterMappings { get { return Character_Mapping; }  } 
					_Output.Write ("        public override byte[] CharacterMappings {{ get {{ return Character_Mapping; }}  }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// State transitions in response to character set 
					_Output.Write ("        /// State transitions in response to character set\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         public override short[,] CompressedTransitions { get { return Compressed_Transitions; } } 
					_Output.Write ("        public override short[,] CompressedTransitions {{ get {{ return Compressed_Transitions; }} }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Get the next token from the stream 
					_Output.Write ("        /// Get the next token from the stream\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         /// <param name="StartState">The initial starting state</param> 
					_Output.Write ("        /// <param name=\"StartState\">The initial starting state</param>\n{0}", _Indent);
					//         /// <returns>The token detected or -1 if an error occurred</returns> 
					_Output.Write ("        /// <returns>The token detected or -1 if an error occurred</returns>\n{0}", _Indent);
					//         public Token GetToken(State StartState) { 
					_Output.Write ("        public Token GetToken(State StartState) {{\n{0}", _Indent);
					//             return Tokens [GetTokenInt((int)StartState)]; 
					_Output.Write ("            return Tokens [GetTokenInt((int)StartState)];\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Get the next token from the stream 
					_Output.Write ("        /// Get the next token from the stream\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         /// <returns>The token detected or -1 if an error occurred</returns> 
					_Output.Write ("        /// <returns>The token detected or -1 if an error occurred</returns>\n{0}", _Indent);
					//         public Token GetToken () { 
					_Output.Write ("        public Token GetToken () {{\n{0}", _Indent);
					//             return GetToken (0); 
					_Output.Write ("            return GetToken (0);\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		public enum State { 
					_Output.Write ("		public enum State {{\n{0}", _Indent);
					// #foreach (State State in FSR.States) 
					foreach  (State State in FSR.States) {
						// #if (first)  
						if (  (first)  ) {
							// #% first = false; 
							 first = false;
							// #else  
							} else {
							// , 
							_Output.Write (",\n{0}", _Indent);
							// #end if 
							}
						//             #{State.Id} = #{State.Index}#! 
						_Output.Write ("            {1} = {2}", _Indent, State.Id, State.Index);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			}; 
					_Output.Write ("			}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		public enum Token { 
					_Output.Write ("		public enum Token {{\n{0}", _Indent);
					// 			INVALID = -1#! 
					_Output.Write ("			INVALID = -1", _Indent);
					// #foreach (Token Token in FSR.Tokens) 
					foreach  (Token Token in FSR.Tokens) {
						// , 
						_Output.Write (",\n{0}", _Indent);
						//             #{Token.Tag} = #{Token.Index}#! 
						_Output.Write ("            {1} = {2}", _Indent, Token.Tag, Token.Index);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			}; 
					_Output.Write ("			}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		//##define #{FSR.Id}_Action__Count  #{FSR.Actions.Count} 
					_Output.Write ("		//#define {1}_Action__Count  {2}\n{0}", _Indent, FSR.Id, FSR.Actions.Count);
					// 		//##define #{FSR.Id}_Token__Count  #{FSR.Tokens.Count} 
					_Output.Write ("		//#define {1}_Token__Count  {2}\n{0}", _Indent, FSR.Id, FSR.Tokens.Count);
					// 		//##define #{FSR.Id}_State__Count  #{FSR.States.Count} 
					_Output.Write ("		//#define {1}_State__Count  {2}\n{0}", _Indent, FSR.Id, FSR.States.Count);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		static byte [] Character_Mapping   =  new byte [] { 
					_Output.Write ("		static byte [] Character_Mapping   =  new byte [] {{\n{0}", _Indent);
					// 			#! 
					_Output.Write ("			", _Indent);
					// #for (int j = 0; j < FSR.MaxChar; j++) 
					for  (int j = 0; j < FSR.MaxChar; j++) {
						// #if (j >0) 
						if (  (j >0) ) {
							// , #! 
							_Output.Write (", ", _Indent);
							// #if ((j%16) == 0) 
							if (  ((j%16) == 0) ) {
								//  
								_Output.Write ("\n{0}", _Indent);
								// 			#! 
								_Output.Write ("			", _Indent);
								// #end if 
								}
							// #end if 
							}
						// #{FSR.MappingTable[j]} #! 
						_Output.Write ("{1} ", _Indent, FSR.MappingTable[j]);
						// #end for 
						}
					//   }; 
					_Output.Write ("  }};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		static short [,]  Compressed_Transitions  = new short [,]  { 
					_Output.Write ("		static short [,]  Compressed_Transitions  = new short [,]  {{\n{0}", _Indent);
					// #for (int i = 0; i < FSR.States.Count; i++)  
					for  (int i = 0; i < FSR.States.Count; i++)  {
						// #if (i >0) 
						if (  (i >0) ) {
							// , 
							_Output.Write (",\n{0}", _Indent);
							// #end if 
							}
						// 			{#! 
						_Output.Write ("			{{", _Indent);
						// #for (int j = 0; j < FSR.MaxMap; j++) 
						for  (int j = 0; j < FSR.MaxMap; j++) {
							// #if (j >0) 
							if (  (j >0) ) {
								// , #! 
								_Output.Write (", ", _Indent);
								// #if ((j%16) == 0) 
								if (  ((j%16) == 0) ) {
									//  
									_Output.Write ("\n{0}", _Indent);
									//      #! 
									_Output.Write ("     ", _Indent);
									// #end if 
									}
								// #end if 
								}
							// #{FSR.CompressedTable[i,j]} #! 
							_Output.Write ("{1} ", _Indent, FSR.CompressedTable[i,j]);
							// #end for 
							}
						// }#! 
						_Output.Write ("}}", _Indent);
						// #end for 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		}; 
					_Output.Write ("		}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		static Token [] Tokens = new Token [] { 
					_Output.Write ("		static Token [] Tokens = new Token [] {{\n{0}", _Indent);
					// #% bool comma = false; 
					
					 bool comma = false;
					// #foreach (var State in FSR.States) 
					foreach  (var State in FSR.States) {
						// #if (comma) 
						if (  (comma) ) {
							// , 
							_Output.Write (",\n{0}", _Indent);
							// #end if 
							}
						// #% comma = true; 
						 comma = true;
						// 			Token.#{State.Token}#! 
						_Output.Write ("			Token.{1}", _Indent, State.Token);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			}; 
					_Output.Write ("			}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         public override void Init () { 
					_Output.Write ("        public override void Init () {{\n{0}", _Indent);
					//             Actions = new Action[] { 
					_Output.Write ("            Actions = new Action[] {{\n{0}", _Indent);
					// #%  comma = false; 
					
					  comma = false;
					// #foreach (var State in FSR.States) 
					foreach  (var State in FSR.States) {
						// #if (comma) 
						if (  (comma) ) {
							// , 
							_Output.Write (",\n{0}", _Indent);
							// #end if 
							}
						// #% comma = true; 
						 comma = true;
						// 				#{State.Action}#! 
						_Output.Write ("				{1}", _Indent, State.Action);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 				}; 
					_Output.Write ("				}};\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		//  
		// #end xclass 
		}
	}
