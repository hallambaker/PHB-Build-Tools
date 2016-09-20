// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Copyright ©  2011 by Default Deny Security Inc.
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
// #xclass Goedel.Tool.ProtoGen Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ProtoGen {
	public partial class Generate : global::Goedel.Registry.Script {

		//  
		// #! 
		//
		// #!   Code Generator (C#) 
		//   Code Generator (C#)
		// #! 
		//
		// #!   
		//  
		//  
		// #method GenerateCS ProtoStruct ProtoStruct 
		

		//
		// GenerateCS
		//
		public void GenerateCS (ProtoStruct ProtoStruct) {
			// #% ProtoStruct.Complete (); 
			 ProtoStruct.Complete ();
			// #% var GenerateTime =System.DateTime.UtcNow; 
			 var GenerateTime =System.DateTime.UtcNow;
			// #% Boilerplate.Header (_Output, "//  ", GenerateTime); 
			 Boilerplate.Header (_Output, "//  ", GenerateTime);
			// #% Boilerplate.MITLicense (_Output, "//  ", "Copyright (c) " + "2016", "."); 
			 Boilerplate.MITLicense (_Output, "//  ", "Copyright (c) " + "2016", ".");
			// #% var InheritsOverride = "override"; // "virtual" 
			 var InheritsOverride = "override"; // "virtual"
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// using System; 
			_Output.Write ("using System;\n{0}", _Indent);
			// using System.IO; 
			_Output.Write ("using System.IO;\n{0}", _Indent);
			// using System.Collections; 
			_Output.Write ("using System.Collections;\n{0}", _Indent);
			// using System.Collections.Generic; 
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			// using System.Text; 
			_Output.Write ("using System.Text;\n{0}", _Indent);
			// using Goedel.Protocol; 
			_Output.Write ("using Goedel.Protocol;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Item in ProtoStruct.Top) 
			foreach  (var Item in ProtoStruct.Top) {
				// #switchcast ProtoStructType Item 
				switch (Item._Tag ()) {
					// #casecast Protocol Protocol 
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (var Entry in Protocol.Entries) 
					foreach  (var Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Using Using 
							case ProtoStructType.Using: {
							  Using Using = (Using) Entry; 
							// using #{Using.Id}; 
							_Output.Write ("using {1};\n{0}", _Indent, Using.Id);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #% DescriptionListC (Protocol.Entries, 0); 
					
					 DescriptionListC (Protocol.Entries, 0);
					// #% Namespace = Protocol.Namespace.ToString (); 
					
					 Namespace = Protocol.Namespace.ToString ();
					// namespace #{Protocol.Namespace} { 
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Protocol.Namespace);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//     /// <summary> 
					_Output.Write ("    /// <summary>\n{0}", _Indent);
					//     ///  
					_Output.Write ("    /// \n{0}", _Indent);
					//     /// </summary> 
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					// 	public abstract partial class #{Protocol.Prefix} #{Protocol.ThisInherits} { 
					_Output.Write ("	public abstract partial class {1} {2} {{\n{0}", _Indent, Protocol.Prefix, Protocol.ThisInherits);
					// #% CurrentPrefix = Protocol.Prefix.ToString (); 
					
					 CurrentPrefix = Protocol.Prefix.ToString ();
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         ///  
					_Output.Write ("        /// \n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					// 		public #{InheritsOverride} string Tag () { 
					_Output.Write ("		public {1} string Tag () {{\n{0}", _Indent, InheritsOverride);
					// 			return "#{Protocol.Prefix}"; 
					_Output.Write ("			return \"{1}\";\n{0}", _Indent, Protocol.Prefix);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Default constructor. 
					_Output.Write ("        /// Default constructor.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					// 		public #{Protocol.Prefix} () { 
					_Output.Write ("		public {1} () {{\n{0}", _Indent, Protocol.Prefix);
					// 			_Initialize () ; 
					_Output.Write ("			_Initialize () ;\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Construct an instance from a JSON encoded stream. 
					_Output.Write ("        /// Construct an instance from a JSON encoded stream.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					// 		public #{Protocol.Prefix} (JSONReader JSONReader) { 
					_Output.Write ("		public {1} (JSONReader JSONReader) {{\n{0}", _Indent, Protocol.Prefix);
					// 			Deserialize (JSONReader); 
					_Output.Write ("			Deserialize (JSONReader);\n{0}", _Indent);
					// 			_Initialize () ; 
					_Output.Write ("			_Initialize () ;\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//         /// <summary> 
					_Output.Write ("        /// <summary>\n{0}", _Indent);
					//         /// Construct an instance from a JSON encoded string. 
					_Output.Write ("        /// Construct an instance from a JSON encoded string.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					// 		public #{Protocol.Prefix} (string _String) { 
					_Output.Write ("		public {1} (string _String) {{\n{0}", _Indent, Protocol.Prefix);
					// 			Deserialize (_String); 
					_Output.Write ("			Deserialize (_String);\n{0}", _Indent);
					// 			_Initialize () ; 
					_Output.Write ("			_Initialize () ;\n{0}", _Indent);
					// 			} 
					_Output.Write ("			}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		/// <summary> 
					_Output.Write ("		/// <summary>\n{0}", _Indent);
					//         /// Construct an instance from the specified tagged JSONReader stream. 
					_Output.Write ("        /// Construct an instance from the specified tagged JSONReader stream.\n{0}", _Indent);
					//         /// </summary> 
					_Output.Write ("        /// </summary>\n{0}", _Indent);
					//         public static void Deserialize(JSONReader JSONReader, out JSONObject Out) { 
					_Output.Write ("        public static void Deserialize(JSONReader JSONReader, out JSONObject Out) {{\n{0}", _Indent);
					// 	 
					_Output.Write ("	\n{0}", _Indent);
					// 			JSONReader.StartObject (); 
					_Output.Write ("			JSONReader.StartObject ();\n{0}", _Indent);
					//             if (JSONReader.EOR) { 
					_Output.Write ("            if (JSONReader.EOR) {{\n{0}", _Indent);
					//                 Out = null; 
					_Output.Write ("                Out = null;\n{0}", _Indent);
					//                 return; 
					_Output.Write ("                return;\n{0}", _Indent);
					//                 } 
					_Output.Write ("                }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			string token = JSONReader.ReadToken (); 
					_Output.Write ("			string token = JSONReader.ReadToken ();\n{0}", _Indent);
					// 			Out = null; 
					_Output.Write ("			Out = null;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 			switch (token) { 
					_Output.Write ("			switch (token) {{\n{0}", _Indent);
					// #foreach (_Choice Entry in Protocol.Entries) 
					foreach  (_Choice Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Message Message 
							case ProtoStructType.Message: {
							  Message Message = (Message) Entry; 
							// #% DeserializeCase (Message.Id,Message.ID); 
							
							 DeserializeCase (Message.Id,Message.ID);
							// #casecast Structure Structure 
							break; }
							case ProtoStructType.Structure: {
							  Structure Structure = (Structure) Entry; 
							// #% DeserializeCase (Structure.Id,Structure.ID); 
							
							 DeserializeCase (Structure.Id,Structure.ID);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// 				default : { 
					_Output.Write ("				default : {{\n{0}", _Indent);
					// 					throw new Exception ("Not supported"); 
					_Output.Write ("					throw new Exception (\"Not supported\");\n{0}", _Indent);
					// 					} 
					_Output.Write ("					}}\n{0}", _Indent);
					// 				}	 
					_Output.Write ("				}}	\n{0}", _Indent);
					// 			JSONReader.EndObject (); 
					_Output.Write ("			JSONReader.EndObject ();\n{0}", _Indent);
					//             } 
					_Output.Write ("            }}\n{0}", _Indent);
					// 		} 
					_Output.Write ("		}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		// Service Dispatch Classes 
					_Output.Write ("		// Service Dispatch Classes\n{0}", _Indent);
					// #foreach (_Choice Entry in Protocol.Entries) 
					foreach  (_Choice Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Service Service 
							case ProtoStructType.Service: {
							  Service Service = (Service) Entry; 
							//  
							_Output.Write ("\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//     /// <summary> 
							_Output.Write ("    /// <summary>\n{0}", _Indent);
							// 	/// The new base class for the client and service side APIs. 
							_Output.Write ("	/// The new base class for the client and service side APIs.\n{0}", _Indent);
							//     /// </summary>		 
							_Output.Write ("    /// </summary>		\n{0}", _Indent);
							//     public abstract partial class #{Service.Id} : Goedel.Protocol.JPCInterface { 
							_Output.Write ("    public abstract partial class {1} : Goedel.Protocol.JPCInterface {{\n{0}", _Indent, Service.Id);
							// 		 
							_Output.Write ("		\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         /// Well Known service identifier. 
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							//         /// </summary> 
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							// 		public const string WellKnown = "#{Service.WellKnown}"; 
							_Output.Write ("		public const string WellKnown = \"{1}\";\n{0}", _Indent, Service.WellKnown);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         /// Well Known service identifier. 
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							//         /// </summary> 
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							// 		public override string GetWellKnown { 
							_Output.Write ("		public override string GetWellKnown {{\n{0}", _Indent);
							// 			get {return WellKnown;} 
							_Output.Write ("			get {{return WellKnown;}}\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         /// Well Known service identifier. 
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							//         /// </summary> 
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							// 		public const string Discovery = "#{Service.Discovery}"; 
							_Output.Write ("		public const string Discovery = \"{1}\";\n{0}", _Indent, Service.Discovery);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         /// Well Known service identifier. 
							_Output.Write ("        /// Well Known service identifier.\n{0}", _Indent);
							//         /// </summary> 
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							// 		public override string GetDiscovery { 
							_Output.Write ("		public override string GetDiscovery {{\n{0}", _Indent);
							// 			get {return Discovery;} 
							_Output.Write ("			get {{return Discovery;}}\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// 		JPCSession _JPCSession; 
							_Output.Write ("		JPCSession _JPCSession;\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         /// The active JPCSession. 
							_Output.Write ("        /// The active JPCSession.\n{0}", _Indent);
							//         /// </summary>		 
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							// 		public virtual JPCSession JPCSession { 
							_Output.Write ("		public virtual JPCSession JPCSession {{\n{0}", _Indent);
							// 			get {return _JPCSession;} 
							_Output.Write ("			get {{return _JPCSession;}}\n{0}", _Indent);
							// 			set {_JPCSession = value;} 
							_Output.Write ("			set {{_JPCSession = value;}}\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #foreach (_Choice Entry2 in Protocol.Entries) 
							foreach  (_Choice Entry2 in Protocol.Entries) {
								// #switchcast ProtoStructType Entry2 
								switch (Entry2._Tag ()) {
									// #casecast Transaction Transaction 
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									//  
									_Output.Write ("\n{0}", _Indent);
									//         /// <summary> 
									_Output.Write ("        /// <summary>\n{0}", _Indent);
									// 		/// Base class for implementing the transaction. 
									_Output.Write ("		/// Base class for implementing the transaction.\n{0}", _Indent);
									//         /// </summary>		 
									_Output.Write ("        /// </summary>		\n{0}", _Indent);
									//         public virtual #{Transaction.Response} #{Transaction.Id} ( 
									_Output.Write ("        public virtual {1} {2} (\n{0}", _Indent, Transaction.Response, Transaction.Id);
									//                 #{Transaction.Request} Request) { 
									_Output.Write ("                {1} Request) {{\n{0}", _Indent, Transaction.Request);
									//             return null; 
									_Output.Write ("            return null;\n{0}", _Indent);
									//             } 
									_Output.Write ("            }}\n{0}", _Indent);
									// #end switchcast 
								break; }
									}
								// #end foreach 
								}
							//  
							_Output.Write ("\n{0}", _Indent);
							//         } 
							_Output.Write ("        }}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//     /// <summary> 
							_Output.Write ("    /// <summary>\n{0}", _Indent);
							// 	/// Client class for #{Service.Id}. 
							_Output.Write ("	/// Client class for {1}.\n{0}", _Indent, Service.Id);
							//     /// </summary>		 
							_Output.Write ("    /// </summary>		\n{0}", _Indent);
							//     public partial class #{Service.Id}Client : #{Service.Id} { 
							_Output.Write ("    public partial class {1}Client : {2} {{\n{0}", _Indent, Service.Id, Service.Id);
							//  		 
							_Output.Write (" 		\n{0}", _Indent);
							// 		JPCRemoteSession JPCRemoteSession; 
							_Output.Write ("		JPCRemoteSession JPCRemoteSession;\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         /// The active JPCSession. 
							_Output.Write ("        /// The active JPCSession.\n{0}", _Indent);
							//         /// </summary>		 
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							// 		public override JPCSession JPCSession { 
							_Output.Write ("		public override JPCSession JPCSession {{\n{0}", _Indent);
							// 			get {return JPCRemoteSession;} 
							_Output.Write ("			get {{return JPCRemoteSession;}}\n{0}", _Indent);
							// 			set {JPCRemoteSession = value as JPCRemoteSession; } 
							_Output.Write ("			set {{JPCRemoteSession = value as JPCRemoteSession; }}\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							// 		/// Create a client connection to the specified service. 
							_Output.Write ("		/// Create a client connection to the specified service.\n{0}", _Indent);
							//         /// </summary>	 
							_Output.Write ("        /// </summary>	\n{0}", _Indent);
							// 		public #{Service.Id}Client (JPCRemoteSession JPCRemoteSession) { 
							_Output.Write ("		public {1}Client (JPCRemoteSession JPCRemoteSession) {{\n{0}", _Indent, Service.Id);
							// 			this.JPCRemoteSession = JPCRemoteSession; 
							_Output.Write ("			this.JPCRemoteSession = JPCRemoteSession;\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #foreach (_Choice Entry2 in Protocol.Entries) 
							foreach  (_Choice Entry2 in Protocol.Entries) {
								// #switchcast ProtoStructType Entry2 
								switch (Entry2._Tag ()) {
									// #casecast Transaction Transaction 
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									//  
									_Output.Write ("\n{0}", _Indent);
									//         /// <summary> 
									_Output.Write ("        /// <summary>\n{0}", _Indent);
									// 		/// Implement the transaction 
									_Output.Write ("		/// Implement the transaction\n{0}", _Indent);
									//         /// </summary>		 
									_Output.Write ("        /// </summary>		\n{0}", _Indent);
									//         public override #{Transaction.Response} #{Transaction.Id} ( 
									_Output.Write ("        public override {1} {2} (\n{0}", _Indent, Transaction.Response, Transaction.Id);
									//                 #{Transaction.Request} Request) { 
									_Output.Write ("                {1} Request) {{\n{0}", _Indent, Transaction.Request);
									//  
									_Output.Write ("\n{0}", _Indent);
									//             var ResponseData = JPCRemoteSession.Post("#{Transaction.Id}", Request); 
									_Output.Write ("            var ResponseData = JPCRemoteSession.Post(\"{1}\", Request);\n{0}", _Indent, Transaction.Id);
									//             var Response = #{Transaction.Response}.FromTagged(ResponseData); 
									_Output.Write ("            var Response = {1}.FromTagged(ResponseData);\n{0}", _Indent, Transaction.Response);
									//  
									_Output.Write ("\n{0}", _Indent);
									//             return Response; 
									_Output.Write ("            return Response;\n{0}", _Indent);
									//             } 
									_Output.Write ("            }}\n{0}", _Indent);
									// #end switchcast 
								break; }
									}
								// #end foreach 
								}
							//  
							_Output.Write ("\n{0}", _Indent);
							// 		} 
							_Output.Write ("		}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//     /// <summary> 
							_Output.Write ("    /// <summary>\n{0}", _Indent);
							// 	/// Client class for #{Service.Id}. 
							_Output.Write ("	/// Client class for {1}.\n{0}", _Indent, Service.Id);
							//     /// </summary>		 
							_Output.Write ("    /// </summary>		\n{0}", _Indent);
							//     public partial class #{Service.Id}Provider : Goedel.Protocol.JPCProvider { 
							_Output.Write ("    public partial class {1}Provider : Goedel.Protocol.JPCProvider {{\n{0}", _Indent, Service.Id);
							//  
							_Output.Write ("\n{0}", _Indent);
							// 		/// <summary> 
							_Output.Write ("		/// <summary>\n{0}", _Indent);
							// 		/// Interface object to dispatch requests to. 
							_Output.Write ("		/// Interface object to dispatch requests to.\n{0}", _Indent);
							// 		/// </summary>	 
							_Output.Write ("		/// </summary>	\n{0}", _Indent);
							// 		public #{Service.Id} Service; 
							_Output.Write ("		public {1} Service;\n{0}", _Indent, Service.Id);
							//  
							_Output.Write ("\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// 		/// <summary> 
							_Output.Write ("		/// <summary>\n{0}", _Indent);
							// 		/// Dispatch object request in specified authentication context. 
							_Output.Write ("		/// Dispatch object request in specified authentication context.\n{0}", _Indent);
							// 		/// </summary>			 
							_Output.Write ("		/// </summary>			\n{0}", _Indent);
							//         /// <param name="Session">The client context.</param> 
							_Output.Write ("        /// <param name=\"Session\">The client context.</param>\n{0}", _Indent);
							//         /// <param name="JSONReader">Reader for data object.</param> 
							_Output.Write ("        /// <param name=\"JSONReader\">Reader for data object.</param>\n{0}", _Indent);
							//         /// <returns>The response object returned by the corresponding dispatch.</returns> 
							_Output.Write ("        /// <returns>The response object returned by the corresponding dispatch.</returns>\n{0}", _Indent);
							// 		public override Goedel.Protocol.JSONObject Dispatch(JPCSession  Session,   
							_Output.Write ("		public override Goedel.Protocol.JSONObject Dispatch(JPCSession  Session,  \n{0}", _Indent);
							// 								Goedel.Protocol.JSONReader JSONReader) { 
							_Output.Write ("								Goedel.Protocol.JSONReader JSONReader) {{\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// 			JSONReader.StartObject (); 
							_Output.Write ("			JSONReader.StartObject ();\n{0}", _Indent);
							// 			string token = JSONReader.ReadToken (); 
							_Output.Write ("			string token = JSONReader.ReadToken ();\n{0}", _Indent);
							// 			JSONObject Response = null; 
							_Output.Write ("			JSONObject Response = null;\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// 			switch (token) { 
							_Output.Write ("			switch (token) {{\n{0}", _Indent);
							// #foreach (_Choice Entry2 in Protocol.Entries) 
							foreach  (_Choice Entry2 in Protocol.Entries) {
								// #switchcast ProtoStructType Entry2 
								switch (Entry2._Tag ()) {
									// #casecast Transaction Transaction 
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									// 				case "#{Transaction.Id}" : { 
									_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Transaction.Id);
									// 					var Request = #{Transaction.Request}.FromTagged (JSONReader); 
									_Output.Write ("					var Request = {1}.FromTagged (JSONReader);\n{0}", _Indent, Transaction.Request);
									// 					Response = Service.#{Transaction.Id} (Request); 
									_Output.Write ("					Response = Service.{1} (Request);\n{0}", _Indent, Transaction.Id);
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
							// 					throw new Goedel.Protocol.UnknownOperation (); 
							_Output.Write ("					throw new Goedel.Protocol.UnknownOperation ();\n{0}", _Indent);
							// 					} 
							_Output.Write ("					}}\n{0}", _Indent);
							// 				} 
							_Output.Write ("				}}\n{0}", _Indent);
							// 			JSONReader.EndObject (); 
							_Output.Write ("			JSONReader.EndObject ();\n{0}", _Indent);
							// 			return Response; 
							_Output.Write ("			return Response;\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// 		} 
							_Output.Write ("		}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 		// Transaction Classes 
					_Output.Write ("		// Transaction Classes\n{0}", _Indent);
					// #foreach (_Choice Entry in Protocol.Entries) 
					foreach  (_Choice Entry in Protocol.Entries) {
						// #switchcast ProtoStructType Entry 
						switch (Entry._Tag ()) {
							// #casecast Message Message 
							case ProtoStructType.Message: {
							  Message Message = (Message) Entry; 
							// #% MakeClass (Message.Id, Message.Entries); 
							
							 MakeClass (Message.Id, Message.Entries);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							// 		/// Initialize class from JSONReader stream. 
							_Output.Write ("		/// Initialize class from JSONReader stream.\n{0}", _Indent);
							//         /// </summary>		 
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							// 		public #{Message.Id} (JSONReader JSONReader) { 
							_Output.Write ("		public {1} (JSONReader JSONReader) {{\n{0}", _Indent, Message.Id);
							// 			Deserialize (JSONReader); 
							_Output.Write ("			Deserialize (JSONReader);\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							// 		/// Initialize class from a JSON encoded class. 
							_Output.Write ("		/// Initialize class from a JSON encoded class.\n{0}", _Indent);
							//         /// </summary>		 
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							// 		public #{Message.Id} (string _String) { 
							_Output.Write ("		public {1} (string _String) {{\n{0}", _Indent, Message.Id);
							// 			Deserialize (_String); 
							_Output.Write ("			Deserialize (_String);\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// #% var Inherits = HasInherits  (Message.Entries); 
							
							 var Inherits = HasInherits  (Message.Entries);
							// #% MakeSerializers (Message.Id, Message.ID, Message.Entries, Inherits); 
							
							 MakeSerializers (Message.Id, Message.ID, Message.Entries, Inherits);
							// 		} 
							_Output.Write ("		}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #casecast Structure Structure 
							break; }
							case ProtoStructType.Structure: {
							  Structure Structure = (Structure) Entry; 
							// #% MakeClass (Structure.Id, Structure.Entries, Structure.Parameterized); 
							
							 MakeClass (Structure.Id, Structure.Entries, Structure.Parameterized);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							// 		/// Initialize class from JSONReader stream. 
							_Output.Write ("		/// Initialize class from JSONReader stream.\n{0}", _Indent);
							//         /// </summary>		 
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							// 		public #{Structure.Id} (JSONReader JSONReader) { 
							_Output.Write ("		public {1} (JSONReader JSONReader) {{\n{0}", _Indent, Structure.Id);
							// 			Deserialize (JSONReader); 
							_Output.Write ("			Deserialize (JSONReader);\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							//         /// <summary>  
							_Output.Write ("        /// <summary> \n{0}", _Indent);
							// 		/// Initialize class from a JSON encoded class. 
							_Output.Write ("		/// Initialize class from a JSON encoded class.\n{0}", _Indent);
							//         /// </summary>		 
							_Output.Write ("        /// </summary>		\n{0}", _Indent);
							// 		public #{Structure.Id} (string _String) { 
							_Output.Write ("		public {1} (string _String) {{\n{0}", _Indent, Structure.Id);
							// 			Deserialize (_String); 
							_Output.Write ("			Deserialize (_String);\n{0}", _Indent);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #% var Inherits = HasInherits  (Structure.Entries); 
							
							 var Inherits = HasInherits  (Structure.Entries);
							// #% MakeSerializers (Structure.Id, Structure.ID, Structure.Entries, Inherits); 
							
							 MakeSerializers (Structure.Id, Structure.ID, Structure.Entries, Inherits);
							// 		} 
							_Output.Write ("		}}\n{0}", _Indent);
							//  
							_Output.Write ("\n{0}", _Indent);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #block  
		

		//
		//  
		//

			// #% public bool IsAbstract  (List<_Choice> Entries) { 
			 public bool IsAbstract  (List<_Choice> Entries) {
			// #% bool result = false; 
			 bool result = false;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Abstract Abstract 
					case ProtoStructType.Abstract: {
					  Abstract Abstract = (Abstract) Entry; 
					// #% result = true; 
					
					 result = true;
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #% return result; 
			 return result;
			// #% } 
			 }
			// #end block  
		
		//  
		// #block  
		

		//
		//  
		//

			// #% public bool IsMultiple  (List<_Choice> Entries) { 
			 public bool IsMultiple  (List<_Choice> Entries) {
			// #% bool result = false; 
			 bool result = false;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Multiple Multiple 
					case ProtoStructType.Multiple: {
					  Multiple Multiple = (Multiple) Entry; 
					// #% result = true; 
					
					 result = true;
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #% return result; 
			 return result;
			// #% } 
			 }
			// #end block  
		
		//  
		// #block  
		

		//
		//  
		//

			// #% public bool IsRequired  (List<_Choice> Entries) { 
			 public bool IsRequired  (List<_Choice> Entries) {
			// #% bool result = false; 
			 bool result = false;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Required Required 
					case ProtoStructType.Required: {
					  Required Required = (Required) Entry; 
					// #% result = true; 
					
					 result = true;
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #% return result; 
			 return result;
			// #% } 
			 }
			// #end block  
		
		//  
		// #block  
		

		//
		//  
		//

			// #% public string HasInherits  (List<_Choice> Entries) { 
			 public string HasInherits  (List<_Choice> Entries) {
			// #% string result = null; 
			 string result = null;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Inherits Inherits 
					case ProtoStructType.Inherits: {
					  Inherits Inherits = (Inherits) Entry; 
					// #% result = Inherits.Ref.ToString(); 
					
					 result = Inherits.Ref.ToString();
					// #casecast External External 
					break; }
					case ProtoStructType.External: {
					  External External = (External) Entry; 
					// #% result = External.Ref.ToString(); 
					
					 result = External.Ref.ToString();
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #% return result; 
			 return result;
			// #% } 
			 }
			// #end block  
		
		//  
		// #block  
		

		//
		//  
		//

			// #% public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries) { 
			 public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries) {
			// #% var Inherits = HasInherits (Entries); 
			 var Inherits = HasInherits (Entries);
			// #!#% string Override; 
			// #% DescriptionListC (Entries, 1); 
			 DescriptionListC (Entries, 1);
			// 	#! 
			_Output.Write ("	", _Indent);
			// #if (IsAbstract (Entries)) 
			if (  (IsAbstract (Entries)) ) {
				// abstract #! 
				_Output.Write ("abstract ", _Indent);
				// #end if 
				}
			// #if (Inherits == null)  
			if (  (Inherits == null)  ) {
				// public partial class #{Id} : #{CurrentPrefix} { 
				_Output.Write ("public partial class {1} : {2} {{\n{0}", _Indent, Id, CurrentPrefix);
				// #else  
				} else {
				// public partial class #{Id} : #{Inherits} { 
				_Output.Write ("public partial class {1} : {2} {{\n{0}", _Indent, Id, Inherits);
				// #end if 
				}
			// #call DeclareMembers (Entries) 
			DeclareMembers ((Entries));
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Tag identifying this class. 
			_Output.Write ("        /// Tag identifying this class.\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <returns>The tag</returns> 
			_Output.Write ("        /// <returns>The tag</returns>\n{0}", _Indent);
			// 		public override string Tag () { 
			_Output.Write ("		public override string Tag () {{\n{0}", _Indent);
			// 			return "#{Id}"; 
			_Output.Write ("			return \"{1}\";\n{0}", _Indent, Id);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Default Constructor 
			_Output.Write ("        /// Default Constructor\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			// 		public #{Id} () { 
			_Output.Write ("		public {1} () {{\n{0}", _Indent, Id);
			// 			_Initialize (); 
			_Output.Write ("			_Initialize ();\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			// #% } 
			 }
			// #end block  
		
		//  
		//  
		// #block  
		

		//
		//  
		//

			// #% public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries, bool Param) { 
			 public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries, bool Param) {
			// #% var Inherits = HasInherits (Entries); 
			 var Inherits = HasInherits (Entries);
			// #!#% string Override; 
			// #% DescriptionListC (Entries, 1); 
			 DescriptionListC (Entries, 1);
			// 	#! 
			_Output.Write ("	", _Indent);
			// #if (IsAbstract (Entries)) 
			if (  (IsAbstract (Entries)) ) {
				// abstract #! 
				_Output.Write ("abstract ", _Indent);
				// #end if 
				}
			// #% var TTT = Param ? "<T>" : ""; 
			 var TTT = Param ? "<T>" : "";
			// #if (Inherits == null)  
			if (  (Inherits == null)  ) {
				// public partial class #{Id}#{TTT} : #{CurrentPrefix} { 
				_Output.Write ("public partial class {1}{2} : {3} {{\n{0}", _Indent, Id, TTT, CurrentPrefix);
				// #else  
				} else {
				// public partial class #{Id}#{TTT} : #{Inherits} { 
				_Output.Write ("public partial class {1}{2} : {3} {{\n{0}", _Indent, Id, TTT, Inherits);
				// #end if 
				}
			// #call DeclareMembers (Entries) 
			DeclareMembers ((Entries));
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Tag identifying this class. 
			_Output.Write ("        /// Tag identifying this class.\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <returns>The tag</returns> 
			_Output.Write ("        /// <returns>The tag</returns>\n{0}", _Indent);
			// 		public override string Tag () { 
			_Output.Write ("		public override string Tag () {{\n{0}", _Indent);
			// 			return "#{Id}"; 
			_Output.Write ("			return \"{1}\";\n{0}", _Indent, Id);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Default Constructor 
			_Output.Write ("        /// Default Constructor\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			// 		public #{Id} () { 
			_Output.Write ("		public {1} () {{\n{0}", _Indent, Id);
			// 			_Initialize (); 
			_Output.Write ("			_Initialize ();\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			// #% } 
			 }
			// #end block  
		
		//  
		//  
		// #block ParameterList 
		

		//
		//  ParameterList
		//

			// #% public void DeclareMembers  (List<_Choice> Entries) { 
			 public void DeclareMembers  (List<_Choice> Entries) {
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #% TOKEN<_Choice> Token = null; 
				 TOKEN<_Choice> Token = null;
				// #% string Type = null; string TType = null; 
				 string Type = null; string TType = null;
				// #% List<_Choice> Options = null; 
				 List<_Choice> Options = null;
				// #% bool Nullable; 
				 bool Nullable;
				// #% string Tag; 
				 string Tag;
				// #% GetType (Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag); 
				 GetType (Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag);
				// #if (Token != null) 
				if (  (Token != null) ) {
					// #% bool Multiple = IsMultiple (Options); 
					 bool Multiple = IsMultiple (Options);
					// #if Multiple 
					if (  Multiple ) {
						// 		/// <summary> 
						_Output.Write ("		/// <summary>\n{0}", _Indent);
						//         ///  
						_Output.Write ("        /// \n{0}", _Indent);
						//         /// </summary> 
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						// 		public virtual List<#{Type}>				#{Token} { 
						_Output.Write ("		public virtual List<{1}>				{2} {{\n{0}", _Indent, Type, Token);
						// 			get {return _#{Token};}			 
						_Output.Write ("			get {{return _{1};}}			\n{0}", _Indent, Token);
						// 			set {_#{Token} = value;} 
						_Output.Write ("			set {{_{1} = value;}}\n{0}", _Indent, Token);
						// 			} 
						_Output.Write ("			}}\n{0}", _Indent);
						// 		List<#{Type}>				_#{Token}; 
						_Output.Write ("		List<{1}>				_{2};\n{0}", _Indent, Type, Token);
						// #else 
						} else {
						// #if !Nullable 
						if (  !Nullable ) {
							// 		bool								__#{Token} = false; 
							_Output.Write ("		bool								__{1} = false;\n{0}", _Indent, Token);
							// 		private #{Type}						_#{Token}; 
							_Output.Write ("		private {1}						_{2};\n{0}", _Indent, Type, Token);
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         ///  
							_Output.Write ("        /// \n{0}", _Indent);
							//         /// </summary> 
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							// 		public virtual #{Type}						#{Token} { 
							_Output.Write ("		public virtual {1}						{2} {{\n{0}", _Indent, Type, Token);
							// 			get {return _#{Token};} 
							_Output.Write ("			get {{return _{1};}}\n{0}", _Indent, Token);
							// 			set {_#{Token} = value; __#{Token} = true; } 
							_Output.Write ("			set {{_{1} = value; __{2} = true; }}\n{0}", _Indent, Token, Token);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// #else  
							} else {
							//         /// <summary> 
							_Output.Write ("        /// <summary>\n{0}", _Indent);
							//         ///  
							_Output.Write ("        /// \n{0}", _Indent);
							//         /// </summary> 
							_Output.Write ("        /// </summary>\n{0}", _Indent);
							// 		public virtual #{Type}						#{Token} { 
							_Output.Write ("		public virtual {1}						{2} {{\n{0}", _Indent, Type, Token);
							// 			get {return _#{Token};}			 
							_Output.Write ("			get {{return _{1};}}			\n{0}", _Indent, Token);
							// 			set {_#{Token} = value;} 
							_Output.Write ("			set {{_{1} = value;}}\n{0}", _Indent, Token);
							// 			} 
							_Output.Write ("			}}\n{0}", _Indent);
							// 		#{Type}						_#{Token} ; 
							_Output.Write ("		{1}						_{2} ;\n{0}", _Indent, Type, Token);
							// #end if 
							}
						// #end if 
						}
					// #end if 
					}
				// #end foreach 
				}
			// #% } 
			 }
			// #end block 
		
		//  
		// #block MakeSerializers 
		

		//
		//  MakeSerializers
		//

			// #% public void MakeSerializers  (ID<_Choice> Id, string STag, List<_Choice> Entries, string Inherits) { 
			 public void MakeSerializers  (ID<_Choice> Id, string STag, List<_Choice> Entries, string Inherits) {
			// #% string IsOverride = (Id.Object.Superclass == null) ? "virtual " : "virtual "; 
			 string IsOverride = (Id.Object.Superclass == null) ? "virtual " : "virtual ";
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Serialize this object to the specified output stream. 
			_Output.Write ("        /// Serialize this object to the specified output stream.\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <param name="Writer">Output stream</param> 
			_Output.Write ("        /// <param name=\"Writer\">Output stream</param>\n{0}", _Indent);
			//         /// <param name="wrap">If true, output is wrapped with object 
			_Output.Write ("        /// <param name=\"wrap\">If true, output is wrapped with object\n{0}", _Indent);
			//         /// start and end sequences '{ ... }'.</param> 
			_Output.Write ("        /// start and end sequences '{{ ... }}'.</param>\n{0}", _Indent);
			//         /// <param name="first">If true, item is the first entry in a list.</param> 
			_Output.Write ("        /// <param name=\"first\">If true, item is the first entry in a list.</param>\n{0}", _Indent);
			// 		public override void Serialize (Writer Writer, bool wrap, ref bool first) { 
			_Output.Write ("		public override void Serialize (Writer Writer, bool wrap, ref bool first) {{\n{0}", _Indent);
			// 			SerializeX (Writer, wrap, ref first); 
			_Output.Write ("			SerializeX (Writer, wrap, ref first);\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Serialize this object to the specified output stream. 
			_Output.Write ("        /// Serialize this object to the specified output stream.\n{0}", _Indent);
			//         /// Unlike the Serlialize() method, this method is not inherited from the 
			_Output.Write ("        /// Unlike the Serlialize() method, this method is not inherited from the\n{0}", _Indent);
			//         /// parent class allowing a specific version of the method to be called. 
			_Output.Write ("        /// parent class allowing a specific version of the method to be called.\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <param name="_Writer">Output stream</param> 
			_Output.Write ("        /// <param name=\"_Writer\">Output stream</param>\n{0}", _Indent);
			//         /// <param name="_wrap">If true, output is wrapped with object 
			_Output.Write ("        /// <param name=\"_wrap\">If true, output is wrapped with object\n{0}", _Indent);
			//         /// start and end sequences '{ ... }'.</param> 
			_Output.Write ("        /// start and end sequences '{{ ... }}'.</param>\n{0}", _Indent);
			//         /// <param name="_first">If true, item is the first entry in a list.</param> 
			_Output.Write ("        /// <param name=\"_first\">If true, item is the first entry in a list.</param>\n{0}", _Indent);
			// 		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) { 
			_Output.Write ("		public new void SerializeX (Writer _Writer, bool _wrap, ref bool _first) {{\n{0}", _Indent);
			// 			if (_wrap) { 
			_Output.Write ("			if (_wrap) {{\n{0}", _Indent);
			// 				_Writer.WriteObjectStart (); 
			_Output.Write ("				_Writer.WriteObjectStart ();\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// #if (Inherits != null) 
			if (  (Inherits != null) ) {
				// 			((#{Inherits})this).SerializeX(_Writer, false, ref _first); 
				_Output.Write ("			(({1})this).SerializeX(_Writer, false, ref _first);\n{0}", _Indent, Inherits);
				// #end if 
				}
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #% TOKEN<_Choice> Token = null; 
				 TOKEN<_Choice> Token = null;
				// #% string Type = null; string TType = null; 
				 string Type = null; string TType = null;
				// #% List<_Choice> Options = null; bool Nullable; 
				 List<_Choice> Options = null; bool Nullable;
				// #% string Tag; 
				 string Tag;
				// #% GetType (Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag); 
				 GetType (Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag);
				// #if (Token != null) 
				if (  (Token != null) ) {
					// #% bool Multiple = IsMultiple (Options); 
					 bool Multiple = IsMultiple (Options);
					// #if Multiple 
					if (  Multiple ) {
						// 			if (#{Token} != null) { 
						_Output.Write ("			if ({1} != null) {{\n{0}", _Indent, Token);
						// 				_Writer.WriteObjectSeparator (ref _first); 
						_Output.Write ("				_Writer.WriteObjectSeparator (ref _first);\n{0}", _Indent);
						// 				_Writer.WriteToken ("#{Tag}", 1); 
						_Output.Write ("				_Writer.WriteToken (\"{1}\", 1);\n{0}", _Indent, Tag);
						// 				_Writer.WriteArrayStart (); 
						_Output.Write ("				_Writer.WriteArrayStart ();\n{0}", _Indent);
						// 				bool _firstarray = true; 
						_Output.Write ("				bool _firstarray = true;\n{0}", _Indent);
						// 				foreach (var _index in #{Token}) { 
						_Output.Write ("				foreach (var _index in {1}) {{\n{0}", _Indent, Token);
						// 					_Writer.WriteArraySeparator (ref _firstarray); 
						_Output.Write ("					_Writer.WriteArraySeparator (ref _firstarray);\n{0}", _Indent);
						// #% MakeSerializeArrayEntry (Entry, "_index"); 
						 MakeSerializeArrayEntry (Entry, "_index");
						// 					} 
						_Output.Write ("					}}\n{0}", _Indent);
						// 				_Writer.WriteArrayEnd (); 
						_Output.Write ("				_Writer.WriteArrayEnd ();\n{0}", _Indent);
						// 				} 
						_Output.Write ("				}}\n{0}", _Indent);
						// #else 
						} else {
						// #if Nullable 
						if (  Nullable ) {
							// 			if (#{Token} != null) { 
							_Output.Write ("			if ({1} != null) {{\n{0}", _Indent, Token);
							// #else 
							} else {
							// 			if (__#{Token}){ 
							_Output.Write ("			if (__{1}){{\n{0}", _Indent, Token);
							// #end if 
							}
						// 				_Writer.WriteObjectSeparator (ref _first); 
						_Output.Write ("				_Writer.WriteObjectSeparator (ref _first);\n{0}", _Indent);
						// 				_Writer.WriteToken ("#{Tag}", 1); 
						_Output.Write ("				_Writer.WriteToken (\"{1}\", 1);\n{0}", _Indent, Tag);
						// #% MakeSerializeEntry (Entry, Token.ToString()); 
						 MakeSerializeEntry (Entry, Token.ToString());
						// 				} 
						_Output.Write ("				}}\n{0}", _Indent);
						// #end if 
						}
					// #if Multiple 
					if (  Multiple ) {
						//  
						_Output.Write ("\n{0}", _Indent);
						// #end if 
						}
					// #end if 
					}
				// #end foreach 
				}
			// 			if (_wrap) { 
			_Output.Write ("			if (_wrap) {{\n{0}", _Indent);
			// 				_Writer.WriteObjectEnd (); 
			_Output.Write ("				_Writer.WriteObjectEnd ();\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (!IsAbstract (Entries)) 
			if (  (!IsAbstract (Entries)) ) {
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				// 		/// Create a new instance from untagged byte input. 
				_Output.Write ("		/// Create a new instance from untagged byte input.\n{0}", _Indent);
				// 		/// i.e. {... data ... } 
				_Output.Write ("		/// i.e. {{... data ... }}\n{0}", _Indent);
				//         /// </summary>	 
				_Output.Write ("        /// </summary>	\n{0}", _Indent);
				//         /// <param name="_Data">The input data.</param> 
				_Output.Write ("        /// <param name=\"_Data\">The input data.</param>\n{0}", _Indent);
				//         /// <returns>The created object.</returns>		 
				_Output.Write ("        /// <returns>The created object.</returns>		\n{0}", _Indent);
				// 		public static new #{Id} From (byte[] _Data) { 
				_Output.Write ("		public static new {1} From (byte[] _Data) {{\n{0}", _Indent, Id);
				// 			var _Input = System.Text.Encoding.UTF8.GetString(_Data); 
				_Output.Write ("			var _Input = System.Text.Encoding.UTF8.GetString(_Data);\n{0}", _Indent);
				// 			return From (_Input); 
				_Output.Write ("			return From (_Input);\n{0}", _Indent);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//         /// <summary> 
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				// 		/// Create a new instance from untagged string input. 
				_Output.Write ("		/// Create a new instance from untagged string input.\n{0}", _Indent);
				// 		/// i.e. {... data ... } 
				_Output.Write ("		/// i.e. {{... data ... }}\n{0}", _Indent);
				//         /// </summary>	 
				_Output.Write ("        /// </summary>	\n{0}", _Indent);
				//         /// <param name="_Input">The input data.</param> 
				_Output.Write ("        /// <param name=\"_Input\">The input data.</param>\n{0}", _Indent);
				//         /// <returns>The created object.</returns>				 
				_Output.Write ("        /// <returns>The created object.</returns>				\n{0}", _Indent);
				// 		public static new #{Id} From (string _Input) { 
				_Output.Write ("		public static new {1} From (string _Input) {{\n{0}", _Indent, Id);
				// 			StringReader _Reader = new StringReader (_Input); 
				_Output.Write ("			StringReader _Reader = new StringReader (_Input);\n{0}", _Indent);
				//             JSONReader JSONReader = new JSONReader (_Reader); 
				_Output.Write ("            JSONReader JSONReader = new JSONReader (_Reader);\n{0}", _Indent);
				// 			return new #{Id} (JSONReader); 
				_Output.Write ("			return new {1} (JSONReader);\n{0}", _Indent, Id);
				// 			} 
				_Output.Write ("			}}\n{0}", _Indent);
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			// 		/// Create a new instance from tagged byte input. 
			_Output.Write ("		/// Create a new instance from tagged byte input.\n{0}", _Indent);
			// 		/// i.e. { "#{Id}" : {... data ... } } 
			_Output.Write ("		/// i.e. {{ \"{1}\" : {{... data ... }} }}\n{0}", _Indent, Id);
			//         /// </summary>	 
			_Output.Write ("        /// </summary>	\n{0}", _Indent);
			//         /// <param name="_Data">The input data.</param> 
			_Output.Write ("        /// <param name=\"_Data\">The input data.</param>\n{0}", _Indent);
			//         /// <returns>The created object.</returns>				 
			_Output.Write ("        /// <returns>The created object.</returns>				\n{0}", _Indent);
			// 		public static new #{Id} FromTagged (byte[] _Data) { 
			_Output.Write ("		public static new {1} FromTagged (byte[] _Data) {{\n{0}", _Indent, Id);
			// 			var _Input = System.Text.Encoding.UTF8.GetString(_Data); 
			_Output.Write ("			var _Input = System.Text.Encoding.UTF8.GetString(_Data);\n{0}", _Indent);
			// 			return FromTagged (_Input); 
			_Output.Write ("			return FromTagged (_Input);\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Create a new instance from tagged string input. 
			_Output.Write ("        /// Create a new instance from tagged string input.\n{0}", _Indent);
			// 		/// i.e. { "#{Id}" : {... data ... } } 
			_Output.Write ("		/// i.e. {{ \"{1}\" : {{... data ... }} }}\n{0}", _Indent, Id);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <param name="_Input">The input data.</param> 
			_Output.Write ("        /// <param name=\"_Input\">The input data.</param>\n{0}", _Indent);
			//         /// <returns>The created object.</returns>		 
			_Output.Write ("        /// <returns>The created object.</returns>		\n{0}", _Indent);
			// 		public static new #{Id} FromTagged (string _Input) { 
			_Output.Write ("		public static new {1} FromTagged (string _Input) {{\n{0}", _Indent, Id);
			// 			//#{Id} _Result; 
			_Output.Write ("			//{1} _Result;\n{0}", _Indent, Id);
			// 			//Deserialize (_Input, out _Result); 
			_Output.Write ("			//Deserialize (_Input, out _Result);\n{0}", _Indent);
			// 			StringReader _Reader = new StringReader (_Input); 
			_Output.Write ("			StringReader _Reader = new StringReader (_Input);\n{0}", _Indent);
			//             JSONReader JSONReader = new JSONReader (_Reader); 
			_Output.Write ("            JSONReader JSONReader = new JSONReader (_Reader);\n{0}", _Indent);
			// 			return FromTagged (JSONReader) ; 
			_Output.Write ("			return FromTagged (JSONReader) ;\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Deserialize a tagged stream 
			_Output.Write ("        /// Deserialize a tagged stream\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <param name="JSONReader"></param> 
			_Output.Write ("        /// <param name=\"JSONReader\"></param>\n{0}", _Indent);
			//         public static new #{Id}  FromTagged (JSONReader JSONReader) { 
			_Output.Write ("        public static new {1}  FromTagged (JSONReader JSONReader) {{\n{0}", _Indent, Id);
			// 			#{Id} Out = null; 
			_Output.Write ("			{1} Out = null;\n{0}", _Indent, Id);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			JSONReader.StartObject (); 
			_Output.Write ("			JSONReader.StartObject ();\n{0}", _Indent);
			//             if (JSONReader.EOR) { 
			_Output.Write ("            if (JSONReader.EOR) {{\n{0}", _Indent);
			//                 return null; 
			_Output.Write ("                return null;\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			string token = JSONReader.ReadToken (); 
			_Output.Write ("			string token = JSONReader.ReadToken ();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			switch (token) { 
			_Output.Write ("			switch (token) {{\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #% MapInheritors (Id, STag); 
			 MapInheritors (Id, STag);
			// 				default : { 
			_Output.Write ("				default : {{\n{0}", _Indent);
			// 					//Ignore the unknown data 
			_Output.Write ("					//Ignore the unknown data\n{0}", _Indent);
			//                     //throw new Exception ("Not supported"); 
			_Output.Write ("                    //throw new Exception (\"Not supported\");\n{0}", _Indent);
			//                     break; 
			_Output.Write ("                    break;\n{0}", _Indent);
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			JSONReader.EndObject (); 
			_Output.Write ("			JSONReader.EndObject ();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			return Out; 
			_Output.Write ("			return Out;\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         /// <summary> 
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			//         /// Having read a tag, process the corresponding value data. 
			_Output.Write ("        /// Having read a tag, process the corresponding value data.\n{0}", _Indent);
			//         /// </summary> 
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			//         /// <param name="JSONReader"></param> 
			_Output.Write ("        /// <param name=\"JSONReader\"></param>\n{0}", _Indent);
			//         /// <param name="Tag"></param> 
			_Output.Write ("        /// <param name=\"Tag\"></param>\n{0}", _Indent);
			// 		public override void DeserializeToken (JSONReader JSONReader, string Tag) { 
			_Output.Write ("		public override void DeserializeToken (JSONReader JSONReader, string Tag) {{\n{0}", _Indent);
			// 			 
			_Output.Write ("			\n{0}", _Indent);
			// 			switch (Tag) { 
			_Output.Write ("			switch (Tag) {{\n{0}", _Indent);
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #% TOKEN<_Choice> Token = null; 
				 TOKEN<_Choice> Token = null;
				// #% string Type = null; string TType = null; 
				 string Type = null; string TType = null;
				// #% List<_Choice> Options = null; 
				 List<_Choice> Options = null;
				// #% bool Nullable; 
				 bool Nullable;
				// #% string Tag; 
				 string Tag;
				// #% GetType(Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag); 
				 GetType(Entry, out Token, out Type, out TType, out Options, out Nullable, out Tag);
				// #if (Token != null) 
				if (  (Token != null) ) {
					// #% bool Multiple = IsMultiple (Options); 
					 bool Multiple = IsMultiple (Options);
					// 				case "#{Tag}" : { 
					_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Tag);
					// #if Multiple 
					if (  Multiple ) {
						// 					// Have a sequence of values 
						_Output.Write ("					// Have a sequence of values\n{0}", _Indent);
						// 					bool _Going = JSONReader.StartArray (); 
						_Output.Write ("					bool _Going = JSONReader.StartArray ();\n{0}", _Indent);
						// 					#{Token} = new List <#{Type}> (); 
						_Output.Write ("					{1} = new List <{2}> ();\n{0}", _Indent, Token, Type);
						// 					while (_Going) { 
						_Output.Write ("					while (_Going) {{\n{0}", _Indent);
						// #if Entry._Tag () == ProtoStructType.Struct 
						if (  Entry._Tag () == ProtoStructType.Struct ) {
							// 						// an untagged structure. 
							_Output.Write ("						// an untagged structure.\n{0}", _Indent);
							// 						var _Item = new #{Type} (JSONReader); 
							_Output.Write ("						var _Item = new {1} (JSONReader);\n{0}", _Indent, Type);
							// #elseif Entry._Tag () == ProtoStructType.TStruct 
							} else if (  Entry._Tag () == ProtoStructType.TStruct) {
							// 						var _Item = #{Type}.FromTagged (JSONReader); // a tagged structure 
							_Output.Write ("						var _Item = {1}.FromTagged (JSONReader); // a tagged structure\n{0}", _Indent, Type);
							// #else 
							} else {
							// 						#{Type} _Item = JSONReader.Read#{TType} (); 
							_Output.Write ("						{1} _Item = JSONReader.Read{2} ();\n{0}", _Indent, Type, TType);
							// #end if 
							}
						// 						#{Token}.Add (_Item); 
						_Output.Write ("						{1}.Add (_Item);\n{0}", _Indent, Token);
						// 						_Going = JSONReader.NextArray (); 
						_Output.Write ("						_Going = JSONReader.NextArray ();\n{0}", _Indent);
						// 						} 
						_Output.Write ("						}}\n{0}", _Indent);
						// #else 
						} else {
						// #if Entry._Tag () == ProtoStructType.Struct 
						if (  Entry._Tag () == ProtoStructType.Struct ) {
							// 					// An untagged structure 
							_Output.Write ("					// An untagged structure\n{0}", _Indent);
							// 					#{Token} = new #{Type} (JSONReader); 
							_Output.Write ("					{1} = new {2} (JSONReader);\n{0}", _Indent, Token, Type);
							//   
							_Output.Write (" \n{0}", _Indent);
							// #elseif Entry._Tag () == ProtoStructType.TStruct 
							} else if (  Entry._Tag () == ProtoStructType.TStruct) {
							// 					#{Token} = #{Type}.FromTagged (JSONReader) ;  // A tagged structure 
							_Output.Write ("					{1} = {2}.FromTagged (JSONReader) ;  // A tagged structure\n{0}", _Indent, Token, Type);
							// #else 
							} else {
							// 					#{Token} = JSONReader.Read#{TType} (); 
							_Output.Write ("					{1} = JSONReader.Read{2} ();\n{0}", _Indent, Token, TType);
							// #end if 
							}
						// #end if 
						}
					// 					break; 
					_Output.Write ("					break;\n{0}", _Indent);
					// 					} 
					_Output.Write ("					}}\n{0}", _Indent);
					// #end if 
					}
				// #end foreach 
				}
			// 				default : { 
			_Output.Write ("				default : {{\n{0}", _Indent);
			// #if (Inherits != null) 
			if (  (Inherits != null) ) {
				// 					base.DeserializeToken(JSONReader, Tag); 
				_Output.Write ("					base.DeserializeToken(JSONReader, Tag);\n{0}", _Indent);
				// #end if 
				}
			// 					break; 
			_Output.Write ("					break;\n{0}", _Indent);
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			// 				} 
			_Output.Write ("				}}\n{0}", _Indent);
			// 			// check up that all the required elements are present 
			_Output.Write ("			// check up that all the required elements are present\n{0}", _Indent);
			// 			} 
			_Output.Write ("			}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #% } 
			 }
			// #end block 
		
		//  
		// #method2 MakeSerializeEntry _Choice Entry string Tag 
		

		//
		// MakeSerializeEntry
		//
		public void MakeSerializeEntry (_Choice Entry, string Tag) {
			// #switchcast ProtoStructType Entry 
			switch (Entry._Tag ()) {
				// #casecast Boolean Param 
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				// 					_Writer.WriteBoolean (#{Tag}); 
				_Output.Write ("					_Writer.WriteBoolean ({1});\n{0}", _Indent, Tag);
				// #casecast Integer Param 
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				// 					_Writer.WriteInteger32 (#{Tag}); 
				_Output.Write ("					_Writer.WriteInteger32 ({1});\n{0}", _Indent, Tag);
				// #casecast Binary Param 
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				// 					_Writer.WriteBinary (#{Tag}); 
				_Output.Write ("					_Writer.WriteBinary ({1});\n{0}", _Indent, Tag);
				// #casecast Struct Param 
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				// 					#{Tag}.Serialize (_Writer, false); 
				_Output.Write ("					{1}.Serialize (_Writer, false);\n{0}", _Indent, Tag);
				// #casecast TStruct Param 
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				// 					// expand this to a tagged structure 
				_Output.Write ("					// expand this to a tagged structure\n{0}", _Indent);
				// 					//#{Tag}.Serialize (_Writer, false); 
				_Output.Write ("					//{1}.Serialize (_Writer, false);\n{0}", _Indent, Tag);
				// 					{ 
				_Output.Write ("					{{\n{0}", _Indent);
				// 						_Writer.WriteObjectStart(); 
				_Output.Write ("						_Writer.WriteObjectStart();\n{0}", _Indent);
				// 						_Writer.WriteToken(#{Tag}.Tag(), 1); 
				_Output.Write ("						_Writer.WriteToken({1}.Tag(), 1);\n{0}", _Indent, Tag);
				// 						bool firstinner = true; 
				_Output.Write ("						bool firstinner = true;\n{0}", _Indent);
				// 						#{Tag}.Serialize (_Writer, true, ref firstinner); 
				_Output.Write ("						{1}.Serialize (_Writer, true, ref firstinner);\n{0}", _Indent, Tag);
				// 						_Writer.WriteObjectEnd(); 
				_Output.Write ("						_Writer.WriteObjectEnd();\n{0}", _Indent);
				// 						} 
				_Output.Write ("						}}\n{0}", _Indent);
				// #casecast Label Param 
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast Name Param 
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast String Param 
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast URI Param 
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast DateTime Param 
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				// 					_Writer.WriteDateTime (#{Tag}); 
				_Output.Write ("					_Writer.WriteDateTime ({1});\n{0}", _Indent, Tag);
				// #%							break; } 
				
											break; }
				// #% default : { 
				
				 default : {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end switchcast 
			break; }
				}
			// #end method2 
			}
		//  
		// #method2 MakeSerializeArrayEntry _Choice Entry string Tag 
		

		//
		// MakeSerializeArrayEntry
		//
		public void MakeSerializeArrayEntry (_Choice Entry, string Tag) {
			// #switchcast ProtoStructType Entry 
			switch (Entry._Tag ()) {
				// #casecast Boolean Param 
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				// 					_Writer.WriteBoolean (#{Tag}); 
				_Output.Write ("					_Writer.WriteBoolean ({1});\n{0}", _Indent, Tag);
				// #casecast Integer Param 
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				// 					_Writer.WriteInteger32 (#{Tag}); 
				_Output.Write ("					_Writer.WriteInteger32 ({1});\n{0}", _Indent, Tag);
				// #casecast Binary Param 
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				// 					_Writer.WriteBinary (#{Tag}); 
				_Output.Write ("					_Writer.WriteBinary ({1});\n{0}", _Indent, Tag);
				// #casecast Struct Param 
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				// 					// This is an untagged structure. Cannot inherit. 
				_Output.Write ("					// This is an untagged structure. Cannot inherit.\n{0}", _Indent);
				//                     //_Writer.WriteObjectStart(); 
				_Output.Write ("                    //_Writer.WriteObjectStart();\n{0}", _Indent);
				//                     //_Writer.WriteToken(#{Tag}.Tag(), 1); 
				_Output.Write ("                    //_Writer.WriteToken({1}.Tag(), 1);\n{0}", _Indent, Tag);
				// 					bool firstinner = true; 
				_Output.Write ("					bool firstinner = true;\n{0}", _Indent);
				// 					#{Tag}.Serialize (_Writer, true, ref firstinner); 
				_Output.Write ("					{1}.Serialize (_Writer, true, ref firstinner);\n{0}", _Indent, Tag);
				//                     //_Writer.WriteObjectEnd(); 
				_Output.Write ("                    //_Writer.WriteObjectEnd();\n{0}", _Indent);
				// #casecast TStruct Param 
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				//                     _Writer.WriteObjectStart(); 
				_Output.Write ("                    _Writer.WriteObjectStart();\n{0}", _Indent);
				//                     _Writer.WriteToken(#{Tag}.Tag(), 1); 
				_Output.Write ("                    _Writer.WriteToken({1}.Tag(), 1);\n{0}", _Indent, Tag);
				// 					bool firstinner = true; 
				_Output.Write ("					bool firstinner = true;\n{0}", _Indent);
				// 					#{Tag}.Serialize (_Writer, true, ref firstinner); 
				_Output.Write ("					{1}.Serialize (_Writer, true, ref firstinner);\n{0}", _Indent, Tag);
				//                     _Writer.WriteObjectEnd(); 
				_Output.Write ("                    _Writer.WriteObjectEnd();\n{0}", _Indent);
				// #casecast Label Param 
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast Name Param 
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast String Param 
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast URI Param 
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				// 					_Writer.WriteString (#{Tag}); 
				_Output.Write ("					_Writer.WriteString ({1});\n{0}", _Indent, Tag);
				// #casecast DateTime Param 
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				// 					_Writer.WriteDateTime (#{Tag}); 
				_Output.Write ("					_Writer.WriteDateTime ({1});\n{0}", _Indent, Tag);
				// #%							break; } 
				
											break; }
				// #% default : { 
				
				 default : {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end switchcast 
			break; }
				}
			// #end method2 
			}
		//  
		//  
		// #block 
		

		//
		// 
		//

			// #% void GetType (_Choice Entry, out TOKEN<_Choice> Token, out string Type, out string TType,  
			 void GetType (_Choice Entry, out TOKEN<_Choice> Token, out string Type, out string TType, 
			// #%				out List<_Choice> Options, out bool Nullable, out string Tag) { 
							out List<_Choice> Options, out bool Nullable, out string Tag) {
			// #% Nullable = true; 
			 Nullable = true;
			// #switchcast ProtoStructType Entry 
			switch (Entry._Tag ()) {
				// #casecast Boolean Param 
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "bool";  Nullable = false;TType = "Boolean"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "bool";  Nullable = false;TType = "Boolean";
				// #casecast Integer Param 
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "int"; Nullable = false;TType = "Integer32"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "int"; Nullable = false;TType = "Integer32";
				// #casecast Binary Param 
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "byte[]";TType = "Binary"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "byte[]";TType = "Binary";
				// #casecast Label Param 
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				// #casecast Name Param 
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				// #casecast String Param 
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				// #casecast URI Param 
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "string";TType = "String";
				// #casecast DateTime Param 
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "DateTime"; Nullable = false; TType = "DateTime"; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "DateTime"; Nullable = false; TType = "DateTime";
				// #casecast Struct Param 
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type;
				// #casecast TStruct Param 
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				// #% Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type; 
				
				 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type;
				// #% break; } default : { 
				
				 break; } default : {
				// #% Tag = null ; Token = null; Options = null; Type = null; TType = null; 
				
				 Tag = null ; Token = null; Options = null; Type = null; TType = null;
				// #end switchcast 
			break; }
				}
			// #% } 
			 }
			// #end block 
		
		//  
		// #block 
		

		//
		// 
		//

			// #% void GetSerializerz (_Choice Entry, out TOKEN<_Choice> Token, out string Type, 
			 void GetSerializerz (_Choice Entry, out TOKEN<_Choice> Token, out string Type,
			// #%				out List<_Choice> Options) { 
							out List<_Choice> Options) {
			// #switchcast ProtoStructType Entry 
			switch (Entry._Tag ()) {
				// #casecast Boolean Param 
				case ProtoStructType.Boolean: {
				  Boolean Param = (Boolean) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "Boolean"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "Boolean";
				// #casecast Integer Param 
				break; }
				case ProtoStructType.Integer: {
				  Integer Param = (Integer) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "Integer32"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "Integer32";
				// #casecast Binary Param 
				break; }
				case ProtoStructType.Binary: {
				  Binary Param = (Binary) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "Binary"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "Binary";
				// #casecast Label Param 
				break; }
				case ProtoStructType.Label: {
				  Label Param = (Label) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "String"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				// #casecast Name Param 
				break; }
				case ProtoStructType.Name: {
				  Name Param = (Name) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "String"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				// #casecast String Param 
				break; }
				case ProtoStructType.String: {
				  String Param = (String) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "String"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				// #casecast URI Param 
				break; }
				case ProtoStructType.URI: {
				  URI Param = (URI) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "String"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "String";
				// #casecast DateTime Param 
				break; }
				case ProtoStructType.DateTime: {
				  DateTime Param = (DateTime) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = "DateTime"; 
				
				 Token = Param.Id; Options = Param.Options; Type = "DateTime";
				// #casecast Struct Param 
				break; }
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); 
				
				 Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString();
				// #casecast TStruct Param 
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				// #% Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); 
				
				 Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString();
				// #% break; } default : { 
				
				 break; } default : {
				// #% Token = null; Options = null; Type = null; 
				
				 Token = null; Options = null; Type = null;
				// #end switchcast 
			break; }
				}
			// #% } 
			 }
			// #end block 
		
		//  
		// #block DeserializeCase 
		

		//
		//  DeserializeCase
		//

			// #% void DeserializeCase (ID<_Choice> Id, string Tag) { 
			 void DeserializeCase (ID<_Choice> Id, string Tag) {
			//  
			_Output.Write ("\n{0}", _Indent);
			// 				case "#{Id}" : { 
			_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Id);
			// #if Id.Object.IsAbstract 
			if (  Id.Object.IsAbstract ) {
				// 					Out = null; 
				_Output.Write ("					Out = null;\n{0}", _Indent);
				// 					throw new Exception ("Can't create abstract type"); 
				_Output.Write ("					throw new Exception (\"Can't create abstract type\");\n{0}", _Indent);
				// #else 
				} else {
				// 					var Result = new #{Id} (); 
				_Output.Write ("					var Result = new {1} ();\n{0}", _Indent, Id);
				// 					Result.Deserialize (JSONReader); 
				_Output.Write ("					Result.Deserialize (JSONReader);\n{0}", _Indent);
				// 					Out = Result; 
				_Output.Write ("					Out = Result;\n{0}", _Indent);
				// 					break; 
				_Output.Write ("					break;\n{0}", _Indent);
				// #end if 
				}
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #%	} 
				}
			// #end block 
		
		//  
		// #block MapInheritors 
		

		//
		//  MapInheritors
		//

			// #% void MapInheritors (ID<_Choice> Id, string Tag) { 
			 void MapInheritors (ID<_Choice> Id, string Tag) {
			// 				case "#{Id}" : { 
			_Output.Write ("				case \"{1}\" : {{\n{0}", _Indent, Id);
			// #if Id.Object.IsAbstract 
			if (  Id.Object.IsAbstract ) {
				// 					Out = null; 
				_Output.Write ("					Out = null;\n{0}", _Indent);
				// 					throw new Exception ("Can't create abstract type"); 
				_Output.Write ("					throw new Exception (\"Can't create abstract type\");\n{0}", _Indent);
				// #else 
				} else {
				// 					var Result = new #{Id} (); 
				_Output.Write ("					var Result = new {1} ();\n{0}", _Indent, Id);
				// 					Result.Deserialize (JSONReader); 
				_Output.Write ("					Result.Deserialize (JSONReader);\n{0}", _Indent);
				// 					Out = Result; 
				_Output.Write ("					Out = Result;\n{0}", _Indent);
				// 					break; 
				_Output.Write ("					break;\n{0}", _Indent);
				// #end if 
				}
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (REF<_Choice> Ref in Id.REFs) 
			foreach  (REF<_Choice> Ref in Id.REFs) {
				// #switchcast ProtoStructType Ref.Object 
				switch (Ref.Object._Tag ()) {
					// #casecast Message Message 
					case ProtoStructType.Message: {
					  Message Message = (Message) Ref.Object; 
					// #% MapInheritors (Message.Id, Message.ID); 
					
					 MapInheritors (Message.Id, Message.ID);
					// #casecast Structure Structure 
					break; }
					case ProtoStructType.Structure: {
					  Structure Structure = (Structure) Ref.Object; 
					// #% MapInheritors (Structure.Id, Structure.ID); 
					
					 MapInheritors (Structure.Id, Structure.ID);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #%	} 
				}
			// #end block 
		
		//  
		//  
		// #block DescriptionListC 
		

		//
		//  DescriptionListC
		//

			// #% public void DescriptionListC  (List<_Choice> Entries, int indent) { 
			 public void DescriptionListC  (List<_Choice> Entries, int indent) {
			// #% Indentify (indent); 
			 Indentify (indent);
			// #if (indent > 0)  
			if (  (indent > 0)  ) {
				// /// <summary> 
				_Output.Write ("/// <summary>\n{0}", _Indent);
				// #end if 
				}
			// #% bool first = true; 
			 bool first = true;
			// #foreach (_Choice Entry in Entries) 
			foreach  (_Choice Entry in Entries) {
				// #switchcast ProtoStructType Entry 
				switch (Entry._Tag ()) {
					// #casecast Description Description 
					case ProtoStructType.Description: {
					  Description Description = (Description) Entry; 
					// #if first 
					if (  first ) {
						// #% first = false; 
						 first = false;
						// #elseFstart 
						// #% Indentify (indent); 
						 Indentify (indent);
						// /// 
						_Output.Write ("///\n{0}", _Indent);
						// #end if 
						}
					// #foreach (string s in Description.Text1) 
					foreach  (string s in Description.Text1) {
						// #for (int i=0; i<indent; i++) 
						for  (int i=0; i<indent; i++) {
							// 	#! 
							_Output.Write ("	", _Indent);
							// #end for 
							}
						// /// #{s} 
						_Output.Write ("/// {1}\n{0}", _Indent, s);
						// #end foreach 
						}
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #% Indentify (indent); 
			 Indentify (indent);
			// #if (indent > 0)  
			if (  (indent > 0)  ) {
				// /// </summary> 
				_Output.Write ("/// </summary>\n{0}", _Indent);
				// #end if 
				}
			// #% } 
			 }
			// #end block 
		
		// #end xclass 
		}
	}
