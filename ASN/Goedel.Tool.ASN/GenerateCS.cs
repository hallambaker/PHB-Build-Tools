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
// #pclass Goedel.Tool.ASN Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ASN {
	public partial class Generate : global::Goedel.Registry.Script {
		public Generate () : base () {
			}
		public Generate (TextWriter Output) : base (Output) {
			}

		// #% DateTime GenerateTime = DateTime.UtcNow; 
		 DateTime GenerateTime = DateTime.UtcNow;
		// #% string Namespace = "Goedel.ASN"; 
		 string Namespace = "Goedel.ASN";
		//  
		// #method GenerateCS ASN2 ASN2 
		

		//
		// GenerateCS
		//
		public void GenerateCS (ASN2 ASN2) {
			// #% ASN2.Complete (); 
			 ASN2.Complete ();
			//  
			_Output.Write ("\n{0}", _Indent);
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using System.Text; 
			_Output.Write ("using System.Text;\n{0}", _Indent);
			// using Goedel.ASN; 
			_Output.Write ("using Goedel.ASN;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// // This is the generated code Don't edit. 
			_Output.Write ("// This is the generated code Don't edit.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// // Generate OID declarations 
			_Output.Write ("// Generate OID declarations\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// namespace Goedel.ASN {  // default namespace 
			_Output.Write ("namespace Goedel.ASN {{  // default namespace\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Toplevel in ASN2.Top) 
			foreach  (_Choice Toplevel in ASN2.Top) {
				// #switchcast ASN2Type Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast Namespace Cast 
					case ASN2Type.Namespace: {
					  Namespace Cast = (Namespace) Toplevel; 
					// #% Namespace = Cast.Name.ToString(); 
					
					 Namespace = Cast.Name.ToString();
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					// namespace #{Cast.Name} { 
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Cast.Name);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #casecast ROOT ROOT 
					break; }
					case ASN2Type.ROOT: {
					  ROOT ROOT = (ROOT) Toplevel; 
					//     /// <summary> 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					//     /// #{ROOT.Name} = #! 
					_Output.Write ("    /// {1} = ", _Indent, ROOT.Name);
					// #foreach (Entry Entry in ROOT.Entries) 
					foreach  (Entry Entry in ROOT.Entries) {
						//  #{Entry.Name}(#{Entry.Value}) #! 
						_Output.Write (" {1}({2}) ", _Indent, Entry.Name, Entry.Value);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//     /// </summary> 
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					// 	public partial class Constants { 
					_Output.Write ("	public partial class Constants {{\n{0}", _Indent);
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// #{ROOT.Name} as integer sequence 
					_Output.Write ("		/// {1} as integer sequence\n{0}", _Indent, ROOT.Name);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		public readonly static int [] OID__#{ROOT.Name} = new int [] { #! 
					_Output.Write ("		public readonly static int [] OID__{1} = new int [] {{ ", _Indent, ROOT.Name);
					// #% MakeConst(ROOT.Binary) ;	 
					
					 MakeConst(ROOT.Binary) ;	
					// }; 
					_Output.Write ("}};\n{0}", _Indent);
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// #{ROOT.Name} as string 
					_Output.Write ("		/// {1} as string\n{0}", _Indent, ROOT.Name);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		public const string OIDS__#{ROOT.Name} = "#! 
					_Output.Write ("		public const string OIDS__{1} = \"", _Indent, ROOT.Name);
					// #% MakeString(ROOT.Binary) ;	 
					
					 MakeString(ROOT.Binary) ;	
					// "; 
					_Output.Write ("\";\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #% MakeChildren (ROOT); 
					
					 MakeChildren (ROOT);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 	} 
			_Output.Write ("	}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// // Generate Classes 
			_Output.Write ("// Generate Classes\n{0}", _Indent);
			// namespace Goedel.ASN {  // default namespace 
			_Output.Write ("namespace Goedel.ASN {{  // default namespace\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Toplevel in ASN2.Top) 
			foreach  (_Choice Toplevel in ASN2.Top) {
				// #switchcast ASN2Type Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast Namespace Cast 
					case ASN2Type.Namespace: {
					  Namespace Cast = (Namespace) Toplevel; 
					// #% Namespace = Cast.Name.ToString(); 
					
					 Namespace = Cast.Name.ToString();
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					// namespace #{Cast.Name} { 
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Cast.Name);
					// #casecast Class Class 
					break; }
					case ASN2Type.Class: {
					  Class Class = (Class) Toplevel; 
					//     /// <summary> 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					// 	/// #{Class.Name}  
					_Output.Write ("	/// {1} \n{0}", _Indent, Class.Name);
					//     /// </summary> 
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					// 	public partial class #{Class.Name} : Goedel.ASN.Root { 
					_Output.Write ("	public partial class {1} : Goedel.ASN.Root {{\n{0}", _Indent, Class.Name);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Member Member in Class.Entries) 
					foreach  (Member Member in Class.Entries) {
						// #% EntryDeclaration (Member); 
						 EntryDeclaration (Member);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// Encode ASN.1 class members to specified buffer.  
					_Output.Write ("		/// Encode ASN.1 class members to specified buffer. \n{0}", _Indent);
					// 		/// 
					_Output.Write ("		///\n{0}", _Indent);
					// 		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order. 
					_Output.Write ("		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order.\n{0}", _Indent);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		/// <param name="Buffer">Output buffer</param> 
					_Output.Write ("		/// <param name=\"Buffer\">Output buffer</param>\n{0}", _Indent);
					//         public override void Encode (Goedel.ASN.Buffer Buffer) { 
					_Output.Write ("        public override void Encode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					// 			int Position = Buffer.Encode__Sequence_Start (); 
					_Output.Write ("			int Position = Buffer.Encode__Sequence_Start ();\n{0}", _Indent);
					// #foreach (Member Member in Class.Entries) 
					foreach  (Member Member in Class.Entries) {
						// #! Encode (Member.Name.ToString(), Member.Spec, Member.Flags, Member.Code); 
						// Encode (Member.Name.ToString(), Member.Spec, Member.Flags, Member.Code);
						// #% Encode (Member); 
						 Encode (Member);
						// 			Buffer.Debug ("#{Member.Name}"); 
						_Output.Write ("			Buffer.Debug (\"{1}\");\n{0}", _Indent, Member.Name);
						// #end foreach 
						}
					// 			Buffer.Encode__Sequence_End (Position); 
					_Output.Write ("			Buffer.Encode__Sequence_End (Position);\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					// #casecast Object Object 
					break; }
					case ASN2Type.Object: {
					  Object Object = (Object) Toplevel; 
					//     /// <summary> 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					// 	/// #{Object.Name}  
					_Output.Write ("	/// {1} \n{0}", _Indent, Object.Name);
					//     /// </summary> 
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					// 	public partial class #{Object.Name} : Goedel.ASN.Root { 
					_Output.Write ("	public partial class {1} : Goedel.ASN.Root {{\n{0}", _Indent, Object.Name);
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// The OID value 
					_Output.Write ("		/// The OID value\n{0}", _Indent);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		public override int [] OID {  
					_Output.Write ("		public override int [] OID {{ \n{0}", _Indent);
					// 			get { return Constants.OID__#{Object.OID}; } }   
					_Output.Write ("			get {{ return Constants.OID__{1}; }} }}  \n{0}", _Indent, Object.OID);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #%  
					
					 
					// #foreach (Member Member in Object.Entries) 
					foreach  (Member Member in Object.Entries) {
						// #% EntryDeclaration (Member); 
						 EntryDeclaration (Member);
						// #end foreach 
						}
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// Encode ASN.1 class members to specified buffer.  
					_Output.Write ("		/// Encode ASN.1 class members to specified buffer. \n{0}", _Indent);
					// 		/// 
					_Output.Write ("		///\n{0}", _Indent);
					// 		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order. 
					_Output.Write ("		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order.\n{0}", _Indent);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		/// <param name="Buffer">Output buffer</param> 
					_Output.Write ("		/// <param name=\"Buffer\">Output buffer</param>\n{0}", _Indent);
					//         public override void Encode (Goedel.ASN.Buffer Buffer) { 
					_Output.Write ("        public override void Encode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					// 			int Position = Buffer.Encode__Sequence_Start (); 
					_Output.Write ("			int Position = Buffer.Encode__Sequence_Start ();\n{0}", _Indent);
					// #foreach (Member Member in Object.Entries) 
					foreach  (Member Member in Object.Entries) {
						// #! Encode (Member.Name.ToString(), Member.Spec, Member.Flags, Member.Code); 
						// Encode (Member.Name.ToString(), Member.Spec, Member.Flags, Member.Code);
						// #% Encode (Member); 
						 Encode (Member);
						// #end foreach 
						}
					// 			Buffer.Encode__Sequence_End (Position); 
					_Output.Write ("			Buffer.Encode__Sequence_End (Position);\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #casecast SingularObject SingularObject 
					break; }
					case ASN2Type.SingularObject: {
					  SingularObject SingularObject = (SingularObject) Toplevel; 
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	// Singular, no sequence boundaries 
					_Output.Write ("	// Singular, no sequence boundaries\n{0}", _Indent);
					//     /// <summary> 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					// 	/// #{SingularObject.Name}  
					_Output.Write ("	/// {1} \n{0}", _Indent, SingularObject.Name);
					//     /// </summary> 
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					// 	public partial class #{SingularObject.Name} : Goedel.ASN.Root { 
					_Output.Write ("	public partial class {1} : Goedel.ASN.Root {{\n{0}", _Indent, SingularObject.Name);
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// The OID value 
					_Output.Write ("		/// The OID value\n{0}", _Indent);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		public override int [] OID {  
					_Output.Write ("		public override int [] OID {{ \n{0}", _Indent);
					// 			get { return Constants.OID__#{SingularObject.OID}; } }   
					_Output.Write ("			get {{ return Constants.OID__{1}; }} }}  \n{0}", _Indent, SingularObject.OID);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Member Member in SingularObject.Entries) 
					foreach  (Member Member in SingularObject.Entries) {
						// #% EntryDeclaration (Member); 
						 EntryDeclaration (Member);
						// #end foreach 
						}
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					// 		/// Encode ASN.1 class members to specified buffer.  
					_Output.Write ("		/// Encode ASN.1 class members to specified buffer. \n{0}", _Indent);
					// 		/// 
					_Output.Write ("		///\n{0}", _Indent);
					// 		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order. 
					_Output.Write ("		/// NB Assinine ASN.1 DER encoding rules requires members be added in reverse order.\n{0}", _Indent);
					// 		/// </summary> 
					_Output.Write ("		/// </summary>\n{0}", _Indent);
					// 		/// <param name="Buffer">Output buffer</param> 
					_Output.Write ("		/// <param name=\"Buffer\">Output buffer</param>\n{0}", _Indent);
					//         public override void Encode (Goedel.ASN.Buffer Buffer) { 
					_Output.Write ("        public override void Encode (Goedel.ASN.Buffer Buffer) {{\n{0}", _Indent);
					// #foreach (Member Member in SingularObject.Entries) 
					foreach  (Member Member in SingularObject.Entries) {
						// #! Encode (Member.Name.ToString(), Member.Spec, Member.Flags, Member.Code); 
						// Encode (Member.Name.ToString(), Member.Spec, Member.Flags, Member.Code);
						// #% Encode (Member); 
						 Encode (Member);
						// #end foreach 
						}
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 	} 
			_Output.Write ("	}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method EntryDeclaration Member Member 
		

		//
		// EntryDeclaration
		//
		public void EntryDeclaration (Member Member) {
			// #switchcast ASN2Type Member.Spec 
			switch (Member.Spec._Tag ()) {
				// #casecast Choice Cast 
				case ASN2Type.Choice: {
				  Choice Cast = (Choice) Member.Spec; 
				// #foreach (Member Entry in Cast.Entries) 
				foreach  (Member Entry in Cast.Entries) {
					// #% EntryDeclaration (Entry); 
					 EntryDeclaration (Entry);
					// #end foreach 
					}
				// #%  break; } default : { 
				
				  break; } default : {
				// 		/// <summary> 
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				// 		/// ASN.1 member #{Member.Name}  
				_Output.Write ("		/// ASN.1 member {1} \n{0}", _Indent, Member.Name);
				// 		/// </summary> 
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				// 		public #! 
				_Output.Write ("		public ", _Indent);
				// #% TypeDeclaration (Member.Spec); 
				
				 TypeDeclaration (Member.Spec);
				// #{Member.Name} ; 
				_Output.Write ("{1} ;\n{0}", _Indent, Member.Name);
				// #end switchcast 
			break; }
				}
			// #end method 
			}
		//  
		//  
		// #method TypeDeclaration _Choice Type 
		

		//
		// TypeDeclaration
		//
		public void TypeDeclaration (_Choice Type) {
			// #switchcast ASN2Type Type 
			switch (Type._Tag ()) {
				// #casecast OIDRef Cast 
				case ASN2Type.OIDRef: {
				  OIDRef Cast = (OIDRef) Type; 
				// int []  #! 
				_Output.Write ("int []  ", _Indent);
				// #casecast Any Cast 
				break; }
				case ASN2Type.Any: {
				  Any Cast = (Any) Type; 
				// byte []  #! 
				_Output.Write ("byte []  ", _Indent);
				// #casecast Integer Cast 
				break; }
				case ASN2Type.Integer: {
				  Integer Cast = (Integer) Type; 
				// int  #! 
				_Output.Write ("int  ", _Indent);
				// #casecast BigInteger Cast 
				break; }
				case ASN2Type.BigInteger: {
				  BigInteger Cast = (BigInteger) Type; 
				// byte []  #! 
				_Output.Write ("byte []  ", _Indent);
				// #casecast Boolean Cast 
				break; }
				case ASN2Type.Boolean: {
				  Boolean Cast = (Boolean) Type; 
				// bool  #! 
				_Output.Write ("bool  ", _Indent);
				// #casecast Time Cast 
				break; }
				case ASN2Type.Time: {
				  Time Cast = (Time) Type; 
				// DateTime  #! 
				_Output.Write ("DateTime  ", _Indent);
				// #casecast Bits Cast 
				break; }
				case ASN2Type.Bits: {
				  Bits Cast = (Bits) Type; 
				// byte []  #! 
				_Output.Write ("byte []  ", _Indent);
				// #casecast VBits Cast 
				break; }
				case ASN2Type.VBits: {
				  VBits Cast = (VBits) Type; 
				// byte []  #! 
				_Output.Write ("byte []  ", _Indent);
				// #casecast Octets Cast 
				break; }
				case ASN2Type.Octets: {
				  Octets Cast = (Octets) Type; 
				// byte []  #! 
				_Output.Write ("byte []  ", _Indent);
				// #casecast IA5String Cast 
				break; }
				case ASN2Type.IA5String: {
				  IA5String Cast = (IA5String) Type; 
				// string #! 
				_Output.Write ("string ", _Indent);
				// #casecast BMPString Cast 
				break; }
				case ASN2Type.BMPString: {
				  BMPString Cast = (BMPString) Type; 
				// string #! 
				_Output.Write ("string ", _Indent);
				// #casecast UTF8String Cast 
				break; }
				case ASN2Type.UTF8String: {
				  UTF8String Cast = (UTF8String) Type; 
				// string #! 
				_Output.Write ("string ", _Indent);
				// #casecast PrintableString Cast 
				break; }
				case ASN2Type.PrintableString: {
				  PrintableString Cast = (PrintableString) Type; 
				// string #! 
				_Output.Write ("string ", _Indent);
				// #casecast _Label Case 
				break; }
				case ASN2Type._Label: {
				  _Label Case = (_Label) Type; 
				// #{Namespace}.#{Case.Label} #! 
				_Output.Write ("{1}.{2} ", _Indent, Namespace, Case.Label);
				// #casecast List Cast 
				break; }
				case ASN2Type.List: {
				  List Cast = (List) Type; 
				// List <#! 
				_Output.Write ("List <", _Indent);
				// #% TypeDeclaration (Cast.Spec); 
				
				 TypeDeclaration (Cast.Spec);
				// > #! 
				_Output.Write ("> ", _Indent);
				// #casecast Set Cast 
				break; }
				case ASN2Type.Set: {
				  Set Cast = (Set) Type; 
				// List <#! 
				_Output.Write ("List <", _Indent);
				// #% TypeDeclaration (Cast.Spec); 
				
				 TypeDeclaration (Cast.Spec);
				// > #! 
				_Output.Write ("> ", _Indent);
				// #casecast Choice Cast 
				break; }
				case ASN2Type.Choice: {
				  Choice Cast = (Choice) Type; 
				// // Choice declare all the type parts 
				_Output.Write ("// Choice declare all the type parts\n{0}", _Indent);
				// #end switchcast 
			break; }
				}
			// #end method 
			}
		//  
		// #method Encode Member Member 
		

		//
		// Encode
		//
		public void Encode (Member Member) {
			// #%  Encode (Member.Name.ToString(), Member.Default, Member.Spec, Member.Flags, Member.Code); 
			  Encode (Member.Name.ToString(), Member.Default, Member.Spec, Member.Flags, Member.Code);
			// #end method 
			}
		// #block 
		

		//
		// 
		//

			// #%  public void Encode (String Name, _Choice Spec, int Flags, int Code) { 
			  public void Encode (String Name, _Choice Spec, int Flags, int Code) {
			// #%		Encode (Name, null, Spec, Flags, Code); 
					Encode (Name, null, Spec, Flags, Code);
			// #%		} 
					}
			// #%  public void Encode (String Name, String Default, _Choice Spec, int Flags, int Code) { 
			  public void Encode (String Name, String Default, _Choice Spec, int Flags, int Code) {
			//  
			_Output.Write ("\n{0}", _Indent);
			// #% bool Call = false; 
			 bool Call = false;
			// #switchcast ASN2Type Spec 
			switch (Spec._Tag ()) {
				// #casecast OIDRef Cast 
				case ASN2Type.OIDRef: {
				  OIDRef Cast = (OIDRef) Spec; 
				// 			Buffer.Encode__OIDRef #! 
				_Output.Write ("			Buffer.Encode__OIDRef ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast Any Cast 
				break; }
				case ASN2Type.Any: {
				  Any Cast = (Any) Spec; 
				// 			Buffer.Encode__Any #! 
				_Output.Write ("			Buffer.Encode__Any ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast Integer Cast 
				break; }
				case ASN2Type.Integer: {
				  Integer Cast = (Integer) Spec; 
				// #if (Default != null) 
				if (  (Default != null) ) {
					// 			// Default is #{Default} 
					_Output.Write ("			// Default is {1}\n{0}", _Indent, Default);
					// 			if (#{Name} != #{Default}) { 
					_Output.Write ("			if ({1} != {2}) {{\n{0}", _Indent, Name, Default);
					// 				Buffer.Encode__Integer (#{Name}, #{Flags}, #{Code}); 
					_Output.Write ("				Buffer.Encode__Integer ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
					// 				} 
					_Output.Write ("				}}\n{0}", _Indent);
					// #else  
					} else {
					// 			Buffer.Encode__Integer #! 
					_Output.Write ("			Buffer.Encode__Integer ", _Indent);
					// #% Call = true; 
					 Call = true;
					// #end if 
					}
				// #casecast BigInteger Cast 
				break; }
				case ASN2Type.BigInteger: {
				  BigInteger Cast = (BigInteger) Spec; 
				// 			Buffer.Encode__BigInteger #! 
				_Output.Write ("			Buffer.Encode__BigInteger ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast Boolean Cast 
				break; }
				case ASN2Type.Boolean: {
				  Boolean Cast = (Boolean) Spec; 
				// #if (Default != null) 
				if (  (Default != null) ) {
					// 			// Default is #{Default} 
					_Output.Write ("			// Default is {1}\n{0}", _Indent, Default);
					// 			if (#{Name} != #{Default}) { 
					_Output.Write ("			if ({1} != {2}) {{\n{0}", _Indent, Name, Default);
					// 				Buffer.Encode__Boolean (#{Name}, #{Flags}, #{Code}); 
					_Output.Write ("				Buffer.Encode__Boolean ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
					// 				} 
					_Output.Write ("				}}\n{0}", _Indent);
					// #else  
					} else {
					// 			Buffer.Encode__Boolean #! 
					_Output.Write ("			Buffer.Encode__Boolean ", _Indent);
					// #% Call = true; 
					 Call = true;
					// #end if 
					}
				// #casecast Time Cast 
				break; }
				case ASN2Type.Time: {
				  Time Cast = (Time) Spec; 
				// 			Buffer.Encode__Time #! 
				_Output.Write ("			Buffer.Encode__Time ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast Bits Cast 
				break; }
				case ASN2Type.Bits: {
				  Bits Cast = (Bits) Spec; 
				// 			Buffer.Encode__Bits #! 
				_Output.Write ("			Buffer.Encode__Bits ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast VBits Cast 
				break; }
				case ASN2Type.VBits: {
				  VBits Cast = (VBits) Spec; 
				// 			Buffer.Encode__VBits #! 
				_Output.Write ("			Buffer.Encode__VBits ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast Octets Cast 
				break; }
				case ASN2Type.Octets: {
				  Octets Cast = (Octets) Spec; 
				// 			Buffer.Encode__Octets #! 
				_Output.Write ("			Buffer.Encode__Octets ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast IA5String Cast 
				break; }
				case ASN2Type.IA5String: {
				  IA5String Cast = (IA5String) Spec; 
				// 			Buffer.Encode__IA5String #! 
				_Output.Write ("			Buffer.Encode__IA5String ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast BMPString Cast 
				break; }
				case ASN2Type.BMPString: {
				  BMPString Cast = (BMPString) Spec; 
				// 			Buffer.Encode__BMPString #! 
				_Output.Write ("			Buffer.Encode__BMPString ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast UTF8String Cast 
				break; }
				case ASN2Type.UTF8String: {
				  UTF8String Cast = (UTF8String) Spec; 
				// 			Buffer.Encode__UTF8String #! 
				_Output.Write ("			Buffer.Encode__UTF8String ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast PrintableString Cast 
				break; }
				case ASN2Type.PrintableString: {
				  PrintableString Cast = (PrintableString) Spec; 
				// 			Buffer.Encode__PrintableString #! 
				_Output.Write ("			Buffer.Encode__PrintableString ", _Indent);
				// #% Call = true; 
				
				 Call = true;
				// #casecast _Label Case 
				break; }
				case ASN2Type._Label: {
				  _Label Case = (_Label) Spec; 
				// 			Buffer.Encode__Object (#{Name}, #{Flags}, #{Code}); 
				_Output.Write ("			Buffer.Encode__Object ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
				// #casecast List Cast 
				break; }
				case ASN2Type.List: {
				  List Cast = (List) Spec; 
				// 			if (#{Name} == null || #{Name}.Count == 0) { 
				_Output.Write ("			if ({1} == null || {2}.Count == 0) {{\n{0}", _Indent, Name, Name);
				// 				Buffer.Encode__Object (null, #{Flags}, #{Code}); 
				_Output.Write ("				Buffer.Encode__Object (null, {1}, {2});\n{0}", _Indent, Flags, Code);
				// 				} 
				_Output.Write ("				}}\n{0}", _Indent);
				// 			else { 
				_Output.Write ("			else {{\n{0}", _Indent);
				// 				int XPosition = Buffer.Encode__Sequence_Start(); 
				_Output.Write ("				int XPosition = Buffer.Encode__Sequence_Start();\n{0}", _Indent);
				// 				foreach (#! 
				_Output.Write ("				foreach (", _Indent);
				// #% TypeDeclaration (Cast.Spec); 
				
				 TypeDeclaration (Cast.Spec);
				//  _Index in #{Name}) { 
				_Output.Write (" _Index in {1}) {{\n{0}", _Indent, Name);
				// 		#! 
				_Output.Write ("		", _Indent);
				// #% Encode ("_Index", Cast.Spec, 0, 0); 
				
				 Encode ("_Index", Cast.Spec, 0, 0);
				// 					} 
				_Output.Write ("					}}\n{0}", _Indent);
				// 				Buffer.Encode__Sequence_End(XPosition, #{Flags}, #{Code}); 
				_Output.Write ("				Buffer.Encode__Sequence_End(XPosition, {1}, {2});\n{0}", _Indent, Flags, Code);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				// #casecast Set Cast 
				break; }
				case ASN2Type.Set: {
				  Set Cast = (Set) Spec; 
				// 			if (#{Name} == null || #{Name}.Count == 0) { 
				_Output.Write ("			if ({1} == null || {2}.Count == 0) {{\n{0}", _Indent, Name, Name);
				// 				Buffer.Encode__Object (null, #{Flags}, #{Code}); 
				_Output.Write ("				Buffer.Encode__Object (null, {1}, {2});\n{0}", _Indent, Flags, Code);
				// 				} 
				_Output.Write ("				}}\n{0}", _Indent);
				// 			else { 
				_Output.Write ("			else {{\n{0}", _Indent);
				// 				int XPosition = Buffer.Encode__Set_Start(); 
				_Output.Write ("				int XPosition = Buffer.Encode__Set_Start();\n{0}", _Indent);
				// 				foreach (#! 
				_Output.Write ("				foreach (", _Indent);
				// #% TypeDeclaration (Cast.Spec); 
				
				 TypeDeclaration (Cast.Spec);
				//  _Index in #{Name}) { 
				_Output.Write (" _Index in {1}) {{\n{0}", _Indent, Name);
				// 		#! 
				_Output.Write ("		", _Indent);
				// #% Encode ("_Index", Cast.Spec, 0, 0); 
				
				 Encode ("_Index", Cast.Spec, 0, 0);
				// 					} 
				_Output.Write ("					}}\n{0}", _Indent);
				// 				Buffer.Encode__Set_End(XPosition, #{Flags}, #{Code}); 
				_Output.Write ("				Buffer.Encode__Set_End(XPosition, {1}, {2});\n{0}", _Indent, Flags, Code);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				// #casecast Choice Cast 
				break; }
				case ASN2Type.Choice: {
				  Choice Cast = (Choice) Spec; 
				// 	// Do Choice 
				_Output.Write ("	// Do Choice\n{0}", _Indent);
				// #foreach (Member ChoiceEntry in Cast.Entries) 
				foreach  (Member ChoiceEntry in Cast.Entries) {
					//             // 
					_Output.Write ("            //\n{0}", _Indent);
					// #! Encode (ChoiceEntry.Name.ToString(), ChoiceEntry.Spec, ChoiceEntry.Flags, ChoiceEntry.Code); 
					// Encode (ChoiceEntry.Name.ToString(), ChoiceEntry.Spec, ChoiceEntry.Flags, ChoiceEntry.Code);
					// #% Encode (ChoiceEntry); 
					 Encode (ChoiceEntry);
					// #end foreach 
					}
				// #end switchcast 
			break; }
				}
			// #if Call 
			if (  Call ) {
				//  (#{Name}, #{Flags}, #{Code}); 
				_Output.Write (" ({1}, {2}, {3});\n{0}", _Indent, Name, Flags, Code);
				// #end if 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #method MakeChildren _Choice Element 
		

		//
		// MakeChildren
		//
		public void MakeChildren (_Choice Element) {
			// #foreach (OID OID in Element.Children) 
			foreach  (OID OID in Element.Children) {
				//  
				_Output.Write ("\n{0}", _Indent);
				// 		/// <summary> 
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				// 		/// #{OID.Name} = #{OID.Root} (#{OID.Value}) as integer sequence 
				_Output.Write ("		/// {1} = {2} ({3}) as integer sequence\n{0}", _Indent, OID.Name, OID.Root, OID.Value);
				// 		/// </summary> 
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				// 		public readonly static int [] OID__#{OID.Name} = new int [] { #! 
				_Output.Write ("		public readonly static int [] OID__{1} = new int [] {{ ", _Indent, OID.Name);
				// #% MakeConst(OID.Binary) ;	 
				 MakeConst(OID.Binary) ;	
				// }; 
				_Output.Write ("}};\n{0}", _Indent);
				// 		/// <summary> 
				_Output.Write ("		/// <summary>\n{0}", _Indent);
				// 		/// #{OID.Name} = #{OID.Root} (#{OID.Value}) as string 
				_Output.Write ("		/// {1} = {2} ({3}) as string\n{0}", _Indent, OID.Name, OID.Root, OID.Value);
				// 		/// </summary> 
				_Output.Write ("		/// </summary>\n{0}", _Indent);
				// 		public const string OIDS__#{OID.Name} = "#! 
				_Output.Write ("		public const string OIDS__{1} = \"", _Indent, OID.Name);
				// #% MakeString(OID.Binary) ;	 
				 MakeString(OID.Binary) ;	
				// "; 
				_Output.Write ("\";\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #% MakeChildren (OID); 
				 MakeChildren (OID);
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #method MakeConst int[] Array 
		

		//
		// MakeConst
		//
		public void MakeConst (int[] Array) {
			// #% bool Comma = false; 
			 bool Comma = false;
			// #foreach (int Value in Array) 
			foreach  (int Value in Array) {
				// #if Comma 
				if (  Comma ) {
					// , #! 
					_Output.Write (", ", _Indent);
					// #end if 
					}
				// #% Comma = true; 
				 Comma = true;
				// #{Value}#! 
				_Output.Write ("{1}", _Indent, Value);
				// #end foreach 
				}
			// #end method 
			}
		//  
		//  
		// #method MakeString int[] Array 
		

		//
		// MakeString
		//
		public void MakeString (int[] Array) {
			// #% bool Comma = false; 
			 bool Comma = false;
			// #foreach (int Value in Array) 
			foreach  (int Value in Array) {
				// #if Comma 
				if (  Comma ) {
					// .#! 
					_Output.Write (".", _Indent);
					// #end if 
					}
				// #% Comma = true; 
				 Comma = true;
				// #{Value}#! 
				_Output.Write ("{1}", _Indent, Value);
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #end pclass 
		}
	}
