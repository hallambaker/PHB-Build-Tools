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
namespace Goedel.Tool.Domainer {
	/// <summary>A Goedel script.</summary>
	public partial class Generate : global::Goedel.Registry.Script {
		/// <summary>Default constructor.</summary>
		public Generate () : base () {
			}
		/// <summary>Constructor with output stream.</summary>
		/// <param name="Output">The output stream</param>
		public Generate (TextWriter Output) : base (Output) {
			}

		

		//
		// GenerateCS
		//
		public void GenerateCS (Domainer Domainer) {
			_Output.Write ("using System.Net;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("namespace Goedel.Discovery {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <summary>DNS management interface class.</summary>	\n{0}", _Indent);
			_Output.Write ("	public partial class DNS {{\n{0}", _Indent);
			_Output.Write ("		// Dictionary of Type names to codes\n{0}", _Indent);
			_Output.Write ("		//\n{0}", _Indent);
			_Output.Write ("		// if (DictionaryType.ContainsKey(\"RR\") {{\n{0}", _Indent);
			_Output.Write ("		//    int value = dictionary[\"RR\"];\n{0}", _Indent);
			_Output.Write ("		//    }}\n{0}", _Indent);
			_Output.Write ("		static Dictionary <string, ushort> DictionaryType = new Dictionary <string, ushort> () {{\n{0}", _Indent);
			foreach  (_Choice Toplevel in Domainer.Top) {
				switch (Toplevel._Tag ()) {
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					_Output.Write ("			{{\"{1}\", {2}}},\n{0}", _Indent, RR.Id, RR.Code);
					break; }
					case DomainerType.Q: {
					  Q Q = (Q) Toplevel; 
					_Output.Write ("			{{\"{1}\", {2}}},\n{0}", _Indent, Q.Id, Q.Code);
				break; }
					}
				}
			_Output.Write ("			{{\"*\", 255}} // End of list * = ALL\n{0}", _Indent);
			_Output.Write ("			}} ;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		static Dictionary <ushort, string> DictionaryCode = new Dictionary <ushort, string> () {{\n{0}", _Indent);
			foreach  (_Choice Toplevel in Domainer.Top) {
				switch (Toplevel._Tag ()) {
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					_Output.Write ("			{{{1}, \"{2}\"}},\n{0}", _Indent, RR.Code, RR.Id);
					break; }
					case DomainerType.Q: {
					  Q Q = (Q) Toplevel; 
					_Output.Write ("			{{{1}, \"{2}\"}},\n{0}", _Indent, Q.Code, Q.Id);
				break; }
					}
				}
			_Output.Write ("			{{0, \"\"}} // End of list * = ALL\n{0}", _Indent);
			_Output.Write ("			}} ;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>Convert RR text code to type code.</summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Tag\">DNS text code</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>Type code</returns>\n{0}", _Indent);
			_Output.Write ("        public static DNSTypeCode TypeCode(string Tag) {{\n{0}", _Indent);
			_Output.Write ("            if (Tag != null) {{\n{0}", _Indent);
			_Output.Write ("                return (DNSTypeCode) DictionaryType[Tag];\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            return 0;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>Convert RR type code to text code.</summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Code\">Type code</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>DNS text code</returns>\n{0}", _Indent);
			_Output.Write ("        public static string TypeCode(int Code ) {{\n{0}", _Indent);
			_Output.Write ("            return DictionaryCode[(ushort)Code];\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>DNT Type codes</summary>\n{0}", _Indent);
			_Output.Write ("	public enum DNSTypeCode : ushort {{\n{0}", _Indent);
			foreach  (_Choice Toplevel in Domainer.Top) {
				switch (Toplevel._Tag ()) {
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					_Output.Write ("		/// <summary>Resource record {1} = {2}, {3}</summary>\n{0}", _Indent, RR.Id, RR.Code, RR.Description);
					_Output.Write ("		{1} = {2},\n{0}", _Indent, RR.Id, RR.Code);
					break; }
					case DomainerType.Q: {
					  Q Q = (Q) Toplevel; 
					_Output.Write ("		/// <summary>Query type {1} = {2}</summary>\n{0}", _Indent, Q.Id, Q.Code);
					_Output.Write ("		{1} = {2},  // Used in Queries only\n{0}", _Indent, Q.Id, Q.Code);
					break; }
					case DomainerType.IG: {
					  IG IG = (IG) Toplevel; 
					_Output.Write ("		/// <summary>Deprecated type {1} = {2}</summary>\n{0}", _Indent, IG.Id, IG.Code);
					_Output.Write ("		{1} = {2},  // Deprecated, NOT IMPLEMENTED\n{0}", _Indent, IG.Id, IG.Code);
				break; }
					}
				}
			_Output.Write ("		/// Unknown record type.\n{0}", _Indent);
			_Output.Write ("		Unknown = 0\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// All resource record classes are descended from DNSRR\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	public abstract partial  class DNSRecord {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>The type code</summary>\n{0}", _Indent);
			_Output.Write ("		public virtual DNSTypeCode			Code => (0);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>The type text</summary>		\n{0}", _Indent);
			_Output.Write ("		public virtual string	Label => (\"Unknown\");\n{0}", _Indent);
			_Output.Write ("		\n{0}", _Indent);
			_Output.Write ("		/// <summary>Description</summary>\n{0}", _Indent);
			_Output.Write ("		public virtual string	Description=> (\"Record is not defined\");\n{0}", _Indent);
			_Output.Write ("		\n{0}", _Indent);
			_Output.Write ("		/// <summary>Decode record or query from buffer</summary>	\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Index\">Input data</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>Parsed record.</returns>\n{0}", _Indent);
			_Output.Write ("        public static DNSRecord Decode(DNSBufferIndex Index) {{\n{0}", _Indent);
			_Output.Write ("			DNSRecord			DNSRecord;\n{0}", _Indent);
			_Output.Write ("			\n{0}", _Indent);
			_Output.Write ("			Domain				Domain;\n{0}", _Indent);
			_Output.Write ("			DNSTypeCode			RType;\n{0}", _Indent);
			_Output.Write ("			DNSClass			RClass;\n{0}", _Indent);
			_Output.Write ("			uint				TTL;\n{0}", _Indent);
			_Output.Write ("			int				RDLength;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            Domain = Index.ReadDomain ();\n{0}", _Indent);
			_Output.Write ("            RType = (DNSTypeCode)Index.ReadInt16 ();\n{0}", _Indent);
			_Output.Write ("            RClass = (DNSClass)Index.ReadInt16 ();\n{0}", _Indent);
			_Output.Write ("            TTL = Index.ReadInt32 ();\n{0}", _Indent);
			_Output.Write ("			RDLength = Index.ReadInt16 ();\n{0}", _Indent);
			_Output.Write ("			int NextRecord = Index.Pointer + RDLength;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            //Index.ReadL16Data (out RData);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			switch ((int) RType) {{\n{0}", _Indent);
			foreach  (_Choice Toplevel in Domainer.Top) {
				switch (Toplevel._Tag ()) {
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					_Output.Write ("				case ({1}) : {{\n{0}", _Indent, RR.Code);
					_Output.Write ("					DNSRecord = DNSRecord_{1}.Decode (Index, RDLength);\n{0}", _Indent, RR.Id);
					_Output.Write ("					break;\n{0}", _Indent);
					_Output.Write ("					}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("				default : {{\n{0}", _Indent);
			_Output.Write ("					DNSRecord = DNSRecord_Unknown.Decode (Index, RDLength) ;\n{0}", _Indent);
			_Output.Write ("					break;\n{0}", _Indent);
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			DNSRecord.Domain = Domain;\n{0}", _Indent);
			_Output.Write ("			DNSRecord.RType = RType;\n{0}", _Indent);
			_Output.Write ("			DNSRecord.RClass = RClass;\n{0}", _Indent);
			_Output.Write ("			DNSRecord.TTL = TTL;\n{0}", _Indent);
			_Output.Write ("			Index.Pointer = NextRecord;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            return DNSRecord;\n{0}", _Indent);
			_Output.Write ("            }}				\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>Dispatch parser to parse text representation of specific DNS record</summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Tag\">Record tag</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Parse\">Parser</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>Parsed record</returns>\n{0}", _Indent);
			_Output.Write ("		public static DNSRecord Parse(string Tag, Parse Parse) {{\n{0}", _Indent);
			_Output.Write ("			switch (Tag) {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Toplevel in Domainer.Top) {
				switch (Toplevel._Tag ()) {
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					_Output.Write ("				case (\"{1}\") : {{\n{0}", _Indent, RR.Id);
					_Output.Write ("					return DNSRecord_{1}.Parse (Parse);\n{0}", _Indent, RR.Id);
					_Output.Write ("					}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("				default : {{\n{0}", _Indent);
			_Output.Write ("					return null;\n{0}", _Indent);
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Toplevel in Domainer.Top) {
				switch (Toplevel._Tag ()) {
					case DomainerType.RR: {
					  RR RR = (RR) Toplevel; 
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	/// <summary> {1} {2} {3} see {4}</summary>\n{0}", _Indent, RR.Id, RR.Code, RR.Description, RR.Reference);
					_Output.Write ("	public class DNSRecord_{1} : DNSRecord {{\n{0}", _Indent, RR.Id);
					_Output.Write ("\n{0}", _Indent);
					foreach  (_Choice Entry in RR.Entries) {
						if (  (Entry.TypeCS != null) ) {
							_Output.Write ("		/// <summary>{1}</summary>\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("		public {1}   {2}  ;\n{0}", _Indent, Entry.TypeCS, Entry.IdLabel);
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>The type code</summary>\n{0}", _Indent);
					_Output.Write ("		public override DNSTypeCode		Code => (DNSTypeCode.{1});	\n{0}", _Indent, RR.Id);
					_Output.Write ("		\n{0}", _Indent);
					_Output.Write ("		/// <summary>The type text</summary>\n{0}", _Indent);
					_Output.Write ("		public override string	Label => (\"{1}\");\n{0}", _Indent, RR.Id);
					_Output.Write ("			\n{0}", _Indent);
					_Output.Write ("		/// <summary>Description</summary>	\n{0}", _Indent);
					_Output.Write ("		public override string	Description => (\"{1}\");\n{0}", _Indent, RR.Description);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>Convert to canonical form</summary>\n{0}", _Indent);
					_Output.Write ("        /// <returns>Canonical form of record data contents</returns>\n{0}", _Indent);
					_Output.Write ("        public override string Canonical () {{\n{0}", _Indent);
					_Output.Write ("			Canonicalize Canonicalize = new Canonicalize (\"{1}\", Domain);\n{0}", _Indent, RR.Id);
					foreach  (_Choice Entry in RR.Entries) {
						if (  (Entry.Tag != null) ) {
							_Output.Write ("			Canonicalize.{1}  ({2});\n{0}", _Indent, Entry.Tag, Entry.IdLabel);
							}
						}
					_Output.Write ("			return Canonicalize.Text;\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>Parse record or query from string</summary>	\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"Parse\">Input data</param>\n{0}", _Indent);
					_Output.Write ("        /// <returns>Parsed record.</returns>\n{0}", _Indent);
					_Output.Write ("        public static DNSRecord_{1} Parse(Parse Parse) {{\n{0}", _Indent, RR.Id);
					_Output.Write ("			DNSRecord_{1} NewRecord = new DNSRecord_{2} () {{\n{0}", _Indent, RR.Id, RR.Id);
					foreach  (_Choice Entry in RR.Entries) {
						if (  (Entry.Tag != null) ) {
							_Output.Write ("			    {1} = Parse.{2}  (),\n{0}", _Indent, Entry.IdLabel, Entry.Tag);
							}
						}
					_Output.Write ("				}};\n{0}", _Indent);
					_Output.Write ("			return NewRecord;\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>Convert to wire form</summary>\n{0}", _Indent);
					_Output.Write ("		/// <param name=\"Index\">Output buffer</param>\n{0}", _Indent);
					_Output.Write ("        /// <returns>Canonical form of record data contents</returns>\n{0}", _Indent);
					_Output.Write ("        public override void Encode(DNSBufferIndex Index) {{\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (_Choice Entry in RR.Entries) {
						switch (Entry._Tag ()) {
							case DomainerType.IPv4: { 
							_Output.Write ("			Index.WriteIPv4 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.IPv6: { 
							_Output.Write ("			Index.WriteIPv6 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Domain: { 
							_Output.Write ("			Index.WriteDomain ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Mail: { 
							_Output.Write ("			Index.WriteMail ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.NodeID: { 
							_Output.Write ("			Index.WriteInt64 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Byte: { 
							_Output.Write ("			Index.WriteByte ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Int16: { 
							_Output.Write ("			Index.WriteInt16 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Int32: { 
							_Output.Write ("			Index.WriteInt32 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Time32: { 
							_Output.Write ("			Index.WriteInt32 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Time48: { 
							_Output.Write ("			Index.WriteInt48 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.String: { 
							_Output.Write ("			Index.WriteString8 ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.OptionalString: { 
							_Output.Write ("			if ({1} != null) {{\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("				Index.WriteString8 ({1});\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("				}}\n{0}", _Indent);
							break; }
							case DomainerType.Strings: { 
							_Output.Write ("			foreach (string s in {1}) {{\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("				Index.WriteString8 (s);\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
							break; }
							case DomainerType.StringX: { 
							_Output.Write ("			Index.WriteString ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Binary: { 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Binary8: { 
							_Output.Write ("			Index.WriteByte ((byte){1}.Length);\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Binary16: { 
							_Output.Write ("			Index.WriteInt16 ({1}.Length);\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.LBinary: {
							  LBinary Cast = (LBinary) Entry; 
							_Output.Write ("			Index.Write ({1});\n{0}", _Indent, Cast.Length);
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Hex: { 
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Hex8: { 
							_Output.Write ("			Index.WriteByte ((byte){1}.Length);\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Hex16: { 
							_Output.Write ("			Index.WriteInt16 ({1}.Length);\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("			Index.WriteData ({1});\n{0}", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.OptionList: { 
							_Output.Write ("			foreach (DNSOption opt in {1}) {{\n{0}", _Indent, Entry.IdLabel);
							_Output.Write ("				Index.WriteInt16 (opt.Code);\n{0}", _Indent);
							_Output.Write ("				Index.WriteInt16 (opt.Data.Length);\n{0}", _Indent);
							_Output.Write ("				Index.WriteData (opt.Data);\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
							break; }
							case DomainerType.Gateway: { 
						break; }
							}
						_Output.Write ("\n{0}", _Indent);
						}
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>Decode record or query from byte form buffer</summary>	\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"Index\">Input data</param>\n{0}", _Indent);
					_Output.Write ("		/// <param name=\"Length\">Maximum amount of data to read</param>\n{0}", _Indent);
					_Output.Write ("        /// <returns>Parsed record.</returns>\n{0}", _Indent);
					_Output.Write ("        public static  DNSRecord_{1} Decode (DNSBufferIndex Index, int Length) {{\n{0}", _Indent, RR.Id);
					_Output.Write ("			DNSRecord_{1} NewRecord = new DNSRecord_{2} ()  {{\n{0}", _Indent, RR.Id, RR.Id);
					_Output.Write ("				Start = Index.Start", _Indent);
					foreach  (_Choice Entry in RR.Entries) {
						switch (Entry._Tag ()) {
							case DomainerType.Binary: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadData (Index.Remainder(Length))", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Binary8: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadData (Index.ReadByte ())", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Binary16: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadData (Index.ReadInt16 ())", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.LBinary: {
							  LBinary LBinary = (LBinary) Entry; 
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("				// Binary - length specified by {1}", _Indent, LBinary.Length);
							break; }
							case DomainerType.Hex: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadData (Index.Remainder(Length))", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Hex8: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadData (Index.ReadByte ())", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Hex16: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadData (Index.ReadInt16 ())", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.Strings: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadStrings (Index.Remainder(Length)) ", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.OptionalString: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = (Index.Remainder(Length) > 0) ? Index.ReadString () : null", _Indent, Entry.IdLabel);
							break; }
							case DomainerType.StringX: { 
							_Output.Write (",\n{0}", _Indent);
							_Output.Write ("				{1} = Index.ReadString (Index.Remainder(Length))", _Indent, Entry.IdLabel);
							
							 break; } default: {
							if (  (Entry.Tag != null) ) {
								_Output.Write (",\n{0}", _Indent);
								_Output.Write ("				{1} = Index.Read{2} ()", _Indent, Entry.IdLabel, Entry.Tag);
								}
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("				}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			return NewRecord;\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		}
	}
