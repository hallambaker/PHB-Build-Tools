using System;
using System.Collections.Generic;
using Goedel.Utilities;

namespace Goedel.Protocol {

    public enum DataEncoding {
        /// <summary>JSON encoding in UTF8</summary>
        JSON,
        /// <summary>JSON easy to edit format in UTF8</summary>
        JSON_A,
        /// <summary>JSON-B encoding in UTF8 plus binary extensions</summary>
        JSON_B,
        /// <summary>JSON-C encoding in UTF8 plus binary extensions</summary>
        JSON_C,
        /// <summary>JSON-D encoding in UTF8 plus binary extensions</summary>
        JSON_D,
        /// <summary>ASN-1</summary>
        ASN_1,
        /// <summary>RFC 822 style message header</summary>
        RFC822
        }


    public static partial class Extensions {


        public static byte[] GetBytes (this JSONObject Object, 
                    DataEncoding Encoding, 
                    bool Tagged = true) {

            switch (Encoding) {
                case DataEncoding.JSON: {
                    return GetJson(Object, Tagged);
                    }
                case DataEncoding.JSON_A: {
                    return GetJsonA(Object, Tagged);
                    }
                case DataEncoding.JSON_B: {
                    return GetJsonB(Object, Tagged);
                    }
                case DataEncoding.JSON_C: {
                    return GetJsonC(Object, Tagged);
                    }
                case DataEncoding.JSON_D: {
                    return GetJsonD(Object, Tagged);
                    }
                }

            throw new NYI();
            }


        public static byte[] GetJson (this JSONObject Object, bool Tagged = true) {
            return Object.GetBytes(Tagged);
            }

        /// <summary>
        /// Convert object to byte sequence in JSON form.
        /// </summary>
        /// <param name="Tagged">If true, serialization is tagged with the object type.</param>
        /// <returns>Data as byte sequence.</returns>
        public static byte[] GetJsonA (this JSONObject Object, bool Tagged = true) {
            JSONWriter JSONWriter = new JSONAWriter();
            Object.Serialize(JSONWriter, Tagged);
            return JSONWriter.GetBytes;
            }

        /// <summary>
        /// Convert object to byte sequence in JSON form.
        /// </summary>
        /// <param name="Tagged">If true, serialization is tagged with the object type.</param>
        /// <returns>Data as byte sequence.</returns>
        public static byte[] GetJsonB (this JSONObject Object, bool Tagged = true) {
            JSONWriter JSONWriter = new JSONBWriter();
            Object.Serialize(JSONWriter, Tagged);
            return JSONWriter.GetBytes;
            }

        /// <summary>
        /// Convert object to byte sequence in JSON form.
        /// </summary>
        /// <param name="Tagged">If true, serialization is tagged with the object type.</param>
        /// <returns>Data as byte sequence.</returns>
        public static byte[] GetJsonC (this JSONObject Object, bool Tagged = true,
                    Dictionary<string, int> TagDictionary = null) {
            JSONWriter JSONWriter = new JSONCWriter(TagDictionary: TagDictionary);
            Object.Serialize(JSONWriter, Tagged);
            return JSONWriter.GetBytes;
            }

        /// <summary>
        /// Convert object to byte sequence in JSON form.
        /// </summary>
        /// <param name="Tagged">If true, serialization is tagged with the object type.</param>
        /// <returns>Data as byte sequence.</returns>
        public static byte[] GetJsonD (this JSONObject Object, bool Tagged = true,
                    Dictionary<string, int> TagDictionary = null) {
            // NYI: Implement the JSON D format.
            JSONWriter JSONWriter = new JSONCWriter(TagDictionary: TagDictionary);
            Object.Serialize(JSONWriter, Tagged);
            return JSONWriter.GetBytes;
            }


        }
    }
