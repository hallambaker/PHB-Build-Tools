// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Copyright ©  2011 by Default Deny Security Inc.
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
// #xclass ProtoGen Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace ProtoGen {
	public partial class Generate : global::Goedel.Registry.Script {

		// #% public bool MakeTop = false; 
		 public bool MakeTop = false;
		// #%  public Generate (TextWriter Output) : base (Output) {} 
		  public Generate (TextWriter Output) : base (Output) {}
		// #!output GenerateHTML HTML html 
		// #!output GenerateXML2RFC XML2RFC xml 
		// #!output GenerateCS	CS cs 
		// #!output GenerateJava Java java 
		// #!output GenerateC C c 
		// #!output  
		// #% System.DateTime GenerateTime = System.DateTime.UtcNow; 
		 System.DateTime GenerateTime = System.DateTime.UtcNow;
		// #% string StartP , EndP ; 
		 string StartP , EndP ;
		// #% string StartParamList , EndParamList ; 
		 string StartParamList , EndParamList ;
		// #% string StartParam , MidParam , EndParam, EndParamBlock; 
		 string StartParam , MidParam , EndParam, EndParamBlock;
		// #% string StartSection1 , MidSection1 , EndSection1; 
		 string StartSection1 , MidSection1 , EndSection1;
		// #% string StartSection2 , MidSection2 , EndSection2; 
		 string StartSection2 , MidSection2 , EndSection2;
		// #% string StartSection3 , MidSection3 , EndSection3; 
		 string StartSection3 , MidSection3 , EndSection3;
		// #% string CurrentPrefix; 
		 string CurrentPrefix;
		// #% string Namespace; 
		 string Namespace;
		// #% string StartTransaction, MidTransaction, EndTransaction; 
		 string StartTransaction, MidTransaction, EndTransaction;
		// #% bool AddComments = true; 
		 bool AddComments = true;
		//  
		//  
		// #method GenerateRFC2XML ProtoStruct ProtoStruct 
		

		//
		// GenerateRFC2XML
		//
		public void GenerateRFC2XML (ProtoStruct ProtoStruct) {
			// #% ProtoStruct.Complete (); 
			 ProtoStruct.Complete ();
			// #%  StartP = "<t>"; EndP = "</t>"; 
			  StartP = "<t>"; EndP = "</t>";
			// #%  StartParamList = "<t> <list style=\"hanging\" hangIndent=\"6\">"; 
			  StartParamList = "<t> <list style=\"hanging\" hangIndent=\"6\">";
			// #%  EndParamList = "</list></t>"; 
			  EndParamList = "</list></t>";
			// #%  StartParam = "<t hangText=\""; MidParam = " : "; EndParam = "\"><vspace />";EndParamBlock = ""; 
			  StartParam = "<t hangText=\""; MidParam = " : "; EndParam = "\"><vspace />";EndParamBlock = "";
			// #%  StartSection1 = "<section title=\""; MidSection1 = "\">"; EndSection1 = "</section>"; 
			  StartSection1 = "<section title=\""; MidSection1 = "\">"; EndSection1 = "</section>";
			// #%  StartSection2 = "<section title=\""; MidSection2 = "\">"; EndSection2 = "</section>"; 
			  StartSection2 = "<section title=\""; MidSection2 = "\">"; EndSection2 = "</section>";
			// #%  StartSection3 = "<section title=\""; MidSection3 = "\">"; EndSection3 = "</section>"; 
			  StartSection3 = "<section title=\""; MidSection3 = "\">"; EndSection3 = "</section>";
			// #call GenerateBody ProtoStruct 
			GenerateBody (ProtoStruct);
			// #end method 
			}
		//  
		// #method2 StartSection int level string title 
		

		//
		// StartSection
		//
		public void StartSection (int level, string title) {
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method2 
			}
		//  
		// #method EndSection int level 
		

		//
		// EndSection
		//
		public void EndSection (int level) {
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method GenerateHTML ProtoStruct ProtoStruct 
		

		//
		// GenerateHTML
		//
		public void GenerateHTML (ProtoStruct ProtoStruct) {
			// #% ProtoStruct.Complete (); 
			 ProtoStruct.Complete ();
			// #%  StartP = "<p>"; EndP = "</p>"; 
			  StartP = "<p>"; EndP = "</p>";
			// #%  StartParamList = "<dl>"; 
			  StartParamList = "<dl>";
			// #%  EndParamList = "</dl>"; 
			  EndParamList = "</dl>";
			// #%  StartParam = "<dt>"; MidParam = " :</dt><dd>"; EndParam = "<p>"; EndParamBlock = "</dd>"; 
			  StartParam = "<dt>"; MidParam = " :</dt><dd>"; EndParam = "<p>"; EndParamBlock = "</dd>";
			// #%  StartSection1 = "<h1>"; MidSection1 = "</h1>"; EndSection1 = ""; 
			  StartSection1 = "<h1>"; MidSection1 = "</h1>"; EndSection1 = "";
			// #%  StartSection2 = "<h2>"; MidSection2 = "</h2>"; EndSection2 = ""; 
			  StartSection2 = "<h2>"; MidSection2 = "</h2>"; EndSection2 = "";
			// #%  StartSection3 = "<h3>"; MidSection3 = "</h3>"; EndSection3 = ""; 
			  StartSection3 = "<h3>"; MidSection3 = "</h3>"; EndSection3 = "";
			// #%  StartTransaction = "<ul><li>"; MidTransaction = "</li><li>"; EndTransaction="</li></ul>"; 
			  StartTransaction = "<ul><li>"; MidTransaction = "</li><li>"; EndTransaction="</li></ul>";
			// <!-- <!DOCTYPE html> 
			_Output.Write ("<!-- <!DOCTYPE html>\n{0}", _Indent);
			// <html> 
			_Output.Write ("<html>\n{0}", _Indent);
			// <head> 
			_Output.Write ("<head>\n{0}", _Indent);
			// <title></title> 
			_Output.Write ("<title></title>\n{0}", _Indent);
			// </head> 
			_Output.Write ("</head>\n{0}", _Indent);
			// <body> --> 
			_Output.Write ("<body> -->\n{0}", _Indent);
			// #call GenerateBody ProtoStruct 
			GenerateBody (ProtoStruct);
			// <!-- </body> 
			_Output.Write ("<!-- </body>\n{0}", _Indent);
			// </html> --> 
			_Output.Write ("</html> -->\n{0}", _Indent);
			// #end method 
			}
		//  
		//  
		// #method GenerateMD ProtoStruct ProtoStruct 
		

		//
		// GenerateMD
		//
		public void GenerateMD (ProtoStruct ProtoStruct) {
			// #% ProtoStruct.Complete (); 
			 ProtoStruct.Complete ();
			// #%  StartP = ""; EndP = "\n"; 
			  StartP = ""; EndP = "\n";
			// #%  StartParamList = ""; 
			  StartParamList = "";
			// #%  EndParamList = ""; 
			  EndParamList = "";
			// #%  StartParam = ":"; MidParam = " :\n::"; EndParam = "\n"; EndParamBlock = ""; 
			  StartParam = ":"; MidParam = " :\n::"; EndParam = "\n"; EndParamBlock = "";
			// #%  StartSection1 = "#"; MidSection1 = "\n"; EndSection1 = ""; 
			  StartSection1 = "#"; MidSection1 = "\n"; EndSection1 = "";
			// #%  StartSection2 = "##"; MidSection2 = "\n"; EndSection2 = ""; 
			  StartSection2 = "##"; MidSection2 = "\n"; EndSection2 = "";
			// #%  StartSection3 = "###"; MidSection3 = "\n"; EndSection3 = ""; 
			  StartSection3 = "###"; MidSection3 = "\n"; EndSection3 = "";
			// #%  StartTransaction = "*"; MidTransaction = "\n*"; EndTransaction="\n"; 
			  StartTransaction = "*"; MidTransaction = "\n*"; EndTransaction="\n";
			// #% AddComments = false; 
			 AddComments = false;
			// #call GenerateBody ProtoStruct 
			GenerateBody (ProtoStruct);
			// #end method 
			}
		//  
		// #method Comment string Text 
		

		//
		// Comment
		//
		public void Comment (string Text) {
			// #if (AddComments) 
			if (  (AddComments) ) {
				// <!-- #{Text} --> 
				_Output.Write ("<!-- {1} -->\n{0}", _Indent, Text);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end if 
				}
			// #end method 
			}
		//  
		// #method GenerateBody ProtoStruct ProtoStruct 
		

		//
		// GenerateBody
		//
		public void GenerateBody (ProtoStruct ProtoStruct) {
			// #foreach (var Item in ProtoStruct.Top) 
			foreach  (var Item in ProtoStruct.Top) {
				// #% Item.Normalize (); 
				 Item.Normalize ();
				// #switchcast ProtoStructType Item 
				switch (Item._Tag ()) {
					// #casecast Protocol Protocol 
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					// #{StartSection1}#{Protocol.Id}#{MidSection1} 
					_Output.Write ("{1}{2}{3}\n{0}", _Indent, StartSection1, Protocol.Id, MidSection1);
					// #% Comment ("Protocol description here"); 
					
					 Comment ("Protocol description here");
					// #call DescriptionList Protocol.Entries 
					
					DescriptionList (Protocol.Entries);
					// #{StartSection2}#{Protocol.Id} Transactions#{MidSection2} 
					_Output.Write ("{1}{2} Transactions{3}\n{0}", _Indent, StartSection2, Protocol.Id, MidSection2);
					// #% Comment ("List of Transactions here as H3 entries"); 
					
					 Comment ("List of Transactions here as H3 entries");
					// #foreach (var Entry in Protocol.Entries) 
					foreach  (var Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Transaction Transaction 
							case ProtoStructType.Transaction: {
							  Transaction Transaction = (Transaction) Entry; 
							// #{StartSection3}Transaction: #{Transaction.Id}#{MidSection3} 
							_Output.Write ("{1}Transaction: {2}{3}\n{0}", _Indent, StartSection3, Transaction.Id, MidSection3);
							// #{StartTransaction}Request: #{Transaction.Request} 
							_Output.Write ("{1}Request: {2}\n{0}", _Indent, StartTransaction, Transaction.Request);
							// #{MidTransaction}Response: #{Transaction.Response} 
							_Output.Write ("{1}Response: {2}\n{0}", _Indent, MidTransaction, Transaction.Response);
							// #{EndTransaction}#! 
							_Output.Write ("{1}", _Indent, EndTransaction);
							// #call DescriptionList Transaction.Entries 
							
							DescriptionList (Transaction.Entries);
							// #{EndSection3}#! 
							_Output.Write ("{1}", _Indent, EndSection3);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// #{EndSection2}#! 
					_Output.Write ("{1}", _Indent, EndSection2);
					// #{StartSection2}#{Protocol.Id} Messages#{MidSection2} 
					_Output.Write ("{1}{2} Messages{3}\n{0}", _Indent, StartSection2, Protocol.Id, MidSection2);
					// #% Comment ("List of Messages here as H3 entries"); 
					
					 Comment ("List of Messages here as H3 entries");
					// #foreach (var Entry in Protocol.Entries) 
					foreach  (var Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Message Message 
							case ProtoStructType.Message: {
							  Message Message = (Message) Entry; 
							// #if (!Message.IsAbstract) 
							if (  (!Message.IsAbstract) ) {
								// #{StartSection3}Message: #{Message.Id}#{MidSection3} 
								_Output.Write ("{1}Message: {2}{3}\n{0}", _Indent, StartSection3, Message.Id, MidSection3);
								// #call DescriptionList Message.Entries 
								DescriptionList (Message.Entries);
								// #call ParameterList Message.AllEntries 
								ParameterList (Message.AllEntries);
								// #{EndSection3}#! 
								_Output.Write ("{1}", _Indent, EndSection3);
								// #end if 
								}
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// #{EndSection2}#! 
					_Output.Write ("{1}", _Indent, EndSection2);
					// #{StartSection2}#{Protocol.Id} Structures#{MidSection2} 
					_Output.Write ("{1}{2} Structures{3}\n{0}", _Indent, StartSection2, Protocol.Id, MidSection2);
					// #% Comment ("List of Structures here as H3 entries"); 
					
					 Comment ("List of Structures here as H3 entries");
					// #foreach (var Entry in Protocol.Entries) 
					foreach  (var Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Structure Structure 
							case ProtoStructType.Structure: {
							  Structure Structure = (Structure) Entry; 
							// #{StartSection3}Structure: #{Structure.Id}#{MidSection3} 
							_Output.Write ("{1}Structure: {2}{3}\n{0}", _Indent, StartSection3, Structure.Id, MidSection3);
							// #call DescriptionList Structure.Entries 
							
							DescriptionList (Structure.Entries);
							// #call ParameterList Structure.AllEntries 
							
							ParameterList (Structure.AllEntries);
							// #{EndSection3}#! 
							_Output.Write ("{1}", _Indent, EndSection3);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// #{EndSection2}#! 
					_Output.Write ("{1}", _Indent, EndSection2);
					// #{EndSection1}#! 
					_Output.Write ("{1}", _Indent, EndSection1);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #block DescriptionListSkip 
		

		//
		//  DescriptionListSkip
		//

			// #% public void DescriptionListSkip  (List<_Choice> Entries) { 
			 public void DescriptionListSkip  (List<_Choice> Entries) {
			// #% bool Skip = true; 
			 bool Skip = true;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Description Description 
					case ProtoStructType.Description: {
					  Description Description = (Description) Entry; 
					// #if Skip 
					if (  Skip ) {
						// #% Skip = false; 
						 Skip = false;
						// #else 
						} else {
						// #{StartP}#! 
						_Output.Write ("{1}", _Indent, StartP);
						// #end if 
						}
					// #foreach (string s in Description.Text1) 
					foreach  (string s in Description.Text1) {
						// #{s} 
						_Output.Write ("{1}\n{0}", _Indent, s);
						// #end foreach 
						}
					// #{EndP}#! 
					_Output.Write ("{1}", _Indent, EndP);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #if Skip 
			if (  Skip ) {
				// [TBS]#{EndP} 
				_Output.Write ("[TBS]{1}\n{0}", _Indent, EndP);
				// #end if 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #block DescriptionList 
		

		//
		//  DescriptionList
		//

			// #% public void DescriptionList  (List<_Choice> Entries) { 
			 public void DescriptionList  (List<_Choice> Entries) {
			// #foreach (var Entry in Entries) 
			foreach  (var Entry in Entries) {
				// #call Description Entry 
				Description (Entry);
				// #end foreach 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #method Description _Choice TEntry 
		

		//
		// Description
		//
		public void Description (_Choice TEntry) {
			// #switchcast ProtoStructType TEntry 
			switch (TEntry._Tag ()) {
				// #casecast Description Description 
				case ProtoStructType.Description: {
				  Description Description = (Description) TEntry; 
				// #{StartP}#! 
				_Output.Write ("{1}", _Indent, StartP);
				// #foreach (string s in Description.Text1) 
				foreach  (string s in Description.Text1) {
					// #{s} 
					_Output.Write ("{1}\n{0}", _Indent, s);
					// #end foreach 
					}
				// #{EndP}#! 
				_Output.Write ("{1}", _Indent, EndP);
				// #end switchcast 
			break; }
				}
			// #end method 
			}
		//  
		//  
		//  
		//  
		// #block Indentify 
		

		//
		//  Indentify
		//

			// #% public void Indentify (int indent) { 
			 public void Indentify (int indent) {
			// #for (int i=0; i<indent; i++) 
			for  (int i=0; i<indent; i++) {
				// 	#! 
				_Output.Write ("	", _Indent);
				// #end for 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		// #method TMessage _Choice TEntry 
		

		//
		// TMessage
		//
		public void TMessage (_Choice TEntry) {
			// #switchcast ProtoStructType TEntry 
			switch (TEntry._Tag ()) {
				// #casecast Request Mess 
				case ProtoStructType.Request: {
				  Request Mess = (Request) TEntry; 
				// #{StartP}Request :#{Mess.Ref} #{EndP} 
				_Output.Write ("{1}Request :{2} {3}\n{0}", _Indent, StartP, Mess.Ref, EndP);
				// #casecast Response Mess 
				break; }
				case ProtoStructType.Response: {
				  Response Mess = (Response) TEntry; 
				// #{StartP}Response :#{Mess.Ref}#{EndP} 
				_Output.Write ("{1}Response :{2}{3}\n{0}", _Indent, StartP, Mess.Ref, EndP);
				// #end switchcast 
			break; }
				}
			// #end method 
			}
		//  
		//  
		// #block ParameterList 
		

		//
		//  ParameterList
		//

			// #% public void ParameterList  (List<_Choice> Entries) { 
			 public void ParameterList  (List<_Choice> Entries) {
			// #% bool NullList = true; 
			 bool NullList = true;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #% List<_Choice> Options = null; 
				 List<_Choice> Options = null;
				// #% string Type = null;  
				 string Type = null; 
				// #% string Name = null; 
				 string Name = null;
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Boolean Param 
					case ProtoStructType.Boolean: {
					  Boolean Param = (Boolean) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "Boolean"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Boolean";
					// #casecast Integer Param 
					break; }
					case ProtoStructType.Integer: {
					  Integer Param = (Integer) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "Integer"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Integer";
					// #casecast Binary Param 
					break; }
					case ProtoStructType.Binary: {
					  Binary Param = (Binary) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "Binary"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Binary";
					// #casecast Label Param 
					break; }
					case ProtoStructType.Label: {
					  Label Param = (Label) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "Label"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Label";
					// #casecast Name Param 
					break; }
					case ProtoStructType.Name: {
					  Name Param = (Name) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "Name"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Name";
					// #casecast String Param 
					break; }
					case ProtoStructType.String: {
					  String Param = (String) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "String"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "String";
					// #casecast URI Param 
					break; }
					case ProtoStructType.URI: {
					  URI Param = (URI) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "URI"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "URI";
					// #casecast DateTime Param 
					break; }
					case ProtoStructType.DateTime: {
					  DateTime Param = (DateTime) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = "DateTime"; 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = "DateTime";
					// #casecast Struct Param 
					break; }
					case ProtoStructType.Struct: {
					  Struct Param = (Struct) Entry; 
					// #% Name = Param.Id.ToString (); Options = Param.Options ; Type = Param.Type.ToString (); 
					
					 Name = Param.Id.ToString (); Options = Param.Options ; Type = Param.Type.ToString ();
					// #end switchcast 
				break; }
					}
				// #if Options != null 
				if (  Options != null ) {
					// #if NullList 
					if (  NullList ) {
						// #{StartParamList} 
						_Output.Write ("{1}\n{0}", _Indent, StartParamList);
						// #% NullList = false; 
						 NullList = false;
						// #end if 
						}
					// #{StartParam}#{Name}#{MidParam}#{Type}#! 
					_Output.Write ("{1}{2}{3}{4}", _Indent, StartParam, Name, MidParam, Type);
					// #call OptionList Options 
					OptionList (Options);
					// #end if 
					}
				// #end foreach 
				}
			// #if !NullList 
			if (  !NullList ) {
				// #{EndParamList}#! 
				_Output.Write ("{1}", _Indent, EndParamList);
				// #else 
				} else {
				// [None] 
				_Output.Write ("[None]\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end if 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		//  
		// #block OptionList 
		

		//
		//  OptionList
		//

			// #% public void OptionList  (List<_Choice> Entries) { 
			 public void OptionList  (List<_Choice> Entries) {
			// #% string min = "0", max = "1", def = ""; 
			 string min = "0", max = "1", def = "";
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Required Required 
					case ProtoStructType.Required: {
					  Required Required = (Required) Entry; 
					// #% min = "1"; 
					
					 min = "1";
					// #casecast Multiple Multiple 
					break; }
					case ProtoStructType.Multiple: {
					  Multiple Multiple = (Multiple) Entry; 
					// #% max = "Many"; 
					
					 max = "Many";
					// #casecast Default Default 
					break; }
					case ProtoStructType.Default: {
					  Default Default = (Default) Entry; 
					// #% def = "Default =" + Default.Value; 
					
					 def = "Default =" + Default.Value;
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  [#{min}..#{max}]  #{def} #{EndParam} 
			_Output.Write (" [{1}..{2}]  {3} {4}\n{0}", _Indent, min, max, def, EndParam);
			// #call DescriptionListSkip Entries 
			DescriptionListSkip (Entries);
			// #{EndParamBlock}#! 
			_Output.Write ("{1}", _Indent, EndParamBlock);
			// #% } 
			 }
			// #end block 
		
		// #end xclass 
		}
	}
