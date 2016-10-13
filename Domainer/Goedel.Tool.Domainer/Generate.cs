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
// #pclass Goedel.Tool.Domainer Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Domainer {
	public partial class Generate : global::Goedel.Registry.Script {
		public Generate () : base () {
			}
		public Generate (TextWriter Output) : base (Output) {
			}

		//  
		// #method GenerateCS Domainer Domainer 
		

		//
		// GenerateCS
		//
		public void GenerateCS (Domainer Domainer) {
			// using System.Net; 
			_Output.Write ("using System.Net;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// namespace Goedel.DNS { 
			_Output.Write ("namespace Goedel.DNS {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	public partial class DNS { 
			_Output.Write ("	public partial class DNS {{\n{0}", _Indent);
			// 		// Dictionary of Type names to codes 
			_Output.Write ("		// Dictionary of Type names to codes\n{0}", _Indent);
			// 		// 
			_Output.Write ("		//\n{0}", _Indent);
			// 		// if (DictionaryType.ContainsKey("RR") { 
			_Output.Write ("		// if (DictionaryType.ContainsKey(\"RR\") {{\n{0}", _Indent);
			// 		//    int value = dictionary["RR"]; 
			_Output.Write ("		//    int value = dictionary[\"RR\"];\n{0}", _Indent);
			// 		//    } 
			_Output.Write ("		//    }}\n{0}", _Indent);
			// 		static Dictionary <string, ushort> DictionaryType = new Dictionary <string, ushort> () { 
			_Output.Write ("		static Dictionary <string, ushort> DictionaryType = new Dictionary <string, ushort> () {{\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Domainer.Top) 
			foreach  (_Choice Toplevel in Domainer.Top) {
				// #switchcast DomainerType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast RR RR 
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					// 			{"#{RR.Id}", #{RR.Code}}, 
					_Output.Write ("			{{\"{1}\", {2}}},\n{0}", _Indent, RR.Id, RR.Code);
					// #casecast Q Q 
					break; }
					case DomainerType.Q: {
					  Q Q = (Q) Toplevel; 
					// 			{"#{Q.Id}", #{Q.Code}}, 
					_Output.Write ("			{{\"{1}\", {2}}},\n{0}", _Indent, Q.Id, Q.Code);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 			{"*", 255} // End of list * = ALL 
			_Output.Write ("			{{\"*\", 255}} // End of list * = ALL\n{0}", _Indent);
			// 			} ; 
			_Output.Write ("			}} ;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		static Dictionary <ushort, string> DictionaryCode = new Dictionary <ushort, string> () { 
			_Output.Write ("		static Dictionary <ushort, string> DictionaryCode = new Dictionary <ushort, string> () {{\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Domainer.Top) 
			foreach  (_Choice Toplevel in Domainer.Top) {
				// #switchcast DomainerType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast RR RR 
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					// 			{#{RR.Code}, "#{RR.Id}"}, 
					_Output.Write ("			{{{1}, \"{2}\"}},\n{0}", _Indent, RR.Code, RR.Id);
					// #casecast Q Q 
					break; }
					case DomainerType.Q: {
					  Q Q = (Q) Toplevel; 
					// 			{#{Q.Code}, "#{Q.Id}"}, 
					_Output.Write ("			{{{1}, \"{2}\"}},\n{0}", _Indent, Q.Code, Q.Id);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 			{0, ""} // End of list * = ALL 
			_Output.Write ("			{{0, \"\"}} // End of list * = ALL\n{0}", _Indent);
			// 			} ; 
			_Output.Write ("			}} ;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public static DNSTypeCode TypeCode(string Tag) { 
			_Output.Write ("        public static DNSTypeCode TypeCode(string Tag) {{\n{0}", _Indent);
			//             if (Tag != null) { 
			_Output.Write ("            if (Tag != null) {{\n{0}", _Indent);
			//                 return (DNSTypeCode) DictionaryType[Tag]; 
			_Output.Write ("                return (DNSTypeCode) DictionaryType[Tag];\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//             return 0; 
			_Output.Write ("            return 0;\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//         public static string TypeCode(int Code ) { 
			_Output.Write ("        public static string TypeCode(int Code ) {{\n{0}", _Indent);
			//             return DictionaryCode[(ushort)Code]; 
			_Output.Write ("            return DictionaryCode[(ushort)Code];\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	public enum DNSTypeCode : ushort { 
			_Output.Write ("	public enum DNSTypeCode : ushort {{\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Domainer.Top) 
			foreach  (_Choice Toplevel in Domainer.Top) {
				// #switchcast DomainerType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast RR RR 
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					// 		#{RR.Id} = #{RR.Code}, 
					_Output.Write ("		{1} = {2},\n{0}", _Indent, RR.Id, RR.Code);
					// #casecast Q Q 
					break; }
					case DomainerType.Q: {
					  Q Q = (Q) Toplevel; 
					// 		#{Q.Id} = #{Q.Code},  // Used in Queries only 
					_Output.Write ("		{1} = {2},  // Used in Queries only\n{0}", _Indent, Q.Id, Q.Code);
					// #casecast IG IG 
					break; }
					case DomainerType.IG: {
					  IG IG = (IG) Toplevel; 
					// 		#{IG.Id} = #{IG.Code},  // Deprecated, NOT IMPLEMENTED 
					_Output.Write ("		{1} = {2},  // Deprecated, NOT IMPLEMENTED\n{0}", _Indent, IG.Id, IG.Code);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 		Unknown = 0 
			_Output.Write ("		Unknown = 0\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// All resource record classes are descended from DNSRR 
			_Output.Write ("	// All resource record classes are descended from DNSRR\n{0}", _Indent);
			// 	public abstract partial  class DNSRecord { 
			_Output.Write ("	public abstract partial  class DNSRecord {{\n{0}", _Indent);
			// 		public virtual DNSTypeCode			Code { 
			_Output.Write ("		public virtual DNSTypeCode			Code {{\n{0}", _Indent);
			// 			get {return (0);} }		 
			_Output.Write ("			get {{return (0);}} }}		\n{0}", _Indent);
			// 		public virtual string	Label { 
			_Output.Write ("		public virtual string	Label {{\n{0}", _Indent);
			// 			get {return ("Unknown");} }	 
			_Output.Write ("			get {{return (\"Unknown\");}} }}	\n{0}", _Indent);
			// 		public virtual string	Description { 
			_Output.Write ("		public virtual string	Description {{\n{0}", _Indent);
			// 			get {return ("Record is not defined");} } 
			_Output.Write ("			get {{return (\"Record is not defined\");}} }}\n{0}", _Indent);
			// 			 
			_Output.Write ("			\n{0}", _Indent);
			// 			 
			_Output.Write ("			\n{0}", _Indent);
			// 			 
			_Output.Write ("			\n{0}", _Indent);
			//         public static DNSRecord Decode(DNSBufferIndex Index) { 
			_Output.Write ("        public static DNSRecord Decode(DNSBufferIndex Index) {{\n{0}", _Indent);
			// 			DNSRecord			DNSRecord; 
			_Output.Write ("			DNSRecord			DNSRecord;\n{0}", _Indent);
			// 			 
			_Output.Write ("			\n{0}", _Indent);
			// 			Domain				Domain; 
			_Output.Write ("			Domain				Domain;\n{0}", _Indent);
			// 			DNSTypeCode			RType; 
			_Output.Write ("			DNSTypeCode			RType;\n{0}", _Indent);
			// 			DNSClass			RClass; 
			_Output.Write ("			DNSClass			RClass;\n{0}", _Indent);
			// 			uint				TTL; 
			_Output.Write ("			uint				TTL;\n{0}", _Indent);
			// 			int				RDLength; 
			_Output.Write ("			int				RDLength;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             Domain = Index.ReadDomain (); 
			_Output.Write ("            Domain = Index.ReadDomain ();\n{0}", _Indent);
			//             RType = (DNSTypeCode)Index.ReadInt16 (); 
			_Output.Write ("            RType = (DNSTypeCode)Index.ReadInt16 ();\n{0}", _Indent);
			//             RClass = (DNSClass)Index.ReadInt16 (); 
			_Output.Write ("            RClass = (DNSClass)Index.ReadInt16 ();\n{0}", _Indent);
			//             TTL = Index.ReadInt32 (); 
			_Output.Write ("            TTL = Index.ReadInt32 ();\n{0}", _Indent);
			// 			RDLength = Index.ReadInt16 (); 
			_Output.Write ("			RDLength = Index.ReadInt16 ();\n{0}", _Indent);
			// 			int NextRecord = Index.Pointer + RDLength; 
			_Output.Write ("			int NextRecord = Index.Pointer + RDLength;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             //Index.ReadL16Data (out RData); 
			_Output.Write ("            //Index.ReadL16Data (out RData);\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			switch ((int) RType) { 
			_Output.Write ("			switch ((int) RType) {{\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Domainer.Top) 
			foreach  (_Choice Toplevel in Domainer.Top) {
				// #switchcast DomainerType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast RR RR 
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					// 				case (#{RR.Code}) : { 
					_Output.Write ("				case ({1}) : {{\n{0}", _Indent, RR.Code);
					// 					DNSRecord = DNSRecord_#{RR.Id}.Decode (Index, RDLength); 
					_Output.Write ("					DNSRecord = DNSRecord_{1}.Decode (Index, RDLength);\n{0}", _Indent, RR.Id);
					// 					break; 
					_Output.Write ("					break;\n{0}", _Indent);
					// 					} 
					_Output.Write ("					}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// 				default : { 
			_Output.Write ("				default : {{\n{0}", _Indent);
			// 					DNSRecord = DNSRecord_Unknown.Decode (Index, RDLength) ; 
			_Output.Write ("					DNSRecord = DNSRecord_Unknown.Decode (Index, RDLength) ;\n{0}", _Indent);
			// 					break; 
			_Output.Write ("					break;\n{0}", _Indent);
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			DNSRecord.Domain = Domain; 
			_Output.Write ("			DNSRecord.Domain = Domain;\n{0}", _Indent);
			// 			DNSRecord.RType = RType; 
			_Output.Write ("			DNSRecord.RType = RType;\n{0}", _Indent);
			// 			DNSRecord.RClass = RClass; 
			_Output.Write ("			DNSRecord.RClass = RClass;\n{0}", _Indent);
			// 			DNSRecord.TTL = TTL; 
			_Output.Write ("			DNSRecord.TTL = TTL;\n{0}", _Indent);
			// 			Index.Pointer = NextRecord; 
			_Output.Write ("			Index.Pointer = NextRecord;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             return DNSRecord; 
			_Output.Write ("            return DNSRecord;\n{0}", _Indent);
			//             }				 
			_Output.Write ("            }}				\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public static DNSRecord Parse(string Tag, Parse Parse) { 
			_Output.Write ("		public static DNSRecord Parse(string Tag, Parse Parse) {{\n{0}", _Indent);
			// 			switch (Tag) { 
			_Output.Write ("			switch (Tag) {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Domainer.Top) 
			foreach  (_Choice Toplevel in Domainer.Top) {
				// #switchcast DomainerType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast RR RR 
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					// 				case ("#{RR.Id}") : { 
					_Output.Write ("				case (\"{1}\") : {{\n{0}", _Indent, RR.Id);
					// 					return DNSRecord_#{RR.Id}.Parse (Parse); 
					_Output.Write ("					return DNSRecord_{1}.Parse (Parse);\n{0}", _Indent, RR.Id);
					// 					} 
					_Output.Write ("					}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 				default : { 
			_Output.Write ("				default : {{\n{0}", _Indent);
			// 					return null; 
			_Output.Write ("					return null;\n{0}", _Indent);
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		 
			_Output.Write ("		\n{0}", _Indent);
			// 	public partial class DNSRecord_Unknown : DNSRecord { 
			_Output.Write ("	public partial class DNSRecord_Unknown : DNSRecord {{\n{0}", _Indent);
			// 		//public DNSBufferIndex   RData; 
			_Output.Write ("		//public DNSBufferIndex   RData;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		public static DNSRecord_Unknown Decode (DNSBufferIndex Index, int Length) { 
			_Output.Write ("		public static DNSRecord_Unknown Decode (DNSBufferIndex Index, int Length) {{\n{0}", _Indent);
			// 			DNSRecord_Unknown NewRecord = new DNSRecord_Unknown ();  
			_Output.Write ("			DNSRecord_Unknown NewRecord = new DNSRecord_Unknown (); \n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			Index.ReadL16Data (out NewRecord.RData); 
			_Output.Write ("			Index.ReadL16Data (out NewRecord.RData);\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			return NewRecord; 
			_Output.Write ("			return NewRecord;\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			// 		} 
			_Output.Write ("		}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Toplevel in Domainer.Top) 
			foreach  (_Choice Toplevel in Domainer.Top) {
				// #switchcast DomainerType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast RR RR 
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	// #{RR.Id} #{RR.Code} #{RR.Description} 
					_Output.Write ("	// {1} {2} {3}\n{0}", _Indent, RR.Id, RR.Code, RR.Description);
					// 	//     See #{RR.Reference} 
					_Output.Write ("	//     See {1}\n{0}", _Indent, RR.Reference);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	public class DNSRecord_#{RR.Id} : DNSRecord { 
					_Output.Write ("	public class DNSRecord_{1} : DNSRecord {{\n{0}", _Indent, RR.Id);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (_Choice Entry in RR.Entries) 
					foreach  (_Choice Entry in RR.Entries) {
						// #if (Entry.TypeCS != null) 
						if (  (Entry.TypeCS != null) ) {
							// 		public #{Entry.TypeCS}   #{Entry.IdLabel}  ; 
							_Output.Write ("		public {1}   {2}  ;\n{0}", _Indent, Entry.TypeCS, Entry.IdLabel);
							// #end if 
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		public override DNSTypeCode		Code { 
					_Output.Write ("		public override DNSTypeCode		Code {{\n{0}", _Indent);
					// 			get {return (DNSTypeCode.#{RR.Id});} }		 
					_Output.Write ("			get {{return (DNSTypeCode.{1});}} }}		\n{0}", _Indent, RR.Id);
					// 		public override string	Label { 
					_Output.Write ("		public override string	Label {{\n{0}", _Indent);
					// 			get {return ("#{RR.Id}");} }	 
					_Output.Write ("			get {{return (\"{1}\");}} }}	\n{0}", _Indent, RR.Id);
					// 		public override string	Description { 
					_Output.Write ("		public override string	Description {{\n{0}", _Indent);
					// 			get {return ("#{RR.Description}");} } 
					_Output.Write ("			get {{return (\"{1}\");}} }}\n{0}", _Indent, RR.Description);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         // Convert to canonical form 
					_Output.Write ("        // Convert to canonical form\n{0}", _Indent);
					//         public override string Canonical () { 
					_Output.Write ("        public override string Canonical () {{\n{0}", _Indent);
					// 			Canonicalize Canonicalize = new Canonicalize ("#{RR.Id}", Domain); 
					_Output.Write ("			Canonicalize Canonicalize = new Canonicalize (\"{1}\", Domain);\n{0}", _Indent, RR.Id);
					// #foreach (_Choice Entry in RR.Entries) 
					foreach  (_Choice Entry in RR.Entries) {
						// #if (Entry.Tag != null) 
						if (  (Entry.Tag != null) ) {
							// 			Canonicalize.#{Entry.Tag}  (#{Entry.IdLabel}); 
							_Output.Write ("			Canonicalize.{1}  ({2});\n{0}", _Indent, Entry.Tag, Entry.IdLabel);
							// #end if 
							}
						// #end foreach 
						}
					// 			return Canonicalize.Text; 
					_Output.Write ("			return Canonicalize.Text;\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         public static DNSRecord_#{RR.Id} Parse(Parse Parse) { 
					_Output.Write ("        public static DNSRecord_{1} Parse(Parse Parse) {{\n{0}", _Indent, RR.Id);
					// 			DNSRecord_#{RR.Id} NewRecord = new DNSRecord_#{RR.Id} () ; 
					_Output.Write ("			DNSRecord_{1} NewRecord = new DNSRecord_{2} () ;\n{0}", _Indent, RR.Id, RR.Id);
					// 			 
					_Output.Write ("			\n{0}", _Indent);
					// #foreach (_Choice Entry in RR.Entries) 
					foreach  (_Choice Entry in RR.Entries) {
						// #if (Entry.Tag != null) 
						if (  (Entry.Tag != null) ) {
							// 			NewRecord.#{Entry.IdLabel} = Parse.#{Entry.Tag}  (); 
							_Output.Write ("			NewRecord.{1} = Parse.{2}  ();\n{0}", _Indent, Entry.IdLabel, Entry.Tag);
							// #end if 
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			return NewRecord; 
					_Output.Write ("			return NewRecord;\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         // Convert to byte form 
					_Output.Write ("        // Convert to byte form\n{0}", _Indent);
					//         public override void Encode(DNSBufferIndex Index) { 
					_Output.Write ("        public override void Encode(DNSBufferIndex Index) {{\n{0}", _Indent);
					// 			//Encode Encode = new Encode (); 
					_Output.Write ("			//Encode Encode = new Encode ();\n{0}", _Indent);
					// #foreach (_Choice Entry in RR.Entries) 
					foreach  (_Choice Entry in RR.Entries) {
						// #switchcast DomainerType Entry 
						switch (Entry._Tag ()) {
							// #casecast IPv4 null 
							case DomainerType.IPv4: { 
							// 			Index.WriteIPv4 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteIPv4 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast IPv6 null 
							break; }
							case DomainerType.IPv6: { 
							// 			Index.WriteIPv6 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteIPv6 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Domain null 
							break; }
							case DomainerType.Domain: { 
							// 			Index.WriteDomain (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteDomain ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Mail null 
							break; }
							case DomainerType.Mail: { 
							// 			Index.WriteMail (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteMail ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast NodeID null 
							break; }
							case DomainerType.NodeID: { 
							// 			Index.WriteInt64 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteInt64 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Byte null 
							break; }
							case DomainerType.Byte: { 
							// 			Index.WriteByte (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteByte ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Int16 null 
							break; }
							case DomainerType.Int16: { 
							// 			Index.WriteInt16 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteInt16 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Int32 null 
							break; }
							case DomainerType.Int32: { 
							// 			Index.WriteInt32 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteInt32 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Time32 null 
							break; }
							case DomainerType.Time32: { 
							// 			Index.WriteInt32 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteInt32 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Time48 null 
							break; }
							case DomainerType.Time48: { 
							// 			Index.WriteInt48 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteInt48 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast String null 
							break; }
							case DomainerType.String: { 
							// 			Index.WriteString8 (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteString8 ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast OptionalString null 
							break; }
							case DomainerType.OptionalString: { 
							// 			if (#{Entry.IdLabel} != null) { 
							_Output.Write ("			if ({1} != null) {{\n{0}", _Indent, Entry.IdLabel);
							// 				Index.WriteString8 (#{Entry.IdLabel}); 
							_Output.Write ("				Index.WriteString8 ({1});\n{0}", _Indent, Entry.IdLabel);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// #casecast Strings null 
							break; }
							case DomainerType.Strings: { 
							// 			foreach (string s in #{Entry.IdLabel}) { 
							_Output.Write ("			foreach (string s in {1}) {{\n{0}", _Indent, Entry.IdLabel);
							// 				Index.WriteString8 (s); 
							_Output.Write ("				Index.WriteString8 (s);\n{0}", _Indent);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// #casecast StringX null 
							break; }
							case DomainerType.StringX: { 
							// 			Index.WriteString (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteString ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Binary null 
							break; }
							case DomainerType.Binary: { 
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Binary8 null 
							break; }
							case DomainerType.Binary8: { 
							// 			Index.WriteByte ((byte)#{Entry.IdLabel}.Length); 
							_Output.Write ("			Index.WriteByte ((byte){1}.Length);\n{0}", _Indent, Entry.IdLabel);
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Binary16 null 
							break; }
							case DomainerType.Binary16: { 
							// 			Index.WriteInt16 (#{Entry.IdLabel}.Length); 
							_Output.Write ("			Index.WriteInt16 ({1}.Length);\n{0}", _Indent, Entry.IdLabel);
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast LBinary Cast 
							break; }
							case DomainerType.LBinary: {
							  LBinary Cast = (LBinary) Entry; 
							// 			Index.Write (#{Cast.Length}); 
							_Output.Write ("			Index.Write ({1});\n{0}", _Indent, Cast.Length);
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Hex null 
							break; }
							case DomainerType.Hex: { 
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Hex8 null 
							break; }
							case DomainerType.Hex8: { 
							// 			Index.WriteByte ((byte)#{Entry.IdLabel}.Length); 
							_Output.Write ("			Index.WriteByte ((byte){1}.Length);\n{0}", _Indent, Entry.IdLabel);
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast Hex16 null 
							break; }
							case DomainerType.Hex16: { 
							// 			Index.WriteInt16 (#{Entry.IdLabel}.Length); 
							_Output.Write ("			Index.WriteInt16 ({1}.Length);\n{0}", _Indent, Entry.IdLabel);
							// 			Index.WriteData (#{Entry.IdLabel}); 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							// #casecast OptionList null 
							break; }
							case DomainerType.OptionList: { 
							// 			foreach (DNSOption opt in #{Entry.IdLabel}) { 
							_Output.Write ("			foreach (DNSOption opt in {1}) {{\n{0}", _Indent, Entry.IdLabel);
							// 				Index.WriteInt16 (opt.Code); 
							_Output.Write ("				Index.WriteInt16 (opt.Code);\n{0}", _Indent);
							// 				Index.WriteInt16 (opt.Data.Length); 
							_Output.Write ("				Index.WriteInt16 (opt.Data.Length);\n{0}", _Indent);
							// 				Index.WriteData (opt.Data); 
							_Output.Write ("				Index.WriteData (opt.Data);\n{0}", _Indent);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// #casecast Gateway null 
							break; }
							case DomainerType.Gateway: { 
							// #end switchcast 
						break; }
							}
						// 			//Encode.#{Entry.Tag}  (#{Entry.IdLabel}); 
						_Output.Write ("			//Encode.{1}  ({2});\n{0}", _Indent, Entry.Tag, Entry.IdLabel);
						// #end foreach 
						}
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         // Convert from byte form 
					_Output.Write ("        // Convert from byte form\n{0}", _Indent);
					//         public static  DNSRecord_#{RR.Id} Decode (DNSBufferIndex Index, int Length) { 
					_Output.Write ("        public static  DNSRecord_{1} Decode (DNSBufferIndex Index, int Length) {{\n{0}", _Indent, RR.Id);
					// 			DNSRecord_#{RR.Id} NewRecord = new DNSRecord_#{RR.Id} () ; 
					_Output.Write ("			DNSRecord_{1} NewRecord = new DNSRecord_{2} () ;\n{0}", _Indent, RR.Id, RR.Id);
					// 			NewRecord.Start = Index.Pointer; 
					_Output.Write ("			NewRecord.Start = Index.Pointer;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (_Choice Entry in RR.Entries) 
					foreach  (_Choice Entry in RR.Entries) {
						// #switchcast DomainerType Entry 
						switch (Entry._Tag ()) {
							// #casecast Binary null 
							case DomainerType.Binary: { 
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadData (Length - (Index.Pointer - NewRecord.Start)); 
							_Output.Write ("			NewRecord.{1} = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));\n{0}", _Indent, Entry.IdLabel);
							// #casecast Binary8 null 
							break; }
							case DomainerType.Binary8: { 
							// 			{ 
							_Output.Write ("			{{\n{0}", _Indent);
							// 			int FieldLength = Index.ReadByte (); 
							_Output.Write ("			int FieldLength = Index.ReadByte ();\n{0}", _Indent);
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadData (FieldLength); 
							_Output.Write ("			NewRecord.{1} = Index.ReadData (FieldLength);\n{0}", _Indent, Entry.IdLabel);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// #casecast Binary16 null 
							break; }
							case DomainerType.Binary16: { 
							// 			{ 
							_Output.Write ("			{{\n{0}", _Indent);
							// 			int FieldLength = Index.ReadInt16 (); 
							_Output.Write ("			int FieldLength = Index.ReadInt16 ();\n{0}", _Indent);
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadData (FieldLength); 
							_Output.Write ("			NewRecord.{1} = Index.ReadData (FieldLength);\n{0}", _Indent, Entry.IdLabel);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// #casecast LBinary LBinary 
							break; }
							case DomainerType.LBinary: {
							  LBinary LBinary = (LBinary) Entry; 
							// 			// Binary - length specified by #{LBinary.Length} 
							_Output.Write ("			// Binary - length specified by {1}\n{0}", _Indent, LBinary.Length);
							// #casecast Hex null 
							break; }
							case DomainerType.Hex: { 
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadData (Length - (Index.Pointer - NewRecord.Start)); 
							_Output.Write ("			NewRecord.{1} = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));\n{0}", _Indent, Entry.IdLabel);
							// #casecast Hex8 null 
							break; }
							case DomainerType.Hex8: { 
							// 			{ 
							_Output.Write ("			{{\n{0}", _Indent);
							// 			int FieldLength = Index.ReadByte (); 
							_Output.Write ("			int FieldLength = Index.ReadByte ();\n{0}", _Indent);
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadData (FieldLength); 
							_Output.Write ("			NewRecord.{1} = Index.ReadData (FieldLength);\n{0}", _Indent, Entry.IdLabel);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// #casecast Hex16 null 
							break; }
							case DomainerType.Hex16: { 
							// 			{ 
							_Output.Write ("			{{\n{0}", _Indent);
							// 			int FieldLength = Index.ReadInt16 (); 
							_Output.Write ("			int FieldLength = Index.ReadInt16 ();\n{0}", _Indent);
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadData (FieldLength); 
							_Output.Write ("			NewRecord.{1} = Index.ReadData (FieldLength);\n{0}", _Indent, Entry.IdLabel);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// #casecast Strings null 
							break; }
							case DomainerType.Strings: { 
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadStrings (Length - (Index.Pointer - NewRecord.Start)) ; 
							_Output.Write ("			NewRecord.{1} = Index.ReadStrings (Length - (Index.Pointer - NewRecord.Start)) ;\n{0}", _Indent, Entry.IdLabel);
							// #casecast OptionalString null 
							break; }
							case DomainerType.OptionalString: { 
							// 			// If there is space left in the record, the optional string will be represented as 
							_Output.Write ("			// If there is space left in the record, the optional string will be represented as\n{0}", _Indent);
							// 			// an octet followed by the string data 
							_Output.Write ("			// an octet followed by the string data\n{0}", _Indent);
							// 			if (Length - (Index.Pointer - NewRecord.Start) > 0) { 
							_Output.Write ("			if (Length - (Index.Pointer - NewRecord.Start) > 0) {{\n{0}", _Indent);
							// 				NewRecord.#{Entry.IdLabel} = Index.ReadString (); 
							_Output.Write ("				NewRecord.{1} = Index.ReadString ();\n{0}", _Indent, Entry.IdLabel);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// 			else { 
							_Output.Write ("			else {{\n{0}", _Indent);
							// 				NewRecord.#{Entry.IdLabel} = null; 
							_Output.Write ("				NewRecord.{1} = null;\n{0}", _Indent, Entry.IdLabel);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// #casecast StringX null 
							break; }
							case DomainerType.StringX: { 
							// 			NewRecord.#{Entry.IdLabel} = Index.ReadString (Length - (Index.Pointer - NewRecord.Start)); 
							_Output.Write ("			NewRecord.{1} = Index.ReadString (Length - (Index.Pointer - NewRecord.Start));\n{0}", _Indent, Entry.IdLabel);
							// #% break; } default: { 
							
							 break; } default: {
							// #if (Entry.Tag != null) 
							if (  (Entry.Tag != null) ) {
								// 			NewRecord.#{Entry.IdLabel} = Index.Read#{Entry.Tag} (); 
								_Output.Write ("			NewRecord.{1} = Index.Read{2} ();\n{0}", _Indent, Entry.IdLabel, Entry.Tag);
								// #end if 
								}
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// 			 
					_Output.Write ("			\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			return NewRecord; 
					_Output.Write ("			return NewRecord;\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	} 
			_Output.Write ("	}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		// #end pclass 
		}
	}
