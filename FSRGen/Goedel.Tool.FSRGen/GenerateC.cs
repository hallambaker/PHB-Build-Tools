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

		// #%  public Generate (TextWriter Output) : base (Output) {} 
		  public Generate (TextWriter Output) : base (Output) {}
		// #method GenerateH FSRSchema FSRSchema 
		

		//
		// GenerateH
		//
		public void GenerateH (FSRSchema FSRSchema) {
			// #% FSRSchema.Complete (); 
			 FSRSchema.Complete ();
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Item in FSRSchema.Top) 
			foreach  (_Choice Item in FSRSchema.Top) {
				// #switchcast FSRSchemaType Item 
				switch (Item._Tag ()) {
					// #casecast FSR FSR 
					case FSRSchemaType.FSR: {
					  FSR FSR = (FSR) Item; 
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (State State in FSR.States) 
					foreach  (State State in FSR.States) {
						// ##define #{FSR.Id}_State_#{State.Id}  #{State.Index} 
						_Output.Write ("#define {1}_State_{2}  {3}\n{0}", _Indent, FSR.Id, State.Id, State.Index);
						// #end foreach 
						}
					// ##define #{FSR.Id}_State__Count  #{FSR.States.Count} 
					_Output.Write ("#define {1}_State__Count  {2}\n{0}", _Indent, FSR.Id, FSR.States.Count);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Action Action in FSR.Actions) 
					foreach  (Action Action in FSR.Actions) {
						// ##define #{FSR.Id}_Action_#{Action.Tag}  #{Action.Index} 
						_Output.Write ("#define {1}_Action_{2}  {3}\n{0}", _Indent, FSR.Id, Action.Tag, Action.Index);
						// #end foreach 
						}
					// ##define #{FSR.Id}_Action__Count  #{FSR.Actions.Count} 
					_Output.Write ("#define {1}_Action__Count  {2}\n{0}", _Indent, FSR.Id, FSR.Actions.Count);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Token Token in FSR.Tokens) 
					foreach  (Token Token in FSR.Tokens) {
						// ##define #{FSR.Id}_Token_#{Token.Tag}  #{Token.Index} 
						_Output.Write ("#define {1}_Token_{2}  {3}\n{0}", _Indent, FSR.Id, Token.Tag, Token.Index);
						// #end foreach 
						}
					// ##define #{FSR.Id}_Token__Count  #{FSR.Tokens.Count} 
					_Output.Write ("#define {1}_Token__Count  {2}\n{0}", _Indent, FSR.Id, FSR.Tokens.Count);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// static #{FSR.StateType} #{FSR.Id}_Character_Mapping [#{FSR.MaxChar}]   =   { 
					_Output.Write ("static {1} {2}_Character_Mapping [{3}]   =   {{\n{0}", _Indent, FSR.StateType, FSR.Id, FSR.MaxChar);
					//      #! 
					_Output.Write ("     ", _Indent);
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
								//      #! 
								_Output.Write ("     ", _Indent);
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
					// static #{FSR.StateType} #{FSR.Id}_Compressed_Transitions [#{FSR.Id}_State__Count][#{FSR.MaxMap}]   =   { 
					_Output.Write ("static {1} {2}_Compressed_Transitions [{3}_State__Count][{4}]   =   {{\n{0}", _Indent, FSR.StateType, FSR.Id, FSR.Id, FSR.MaxMap);
					// #for (int i = 0; i < FSR.States.Count; i++)  
					for  (int i = 0; i < FSR.States.Count; i++)  {
						// #if (i >0) 
						if (  (i >0) ) {
							// , 
							_Output.Write (",\n{0}", _Indent);
							// #end if 
							}
						// 	{#! 
						_Output.Write ("	{{", _Indent);
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
					//   }; 
					_Output.Write ("  }};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// static int #{FSR.Id}_Actions [#{FSR.Id}_State__Count] = { 
					_Output.Write ("static int {1}_Actions [{2}_State__Count] = {{\n{0}", _Indent, FSR.Id, FSR.Id);
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
						// 	#{FSR.Id}_Action_#{State.Action}#! 
						_Output.Write ("	{1}_Action_{2}", _Indent, FSR.Id, State.Action);
						// #end foreach 
						}
					// 	}; 
					_Output.Write ("	}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// static int #{FSR.Id}_Tokens [#{FSR.Id}_State__Count] = { 
					_Output.Write ("static int {1}_Tokens [{2}_State__Count] = {{\n{0}", _Indent, FSR.Id, FSR.Id);
					// #% comma = false; 
					
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
						// 	#{FSR.Id}_Token_#{State.Token}#! 
						_Output.Write ("	{1}_Token_{2}", _Indent, FSR.Id, State.Token);
						// #end foreach 
						}
					// 	}; 
					_Output.Write ("	}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
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
		// #method GenerateRaw FSR FSR 
		

		//
		// GenerateRaw
		//
		public void GenerateRaw (FSR FSR) {
			// static #{FSR.StateType} #{FSR.Id}_Transitions [#{FSR.Id}_State__Count][#{FSR.MaxChar}]   =   { 
			_Output.Write ("static {1} {2}_Transitions [{3}_State__Count][{4}]   =   {{\n{0}", _Indent, FSR.StateType, FSR.Id, FSR.Id, FSR.MaxChar);
			// #for (int i = 0; i < FSR.States.Count; i++)  
			for  (int i = 0; i < FSR.States.Count; i++)  {
				// #if (i >0) 
				if (  (i >0) ) {
					// , 
					_Output.Write (",\n{0}", _Indent);
					// #end if 
					}
				// 	{#! 
				_Output.Write ("	{{", _Indent);
				// #for (int j = 0; j < 127; j++) 
				for  (int j = 0; j < 127; j++) {
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
					// #{FSR.TransitionTable[i,j]} #! 
					_Output.Write ("{1} ", _Indent, FSR.TransitionTable[i,j]);
					// #end for 
					}
				// }#! 
				_Output.Write ("}}", _Indent);
				// #end for 
				}
			// 	}; 
			_Output.Write ("	}};\n{0}", _Indent);
			// #end method 
			}
		//  
		// #end xclass 
		}
	}
