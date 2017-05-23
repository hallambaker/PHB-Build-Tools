// Script Syntax Version:  1.0

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
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ProtoGen {
	public partial class Generate : global::Goedel.Registry.Script {

		 bool OldConstructors = false;
		 Separator Separator = new Separator (",");
		//
		//   Code Generator (C#)
		//
		//  
		

		//
		// GenerateCS
		//
		public void GenerateCS (ProtoStruct ProtoStruct) {
			 ProtoStruct.Complete ();
			 var GenerateTime =System.DateTime.UtcNow;
			 Boilerplate.MITLicense (_Output, "//  ", "Copyright (c) " + "2016", ".");
			 var InheritsOverride = "override"; // "virtual"
			 string Namespace;
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.IO;\n{0}", _Indent);
			_Output.Write ("using System.Collections;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using System.Text;\n{0}", _Indent);
			_Output.Write ("using Goedel.Protocol;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var Item in ProtoStruct.Top) {
				switch (Item._Tag ()) {
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					foreach  (var Entry in Protocol.Entries) {
						switch (Entry._Tag ()) {
							case ProtoStructType.Using: {
							  Using Using = (Using) Entry; 
							_Output.Write ("using {1};\n{0}", _Indent, Using.Id);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					
					 Namespace = Protocol.Namespace.ToString ();
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Namespace);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					
					 DescriptionListC (Protocol.Entries, 1);
					_Output.Write ("	public abstract partial class {1} {2} {{\n{0}", _Indent, Protocol.Prefix, Protocol.ThisInherits);
					
					 CurrentPrefix = Protocol.Prefix.ToString ();
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Schema tag.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <returns>The tag value</returns>\n{0}", _Indent);
					_Output.Write ("		public {1} string Tag () {{\n{0}", _Indent, InheritsOverride);
					_Output.Write ("			return _Tag;\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Tag identifying this class\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("		public override string _Tag {{ get; }} = \"{1}\";\n{0}", _Indent, Protocol.Prefix);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Dictionary mapping tags to factory methods\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("		public static Dictionary<string, JSONFactoryDelegate> _TagDictionary = \n{0}", _Indent);
					_Output.Write ("				new Dictionary<string, JSONFactoryDelegate> () {{\n{0}", _Indent);
					
					 Separator.IsFirst = true;
					foreach  (_Choice Entry in Protocol.Structures) {
						_Output.Write ("{1}\n{0}", _Indent, Separator);
						_Output.Write ("			{{\"{1}\", {2}._Factory}}", _Indent, Entry.ID, Entry.ID);
						}
					_Output.Write ("			}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Factory method. Throws exception as this is an abstract class.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <returns>Object of this type</returns>\n{0}", _Indent);
					_Output.Write ("		public static JSONObject _Factory () {{\n{0}", _Indent);
					_Output.Write ("			throw new CannotCreateAbstract();\n{0}", _Indent);
					_Output.Write ("			}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					_Output.Write ("        /// Construct an instance from the specified tagged JSONReader stream.\n{0}", _Indent);
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"JSONReader\">Input stream</param>\n{0}", _Indent);
					_Output.Write ("        /// <param name=\"Out\">The created object</param>\n{0}", _Indent);
					_Output.Write ("        public static void Deserialize(JSONReader JSONReader, out JSONObject Out) {{\n{0}", _Indent);
					_Output.Write ("			Out = JSONReader.ReadTaggedObject (_TagDictionary);\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					if (  (OldConstructors) ) {
						_Output.Write ("			JSONReader.StartObject ();\n{0}", _Indent);
						_Output.Write ("            if (JSONReader.EOR) {{\n{0}", _Indent);
						_Output.Write ("                Out = null;\n{0}", _Indent);
						_Output.Write ("                return;\n{0}", _Indent);
						_Output.Write ("                }}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("			string token = JSONReader.ReadToken ();\n{0}", _Indent);
						_Output.Write ("			Out = null;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("			switch (token) {{\n{0}", _Indent);
						foreach  (_Choice Entry in Protocol.Entries) {
							switch (Entry._Tag ()) {
								case ProtoStructType.Message: {
								  Message Message = (Message) Entry; 
								
								 DeserializeCase (Message.Id,Message.ID);
								break; }
								case ProtoStructType.Structure: {
								  Structure Structure = (Structure) Entry; 
								
								 DeserializeCase (Structure.Id,Structure.ID);
							break; }
								}
							}
						_Output.Write ("				default : {{\n{0}", _Indent);
						_Output.Write ("					throw new Exception (\"Not supported\");\n{0}", _Indent);
						_Output.Write ("					}}\n{0}", _Indent);
						_Output.Write ("				}}	\n{0}", _Indent);
						_Output.Write ("			JSONReader.EndObject ();\n{0}", _Indent);
						}
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		// Service Dispatch Classes\n{0}", _Indent);
					foreach  (_Choice Entry in Protocol.Entries) {
						switch (Entry._Tag ()) {
							case ProtoStructType.Service: {
							  Service Service = (Service) Entry; 
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("    /// <summary>\n{0}", _Indent);
							_Output.Write ("	/// The new base class for the client and service side APIs.\n{0}", _Indent);
							_Output.Write ("    /// </summary>		\n{0}", _Indent);
							_Output.Write ("    public abstract partial class {1} : Goedel.Protocol.JPCInterface {{\n{0}", _Indent, Service.Id);
							_Output.Write ("		\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							_Output.Write ("		public const string WellKnown = \"{1}\";\n{0}", _Indent, Service.WellKnown);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							_Output.Write ("		public override string GetWellKnown {{\n{0}", _Indent);
							_Output.Write ("			get => WellKnown;\n{0}", _Indent);
							_Output.Write ("			}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							_Output.Write ("		public const string Discovery = \"{1}\";\n{0}", _Indent, Service.Discovery);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							_Output.Write ("		public override string GetDiscovery {{\n{0}", _Indent);
							_Output.Write ("			get => Discovery;\n{0}", _Indent);
							_Output.Write ("			}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("        /// The active JPCSession.\n{0}", _Indent);
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							_Output.Write ("		public virtual JPCSession JPCSession {{get; set;}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							foreach  (_Choice Entry2 in Protocol.Entries) {
								switch (Entry2._Tag ()) {
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("        /// <summary>\n{0}", _Indent);
									_Output.Write ("		/// Base method for implementing the transaction  {1}.\n{0}", _Indent, Transaction.Id);
									_Output.Write ("        /// </summary>\n{0}", _Indent);
									_Output.Write ("        /// <param name=\"Request\">The request object to send to the host.</param>\n{0}", _Indent);
									_Output.Write ("		/// <returns>The response object from the service</returns>\n{0}", _Indent);
									_Output.Write ("        public virtual {1} {2} (\n{0}", _Indent, Transaction.Response, Transaction.Id);
									_Output.Write ("                {1} Request) {{\n{0}", _Indent, Transaction.Request);
									_Output.Write ("            return null;\n{0}", _Indent);
									_Output.Write ("            }}\n{0}", _Indent);
								break; }
									}
								}
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("        }}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("    /// <summary>\n{0}", _Indent);
							_Output.Write ("	/// Client class for {1}.\n{0}", _Indent, Service.Id);
							_Output.Write ("    /// </summary>		\n{0}", _Indent);
							_Output.Write ("    public partial class {1}Client : {2} {{\n{0}", _Indent, Service.Id, Service.Id);
							_Output.Write (" 		\n{0}", _Indent);
							_Output.Write ("		JPCRemoteSession JPCRemoteSession;\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("        /// The active JPCSession.\n{0}", _Indent);
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							_Output.Write ("		public override JPCSession JPCSession {{\n{0}", _Indent);
							_Output.Write ("			get {{return JPCRemoteSession;}}\n{0}", _Indent);
							_Output.Write ("			set {{JPCRemoteSession = value as JPCRemoteSession; }}\n{0}", _Indent);
							_Output.Write ("			}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							_Output.Write ("		/// Create a client connection to the specified service.\n{0}", _Indent);
							_Output.Write ("        /// </summary>	\n{0}", _Indent);
							_Output.Write ("        /// <param name=\"JPCRemoteSession\">The remote session to connect to</param>\n{0}", _Indent);
							_Output.Write ("		public {1}Client (JPCRemoteSession JPCRemoteSession) {{\n{0}", _Indent, Service.Id);
							_Output.Write ("			this.JPCRemoteSession = JPCRemoteSession;\n{0}", _Indent);
							_Output.Write ("			}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							foreach  (_Choice Entry2 in Protocol.Entries) {
								switch (Entry2._Tag ()) {
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("        /// <summary>\n{0}", _Indent);
									_Output.Write ("		/// Implement the transaction\n{0}", _Indent);
									_Output.Write ("        /// </summary>		\n{0}", _Indent);
									_Output.Write ("        /// <param name=\"Request\">The request object</param>\n{0}", _Indent);
									_Output.Write ("		/// <returns>The response object</returns>\n{0}", _Indent);
									_Output.Write ("        public override {1} {2} (\n{0}", _Indent, Transaction.Response, Transaction.Id);
									_Output.Write ("                {1} Request) {{\n{0}", _Indent, Transaction.Request);
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("            var ResponseData = JPCRemoteSession.Post(\"{1}\", Request);\n{0}", _Indent, Transaction.Id);
									_Output.Write ("            var Response = {1}.FromTagged(ResponseData);\n{0}", _Indent, Transaction.Response);
									_Output.Write ("\n{0}", _Indent);
									_Output.Write ("            return Response;\n{0}", _Indent);
									_Output.Write ("            }}\n{0}", _Indent);
								break; }
									}
								}
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("		}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("    /// <summary>\n{0}", _Indent);
							_Output.Write ("	/// Client class for {1}.\n{0}", _Indent, Service.Id);
							_Output.Write ("    /// </summary>		\n{0}", _Indent);
							_Output.Write ("    public partial class {1}Provider : Goedel.Protocol.JPCProvider {{\n{0}", _Indent, Service.Id);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("		/// <summary>\n{0}", _Indent);
							_Output.Write ("		/// Interface object to dispatch requests to.\n{0}", _Indent);
							_Output.Write ("		/// </summary>	\n{0}", _Indent);
							_Output.Write ("		public {1} Service;\n{0}", _Indent, Service.Id);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("		/// <summary>\n{0}", _Indent);
							_Output.Write ("		/// Dispatch object request in specified authentication context.\n{0}", _Indent);
							_Output.Write ("		/// </summary>			\n{0}", _Indent);
							_Output.Write ("        /// <param name=\"Session\">The client context.</param>\n{0}", _Indent);
							_Output.Write ("        /// <param name=\"JSONReader\">Reader for data object.</param>\n{0}", _Indent);
							_Output.Write ("        /// <returns>The response object returned by the corresponding dispatch.</returns>\n{0}", _Indent);
							_Output.Write ("		public override Goedel.Protocol.JSONObject Dispatch(JPCSession  Session,  \n{0}", _Indent);
							_Output.Write ("								Goedel.Protocol.JSONReader JSONReader) {{\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("			JSONReader.StartObject ();\n{0}", _Indent);
							_Output.Write ("			string token = JSONReader.ReadToken ();\n{0}", _Indent);
							_Output.Write ("			JSONObject Response = null;\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("			switch (token) {{\n{0}", _Indent);
							foreach  (_Choice Entry2 in Protocol.Entries) {
								switch (Entry2._Tag ()) {
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Transaction.Id);
									_Output.Write ("					var Request = {1}.FromTagged (JSONReader);\n{0}", _Indent, Transaction.Request);
									_Output.Write ("					Response = Service.{1} (Request);\n{0}", _Indent, Transaction.Id);
									_Output.Write ("					break;\n{0}", _Indent);
									_Output.Write ("					}}\n{0}", _Indent);
								break; }
									}
								}
							_Output.Write ("				default : {{\n{0}", _Indent);
							_Output.Write ("					throw new Goedel.Protocol.UnknownOperation ();\n{0}", _Indent);
							_Output.Write ("					}}\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
							_Output.Write ("			JSONReader.EndObject ();\n{0}", _Indent);
							_Output.Write ("			return Response;\n{0}", _Indent);
							_Output.Write ("			}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("		}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("		// Transaction Classes\n{0}", _Indent);
					foreach  (_Choice Entry in Protocol.Entries) {
						switch (Entry._Tag ()) {
							case ProtoStructType.Message: {
							  Message Message = (Message) Entry; 
							
							 MakeClass (Message.Id, Message.Entries);
							if (  (OldConstructors) ) {
								_Output.Write ("        /// <summary>\n{0}", _Indent);
								_Output.Write ("		/// Initialize class from JSONReader stream.\n{0}", _Indent);
								_Output.Write ("        /// </summary>	\n{0}", _Indent);
								_Output.Write ("        /// <param name=\"JSONReader\">Input stream</param>			\n{0}", _Indent);
								_Output.Write ("		public {1} (JSONReader JSONReader) {{\n{0}", _Indent, Message.Id);
								_Output.Write ("			Deserialize (JSONReader);\n{0}", _Indent);
								_Output.Write ("			}}\n{0}", _Indent);
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("        /// <summary>\n{0}", _Indent);
								_Output.Write ("		/// Initialize class from a JSON encoded class.\n{0}", _Indent);
								_Output.Write ("        /// </summary>		\n{0}", _Indent);
								_Output.Write ("        /// <param name=\"_String\">Input string</param>\n{0}", _Indent);
								_Output.Write ("		public {1} (string _String) {{\n{0}", _Indent, Message.Id);
								_Output.Write ("			Deserialize (_String);\n{0}", _Indent);
								_Output.Write ("			}}\n{0}", _Indent);
								}
							_Output.Write ("\n{0}", _Indent);
							
							 var Inherits = HasInherits  (Message.Entries);
							
							 MakeSerializers (Message.Id, Message.ID, Message.Entries, Inherits);
							_Output.Write ("		}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							break; }
							case ProtoStructType.Structure: {
							  Structure Structure = (Structure) Entry; 
							
							 MakeClass (Structure.Id, Structure.Entries, Structure.Parameterized);
							if (  (OldConstructors) ) {
								_Output.Write ("        /// <summary>\n{0}", _Indent);
								_Output.Write ("		/// Initialize class from JSONReader stream.\n{0}", _Indent);
								_Output.Write ("        /// </summary>		\n{0}", _Indent);
								_Output.Write ("        /// <param name=\"JSONReader\">Input stream</param>	\n{0}", _Indent);
								_Output.Write ("		public {1} (JSONReader JSONReader) {{\n{0}", _Indent, Structure.Id);
								_Output.Write ("			Deserialize (JSONReader);\n{0}", _Indent);
								_Output.Write ("			}}\n{0}", _Indent);
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("        /// <summary> \n{0}", _Indent);
								_Output.Write ("		/// Initialize class from a JSON encoded class.\n{0}", _Indent);
								_Output.Write ("        /// </summary>		\n{0}", _Indent);
								_Output.Write ("        /// <param name=\"_String\">Input string</param>\n{0}", _Indent);
								_Output.Write ("		public {1} (string _String) {{\n{0}", _Indent, Structure.Id);
								_Output.Write ("			Deserialize (_String);\n{0}", _Indent);
								_Output.Write ("			}}\n{0}", _Indent);
								}
							
							 var Inherits = HasInherits  (Structure.Entries);
							
							 MakeSerializers (Structure.Id, Structure.ID, Structure.Entries, Inherits);
							_Output.Write ("		}}\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
						break; }
							}
						}
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			}
		

		//
		//  
		//

			 public bool IsAbstract  (List<_Choice> Entries) {
			 bool result = false;
			foreach  (_Choice Entry in Entries) {
				switch (Entry._Tag ()) {
					case ProtoStructType.Abstract: { 
					
					 result = true;
				break; }
					}
				}
			 return result;
			 }
		
		

		//
		//  
		//

			 public bool IsMultiple  (List<_Choice> Entries) {
			 bool result = false;
			foreach  (_Choice Entry in Entries) {
				switch (Entry._Tag ()) {
					case ProtoStructType.Multiple: { 
					
					 result = true;
				break; }
					}
				}
			 return result;
			 }
		
		

		//
		//  
		//

			 public bool IsRequired  (List<_Choice> Entries) {
			 bool result = false;
			foreach  (_Choice Entry in Entries) {
				switch (Entry._Tag ()) {
					case ProtoStructType.Required: { 
					
					 result = true;
				break; }
					}
				}
			 return result;
			 }
		
		

		//
		//  
		//

			 public string HasInherits  (List<_Choice> Entries) {
			 string result = null;
			foreach  (_Choice Entry in Entries) {
				switch (Entry._Tag ()) {
					case ProtoStructType.Inherits: {
					  Inherits Inherits = (Inherits) Entry; 
					
					 result = Inherits.Ref.ToString();
					break; }
					case ProtoStructType.External: {
					  External External = (External) Entry; 
					
					 result = External.Ref.ToString();
				break; }
					}
				}
			 return result;
			 }
		
		

		//
		//  
		//

			 public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries) {
			 var Inherits = HasInherits (Entries);
			 DescriptionListC (Entries, 1);
			_Output.Write ("	", _Indent);
			if (  (IsAbstract (Entries)) ) {
				_Output.Write ("abstract ", _Indent);
				}
			if (  (Inherits == null)  ) {
				_Output.Write ("public partial class {1} : {2} {{\n{0}", _Indent, Id, CurrentPrefix);
				} else {
				_Output.Write ("public partial class {1} : {2} {{\n{0}", _Indent, Id, Inherits);
				}
			DeclareMembers ((Entries));
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Tag identifying this class.\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The tag</returns>\n{0}", _Indent);
			_Output.Write ("		public override string Tag () {{\n{0}", _Indent);
			_Output.Write ("			return \"{1}\";\n{0}", _Indent, Id);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (OldConstructors) ) {
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Default Constructor\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("		public {1} () {{\n{0}", _Indent, Id);
				_Output.Write ("			_Initialize ();\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				}
			 }
		
		

		//
		//  
		//

			 public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries, bool Param) {
			 var Inherits = HasInherits (Entries);
			 DescriptionListC (Entries, 1);
			_Output.Write ("	", _Indent);
			if (  (IsAbstract (Entries)) ) {
				_Output.Write ("abstract ", _Indent);
				}
			 var TTT = Param ? "<T>" : "";
			if (  (Inherits == null)  ) {
				_Output.Write ("public partial class {1}{2} : {3} {{\n{0}", _Indent, Id, TTT, CurrentPrefix);
				} else {
				_Output.Write ("public partial class {1}{2} : {3} {{\n{0}", _Indent, Id, TTT, Inherits);
				}
			DeclareMembers ((Entries));
			_Output.Write ("		\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Tag identifying this class\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("		public override string _Tag {{ get; }} = \"{1}\";\n{0}", _Indent, Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		/// <summary>\n{0}", _Indent);
			if (  (IsAbstract (Entries)) ) {
				_Output.Write ("        /// Factory method. Throws exception as this is an abstract class.\n{0}", _Indent);
				} else {
				_Output.Write ("        /// Factory method\n{0}", _Indent);
				}
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <returns>Object of this type</returns>\n{0}", _Indent);
			_Output.Write ("		public static new JSONObject _Factory () {{\n{0}", _Indent);
			if (  (IsAbstract (Entries)) ) {
				_Output.Write ("			throw new CannotCreateAbstract();\n{0}", _Indent);
				} else {
				_Output.Write ("			return new {1}();\n{0}", _Indent, Id);
				}
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (OldConstructors) ) {
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Default Constructor\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("		public {1} () {{\n{0}", _Indent, Id);
				_Output.Write ("			_Initialize ();\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				}
			 }
		
		

		//
		//  ParameterList
		//

			 public void DeclareMembers  (List<_Choice> Entries) {
			foreach  (_Choice Entry in Entries) {
				 TOKEN<_Choice> Token = null;
				 string Type = null; string TType = null;
				 List<_Choice> Options = null;
				 bool Nullable;
				 string Tag;
				 GetType (Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag);
				if (  (Token != null) ) {
					 bool Multiple = IsMultiple (Options);
					if (  Multiple ) {
						_Output.Write ("{1}\n{0}", _Indent, CommentSummary(8,Entry.Description));
						_Output.Write ("		public virtual List<{1}>				{2}  {{get; set;}}\n{0}", _Indent, Type, Token);
						} else {
						if (  !Nullable ) {
							_Output.Write ("		bool								__{1} = false;\n{0}", _Indent, Token);
							_Output.Write ("		private {1}						_{2};\n{0}", _Indent, Type, Token);
							_Output.Write ("{1}\n{0}", _Indent, CommentSummary(8,Entry.Description));
							_Output.Write ("		public virtual {1}						{2} {{\n{0}", _Indent, Type, Token);
							_Output.Write ("			get {{return _{1};}}\n{0}", _Indent, Token);
							_Output.Write ("			set {{_{1} = value; __{2} = true; }}\n{0}", _Indent, Token, Token);
							_Output.Write ("			}}\n{0}", _Indent);
							} else {
							_Output.Write ("{1}\n{0}", _Indent, CommentSummary(8,Entry.Description));
							_Output.Write ("		public virtual {1}						{2}  {{get; set;}}\n{0}", _Indent, Type, Token);
							}
						}
					}
				}
			 }
		
		

		//
		//  MakeSerializers
		//

			 public void MakeSerializers  (ID<_Choice> Id, string STag, List<_Choice> Entries, string Inherits) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Serialize this object to the specified output stream.\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Writer\">Output stream</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"wrap\">If true, output is wrapped with object\n{0}", _Indent);
			_Output.Write ("        /// start and end sequences '{{ ... }}'.</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"first\">If true, item is the first entry in a list.</param>\n{0}", _Indent);
			_Output.Write ("		public override void Serialize (Writer Writer, bool wrap, ref bool first) {{\n{0}", _Indent);
			_Output.Write ("			SerializeX (Writer, wrap, ref first);\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Serialize this object to the specified output stream.\n{0}", _Indent);
			_Output.Write ("        /// Unlike the Serlialize() method, this method is not inherited from the\n{0}", _Indent);
			_Output.Write ("        /// parent class allowing a specific version of the method to be called.\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"_Writer\">Output stream</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"_wrap\">If true, output is wrapped with object\n{0}", _Indent);
			_Output.Write ("        /// start and end sequences '{{ ... }}'.</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"_first\">If true, item is the first entry in a list.</param>\n{0}", _Indent);
			_Output.Write ("		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {{\n{0}", _Indent);
			_Output.Write ("			if (_wrap) {{\n{0}", _Indent);
			_Output.Write ("				_Writer.WriteObjectStart ();\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			if (  (Inherits != null) ) {
				_Output.Write ("			(({1})this).SerializeX(_Writer, false, ref _first);\n{0}", _Indent, Inherits);
				}
			foreach  (_Choice Entry in Entries) {
				 TOKEN<_Choice> Token = null;
				 string Type = null; string TType = null;
				 List<_Choice> Options = null; bool Nullable;
				 string Tag;
				 GetType (Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag);
				if (  (Token != null) ) {
					 bool Multiple = IsMultiple (Options);
					if (  Multiple ) {
						_Output.Write ("			if ({1} != null) {{\n{0}", _Indent, Token);
						_Output.Write ("				_Writer.WriteObjectSeparator (ref _first);\n{0}", _Indent);
						_Output.Write ("				_Writer.WriteToken (\"{1}\", 1);\n{0}", _Indent, Tag);
						_Output.Write ("				_Writer.WriteArrayStart ();\n{0}", _Indent);
						_Output.Write ("				bool _firstarray = true;\n{0}", _Indent);
						_Output.Write ("				foreach (var _index in {1}) {{\n{0}", _Indent, Token);
						_Output.Write ("					_Writer.WriteArraySeparator (ref _firstarray);\n{0}", _Indent);
						 MakeSerializeArrayEntry (Entry, "_index");
						_Output.Write ("					}}\n{0}", _Indent);
						_Output.Write ("				_Writer.WriteArrayEnd ();\n{0}", _Indent);
						_Output.Write ("				}}\n{0}", _Indent);
						} else {
						if (  Nullable ) {
							_Output.Write ("			if ({1} != null) {{\n{0}", _Indent, Token);
							} else {
							_Output.Write ("			if (__{1}){{\n{0}", _Indent, Token);
							}
						_Output.Write ("				_Writer.WriteObjectSeparator (ref _first);\n{0}", _Indent);
						_Output.Write ("				_Writer.WriteToken (\"{1}\", 1);\n{0}", _Indent, Tag);
						 MakeSerializeEntry (Entry, Token.ToString());
						_Output.Write ("				}}\n{0}", _Indent);
						}
					if (  Multiple ) {
						_Output.Write ("\n{0}", _Indent);
						}
					}
				}
			_Output.Write ("			if (_wrap) {{\n{0}", _Indent);
			_Output.Write ("				_Writer.WriteObjectEnd ();\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (!IsAbstract (Entries)) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("		/// Create a new instance from untagged byte input.\n{0}", _Indent);
				_Output.Write ("		/// i.e. {{... data ... }}\n{0}", _Indent);
				_Output.Write ("        /// </summary>	\n{0}", _Indent);
				_Output.Write ("        /// <param name=\"_Data\">The input data.</param>\n{0}", _Indent);
				_Output.Write ("        /// <returns>The created object.</returns>		\n{0}", _Indent);
				_Output.Write ("		public static new {1} From (byte[] _Data) {{\n{0}", _Indent, Id);
				_Output.Write ("			var _Input = System.Text.Encoding.UTF8.GetString(_Data);\n{0}", _Indent);
				_Output.Write ("			return From (_Input);\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("		/// Create a new instance from untagged string input.\n{0}", _Indent);
				_Output.Write ("		/// i.e. {{... data ... }}\n{0}", _Indent);
				_Output.Write ("        /// </summary>	\n{0}", _Indent);
				_Output.Write ("        /// <param name=\"_Input\">The input data.</param>\n{0}", _Indent);
				_Output.Write ("        /// <returns>The created object.</returns>				\n{0}", _Indent);
				_Output.Write ("		public static new {1} From (string _Input) {{\n{0}", _Indent, Id);
				_Output.Write ("			StringReader _Reader = new StringReader (_Input);\n{0}", _Indent);
				_Output.Write ("            JSONReader JSONReader = new JSONReader (_Reader);\n{0}", _Indent);
				_Output.Write ("			var Result = new {1} ();\n{0}", _Indent, Id);
				_Output.Write ("			Result.Deserialize (JSONReader);\n{0}", _Indent);
				_Output.Write ("			return Result;\n{0}", _Indent);
				_Output.Write ("			// return new {1} (JSONReader);\n{0}", _Indent, Id);
				_Output.Write ("			}}\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("		/// Create a new instance from tagged byte input.\n{0}", _Indent);
			_Output.Write ("		/// i.e. {{ \"{1}\" : {{... data ... }} }}\n{0}", _Indent, Id);
			_Output.Write ("        /// </summary>	\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"_Data\">The input data.</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The created object.</returns>				\n{0}", _Indent);
			_Output.Write ("		public static new {1} FromTagged (byte[] _Data) {{\n{0}", _Indent, Id);
			_Output.Write ("			var _Input = System.Text.Encoding.UTF8.GetString(_Data);\n{0}", _Indent);
			_Output.Write ("			return FromTagged (_Input);\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Create a new instance from tagged string input.\n{0}", _Indent);
			_Output.Write ("		/// i.e. {{ \"{1}\" : {{... data ... }} }}\n{0}", _Indent, Id);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"_Input\">The input data.</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The created object.</returns>		\n{0}", _Indent);
			_Output.Write ("		public static new {1} FromTagged (string _Input) {{\n{0}", _Indent, Id);
			_Output.Write ("			//{1} _Result;\n{0}", _Indent, Id);
			_Output.Write ("			//Deserialize (_Input, out _Result);\n{0}", _Indent);
			_Output.Write ("			StringReader _Reader = new StringReader (_Input);\n{0}", _Indent);
			_Output.Write ("            JSONReader JSONReader = new JSONReader (_Reader);\n{0}", _Indent);
			_Output.Write ("			return FromTagged (JSONReader) ;\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Deserialize a tagged stream\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"JSONReader\">The input stream</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The created object.</returns>		\n{0}", _Indent);
			_Output.Write ("        public static new {1} FromTagged (JSONReader JSONReader) {{\n{0}", _Indent, Id);
			_Output.Write ("			var Out = JSONReader.ReadTaggedObject (_TagDictionary);\n{0}", _Indent);
			_Output.Write ("			return Out as {1};\n{0}", _Indent, Id);
			if (  (OldConstructors) ) {
				_Output.Write ("			{1} Out = null;\n{0}", _Indent, Id);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			JSONReader.StartObject ();\n{0}", _Indent);
				_Output.Write ("            if (JSONReader.EOR) {{\n{0}", _Indent);
				_Output.Write ("                return null;\n{0}", _Indent);
				_Output.Write ("                }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			string token = JSONReader.ReadToken ();\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			switch (token) {{\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				 MapInheritors (Id, STag);
				_Output.Write ("				default : {{\n{0}", _Indent);
				_Output.Write ("                    break;\n{0}", _Indent);
				_Output.Write ("					}}\n{0}", _Indent);
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("			JSONReader.EndObject ();\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("			return Out;\n{0}", _Indent);
				}
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Having read a tag, process the corresponding value data.\n{0}", _Indent);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"JSONReader\">The input stream</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"Tag\">The tag</param>\n{0}", _Indent);
			_Output.Write ("		public override void DeserializeToken (JSONReader JSONReader, string Tag) {{\n{0}", _Indent);
			_Output.Write ("			\n{0}", _Indent);
			_Output.Write ("			switch (Tag) {{\n{0}", _Indent);
			foreach  (_Choice Entry in Entries) {
				 TOKEN<_Choice> Token = null;
				 string Type = null; string TType = null;
				 List<_Choice> Options = null;
				 bool Nullable;
				 string Tag;
				 GetType(Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag);
				if (  (Token != null) ) {
					 bool Multiple = IsMultiple (Options);
					_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Tag);
					if (  Multiple ) {
						_Output.Write ("					// Have a sequence of values\n{0}", _Indent);
						_Output.Write ("					bool _Going = JSONReader.StartArray ();\n{0}", _Indent);
						_Output.Write ("					{1} = new List <{2}> ();\n{0}", _Indent, Token, Type);
						_Output.Write ("					while (_Going) {{\n{0}", _Indent);
						if (  Entry._Tag () == ProtoStructType.Struct ) {
							_Output.Write ("						// an untagged structure.\n{0}", _Indent);
							_Output.Write ("						var _Item = new  {1} ();\n{0}", _Indent, Type);
							_Output.Write ("						_Item.Deserialize (JSONReader);\n{0}", _Indent);
							_Output.Write ("						// var _Item = new {1} (JSONReader);\n{0}", _Indent, Type);
							} else if (  Entry._Tag () == ProtoStructType.TStruct) {
							_Output.Write ("						var _Item = {1}.FromTagged (JSONReader); // a tagged structure\n{0}", _Indent, Type);
							} else {
							_Output.Write ("						{1} _Item = JSONReader.Read{2} ();\n{0}", _Indent, Type, TType);
							}
						_Output.Write ("						{1}.Add (_Item);\n{0}", _Indent, Token);
						_Output.Write ("						_Going = JSONReader.NextArray ();\n{0}", _Indent);
						_Output.Write ("						}}\n{0}", _Indent);
						} else {
						if (  Entry._Tag () == ProtoStructType.Struct ) {
							_Output.Write ("					// An untagged structure\n{0}", _Indent);
							_Output.Write ("					{1} = new {2} ();\n{0}", _Indent, Token, Type);
							_Output.Write ("					{1}.Deserialize (JSONReader);\n{0}", _Indent, Token);
							_Output.Write (" \n{0}", _Indent);
							} else if (  Entry._Tag () == ProtoStructType.TStruct) {
							_Output.Write ("					{1} = {2}.FromTagged (JSONReader) ;  // A tagged structure\n{0}", _Indent, Token, Type);
							} else {
							_Output.Write ("					{1} = JSONReader.Read{2} ();\n{0}", _Indent, Token, TType);
							}
						}
					_Output.Write ("					break;\n{0}", _Indent);
					_Output.Write ("					}}\n{0}", _Indent);
					}
				}
			_Output.Write ("				default : {{\n{0}", _Indent);
			if (  (Inherits != null) ) {
				_Output.Write ("					base.DeserializeToken(JSONReader, Tag);\n{0}", _Indent);
				}
			_Output.Write ("					break;\n{0}", _Indent);
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			// check up that all the required elements are present\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			 }
		
		

		//
		// MakeSerializeEntry
		//
		public void MakeSerializeEntry (_Choice Entry, string Tag) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Boolean: { 
				_Output.Write ("					_Writer.WriteBoolean ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Integer: { 
				_Output.Write ("					_Writer.WriteInteger32 ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Binary: { 
				_Output.Write ("					_Writer.WriteBinary ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Struct: { 
				_Output.Write ("					{1}.Serialize (_Writer, false);\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.TStruct: { 
				_Output.Write ("					// expand this to a tagged structure\n{0}", _Indent);
				_Output.Write ("					//{1}.Serialize (_Writer, false);\n{0}", _Indent, Tag);
				_Output.Write ("					{{\n{0}", _Indent);
				_Output.Write ("						_Writer.WriteObjectStart();\n{0}", _Indent);
				_Output.Write ("						_Writer.WriteToken({1}.Tag(), 1);\n{0}", _Indent, Tag);
				_Output.Write ("						bool firstinner = true;\n{0}", _Indent);
				_Output.Write ("						{1}.Serialize (_Writer, true, ref firstinner);\n{0}", _Indent, Tag);
				_Output.Write ("						_Writer.WriteObjectEnd();\n{0}", _Indent);
				_Output.Write ("						}}\n{0}", _Indent);
				break; }
				case ProtoStructType.Label: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Name: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.String: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.URI: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.DateTime: { 
				_Output.Write ("					_Writer.WriteDateTime ({1});\n{0}", _Indent, Tag);
				
											break; }
				
				 default : {
				_Output.Write ("\n{0}", _Indent);
			break; }
				}
			}
		

		//
		// MakeSerializeArrayEntry
		//
		public void MakeSerializeArrayEntry (_Choice Entry, string Tag) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Boolean: { 
				_Output.Write ("					_Writer.WriteBoolean ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Integer: { 
				_Output.Write ("					_Writer.WriteInteger32 ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Binary: { 
				_Output.Write ("					_Writer.WriteBinary ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Struct: { 
				_Output.Write ("					// This is an untagged structure. Cannot inherit.\n{0}", _Indent);
				_Output.Write ("                    //_Writer.WriteObjectStart();\n{0}", _Indent);
				_Output.Write ("                    //_Writer.WriteToken({1}.Tag(), 1);\n{0}", _Indent, Tag);
				_Output.Write ("					bool firstinner = true;\n{0}", _Indent);
				_Output.Write ("					{1}.Serialize (_Writer, true, ref firstinner);\n{0}", _Indent, Tag);
				_Output.Write ("                    //_Writer.WriteObjectEnd();\n{0}", _Indent);
				break; }
				case ProtoStructType.TStruct: { 
				_Output.Write ("                    _Writer.WriteObjectStart();\n{0}", _Indent);
				_Output.Write ("                    _Writer.WriteToken({1}.Tag(), 1);\n{0}", _Indent, Tag);
				_Output.Write ("					bool firstinner = true;\n{0}", _Indent);
				_Output.Write ("					{1}.Serialize (_Writer, true, ref firstinner);\n{0}", _Indent, Tag);
				_Output.Write ("                    _Writer.WriteObjectEnd();\n{0}", _Indent);
				break; }
				case ProtoStructType.Label: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.Name: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.String: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.URI: { 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				break; }
				case ProtoStructType.DateTime: { 
				_Output.Write ("					_Writer.WriteDateTime ({1});\n{0}", _Indent, Tag);
				
											break; }
				
				 default : {
				_Output.Write ("\n{0}", _Indent);
			break; }
				}
			}
		

		//
		// 
		//

			 void GetType (_Choice Entry, out TOKEN<_Choice> Token, out string Type, out string TType, 
							out List<_Choice> Options, out bool Nullable, out string Tag) {
			 Nullable = true;
			switch (Entry._Tag ()) {
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "bool";  Nullable = false;TType = "Boolean";
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "int"; Nullable = false;TType = "Integer32";
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "byte[]";TType = "Binary";
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "DateTime"; Nullable = false; TType = "DateTime";
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type;
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type;
				
				 break; } default : {
				
				 Tag = null ; Token = null; Options = null; Type = null; TType = null;
			break; }
				}
			 }
		
		

		//
		// 
		//

			 void GetSerializerz (_Choice Entry, out TOKEN<_Choice> Token, out string Type,
							out List<_Choice> Options) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "Boolean";
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "Integer32";
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "Binary";
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = "DateTime";
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString();
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				
				 Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString();
				
				 break; } default : {
				
				 Token = null; Options = null; Type = null;
			break; }
				}
			 }
		
		

		//
		//  DeserializeCase
		//

			 void DeserializeCase (ID<_Choice> Id, string Tag) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Id);
			if (  Id.Object.IsAbstract ) {
				_Output.Write ("					Out = null;\n{0}", _Indent);
				_Output.Write ("					throw new Exception (\"Can't create abstract type\");\n{0}", _Indent);
				} else {
				_Output.Write ("					// Out = {1}.Factory ();\n{0}", _Indent, Id);
				_Output.Write ("					Out = new {1} ();\n{0}", _Indent, Id);
				_Output.Write ("					Out.Deserialize (JSONReader);\n{0}", _Indent);
				_Output.Write ("					break;\n{0}", _Indent);
				}
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
				}
		
		

		//
		//  MapInheritors
		//

			 void MapInheritors (ID<_Choice> Id, string Tag) {
			_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Id);
			if (  Id.Object.IsAbstract ) {
				_Output.Write ("					Out = null;\n{0}", _Indent);
				_Output.Write ("					throw new Exception (\"Can't create abstract type\");\n{0}", _Indent);
				} else {
				_Output.Write ("					Out = new {1} (); \n{0}", _Indent, Id);
				_Output.Write ("					// Out = {1}.Factory ();\n{0}", _Indent, Id);
				_Output.Write ("					Out.Deserialize (JSONReader);\n{0}", _Indent);
				_Output.Write ("					break;\n{0}", _Indent);
				}
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (REF<_Choice> Ref in Id.REFs) {
				switch (Ref.Object._Tag ()) {
					case ProtoStructType.Message: {
					  Message Message = (Message) Ref.Object; 
					
					 MapInheritors (Message.Id, Message.ID);
					break; }
					case ProtoStructType.Structure: {
					  Structure Structure = (Structure) Ref.Object; 
					
					 MapInheritors (Structure.Id, Structure.ID);
				break; }
					}
				}
				}
		
		

		//
		//  DescriptionListC
		//

			 public void DescriptionListC  (List<_Choice> Entries, int indent) {
			 Indentify (indent);
			if (  (indent > 0)  ) {
				_Output.Write ("/// <summary>\n{0}", _Indent);
				}
			 bool first = true;
			foreach  (_Choice Entry in Entries) {
				switch (Entry._Tag ()) {
					case ProtoStructType.Description: {
					  Description Description = (Description) Entry; 
					if (  first ) {
						 first = false;
						 Indentify (indent);
						_Output.Write ("///\n{0}", _Indent);
						}
					foreach  (string s in Description.Text1) {
						for  (int i=0; i<indent; i++) {
							_Output.Write ("	", _Indent);
							}
						_Output.Write ("/// {1}\n{0}", _Indent, s);
						}
				break; }
					}
				}
			 Indentify (indent);
			if (  (indent > 0)  ) {
				_Output.Write ("/// </summary>\n{0}", _Indent);
				}
			 }
		
		}
	}
