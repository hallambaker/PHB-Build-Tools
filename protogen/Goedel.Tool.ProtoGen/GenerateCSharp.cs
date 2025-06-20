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
				_Output.Write ("	///<summary>Variable used to force static initialization</summary> \n{0}", _Indent);
				_Output.Write ("	public static bool _Initialized => true;\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	static {1}() {{\n{0}", _Indent, CurrentPrefix);
				_Output.Write ("		_Initialize();\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("    internal static void _Initialize() {{\n{0}", _Indent);
				_Output.Write ("		AddDictionary(ref _bindingDictionary);\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
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
						_Output.Write ("	public override Dictionary<string, Type>  GetTagDictionary => _TagDictionary;\n{0}", _Indent);
						_Output.Write ("		\n{0}", _Indent);
						_Output.Write ("	static Dictionary<string, Type> _TagDictionary = new () {{", _Indent);
						
						 var separator = new Separator (",");
						foreach  (_Choice Entry2 in Protocol.Entries) {
							switch (Entry2._Tag ()) {
								case ProtoStructType.Transaction: {
								  Transaction Transaction = (Transaction) Entry2; 
								_Output.Write ("{1}\n{0}", _Indent, separator.ToString());
								_Output.Write ("				{{\"{1}\", typeof({2})}}", _Indent, Transaction.Id, Transaction.Request);
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
						
						// MakeSerializers (Message.Id, Message.ID, Message.Entries, Inherits);
						_Output.Write ("	}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						break; }
						case ProtoStructType.Structure: {
						  Structure Structure = (Structure) Entry; 
						
						 MakeClass (Structure);
						
						 var Inherits = HasInherits  (Structure.Entries);
						
						// MakeSerializers (Structure.Id, Structure.ID, Structure.Entries, Inherits);
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
		_Output.Write ("    ///<summary>Implement IBinding</summary> \n{0}", _Indent);
		_Output.Write ("	public override Property[] _Properties => _properties;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<summary>Binding</summary> \n{0}", _Indent);
		_Output.Write ("	static readonly Property[] _properties = [", _Indent);
		 DeclareProperties (Id, Entries);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		];\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    ///<summary>Implement IBinding</summary> \n{0}", _Indent);
		_Output.Write ("	public override Binding _Binding => _binding;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	///<summary>Binding</summary> \n{0}", _Indent);
		_Output.Write ("	public static readonly new Binding<{1}> _binding = new (\n{0}", _Indent, Id);
		_Output.Write ("			new() {{", _Indent);
		 DeclarePropertyEntries (Id, Entries);
		_Output.Write ("}}, __Tag,\n{0}", _Indent);
		_Output.Write ("		", _Indent);
		if (  isAbstract ) {
			_Output.Write ("null, () => [], () => [], ", _Indent);
			} else {
			_Output.Write ("() => new {1}(), () => [], () => [], ", _Indent, Id);
			}
		_Output.Write ("{1}", _Indent, (Inherits != null).If(Inherits + "._binding", "null"));
		if (  (structure.TypeTag) ) {
			_Output.Write (", \n{0}", _Indent);
			_Output.Write ("		TypeTag:\"{1}\" ", _Indent, structure.TypeElement);
			}
		_Output.Write (", Generic: {1});\n{0}", _Indent, structure.Generic.ToString().ToLower());
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
	///  DeclarePropertyEntries
	/// </summary>
		 public void DeclarePropertyEntries  (ID<_Choice> Id, List<_Choice> Entries) {
		 var separator = new Separator (",");
		 var i =0;
		foreach  (_Choice Entry in Entries) {
			 GetType (Entry, out var Token, out var Type, out var TType, out var Options, 
			    out var Nullable, out var Tag);
			if (  (Token != null) ) {
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("			{{ \"{1}\", _properties [{2}]}}", _Indent, Tag, i++);
				}
			}
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
				_Output.Write ("		new {1} (\"{2}\", typeof ({3}),\n{0}", _Indent, Entry.PropertyName, Tag, Param.Type.Label);
				_Output.Write ("					(IBinding data, object? value) => {{(data as {1}).{2} = value as {3};}}, \n{0}", _Indent, Id, Token, Param.TypeCSCons);
				_Output.Write ("					(IBinding data) => (data as {1}).{2},\n{0}", _Indent, Id, Token);
				_Output.Write ("					false, ()=>new  {1}(), ()=>new {2}()", _Indent, Param.TypeCSCons, Param.BaseType);
				if (  Entry.Dictionary ) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("					(IBinding data) => (data as {1}).{2}.GetEnumerable(),\n{0}", _Indent, Id, Token);
					_Output.Write ("					(object dictionary, object key, object value) =>\n{0}", _Indent);
					_Output.Write ("						 {{(dictionary as {1}).Add (key as string,value as {2});}}", _Indent, Param.TypeCSCons, Param.BaseType);
					}
				_Output.Write (")", _Indent);
				break; }
				case ProtoStructType.TStruct: {
				  TStruct Param = (TStruct) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("		new {1} (\"{2}\", typeof ({3}), \n{0}", _Indent, Entry.PropertyName, Tag, Param.Type.Label);
				_Output.Write ("					(IBinding data, object? value) => {{(data as {1}).{2} = value as {3};}}, \n{0}", _Indent, Id, Token, Param.TypeCSCons);
				_Output.Write ("					(IBinding data) => (data as {1}).{2},\n{0}", _Indent, Id, Token);
				_Output.Write ("					true", _Indent);
				if (  Entry.Multiple ) {
					_Output.Write (", ()=>new {1}()\n{0}", _Indent, Param.TypeCSCons);
					}
				_Output.Write (") ", _Indent);
				break; }
				case ProtoStructType.GStruct: {
				  GStruct Param = (GStruct) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, separator);
				_Output.Write ("		new {1} (\"{2}\", /*typeof ({3}<>),*/typeof ({4}),\n{0}", _Indent, Entry.PropertyName, Tag, Param.Type.Label, Param.GType.Label);
				_Output.Write ("					(IBinding data, object? value) => {{(data as {1}).{2} = value as {3};}},\n{0}", _Indent, Id, Param.MainFieldName, Param.TypeCSCons);
				_Output.Write ("					(IBinding data) => (data as {1}).{2},\n{0}", _Indent, Id, Param.MainFieldName);
				_Output.Write ("					/*(IBinding data, object? value) => {{(data as {1}).{2} = value as {3};}},\n{0}", _Indent, Id, Param.SubFieldName, Param.SubTypeCS);
				_Output.Write ("					(IBinding data) => (data as {1}).{2},*/\n{0}", _Indent, Id, Param.SubFieldName);
				_Output.Write ("					()=>new  {1}(), ()=>new {2}()", _Indent, Param.TypeCSCons, Param.BaseType);
				if (  Entry.Multiple ) {
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("					(object list,object item)=>(list as {1}).Add (item as {2})\n{0}", _Indent, Param.TypeCSCons, Param.BaseType);
					}
				_Output.Write (")", _Indent);
				
				 break; } default : {
				if (  (Token != null) ) {
					_Output.Write ("{1}\n{0}", _Indent, separator);
					_Output.Write ("		new {1} (\"{2}\", \n{0}", _Indent, Entry.PropertyName, Tag);
					_Output.Write ("					(IBinding data, {1}? value) => {{(data as {2}).{3} = value;}}, \n{0}", _Indent, Entry.TypeCS, Id, Token);
					_Output.Write ("					(IBinding data) => (data as {1}).{2} )", _Indent, Id, Token);
					}
			break; }
				}
			}
		 }
	
	
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
				if (  (Entry is GStruct generic) ) {
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual {1}?					{2}  {{get; set;}} \n{0}", _Indent, generic.TypeCS, generic.MainFieldName);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	/// <summary>\n{0}", _Indent);
					_Output.Write ("	/// Wrapped property\n{0}", _Indent);
					_Output.Write ("    /// </summary>\n{0}", _Indent);
					_Output.Write ("	public virtual {1}?				{2}  => {3}.Decode();\n{0}", _Indent, generic.SubTypeCS, generic.SubFieldName, generic.MainFieldName);
					} else if (  Enumerated) {
					_Output.Write ("{1}\n{0}", _Indent, CommentSummary(4,Entry.Description));
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual IEnumerable<{1}>				{2}  {{get; set;}} \n{0}", _Indent, Type, Token);
					} else if (  Multiple) {
					_Output.Write ("{1}\n{0}", _Indent, CommentSummary(4,Entry.Description));
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual {1}?					{2}  {{get; set;}}\n{0}", _Indent, Entry.TypeCS, Token);
					} else {
					_Output.Write ("{1}\n{0}", _Indent, CommentSummary(4,Entry.Description));
					_Output.Write ("	[JsonPropertyName(\"{1}\")]\n{0}", _Indent, Entry.ID);
					_Output.Write ("	public virtual {1}?					{2}  {{get; set;}} //\n{0}", _Indent, Entry.TypeCS, Token);
					_Output.Write ("\n{0}", _Indent);
					}
				}
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
			case ProtoStructType.Float: {
			  Float Param = (Float) Entry; 
			
			 Tag = Param.ID ; Token = Param.Id; Options = Param.Options;  Nullable = false;
			
			 Type = Param.LengthBits>32 ? "double?": "float?";
			
			 TType = Param.LengthBits>32 ? "Real64": "Real32";
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
			break; }
			case ProtoStructType.GStruct: {
			  GStruct Param = (GStruct) Entry; 
			
			 Tag = Param.ID ; Token = Param.Id; Options = Param.Options; Type = Param.Type.ToString(); TType = Type;
			
			 break; } default : {
			
			 Tag = null ; Token = null; Options = null; Type = null; TType = null;
		break; }
			}
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
