
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
			return _Tag;
			}

		/// <summary>
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Jose";

		/// <summary>
        /// Dictionary mapping tags to factory methods
        /// </summary>
		public static Dictionary<string, JSONFactoryDelegate> _TagDictionary = 
				new Dictionary<string, JSONFactoryDelegate> () {

			{"JoseWebSignature", JoseWebSignature._Factory},
			{"JoseWebEncryption", JoseWebEncryption._Factory},
			{"Signed", Signed._Factory},
			{"Encrypted", Encrypted._Factory},
			{"KeyData", KeyData._Factory},
			{"Header", Header._Factory},
			{"Signature", Signature._Factory},
			{"KeyContainer", KeyContainer._Factory},
			{"Key", Key._Factory},
			{"Recipient", Recipient._Factory},
			{"PublicKeyRSA", PublicKeyRSA._Factory},
			{"PrivateKeyRSA", PrivateKeyRSA._Factory},
			{"PublicKeyDH", PublicKeyDH._Factory},
			{"PrivateKeyDH", PrivateKeyDH._Factory}			};

		/// <summary>
        /// Construct an instance from the specified tagged JSONReader stream.
        /// </summary>
        /// <param name="JSONReader">Input stream</param>
        /// <param name="Out">The created object</param>
        public static void Deserialize(JSONReader JSONReader, out JSONObject Out) {
			Out = JSONReader.ReadTaggedObject (_TagDictionary);
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "JoseWebSignature";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new JoseWebSignature();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new JoseWebSignature FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as JoseWebSignature;
				}
		    var Result = new JoseWebSignature ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "JoseWebEncryption";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new JoseWebEncryption();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new JoseWebEncryption FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as JoseWebEncryption;
				}
		    var Result = new JoseWebEncryption ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Signed";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new Signed();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new Signed FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as Signed;
				}
		    var Result = new Signed ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Encrypted";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new Encrypted();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new Encrypted FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as Encrypted;
				}
		    var Result = new Encrypted ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "KeyData";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new KeyData();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new KeyData FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as KeyData;
				}
		    var Result = new KeyData ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Header";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new Header();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new Header FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as Header;
				}
		    var Result = new Header ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Signature";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new Signature();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new Signature FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as Signature;
				}
		    var Result = new Signature ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "KeyContainer";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new KeyContainer();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new KeyContainer FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as KeyContainer;
				}
		    var Result = new KeyContainer ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Key";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new Key();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new Key FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as Key;
				}
		    var Result = new Key ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "Recipient";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new Recipient();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new Recipient FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as Recipient;
				}
		    var Result = new Recipient ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "PublicKeyRSA";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new PublicKeyRSA();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new PublicKeyRSA FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as PublicKeyRSA;
				}
		    var Result = new PublicKeyRSA ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "PrivateKeyRSA";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new PrivateKeyRSA();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new PrivateKeyRSA FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as PrivateKeyRSA;
				}
		    var Result = new PrivateKeyRSA ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "PublicKeyDH";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new PublicKeyDH();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new PublicKeyDH FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as PublicKeyDH;
				}
		    var Result = new PublicKeyDH ();
			Result.Deserialize (JSONReader);
			return Result;
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
        /// Tag identifying this class
        /// </summary>
		public override string _Tag { get; } = "PrivateKeyDH";

		/// <summary>
        /// Factory method
        /// </summary>
        /// <returns>Object of this type</returns>
		public static new JSONObject _Factory () {
			return new PrivateKeyDH();
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
        /// Deserialize a tagged stream
        /// </summary>
        /// <param name="JSONReader">The input stream</param>
		/// <param name="Tagged">If true, the input is wrapped in a tag specifying the type</param>
        /// <returns>The created object.</returns>		
        public static new PrivateKeyDH FromJSON (JSONReader JSONReader, bool Tagged=true) {
			if (Tagged) {
				var Out = JSONReader.ReadTaggedObject (_TagDictionary);
				return Out as PrivateKeyDH;
				}
		    var Result = new PrivateKeyDH ();
			Result.Deserialize (JSONReader);
			return Result;
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

