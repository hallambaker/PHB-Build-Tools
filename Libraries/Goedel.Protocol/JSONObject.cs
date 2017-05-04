//   Copyright © 2015 by Comodo Group Inc.
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

using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Goedel.Protocol;

namespace Goedel.Protocol {

    /// <summary>
    /// Encoding types for unified encoding
    /// </summary>
    public enum ObjectEncoding {
        /// <summary>JSON encoding</summary>
        JSON,
        /// <summary>JSON-A encoding</summary>
        JSON_A,
        /// <summary>JSON-B encoding</summary>
        JSON_B,
        /// <summary>JSON-C encoding</summary>
        JSON_C,
        /// <summary>JSON-D encoding</summary>
        JSON_D,
        /// <summary>XML encoding</summary>
        XML,
        /// <summary>ASN encoding</summary>
        ASN,
        /// <summary>RFC822 header style encoding</summary>
        RFC822
        }

    /// <summary>
    /// Base class for JSON Objects.
    /// </summary>
    public abstract partial class JSONObject {

        ///// <summary>
        ///// Initialization constructor. Allows automatically generated 
        ///// constructors to be overriden in child classes.
        ///// </summary>
        //protected virtual void _Initialize () {
        //    }

        /// <summary>
        /// Tag value used as substitute for reflection internally.
        /// </summary>
        /// <returns>The object tag.</returns>
		public virtual string Tag () {
			return "MeshItem";
			}

        /// <summary>
        /// Base constructor.
        /// </summary>
		public JSONObject () {
            //_Initialize();
			}

  //      /// <summary>
  //      /// Create object from data read from the corresponding reader.
  //      /// </summary>
  //      /// <param name="JSONReader">The input data</param>
		//public JSONObject (JSONReader JSONReader) {
		//	Deserialize (JSONReader);
		//	}

  //      /// <summary>
  //      /// Create object from data read from the corresponding string.
  //      /// </summary>
  //      /// <param name="_String">The input data</param>
  //      public JSONObject(string _String) {
		//	Deserialize (_String);
		//	}

        /// <summary>
        /// If implemented in the child class, performs a deep copy of the structure.
        /// </summary>
        /// <returns>Deep copy of the object with all referenced objects
        /// copied.</returns>
        public virtual JSONObject DeepCopy() {
            return null;
            }

        /// <summary>
        /// Convert object to string in JSON form
        /// </summary>
        /// <returns>Data as string.</returns>
		public override string ToString () {
			JSONWriter _JSONWriter = new JSONWriter ();
			Serialize (_JSONWriter, true);
			return _JSONWriter.GetUTF8;
			}

        /// <summary>
        /// Convert object to string in JSON form.
        /// </summary>
        /// <returns>Data as string.</returns>
		public virtual  string GetUTF8 () {
			JSONWriter _JSONWriter = new JSONWriter ();
			Serialize (_JSONWriter, true);
			return _JSONWriter.GetUTF8;
			}

        /// <summary>
        /// Convert object to byte sequence in JSON form.
        /// </summary>
        /// <param name="tag">If true, serialization is tagged with the object type.</param>
        /// <returns>Data as byte sequence.</returns>
        public virtual byte[] GetBytes(bool tag = true) {
            JSONWriter _JSONWriter = new JSONWriter();
            Serialize(_JSONWriter, tag);
            return _JSONWriter.GetBytes;
            }

        /// <summary>
        /// Serialize to the specified Writer.
        /// </summary>
        /// <param name="Writer">Writer to serialize the data to</param>
        /// <param name="tag">If true, serialization is tagged with the object type.</param>
        public virtual void Serialize (Writer Writer, bool tag=false) {
			bool first = true;
			if (tag) {
				Writer.WriteObjectStart ();
				Writer.WriteToken(Tag(), 0);
				}
			
			Serialize (Writer, true, ref first);
			
			if (tag) {
				Writer.WriteObjectEnd ();
				}			
			}

        /// <summary>
        /// Serialize to the specified Writer.
        /// </summary>
        /// <param name="Writer">Writer to serialize the data to</param>
        /// <param name="first">This is the first field in the object being serialized. This 
        /// value is set to false on exit.</param>
        /// <param name="wrap">Wrap the objects for formatting.</param>
		public abstract void Serialize(Writer Writer, bool wrap, ref bool first);

        /// <summary>
        /// Serialize to the specified Writer. This is a dummy routine
        /// whose sole purpose is to prevent 'new' causing issues in derived
        /// classes.
        /// </summary>
        /// <param name="Writer">Writer to serialize the data to</param>
        /// <param name="first">This is the first field in the object being serialized. This 
        /// value is set to false on exit.</param>
        /// <param name="wrap">Wrap the objects for formatting.</param>
		public void SerializeX(Writer Writer, bool wrap, ref bool first) {
            }

        /// <summary>
        /// Factory method to construct object from byte data.
        /// </summary>
        /// <param name="_Data">Source</param>
        /// <returns>Constructed object</returns>
        public static JSONObject From(byte[] _Data) {
            return null;
            }

        /// <summary>
        /// Factory method to construct object from string data.
        /// </summary>
        /// <param name="_Input">Source</param>
        /// <returns>Constructed object</returns>
        public static JSONObject From(string _Input) {
            return null;
            }

        /// <summary>
        /// Factory method to construct object from tagged byte data.
        /// </summary>
        /// <param name="_Data">Source</param>
        /// <returns>Constructed object</returns>
        public static JSONObject FromTagged(byte[] _Data) {
            return null;
            }

        /// <summary>
        /// Factory method to construct object from tagged string data.
        /// </summary>
        /// <param name="_Input">Source</param>
        /// <returns>Constructed object</returns>
        public static JSONObject FromTagged(string _Input) {
            return null;
            }

        /// <summary>
        /// Factory method to construct object from tagged string data.
        /// </summary>
        /// <param name="_Input">Source</param>
        /// <returns>Constructed object</returns>
        public static JSONObject FromTagged(JSONReader _Input) {
            return null;
            }

        /// <summary>
        /// Deserialize the input string to populate this object
        /// </summary>
        /// <param name="_Input">Input string</param>
        public virtual void Deserialize (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			Deserialize (JSONReader);
			}

        /// <summary>
        /// Deserialize the input string to populate this object
        /// </summary>
        /// <param name="JSONReader">Input data</param>
        public virtual void Deserialize(JSONReader JSONReader) {

            bool Going = JSONReader.StartObject();
            while (Going) {
                string Token = JSONReader.ReadToken();
                if (Token == null) {
                    Going = false;
                    }
                else {
                    DeserializeToken(JSONReader, Token);
                    }
                Going = JSONReader.NextObject();
                }
            // JSONReader.EndObject (); Implicit 
            }

        /// <summary>
        /// Deserialize the input stream to populate this object having recieved the specified tag.
        /// </summary>
        /// <param name="JSONReader">Input data</param>
        /// <param name="Tag">Input tag</param>
        public virtual void DeserializeToken (JSONReader JSONReader, string Tag) {
			} 


        }
    }
