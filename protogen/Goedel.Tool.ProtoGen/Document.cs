// Script Syntax Version:  1.0

//  © 2015-2021 by Threshold Secrets LLC.
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
namespace Goedel.Tool.ProtoGen;
public partial class Generate : global::Goedel.Registry.Script {

	 public bool MakeTop = false;
	 string StartP , EndP ;
	 string StartDD , EndDD ;
	 string StartParamList , EndParamList ;
	 public string StartParam , MidParam , EndParam, EndParamBlock;
	 string StartSection1 , MidSection1 , EndSection1;
	 string StartSection2 , MidSection2 , EndSection2;
	 string StartSection3 , MidSection3 , EndSection3;
	 string StartSection4 , MidSection4 , EndSection4;
	 string CurrentPrefix;
	 string StartTransaction, MidTransaction, EndTransaction;
	
	
	 string [,] Sections; 
	 bool AddComments = true;
	
	/// <summary>	
	/// GenerateRFC2XML
	/// </summary>
	/// <param name="ProtoStruct"></param>
	public void GenerateRFC2XML (ProtoStruct ProtoStruct) {
		 ProtoStruct.Complete ();
		  StartP = "<t>"; EndP = "</t>";
		  StartDD = "<t>"; EndDD = "</t>";
		  StartParamList = "<t> <list style=\"hanging\" hangIndent=\"6\">";
		  EndParamList = "</list></t>";
		  StartParam = "<t hangText=\""; MidParam = " : "; EndParam = "\"><vspace />";EndParamBlock = "";
		  StartSection1 = "<section title=\""; MidSection1 = "\">"; EndSection1 = "</section>";
		  StartSection2 = "<section title=\""; MidSection2 = "\">"; EndSection2 = "</section>";
		  StartSection3 = "<section title=\""; MidSection3 = "\">"; EndSection3 = "</section>";
		GenerateBody (ProtoStruct);
		}
	
	/// <summary>	
	/// GenerateHTML
	/// </summary>
	/// <param name="ProtoStruct"></param>
	public void GenerateHTML (ProtoStruct ProtoStruct) {
		 ProtoStruct.Complete ();
		  StartP = "<p>"; EndP = "</p>";
		  StartDD = "<dd>"; EndDD = "</dd>";
		  StartParamList = "<dl>";
		  EndParamList = "</dl>";
		  StartParam = "<dt>"; MidParam = " :</dt><dd>"; EndParam = "<p>"; EndParamBlock = "</dd>";
		  StartSection1 = "<h1>"; MidSection1 = "</h1>"; EndSection1 = "";
		  StartSection2 = "<h2>"; MidSection2 = "</h2>"; EndSection2 = "";
		  StartSection3 = "<h3>"; MidSection3 = "</h3>"; EndSection3 = "";
		  StartTransaction = "<ul><li>"; MidTransaction = "</li><li>"; EndTransaction="</li></ul>";
		_Output.Write ("<!-- <!DOCTYPE html>\n{0}", _Indent);
		_Output.Write ("<html>\n{0}", _Indent);
		_Output.Write ("<head>\n{0}", _Indent);
		_Output.Write ("<title></title>\n{0}", _Indent);
		_Output.Write ("</head>\n{0}", _Indent);
		_Output.Write ("<body> -->\n{0}", _Indent);
		GenerateBody (ProtoStruct);
		_Output.Write ("<!-- </body>\n{0}", _Indent);
		_Output.Write ("</html> -->\n{0}", _Indent);
		}
	
	/// <summary>	
	/// GenerateMD
	/// </summary>
	/// <param name="ProtoStruct"></param>
	public void GenerateMD (ProtoStruct ProtoStruct) {
		 ProtoStruct.Complete ();
		  StartP = ""; EndP = "\n";
		  StartDD = "<dd>"; EndDD = "";
		  StartParamList = "<dl>\n";
		  EndParamList = "</dl>\n";
		//  StartParam = "<dt>"; MidParam = ": \n<dd>"; EndParam = "\n"; EndParamBlock = "";
		  StartParam = "<dt>"; MidParam = ": "; EndParam = "\n"; EndParamBlock = "";
		  StartSection1 = "#"; MidSection1 = "\n"; EndSection1 = "";
		  StartSection2 = "##"; MidSection2 = "\n"; EndSection2 = "";
		  StartSection3 = "###"; MidSection3 = "\n"; EndSection3 = "";
		  StartSection4 = "####"; MidSection4 = "\n"; EndSection4 = "";
		  StartTransaction = "<ul>\n<li>"; MidTransaction = "\n<li>"; EndTransaction="\n</ul>";
		 Sections = new string [,] {
		    {StartSection1, MidSection1, EndSection1},
		    {StartSection2, MidSection2, EndSection2},
		    {StartSection3, MidSection3, EndSection3},
		    {StartSection4, MidSection4, EndSection4}};
		 AddComments = false;
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Entry in ProtoStruct.Top) {
			 Entry.Normalize ();
			}
		foreach  (var Entry in ProtoStruct.Top) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Protocol: {
				  Protocol Protocol = (Protocol) Entry; 
				foreach  (var Item in Protocol.Entries) {
					switch (Item._Tag ()) {
						case ProtoStructType.Section: {
						  Section Section = (Section) Item; 
						
						SectionHeading (Section);
						break; }
						case ProtoStructType.Service: {
						  Service Service = (Service) Item; 
						
						MakeService (Service);
						break; }
						case ProtoStructType.Message: {
						  Message Message = (Message) Item; 
						
						MakeMessage (Message);
						break; }
						case ProtoStructType.Structure: {
						  Structure Structure = (Structure) Item; 
						
						MakeStructure (Structure);
						break; }
						case ProtoStructType.Transaction: {
						  Transaction Transaction = (Transaction) Item; 
						
						MakeTransaction (Transaction);
						break; }
						case ProtoStructType.Success: {
						  Success Success = (Success) Item; 
						
						MakeSuccess (Success);
						break; }
						case ProtoStructType.Warning: {
						  Warning Warning = (Warning) Item; 
						
						MakeWarning (Warning);
						break; }
						case ProtoStructType.Error: {
						  Error Error = (Error) Item; 
						
						MakeError (Error);
					break; }
						}
					}
			break; }
				}
			}
		}
	
	/// <summary>	
	/// SectionHeading
	/// </summary>
	/// <param name="Section"></param>
	public void SectionHeading (Section Section) {
		 var Level = Section.Level;
		 StartSection (Level, Section.Title);
		_Output.Write ("\n{0}", _Indent);
		DescriptionList (Section.Entries);
		EndSection (Level);
		}
	
	/// <summary>	
	/// StartSection
	/// </summary>
	/// <param name="Level"></param>
	/// <param name="Title"></param>
	public void StartSection (int Level, string Title) {
		StartSection (Level);
		_Output.Write ("{1}", _Indent, Title);
		MidSection (Level);
		}
	
	/// <summary>	
	/// StartSection
	/// </summary>
	/// <param name="Level"></param>
	public void StartSection (int Level) {
		_Output.Write ("{1}", _Indent, Sections[Level-1,0]);
		}
	
	/// <summary>	
	/// MidSection
	/// </summary>
	/// <param name="Level"></param>
	public void MidSection (int Level) {
		_Output.Write ("{1}", _Indent, Sections[Level-1,1]);
		}
	
	/// <summary>	
	/// EndSection
	/// </summary>
	/// <param name="Level"></param>
	public void EndSection (int Level) {
		_Output.Write ("{1}", _Indent, Sections[Level-1,2]);
		}
	
	/// <summary>	
	/// MakeService
	/// </summary>
	/// <param name="Service"></param>
	public void MakeService (Service Service) {
		_Output.Write ("{1}SRV Prefix{2}{3}{4}\n{0}", _Indent, StartParam, MidParam, Service.Discovery, EndParam);
		_Output.Write ("{1}HTTP Well Known Service Prefix{2}/.well-known/{3}{4}\n{0}", _Indent, StartParam, MidParam, Service.WellKnown, EndParam);
		_Output.Write ("{1}\n{0}", _Indent, EndParamBlock);
		_Output.Write ("\n{0}", _Indent);
		DescriptionList (Service.Entries);
		}
	
	/// <summary>	
	/// MakeMessage
	/// </summary>
	/// <param name="Message"></param>
	public void MakeMessage (Message Message) {
		StartSection (3);
		_Output.Write ("Message: {1}", _Indent, Message.Id);
		MidSection (3);
		_Output.Write ("\n{0}", _Indent);
		ParameterList (Message.Entries);
		EndSection (3);
		}
	
	/// <summary>	
	/// MakeStructure
	/// </summary>
	/// <param name="Structure"></param>
	public void MakeStructure (Structure Structure) {
		StartSection (3);
		_Output.Write ("Structure: {1}\n{0}", _Indent, Structure.Id);
		MidSection (3);
		ParameterList (Structure.Entries);
		EndSection (3);
		}
	
	/// <summary>	
	/// MakeTransaction
	/// </summary>
	/// <param name="Transaction"></param>
	public void MakeTransaction (Transaction Transaction) {
		StartSection (2);
		_Output.Write ("Transaction: {1}\n{0}", _Indent, Transaction.Id);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("{1}", _Indent, StartParamList);
		_Output.Write ("{1}Request{2} {3}{4}{5}", _Indent, StartParam, MidParam, Transaction.Request, EndParam, EndParamBlock);
		_Output.Write ("{1}Response{2} {3}{4}{5}", _Indent, StartParam, MidParam, Transaction.Response, EndParam, EndParamBlock);
		_Output.Write ("{1}", _Indent, EndParamList);
		MidSection (2);
		DescriptionList (Transaction.Entries);
		EndSection (2);
		}
	
	/// <summary>	
	/// MakeSuccess
	/// </summary>
	/// <param name="Success"></param>
	public void MakeSuccess (Success Success) {
		_Output.Write ("[{1}] {2}\n{0}", _Indent, Success.Code, Success.Id);
		DescriptionListDD (Success.Entries);
		}
	
	/// <summary>	
	/// MakeWarning
	/// </summary>
	/// <param name="Warning"></param>
	public void MakeWarning (Warning Warning) {
		_Output.Write ("[{1}] {2}\n{0}", _Indent, Warning.Code, Warning.Id);
		DescriptionListDD (Warning.Entries);
		}
	
	/// <summary>	
	/// MakeError
	/// </summary>
	/// <param name="Error"></param>
	public void MakeError (Error Error) {
		_Output.Write ("[{1}] {2}\n{0}", _Indent, Error.Code, Error.Id);
		DescriptionListDD (Error.Entries);
		}
	
	/// <summary>	
	/// Comment
	/// </summary>
	/// <param name="Text"></param>
	public void Comment (string Text) {
		if (  (AddComments) ) {
			_Output.Write ("<!-- {1} -->\n{0}", _Indent, Text);
			_Output.Write ("\n{0}", _Indent);
			}
		}
	
	/// <summary>	
	/// GenerateBody
	/// </summary>
	/// <param name="ProtoStruct"></param>
	public void GenerateBody (ProtoStruct ProtoStruct) {
		foreach  (var Item in ProtoStruct.Top) {
			 Item.Normalize ();
			switch (Item._Tag ()) {
				case ProtoStructType.Protocol: {
				  Protocol Protocol = (Protocol) Item; 
				_Output.Write ("{1}{2}{3}\n{0}", _Indent, StartSection1, Protocol.Id, MidSection1);
				
				 Comment ("Protocol description here");
				
				DescriptionList (Protocol.Entries);
				_Output.Write ("{1}{2} Transactions{3}\n{0}", _Indent, StartSection2, Protocol.Id, MidSection2);
				
				 Comment ("List of Transactions here as H3 entries");
				foreach  (var Entry in Protocol.Entries) {
					switch (Entry._Tag ()) {
						case ProtoStructType.Transaction: {
						  Transaction Transaction = (Transaction) Entry; 
						_Output.Write ("{1}Transaction: {2}{3}\n{0}", _Indent, StartSection3, Transaction.Id, MidSection3);
						_Output.Write ("{1}Request: {2}\n{0}", _Indent, StartTransaction, Transaction.Request);
						_Output.Write ("{1}Response: {2}\n{0}", _Indent, MidTransaction, Transaction.Response);
						_Output.Write ("{1}", _Indent, EndTransaction);
						
						DescriptionList (Transaction.Entries);
						_Output.Write ("{1}", _Indent, EndSection3);
					break; }
						}
					}
				_Output.Write ("{1}", _Indent, EndSection2);
				_Output.Write ("{1}{2} Messages{3}\n{0}", _Indent, StartSection2, Protocol.Id, MidSection2);
				
				 Comment ("List of Messages here as H3 entries");
				foreach  (var Entry in Protocol.Entries) {
					switch (Entry._Tag ()) {
						case ProtoStructType.Message: {
						  Message Message = (Message) Entry; 
						if (  (!Message.IsAbstract) ) {
							_Output.Write ("{1}Message: {2}{3}\n{0}", _Indent, StartSection3, Message.Id, MidSection3);
							DescriptionList (Message.Entries);
							ParameterList (Message.AllEntries);
							_Output.Write ("{1}", _Indent, EndSection3);
							}
					break; }
						}
					}
				_Output.Write ("{1}", _Indent, EndSection2);
				_Output.Write ("{1}{2} Structures{3}\n{0}", _Indent, StartSection2, Protocol.Id, MidSection2);
				
				 Comment ("List of Structures here as H3 entries");
				foreach  (var Entry in Protocol.Entries) {
					switch (Entry._Tag ()) {
						case ProtoStructType.Structure: {
						  Structure Structure = (Structure) Entry; 
						_Output.Write ("{1}Structure: {2}{3}\n{0}", _Indent, StartSection3, Structure.Id, MidSection3);
						
						DescriptionList (Structure.Entries);
						
						ParameterList (Structure.AllEntries);
						_Output.Write ("{1}", _Indent, EndSection3);
					break; }
						}
					}
				_Output.Write ("{1}", _Indent, EndSection2);
				_Output.Write ("{1}", _Indent, EndSection1);
			break; }
				}
			}
		}
	
	/// <summary>	
	///  DescriptionListSkip
	/// </summary>
		 public void DescriptionListSkip  (List<_Choice> Entries) {
		 bool Skip = true;
		foreach  (_Choice Entry in Entries) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Description: {
				  Description Description = (Description) Entry; 
				if (  Skip ) {
					 Skip = false;
					} else {
					_Output.Write ("{1}", _Indent, StartP);
					}
				foreach  (string s in Description.Text1) {
					_Output.Write ("{1}\n{0}", _Indent, s);
					}
				_Output.Write ("{1}", _Indent, EndP);
			break; }
				}
			}
		if (  Skip ) {
			_Output.Write ("[TBS]{1}\n{0}", _Indent, EndP);
			}
		 }
	
	
	/// <summary>	
	///  DescriptionList
	/// </summary>
		 public void DescriptionList  (List<_Choice> Entries) {
		foreach  (var Entry in Entries) {
			Description (Entry);
			}
		 }
	
	
	/// <summary>	
	/// Description
	/// </summary>
	/// <param name="TEntry"></param>
	public void Description (_Choice TEntry) {
		switch (TEntry._Tag ()) {
			case ProtoStructType.Description: {
			  Description Description = (Description) TEntry; 
			_Output.Write ("{1}", _Indent, StartP);
			foreach  (string s in Description.Text1) {
				_Output.Write ("{1}\n{0}", _Indent, s);
				}
			_Output.Write ("{1}", _Indent, EndP);
		break; }
			}
		}
	
	/// <summary>	
	///  DescriptionListDD
	/// </summary>
		 public void DescriptionListDD  (List<_Choice> Entries) {
		foreach  (var Entry in Entries) {
			DescriptionDD (Entry);
			}
		 }
	
	
	/// <summary>	
	/// DescriptionDD
	/// </summary>
	/// <param name="TEntry"></param>
	public void DescriptionDD (_Choice TEntry) {
		switch (TEntry._Tag ()) {
			case ProtoStructType.Description: {
			  Description Description = (Description) TEntry; 
			_Output.Write ("{1}", _Indent, StartDD);
			foreach  (string s in Description.Text1) {
				_Output.Write ("{1}\n{0}", _Indent, s);
				}
			_Output.Write ("{1}", _Indent, EndDD);
		break; }
			}
		}
	
	/// <summary>	
	///  Indentify
	/// </summary>
		 public void Indentify (int indent) {
		for  (int i=0; i<indent; i++) {
			_Output.Write ("	", _Indent);
			}
		 }
	
	
	/// <summary>	
	/// TMessage
	/// </summary>
	/// <param name="TEntry"></param>
	public void TMessage (_Choice TEntry) {
		switch (TEntry._Tag ()) {
			case ProtoStructType.Request: {
			  Request Mess = (Request) TEntry; 
			_Output.Write ("{1}Request :{2} {3}\n{0}", _Indent, StartP, Mess.Ref, EndP);
			break; }
			case ProtoStructType.Response: {
			  Response Mess = (Response) TEntry; 
			_Output.Write ("{1}Response :{2}{3}\n{0}", _Indent, StartP, Mess.Ref, EndP);
		break; }
			}
		}
	
	/// <summary>	
	///  ParameterList
	/// </summary>
		 public void ParameterList  (List<_Choice> Entries) {
		 bool NullList = true;
		foreach  (_Choice Entry in Entries) {
			 List<_Choice> Options = null;
			 string Type = null; 
			 string Name = null;
			switch (Entry._Tag ()) {
				case ProtoStructType.Inherits: {
				  Inherits Inherits = (Inherits) Entry; 
				_Output.Write ("{1}", _Indent, StartParamList);
				_Output.Write ("{1}Inherits{2} {3}{4}{5}", _Indent, StartParam, MidParam, Inherits.Ref, EndParam, EndParamBlock);
				_Output.Write ("{1}\n{0}", _Indent, EndParamList);
				break; }
				case ProtoStructType.Description: {
				  Description Desc = (Description) Entry; 
				
				Description (Desc);
				break; }
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Boolean";
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Integer";
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Binary";
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Label";
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "Name";
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "String";
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "URI";
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = "DateTime";
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				
				 Name = Param.Id.ToString (); Options = Param.Options ; Type = Param.Type.ToString ();
			break; }
				}
			if (  Options != null ) {
				if (  NullList ) {
					_Output.Write ("{1}", _Indent, StartParamList);
					 NullList = false;
					}
				_Output.Write ("{1}{2}{3}{4}", _Indent, StartParam, Name, MidParam, Type);
				OptionList (Options);
				}
			}
		if (  !NullList ) {
			_Output.Write ("{1}", _Indent, EndParamList);
			} else {
			_Output.Write ("[No fields]\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		 }
	
	
	/// <summary>	
	///  OptionList
	/// </summary>
		 public void OptionList  (List<_Choice> Entries) {
		 bool PRequired = false, PMultiple = false;
		foreach  (_Choice Entry in Entries) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Required: { 
				
				 PRequired = true;
				break; }
				case ProtoStructType.Multiple: { 
				
				 PMultiple = true;
				break; }
				case ProtoStructType.Default: { 
			break; }
				}
			}
		_Output.Write (" ", _Indent);
		if (  PMultiple ) {
			if (  PRequired ) {
				_Output.Write ("[1..Many]", _Indent);
				} else {
				_Output.Write ("[0..Many]", _Indent);
				}
			} else {
			if (  PRequired ) {
				_Output.Write ("(Required)", _Indent);
				} else {
				_Output.Write ("(Optional)", _Indent);
				}
			}
		_Output.Write ("{1}", _Indent, EndParam);
		DescriptionListDD (Entries);
		_Output.Write ("{1}", _Indent, EndParamBlock);
		 }
	
	}
