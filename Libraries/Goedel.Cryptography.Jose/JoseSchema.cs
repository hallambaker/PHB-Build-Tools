
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
    /// </summary>
	public abstract partial class Jose : global::Goedel.Protocol.JSONObject {

        /// <summary>
        /// 
        /// </summary>
		public override string Tag () {
			return "Jose";
			}

        /// <summary>
        /// Default constructor.
        /// </summary>
		public Jose () {
			_Initialize () ;
			}

        /// <summary>
        /// Construct an instance from a JSON encoded stream.
        /// </summary>
		public Jose (JSONReader JSONReader) {
			Deserialize (JSONReader);
			_Initialize () ;
			}

        /// <summary>
        /// Construct an instance from a JSON encoded string.
        /// </summary>
		public Jose (string _String) {
			Deserialize (_String);
			_Initialize () ;
			}

		/// <summary>
        /// Construct an instance from the specified tagged JSONReader stream.
        /// </summary>
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
					var Result = new JoseWebSignature ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "JoseWebEncryption" : {
					var Result = new JoseWebEncryption ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "Signed" : {
					var Result = new Signed ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "Encrypted" : {
					var Result = new Encrypted ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "KeyData" : {
					var Result = new KeyData ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "Header" : {
					var Result = new Header ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "Key" : {
					var Result = new Key ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "Recipient" : {
					var Result = new Recipient ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "PublicKeyRSA" : {
					var Result = new PublicKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}


				case "PrivateKeyRSA" : {
					var Result = new PrivateKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
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
	/// </summary>
	public partial class JoseWebSignature : Jose {
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Protected {
			get {return _Protected;}			
			set {_Protected = value;}
			}
		byte[]						_Protected ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Header {
			get {return _Header;}			
			set {_Header = value;}
			}
		byte[]						_Header ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Payload {
			get {return _Payload;}			
			set {_Payload = value;}
			}
		byte[]						_Payload ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Signature {
			get {return _Signature;}			
			set {_Signature = value;}
			}
		byte[]						_Signature ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "JoseWebSignature";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public JoseWebSignature () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public JoseWebSignature (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public JoseWebSignature (string _String) {
			Deserialize (_String);
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
			if (Header != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("header", 1);
					_Writer.WriteBinary (Header);
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
			return new JoseWebSignature (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new JoseWebSignature  FromTagged (JSONReader JSONReader) {
			JoseWebSignature Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "JoseWebSignature" : {
					var Result = new JoseWebSignature ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "protected" : {
					Protected = JSONReader.ReadBinary ();
					break;
					}
				case "header" : {
					Header = JSONReader.ReadBinary ();
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
	/// </summary>
	public partial class JoseWebEncryption : Jose {
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Protected {
			get {return _Protected;}			
			set {_Protected = value;}
			}
		byte[]						_Protected ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Unprotected {
			get {return _Unprotected;}			
			set {_Unprotected = value;}
			}
		byte[]						_Unprotected ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						IV {
			get {return _IV;}			
			set {_IV = value;}
			}
		byte[]						_IV ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						CipherText {
			get {return _CipherText;}			
			set {_CipherText = value;}
			}
		byte[]						_CipherText ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						JTag {
			get {return _JTag;}			
			set {_JTag = value;}
			}
		byte[]						_JTag ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						AdditionalAuthenticatedData {
			get {return _AdditionalAuthenticatedData;}			
			set {_AdditionalAuthenticatedData = value;}
			}
		byte[]						_AdditionalAuthenticatedData ;
		/// <summary>
        /// 
        /// </summary>
		public virtual List<Recipient>				Recipients {
			get {return _Recipients;}			
			set {_Recipients = value;}
			}
		List<Recipient>				_Recipients;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "JoseWebEncryption";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public JoseWebEncryption () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public JoseWebEncryption (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public JoseWebEncryption (string _String) {
			Deserialize (_String);
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
			if (Unprotected != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("unprotected", 1);
					_Writer.WriteBinary (Unprotected);
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
			if (JTag != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("tag", 1);
					_Writer.WriteBinary (JTag);
				}
			if (AdditionalAuthenticatedData != null) {
				_Writer.WriteObjectSeparator (ref _first);
				_Writer.WriteToken ("aad", 1);
					_Writer.WriteBinary (AdditionalAuthenticatedData);
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
			return new JoseWebEncryption (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new JoseWebEncryption  FromTagged (JSONReader JSONReader) {
			JoseWebEncryption Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "JoseWebEncryption" : {
					var Result = new JoseWebEncryption ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "protected" : {
					Protected = JSONReader.ReadBinary ();
					break;
					}
				case "unprotected" : {
					Unprotected = JSONReader.ReadBinary ();
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
				case "tag" : {
					JTag = JSONReader.ReadBinary ();
					break;
					}
				case "aad" : {
					AdditionalAuthenticatedData = JSONReader.ReadBinary ();
					break;
					}
				case "recipients" : {
					// Have a sequence of values
					bool _Going = JSONReader.StartArray ();
					Recipients = new List <Recipient> ();
					while (_Going) {
						// an untagged structure.
						var _Item = new Recipient (JSONReader);
						Recipients.Add (_Item);
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
	/// </summary>
	public partial class Signed : Jose {
        /// <summary>
        /// 
        /// </summary>
		public virtual Header						Header {
			get {return _Header;}			
			set {_Header = value;}
			}
		Header						_Header ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Payload {
			get {return _Payload;}			
			set {_Payload = value;}
			}
		byte[]						_Payload ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Signature {
			get {return _Signature;}			
			set {_Signature = value;}
			}
		byte[]						_Signature ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Signed";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public Signed () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public Signed (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public Signed (string _String) {
			Deserialize (_String);
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
			return new Signed (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new Signed  FromTagged (JSONReader JSONReader) {
			Signed Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Signed" : {
					var Result = new Signed ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "header" : {
					// An untagged structure
					Header = new Header (JSONReader);
 
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
	/// </summary>
	public partial class Encrypted : Jose {
        /// <summary>
        /// 
        /// </summary>
		public virtual Header						Header {
			get {return _Header;}			
			set {_Header = value;}
			}
		Header						_Header ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						IV {
			get {return _IV;}			
			set {_IV = value;}
			}
		byte[]						_IV ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						CipherText {
			get {return _CipherText;}			
			set {_CipherText = value;}
			}
		byte[]						_CipherText ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						Signature {
			get {return _Signature;}			
			set {_Signature = value;}
			}
		byte[]						_Signature ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Encrypted";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public Encrypted () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public Encrypted (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public Encrypted (string _String) {
			Deserialize (_String);
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
			return new Encrypted (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new Encrypted  FromTagged (JSONReader JSONReader) {
			Encrypted Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Encrypted" : {
					var Result = new Encrypted ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "header" : {
					// An untagged structure
					Header = new Header (JSONReader);
 
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
	/// </summary>
	public partial class KeyData : Jose {
        /// <summary>
        /// 
        /// </summary>
		public virtual string						enc {
			get {return _enc;}			
			set {_enc = value;}
			}
		string						_enc ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						alg {
			get {return _alg;}			
			set {_alg = value;}
			}
		string						_alg ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						kid {
			get {return _kid;}			
			set {_kid = value;}
			}
		string						_kid ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						x5u {
			get {return _x5u;}			
			set {_x5u = value;}
			}
		string						_x5u ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						x5c {
			get {return _x5c;}			
			set {_x5c = value;}
			}
		byte[]						_x5c ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						x5t {
			get {return _x5t;}			
			set {_x5t = value;}
			}
		byte[]						_x5t ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						x5tS256 {
			get {return _x5tS256;}			
			set {_x5tS256 = value;}
			}
		byte[]						_x5tS256 ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "KeyData";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public KeyData () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public KeyData (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public KeyData (string _String) {
			Deserialize (_String);
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
			return new KeyData (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new KeyData  FromTagged (JSONReader JSONReader) {
			KeyData Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "KeyData" : {
					var Result = new KeyData ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "Header" : {
					var Result = new Header ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "Key" : {
					var Result = new Key ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "PublicKeyRSA" : {
					var Result = new PublicKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "PrivateKeyRSA" : {
					var Result = new PrivateKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "enc" : {
					enc = JSONReader.ReadString ();
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
	/// </summary>
	public partial class Header : KeyData {
        /// <summary>
        /// 
        /// </summary>
		public virtual string						jku {
			get {return _jku;}			
			set {_jku = value;}
			}
		string						_jku ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						jwk {
			get {return _jwk;}			
			set {_jwk = value;}
			}
		string						_jwk ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						typ {
			get {return _typ;}			
			set {_typ = value;}
			}
		string						_typ ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						cty {
			get {return _cty;}			
			set {_cty = value;}
			}
		string						_cty ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						crit {
			get {return _crit;}			
			set {_crit = value;}
			}
		string						_crit ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Header";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public Header () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public Header (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public Header (string _String) {
			Deserialize (_String);
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
					_Writer.WriteString (crit);
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
			return new Header (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new Header  FromTagged (JSONReader JSONReader) {
			Header Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Header" : {
					var Result = new Header ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
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
					crit = JSONReader.ReadString ();
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
	public partial class Key : KeyData {
        /// <summary>
        /// 
        /// </summary>
		public virtual string						kty {
			get {return _kty;}			
			set {_kty = value;}
			}
		string						_kty ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						use {
			get {return _use;}			
			set {_use = value;}
			}
		string						_use ;
        /// <summary>
        /// 
        /// </summary>
		public virtual string						key_ops {
			get {return _key_ops;}			
			set {_key_ops = value;}
			}
		string						_key_ops ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						k {
			get {return _k;}			
			set {_k = value;}
			}
		byte[]						_k ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Key";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public Key () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public Key (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public Key (string _String) {
			Deserialize (_String);
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
			return new Key (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new Key  FromTagged (JSONReader JSONReader) {
			Key Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Key" : {
					var Result = new Key ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "PublicKeyRSA" : {
					var Result = new PublicKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "PrivateKeyRSA" : {
					var Result = new PrivateKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
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
	/// </summary>
	public partial class Recipient : Jose {
        /// <summary>
        /// 
        /// </summary>
		public virtual Header						Header {
			get {return _Header;}			
			set {_Header = value;}
			}
		Header						_Header ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						EncryptedKey {
			get {return _EncryptedKey;}			
			set {_EncryptedKey = value;}
			}
		byte[]						_EncryptedKey ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "Recipient";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public Recipient () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public Recipient (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public Recipient (string _String) {
			Deserialize (_String);
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
			return new Recipient (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new Recipient  FromTagged (JSONReader JSONReader) {
			Recipient Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "Recipient" : {
					var Result = new Recipient ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken (JSONReader JSONReader, string Tag) {
			
			switch (Tag) {
				case "Header" : {
					// An untagged structure
					Header = new Header (JSONReader);
 
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
	/// </summary>
	public partial class PublicKeyRSA : Key {
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						n {
			get {return _n;}			
			set {_n = value;}
			}
		byte[]						_n ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						e {
			get {return _e;}			
			set {_e = value;}
			}
		byte[]						_e ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "PublicKeyRSA";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public PublicKeyRSA () {
			_Initialize ();
			}

        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public PublicKeyRSA (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public PublicKeyRSA (string _String) {
			Deserialize (_String);
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
			return new PublicKeyRSA (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new PublicKeyRSA  FromTagged (JSONReader JSONReader) {
			PublicKeyRSA Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "PublicKeyRSA" : {
					var Result = new PublicKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				case "PrivateKeyRSA" : {
					var Result = new PrivateKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
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
	/// </summary>
	public partial class PrivateKeyRSA : PublicKeyRSA {
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						d {
			get {return _d;}			
			set {_d = value;}
			}
		byte[]						_d ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						p {
			get {return _p;}			
			set {_p = value;}
			}
		byte[]						_p ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						q {
			get {return _q;}			
			set {_q = value;}
			}
		byte[]						_q ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						dp {
			get {return _dp;}			
			set {_dp = value;}
			}
		byte[]						_dp ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						dq {
			get {return _dq;}			
			set {_dq = value;}
			}
		byte[]						_dq ;
        /// <summary>
        /// 
        /// </summary>
		public virtual byte[]						qi {
			get {return _qi;}			
			set {_qi = value;}
			}
		byte[]						_qi ;

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag () {
			return "PrivateKeyRSA";
			}

        /// <summary>
        /// Default Constructor
        /// </summary>
		public PrivateKeyRSA () {
			_Initialize ();
			}
        /// <summary>
		/// Initialize class from JSONReader stream.
        /// </summary>		
		public PrivateKeyRSA (JSONReader JSONReader) {
			Deserialize (JSONReader);
			}

        /// <summary> 
		/// Initialize class from a JSON encoded class.
        /// </summary>		
		public PrivateKeyRSA (string _String) {
			Deserialize (_String);
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
			return new PrivateKeyRSA (JSONReader);
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
        /// <param name="JSONReader"></param>
        public static new PrivateKeyRSA  FromTagged (JSONReader JSONReader) {
			PrivateKeyRSA Out = null;

			JSONReader.StartObject ();
            if (JSONReader.EOR) {
                return null;
                }

			string token = JSONReader.ReadToken ();

			switch (token) {

				case "PrivateKeyRSA" : {
					var Result = new PrivateKeyRSA ();
					Result.Deserialize (JSONReader);
					Out = Result;
					break;
					}

				default : {
					//Ignore the unknown data
                    //throw new Exception ("Not supported");
                    break;
					}
				}
			JSONReader.EndObject ();

			return Out;
			}


        /// <summary>
        /// Having read a tag, process the corresponding value data.
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
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

	}

