using System.Net;
using System.Collections.Generic;
namespace Goedel.Platform {

    /// <summary>DNS management interface class.</summary>	
	public partial class DNS {
		// Dictionary of Type names to codes
		//
		// if (DictionaryType.ContainsKey("RR") {
		//    int value = dictionary["RR"];
		//    }
		static Dictionary <string, ushort> DictionaryType = new Dictionary <string, ushort> () {
			{"A", 1},
			{"NS", 2},
			{"MD", 3},
			{"MF", 4},
			{"CNAME", 5},
			{"SOA", 6},
			{"MB", 7},
			{"MG", 8},
			{"MR", 9},
			{"NULL", 10},
			{"WKS", 11},
			{"PTR", 12},
			{"HINFO", 13},
			{"MINFO", 14},
			{"MX", 15},
			{"TXT", 16},
			{"RP", 17},
			{"AFSDB", 18},
			{"X25", 19},
			{"ISDN", 20},
			{"RT", 21},
			{"SIG", 24},
			{"KEY", 25},
			{"AAAA", 28},
			{"SRV", 33},
			{"NAPTR", 35},
			{"KX", 36},
			{"CERT", 37},
			{"DNAME", 39},
			{"OPT", 41},
			{"DS", 43},
			{"SSHFP", 44},
			{"IPSECKEY", 45},
			{"RRSIG", 46},
			{"NSEC", 47},
			{"DNSKEY", 48},
			{"DHCID", 49},
			{"NSEC3", 50},
			{"NSEC3PARAM", 51},
			{"TLSA", 52},
			{"HIP", 55},
			{"SPF", 99},
			{"NID", 104},
			{"L32", 105},
			{"L64", 106},
			{"LP", 107},
			{"TKEY", 249},
			{"TSIG", 250},
			{"IXFR", 251},
			{"AXFR", 252},
			{"MAILB", 253},
			{"MAILA", 254},
			{"ALL", 255},
			{"URI", 256},
			{"CAA", 257},
			{"TA", 32768},
			{"DLV", 32769},
			{"*", 255} // End of list * = ALL
			} ;

		static Dictionary <ushort, string> DictionaryCode = new Dictionary <ushort, string> () {
			{1, "A"},
			{2, "NS"},
			{3, "MD"},
			{4, "MF"},
			{5, "CNAME"},
			{6, "SOA"},
			{7, "MB"},
			{8, "MG"},
			{9, "MR"},
			{10, "NULL"},
			{11, "WKS"},
			{12, "PTR"},
			{13, "HINFO"},
			{14, "MINFO"},
			{15, "MX"},
			{16, "TXT"},
			{17, "RP"},
			{18, "AFSDB"},
			{19, "X25"},
			{20, "ISDN"},
			{21, "RT"},
			{24, "SIG"},
			{25, "KEY"},
			{28, "AAAA"},
			{33, "SRV"},
			{35, "NAPTR"},
			{36, "KX"},
			{37, "CERT"},
			{39, "DNAME"},
			{41, "OPT"},
			{43, "DS"},
			{44, "SSHFP"},
			{45, "IPSECKEY"},
			{46, "RRSIG"},
			{47, "NSEC"},
			{48, "DNSKEY"},
			{49, "DHCID"},
			{50, "NSEC3"},
			{51, "NSEC3PARAM"},
			{52, "TLSA"},
			{55, "HIP"},
			{99, "SPF"},
			{104, "NID"},
			{105, "L32"},
			{106, "L64"},
			{107, "LP"},
			{249, "TKEY"},
			{250, "TSIG"},
			{251, "IXFR"},
			{252, "AXFR"},
			{253, "MAILB"},
			{254, "MAILA"},
			{255, "ALL"},
			{256, "URI"},
			{257, "CAA"},
			{32768, "TA"},
			{32769, "DLV"},
			{0, ""} // End of list * = ALL
			} ;

		/// <summary>Convert RR text code to type code.</summary>
        /// <param name="Tag">DNS text code</param>
        /// <returns>Type code</returns>
        public static DNSTypeCode TypeCode(string Tag) {
            if (Tag != null) {
                return (DNSTypeCode) DictionaryType[Tag];
                }
            return 0;
            }

		/// <summary>Convert RR type code to text code.</summary>
        /// <param name="Code">Type code</param>
        /// <returns>DNS text code</returns>
        public static string TypeCode(int Code ) {
            return DictionaryCode[(ushort)Code];
            }
		}

	/// <summary>DNT Type codes</summary>
	public enum DNSTypeCode : ushort {
		/// <summary>Resource record A = 1, Host address</summary>
		A = 1,
		/// <summary>Resource record NS = 2, Authoritative name server</summary>
		NS = 2,
		/// <summary>Resource record MD = 3, Mail destination</summary>
		MD = 3,
		/// <summary>Resource record MF = 4, Mail forwarder</summary>
		MF = 4,
		/// <summary>Resource record CNAME = 5, Canonical name for an alias</summary>
		CNAME = 5,
		/// <summary>Resource record SOA = 6, Start of a zone of authority</summary>
		SOA = 6,
		/// <summary>Resource record MB = 7, Mailbox domain name</summary>
		MB = 7,
		/// <summary>Resource record MG = 8, Mail group member</summary>
		MG = 8,
		/// <summary>Resource record MR = 9, Mail rename domain name</summary>
		MR = 9,
		/// <summary>Resource record NULL = 10, Null RR</summary>
		NULL = 10,
		/// <summary>Resource record WKS = 11, Well known service description</summary>
		WKS = 11,
		/// <summary>Resource record PTR = 12, Domain name pointer</summary>
		PTR = 12,
		/// <summary>Resource record HINFO = 13, Host information</summary>
		HINFO = 13,
		/// <summary>Resource record MINFO = 14, Mailbox or mail list information</summary>
		MINFO = 14,
		/// <summary>Resource record MX = 15, Mail exchange</summary>
		MX = 15,
		/// <summary>Resource record TXT = 16, Text strings</summary>
		TXT = 16,
		/// <summary>Resource record RP = 17, Responsible Person</summary>
		RP = 17,
		/// <summary>Resource record AFSDB = 18, AFS Data Base location</summary>
		AFSDB = 18,
		/// <summary>Resource record X25 = 19, X.25 PSDN address</summary>
		X25 = 19,
		/// <summary>Resource record ISDN = 20, ISDN address</summary>
		ISDN = 20,
		/// <summary>Resource record RT = 21, Route Through</summary>
		RT = 21,
		/// <summary>Deprecated type NSAP = 22</summary>
		NSAP = 22,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type NSAPPTR = 23</summary>
		NSAPPTR = 23,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record SIG = 24, Security signature</summary>
		SIG = 24,
		/// <summary>Resource record KEY = 25, Security key</summary>
		KEY = 25,
		/// <summary>Deprecated type PX = 26</summary>
		PX = 26,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type GPOS = 27</summary>
		GPOS = 27,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record AAAA = 28, IP6 Address</summary>
		AAAA = 28,
		/// <summary>Deprecated type LOC = 29</summary>
		LOC = 29,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type NXT = 30</summary>
		NXT = 30,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type EID = 31</summary>
		EID = 31,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type NIMLOC = 32</summary>
		NIMLOC = 32,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record SRV = 33, Server Selection</summary>
		SRV = 33,
		/// <summary>Deprecated type ATMA = 34</summary>
		ATMA = 34,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record NAPTR = 35, Naming Authority Pointer</summary>
		NAPTR = 35,
		/// <summary>Resource record KX = 36, Key Exchanger</summary>
		KX = 36,
		/// <summary>Resource record CERT = 37, CERT</summary>
		CERT = 37,
		/// <summary>Deprecated type A6 = 38</summary>
		A6 = 38,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record DNAME = 39, DNAME</summary>
		DNAME = 39,
		/// <summary>Deprecated type SINK = 40</summary>
		SINK = 40,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record OPT = 41, OPT</summary>
		OPT = 41,
		/// <summary>Deprecated type APL = 42</summary>
		APL = 42,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record DS = 43, Delegation Signer</summary>
		DS = 43,
		/// <summary>Resource record SSHFP = 44, SSH Key Fingerprint</summary>
		SSHFP = 44,
		/// <summary>Resource record IPSECKEY = 45, IPSECKEY</summary>
		IPSECKEY = 45,
		/// <summary>Resource record RRSIG = 46, RRSIG</summary>
		RRSIG = 46,
		/// <summary>Resource record NSEC = 47, NSEC</summary>
		NSEC = 47,
		/// <summary>Resource record DNSKEY = 48, DNSKEY</summary>
		DNSKEY = 48,
		/// <summary>Resource record DHCID = 49, DHCID</summary>
		DHCID = 49,
		/// <summary>Resource record NSEC3 = 50, NSEC3</summary>
		NSEC3 = 50,
		/// <summary>Resource record NSEC3PARAM = 51, NSEC3PARAM</summary>
		NSEC3PARAM = 51,
		/// <summary>Resource record TLSA = 52, TLSA</summary>
		TLSA = 52,
		/// <summary>Resource record HIP = 55, Host Identity Protocol</summary>
		HIP = 55,
		/// <summary>Deprecated type NINFO = 56</summary>
		NINFO = 56,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type RKEY = 57</summary>
		RKEY = 57,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type TALINK = 58</summary>
		TALINK = 58,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type CDS = 59</summary>
		CDS = 59,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record SPF = 99, Sender Policy Framework</summary>
		SPF = 99,
		/// <summary>Deprecated type UINFO = 100</summary>
		UINFO = 100,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type UID = 101</summary>
		UID = 101,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type GID = 102</summary>
		GID = 102,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Deprecated type UNSPEC = 103</summary>
		UNSPEC = 103,  // Deprecated, NOT IMPLEMENTED
		/// <summary>Resource record NID = 104, </summary>
		NID = 104,
		/// <summary>Resource record L32 = 105, </summary>
		L32 = 105,
		/// <summary>Resource record L64 = 106, </summary>
		L64 = 106,
		/// <summary>Resource record LP = 107, </summary>
		LP = 107,
		/// <summary>Resource record TKEY = 249, Transaction Key</summary>
		TKEY = 249,
		/// <summary>Resource record TSIG = 250, Transaction Signature</summary>
		TSIG = 250,
		/// <summary>Query type IXFR = 251</summary>
		IXFR = 251,  // Used in Queries only
		/// <summary>Query type AXFR = 252</summary>
		AXFR = 252,  // Used in Queries only
		/// <summary>Query type MAILB = 253</summary>
		MAILB = 253,  // Used in Queries only
		/// <summary>Query type MAILA = 254</summary>
		MAILA = 254,  // Used in Queries only
		/// <summary>Query type ALL = 255</summary>
		ALL = 255,  // Used in Queries only
		/// <summary>Resource record URI = 256, URI</summary>
		URI = 256,
		/// <summary>Resource record CAA = 257, Certification Authority Restriction</summary>
		CAA = 257,
		/// <summary>Resource record TA = 32768, DNSSEC Trust Authorities</summary>
		TA = 32768,
		/// <summary>Resource record DLV = 32769, DNSSEC Lookaside Validation</summary>
		DLV = 32769,
		/// Unknown record type.
		Unknown = 0
		}

	// All resource record classes are descended from DNSRR

	public abstract partial  class DNSRecord {

		/// <summary>The type code</summary>
		public virtual DNSTypeCode			Code {
			get {return (0);} }

		/// <summary>The type text</summary>		
		public virtual string	Label {
			get {return ("Unknown");} }	
		
		/// <summary>Description</summary>
		public virtual string	Description {
			get {return ("Record is not defined");} }
		
		/// <summary>Decode record or query from buffer</summary>	
        /// <param name="Index">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord Decode(DNSBufferIndex Index) {
			DNSRecord			DNSRecord;
			
			Domain				Domain;
			DNSTypeCode			RType;
			DNSClass			RClass;
			uint				TTL;
			int				RDLength;

            Domain = Index.ReadDomain ();
            RType = (DNSTypeCode)Index.ReadInt16 ();
            RClass = (DNSClass)Index.ReadInt16 ();
            TTL = Index.ReadInt32 ();
			RDLength = Index.ReadInt16 ();
			int NextRecord = Index.Pointer + RDLength;

            //Index.ReadL16Data (out RData);

			switch ((int) RType) {
				case (1) : {
					DNSRecord = DNSRecord_A.Decode (Index, RDLength);
					break;
					}
				case (2) : {
					DNSRecord = DNSRecord_NS.Decode (Index, RDLength);
					break;
					}
				case (3) : {
					DNSRecord = DNSRecord_MD.Decode (Index, RDLength);
					break;
					}
				case (4) : {
					DNSRecord = DNSRecord_MF.Decode (Index, RDLength);
					break;
					}
				case (5) : {
					DNSRecord = DNSRecord_CNAME.Decode (Index, RDLength);
					break;
					}
				case (6) : {
					DNSRecord = DNSRecord_SOA.Decode (Index, RDLength);
					break;
					}
				case (7) : {
					DNSRecord = DNSRecord_MB.Decode (Index, RDLength);
					break;
					}
				case (8) : {
					DNSRecord = DNSRecord_MG.Decode (Index, RDLength);
					break;
					}
				case (9) : {
					DNSRecord = DNSRecord_MR.Decode (Index, RDLength);
					break;
					}
				case (10) : {
					DNSRecord = DNSRecord_NULL.Decode (Index, RDLength);
					break;
					}
				case (11) : {
					DNSRecord = DNSRecord_WKS.Decode (Index, RDLength);
					break;
					}
				case (12) : {
					DNSRecord = DNSRecord_PTR.Decode (Index, RDLength);
					break;
					}
				case (13) : {
					DNSRecord = DNSRecord_HINFO.Decode (Index, RDLength);
					break;
					}
				case (14) : {
					DNSRecord = DNSRecord_MINFO.Decode (Index, RDLength);
					break;
					}
				case (15) : {
					DNSRecord = DNSRecord_MX.Decode (Index, RDLength);
					break;
					}
				case (16) : {
					DNSRecord = DNSRecord_TXT.Decode (Index, RDLength);
					break;
					}
				case (17) : {
					DNSRecord = DNSRecord_RP.Decode (Index, RDLength);
					break;
					}
				case (18) : {
					DNSRecord = DNSRecord_AFSDB.Decode (Index, RDLength);
					break;
					}
				case (19) : {
					DNSRecord = DNSRecord_X25.Decode (Index, RDLength);
					break;
					}
				case (20) : {
					DNSRecord = DNSRecord_ISDN.Decode (Index, RDLength);
					break;
					}
				case (21) : {
					DNSRecord = DNSRecord_RT.Decode (Index, RDLength);
					break;
					}
				case (24) : {
					DNSRecord = DNSRecord_SIG.Decode (Index, RDLength);
					break;
					}
				case (25) : {
					DNSRecord = DNSRecord_KEY.Decode (Index, RDLength);
					break;
					}
				case (28) : {
					DNSRecord = DNSRecord_AAAA.Decode (Index, RDLength);
					break;
					}
				case (33) : {
					DNSRecord = DNSRecord_SRV.Decode (Index, RDLength);
					break;
					}
				case (35) : {
					DNSRecord = DNSRecord_NAPTR.Decode (Index, RDLength);
					break;
					}
				case (36) : {
					DNSRecord = DNSRecord_KX.Decode (Index, RDLength);
					break;
					}
				case (37) : {
					DNSRecord = DNSRecord_CERT.Decode (Index, RDLength);
					break;
					}
				case (39) : {
					DNSRecord = DNSRecord_DNAME.Decode (Index, RDLength);
					break;
					}
				case (41) : {
					DNSRecord = DNSRecord_OPT.Decode (Index, RDLength);
					break;
					}
				case (43) : {
					DNSRecord = DNSRecord_DS.Decode (Index, RDLength);
					break;
					}
				case (44) : {
					DNSRecord = DNSRecord_SSHFP.Decode (Index, RDLength);
					break;
					}
				case (45) : {
					DNSRecord = DNSRecord_IPSECKEY.Decode (Index, RDLength);
					break;
					}
				case (46) : {
					DNSRecord = DNSRecord_RRSIG.Decode (Index, RDLength);
					break;
					}
				case (47) : {
					DNSRecord = DNSRecord_NSEC.Decode (Index, RDLength);
					break;
					}
				case (48) : {
					DNSRecord = DNSRecord_DNSKEY.Decode (Index, RDLength);
					break;
					}
				case (49) : {
					DNSRecord = DNSRecord_DHCID.Decode (Index, RDLength);
					break;
					}
				case (50) : {
					DNSRecord = DNSRecord_NSEC3.Decode (Index, RDLength);
					break;
					}
				case (51) : {
					DNSRecord = DNSRecord_NSEC3PARAM.Decode (Index, RDLength);
					break;
					}
				case (52) : {
					DNSRecord = DNSRecord_TLSA.Decode (Index, RDLength);
					break;
					}
				case (55) : {
					DNSRecord = DNSRecord_HIP.Decode (Index, RDLength);
					break;
					}
				case (99) : {
					DNSRecord = DNSRecord_SPF.Decode (Index, RDLength);
					break;
					}
				case (104) : {
					DNSRecord = DNSRecord_NID.Decode (Index, RDLength);
					break;
					}
				case (105) : {
					DNSRecord = DNSRecord_L32.Decode (Index, RDLength);
					break;
					}
				case (106) : {
					DNSRecord = DNSRecord_L64.Decode (Index, RDLength);
					break;
					}
				case (107) : {
					DNSRecord = DNSRecord_LP.Decode (Index, RDLength);
					break;
					}
				case (249) : {
					DNSRecord = DNSRecord_TKEY.Decode (Index, RDLength);
					break;
					}
				case (250) : {
					DNSRecord = DNSRecord_TSIG.Decode (Index, RDLength);
					break;
					}
				case (256) : {
					DNSRecord = DNSRecord_URI.Decode (Index, RDLength);
					break;
					}
				case (257) : {
					DNSRecord = DNSRecord_CAA.Decode (Index, RDLength);
					break;
					}
				case (32768) : {
					DNSRecord = DNSRecord_TA.Decode (Index, RDLength);
					break;
					}
				case (32769) : {
					DNSRecord = DNSRecord_DLV.Decode (Index, RDLength);
					break;
					}
				default : {
					DNSRecord = DNSRecord_Unknown.Decode (Index, RDLength) ;
					break;
					}
				}
			DNSRecord.Domain = Domain;
			DNSRecord.RType = RType;
			DNSRecord.RClass = RClass;
			DNSRecord.TTL = TTL;
			Index.Pointer = NextRecord;

            return DNSRecord;
            }				

		/// <summary>Dispatch parser to parse text representation of specific DNS record</summary>
        /// <param name="Tag">Record tag</param>
        /// <param name="Parse">Parser</param>
        /// <returns>Parsed record</returns>
		public static DNSRecord Parse(string Tag, Parse Parse) {
			switch (Tag) {

				case ("A") : {
					return DNSRecord_A.Parse (Parse);
					}
				case ("NS") : {
					return DNSRecord_NS.Parse (Parse);
					}
				case ("MD") : {
					return DNSRecord_MD.Parse (Parse);
					}
				case ("MF") : {
					return DNSRecord_MF.Parse (Parse);
					}
				case ("CNAME") : {
					return DNSRecord_CNAME.Parse (Parse);
					}
				case ("SOA") : {
					return DNSRecord_SOA.Parse (Parse);
					}
				case ("MB") : {
					return DNSRecord_MB.Parse (Parse);
					}
				case ("MG") : {
					return DNSRecord_MG.Parse (Parse);
					}
				case ("MR") : {
					return DNSRecord_MR.Parse (Parse);
					}
				case ("NULL") : {
					return DNSRecord_NULL.Parse (Parse);
					}
				case ("WKS") : {
					return DNSRecord_WKS.Parse (Parse);
					}
				case ("PTR") : {
					return DNSRecord_PTR.Parse (Parse);
					}
				case ("HINFO") : {
					return DNSRecord_HINFO.Parse (Parse);
					}
				case ("MINFO") : {
					return DNSRecord_MINFO.Parse (Parse);
					}
				case ("MX") : {
					return DNSRecord_MX.Parse (Parse);
					}
				case ("TXT") : {
					return DNSRecord_TXT.Parse (Parse);
					}
				case ("RP") : {
					return DNSRecord_RP.Parse (Parse);
					}
				case ("AFSDB") : {
					return DNSRecord_AFSDB.Parse (Parse);
					}
				case ("X25") : {
					return DNSRecord_X25.Parse (Parse);
					}
				case ("ISDN") : {
					return DNSRecord_ISDN.Parse (Parse);
					}
				case ("RT") : {
					return DNSRecord_RT.Parse (Parse);
					}
				case ("SIG") : {
					return DNSRecord_SIG.Parse (Parse);
					}
				case ("KEY") : {
					return DNSRecord_KEY.Parse (Parse);
					}
				case ("AAAA") : {
					return DNSRecord_AAAA.Parse (Parse);
					}
				case ("SRV") : {
					return DNSRecord_SRV.Parse (Parse);
					}
				case ("NAPTR") : {
					return DNSRecord_NAPTR.Parse (Parse);
					}
				case ("KX") : {
					return DNSRecord_KX.Parse (Parse);
					}
				case ("CERT") : {
					return DNSRecord_CERT.Parse (Parse);
					}
				case ("DNAME") : {
					return DNSRecord_DNAME.Parse (Parse);
					}
				case ("OPT") : {
					return DNSRecord_OPT.Parse (Parse);
					}
				case ("DS") : {
					return DNSRecord_DS.Parse (Parse);
					}
				case ("SSHFP") : {
					return DNSRecord_SSHFP.Parse (Parse);
					}
				case ("IPSECKEY") : {
					return DNSRecord_IPSECKEY.Parse (Parse);
					}
				case ("RRSIG") : {
					return DNSRecord_RRSIG.Parse (Parse);
					}
				case ("NSEC") : {
					return DNSRecord_NSEC.Parse (Parse);
					}
				case ("DNSKEY") : {
					return DNSRecord_DNSKEY.Parse (Parse);
					}
				case ("DHCID") : {
					return DNSRecord_DHCID.Parse (Parse);
					}
				case ("NSEC3") : {
					return DNSRecord_NSEC3.Parse (Parse);
					}
				case ("NSEC3PARAM") : {
					return DNSRecord_NSEC3PARAM.Parse (Parse);
					}
				case ("TLSA") : {
					return DNSRecord_TLSA.Parse (Parse);
					}
				case ("HIP") : {
					return DNSRecord_HIP.Parse (Parse);
					}
				case ("SPF") : {
					return DNSRecord_SPF.Parse (Parse);
					}
				case ("NID") : {
					return DNSRecord_NID.Parse (Parse);
					}
				case ("L32") : {
					return DNSRecord_L32.Parse (Parse);
					}
				case ("L64") : {
					return DNSRecord_L64.Parse (Parse);
					}
				case ("LP") : {
					return DNSRecord_LP.Parse (Parse);
					}
				case ("TKEY") : {
					return DNSRecord_TKEY.Parse (Parse);
					}
				case ("TSIG") : {
					return DNSRecord_TSIG.Parse (Parse);
					}
				case ("URI") : {
					return DNSRecord_URI.Parse (Parse);
					}
				case ("CAA") : {
					return DNSRecord_CAA.Parse (Parse);
					}
				case ("TA") : {
					return DNSRecord_TA.Parse (Parse);
					}
				case ("DLV") : {
					return DNSRecord_DLV.Parse (Parse);
					}

				default : {
					return null;
					}
				}
			}

		}





	/// <summary> A 1 Host address see RFC1035</summary>
	public class DNSRecord_A : DNSRecord {

		/// <summary>Address</summary>
		public IPAddress   Address  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.A);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("A");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Host address");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("A", Domain);
			Canonicalize.IPv4  (Address);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_A Parse(Parse Parse) {
			DNSRecord_A NewRecord = new DNSRecord_A () ;
			
			NewRecord.Address = Parse.IPv4  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteIPv4 (Address);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_A Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_A NewRecord = new DNSRecord_A () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Address = Index.ReadIPv4 ();
			

			return NewRecord;
            }

		}

	/// <summary> NS 2 Authoritative name server see RFC1035</summary>
	public class DNSRecord_NS : DNSRecord {

		/// <summary>NSDNAME</summary>
		public Domain   NSDNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NS);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NS");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Authoritative name server");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NS", Domain);
			Canonicalize.Domain  (NSDNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NS Parse(Parse Parse) {
			DNSRecord_NS NewRecord = new DNSRecord_NS () ;
			
			NewRecord.NSDNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (NSDNAME);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NS Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NS NewRecord = new DNSRecord_NS () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.NSDNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> MD 3 Mail destination see RFC1035</summary>
	public class DNSRecord_MD : DNSRecord {

		/// <summary>MADNAME</summary>
		public Domain   MADNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MD);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MD");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mail destination");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MD", Domain);
			Canonicalize.Domain  (MADNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MD Parse(Parse Parse) {
			DNSRecord_MD NewRecord = new DNSRecord_MD () ;
			
			NewRecord.MADNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (MADNAME);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MD Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MD NewRecord = new DNSRecord_MD () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.MADNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> MF 4 Mail forwarder see RFC1035</summary>
	public class DNSRecord_MF : DNSRecord {

		/// <summary>MADNAME</summary>
		public Domain   MADNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MF);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MF");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mail forwarder");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MF", Domain);
			Canonicalize.Domain  (MADNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MF Parse(Parse Parse) {
			DNSRecord_MF NewRecord = new DNSRecord_MF () ;
			
			NewRecord.MADNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (MADNAME);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MF Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MF NewRecord = new DNSRecord_MF () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.MADNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> CNAME 5 Canonical name for an alias see RFC1035</summary>
	public class DNSRecord_CNAME : DNSRecord {

		/// <summary>CNAME</summary>
		public Domain   CNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.CNAME);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("CNAME");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Canonical name for an alias");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("CNAME", Domain);
			Canonicalize.Domain  (CNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_CNAME Parse(Parse Parse) {
			DNSRecord_CNAME NewRecord = new DNSRecord_CNAME () ;
			
			NewRecord.CNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (CNAME);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_CNAME Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_CNAME NewRecord = new DNSRecord_CNAME () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.CNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> SOA 6 Start of a zone of authority see RFC1035</summary>
	public class DNSRecord_SOA : DNSRecord {

		/// <summary>MNAME</summary>
		public Domain   MNAME  ;
		/// <summary>RNAME</summary>
		public Domain   RNAME  ;
		/// <summary>SERIAL</summary>
		public uint   SERIAL  ;
		/// <summary>REFRESH</summary>
		public uint   REFRESH  ;
		/// <summary>RETRY</summary>
		public uint   RETRY  ;
		/// <summary>EXPIRE</summary>
		public uint   EXPIRE  ;
		/// <summary>MINIMUM</summary>
		public uint   MINIMUM  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.SOA);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("SOA");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Start of a zone of authority");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("SOA", Domain);
			Canonicalize.Domain  (MNAME);
			Canonicalize.Domain  (RNAME);
			Canonicalize.Int32  (SERIAL);
			Canonicalize.Int32  (REFRESH);
			Canonicalize.Int32  (RETRY);
			Canonicalize.Int32  (EXPIRE);
			Canonicalize.Int32  (MINIMUM);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_SOA Parse(Parse Parse) {
			DNSRecord_SOA NewRecord = new DNSRecord_SOA () ;
			
			NewRecord.MNAME = Parse.Domain  ();
			NewRecord.RNAME = Parse.Domain  ();
			NewRecord.SERIAL = Parse.Int32  ();
			NewRecord.REFRESH = Parse.Int32  ();
			NewRecord.RETRY = Parse.Int32  ();
			NewRecord.EXPIRE = Parse.Int32  ();
			NewRecord.MINIMUM = Parse.Int32  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (MNAME);

			Index.WriteDomain (RNAME);

			Index.WriteInt32 (SERIAL);

			Index.WriteInt32 (REFRESH);

			Index.WriteInt32 (RETRY);

			Index.WriteInt32 (EXPIRE);

			Index.WriteInt32 (MINIMUM);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_SOA Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_SOA NewRecord = new DNSRecord_SOA () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.MNAME = Index.ReadDomain ();
			NewRecord.RNAME = Index.ReadDomain ();
			NewRecord.SERIAL = Index.ReadInt32 ();
			NewRecord.REFRESH = Index.ReadInt32 ();
			NewRecord.RETRY = Index.ReadInt32 ();
			NewRecord.EXPIRE = Index.ReadInt32 ();
			NewRecord.MINIMUM = Index.ReadInt32 ();
			

			return NewRecord;
            }

		}

	/// <summary> MB 7 Mailbox domain name see RFC1035</summary>
	public class DNSRecord_MB : DNSRecord {

		/// <summary>MadName</summary>
		public Domain   MadName  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MB);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MB");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mailbox domain name");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MB", Domain);
			Canonicalize.Domain  (MadName);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MB Parse(Parse Parse) {
			DNSRecord_MB NewRecord = new DNSRecord_MB () ;
			
			NewRecord.MadName = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (MadName);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MB Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MB NewRecord = new DNSRecord_MB () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.MadName = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> MG 8 Mail group member see RFC1035</summary>
	public class DNSRecord_MG : DNSRecord {

		/// <summary>MGMNAME</summary>
		public Domain   MGMNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MG);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MG");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mail group member");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MG", Domain);
			Canonicalize.Domain  (MGMNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MG Parse(Parse Parse) {
			DNSRecord_MG NewRecord = new DNSRecord_MG () ;
			
			NewRecord.MGMNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (MGMNAME);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MG Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MG NewRecord = new DNSRecord_MG () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.MGMNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> MR 9 Mail rename domain name see RFC1035</summary>
	public class DNSRecord_MR : DNSRecord {

		/// <summary>NEWNAME</summary>
		public Domain   NEWNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MR);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MR");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mail rename domain name");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MR", Domain);
			Canonicalize.Domain  (NEWNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MR Parse(Parse Parse) {
			DNSRecord_MR NewRecord = new DNSRecord_MR () ;
			
			NewRecord.NEWNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (NEWNAME);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MR Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MR NewRecord = new DNSRecord_MR () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.NEWNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> NULL 10 Null RR see RFC1035</summary>
	public class DNSRecord_NULL : DNSRecord {

		/// <summary>Anything</summary>
		public byte[]   Anything  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NULL);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NULL");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Null RR");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NULL", Domain);
			Canonicalize.Binary  (Anything);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NULL Parse(Parse Parse) {
			DNSRecord_NULL NewRecord = new DNSRecord_NULL () ;
			
			NewRecord.Anything = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteData (Anything);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NULL Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NULL NewRecord = new DNSRecord_NULL () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Anything = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> WKS 11 Well known service description see RFC1035</summary>
	public class DNSRecord_WKS : DNSRecord {

		/// <summary>Address</summary>
		public IPAddress   Address  ;
		/// <summary>Protocol</summary>
		public byte   Protocol  ;
		/// <summary>BITMAP</summary>
		public byte[]   BITMAP  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.WKS);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("WKS");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Well known service description");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("WKS", Domain);
			Canonicalize.IPv4  (Address);
			Canonicalize.Byte  (Protocol);
			Canonicalize.Binary  (BITMAP);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_WKS Parse(Parse Parse) {
			DNSRecord_WKS NewRecord = new DNSRecord_WKS () ;
			
			NewRecord.Address = Parse.IPv4  ();
			NewRecord.Protocol = Parse.Byte  ();
			NewRecord.BITMAP = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteIPv4 (Address);

			Index.WriteByte (Protocol);

			Index.WriteData (BITMAP);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_WKS Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_WKS NewRecord = new DNSRecord_WKS () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Address = Index.ReadIPv4 ();
			NewRecord.Protocol = Index.ReadByte ();
			NewRecord.BITMAP = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> PTR 12 Domain name pointer see RFC1035</summary>
	public class DNSRecord_PTR : DNSRecord {

		/// <summary>PTRDNAME</summary>
		public Domain   PTRDNAME  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.PTR);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("PTR");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Domain name pointer");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("PTR", Domain);
			Canonicalize.Domain  (PTRDNAME);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_PTR Parse(Parse Parse) {
			DNSRecord_PTR NewRecord = new DNSRecord_PTR () ;
			
			NewRecord.PTRDNAME = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (PTRDNAME);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_PTR Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_PTR NewRecord = new DNSRecord_PTR () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.PTRDNAME = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> HINFO 13 Host information see RFC1035</summary>
	public class DNSRecord_HINFO : DNSRecord {

		/// <summary>CPU</summary>
		public string   CPU  ;
		/// <summary>OS</summary>
		public string   OS  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.HINFO);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("HINFO");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Host information");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("HINFO", Domain);
			Canonicalize.String  (CPU);
			Canonicalize.String  (OS);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_HINFO Parse(Parse Parse) {
			DNSRecord_HINFO NewRecord = new DNSRecord_HINFO () ;
			
			NewRecord.CPU = Parse.String  ();
			NewRecord.OS = Parse.String  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteString8 (CPU);

			Index.WriteString8 (OS);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_HINFO Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_HINFO NewRecord = new DNSRecord_HINFO () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.CPU = Index.ReadString ();
			NewRecord.OS = Index.ReadString ();
			

			return NewRecord;
            }

		}

	/// <summary> MINFO 14 Mailbox or mail list information see RFC1035</summary>
	public class DNSRecord_MINFO : DNSRecord {

		/// <summary>RMAILBX</summary>
		public Domain   RMAILBX  ;
		/// <summary>EMAILBX</summary>
		public Domain   EMAILBX  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MINFO);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MINFO");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mailbox or mail list information");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MINFO", Domain);
			Canonicalize.Domain  (RMAILBX);
			Canonicalize.Domain  (EMAILBX);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MINFO Parse(Parse Parse) {
			DNSRecord_MINFO NewRecord = new DNSRecord_MINFO () ;
			
			NewRecord.RMAILBX = Parse.Domain  ();
			NewRecord.EMAILBX = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (RMAILBX);

			Index.WriteDomain (EMAILBX);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MINFO Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MINFO NewRecord = new DNSRecord_MINFO () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.RMAILBX = Index.ReadDomain ();
			NewRecord.EMAILBX = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> MX 15 Mail exchange see RFC1035</summary>
	public class DNSRecord_MX : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>Exchange</summary>
		public Domain   Exchange  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.MX);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("MX");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Mail exchange");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("MX", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.Domain  (Exchange);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_MX Parse(Parse Parse) {
			DNSRecord_MX NewRecord = new DNSRecord_MX () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.Exchange = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteDomain (Exchange);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_MX Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_MX NewRecord = new DNSRecord_MX () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.Exchange = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> TXT 16 Text strings see RFC1035</summary>
	public class DNSRecord_TXT : DNSRecord {

		/// <summary>Text</summary>
		public List<string>   Text  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.TXT);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("TXT");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Text strings");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("TXT", Domain);
			Canonicalize.Strings  (Text);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_TXT Parse(Parse Parse) {
			DNSRecord_TXT NewRecord = new DNSRecord_TXT () ;
			
			NewRecord.Text = Parse.Strings  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			foreach (string s in Text) {
				Index.WriteString8 (s);
				}

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_TXT Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_TXT NewRecord = new DNSRecord_TXT () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Text = Index.ReadStrings (Length - (Index.Pointer - NewRecord.Start)) ;
			

			return NewRecord;
            }

		}

	/// <summary> RP 17 Responsible Person see RFC1183</summary>
	public class DNSRecord_RP : DNSRecord {

		/// <summary>MBox</summary>
		public string   MBox  ;
		/// <summary>Txt</summary>
		public Domain   Txt  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.RP);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("RP");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Responsible Person");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("RP", Domain);
			Canonicalize.Mail  (MBox);
			Canonicalize.Domain  (Txt);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_RP Parse(Parse Parse) {
			DNSRecord_RP NewRecord = new DNSRecord_RP () ;
			
			NewRecord.MBox = Parse.Mail  ();
			NewRecord.Txt = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteMail (MBox);

			Index.WriteDomain (Txt);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_RP Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_RP NewRecord = new DNSRecord_RP () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.MBox = Index.ReadMail ();
			NewRecord.Txt = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> AFSDB 18 AFS Data Base location see RFC1183,RFC5864</summary>
	public class DNSRecord_AFSDB : DNSRecord {

		/// <summary>SubType</summary>
		public ushort   SubType  ;
		/// <summary>HostName</summary>
		public Domain   HostName  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.AFSDB);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("AFSDB");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("AFS Data Base location");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("AFSDB", Domain);
			Canonicalize.Int16  (SubType);
			Canonicalize.Domain  (HostName);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_AFSDB Parse(Parse Parse) {
			DNSRecord_AFSDB NewRecord = new DNSRecord_AFSDB () ;
			
			NewRecord.SubType = Parse.Int16  ();
			NewRecord.HostName = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (SubType);

			Index.WriteDomain (HostName);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_AFSDB Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_AFSDB NewRecord = new DNSRecord_AFSDB () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.SubType = Index.ReadInt16 ();
			NewRecord.HostName = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> X25 19 X.25 PSDN address see RFC1183</summary>
	public class DNSRecord_X25 : DNSRecord {

		/// <summary>PSDN</summary>
		public string   PSDN  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.X25);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("X25");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("X.25 PSDN address");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("X25", Domain);
			Canonicalize.String  (PSDN);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_X25 Parse(Parse Parse) {
			DNSRecord_X25 NewRecord = new DNSRecord_X25 () ;
			
			NewRecord.PSDN = Parse.String  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteString8 (PSDN);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_X25 Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_X25 NewRecord = new DNSRecord_X25 () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.PSDN = Index.ReadString ();
			

			return NewRecord;
            }

		}

	/// <summary> ISDN 20 ISDN address see RFC1183</summary>
	public class DNSRecord_ISDN : DNSRecord {

		/// <summary>ISDN</summary>
		public string   ISDN  ;
		/// <summary>SA</summary>
		public string   SA  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.ISDN);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("ISDN");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("ISDN address");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("ISDN", Domain);
			Canonicalize.String  (ISDN);
			Canonicalize.OptionalString  (SA);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_ISDN Parse(Parse Parse) {
			DNSRecord_ISDN NewRecord = new DNSRecord_ISDN () ;
			
			NewRecord.ISDN = Parse.String  ();
			NewRecord.SA = Parse.OptionalString  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteString8 (ISDN);

			if (SA != null) {
				Index.WriteString8 (SA);
				}


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_ISDN Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_ISDN NewRecord = new DNSRecord_ISDN () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.ISDN = Index.ReadString ();
			// If there is space left in the record, the optional string will be represented as
			// an octet followed by the string data
			if (Length - (Index.Pointer - NewRecord.Start) > 0) {
				NewRecord.SA = Index.ReadString ();
				}
			else {
				NewRecord.SA = null;
				}
			

			return NewRecord;
            }

		}

	/// <summary> RT 21 Route Through see RFC1183</summary>
	public class DNSRecord_RT : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>Exchange</summary>
		public Domain   Exchange  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.RT);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("RT");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Route Through");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("RT", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.Domain  (Exchange);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_RT Parse(Parse Parse) {
			DNSRecord_RT NewRecord = new DNSRecord_RT () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.Exchange = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteDomain (Exchange);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_RT Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_RT NewRecord = new DNSRecord_RT () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.Exchange = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> SIG 24 Security signature see RFC2535</summary>
	public class DNSRecord_SIG : DNSRecord {

		/// <summary>TypeCovered</summary>
		public ushort   TypeCovered  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>Labels</summary>
		public byte   Labels  ;
		/// <summary>OriginalTTL</summary>
		public uint   OriginalTTL  ;
		/// <summary>SignatureExpiration</summary>
		public uint   SignatureExpiration  ;
		/// <summary>SignatureInception</summary>
		public uint   SignatureInception  ;
		/// <summary>KeyTag</summary>
		public ushort   KeyTag  ;
		/// <summary>SignersName</summary>
		public string   SignersName  ;
		/// <summary>Signature</summary>
		public byte[]   Signature  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.SIG);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("SIG");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Security signature");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("SIG", Domain);
			Canonicalize.Int16  (TypeCovered);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Byte  (Labels);
			Canonicalize.Int32  (OriginalTTL);
			Canonicalize.Time32  (SignatureExpiration);
			Canonicalize.Time32  (SignatureInception);
			Canonicalize.Int16  (KeyTag);
			Canonicalize.String  (SignersName);
			Canonicalize.Binary  (Signature);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_SIG Parse(Parse Parse) {
			DNSRecord_SIG NewRecord = new DNSRecord_SIG () ;
			
			NewRecord.TypeCovered = Parse.Int16  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.Labels = Parse.Byte  ();
			NewRecord.OriginalTTL = Parse.Int32  ();
			NewRecord.SignatureExpiration = Parse.Time32  ();
			NewRecord.SignatureInception = Parse.Time32  ();
			NewRecord.KeyTag = Parse.Int16  ();
			NewRecord.SignersName = Parse.String  ();
			NewRecord.Signature = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (TypeCovered);

			Index.WriteByte (Algorithm);

			Index.WriteByte (Labels);

			Index.WriteInt32 (OriginalTTL);

			Index.WriteInt32 (SignatureExpiration);

			Index.WriteInt32 (SignatureInception);

			Index.WriteInt16 (KeyTag);

			Index.WriteString8 (SignersName);

			Index.WriteData (Signature);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_SIG Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_SIG NewRecord = new DNSRecord_SIG () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.TypeCovered = Index.ReadInt16 ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.Labels = Index.ReadByte ();
			NewRecord.OriginalTTL = Index.ReadInt32 ();
			NewRecord.SignatureExpiration = Index.ReadTime32 ();
			NewRecord.SignatureInception = Index.ReadTime32 ();
			NewRecord.KeyTag = Index.ReadInt16 ();
			NewRecord.SignersName = Index.ReadString ();
			NewRecord.Signature = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> KEY 25 Security key see RFC2535</summary>
	public class DNSRecord_KEY : DNSRecord {

		/// <summary>Flags</summary>
		public ushort   Flags  ;
		/// <summary>Protocol</summary>
		public byte   Protocol  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>PublicKey</summary>
		public byte[]   PublicKey  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.KEY);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("KEY");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Security key");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("KEY", Domain);
			Canonicalize.Int16  (Flags);
			Canonicalize.Byte  (Protocol);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Binary  (PublicKey);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_KEY Parse(Parse Parse) {
			DNSRecord_KEY NewRecord = new DNSRecord_KEY () ;
			
			NewRecord.Flags = Parse.Int16  ();
			NewRecord.Protocol = Parse.Byte  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.PublicKey = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Flags);

			Index.WriteByte (Protocol);

			Index.WriteByte (Algorithm);

			Index.WriteData (PublicKey);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_KEY Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_KEY NewRecord = new DNSRecord_KEY () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Flags = Index.ReadInt16 ();
			NewRecord.Protocol = Index.ReadByte ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.PublicKey = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> AAAA 28 IP6 Address see RFC3596</summary>
	public class DNSRecord_AAAA : DNSRecord {

		/// <summary>Address</summary>
		public IPAddress   Address  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.AAAA);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("AAAA");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("IP6 Address");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("AAAA", Domain);
			Canonicalize.IPv6  (Address);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_AAAA Parse(Parse Parse) {
			DNSRecord_AAAA NewRecord = new DNSRecord_AAAA () ;
			
			NewRecord.Address = Parse.IPv6  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteIPv6 (Address);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_AAAA Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_AAAA NewRecord = new DNSRecord_AAAA () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Address = Index.ReadIPv6 ();
			

			return NewRecord;
            }

		}

	/// <summary> SRV 33 Server Selection see RFC2782</summary>
	public class DNSRecord_SRV : DNSRecord {

		/// <summary>Priority</summary>
		public ushort   Priority  ;
		/// <summary>Weight</summary>
		public ushort   Weight  ;
		/// <summary>Port</summary>
		public ushort   Port  ;
		/// <summary>Target</summary>
		public Domain   Target  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.SRV);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("SRV");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Server Selection");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("SRV", Domain);
			Canonicalize.Int16  (Priority);
			Canonicalize.Int16  (Weight);
			Canonicalize.Int16  (Port);
			Canonicalize.Domain  (Target);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_SRV Parse(Parse Parse) {
			DNSRecord_SRV NewRecord = new DNSRecord_SRV () ;
			
			NewRecord.Priority = Parse.Int16  ();
			NewRecord.Weight = Parse.Int16  ();
			NewRecord.Port = Parse.Int16  ();
			NewRecord.Target = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Priority);

			Index.WriteInt16 (Weight);

			Index.WriteInt16 (Port);

			Index.WriteDomain (Target);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_SRV Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_SRV NewRecord = new DNSRecord_SRV () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Priority = Index.ReadInt16 ();
			NewRecord.Weight = Index.ReadInt16 ();
			NewRecord.Port = Index.ReadInt16 ();
			NewRecord.Target = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> NAPTR 35 Naming Authority Pointer see RFC2915,RFC2168,RFC3403</summary>
	public class DNSRecord_NAPTR : DNSRecord {

		/// <summary>Order</summary>
		public ushort   Order  ;
		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>Flags</summary>
		public string   Flags  ;
		/// <summary>Services</summary>
		public string   Services  ;
		/// <summary>Regexp</summary>
		public string   Regexp  ;
		/// <summary>Replacement</summary>
		public Domain   Replacement  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NAPTR);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NAPTR");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Naming Authority Pointer");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NAPTR", Domain);
			Canonicalize.Int16  (Order);
			Canonicalize.Int16  (Preference);
			Canonicalize.String  (Flags);
			Canonicalize.String  (Services);
			Canonicalize.String  (Regexp);
			Canonicalize.Domain  (Replacement);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NAPTR Parse(Parse Parse) {
			DNSRecord_NAPTR NewRecord = new DNSRecord_NAPTR () ;
			
			NewRecord.Order = Parse.Int16  ();
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.Flags = Parse.String  ();
			NewRecord.Services = Parse.String  ();
			NewRecord.Regexp = Parse.String  ();
			NewRecord.Replacement = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Order);

			Index.WriteInt16 (Preference);

			Index.WriteString8 (Flags);

			Index.WriteString8 (Services);

			Index.WriteString8 (Regexp);

			Index.WriteDomain (Replacement);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NAPTR Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NAPTR NewRecord = new DNSRecord_NAPTR () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Order = Index.ReadInt16 ();
			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.Flags = Index.ReadString ();
			NewRecord.Services = Index.ReadString ();
			NewRecord.Regexp = Index.ReadString ();
			NewRecord.Replacement = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> KX 36 Key Exchanger see RFC2230</summary>
	public class DNSRecord_KX : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>Exchange</summary>
		public Domain   Exchange  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.KX);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("KX");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Key Exchanger");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("KX", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.Domain  (Exchange);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_KX Parse(Parse Parse) {
			DNSRecord_KX NewRecord = new DNSRecord_KX () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.Exchange = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteDomain (Exchange);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_KX Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_KX NewRecord = new DNSRecord_KX () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.Exchange = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> CERT 37 CERT see RFC4398</summary>
	public class DNSRecord_CERT : DNSRecord {

		/// <summary>Type</summary>
		public ushort   Type  ;
		/// <summary>KeyTag</summary>
		public ushort   KeyTag  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>Certificate</summary>
		public byte[]   Certificate  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.CERT);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("CERT");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("CERT");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("CERT", Domain);
			Canonicalize.Int16  (Type);
			Canonicalize.Int16  (KeyTag);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Binary  (Certificate);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_CERT Parse(Parse Parse) {
			DNSRecord_CERT NewRecord = new DNSRecord_CERT () ;
			
			NewRecord.Type = Parse.Int16  ();
			NewRecord.KeyTag = Parse.Int16  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.Certificate = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Type);

			Index.WriteInt16 (KeyTag);

			Index.WriteByte (Algorithm);

			Index.WriteData (Certificate);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_CERT Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_CERT NewRecord = new DNSRecord_CERT () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Type = Index.ReadInt16 ();
			NewRecord.KeyTag = Index.ReadInt16 ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.Certificate = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> DNAME 39 DNAME see RFC6672</summary>
	public class DNSRecord_DNAME : DNSRecord {

		/// <summary>Target</summary>
		public Domain   Target  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.DNAME);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("DNAME");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("DNAME");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("DNAME", Domain);
			Canonicalize.Domain  (Target);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_DNAME Parse(Parse Parse) {
			DNSRecord_DNAME NewRecord = new DNSRecord_DNAME () ;
			
			NewRecord.Target = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (Target);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_DNAME Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_DNAME NewRecord = new DNSRecord_DNAME () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Target = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> OPT 41 OPT see RFC2671,RFC3225</summary>
	public class DNSRecord_OPT : DNSRecord {

		/// <summary>Options</summary>
		public List<DNSOption>   Options  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.OPT);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("OPT");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("OPT");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("OPT", Domain);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_OPT Parse(Parse Parse) {
			DNSRecord_OPT NewRecord = new DNSRecord_OPT () ;
			

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {


			foreach (DNSOption opt in Options) {
				Index.WriteInt16 (opt.Code);
				Index.WriteInt16 (opt.Data.Length);
				Index.WriteData (opt.Data);
				}

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_OPT Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_OPT NewRecord = new DNSRecord_OPT () ;
			NewRecord.Start = Index.Pointer;

			

			return NewRecord;
            }

		}

	/// <summary> DS 43 Delegation Signer see RFC4034,RFC3658</summary>
	public class DNSRecord_DS : DNSRecord {

		/// <summary>KeyTag</summary>
		public ushort   KeyTag  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>DigestType</summary>
		public byte   DigestType  ;
		/// <summary>Digest</summary>
		public byte[]   Digest  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.DS);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("DS");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Delegation Signer");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("DS", Domain);
			Canonicalize.Int16  (KeyTag);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Byte  (DigestType);
			Canonicalize.Hex  (Digest);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_DS Parse(Parse Parse) {
			DNSRecord_DS NewRecord = new DNSRecord_DS () ;
			
			NewRecord.KeyTag = Parse.Int16  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.DigestType = Parse.Byte  ();
			NewRecord.Digest = Parse.Hex  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (KeyTag);

			Index.WriteByte (Algorithm);

			Index.WriteByte (DigestType);

			Index.WriteData (Digest);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_DS Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_DS NewRecord = new DNSRecord_DS () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.KeyTag = Index.ReadInt16 ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.DigestType = Index.ReadByte ();
			NewRecord.Digest = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> SSHFP 44 SSH Key Fingerprint see RFC4255</summary>
	public class DNSRecord_SSHFP : DNSRecord {

		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>FPType</summary>
		public byte   FPType  ;
		/// <summary>Fingerprint</summary>
		public byte[]   Fingerprint  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.SSHFP);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("SSHFP");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("SSH Key Fingerprint");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("SSHFP", Domain);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Byte  (FPType);
			Canonicalize.Hex  (Fingerprint);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_SSHFP Parse(Parse Parse) {
			DNSRecord_SSHFP NewRecord = new DNSRecord_SSHFP () ;
			
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.FPType = Parse.Byte  ();
			NewRecord.Fingerprint = Parse.Hex  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteByte (Algorithm);

			Index.WriteByte (FPType);

			Index.WriteData (Fingerprint);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_SSHFP Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_SSHFP NewRecord = new DNSRecord_SSHFP () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.FPType = Index.ReadByte ();
			NewRecord.Fingerprint = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> IPSECKEY 45 IPSECKEY see RFC4025</summary>
	public class DNSRecord_IPSECKEY : DNSRecord {

		/// <summary>Precedence</summary>
		public byte   Precedence  ;
		/// <summary>GatewayType</summary>
		public byte   GatewayType  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>Gateway</summary>
		public DNSGateway   Gateway  ;
		/// <summary>PublicKey</summary>
		public byte[]   PublicKey  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.IPSECKEY);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("IPSECKEY");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("IPSECKEY");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("IPSECKEY", Domain);
			Canonicalize.Byte  (Precedence);
			Canonicalize.Byte  (GatewayType);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Binary  (PublicKey);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_IPSECKEY Parse(Parse Parse) {
			DNSRecord_IPSECKEY NewRecord = new DNSRecord_IPSECKEY () ;
			
			NewRecord.Precedence = Parse.Byte  ();
			NewRecord.GatewayType = Parse.Byte  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.PublicKey = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteByte (Precedence);

			Index.WriteByte (GatewayType);

			Index.WriteByte (Algorithm);


			Index.WriteData (PublicKey);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_IPSECKEY Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_IPSECKEY NewRecord = new DNSRecord_IPSECKEY () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Precedence = Index.ReadByte ();
			NewRecord.GatewayType = Index.ReadByte ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.PublicKey = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> RRSIG 46 RRSIG see RFC4034,RFC3755</summary>
	public class DNSRecord_RRSIG : DNSRecord {

		/// <summary>TypeCovered</summary>
		public ushort   TypeCovered  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>Labels</summary>
		public byte   Labels  ;
		/// <summary>OriginalTTL</summary>
		public uint   OriginalTTL  ;
		/// <summary>SignatureExpiration</summary>
		public uint   SignatureExpiration  ;
		/// <summary>SignatureInception</summary>
		public uint   SignatureInception  ;
		/// <summary>KeyTag</summary>
		public ushort   KeyTag  ;
		/// <summary>SignersName</summary>
		public string   SignersName  ;
		/// <summary>Signature</summary>
		public byte[]   Signature  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.RRSIG);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("RRSIG");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("RRSIG");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("RRSIG", Domain);
			Canonicalize.Int16  (TypeCovered);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Byte  (Labels);
			Canonicalize.Int32  (OriginalTTL);
			Canonicalize.Time32  (SignatureExpiration);
			Canonicalize.Time32  (SignatureInception);
			Canonicalize.Int16  (KeyTag);
			Canonicalize.String  (SignersName);
			Canonicalize.Binary  (Signature);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_RRSIG Parse(Parse Parse) {
			DNSRecord_RRSIG NewRecord = new DNSRecord_RRSIG () ;
			
			NewRecord.TypeCovered = Parse.Int16  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.Labels = Parse.Byte  ();
			NewRecord.OriginalTTL = Parse.Int32  ();
			NewRecord.SignatureExpiration = Parse.Time32  ();
			NewRecord.SignatureInception = Parse.Time32  ();
			NewRecord.KeyTag = Parse.Int16  ();
			NewRecord.SignersName = Parse.String  ();
			NewRecord.Signature = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (TypeCovered);

			Index.WriteByte (Algorithm);

			Index.WriteByte (Labels);

			Index.WriteInt32 (OriginalTTL);

			Index.WriteInt32 (SignatureExpiration);

			Index.WriteInt32 (SignatureInception);

			Index.WriteInt16 (KeyTag);

			Index.WriteString8 (SignersName);

			Index.WriteData (Signature);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_RRSIG Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_RRSIG NewRecord = new DNSRecord_RRSIG () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.TypeCovered = Index.ReadInt16 ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.Labels = Index.ReadByte ();
			NewRecord.OriginalTTL = Index.ReadInt32 ();
			NewRecord.SignatureExpiration = Index.ReadTime32 ();
			NewRecord.SignatureInception = Index.ReadTime32 ();
			NewRecord.KeyTag = Index.ReadInt16 ();
			NewRecord.SignersName = Index.ReadString ();
			NewRecord.Signature = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> NSEC 47 NSEC see RFC4034,RFC3755</summary>
	public class DNSRecord_NSEC : DNSRecord {

		/// <summary>NextDomain</summary>
		public Domain   NextDomain  ;
		/// <summary>TypeBitMaps</summary>
		public byte[]   TypeBitMaps  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NSEC);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NSEC");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("NSEC");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NSEC", Domain);
			Canonicalize.Domain  (NextDomain);
			Canonicalize.Binary  (TypeBitMaps);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NSEC Parse(Parse Parse) {
			DNSRecord_NSEC NewRecord = new DNSRecord_NSEC () ;
			
			NewRecord.NextDomain = Parse.Domain  ();
			NewRecord.TypeBitMaps = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (NextDomain);

			Index.WriteData (TypeBitMaps);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NSEC Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NSEC NewRecord = new DNSRecord_NSEC () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.NextDomain = Index.ReadDomain ();
			NewRecord.TypeBitMaps = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> DNSKEY 48 DNSKEY see RFC4034,RFC3755</summary>
	public class DNSRecord_DNSKEY : DNSRecord {

		/// <summary>Flags</summary>
		public ushort   Flags  ;
		/// <summary>Protocol</summary>
		public byte   Protocol  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>PublicKey</summary>
		public byte[]   PublicKey  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.DNSKEY);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("DNSKEY");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("DNSKEY");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("DNSKEY", Domain);
			Canonicalize.Int16  (Flags);
			Canonicalize.Byte  (Protocol);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Binary  (PublicKey);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_DNSKEY Parse(Parse Parse) {
			DNSRecord_DNSKEY NewRecord = new DNSRecord_DNSKEY () ;
			
			NewRecord.Flags = Parse.Int16  ();
			NewRecord.Protocol = Parse.Byte  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.PublicKey = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Flags);

			Index.WriteByte (Protocol);

			Index.WriteByte (Algorithm);

			Index.WriteData (PublicKey);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_DNSKEY Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_DNSKEY NewRecord = new DNSRecord_DNSKEY () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Flags = Index.ReadInt16 ();
			NewRecord.Protocol = Index.ReadByte ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.PublicKey = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> DHCID 49 DHCID see RFC4701</summary>
	public class DNSRecord_DHCID : DNSRecord {

		/// <summary>Identifier</summary>
		public byte[]   Identifier  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.DHCID);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("DHCID");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("DHCID");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("DHCID", Domain);
			Canonicalize.Binary  (Identifier);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_DHCID Parse(Parse Parse) {
			DNSRecord_DHCID NewRecord = new DNSRecord_DHCID () ;
			
			NewRecord.Identifier = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteData (Identifier);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_DHCID Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_DHCID NewRecord = new DNSRecord_DHCID () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Identifier = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> NSEC3 50 NSEC3 see RFC5155</summary>
	public class DNSRecord_NSEC3 : DNSRecord {

		/// <summary>HashAlgorithm</summary>
		public byte   HashAlgorithm  ;
		/// <summary>Flags</summary>
		public byte   Flags  ;
		/// <summary>Iterations</summary>
		public ushort   Iterations  ;
		/// <summary>Salt</summary>
		public byte[]   Salt  ;
		/// <summary>NextHashedOwnerName</summary>
		public byte[]   NextHashedOwnerName  ;
		/// <summary>TypeBitMaps</summary>
		public byte[]   TypeBitMaps  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NSEC3);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NSEC3");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("NSEC3");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NSEC3", Domain);
			Canonicalize.Byte  (HashAlgorithm);
			Canonicalize.Byte  (Flags);
			Canonicalize.Int16  (Iterations);
			Canonicalize.Binary8  (Salt);
			Canonicalize.Binary8  (NextHashedOwnerName);
			Canonicalize.Binary  (TypeBitMaps);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NSEC3 Parse(Parse Parse) {
			DNSRecord_NSEC3 NewRecord = new DNSRecord_NSEC3 () ;
			
			NewRecord.HashAlgorithm = Parse.Byte  ();
			NewRecord.Flags = Parse.Byte  ();
			NewRecord.Iterations = Parse.Int16  ();
			NewRecord.Salt = Parse.Binary8  ();
			NewRecord.NextHashedOwnerName = Parse.Binary8  ();
			NewRecord.TypeBitMaps = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteByte (HashAlgorithm);

			Index.WriteByte (Flags);

			Index.WriteInt16 (Iterations);

			Index.WriteByte ((byte)Salt.Length);
			Index.WriteData (Salt);

			Index.WriteByte ((byte)NextHashedOwnerName.Length);
			Index.WriteData (NextHashedOwnerName);

			Index.WriteData (TypeBitMaps);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NSEC3 Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NSEC3 NewRecord = new DNSRecord_NSEC3 () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.HashAlgorithm = Index.ReadByte ();
			NewRecord.Flags = Index.ReadByte ();
			NewRecord.Iterations = Index.ReadInt16 ();
			{
			int FieldLength = Index.ReadByte ();
			NewRecord.Salt = Index.ReadData (FieldLength);
			}
			{
			int FieldLength = Index.ReadByte ();
			NewRecord.NextHashedOwnerName = Index.ReadData (FieldLength);
			}
			NewRecord.TypeBitMaps = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> NSEC3PARAM 51 NSEC3PARAM see RFC5155</summary>
	public class DNSRecord_NSEC3PARAM : DNSRecord {

		/// <summary>HashAlgorithm</summary>
		public byte   HashAlgorithm  ;
		/// <summary>Flags</summary>
		public byte   Flags  ;
		/// <summary>Iterations</summary>
		public ushort   Iterations  ;
		/// <summary>Salt</summary>
		public byte[]   Salt  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NSEC3PARAM);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NSEC3PARAM");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("NSEC3PARAM");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NSEC3PARAM", Domain);
			Canonicalize.Byte  (HashAlgorithm);
			Canonicalize.Byte  (Flags);
			Canonicalize.Int16  (Iterations);
			Canonicalize.Binary8  (Salt);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NSEC3PARAM Parse(Parse Parse) {
			DNSRecord_NSEC3PARAM NewRecord = new DNSRecord_NSEC3PARAM () ;
			
			NewRecord.HashAlgorithm = Parse.Byte  ();
			NewRecord.Flags = Parse.Byte  ();
			NewRecord.Iterations = Parse.Int16  ();
			NewRecord.Salt = Parse.Binary8  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteByte (HashAlgorithm);

			Index.WriteByte (Flags);

			Index.WriteInt16 (Iterations);

			Index.WriteByte ((byte)Salt.Length);
			Index.WriteData (Salt);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NSEC3PARAM Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NSEC3PARAM NewRecord = new DNSRecord_NSEC3PARAM () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.HashAlgorithm = Index.ReadByte ();
			NewRecord.Flags = Index.ReadByte ();
			NewRecord.Iterations = Index.ReadInt16 ();
			{
			int FieldLength = Index.ReadByte ();
			NewRecord.Salt = Index.ReadData (FieldLength);
			}
			

			return NewRecord;
            }

		}

	/// <summary> TLSA 52 TLSA see RFC6698</summary>
	public class DNSRecord_TLSA : DNSRecord {

		/// <summary>CertificateUsage</summary>
		public byte   CertificateUsage  ;
		/// <summary>Selector</summary>
		public byte   Selector  ;
		/// <summary>MatchingType</summary>
		public byte   MatchingType  ;
		/// <summary>Certificate</summary>
		public byte[]   Certificate  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.TLSA);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("TLSA");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("TLSA");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("TLSA", Domain);
			Canonicalize.Byte  (CertificateUsage);
			Canonicalize.Byte  (Selector);
			Canonicalize.Byte  (MatchingType);
			Canonicalize.Binary  (Certificate);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_TLSA Parse(Parse Parse) {
			DNSRecord_TLSA NewRecord = new DNSRecord_TLSA () ;
			
			NewRecord.CertificateUsage = Parse.Byte  ();
			NewRecord.Selector = Parse.Byte  ();
			NewRecord.MatchingType = Parse.Byte  ();
			NewRecord.Certificate = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteByte (CertificateUsage);

			Index.WriteByte (Selector);

			Index.WriteByte (MatchingType);

			Index.WriteData (Certificate);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_TLSA Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_TLSA NewRecord = new DNSRecord_TLSA () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.CertificateUsage = Index.ReadByte ();
			NewRecord.Selector = Index.ReadByte ();
			NewRecord.MatchingType = Index.ReadByte ();
			NewRecord.Certificate = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> HIP 55 Host Identity Protocol see RFC5205</summary>
	public class DNSRecord_HIP : DNSRecord {

		/// <summary>HITLength</summary>
		public byte   HITLength  ;
		/// <summary>PublicKeyAlgorithm</summary>
		public byte   PublicKeyAlgorithm  ;
		/// <summary>PublicKeyLength</summary>
		public ushort   PublicKeyLength  ;
		/// <summary>HIT</summary>
		public byte[]   HIT  ;
		/// <summary>PublicKey</summary>
		public byte[]   PublicKey  ;
		/// <summary>RendezvousServers</summary>
		public byte[]   RendezvousServers  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.HIP);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("HIP");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Host Identity Protocol");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("HIP", Domain);
			Canonicalize.Byte  (HITLength);
			Canonicalize.Byte  (PublicKeyAlgorithm);
			Canonicalize.Int16  (PublicKeyLength);
			Canonicalize.LBinary  (HIT);
			Canonicalize.LBinary  (PublicKey);
			Canonicalize.Binary  (RendezvousServers);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_HIP Parse(Parse Parse) {
			DNSRecord_HIP NewRecord = new DNSRecord_HIP () ;
			
			NewRecord.HITLength = Parse.Byte  ();
			NewRecord.PublicKeyAlgorithm = Parse.Byte  ();
			NewRecord.PublicKeyLength = Parse.Int16  ();
			NewRecord.HIT = Parse.LBinary  ();
			NewRecord.PublicKey = Parse.LBinary  ();
			NewRecord.RendezvousServers = Parse.Binary  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {


			Index.WriteByte (PublicKeyAlgorithm);


			Index.Write (HITLength);
			Index.WriteData (HIT);

			Index.Write (PublicKeyLength);
			Index.WriteData (PublicKey);

			Index.WriteData (RendezvousServers);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_HIP Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_HIP NewRecord = new DNSRecord_HIP () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.HITLength = Index.ReadByte ();
			NewRecord.PublicKeyAlgorithm = Index.ReadByte ();
			NewRecord.PublicKeyLength = Index.ReadInt16 ();
			// Binary - length specified by HITLength
			// Binary - length specified by PublicKeyLength
			NewRecord.RendezvousServers = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> SPF 99 Sender Policy Framework see RFC4408</summary>
	public class DNSRecord_SPF : DNSRecord {

		/// <summary>Text</summary>
		public List<string>   Text  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.SPF);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("SPF");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Sender Policy Framework");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("SPF", Domain);
			Canonicalize.Strings  (Text);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_SPF Parse(Parse Parse) {
			DNSRecord_SPF NewRecord = new DNSRecord_SPF () ;
			
			NewRecord.Text = Parse.Strings  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			foreach (string s in Text) {
				Index.WriteString8 (s);
				}

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_SPF Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_SPF NewRecord = new DNSRecord_SPF () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Text = Index.ReadStrings (Length - (Index.Pointer - NewRecord.Start)) ;
			

			return NewRecord;
            }

		}

	/// <summary> NID 104  see RFC6742</summary>
	public class DNSRecord_NID : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>NodeID</summary>
		public ulong   NodeID  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.NID);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("NID");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("NID", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.NodeID  (NodeID);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_NID Parse(Parse Parse) {
			DNSRecord_NID NewRecord = new DNSRecord_NID () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.NodeID = Parse.NodeID  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteInt64 (NodeID);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_NID Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_NID NewRecord = new DNSRecord_NID () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.NodeID = Index.ReadNodeID ();
			

			return NewRecord;
            }

		}

	/// <summary> L32 105  see RFC6742</summary>
	public class DNSRecord_L32 : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>Locator</summary>
		public IPAddress   Locator  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.L32);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("L32");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("L32", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.IPv4  (Locator);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_L32 Parse(Parse Parse) {
			DNSRecord_L32 NewRecord = new DNSRecord_L32 () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.Locator = Parse.IPv4  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteIPv4 (Locator);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_L32 Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_L32 NewRecord = new DNSRecord_L32 () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.Locator = Index.ReadIPv4 ();
			

			return NewRecord;
            }

		}

	/// <summary> L64 106  see RFC6742</summary>
	public class DNSRecord_L64 : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>Locator</summary>
		public ulong   Locator  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.L64);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("L64");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("L64", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.NodeID  (Locator);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_L64 Parse(Parse Parse) {
			DNSRecord_L64 NewRecord = new DNSRecord_L64 () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.Locator = Parse.NodeID  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteInt64 (Locator);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_L64 Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_L64 NewRecord = new DNSRecord_L64 () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.Locator = Index.ReadNodeID ();
			

			return NewRecord;
            }

		}

	/// <summary> LP 107  see RFC6742</summary>
	public class DNSRecord_LP : DNSRecord {

		/// <summary>Preference</summary>
		public ushort   Preference  ;
		/// <summary>FQDN</summary>
		public Domain   FQDN  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.LP);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("LP");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("LP", Domain);
			Canonicalize.Int16  (Preference);
			Canonicalize.Domain  (FQDN);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_LP Parse(Parse Parse) {
			DNSRecord_LP NewRecord = new DNSRecord_LP () ;
			
			NewRecord.Preference = Parse.Int16  ();
			NewRecord.FQDN = Parse.Domain  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Preference);

			Index.WriteDomain (FQDN);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_LP Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_LP NewRecord = new DNSRecord_LP () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Preference = Index.ReadInt16 ();
			NewRecord.FQDN = Index.ReadDomain ();
			

			return NewRecord;
            }

		}

	/// <summary> TKEY 249 Transaction Key see RFC2930</summary>
	public class DNSRecord_TKEY : DNSRecord {

		/// <summary>Algorithm</summary>
		public Domain   Algorithm  ;
		/// <summary>Inception</summary>
		public uint   Inception  ;
		/// <summary>Expiration</summary>
		public uint   Expiration  ;
		/// <summary>Mode</summary>
		public ushort   Mode  ;
		/// <summary>Error</summary>
		public ushort   Error  ;
		/// <summary>KeyData</summary>
		public byte[]   KeyData  ;
		/// <summary>OtherData</summary>
		public byte[]   OtherData  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.TKEY);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("TKEY");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Transaction Key");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("TKEY", Domain);
			Canonicalize.Domain  (Algorithm);
			Canonicalize.Time32  (Inception);
			Canonicalize.Time32  (Expiration);
			Canonicalize.Int16  (Mode);
			Canonicalize.Int16  (Error);
			Canonicalize.Binary16  (KeyData);
			Canonicalize.Binary16  (OtherData);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_TKEY Parse(Parse Parse) {
			DNSRecord_TKEY NewRecord = new DNSRecord_TKEY () ;
			
			NewRecord.Algorithm = Parse.Domain  ();
			NewRecord.Inception = Parse.Time32  ();
			NewRecord.Expiration = Parse.Time32  ();
			NewRecord.Mode = Parse.Int16  ();
			NewRecord.Error = Parse.Int16  ();
			NewRecord.KeyData = Parse.Binary16  ();
			NewRecord.OtherData = Parse.Binary16  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (Algorithm);

			Index.WriteInt32 (Inception);

			Index.WriteInt32 (Expiration);

			Index.WriteInt16 (Mode);

			Index.WriteInt16 (Error);

			Index.WriteInt16 (KeyData.Length);
			Index.WriteData (KeyData);

			Index.WriteInt16 (OtherData.Length);
			Index.WriteData (OtherData);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_TKEY Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_TKEY NewRecord = new DNSRecord_TKEY () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Algorithm = Index.ReadDomain ();
			NewRecord.Inception = Index.ReadTime32 ();
			NewRecord.Expiration = Index.ReadTime32 ();
			NewRecord.Mode = Index.ReadInt16 ();
			NewRecord.Error = Index.ReadInt16 ();
			{
			int FieldLength = Index.ReadInt16 ();
			NewRecord.KeyData = Index.ReadData (FieldLength);
			}
			{
			int FieldLength = Index.ReadInt16 ();
			NewRecord.OtherData = Index.ReadData (FieldLength);
			}
			

			return NewRecord;
            }

		}

	/// <summary> TSIG 250 Transaction Signature see RFC2845</summary>
	public class DNSRecord_TSIG : DNSRecord {

		/// <summary>Algorithm</summary>
		public Domain   Algorithm  ;
		/// <summary>TimeSigned</summary>
		public ulong   TimeSigned  ;
		/// <summary>Fudge</summary>
		public ushort   Fudge  ;
		/// <summary>MAC</summary>
		public byte[]   MAC  ;
		/// <summary>OriginalID</summary>
		public ushort   OriginalID  ;
		/// <summary>Error</summary>
		public ushort   Error  ;
		/// <summary>OtherData</summary>
		public byte[]   OtherData  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.TSIG);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("TSIG");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Transaction Signature");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("TSIG", Domain);
			Canonicalize.Domain  (Algorithm);
			Canonicalize.Time48  (TimeSigned);
			Canonicalize.Int16  (Fudge);
			Canonicalize.Binary16  (MAC);
			Canonicalize.Int16  (OriginalID);
			Canonicalize.Int16  (Error);
			Canonicalize.Binary16  (OtherData);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_TSIG Parse(Parse Parse) {
			DNSRecord_TSIG NewRecord = new DNSRecord_TSIG () ;
			
			NewRecord.Algorithm = Parse.Domain  ();
			NewRecord.TimeSigned = Parse.Time48  ();
			NewRecord.Fudge = Parse.Int16  ();
			NewRecord.MAC = Parse.Binary16  ();
			NewRecord.OriginalID = Parse.Int16  ();
			NewRecord.Error = Parse.Int16  ();
			NewRecord.OtherData = Parse.Binary16  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteDomain (Algorithm);

			Index.WriteInt48 (TimeSigned);

			Index.WriteInt16 (Fudge);

			Index.WriteInt16 (MAC.Length);
			Index.WriteData (MAC);

			Index.WriteInt16 (OriginalID);

			Index.WriteInt16 (Error);

			Index.WriteInt16 (OtherData.Length);
			Index.WriteData (OtherData);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_TSIG Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_TSIG NewRecord = new DNSRecord_TSIG () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Algorithm = Index.ReadDomain ();
			NewRecord.TimeSigned = Index.ReadTime48 ();
			NewRecord.Fudge = Index.ReadInt16 ();
			{
			int FieldLength = Index.ReadInt16 ();
			NewRecord.MAC = Index.ReadData (FieldLength);
			}
			NewRecord.OriginalID = Index.ReadInt16 ();
			NewRecord.Error = Index.ReadInt16 ();
			{
			int FieldLength = Index.ReadInt16 ();
			NewRecord.OtherData = Index.ReadData (FieldLength);
			}
			

			return NewRecord;
            }

		}

	/// <summary> URI 256 URI see Patrik_Faltstrom</summary>
	public class DNSRecord_URI : DNSRecord {

		/// <summary>Priority</summary>
		public ushort   Priority  ;
		/// <summary>Weight</summary>
		public ushort   Weight  ;
		/// <summary>Target</summary>
		public List<string>   Target  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.URI);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("URI");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("URI");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("URI", Domain);
			Canonicalize.Int16  (Priority);
			Canonicalize.Int16  (Weight);
			Canonicalize.Strings  (Target);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_URI Parse(Parse Parse) {
			DNSRecord_URI NewRecord = new DNSRecord_URI () ;
			
			NewRecord.Priority = Parse.Int16  ();
			NewRecord.Weight = Parse.Int16  ();
			NewRecord.Target = Parse.Strings  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (Priority);

			Index.WriteInt16 (Weight);

			foreach (string s in Target) {
				Index.WriteString8 (s);
				}

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_URI Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_URI NewRecord = new DNSRecord_URI () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Priority = Index.ReadInt16 ();
			NewRecord.Weight = Index.ReadInt16 ();
			NewRecord.Target = Index.ReadStrings (Length - (Index.Pointer - NewRecord.Start)) ;
			

			return NewRecord;
            }

		}

	/// <summary> CAA 257 Certification Authority Restriction see RFC-ietf-pkix-caa-15</summary>
	public class DNSRecord_CAA : DNSRecord {

		/// <summary>Flags</summary>
		public byte   Flags  ;
		/// <summary>Tag</summary>
		public string   Tag  ;
		/// <summary>Value</summary>
		public string   Value  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.CAA);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("CAA");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("Certification Authority Restriction");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("CAA", Domain);
			Canonicalize.Byte  (Flags);
			Canonicalize.String  (Tag);
			Canonicalize.StringX  (Value);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_CAA Parse(Parse Parse) {
			DNSRecord_CAA NewRecord = new DNSRecord_CAA () ;
			
			NewRecord.Flags = Parse.Byte  ();
			NewRecord.Tag = Parse.String  ();
			NewRecord.Value = Parse.StringX  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteByte (Flags);

			Index.WriteString8 (Tag);

			Index.WriteString (Value);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_CAA Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_CAA NewRecord = new DNSRecord_CAA () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.Flags = Index.ReadByte ();
			NewRecord.Tag = Index.ReadString ();
			NewRecord.Value = Index.ReadString (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> TA 32768 DNSSEC Trust Authorities see Sam_Weiler</summary>
	public class DNSRecord_TA : DNSRecord {

		/// <summary>KeyTag</summary>
		public ushort   KeyTag  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>DigestType</summary>
		public byte   DigestType  ;
		/// <summary>Digest</summary>
		public byte[]   Digest  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.TA);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("TA");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("DNSSEC Trust Authorities");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("TA", Domain);
			Canonicalize.Int16  (KeyTag);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Byte  (DigestType);
			Canonicalize.Hex  (Digest);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_TA Parse(Parse Parse) {
			DNSRecord_TA NewRecord = new DNSRecord_TA () ;
			
			NewRecord.KeyTag = Parse.Int16  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.DigestType = Parse.Byte  ();
			NewRecord.Digest = Parse.Hex  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (KeyTag);

			Index.WriteByte (Algorithm);

			Index.WriteByte (DigestType);

			Index.WriteData (Digest);


            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_TA Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_TA NewRecord = new DNSRecord_TA () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.KeyTag = Index.ReadInt16 ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.DigestType = Index.ReadByte ();
			NewRecord.Digest = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	/// <summary> DLV 32769 DNSSEC Lookaside Validation see RFC4431</summary>
	public class DNSRecord_DLV : DNSRecord {

		/// <summary>KeyTag</summary>
		public ushort   KeyTag  ;
		/// <summary>Algorithm</summary>
		public byte   Algorithm  ;
		/// <summary>DigestType</summary>
		public byte   DigestType  ;
		/// <summary>Digest</summary>
		public byte[]   Digest  ;

		/// <summary>The type code</summary>
		public override DNSTypeCode		Code {
			get {return (DNSTypeCode.DLV);} }		
		
		/// <summary>The type text</summary>
		public override string	Label {
			get {return ("DLV");} }
			
		/// <summary>Description</summary>	
		public override string	Description {
			get {return ("DNSSEC Lookaside Validation");} }


        /// <summary>Convert to canonical form</summary>
        /// <returns>Canonical form of record data contents</returns>
        public override string Canonical () {
			Canonicalize Canonicalize = new Canonicalize ("DLV", Domain);
			Canonicalize.Int16  (KeyTag);
			Canonicalize.Byte  (Algorithm);
			Canonicalize.Byte  (DigestType);
			Canonicalize.Hex  (Digest);
			return Canonicalize.Text;
            }

		/// <summary>Parse record or query from string</summary>	
        /// <param name="Parse">Input data</param>
        /// <returns>Parsed record.</returns>
        public static DNSRecord_DLV Parse(Parse Parse) {
			DNSRecord_DLV NewRecord = new DNSRecord_DLV () ;
			
			NewRecord.KeyTag = Parse.Int16  ();
			NewRecord.Algorithm = Parse.Byte  ();
			NewRecord.DigestType = Parse.Byte  ();
			NewRecord.Digest = Parse.Hex  ();

			return NewRecord;
            }

        /// <summary>Convert to wire form</summary>
		/// <param name="Index">Output buffer</param>
        /// <returns>Canonical form of record data contents</returns>
        public override void Encode(DNSBufferIndex Index) {

			Index.WriteInt16 (KeyTag);

			Index.WriteByte (Algorithm);

			Index.WriteByte (DigestType);

			Index.WriteData (Digest);

            }

		/// <summary>Decode record or query from byte form buffer</summary>	
        /// <param name="Index">Input data</param>
		/// <param name="Length">Maximum amount of data to read</param>
        /// <returns>Parsed record.</returns>
        public static  DNSRecord_DLV Decode (DNSBufferIndex Index, int Length) {
			DNSRecord_DLV NewRecord = new DNSRecord_DLV () ;
			NewRecord.Start = Index.Pointer;

			NewRecord.KeyTag = Index.ReadInt16 ();
			NewRecord.Algorithm = Index.ReadByte ();
			NewRecord.DigestType = Index.ReadByte ();
			NewRecord.Digest = Index.ReadData (Length - (Index.Pointer - NewRecord.Start));
			

			return NewRecord;
            }

		}

	}


