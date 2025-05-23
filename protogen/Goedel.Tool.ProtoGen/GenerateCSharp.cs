// Script Syntax Version:  1.0

//  © 2015-2021 by Threshold Secrets LLC.
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
using  Goedel.Utilities;
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ProtoGen;
public partial class Generate : global::Goedel.Registry.Script {

	 Separator Separator = new Separator (",");
	//
	//   Code Generator (C#)
	//
	//  
	
	/// <summary>	
	/// GenerateCS
	/// </summary>
	/// <param name="ProtoStruct"></param>
	public void GenerateCS (ProtoStruct ProtoStruct) {
		 ProtoStruct.Complete ();
		 var GenerateTime =System.DateTime.UtcNow;
		 Boilerplate.MITLicense (_Output, "//  ", "Copyright (c) " + "2016", ".");
		 Boilerplate.Header (_Output, "//  ", GenerateTime);
		 string Namespace;
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("using System;\n{0}", _Indent);
		_Output.Write ("using System.IO;\n{0}", _Indent);
		_Output.Write ("using System.Collections;\n{0}", _Indent);
		_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
		_Output.Write ("using System.Runtime.CompilerServices;\n{0}", _Indent);
		_Output.Write ("using System.Text;\n{0}", _Indent);
		_Output.Write ("using System.Text.Json;\n{0}", _Indent);
		_Output.Write ("using System.Text.Json.Serialization;\n{0}", _Indent);
		_Output.Write ("using Goedel.Protocol;\n{0}", _Indent);
		_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE0079\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE1006\n{0}", _Indent);
		_Output.Write ("#pragma warning disable CA2255 // The 'ModuleInitializer' attribute should not be used in libraries\n{0}", _Indent);
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
				_Output.Write ("namespace {1};\n{0}", _Indent, Namespace);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				
				 DescriptionListC (Protocol.Entries, 1);
				_Output.Write ("public abstract partial class {1} {2} {{\n{0}", _Indent, Protocol.Prefix, Protocol.ThisInherits);
				
				 CurrentPrefix = Protocol.Prefix.ToString ();
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Tag identifying this class\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public override string _Tag =>__Tag;\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Tag identifying this class\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public new const string __Tag = \"{1}\";\n{0}", _Indent, Protocol.Prefix);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Dictionary mapping tags to factory methods\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public static Dictionary<string, JsonFactoryDelegate> _TagDictionary=> _tagDictionary;\n{0}", _Indent);
				_Output.Write ("	static Dictionary<string, JsonFactoryDelegate> _tagDictionary = \n{0}", _Indent);
				_Output.Write ("			new () {{\n{0}", _Indent);
				
				 Separator.IsFirst = true;
				foreach  (_Choice Entry in Protocol.Structures) {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("	    {{\"{1}\", {2}._Factory}}", _Indent, Entry.ID, Entry.XID ?? Entry.ID);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		}};\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Dictionary mapping types to bindings\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("	public static Dictionary<System.Type, Binding> _BindingDictionary=> _bindingDictionary;\n{0}", _Indent);
				_Output.Write ("	static Dictionary<System.Type, Binding> _bindingDictionary = \n{0}", _Indent);
				_Output.Write ("			new () {{\n{0}", _Indent);
				
				 Separator.IsFirst = true;
				foreach  (_Choice Entry in Protocol.Structures) {
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("	    {{typeof({1}), {2}._binding}}", _Indent, Entry.XID ?? Entry.ID, Entry.XID ?? Entry.ID);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("		}};\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	///<summary>Variable used to force static initialization</summary> \n{0}", _Indent);
				_Output.Write ("	public static bool _Initialized => true;\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	static {1}() {{\n{0}", _Indent, CurrentPrefix);
				_Output.Write ("		_Initialize();\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("    internal static void _Initialize() {{\n{0}", _Indent);
				_Output.Write ("		AddDictionary(ref _tagDictionary);\n{0}", _Indent);
				_Output.Write ("		AddDictionary(ref _bindingDictionary);\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	/// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Construct an instance from the specified tagged JsonReader stream.\n{0}", _Indent);
				_Output.Write ("    /// </summary>\n{0}", _Indent);
				_Output.Write ("    /// <param name=\"jsonReader\">Input stream</param>\n{0}", _Indent);
				_Output.Write ("    /// <param name=\"result\">The created object</param>\n{0}", _Indent);
				_Output.Write ("    public static void Deserialize(JsonReader jsonReader, out JsonObject result) => \n{0}", _Indent);
				_Output.Write ("		result = jsonReader.ReadTaggedObject(_TagDictionary);\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("// Service Dispatch Classes\n{0}", _Indent);
				foreach  (_Choice Entry in Protocol.Entries) {
					switch (Entry._Tag ()) {
						case ProtoStructType.Service: {
						  Service Service = (Service) Entry; 
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("/// <summary>\n{0}", _Indent);
						_Output.Write ("/// The new base class for the client and service side APIs.\n{0}", _Indent);
						_Output.Write ("/// </summary>		\n{0}", _Indent);
						_Output.Write ("public abstract partial class {1} : Goedel.Protocol.JpcInterface {{\n{0}", _Indent, Service.Id);
						_Output.Write ("		\n{0}", _Indent);
						_Output.Write ("    /// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Well Known service identifier.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("	public const string WellKnown = \"{1}\";\n{0}", _Indent, Service.WellKnown);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
						_Output.Write ("	public override string GetWellKnown => WellKnown;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    /// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Well Known service identifier.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("	public const string Discovery = \"{1}\";\n{0}", _Indent, Service.Discovery);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
						_Output.Write ("	public override string GetDiscovery => Discovery;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	///<inheritdoc/>\n{0}", _Indent);
						_Output.Write ("	public override Dictionary<string, JsonFactoryDelegate>  GetTagDictionary() => _TagDictionary;\n{0}", _Indent);
						_Output.Write ("		\n{0}", _Indent);
						_Output.Write ("	static Dictionary<string, JsonFactoryDelegate> _TagDictionary = new () {{", _Indent);
						
						 var separator = new Separator (",");
						foreach  (_Choice Entry2 in Protocol.Entries) {
							switch (Entry2._Tag ()) {
								case ProtoStructType.Transaction: {
								  Transaction Transaction = (Transaction) Entry2; 
								_Output.Write ("{1}\n{0}", _Indent, separator.ToString());
								_Output.Write ("				{{\"{1}\", {2}._Factory}}", _Indent, Transaction.Id, Transaction.Request);
							break; }
								}
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("		}};\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
						_Output.Write ("	public override Goedel.Protocol.JsonObject Dispatch(\n{0}", _Indent);
						_Output.Write ("			string token,\n{0}", _Indent);
						_Output.Write ("			Goedel.Protocol.JsonObject request,\n{0}", _Indent);
						_Output.Write ("			IJpcSession session) => token switch {{\n{0}", _Indent);
						foreach  (_Choice Entry2 in Protocol.Entries) {
							switch (Entry2._Tag ()) {
								case ProtoStructType.Transaction: {
								  Transaction Transaction = (Transaction) Entry2; 
								_Output.Write ("		\"{1}\" => {2}(request as {3}, session),\n{0}", _Indent, Transaction.Id, Transaction.Id, Transaction.Request);
							break; }
								}
							}
						_Output.Write ("		_ => throw new Goedel.Protocol.UnknownOperation(),\n{0}", _Indent);
						_Output.Write ("        }};\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						if (  (false.True()) ) {
							_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
							_Output.Write ("	public override Goedel.Protocol.JsonObject Dispatch(IJpcSession  session,  \n{0}", _Indent);
							_Output.Write ("							Goedel.Protocol.JsonReader jsonReader) {{\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("		jsonReader.StartObject ();\n{0}", _Indent);
							_Output.Write ("		string token = jsonReader.ReadToken ();\n{0}", _Indent);
							_Output.Write ("		JsonObject response = null;\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("		switch (token) {{\n{0}", _Indent);
							foreach  (_Choice Entry2 in Protocol.Entries) {
								switch (Entry2._Tag ()) {
									case ProtoStructType.Transaction: {
									  Transaction Transaction = (Transaction) Entry2; 
									_Output.Write ("			case \"{1}\" : {{\n{0}", _Indent, Transaction.Id);
									_Output.Write ("				var request = new {1}();\n{0}", _Indent, Transaction.Request);
									_Output.Write ("				request.Deserialize (jsonReader);\n{0}", _Indent);
									_Output.Write ("				response = {1} (request, session);\n{0}", _Indent, Transaction.Id);
									_Output.Write ("				break;\n{0}", _Indent);
									_Output.Write ("				}}\n{0}", _Indent);
								break; }
									}
								}
							_Output.Write ("			default : {{\n{0}", _Indent);
							_Output.Write ("				throw new Goedel.Protocol.UnknownOperation ();\n{0}", _Indent);
							_Output.Write ("				}}\n{0}", _Indent);
							_Output.Write ("			}}\n{0}", _Indent);
							_Output.Write ("		jsonReader.EndObject ();\n{0}", _Indent);
							_Output.Write ("		return response;\n{0}", _Indent);
							_Output.Write ("		}}\n{0}", _Indent);
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    /// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Return a client tapping the service API directly without serialization bound to\n{0}", _Indent);
						_Output.Write ("    /// the session <paramref name=\"jpcSession\"/>. This is intended for use in testing etc.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("    /// <param name=\"jpcSession\">Session to which requests are to be bound.</param>\n{0}", _Indent);
						_Output.Write ("    /// <returns>The direct client instance.</returns>\n{0}", _Indent);
						_Output.Write ("	public override Goedel.Protocol.JpcClientInterface GetDirect (IJpcSession jpcSession) =>\n{0}", _Indent);
						_Output.Write ("			new {1}Direct () {{\n{0}", _Indent, Service.Id);
						_Output.Write ("					JpcSession = jpcSession,\n{0}", _Indent);
						_Output.Write ("					Service = this\n{0}", _Indent);
						_Output.Write ("					}};\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						foreach  (_Choice Entry2 in Protocol.Entries) {
							switch (Entry2._Tag ()) {
								case ProtoStructType.Transaction: {
								  Transaction Transaction = (Transaction) Entry2; 
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("    /// <summary>\n{0}", _Indent);
								_Output.Write ("	/// Base method for implementing the transaction {1}.\n{0}", _Indent, Transaction.Id);
								_Output.Write ("    /// </summary>\n{0}", _Indent);
								_Output.Write ("    /// <param name=\"request\">The request object to send to the host.</param>\n{0}", _Indent);
								_Output.Write ("	/// <param name=\"session\">The request context.</param>\n{0}", _Indent);
								_Output.Write ("	/// <returns>The response object from the service</returns>\n{0}", _Indent);
								_Output.Write ("    public abstract {1} {2} (\n{0}", _Indent, Transaction.Response, Transaction.Id);
								_Output.Write ("            {1} request, IJpcSession session);\n{0}", _Indent, Transaction.Request);
							break; }
								}
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    }}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("/// <summary>\n{0}", _Indent);
						_Output.Write ("/// Client class for {1}.\n{0}", _Indent, Service.Id);
						_Output.Write ("/// </summary>		\n{0}", _Indent);
						_Output.Write ("public partial class {1}Client : Goedel.Protocol.JpcClientInterface {{\n{0}", _Indent, Service.Id);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	/// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Well Known service identifier.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("	public const string WellKnown = \"{1}\";\n{0}", _Indent, Service.WellKnown);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    /// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Well Known service identifier.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("	public override string GetWellKnown => WellKnown;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    /// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Well Known service identifier.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("	public const string Discovery = \"{1}\";\n{0}", _Indent, Service.Discovery);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("    /// <summary>\n{0}", _Indent);
						_Output.Write ("    /// Well Known service identifier.\n{0}", _Indent);
						_Output.Write ("    /// </summary>\n{0}", _Indent);
						_Output.Write ("	public override string GetDiscovery => Discovery;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						foreach  (_Choice Entry2 in Protocol.Entries) {
							switch (Entry2._Tag ()) {
								case ProtoStructType.Transaction: {
								  Transaction Transaction = (Transaction) Entry2; 
								_Output.Write ("    /// <summary>\n{0}", _Indent);
								_Output.Write ("	/// Implement the transaction {1}.\n{0}", _Indent, Transaction.Id);
								_Output.Write ("    /// </summary>		\n{0}", _Indent);
								_Output.Write ("    /// <param name=\"request\">The request object.</param>\n{0}", _Indent);
								_Output.Write ("	/// <returns>The response object</returns>\n{0}", _Indent);
								_Output.Write ("    public {1} {2} ({3} request) =>\n{0}", _Indent, Transaction.Response, Transaction.Id, Transaction.Request);
								_Output.Write ("			{1}Async (request).Sync();\n{0}", _Indent, Transaction.Id);
								
								//			JpcSession.Post("#{Transaction.Id}", request) as #{Transaction.Response};
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("    /// <summary>\n{0}", _Indent);
								_Output.Write ("	/// Implement the transaction {1} asynchronously.\n{0}", _Indent, Transaction.Id);
								_Output.Write ("    /// </summary>		\n{0}", _Indent);
								_Output.Write ("    /// <param name=\"request\">The request object.</param>\n{0}", _Indent);
								_Output.Write ("	/// <returns>The response object</returns>\n{0}", _Indent);
								_Output.Write ("    public virtual async Task<{1}> {2}Async ({3} request) =>\n{0}", _Indent, Transaction.Response, Transaction.Id, Transaction.Request);
								_Output.Write ("			await JpcSession.PostAsync(\"{1}\", request) as {2};\n{0}", _Indent, Transaction.Id, Transaction.Response);
								_Output.Write ("\n{0}", _Indent);
							break; }
								}
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("/// <summary>\n{0}", _Indent);
						_Output.Write ("/// Direct API class for {1}.\n{0}", _Indent, Service.Id);
						_Output.Write ("/// </summary>		\n{0}", _Indent);
						_Output.Write ("public partial class {1}Direct: {2}Client {{\n{0}", _Indent, Service.Id, Service.Id);
						_Output.Write (" 		\n{0}", _Indent);
						_Output.Write ("	/// <summary>\n{0}", _Indent);
						_Output.Write ("	/// Interface object to dispatch requests to.\n{0}", _Indent);
						_Output.Write ("	/// </summary>	\n{0}", _Indent);
						_Output.Write ("	public {1} Service {{get; set;}}\n{0}", _Indent, Service.Id);
						_Output.Write ("\n{0}", _Indent);
						foreach  (_Choice Entry2 in Protocol.Entries) {
							switch (Entry2._Tag ()) {
								case ProtoStructType.Transaction: {
								  Transaction Transaction = (Transaction) Entry2; 
								_Output.Write ("\n{0}", _Indent);
								_Output.Write ("    /// <summary>\n{0}", _Indent);
								_Output.Write ("	/// Implement the transaction\n{0}", _Indent);
								_Output.Write ("    /// </summary>		\n{0}", _Indent);
								_Output.Write ("    /// <param name=\"request\">The request object.</param>\n{0}", _Indent);
								_Output.Write ("	/// <returns>The response object</returns>\n{0}", _Indent);
								_Output.Write ("    public override Task<{1}> {2}Async ({3} request) =>\n{0}", _Indent, Transaction.Response, Transaction.Id, Transaction.Request);
								_Output.Write ("			Task.FromResult(Service.{1} (request, JpcSession));\n{0}", _Indent, Transaction.Id);
								_Output.Write ("\n{0}", _Indent);
							break; }
								}
							}
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("		}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
					break; }
						}
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	// Transaction Classes\n{0}", _Indent);
				foreach  (_Choice Entry in Protocol.Entries) {
					switch (Entry._Tag ()) {
						case ProtoStructType.Message: {
						  Message Message = (Message) Entry; 
						
						 MakeClass (Message);
						
						 var Inherits = HasInherits  (Message.Entries);
						
						 MakeSerializers (Message.Id, Message.ID, Message.Entries, Inherits);
						_Output.Write ("	}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						break; }
						case ProtoStructType.Structure: {
						  Structure Structure = (Structure) Entry; 
						
						 MakeClass (Structure);
						
						 var Inherits = HasInherits  (Structure.Entries);
						
						 MakeSerializers (Structure.Id, Structure.ID, Structure.Entries, Inherits);
						_Output.Write ("	}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
					break; }
						}
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
			break; }
				}
			}
		}
	
	/// <summary>	
	///  
	/// </summary>
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
	
	
	/// <summary>	
	///  
	/// </summary>
		 public bool IsMultiple  (List<_Choice> Entries) {
		 bool result = false;
		foreach  (_Choice Entry in Entries) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Multiple: { 
				
				 result = true;
				break; }
				case ProtoStructType.Enumerated: { 
				
				 result = true;
			break; }
				}
			}
		 return result;
		 }
	
	
	/// <summary>	
	///  
	/// </summary>
		 public bool IsEnumerated  (List<_Choice> Entries) {
		 bool result = false;
		foreach  (_Choice Entry in Entries) {
			switch (Entry._Tag ()) {
				case ProtoStructType.Enumerated: { 
				
				 result = true;
			break; }
				}
			}
		 return result;
		 }
	
	
	/// <summary>	
	///  
	/// </summary>
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
	
	
	/// <summary>	
	///  
	/// </summary>
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
	
	
	/// <summary>	
	///  
	/// </summary>
		 public void MakeClass  (ID<_Choice> Id, List<_Choice> Entries) {
		 var Inherits = HasInherits (Entries);
		 DescriptionListC (Entries, 1);
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
		 }
	
	
	/// <summary>	
	///  
	/// </summary>
		 public void MakeClass  (IStructure structure) {
		 ID<_Choice> Id = structure.IId;
		 List<_Choice> Entries = structure.IEntries;
		 bool Param = structure.IParameterized;
		_Output.Write ("\n{0}", _Indent);
		 var isAbstract = false;
		 var Inherits = HasInherits (Entries);
		 DescriptionListC (Entries, 1);
		if (  (IsAbstract (Entries)) ) {
			 isAbstract = true;
			_Output.Write ("abstract ", _Indent);
			}
		 var TTT = Param ? "<T>" : "";
		if (  (Inherits == null)  ) {
			_Output.Write ("public partial class {1}{2} : {3} {{\n{0}", _Indent, Id, TTT, CurrentPrefix);
			} else {
			_Output.Write ("public partial class {1}{2} : {3} {{\n{0}", _Indent, Id, TTT, Inherits);
			}
		DeclareMembers ((Entries));
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>Implement IBinding</summary> \n{0}", _Indent);
		_Output.Write ("	public override Binding _Binding => _binding;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<summary>Binding</summary> \n{0}", _Indent);
		_Output.Write ("	public static readonly new Binding<{1}> _binding = new (\n{0}", _Indent, Id);
		_Output.Write ("			new() {{\n{0}", _Indent);
		 DeclareProperties (Id, Entries);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        }}, __Tag,", _Indent);
		if (  isAbstract ) {
			_Output.Write ("null, null, null,", _Indent);
			} else {
			_Output.Write ("() => new {1}(), () => new List<{2}>(), () => new Dictionary<string,{3}>(),", _Indent, Id, Id, Id);
			}
		_Output.Write ("{1}", _Indent, (Inherits != null).If(Inherits + "._binding", "null"));
		if (  (structure.TypeTag) ) {
			_Output.Write (", TypeTag:\"{1}\" ", _Indent, structure.TypeElement);
			}
		_Output.Write (");\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>Dictionary describing the serializable properties.</summary> \n{0}", _Indent);
		_Output.Write ("    public readonly static new Dictionary<string, Property> _StaticProperties = _binding.Properties;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  (Inherits == null)  ) {
			_Output.Write ("	///<summary>Dictionary describing the serializable properties.</summary> \n{0}", _Indent);
			_Output.Write ("	public readonly static new Dictionary<string, Property> _StaticAllProperties = _StaticProperties;\n{0}", _Indent);
			} else {
			_Output.Write ("	///<summary>Dictionary describing the serializable properties.</summary> \n{0}", _Indent);
			_Output.Write ("	public readonly static new Dictionary<string, Property> _StaticAllProperties =\n{0}", _Indent);
			_Output.Write ("			Combine({1}._binding, _binding);\n{0}", _Indent, Inherits);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("	public override Dictionary<string, Property> _AllProperties => _StaticAllProperties;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("    public override Dictionary<string, Property> _Properties => _StaticProperties;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
		_Output.Write ("    public override Dictionary<string, Property> _ParentProperties => base._Properties;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  false ) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
			_Output.Write ("    public override Dictionary<string, MetaData> _MetaDataParent => base._MetaData;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    ///<inheritdoc/>\n{0}", _Indent);
			_Output.Write ("	public override Dictionary<string, MetaData> _MetaData => \n{0}", _Indent);
			_Output.Write ("		_metaData ??  new Dictionary<string, MetaData> () {{", _Indent);
			DeclareMetaMembers ((Entries));
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		}}.CacheValue(out _metaData);\n{0}", _Indent);
			_Output.Write ("	Dictionary<string, MetaData> _metaData;\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("    /// Tag identifying this class\n{0}", _Indent);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("	public override string _Tag => __Tag;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		_Output.Write ("    /// Tag identifying this class\n{0}", _Indent);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("	public new const string __Tag = \"{1}\";\n{0}", _Indent, structure.ID);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>\n{0}", _Indent);
		if (  (IsAbstract (Entries)) ) {
			_Output.Write ("    /// Factory method. Throws exception as this is an abstract class.\n{0}", _Indent);
			} else {
			_Output.Write ("    /// Factory method\n{0}", _Indent);
			}
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("    /// <returns>Object of this type</returns>\n{0}", _Indent);
		_Output.Write ("	public static new JsonObject _Factory () => ", _Indent);
		if (  (IsAbstract (Entries)) ) {
			_Output.Write ("throw new CannotCreateAbstract();\n{0}", _Indent);
			} else {
			_Output.Write ("new {1}();\n{0}", _Indent, Id);
			}
		_Output.Write ("\n{0}", _Indent);
		 }
	
	
	/// <summary>	
	///  DeclareProperties
	/// </summary>
		 public void DeclareProperties  (ID<_Choice> Id, List<_Choice> Entries) {
		 var separator = new Separator (",");
		foreach  (_Choice Entry in Entries) {
			 GetType (Entry, out var Token, out var Type, out var TType, out var Options, 
			    out var Nullable, out var Tag);
			 
			 var list = Entry.Multiple ? "List" : "" ;
			 var eType =  Entry.Multiple ? $"List<{Type}>" : Type ;
			switch (Entry._Tag ()) {
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("			{{ \"{1}\", new {2} (\"{3}\", typeof ({4}),\n{0}", _Indent, Tag, Entry.PropertyName, Tag, Param.Type.Label);
				_Output.Write ("					(IBinding data, object? value) => {{(data as {1}).{2} = value as {3};}}, (IBinding data) => (data as {4}).{5},\n{0}", _Indent, Id, Token, Param.TypeCSCons, Id, Token);
				_Output.Write ("					false, ()=>new  {1}(), ()=>new {2}()", _Indent, Param.TypeCSCons, Param.BaseType);
				if (  Entry.Dictionary ) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("					(IBinding data) => (data as {1}).{2}.GetEnumerable(),\n{0}", _Indent, Id, Token);
					_Output.Write ("					(object dictionary, object key, object value) =>\n{0}", _Indent);
					_Output.Write ("						 {{(dictionary as {1}).Add (key as string,value as {2});}}", _Indent, Param.TypeCSCons, Param.BaseType);
					}
				_Output.Write (")}}", _Indent);
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("			{{ \"{1}\", new {2} (\"{3}\", typeof ({4}), \n{0}", _Indent, Tag, Entry.PropertyName, Tag, Param.Type.Label);
				_Output.Write ("					(IBinding data, object? value) => {{(data as {1}).{2} = value as {3};}}, (IBinding data) => (data as {4}).{5},\n{0}", _Indent, Id, Token, Param.TypeCSCons, Id, Token);
				_Output.Write ("					true", _Indent);
				if (  Entry.Multiple ) {
					_Output.Write (", ()=>new {1}()\n{0}", _Indent, Param.TypeCSCons);
					}
				_Output.Write (")}} ", _Indent);
				
				 break; } default : {
				if (  (Token != null) ) {
					_Output.Write ("{1}\n{0}", _Indent, separator);
					_Output.Write ("			{{ \"{1}\", new {2} (\"{3}\", \n{0}", _Indent, Tag, Entry.PropertyName, Tag);
					_Output.Write ("					(IBinding data, {1}? value) => {{(data as {2}).{3} = value;}}, (IBinding data) => (data as {4}).{5} )}}", _Indent, Entry.TypeCS, Id, Token, Id, Token);
					}
			break; }
				}
			}
		 }
	
	//                    (IBinding data) => (data as SpeakToAs).Pronouns.GetEnumerable())} 
	
	/// <summary>	
	///  ParameterList
	/// </summary>
		 public void DeclareMetaMembers  (List<_Choice> Entries) {
		 var separator = new Separator (",");
		foreach  (_Choice Entry in Entries) {
			 GetType (Entry, out var Token, out var Type, out var TType, out var Options, 
			    out var Nullable, out var Tag);
			 
			 var list = Entry.Multiple ? "List" : "" ;
			 var eType =  Entry.Multiple ? $"List<{Type}>" : Type ;
			switch (Entry._Tag ()) {
				case ProtoStructType.Struct: {
				  Struct Param = (Struct) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("			{{ \"{1}\", new MetaData{2}Struct(\n{0}", _Indent, Tag, list);
				_Output.Write ("				delegate (object _a) {{  {1} = _a as {2}; }},\n{0}", _Indent, Token, eType);
				_Output.Write ("				() => {1},\n{0}", _Indent, Token);
				_Output.Write ("				\"{1}\" )}} ", _Indent, Type);
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("			{{ \"{1}\", new MetaData{2}Struct(\n{0}", _Indent, Tag, list);
				_Output.Write ("				delegate (object _a) {{  {1} = _a as {2}; }},\n{0}", _Indent, Token, eType);
				_Output.Write ("				() => {1},\n{0}", _Indent, Token);
				_Output.Write ("				\"{1}\", true)}}", _Indent, Type);
				
				 break; } default : {
				if (  (Token != null) ) {
					_Output.Write ("{1}\n{0}", _Indent, separator);
					_Output.Write ("			{{ \"{1}\", new MetaData{2}{3}(\n{0}", _Indent, Tag, list, TType);
					_Output.Write ("				delegate ({1} _a) {{  {2} = _a; }},\n{0}", _Indent, eType, Token);
					_Output.Write ("				() => {1}) }} ", _Indent, Token);
					}
			break; }
				}
			}
		 }
	
	
	/// <summary>	
	///  ParameterList
	/// </summary>
		 public void DeclareMembers  (List<_Choice> Entries) {
		foreach  (_Choice Entry in Entries) {
			 GetType (Entry, out var Token, out var Type, out var TType, out var Options, 
			    out var Nullable, out var Tag);
			if (  (Token != null) ) {
				 bool Enumerated = IsEnumerated (Options);
				 bool Multiple = IsMultiple (Options);
				if (  Enumerated ) {
					_Output.Write ("{1}\n{0}", _Indent, CommentSummary(8,Entry.Description));
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual IEnumerable<{1}>				{2}  {{get; set;}}\n{0}", _Indent, Type, Token);
					} else if (  Multiple) {
					_Output.Write ("{1}\n{0}", _Indent, CommentSummary(8,Entry.Description));
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual {1}?					{2}  {{get; set;}}\n{0}", _Indent, Entry.TypeCS, Token);
					} else {
					_Output.Write ("{1}\n{0}", _Indent, CommentSummary(8,Entry.Description));
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual {1}?					{2}  {{get; set;}}\n{0}", _Indent, Entry.TypeCS, Token);
					_Output.Write ("\n{0}", _Indent);
					}
				}
			}
		 }
	
	
	/// <summary>	
	///  MakeSerializers
	/// </summary>
		 public void MakeSerializers  (ID<_Choice> Id, string STag, List<_Choice> Entries, string Inherits) {
		if (  false.True() ) {
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Serialize this object to the specified output stream.\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"writer\">Output stream</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"wrap\">If true, output is wrapped with object\n{0}", _Indent);
			_Output.Write ("    /// start and end sequences '{{ ... }}'.</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"first\">If true, item is the first entry in a list.</param>\n{0}", _Indent);
			_Output.Write ("	public override void Serialize (Writer writer, bool wrap, ref bool first) =>\n{0}", _Indent);
			_Output.Write ("		SerializeX (writer, wrap, ref first);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Serialize this object to the specified output stream.\n{0}", _Indent);
			_Output.Write ("    /// Unlike the Serlialize() method, this method is not inherited from the\n{0}", _Indent);
			_Output.Write ("    /// parent class allowing a specific version of the method to be called.\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"_writer\">Output stream</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"_wrap\">If true, output is wrapped with object\n{0}", _Indent);
			_Output.Write ("    /// start and end sequences '{{ ... }}'.</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"_first\">If true, item is the first entry in a list.</param>\n{0}", _Indent);
			_Output.Write ("	public new void SerializeX (Writer _writer, bool _wrap, ref bool _first) {{\n{0}", _Indent);
			_Output.Write ("		PreEncode();\n{0}", _Indent);
			_Output.Write ("		if (_wrap) {{\n{0}", _Indent);
			_Output.Write ("			_writer.WriteObjectStart ();\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			if (  (Inherits != null) ) {
				_Output.Write ("		(({1})this).SerializeX(_writer, false, ref _first);\n{0}", _Indent, Inherits);
				}
			foreach  (_Choice Entry in Entries) {
				 GetType (Entry, out var Token, out var Type, out var TType, 
				      out var Options, out var Nullable, out var Tag);
				if (  (Token != null) ) {
					 bool Multiple = IsMultiple (Options);
					if (  Multiple ) {
						_Output.Write ("		if ({1} != null) {{\n{0}", _Indent, Token);
						_Output.Write ("			_writer.WriteObjectSeparator (ref _first);\n{0}", _Indent);
						_Output.Write ("			_writer.WriteToken (\"{1}\", 1);\n{0}", _Indent, Tag);
						_Output.Write ("			_writer.WriteArrayStart ();\n{0}", _Indent);
						_Output.Write ("			bool _firstarray = true;\n{0}", _Indent);
						_Output.Write ("			foreach (var _index in {1}) {{\n{0}", _Indent, Token);
						_Output.Write ("				_writer.WriteArraySeparator (ref _firstarray);\n{0}", _Indent);
						 MakeSerializeArrayEntry (Entry, "_index");
						_Output.Write ("				}}\n{0}", _Indent);
						_Output.Write ("			_writer.WriteArrayEnd ();\n{0}", _Indent);
						_Output.Write ("			}}\n{0}", _Indent);
						} else {
						if (  Nullable ) {
							_Output.Write ("		if ({1} != null) {{\n{0}", _Indent, Token);
							} else {
							_Output.Write ("		if ({1} != null) {{\n{0}", _Indent, Token);
							//		if (__#{Token}){
							}
						_Output.Write ("			_writer.WriteObjectSeparator (ref _first);\n{0}", _Indent);
						_Output.Write ("			_writer.WriteToken (\"{1}\", 1);\n{0}", _Indent, Tag);
						 MakeSerializeEntry (Entry, Token.ToString());
						_Output.Write ("			}}\n{0}", _Indent);
						}
					if (  Multiple ) {
						_Output.Write ("\n{0}", _Indent);
						}
					}
				}
			_Output.Write ("		if (_wrap) {{\n{0}", _Indent);
			_Output.Write ("			_writer.WriteObjectEnd ();\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    /// <summary>\n{0}", _Indent);
		_Output.Write ("    /// Deserialize a tagged stream\n{0}", _Indent);
		_Output.Write ("    /// </summary>\n{0}", _Indent);
		_Output.Write ("    /// <param name=\"jsonReader\">The input stream</param>\n{0}", _Indent);
		_Output.Write ("	/// <param name=\"tagged\">If true, the input is wrapped in a tag specifying the type</param>\n{0}", _Indent);
		_Output.Write ("    /// <returns>The created object.</returns>		\n{0}", _Indent);
		_Output.Write ("    public static new {1} FromJson (JsonReader jsonReader, bool tagged=true) {{\n{0}", _Indent, Id);
		_Output.Write ("		if (jsonReader == null) {{\n{0}", _Indent);
		_Output.Write ("			return null;\n{0}", _Indent);
		_Output.Write ("			}}\n{0}", _Indent);
		_Output.Write ("		if (tagged) {{\n{0}", _Indent);
		_Output.Write ("			var Out = jsonReader.ReadTaggedObject (_TagDictionary);\n{0}", _Indent);
		_Output.Write ("			return Out as {1};\n{0}", _Indent, Id);
		_Output.Write ("			}}\n{0}", _Indent);
		if (  (!IsAbstract (Entries)) ) {
			_Output.Write ("		var Result = new {1} ();\n{0}", _Indent, Id);
			_Output.Write ("		Result.Deserialize (jsonReader);\n{0}", _Indent);
			_Output.Write ("		Result.PostDecode();\n{0}", _Indent);
			_Output.Write ("		return Result;\n{0}", _Indent);
			} else {
			_Output.Write ("		throw new CannotCreateAbstract();\n{0}", _Indent);
			}
		_Output.Write ("		}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  false.True() ) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Having read a tag, process the corresponding value data.\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"jsonReader\">The input stream</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"tag\">The tag</param>\n{0}", _Indent);
			_Output.Write ("	public override void DeserializeToken (JsonReader jsonReader, string tag) {{\n{0}", _Indent);
			_Output.Write ("			\n{0}", _Indent);
			_Output.Write ("		switch (tag) {{\n{0}", _Indent);
			foreach  (_Choice entry in Entries) {
				 GetType(entry, out var token, out var type, out var ttype, 
				    out var options, out var nullable, out var tag);
				if (  (token != null) ) {
					 bool Multiple = IsMultiple (options);
					_Output.Write ("			case \"{1}\" : {{\n{0}", _Indent, tag);
					if (  Multiple ) {
						_Output.Write ("				// Have a sequence of values\n{0}", _Indent);
						_Output.Write ("				bool _Going = jsonReader.StartArray ();\n{0}", _Indent);
						_Output.Write ("				{1} = new List <{2}> ();\n{0}", _Indent, token, type);
						_Output.Write ("				while (_Going) {{\n{0}", _Indent);
						if (  entry._Tag () == ProtoStructType.Struct ) {
							_Output.Write ("					// an untagged structure.\n{0}", _Indent);
							_Output.Write ("					var _Item = new  {1} ();\n{0}", _Indent, type);
							_Output.Write ("					_Item.Deserialize (jsonReader);\n{0}", _Indent);
							_Output.Write ("					// var _Item = new {1} (jsonReader);\n{0}", _Indent, type);
							} else if (  entry._Tag () == ProtoStructType.TStruct) {
							_Output.Write ("					var _Item = {1}.FromJson (jsonReader, true); // a tagged structure\n{0}", _Indent, type);
							} else {
							_Output.Write ("					{1} _Item = jsonReader.Read{2} ();\n{0}", _Indent, type, ttype);
							}
						_Output.Write ("					{1}.Add (_Item);\n{0}", _Indent, token);
						_Output.Write ("					_Going = jsonReader.NextArray ();\n{0}", _Indent);
						_Output.Write ("					}}\n{0}", _Indent);
						} else {
						if (  entry._Tag () == ProtoStructType.Struct ) {
							_Output.Write ("				// An untagged structure\n{0}", _Indent);
							_Output.Write ("				{1} = new {2} ();\n{0}", _Indent, token, type);
							_Output.Write ("				{1}.Deserialize (jsonReader);\n{0}", _Indent, token);
							_Output.Write (" \n{0}", _Indent);
							} else if (  entry._Tag () == ProtoStructType.TStruct) {
							_Output.Write ("				{1} = {2}.FromJson (jsonReader, true) ;  // A tagged structure\n{0}", _Indent, token, type);
							} else {
							_Output.Write ("				{1} = jsonReader.Read{2} ();\n{0}", _Indent, token, ttype);
							}
						}
					_Output.Write ("				break;\n{0}", _Indent);
					_Output.Write ("				}}\n{0}", _Indent);
					}
				}
			_Output.Write ("			default : {{\n{0}", _Indent);
			if (  (Inherits != null) ) {
				_Output.Write ("				base.DeserializeToken(jsonReader, tag);\n{0}", _Indent);
				}
			_Output.Write ("				break;\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("		// check up that all the required elements are present\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		 }
	
	
	/// <summary>	
	/// MakeSerializeEntry
	/// </summary>
	/// <param name="Entry"></param>
	/// <param name="Tag"></param>
	public void MakeSerializeEntry (_Choice Entry, string Tag) {
		switch (Entry._Tag ()) {
			case ProtoStructType.Boolean: { 
			_Output.Write ("				_writer.WriteBoolean ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Integer: { 
			if (  Entry.LengthBits >32 ) {
				_Output.Write ("				_writer.WriteInteger64 ({1});\n{0}", _Indent, Tag);
				} else {
				_Output.Write ("				_writer.WriteInteger32 ({1});\n{0}", _Indent, Tag);
				}
			break; }
			case ProtoStructType.Binary: { 
			_Output.Write ("				_writer.WriteBinary ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Struct: { 
			_Output.Write ("				{1}.Serialize (_writer, false);\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.TStruct: { 
			_Output.Write ("				// expand this to a tagged structure\n{0}", _Indent);
			_Output.Write ("				//{1}.Serialize (_writer, false);\n{0}", _Indent, Tag);
			_Output.Write ("				{{\n{0}", _Indent);
			_Output.Write ("					_writer.WriteObjectStart();\n{0}", _Indent);
			_Output.Write ("					_writer.WriteToken({1}._Tag, 1);\n{0}", _Indent, Tag);
			_Output.Write ("					bool firstinner = true;\n{0}", _Indent);
			_Output.Write ("					{1}.Serialize (_writer, true, ref firstinner);\n{0}", _Indent, Tag);
			_Output.Write ("					_writer.WriteObjectEnd();\n{0}", _Indent);
			_Output.Write ("					}}\n{0}", _Indent);
			break; }
			case ProtoStructType.Label: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Name: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.String: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.URI: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.DateTime: { 
			_Output.Write ("				_writer.WriteDateTime ({1});\n{0}", _Indent, Tag);
			
										break; }
			
			 default : {
			_Output.Write ("\n{0}", _Indent);
		break; }
			}
		}
	
	/// <summary>	
	/// MakeSerializeArrayEntry
	/// </summary>
	/// <param name="Entry"></param>
	/// <param name="Tag"></param>
	public void MakeSerializeArrayEntry (_Choice Entry, string Tag) {
		switch (Entry._Tag ()) {
			case ProtoStructType.Boolean: { 
			_Output.Write ("				_writer.WriteBoolean ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Integer: { 
			_Output.Write ("				_writer.WriteInteger32 ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Binary: { 
			_Output.Write ("				_writer.WriteBinary ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Struct: { 
			_Output.Write ("				// This is an untagged structure. Cannot inherit.\n{0}", _Indent);
			_Output.Write ("                //_writer.WriteObjectStart();\n{0}", _Indent);
			_Output.Write ("                //_writer.WriteToken({1}._Tag, 1);\n{0}", _Indent, Tag);
			_Output.Write ("				bool firstinner = true;\n{0}", _Indent);
			_Output.Write ("				{1}.Serialize (_writer, true, ref firstinner);\n{0}", _Indent, Tag);
			_Output.Write ("                //_writer.WriteObjectEnd();\n{0}", _Indent);
			break; }
			case ProtoStructType.TStruct: { 
			_Output.Write ("                _writer.WriteObjectStart();\n{0}", _Indent);
			_Output.Write ("                _writer.WriteToken({1}._Tag, 1);\n{0}", _Indent, Tag);
			_Output.Write ("				bool firstinner = true;\n{0}", _Indent);
			_Output.Write ("				{1}.Serialize (_writer, true, ref firstinner);\n{0}", _Indent, Tag);
			_Output.Write ("                _writer.WriteObjectEnd();\n{0}", _Indent);
			break; }
			case ProtoStructType.Label: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.Name: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.String: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.URI: { 
			_Output.Write ("				_writer.WriteString ({1});\n{0}", _Indent, Tag);
			break; }
			case ProtoStructType.DateTime: { 
			_Output.Write ("				_writer.WriteDateTime ({1});\n{0}", _Indent, Tag);
			
										break; }
			
			 default : {
			_Output.Write ("\n{0}", _Indent);
		break; }
			}
		}
	
	/// <summary>	
	/// 
	/// </summary>
		 void GetType (_Choice Entry, out TOKEN<_Choice> Token, out string Type, out string TType, 
						out List<_Choice> Options, out bool Nullable, out string Tag) {
		 Nullable = true;
		switch (Entry._Tag ()) {
			case ProtoStructType.Boolean: {
			  Boolean Param = (Boolean) Entry; 
			
			 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "bool?";  Nullable = false;TType = "Boolean";
			break; }
			case ProtoStructType.Integer: {
			  Integer Param = (Integer) Entry; 
			
			 Tag = Param.ID ; Token = Param.Id; Options = Param.Options;  Nullable = false;
			
			 Type = Param.LengthBits>32 ? "long?": "int?";
			
			 TType = Param.LengthBits>32 ? "Integer64": "Integer32";
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
			
			 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = "DateTime?"; Nullable = true; TType = "DateTime";
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
	
	
	/// <summary>	
	/// 
	/// </summary>
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
	
	
	/// <summary>	
	///  DeserializeCase
	/// </summary>
		 void DeserializeCase (ID<_Choice> Id, string Tag) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			case \"{1}\" : {{\n{0}", _Indent, Id);
		if (  Id.Object.IsAbstract ) {
			_Output.Write ("				Out = null;\n{0}", _Indent);
			_Output.Write ("				throw new Exception (\"Can't create abstract type\");\n{0}", _Indent);
			} else {
			_Output.Write ("				// Out = {1}.Factory ();\n{0}", _Indent, Id);
			_Output.Write ("				Out = new {1} ();\n{0}", _Indent, Id);
			_Output.Write ("				Out.Deserialize (jsonReader);\n{0}", _Indent);
			_Output.Write ("				break;\n{0}", _Indent);
			}
		_Output.Write ("				}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
			}
	
	
	/// <summary>	
	///  MapInheritors
	/// </summary>
		 void MapInheritors (ID<_Choice> Id, string Tag) {
		_Output.Write ("			case \"{1}\" : {{\n{0}", _Indent, Id);
		if (  Id.Object.IsAbstract ) {
			_Output.Write ("				Out = null;\n{0}", _Indent);
			_Output.Write ("				throw new Exception (\"Can't create abstract type\");\n{0}", _Indent);
			} else {
			_Output.Write ("				Out = new {1} (); \n{0}", _Indent, Id);
			_Output.Write ("				// Out = {1}.Factory ();\n{0}", _Indent, Id);
			_Output.Write ("				Out.Deserialize (jsonReader);\n{0}", _Indent);
			_Output.Write ("				break;\n{0}", _Indent);
			}
		_Output.Write ("				}}\n{0}", _Indent);
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
	
	
	/// <summary>	
	///  DescriptionListC
	/// </summary>
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
