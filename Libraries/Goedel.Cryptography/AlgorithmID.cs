using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Cryptography {

    /// <summary>
    /// Extension class to manage OID and XML references.
    /// </summary>
    public static class AlgorithmID {

        /// <summary>
        /// Mapping of XML DigSig entries to CryptoAlgorithmID
        /// </summary>
        static readonly Dictionary<string, CryptoAlgorithmID> XMLToID =
            new Dictionary<string, CryptoAlgorithmID> {
                    {"2001/04/xmldsig-more#rsa-sha256", CryptoAlgorithmID.RSASign |CryptoAlgorithmID.SHA_2_256 },
                    {"2001/04/xmldsig-more#rsa-sha512", CryptoAlgorithmID.RSASign |CryptoAlgorithmID.SHA_2_512 },
                    {"2001/04/xmlenc#aes128-cbc", CryptoAlgorithmID.AES128CBC },
                    {"2001/04/xmlenc#aes256-cbc", CryptoAlgorithmID.AES256CBC },
                    {"2001/04/xmlenc#kw-aes128", CryptoAlgorithmID.AES128_KW },
                    {"2001/04/xmlenc#kw-aes256", CryptoAlgorithmID.AES256_KW },
                    {"2009/xmlenc11#rsa-oaep", CryptoAlgorithmID.RSAExch },
                    { "2001/04/xmlenc#rsa-1_5", CryptoAlgorithmID.RSAExch_P15 },
                    {"2001/04/xmlenc#sha256", CryptoAlgorithmID.SHA_2_256 },
                    {"2001/04/xmlenc#sha512", CryptoAlgorithmID.SHA_2_512 },
                    {"2007/05/xmldsig-more#sha3-256", CryptoAlgorithmID.SHA_3_256 },
                    {"2007/05/xmldsig-more#sha3-512", CryptoAlgorithmID.SHA_3_512 },
                    {"2009/xmlenc11#aes128-gcm", CryptoAlgorithmID.AES128GCM },
                    {"2009/xmlenc11#aes256-gcm", CryptoAlgorithmID.AES256GCM },
                    {"2009/xmlenc11#dh-es", CryptoAlgorithmID.DH },
            };

        static readonly Dictionary<CryptoAlgorithmID, string> IDToXML =
            new Dictionary<CryptoAlgorithmID, string>();


        /// <summary>
        /// Mapping of OID entries to CryptoAlgorithmID
        /// </summary>
        static readonly Dictionary<string, CryptoAlgorithmID> OIDToID =
            new Dictionary<string, CryptoAlgorithmID> {
                    { PKIX.Constants.OIDS__id_sha256, CryptoAlgorithmID.SHA_2_256},
                    { PKIX.Constants.OIDS__id_sha512, CryptoAlgorithmID.SHA_2_512}
                    //{ PKIX.Constants., CryptoAlgorithmID.SHA_3_256},
                    //{ PKIX.Constants.OIDS__sha1WithRSAEncryption, CryptoAlgorithmID.SHA_3_512},
            };


        ///// <summary>
        ///// ASN.1 Object Identifier.
        ///// </summary>
        //public string OID {
        //    get {
        //        switch (BulkAlgorithm) {
        //            case CryptoAlgorithmID.SHA_1_DEPRECATED:
        //                return PKIX.Constants.OIDS__sha1WithRSAEncryption;
        //            case CryptoAlgorithmID.SHA_2_256:
        //                return PKIX.Constants.OIDS__sha256WithRSAEncryption;
        //            case CryptoAlgorithmID.SHA_2_512:
        //                return PKIX.Constants.OIDS__sha512WithRSAEncryption;
        //            case CryptoAlgorithmID.SHA_3_256:
        //                return PKIX.Constants.OIDS__sha256WithRSAEncryption;
        //            case CryptoAlgorithmID.SHA_3_512:
        //                return PKIX.Constants.OIDS__sha512WithRSAEncryption;
        //            default:
        //                return null;
        //            }
        //        }
        //    }


        static readonly Dictionary<CryptoAlgorithmID, string> IDToOID =
            new Dictionary<CryptoAlgorithmID, string>();


        static AlgorithmID() {

            foreach (var Entry in XMLToID) {
                IDToXML.Add(Entry.Value, Entry.Key);
                }
            foreach (var Entry in OIDToID) {
                IDToOID.Add(Entry.Value, Entry.Key);
                }
            }


        /// <summary>
        /// Add an algorithm entry. Note this is not thread safe and is only intended to be 
        /// called during class library initialization.
        /// </summary>
        /// <param name="ID">CryptoAlgorithmID Identifier</param>
        /// <param name="OID">OID</param>
        /// <param name="XML">XML Signature and Encryption algorithm identifier</param>
        public static void Add(CryptoAlgorithmID ID, string OID=null, string XML=null) {
            if (OID != null) {
                IDToOID.Add(ID, OID);
                OIDToID.Add(OID, ID);
                }
            if (XML != null) {
                IDToXML.Add(ID, XML);
                XMLToID.Add(XML, ID);
                }
            }


        /// <summary>
        /// Get the CryptoAlgorithmID corresponding to an XML DigSig URL
        /// </summary>
        /// <param name="URL">XML Signature and Encryption algorithm identifier</param>
        /// <returns>Algorithm Identifier</returns>
        public static CryptoAlgorithmID FromXMLID (this string URL) {
            CryptoAlgorithmID Result;
            var Found = XMLToID.TryGetValue(URL, out Result);
            return Found ? Result : CryptoAlgorithmID.NULL;
            }


        /// <summary>
        /// Get the XML DigSig URL corresponding to a CryptoAlgorithmID
        /// </summary>
        /// <param name="ID">Algorithm Identifier</param>
        /// <returns>XML Signature and Encryption algorithm identifier</returns>
        public static string ToXMLID(this CryptoAlgorithmID ID) {
            string Result;
            var Found = IDToXML.TryGetValue(ID, out Result);
            return Found ? Result : null;
            }

        /// <summary>
        /// Get the CryptoAlgorithmID corresponding to an XML DigSig URL
        /// </summary>
        /// <param name="URL">XML Signature and Encryption algorithm identifier</param>
        /// <returns>Algorithm Identifier</returns>
        public static CryptoAlgorithmID FromOID(this string URL) {
            CryptoAlgorithmID Result;
            var Found = OIDToID.TryGetValue(URL, out Result);
            return Found ? Result : CryptoAlgorithmID.NULL;
            }

        /// <summary>
        /// Get the XML DigSig URL corresponding to a CryptoAlgorithmID
        /// </summary>
        /// <param name="ID">Algorithm Identifier</param>
        /// <returns>XML Signature and Encryption algorithm identifier</returns>
        public static string ToOID(this CryptoAlgorithmID ID) {
            string Result;
            var Found = IDToOID.TryGetValue(ID, out Result);
            return Found ? Result : null;
            }


        }
    }
