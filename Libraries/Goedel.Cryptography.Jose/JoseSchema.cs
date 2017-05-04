
//  Copyright (c) 2016 by .
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Goedel.Protocol;





namespace Goedel.Cryptography.Jose {


	/// <summary>
	///
	/// Support classes for JSON Object Signing and Encryption
	/// </summary>
	public abstract partial class Jose : global::Goedel.Protocol.JSONObject {

        /// <summary>
        /// Schema tag.
        /// </summary>
        /// <returns>The tag value</returns>
		public override string Tag () {
			return "Jose";
			}

		/// <summary>
        /// Construct an instance from the specified tagged JSONReader stream.
        /// </summary>
        /// <param name="JSONReader">Input stream</param>
        /// <param name="Out">The created object</param>
        public static void Deserialize(JSONReader JSONReader, out JSONObject Out) {
	
			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                Out = null;
                return;
                }

			string token = JSONReader.ReadToken ();
			Out = null;

			switch (token) {

				case "JoseWebSignature" : {
					Out = new JoseWebSignature ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "JoseWebEncryption" : {
					Out = new JoseWebEncryption ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "Signed" : {
					Out = new Signed ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "Encrypted" : {
					Out = new Encrypted ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "KeyData" : {
					Out = new KeyData ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "Header" : {
					Out = new Header ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "Signature" : {
					Out = new Signature ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "KeyContainer" : {
					Out = new KeyContainer ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "Key" : {
					Out = new Key ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "Recipient" : {
					Out = new Recipient ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "PublicKeyRSA" : {
					Out = new PublicKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "PrivateKeyRSA" : {
					Out = new PrivateKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "PublicKeyDH" : {
					Out = new PublicKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}


				case "PrivateKeyDH" : {
					Out = new PrivateKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
					throw new Exception ("Not supported");
					}
				}	
			JSONReader.EndObject ();
            }
		}



		// Service Dispatch Classes



		// Transaction Classes
	/// <summary>
	///
	/// A signed JOSE data object. The data contents are all binary encoded to 
	/// enable direct authentication of the contents.
	/// </summary>
	public partial class JoseWebSignature : Jose {
        /// <summary>
        ///Data not protected by the signature
        /// </summary>

		public virtual Header						Unprotected  {get; set;}
        /// <summary>
        ///The signed data
        /// </summary>

		public virtual byte[]						Payload  {get; set;}
        /// <summary>
        ///The signature value
        /// </summary>

		public virtual List<Signature>				Signatures  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "JoseWebSignature";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (Unprotected != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("unprotected", 1);
					Unprotected.Serialize (_Writer, false);
				}
			if (Payload != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("payload", 1);
					_Writer.WriteBinary (Payload);
				}
			if (Signatures != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("signatures", 1);
				_Writer.WriteArrayStart ();
				bool _firstarray = true;
				foreach (var _index in Signatures) {
					_Writer.WriteArraySeparator (ref _firstarray);
					// This is an untagged structure. Cannot inherit.
                    //_Writer.WriteObjectStart();
                    //_Writer.WriteToken(_index.Tag(), 1);
					bool firstinner = true;
					_index.Serialize (_Writer, true, ref firstinner);
                    //_Writer.WriteObjectEnd();
					}
				_Writer.WriteArrayEnd ();
				}

			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new JoseWebSignature From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new JoseWebSignature From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new JoseWebSignature ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new JoseWebSignature (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "JoseWebSignature" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new JoseWebSignature FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "JoseWebSignature" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new JoseWebSignature FromTagged (string _Input) {
			//JoseWebSignature _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new JoseWebSignature  FromTagged (JSONReader JSONReader) {
			JoseWebSignature Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "JoseWebSignature" : {
					Out = new JoseWebSignature ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "JoseWebEncryption" : {
					Out = new JoseWebEncryption ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "unprotected" : {
					// An untagged structure
					Unprotected = new Header ();
					Unprotected.Deserialize (JSONReader);
 
					break;
					}
				case "payload" : {
					Payload = JSONReader.ReadBinary ();
					break;
					}
				case "signatures" : {
					// Have a sequence of values
					bool _Going = JSONReader.StartArray ();
					Signatures = new List <Signature> ();
					while (_Going) {
						// an untagged structure.
						var _Item = new  Signature ();
						_Item.Deserialize (JSONReader);
						// var _Item = new Signature (JSONReader);
						Signatures.Add (_Item);
						_Going = JSONReader.NextArray ();
						}
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// A signed JOSE data object. The encrypted data contents are all binary encoded.
	/// </summary>
	public partial class JoseWebEncryption : JoseWebSignature {
        /// <summary>
        ///Data protected by the signature
        /// </summary>

		public virtual byte[]						Protected  {get; set;}
        /// <summary>
        ///The initialization vector for the bulk cipher.
        /// </summary>

		public virtual byte[]						IV  {get; set;}
        /// <summary>
        ///Per recipient decryption data.
        /// </summary>

		public virtual List<Recipient>				Recipients  {get; set;}
        /// <summary>
        ///The decryption data for use by this recipient.
        /// </summary>

		public virtual byte[]						EncryptedKey  {get; set;}
        /// <summary>
        ///Additional data that is included in the authentication scope but not the encryption
        /// </summary>

		public virtual byte[]						AdditionalAuthenticatedData  {get; set;}
        /// <summary>
        ///The encrypted data
        /// </summary>

		public virtual byte[]						CipherText  {get; set;}
        /// <summary>
        ///Authentication tag
        /// </summary>

		public virtual byte[]						JTag  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "JoseWebEncryption";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((JoseWebSignature)this).SerializeX(_Writer, false, ref _first);
			if (Protected != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("protected", 1);
					_Writer.WriteBinary (Protected);
				}
			if (IV != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("iv", 1);
					_Writer.WriteBinary (IV);
				}
			if (Recipients != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("recipients", 1);
				_Writer.WriteArrayStart ();
				bool _firstarray = true;
				foreach (var _index in Recipients) {
					_Writer.WriteArraySeparator (ref _firstarray);
					// This is an untagged structure. Cannot inherit.
                    //_Writer.WriteObjectStart();
                    //_Writer.WriteToken(_index.Tag(), 1);
					bool firstinner = true;
					_index.Serialize (_Writer, true, ref firstinner);
                    //_Writer.WriteObjectEnd();
					}
				_Writer.WriteArrayEnd ();
				}

			if (EncryptedKey != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("encrypted_key", 1);
					_Writer.WriteBinary (EncryptedKey);
				}
			if (AdditionalAuthenticatedData != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("aad", 1);
					_Writer.WriteBinary (AdditionalAuthenticatedData);
				}
			if (CipherText != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("ciphertext", 1);
					_Writer.WriteBinary (CipherText);
				}
			if (JTag != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("tag", 1);
					_Writer.WriteBinary (JTag);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new JoseWebEncryption From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new JoseWebEncryption From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new JoseWebEncryption ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new JoseWebEncryption (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "JoseWebEncryption" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new JoseWebEncryption FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "JoseWebEncryption" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new JoseWebEncryption FromTagged (string _Input) {
			//JoseWebEncryption _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new JoseWebEncryption  FromTagged (JSONReader JSONReader) {
			JoseWebEncryption Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "JoseWebEncryption" : {
					Out = new JoseWebEncryption ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "protected" : {
					Protected = JSONReader.ReadBinary ();
					break;
					}
				case "iv" : {
					IV = JSONReader.ReadBinary ();
					break;
					}
				case "recipients" : {
					// Have a sequence of values
					bool _Going = JSONReader.StartArray ();
					Recipients = new List <Recipient> ();
					while (_Going) {
						// an untagged structure.
						var _Item = new  Recipient ();
						_Item.Deserialize (JSONReader);
						// var _Item = new Recipient (JSONReader);
						Recipients.Add (_Item);
						_Going = JSONReader.NextArray ();
						}
					break;
					}
				case "encrypted_key" : {
					EncryptedKey = JSONReader.ReadBinary ();
					break;
					}
				case "aad" : {
					AdditionalAuthenticatedData = JSONReader.ReadBinary ();
					break;
					}
				case "ciphertext" : {
					CipherText = JSONReader.ReadBinary ();
					break;
					}
				case "tag" : {
					JTag = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// Compact representation for signed data
	/// </summary>
	public partial class Signed : Jose {
        /// <summary>
        ///Data protected by the signature
        /// </summary>

		public virtual byte[]						Protected  {get; set;}
        /// <summary>
        ///The authenticated data
        /// </summary>

		public virtual byte[]						Payload  {get; set;}
        /// <summary>
        ///The signature data
        /// </summary>

		public virtual byte[]						Signature  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Signed";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (Protected != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("protected", 1);
					_Writer.WriteBinary (Protected);
				}
			if (Payload != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("payload", 1);
					_Writer.WriteBinary (Payload);
				}
			if (Signature != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("signature", 1);
					_Writer.WriteBinary (Signature);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Signed From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Signed From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new Signed ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new Signed (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "Signed" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Signed FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "Signed" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Signed FromTagged (string _Input) {
			//Signed _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new Signed  FromTagged (JSONReader JSONReader) {
			Signed Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Signed" : {
					Out = new Signed ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "protected" : {
					Protected = JSONReader.ReadBinary ();
					break;
					}
				case "payload" : {
					Payload = JSONReader.ReadBinary ();
					break;
					}
				case "signature" : {
					Signature = JSONReader.ReadBinary ();
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// Compact representation for encrypted data
	/// </summary>
	public partial class Encrypted : Jose {
        /// <summary>
        ///Header
        /// </summary>

		public virtual Header						Header  {get; set;}
        /// <summary>
        ///The initialization vector for the cipher
        /// </summary>

		public virtual byte[]						IV  {get; set;}
        /// <summary>
        ///The encrypted data 
        /// </summary>

		public virtual byte[]						CipherText  {get; set;}
        /// <summary>
        ///The signature data
        /// </summary>

		public virtual byte[]						Signature  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Encrypted";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (Header != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("header", 1);
					Header.Serialize (_Writer, false);
				}
			if (IV != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("iv", 1);
					_Writer.WriteBinary (IV);
				}
			if (CipherText != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("ciphertext", 1);
					_Writer.WriteBinary (CipherText);
				}
			if (Signature != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("signature", 1);
					_Writer.WriteBinary (Signature);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Encrypted From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Encrypted From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new Encrypted ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new Encrypted (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "Encrypted" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Encrypted FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "Encrypted" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Encrypted FromTagged (string _Input) {
			//Encrypted _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new Encrypted  FromTagged (JSONReader JSONReader) {
			Encrypted Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Encrypted" : {
					Out = new Encrypted ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "header" : {
					// An untagged structure
					Header = new Header ();
					Header.Deserialize (JSONReader);
 
					break;
					}
				case "iv" : {
					IV = JSONReader.ReadBinary ();
					break;
					}
				case "ciphertext" : {
					CipherText = JSONReader.ReadBinary ();
					break;
					}
				case "signature" : {
					Signature = JSONReader.ReadBinary ();
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// Describe a cryptographic key
	/// </summary>
	public partial class KeyData : Jose {
        /// <summary>
        ///Bulk encryption algorithm for content
        /// </summary>

		public virtual string						enc  {get; set;}
        /// <summary>
        ///Digest algorithm hint
        /// </summary>

		public virtual string						dig  {get; set;}
        /// <summary>
        ///Key exchange algorithm
        /// </summary>

		public virtual string						alg  {get; set;}
        /// <summary>
        ///Key identifier. If a UDF fingerprint is used to identify the 
        ///key it is placed in this field.
        /// </summary>

		public virtual string						kid  {get; set;}
        /// <summary>
        ///URL identifying an X.509 public key certificate
        /// </summary>

		public virtual string						x5u  {get; set;}
        /// <summary>
        ///An X.509 public key certificate
        /// </summary>

		public virtual byte[]						x5c  {get; set;}
        /// <summary>
        ///SHA-1 fingerprint of X.509 certificate
        /// </summary>

		public virtual byte[]						x5t  {get; set;}
        /// <summary>
        ///SHA-2-256 fingerprint of X.509 certificate
        /// </summary>

		public virtual byte[]						x5tS256  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "KeyData";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (enc != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("enc", 1);
					_Writer.WriteString (enc);
				}
			if (dig != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("dig", 1);
					_Writer.WriteString (dig);
				}
			if (alg != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("alg", 1);
					_Writer.WriteString (alg);
				}
			if (kid != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("kid", 1);
					_Writer.WriteString (kid);
				}
			if (x5u != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("x5u", 1);
					_Writer.WriteString (x5u);
				}
			if (x5c != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("x5c", 1);
					_Writer.WriteBinary (x5c);
				}
			if (x5t != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("x5t", 1);
					_Writer.WriteBinary (x5t);
				}
			if (x5tS256 != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("x5t#S256", 1);
					_Writer.WriteBinary (x5tS256);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new KeyData From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new KeyData From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new KeyData ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new KeyData (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "KeyData" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new KeyData FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "KeyData" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new KeyData FromTagged (string _Input) {
			//KeyData _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new KeyData  FromTagged (JSONReader JSONReader) {
			KeyData Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "KeyData" : {
					Out = new KeyData ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "Header" : {
					Out = new Header ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "Key" : {
					Out = new Key ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PublicKeyRSA" : {
					Out = new PublicKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PrivateKeyRSA" : {
					Out = new PrivateKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PublicKeyDH" : {
					Out = new PublicKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PrivateKeyDH" : {
					Out = new PrivateKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "enc" : {
					enc = JSONReader.ReadString ();
					break;
					}
				case "dig" : {
					dig = JSONReader.ReadString ();
					break;
					}
				case "alg" : {
					alg = JSONReader.ReadString ();
					break;
					}
				case "kid" : {
					kid = JSONReader.ReadString ();
					break;
					}
				case "x5u" : {
					x5u = JSONReader.ReadString ();
					break;
					}
				case "x5c" : {
					x5c = JSONReader.ReadBinary ();
					break;
					}
				case "x5t" : {
					x5t = JSONReader.ReadBinary ();
					break;
					}
				case "x5t#S256" : {
					x5tS256 = JSONReader.ReadBinary ();
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// A JOSE Header.
	/// </summary>
	public partial class Header : KeyData {
        /// <summary>
        ///JWK Set URL
        /// </summary>

		public virtual string						jku  {get; set;}
        /// <summary>
        ///The key identifier
        /// </summary>

		public virtual string						jwk  {get; set;}
        /// <summary>
        ///Another IANA content type parameter
        /// </summary>

		public virtual string						typ  {get; set;}
        /// <summary>
        ///Content type parameter
        /// </summary>

		public virtual string						cty  {get; set;}
        /// <summary>
        ///List of header parameters that a recipient MUST understand to interpret
        ///the authentication portion of the JOSE object.
        /// </summary>

		public virtual List<string>				crit  {get; set;}
        /// <summary>
        ///The digest value
        /// </summary>

		public virtual byte[]						val  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Header";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((KeyData)this).SerializeX(_Writer, false, ref _first);
			if (jku != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("jku", 1);
					_Writer.WriteString (jku);
				}
			if (jwk != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("jwk", 1);
					_Writer.WriteString (jwk);
				}
			if (typ != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("typ", 1);
					_Writer.WriteString (typ);
				}
			if (cty != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("cty", 1);
					_Writer.WriteString (cty);
				}
			if (crit != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("crit", 1);
				_Writer.WriteArrayStart ();
				bool _firstarray = true;
				foreach (var _index in crit) {
					_Writer.WriteArraySeparator (ref _firstarray);
					_Writer.WriteString (_index);
					}
				_Writer.WriteArrayEnd ();
				}

			if (val != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("val", 1);
					_Writer.WriteBinary (val);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Header From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Header From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new Header ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new Header (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "Header" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Header FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "Header" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Header FromTagged (string _Input) {
			//Header _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new Header  FromTagged (JSONReader JSONReader) {
			Header Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Header" : {
					Out = new Header ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "jku" : {
					jku = JSONReader.ReadString ();
					break;
					}
				case "jwk" : {
					jwk = JSONReader.ReadString ();
					break;
					}
				case "typ" : {
					typ = JSONReader.ReadString ();
					break;
					}
				case "cty" : {
					cty = JSONReader.ReadString ();
					break;
					}
				case "crit" : {
					// Have a sequence of values
					bool _Going = JSONReader.StartArray ();
					crit = new List <string> ();
					while (_Going) {
						string _Item = JSONReader.ReadString ();
						crit.Add (_Item);
						_Going = JSONReader.NextArray ();
						}
					break;
					}
				case "val" : {
					val = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	/// </summary>
	public partial class Signature : Jose {
        /// <summary>
        ///The signature header
        /// </summary>

		public virtual Header						Header  {get; set;}
        /// <summary>
        ///Data protected by the signature
        /// </summary>

		public virtual byte[]						Protected  {get; set;}
        /// <summary>
        ///The signature value
        /// </summary>

		public virtual byte[]						SignatureValue  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Signature";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (Header != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("header", 1);
					Header.Serialize (_Writer, false);
				}
			if (Protected != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("protected", 1);
					_Writer.WriteBinary (Protected);
				}
			if (SignatureValue != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("signature", 1);
					_Writer.WriteBinary (SignatureValue);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Signature From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Signature From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new Signature ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new Signature (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "Signature" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Signature FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "Signature" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Signature FromTagged (string _Input) {
			//Signature _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new Signature  FromTagged (JSONReader JSONReader) {
			Signature Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Signature" : {
					Out = new Signature ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "header" : {
					// An untagged structure
					Header = new Header ();
					Header.Deserialize (JSONReader);
 
					break;
					}
				case "protected" : {
					Protected = JSONReader.ReadBinary ();
					break;
					}
				case "signature" : {
					SignatureValue = JSONReader.ReadBinary ();
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// A wrapper object for storing key data.
	/// </summary>
	public partial class KeyContainer : Jose {
		bool								__Exportable = false;
		private bool						_Exportable;
        /// <summary>
        ///If false a handler library MUST NOT permit the private key to be exported.
        /// </summary>

		public virtual bool						Exportable {
			get {return _Exportable;}
			set {_Exportable = value; __Exportable = true; }
			}
        /// <summary>
        ///The key data.
        /// </summary>

		public virtual byte[]						KeyData  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "KeyContainer";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (__Exportable){
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("Exportable", 1);
					_Writer.WriteBoolean (Exportable);
				}
			if (KeyData != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("KeyData", 1);
					_Writer.WriteBinary (KeyData);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new KeyContainer From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new KeyContainer From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new KeyContainer ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new KeyContainer (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "KeyContainer" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new KeyContainer FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "KeyContainer" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new KeyContainer FromTagged (string _Input) {
			//KeyContainer _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new KeyContainer  FromTagged (JSONReader JSONReader) {
			KeyContainer Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "KeyContainer" : {
					Out = new KeyContainer ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "Exportable" : {
					Exportable = JSONReader.ReadBoolean ();
					break;
					}
				case "KeyData" : {
					KeyData = JSONReader.ReadBinary ();
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// A JOSE key. All fields map onto the equivalent fields defined in
	/// RFC 7517
	/// </summary>
	public partial class Key : KeyData {
        /// <summary>
        ///Key type
        /// </summary>

		public virtual string						kty  {get; set;}
        /// <summary>
        ///Public Key use
        /// </summary>

		public virtual string						use  {get; set;}
        /// <summary>
        ///Key operations
        /// </summary>

		public virtual string						key_ops  {get; set;}
        /// <summary>
        ///Symmetric key value.
        /// </summary>

		public virtual byte[]						k  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Key";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((KeyData)this).SerializeX(_Writer, false, ref _first);
			if (kty != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("kty", 1);
					_Writer.WriteString (kty);
				}
			if (use != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("use", 1);
					_Writer.WriteString (use);
				}
			if (key_ops != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("key_ops", 1);
					_Writer.WriteString (key_ops);
				}
			if (k != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("k", 1);
					_Writer.WriteBinary (k);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Key From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Key From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new Key ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new Key (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "Key" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Key FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "Key" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Key FromTagged (string _Input) {
			//Key _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new Key  FromTagged (JSONReader JSONReader) {
			Key Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Key" : {
					Out = new Key ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PublicKeyRSA" : {
					Out = new PublicKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PrivateKeyRSA" : {
					Out = new PrivateKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PublicKeyDH" : {
					Out = new PublicKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PrivateKeyDH" : {
					Out = new PrivateKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "kty" : {
					kty = JSONReader.ReadString ();
					break;
					}
				case "use" : {
					use = JSONReader.ReadString ();
					break;
					}
				case "key_ops" : {
					key_ops = JSONReader.ReadString ();
					break;
					}
				case "k" : {
					k = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// Recipient information
	/// </summary>
	public partial class Recipient : Jose {
        /// <summary>
        ///Specify the recipient and per recipient data
        /// </summary>

		public virtual Header						Header  {get; set;}
        /// <summary>
        ///The decryption data for use by this recipient.
        /// </summary>

		public virtual byte[]						EncryptedKey  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Recipient";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			if (Header != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("Header", 1);
					Header.Serialize (_Writer, false);
				}
			if (EncryptedKey != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("encrypted_key", 1);
					_Writer.WriteBinary (EncryptedKey);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Recipient From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Recipient From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new Recipient ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new Recipient (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "Recipient" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new Recipient FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "Recipient" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new Recipient FromTagged (string _Input) {
			//Recipient _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new Recipient  FromTagged (JSONReader JSONReader) {
			Recipient Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Recipient" : {
					Out = new Recipient ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "Header" : {
					// An untagged structure
					Header = new Header ();
					Header.Deserialize (JSONReader);
 
					break;
					}
				case "encrypted_key" : {
					EncryptedKey = JSONReader.ReadBinary ();
					break;
					}
				default : {
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// An RSA Public key
	/// </summary>
	public partial class PublicKeyRSA : Key {
        /// <summary>
        ///The public modulus
        /// </summary>

		public virtual byte[]						n  {get; set;}
        /// <summary>
        ///The public exponent
        /// </summary>

		public virtual byte[]						e  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "PublicKeyRSA";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((Key)this).SerializeX(_Writer, false, ref _first);
			if (n != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("n", 1);
					_Writer.WriteBinary (n);
				}
			if (e != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("e", 1);
					_Writer.WriteBinary (e);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PublicKeyRSA From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PublicKeyRSA From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new PublicKeyRSA ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new PublicKeyRSA (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "PublicKeyRSA" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PublicKeyRSA FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "PublicKeyRSA" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PublicKeyRSA FromTagged (string _Input) {
			//PublicKeyRSA _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new PublicKeyRSA  FromTagged (JSONReader JSONReader) {
			PublicKeyRSA Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "PublicKeyRSA" : {
					Out = new PublicKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PrivateKeyRSA" : {
					Out = new PrivateKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "n" : {
					n = JSONReader.ReadBinary ();
					break;
					}
				case "e" : {
					e = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// RSA private key parameters
	/// </summary>
	public partial class PrivateKeyRSA : PublicKeyRSA {
        /// <summary>
        ///The parameter d
        /// </summary>

		public virtual byte[]						d  {get; set;}
        /// <summary>
        ///The parameter p
        /// </summary>

		public virtual byte[]						p  {get; set;}
        /// <summary>
        ///The parameter q
        /// </summary>

		public virtual byte[]						q  {get; set;}
        /// <summary>
        ///The parameter dp
        /// </summary>

		public virtual byte[]						dp  {get; set;}
        /// <summary>
        ///The parameter dq
        /// </summary>

		public virtual byte[]						dq  {get; set;}
        /// <summary>
        ///The parameter QInverse
        /// </summary>

		public virtual byte[]						qi  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "PrivateKeyRSA";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((PublicKeyRSA)this).SerializeX(_Writer, false, ref _first);
			if (d != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("d", 1);
					_Writer.WriteBinary (d);
				}
			if (p != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("p", 1);
					_Writer.WriteBinary (p);
				}
			if (q != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("q", 1);
					_Writer.WriteBinary (q);
				}
			if (dp != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("dp", 1);
					_Writer.WriteBinary (dp);
				}
			if (dq != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("dq", 1);
					_Writer.WriteBinary (dq);
				}
			if (qi != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("qi", 1);
					_Writer.WriteBinary (qi);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PrivateKeyRSA From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PrivateKeyRSA From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new PrivateKeyRSA ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new PrivateKeyRSA (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "PrivateKeyRSA" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PrivateKeyRSA FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "PrivateKeyRSA" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PrivateKeyRSA FromTagged (string _Input) {
			//PrivateKeyRSA _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new PrivateKeyRSA  FromTagged (JSONReader JSONReader) {
			PrivateKeyRSA Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "PrivateKeyRSA" : {
					Out = new PrivateKeyRSA ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "d" : {
					d = JSONReader.ReadBinary ();
					break;
					}
				case "p" : {
					p = JSONReader.ReadBinary ();
					break;
					}
				case "q" : {
					q = JSONReader.ReadBinary ();
					break;
					}
				case "dp" : {
					dp = JSONReader.ReadBinary ();
					break;
					}
				case "dq" : {
					dq = JSONReader.ReadBinary ();
					break;
					}
				case "qi" : {
					qi = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// A Diffie Helllman Public key
	/// </summary>
	public partial class PublicKeyDH : Key {
        /// <summary>
        ///The fingerprint of the domain
        /// </summary>

		public virtual byte[]						Domain  {get; set;}
        /// <summary>
        ///The public key
        /// </summary>

		public virtual byte[]						Public  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "PublicKeyDH";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((Key)this).SerializeX(_Writer, false, ref _first);
			if (Domain != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("Domain", 1);
					_Writer.WriteBinary (Domain);
				}
			if (Public != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("Public", 1);
					_Writer.WriteBinary (Public);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PublicKeyDH From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PublicKeyDH From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new PublicKeyDH ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new PublicKeyDH (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "PublicKeyDH" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PublicKeyDH FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "PublicKeyDH" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PublicKeyDH FromTagged (string _Input) {
			//PublicKeyDH _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new PublicKeyDH  FromTagged (JSONReader JSONReader) {
			PublicKeyDH Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "PublicKeyDH" : {
					Out = new PublicKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				case "PrivateKeyDH" : {
					Out = new PrivateKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "Domain" : {
					Domain = JSONReader.ReadBinary ();
					break;
					}
				case "Public" : {
					Public = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	/// <summary>
	///
	/// Diffie Helllman private key parameters
	/// </summary>
	public partial class PrivateKeyDH : PublicKeyDH {
        /// <summary>
        ///The private key
        /// </summary>

		public virtual byte[]						Private  {get; set;}

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "PrivateKeyDH";
			}



        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize (Writer Writer, bool wrap, ref bool first) {
			SerializeX (Writer, wrap, ref first);
			}

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {
			if (_wrap) {
				_Writer.WriteObjectStart ();
				}
			((PublicKeyDH)this).SerializeX(_Writer, false, ref _first);
			if (Private != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("Private", 1);
					_Writer.WriteBinary (Private);
				}
			if (_wrap) {
				_Writer.WriteObjectEnd ();
				}
			}



        /// <summary>
		/// Create a new instance from untagged byte input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PrivateKeyDH From (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return From (_Input);
			}

        /// <summary>
		/// Create a new instance from untagged string input.
		/// i.e. {... data ... }
        /// </summary>	
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PrivateKeyDH From (string _Input) {
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			var Result = new PrivateKeyDH ();
			Result.Deserialize (JSONReader);
			return Result;
			// return new PrivateKeyDH (JSONReader);
			}

        /// <summary>
		/// Create a new instance from tagged byte input.
		/// i.e. { "PrivateKeyDH" : {... data ... } }
        /// </summary>	
        /// <param name="_Data">The input data.</param>
        /// <returns>The created object.</returns>				
		public static new PrivateKeyDH FromTagged (byte[] _Data) {
			var _Input = System.Text.Encoding.UTF8.GetString(_Data);
			return FromTagged (_Input);
			}

        /// <summary>
        /// Create a new instance from tagged string input.
		/// i.e. { "PrivateKeyDH" : {... data ... } }
        /// </summary>
        /// <param name="_Input">The input data.</param>
        /// <returns>The created object.</returns>		
		public static new PrivateKeyDH FromTagged (string _Input) {
			//PrivateKeyDH _Result;
			//Deserialize (_Input, out _Result);
			StringReader _Reader = new StringReader (_Input);
            JSONReader JSONReader = new JSONReader (_Reader);
			return FromTagged (JSONReader) ;
			}


        /// <summary>
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <returns>The created object.</returns>		
        public static new PrivateKeyDH  FromTagged (JSONReader JSONReader) {
			PrivateKeyDH Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "PrivateKeyDH" : {
					Out = new PrivateKeyDH ();
					Out.Deserialize (JSONReader);
					break;
					}

				default : {
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
        /// <param name="Tag">The tag</param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "Private" : {
					Private = JSONReader.ReadBinary ();
					break;
					}
				default : {
					base.DeserializeToken(JSONReader, Tag);
					break;
					}
				}
			// check up that all the required elements are present
			}


		}

	}

