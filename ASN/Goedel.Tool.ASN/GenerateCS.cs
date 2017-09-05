// Script Syntax Version:  1.0

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
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ASN {
	/// <summary>A Goedel script.</summary>
	public partial class Generate : global::Goedel.Registry.Script {
		/// <summary>Default constructor.</summary>
		public Generate () : base () {
			}
		/// <summary>Constructor with output stream.</summary>
		/// <param name="Output">The output stream</param>
		public Generate (TextWriter Output) : base (Output) {
			}

		 string Namespace = "Goedel.ASN";
		

		//
		// GenerateCS
		//
		public void GenerateCS (ASN2 ASN2) {
			 ASN2.Complete ();
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using System.Text;\n{0}", _Indent);
			_Output.Write ("using Goedel.ASN;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("// This is the generated code Don't edit.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("// Generate OID declarations\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("namespace Goedel.ASN {{  // default namespace\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Toplevel in ASN2.Top) {
				switch (Toplevel._Tag ()) {
					case ASN2Type.Namespace: {
					  Namespace Cast = (Namespace) Toplevel; 
					
					 Namespace = Cast.Name.ToString();
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Cast.Name);
					_Output.Write ("\n{0}", _Indent);
					break; }
					case ASN2Type.ROOT: {
					  ROOT ROOT = (ROOT) Toplevel; 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					_Output.Write ("    /// {1} = ", _Indent, ROOT.Name);
					foreach  (Entry Entry in ROOT.Entries) {
						_Output.Write (" {1}({2}) ", _Indent, Entry.Name, Entry.Value);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("	public partial class Constants {{\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// {1} as integer sequence\n{0}", _Indent, ROOT.Name);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		public readonly static int [] OID__{1} = new int [] {{ ", _Indent, ROOT.Name);
					
					 MakeConst(ROOT.Binary) ;	
					_Output.Write ("}};\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// {1} as string\n{0}", _Indent, ROOT.Name);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		public const string OIDS__{1} = \"", _Indent, ROOT.Name);
					
					 MakeString(ROOT.Binary) ;	
					_Output.Write ("\";\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					
					 MakeChildren (ROOT);
					_Output.Write ("		}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("	}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("// Generate Classes\n{0}", _Indent);
			_Output.Write ("namespace Goedel.ASN {{  // default namespace\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Toplevel in ASN2.Top) {
				switch (Toplevel._Tag ()) {
					case ASN2Type.Namespace: {
					  Namespace Cast = (Namespace) Toplevel; 
					
					 Namespace = Cast.Name.ToString();
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Cast.Name);
					break; }
					case ASN2Type.Class: {
					  Class Class = (Class) Toplevel; 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					_Output.Write ("	/// {1} \n{0}", _Indent, Class.Name);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("	public partial class {1} : Goedel.ASN.Root {{\n{0}", _Indent, Class.Name);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Member Member in Class.Entries) {
						 EntryDeclaration (Member);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// Encode ASN.1 class members to specified buffer. \n{0}", _Indent);
					_Output.Write ("		///\n{0}", _Indent);
					_Output.Write ("		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order.\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		/// <param name=\"Buffer\">Output buffer</param>\n{0}", _Indent);
					_Output.Write ("        public override void Encode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					_Output.Write ("			int Position = Buffer.Encode__Sequence_Start ();\n{0}", _Indent);
					foreach  (Member Member in Class.ReversedEntries) {
						 Encode (Member);
						_Output.Write ("			Buffer.Debug (\"{1}\");\n{0}", _Indent, Member.Name);
						}
					_Output.Write ("			Buffer.Encode__Sequence_End (Position);\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			/*\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// Decode buffer to populate class members\n{0}", _Indent);
					_Output.Write ("		///\n{0}", _Indent);
					_Output.Write ("		/// This is done in the forward direction\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("        public override void Decode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					_Output.Write ("			int Position = Buffer.Decode__Sequence_Start ();\n{0}", _Indent);
					foreach  (Member Member in Class.Entries) {
						 Decode (Member);
						_Output.Write ("			Buffer.Debug (\"{1}\");\n{0}", _Indent, Member.Name);
						}
					_Output.Write ("			Buffer.Decode__Sequence_End (Position);\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("			*/\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					break; }
					case ASN2Type.Object: {
					  Object Object = (Object) Toplevel; 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					_Output.Write ("	/// {1} \n{0}", _Indent, Object.Name);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("	public partial class {1} : Goedel.ASN.Root {{\n{0}", _Indent, Object.Name);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// The OID value\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		public override int [] OID {{ \n{0}", _Indent);
					_Output.Write ("			get => Constants.OID__{1}; }}  \n{0}", _Indent, Object.OID);
					_Output.Write ("\n{0}", _Indent);
					
					 
					foreach  (Member Member in Object.Entries) {
						 EntryDeclaration (Member);
						}
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// Encode ASN.1 class members to specified buffer. \n{0}", _Indent);
					_Output.Write ("		///\n{0}", _Indent);
					_Output.Write ("		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order.\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		/// <param name=\"Buffer\">Output buffer</param>\n{0}", _Indent);
					_Output.Write ("        public override void Encode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					_Output.Write ("			int Position = Buffer.Encode__Sequence_Start ();\n{0}", _Indent);
					foreach  (Member Member in Object.ReversedEntries) {
						 Encode (Member);
						}
					_Output.Write ("			Buffer.Encode__Sequence_End (Position);\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			/*\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// Decode buffer to populate class members\n{0}", _Indent);
					_Output.Write ("		///\n{0}", _Indent);
					_Output.Write ("		/// This is done in the forward direction\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("        public override void Decode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					_Output.Write ("			int Position = Buffer.Decode__Sequence_Start ();\n{0}", _Indent);
					foreach  (Member Member in Object.Entries) {
						 Decode (Member);
						_Output.Write ("			Buffer.Debug (\"{1}\");\n{0}", _Indent, Member.Name);
						}
					_Output.Write ("			Buffer.Decode__Sequence_End (Position);\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("			*/\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					break; }
					case ASN2Type.SingularObject: {
					  SingularObject SingularObject = (SingularObject) Toplevel; 
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	// Singular, no sequence boundaries\n{0}", _Indent);
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					_Output.Write ("	/// {1} \n{0}", _Indent, SingularObject.Name);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("	public partial class {1} : Goedel.ASN.Root {{\n{0}", _Indent, SingularObject.Name);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// The OID value\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		public override int [] OID {{ \n{0}", _Indent);
					_Output.Write ("			get => Constants.OID__{1}; }} \n{0}", _Indent, SingularObject.OID);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Member Member in SingularObject.Entries) {
						 EntryDeclaration (Member);
						}
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// Encode ASN.1 class members to specified buffer. \n{0}", _Indent);
					_Output.Write ("		///\n{0}", _Indent);
					_Output.Write ("		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order.\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("		/// <param name=\"Buffer\">Output buffer</param>\n{0}", _Indent);
					_Output.Write ("        public override void Encode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					foreach  (Member Member in SingularObject.ReversedEntries) {
						 Encode (Member);
						}
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			/*\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("		/// Decode buffer to populate class members\n{0}", _Indent);
					_Output.Write ("		///\n{0}", _Indent);
					_Output.Write ("		/// This is done in the forward direction\n{0}", _Indent);
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					_Output.Write ("        public override void Decode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					_Output.Write ("			int Position = Buffer.Decode__Sequence_Start ();\n{0}", _Indent);
					foreach  (Member Member in SingularObject.Entries) {
						 Decode (Member);
						_Output.Write ("			Buffer.Debug (\"{1}\");\n{0}", _Indent, Member.Name);
						}
					_Output.Write ("			Buffer.Decode__Sequence_End (Position);\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("			*/\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("	}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// EntryDeclaration
		//
		public void EntryDeclaration (Member Member) {
			switch (Member.Spec._Tag ()) {
				case ASN2Type.Choice: {
				  Choice Cast = (Choice) Member.Spec; 
				foreach  (Member Entry in Cast.Entries) {
					 EntryDeclaration (Entry);
					}
				
				  break; } default : {
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				_Output.Write ("		/// ASN.1 member {1} \n{0}", _Indent, Member.Name);
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				_Output.Write ("		public ", _Indent);
				
				 TypeDeclaration (Member.Spec);
				_Output.Write ("{1} ;\n{0}", _Indent, Member.Name);
			break; }
				}
			}
		

		//
		// TypeDeclaration
		//
		public void TypeDeclaration (_Choice Type) {
			switch (Type._Tag ()) {
				case  ASN2Type.OIDRef: {
				_Output.Write ("int []  ", _Indent);
				break; }
				case  ASN2Type.Any : {
				_Output.Write ("byte []  ", _Indent);
				break; }
				case  ASN2Type.Integer : {
				_Output.Write ("int  ", _Indent);
				break; }
				case  ASN2Type.BigInteger : {
				_Output.Write ("byte []  ", _Indent);
				break; }
				case  ASN2Type.Boolean : {
				_Output.Write ("bool  ", _Indent);
				break; }
				case  ASN2Type.Time : {
				_Output.Write ("DateTime  ", _Indent);
				break; }
				case  ASN2Type.Bits : {
				_Output.Write ("byte []  ", _Indent);
				break; }
				case  ASN2Type.VBits : {
				_Output.Write ("byte []  ", _Indent);
				break; }
				case  ASN2Type.Octets : {
				_Output.Write ("byte []  ", _Indent);
				break; }
				case  ASN2Type.IA5String : {
				_Output.Write ("string ", _Indent);
				break; }
				case  ASN2Type.BMPString : {
				_Output.Write ("string ", _Indent);
				break; }
				case  ASN2Type.UTF8String : {
				_Output.Write ("string ", _Indent);
				break; }
				case  ASN2Type.PrintableString : {
				_Output.Write ("string ", _Indent);
				break; }
				case ASN2Type._Label: {
				  _Label Cast = (_Label) Type; 
				_Output.Write ("{1}.{2} ", _Indent, Namespace, Cast.Label);
				break; }
				case ASN2Type.List: {
				  List Cast = (List) Type; 
				_Output.Write ("List <", _Indent);
				
				 TypeDeclaration (Cast.Spec);
				_Output.Write ("> ", _Indent);
				break; }
				case ASN2Type.Set: {
				  Set Cast = (Set) Type; 
				_Output.Write ("List <", _Indent);
				
				 TypeDeclaration (Cast.Spec);
				_Output.Write ("> ", _Indent);
				break; }
				case  ASN2Type.Choice: {
				_Output.Write ("// Choice declare all the type parts\n{0}", _Indent);
			break; }
				}
			}
		

		//
		// Encode
		//
		public void Encode (Member Member) {
			  Encode (Member.Name.ToString(), Member.Default, Member.Spec, Member.Flags, Member.Code);
			}
		

		//
		// Decode
		//
		public void Decode (Member Member) {
			  Decode (Member.Name.ToString(), Member.Default, Member.Spec, Member.Flags, Member.Code);
			}
		

		//
		// 
		//

			  public void Encode (String Name, _Choice Spec, int Flags, int Code) {
					Encode (Name, null, Spec, Flags, Code);
					}
			  public void Encode (String Name, String Default, _Choice Spec, int Flags, int Code) {
			_Output.Write ("\n{0}", _Indent);
			 bool Call = false;
			switch (Spec._Tag ()) {
				case  ASN2Type.OIDRef: {
				_Output.Write ("			Buffer.Encode__OIDRef ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Any: {
				_Output.Write ("			Buffer.Encode__Any ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Integer: {
				if (  (Default != null) ) {
					_Output.Write ("			// Default is {1}\n{0}", _Indent, Default);
					_Output.Write ("			if ({1} != {2}) {{\n{0}", _Indent, Name, Default);
					_Output.Write ("				Buffer.Encode__Integer ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
					_Output.Write ("				}}\n{0}", _Indent);
					} else {
					_Output.Write ("			Buffer.Encode__Integer ", _Indent);
					 Call = true;
					}
				break; }
				case  ASN2Type.BigInteger: {
				_Output.Write ("			Buffer.Encode__BigInteger ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Boolean: {
				if (  (Default != null) ) {
					_Output.Write ("			// Default is {1}\n{0}", _Indent, Default);
					_Output.Write ("			if ({1} != {2}) {{\n{0}", _Indent, Name, Default);
					_Output.Write ("				Buffer.Encode__Boolean ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
					_Output.Write ("				}}\n{0}", _Indent);
					} else {
					_Output.Write ("			Buffer.Encode__Boolean ", _Indent);
					 Call = true;
					}
				break; }
				case  ASN2Type.Time : {
				_Output.Write ("			Buffer.Encode__Time ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Bits : {
				_Output.Write ("			Buffer.Encode__Bits ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.VBits : {
				_Output.Write ("			Buffer.Encode__VBits ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Octets : {
				_Output.Write ("			Buffer.Encode__Octets ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.IA5String : {
				_Output.Write ("			Buffer.Encode__IA5String ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.BMPString : {
				_Output.Write ("			Buffer.Encode__BMPString ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.UTF8String : {
				_Output.Write ("			Buffer.Encode__UTF8String ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.PrintableString : {
				_Output.Write ("			Buffer.Encode__PrintableString ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type._Label : {
				_Output.Write ("			Buffer.Encode__Object ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
				break; }
				case ASN2Type.List: {
				  List Cast = (List) Spec; 
				_Output.Write ("			if ({1} == null || {2}.Count == 0) {{\n{0}", _Indent, Name, Name);
				_Output.Write ("				Buffer.Encode__Object (null, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("			else {{\n{0}", _Indent);
				_Output.Write ("				int XPosition = Buffer.Encode__Sequence_Start();\n{0}", _Indent);
				_Output.Write ("				foreach (", _Indent);
				
				 TypeDeclaration (Cast.Spec);
				_Output.Write (" _Index in {1}) {{\n{0}", _Indent, Name);
				_Output.Write ("		", _Indent);
				
				 Encode ("_Index", Cast.Spec, 0, 0);
				_Output.Write ("					}}\n{0}", _Indent);
				_Output.Write ("				Buffer.Encode__Sequence_End(XPosition, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("			}}\n{0}", _Indent);
				break; }
				case ASN2Type.Set: {
				  Set Cast = (Set) Spec; 
				_Output.Write ("			if ({1} == null || {2}.Count == 0) {{\n{0}", _Indent, Name, Name);
				_Output.Write ("				Buffer.Encode__Object (null, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("			else {{\n{0}", _Indent);
				_Output.Write ("				int XPosition = Buffer.Encode__Set_Start();\n{0}", _Indent);
				_Output.Write ("				foreach (", _Indent);
				
				 TypeDeclaration (Cast.Spec);
				_Output.Write (" _Index in {1}) {{\n{0}", _Indent, Name);
				_Output.Write ("		", _Indent);
				
				 Encode ("_Index", Cast.Spec, 0, 0);
				_Output.Write ("					}}\n{0}", _Indent);
				_Output.Write ("				Buffer.Encode__Set_End(XPosition, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("			}}\n{0}", _Indent);
				break; }
				case ASN2Type.Choice: {
				  Choice Cast = (Choice) Spec; 
				_Output.Write ("	// Do Choice\n{0}", _Indent);
				foreach  (Member ChoiceEntry in Cast.Entries) {
					_Output.Write ("            //\n{0}", _Indent);
					// Encode (ChoiceEntry.Name.ToString(), ChoiceEntry.Spec, ChoiceEntry.Flags, ChoiceEntry.Code);
					 Encode (ChoiceEntry);
					}
			break; }
				}
			if (  Call ) {
				_Output.Write (" ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
				}
			 }
		
		

		//
		// 
		//

			  public void Decode (String Name, _Choice Spec, int Flags, int Code) {
					Decode (Name, null, Spec, Flags, Code);
					}
			  public void Decode (String Name, String Default, _Choice Spec, int Flags, int Code) {
			_Output.Write ("\n{0}", _Indent);
			 bool Call = false;
			switch (Spec._Tag ()) {
				case  ASN2Type.OIDRef : {
				_Output.Write ("			Buffer.Decode__OIDRef ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Any : {
				_Output.Write ("			Buffer.Decode__Any ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Integer : {
				if (  (Default != null) ) {
					_Output.Write ("			// Default is {1}\n{0}", _Indent, Default);
					_Output.Write ("			if ({1} != {2}) {{\n{0}", _Indent, Name, Default);
					_Output.Write ("				Buffer.Decode__Integer ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
					_Output.Write ("				}}\n{0}", _Indent);
					} else {
					_Output.Write ("			Buffer.Encode__Integer ", _Indent);
					 Call = true;
					}
				break; }
				case  ASN2Type.BigInteger : {
				_Output.Write ("			Buffer.Decode__BigInteger ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Boolean : {
				if (  (Default != null) ) {
					_Output.Write ("			// Default is {1}\n{0}", _Indent, Default);
					_Output.Write ("			if ({1} != {2}) {{\n{0}", _Indent, Name, Default);
					_Output.Write ("				Buffer.Decode__Boolean ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
					_Output.Write ("				}}\n{0}", _Indent);
					} else {
					_Output.Write ("			Buffer.Decode__Boolean ", _Indent);
					 Call = true;
					}
				break; }
				case  ASN2Type.Time : {
				_Output.Write ("			Buffer.EDecode__Time ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Bits : {
				_Output.Write ("			Buffer.Decode__Bits ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.VBits : {
				_Output.Write ("			Buffer.Decode__VBits ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.Octets : {
				_Output.Write ("			Buffer.Decode__Octets ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.IA5String : {
				_Output.Write ("			Buffer.Decode__IA5String ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.BMPString : {
				_Output.Write ("			Buffer.Decode__BMPString ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.UTF8String : {
				_Output.Write ("			Buffer.Decode__UTF8String ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type.PrintableString : {
				_Output.Write ("			Buffer.Decode__PrintableString ", _Indent);
				
				 Call = true;
				break; }
				case  ASN2Type._Label : {
				_Output.Write ("			Buffer.Decode__Object ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
				break; }
				case ASN2Type.List: {
				  List Cast = (List) Spec; 
				_Output.Write ("			if ({1} == null || {2}.Count == 0) {{\n{0}", _Indent, Name, Name);
				_Output.Write ("				Buffer.Decode__Object (null, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("			else {{\n{0}", _Indent);
				_Output.Write ("				int XPosition = Buffer.Decode__Sequence_Start();\n{0}", _Indent);
				_Output.Write ("				foreach (", _Indent);
				
				 TypeDeclaration (Cast.Spec);
				_Output.Write (" _Index in {1}) {{\n{0}", _Indent, Name);
				_Output.Write ("		", _Indent);
				
				 Encode ("_Index", Cast.Spec, 0, 0);
				_Output.Write ("					}}\n{0}", _Indent);
				_Output.Write ("				Buffer.Decode__Sequence_End(XPosition, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("			}}\n{0}", _Indent);
				break; }
				case ASN2Type.Set: {
				  Set Cast = (Set) Spec; 
				_Output.Write ("			if ({1} == null || {2}.Count == 0) {{\n{0}", _Indent, Name, Name);
				_Output.Write ("				Buffer.Decode__Object (null, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("			else {{\n{0}", _Indent);
				_Output.Write ("				int XPosition = Decode.Encode__Set_Start();\n{0}", _Indent);
				_Output.Write ("				foreach (", _Indent);
				
				 TypeDeclaration (Cast.Spec);
				_Output.Write (" _Index in {1}) {{\n{0}", _Indent, Name);
				_Output.Write ("		", _Indent);
				
				 Encode ("_Index", Cast.Spec, 0, 0);
				_Output.Write ("					}}\n{0}", _Indent);
				_Output.Write ("				Buffer.Decode__Set_End(XPosition, {1}, {2});\n{0}", _Indent, Flags, Code);
				_Output.Write ("			}}\n{0}", _Indent);
				break; }
				case ASN2Type.Choice: {
				  Choice Cast = (Choice) Spec; 
				_Output.Write ("	// Do Choice\n{0}", _Indent);
				foreach  (Member ChoiceEntry in Cast.Entries) {
					_Output.Write ("            //\n{0}", _Indent);
					// Encode (ChoiceEntry.Name.ToString(), ChoiceEntry.Spec, ChoiceEntry.Flags, ChoiceEntry.Code);
					 Encode (ChoiceEntry);
					}
			break; }
				}
			if (  Call ) {
				_Output.Write (" ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
				}
			 }
		
		

		//
		// MakeChildren
		//
		public void MakeChildren (_Choice Element) {
			foreach  (OID OID in Element.Children) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				_Output.Write ("		/// {1} = {2} ({3}) as integer sequence\n{0}", _Indent, OID.Name, OID.Root, OID.Value);
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				_Output.Write ("		public readonly static int [] OID__{1} = new int [] {{ ", _Indent, OID.Name);
				 MakeConst(OID.Binary) ;	
				_Output.Write ("}};\n{0}", _Indent);
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				_Output.Write ("		/// {1} = {2} ({3}) as string\n{0}", _Indent, OID.Name, OID.Root, OID.Value);
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				_Output.Write ("		public const string OIDS__{1} = \"", _Indent, OID.Name);
				 MakeString(OID.Binary) ;	
				_Output.Write ("\";\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				 MakeChildren (OID);
				}
			}
		

		//
		// MakeConst
		//
		public void MakeConst (int[] Array) {
			 bool Comma = false;
			foreach  (int Value in Array) {
				if (  Comma ) {
					_Output.Write (", ", _Indent);
					}
				 Comma = true;
				_Output.Write ("{1}", _Indent, Value);
				}
			}
		

		//
		// MakeString
		//
		public void MakeString (int[] Array) {
			 bool Comma = false;
			foreach  (int Value in Array) {
				if (  Comma ) {
					_Output.Write (".", _Indent);
					}
				 Comma = true;
				_Output.Write ("{1}", _Indent, Value);
				}
			}
		}
	}
