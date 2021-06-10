// Script Syntax Version:  1.0

//  © 2015-2019 by Phill Hallam-Baker
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
namespace Goedel.Tool.Yaschema {
	public partial class Generate : global::Goedel.Registry.Script {

		

		//
		// GenerateCS
		//
		public void GenerateCS (YaschemaStruct Yaschema) {
			 Registry.Boilerplate.License(_Output, "//  ", "MITLicense");
			 Registry.Boilerplate.Header(_Output, "//  ", DateTime.Now);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.IO;\n{0}", _Indent);
			_Output.Write ("using System.Net;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using Goedel.Cryptography;\n{0}", _Indent);
			_Output.Write ("using Goedel.Cryptography.Dare;\n{0}", _Indent);
			_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
			_Output.Write ("using Goedel.Protocol;\n{0}", _Indent);
			_Output.Write ("using System.Threading.Tasks;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("namespace {1} {{\n{0}", _Indent, Yaschema.NameSpaceName);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Client connection class. Tracks the state of a client connection.\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    public partial class  ConnectionInitiator : RudConnection {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach (var _client in Yaschema.Top) {  if (_client.GetType() == typeof (Client)) { var client = (Client) _client; 
				_Output.Write ("\n{0}", _Indent);
				foreach  (var packet in client.Entries) {
					GenersateSerializer (packet);
					}
					}
	}
			_Output.Write ("\n{0}", _Indent);
			foreach (var _host in Yaschema.Top) {  if (_host.GetType() == typeof (Host)) { var host = (Host) _host; 
				foreach  (var packet in host.Entries) {
					 GenerateParser (packet, false);
					}
					}
	}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public partial class ConnectionResponder : RudConnection {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach (var _host in Yaschema.Top) {  if (_host.GetType() == typeof (Host)) { var host = (Host) _host; 
				foreach  (var packet in host.Entries) {
					GenersateSerializer (packet);
					}
					}
	}
			_Output.Write ("\n{0}", _Indent);
			foreach (var _client in Yaschema.Top) {  if (_client.GetType() == typeof (Client)) { var client = (Client) _client; 
				foreach  (var packet in client.Entries) {
					if (  (packet.Completer) ) {
						_Output.Write ("        // Perform initial parse as static listener, only complete decrypt in session context\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("        /// <summary>\n{0}", _Indent);
						_Output.Write ("        /// Perform key exchanges and complete parsing of the packet\n{0}", _Indent);
						_Output.Write ("        /// </summary>\n{0}", _Indent);
						_Output.Write ("        public void Complete{1} ({2} result) {{\n{0}", _Indent, packet.Id, packet.ClassName);
						_Output.Write ("            var outerReader = result.Reader;\n{0}", _Indent);
						ParseMezzanine (packet);
						_Output.Write ("            }}\n{0}", _Indent);
						} else {
						 GenerateParser (packet, false);
						}
					}
					}
	}
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public partial class Listener {{\n{0}", _Indent);
			foreach (var _client in Yaschema.Top) {  if (_client.GetType() == typeof (Client)) { var client = (Client) _client; 
				foreach  (var packet in client.Entries) {
					if (  (packet.IsInitial | packet.Completer) ) {
						 GenerateParser (packet, true);
						}
					}
					}
	}
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var packet in Yaschema.Packets) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("    /// <summary>\n{0}", _Indent);
				_Output.Write ("    /// Parsed {1} packet\n{0}", _Indent, packet.Id);
				_Output.Write ("    /// </summary>   \n{0}", _Indent);
				_Output.Write ("    public partial class {1} : Packet {{\n{0}", _Indent, packet.ClassName);
				GeneratePacket (packet);
				_Output.Write ("        }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("	}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// GenersateSerializer
		//
		public void GenersateSerializer (Packet packet) {
			 var plaintext = packet.Plaintext;
			 var mezzanine = packet.Mezzanine;
			_Output.Write ("        // Serialize {1} packet {2}\n{0}", _Indent, packet.PacketType, packet.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Create a serialised packet of type {1} packet.\n{0}", _Indent, packet.Id);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"sourceId\">The source identifier.</param>\n{0}", _Indent);
			if (  (!packet.IsInitial) ) {
				_Output.Write ("        /// <param name=\"destinationId\">The destination identifier.</param>\n{0}", _Indent);
				}
			_Output.Write ("        /// <param name=\"payload\">The payload data.</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"plaintextExtensionsIn\">Additional extensions to be presented \n{0}", _Indent);
			_Output.Write ("        /// in the plaintext segment.</param>\n{0}", _Indent);
			if (  (packet.HasMezzanine) ) {
				_Output.Write ("        /// <param name=\"mezanineExtensionsIn\">Additional extensions to be presented\n{0}", _Indent);
				_Output.Write ("        /// in the mezzanine segment.</param>\n{0}", _Indent);
				}
			if (  (packet.HasEncrypted) ) {
				_Output.Write ("        /// <param name=\"ciphertextExtensions\">Additional extensions to be presented \n{0}", _Indent);
				_Output.Write ("        /// in the encrypted segment.</param>\n{0}", _Indent);
				}
			_Output.Write ("        /// <returns>The serialized data.</returns>\n{0}", _Indent);
			_Output.Write ("        public byte[] Serialize{1} (\n{0}", _Indent, packet.Id);
			_Output.Write ("                byte[] sourceId,\n{0}", _Indent);
			if (  (!packet.IsInitial) ) {
				_Output.Write ("                byte[] destinationId,\n{0}", _Indent);
				}
			_Output.Write ("                byte[] payload = null,\n{0}", _Indent);
			_Output.Write ("                List<PacketExtension> plaintextExtensionsIn = null", _Indent);
			if (  (packet.HasMezzanine) ) {
				_Output.Write (",\n{0}", _Indent);
				_Output.Write ("                List<PacketExtension> mezanineExtensionsIn = null", _Indent);
				}
			if (  (packet.HasEncrypted) ) {
				_Output.Write (",\n{0}", _Indent);
				_Output.Write ("                List<PacketExtension> ciphertextExtensions = null", _Indent);
				}
			//                byte[] buffer=null,
			//                int position=0
			_Output.Write (") {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // The plaintext part\n{0}", _Indent);
			_Output.Write ("            var outerWriter = PacketWriterFactory();\n{0}", _Indent);
			if (  (packet.IsInitial) ) {
				_Output.Write ("            byte[] destinationId = null;\n{0}", _Indent);
				}
			_Output.Write ("            outerWriter.WriteStreamId(destinationId);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (packet.IsClient) ) {
				_Output.Write ("            outerWriter.Write(InitiatorMessageType.{1});\n{0}", _Indent, packet.Id);
				} else {
				_Output.Write ("            outerWriter.Write(ResponderMessageType.{1});\n{0}", _Indent, packet.Id);
				}
			if (  (plaintext == null) ) {
				_Output.Write ("            // There are no plaintext fields.\n{0}", _Indent);
				_Output.Write ("            outerWriter.WriteExtensions(plaintextExtensionsIn);\n{0}", _Indent);
				} else {
				_Output.Write ("            // Plaintext fields..\n{0}", _Indent);
				if (  (plaintext.AddExtensions) ) {
					_Output.Write ("            var plaintextExtensions = new List<PacketExtension>();\n{0}", _Indent);
					}
				if (  (plaintext.Ephemeral) ) {
					_Output.Write ("            ClientKeyExchange (out var ephemeral, out var hostKeyId);\n{0}", _Indent);
					_Output.Write ("            outerWriter.Write (hostKeyId);\n{0}", _Indent);
					_Output.Write ("            outerWriter.Write (ephemeral);\n{0}", _Indent);
					} else if (  (plaintext.KeyId)) {
					_Output.Write ("            ClientKeyExchange (out var clientKeyId);\n{0}", _Indent);
					_Output.Write ("            outerWriter.Write (clientKeyId);\n{0}", _Indent);
					}
				if (  (plaintext.Ephemerals) ) {
					_Output.Write ("            AddEphemerals (destinationId, plaintextExtensions);\n{0}", _Indent);
					}
				if (  (plaintext.Challenge) ) {
					_Output.Write ("            AddChallenge (plaintextExtensions);\n{0}", _Indent);
					} else if (  (plaintext.Response)) {
					_Output.Write ("            AddResponse (plaintextExtensions);\n{0}", _Indent);
					}
				if (  (plaintext.Credential) ) {
					_Output.Write ("            AddCredentials (plaintextExtensions);\n{0}", _Indent);
					}
				if (  (plaintext.AddExtensions) ) {
					_Output.Write ("            plaintextExtensions.AddRangeSafe(plaintextExtensionsIn);\n{0}", _Indent);
					_Output.Write ("            outerWriter.WriteExtensions(plaintextExtensions);\n{0}", _Indent);
					} else {
					_Output.Write ("            outerWriter.WriteExtensions(plaintextExtensionsIn);\n{0}", _Indent);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (packet.HasMezzanine)  ) {
				_Output.Write ("            // Mezzanine\n{0}", _Indent);
				_Output.Write ("            var mezanineWriter = PacketWriterFactory(outerWriter);\n{0}", _Indent);
				if (  (mezzanine.AddExtensions) ) {
					_Output.Write ("            var mezanineExtensions = new List<PacketExtension>();\n{0}", _Indent);
					}
				if (  (mezzanine.Ephemeral) ) {
					_Output.Write ("            MutualKeyExchange (out var ephemeral, out var clientKeyId);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.Write (clientKeyId);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.Write (ephemeral);\n{0}", _Indent);
					} else if (  (mezzanine.KeyId)) {
					_Output.Write ("            MutualKeyExchange (out var clientKeyId);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.Write (clientKeyId);\n{0}", _Indent);
					}
				if (  (mezzanine.Credential) ) {
					_Output.Write ("            AddCredentials (mezanineExtensions);\n{0}", _Indent);
					}
				if (  (mezzanine.AddExtensions) ) {
					_Output.Write ("            mezanineExtensions.AddRangeSafe(mezanineExtensionsIn);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.WriteExtensions(mezanineExtensions);\n{0}", _Indent);
					} else {
					_Output.Write ("            mezanineWriter.WriteExtensions(mezanineExtensionsIn);\n{0}", _Indent);
					}
				if (  (packet.HasEncrypted)  ) {
					_Output.Write ("            // Encrypted inside Mezzanine\n{0}", _Indent);
					_Output.Write ("            var innerWriter = PacketWriterFactory(mezanineWriter);\n{0}", _Indent);
					_Output.Write ("            innerWriter.WriteExtensions(ciphertextExtensions);\n{0}", _Indent);
					_Output.Write ("            innerWriter.Write(sourceId);\n{0}", _Indent);
					_Output.Write ("            innerWriter.Write(payload);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.Encrypt(MutualKeyOut, innerWriter, false);\n{0}", _Indent);
					} else {
					_Output.Write ("            mezanineWriter.Write(sourceId);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.Write(payload);\n{0}", _Indent);
					}
				_Output.Write ("            outerWriter.Encrypt(ClientKeyOut, mezanineWriter, true);\n{0}", _Indent);
				} else if (  (packet.HasEncrypted) ) {
				_Output.Write ("            // Encrypted in plaintext\n{0}", _Indent);
				_Output.Write ("            var innerWriter = PacketWriterFactory(outerWriter);\n{0}", _Indent);
				_Output.Write ("            innerWriter.WriteExtensions(ciphertextExtensions);\n{0}", _Indent);
				_Output.Write ("            innerWriter.Write(sourceId);\n{0}", _Indent);
				_Output.Write ("            innerWriter.Write(payload);\n{0}", _Indent);
				_Output.Write ("            outerWriter.Encrypt(MutualKeyOut, innerWriter, true);\n{0}", _Indent);
				} else {
				_Output.Write ("            // Only have plaintext\n{0}", _Indent);
				if (  (packet.IsClient) ) {
					_Output.Write ("            outerWriter.Write(sourceId);\n{0}", _Indent);
					}
				_Output.Write ("            outerWriter.Write(payload);\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // Return the outermost packet\n{0}", _Indent);
			_Output.Write ("            return outerWriter.Packet;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// GenerateParser
		//
		public void GenerateParser (Packet packet, bool isstatic) {
			 var plaintext = packet.Plaintext;
			 var mezzanine = packet.Mezzanine;
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Parse the packet <paramref name=\"packet\"/> as a {1} packet.\n{0}", _Indent, packet.Id);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"packet\">The packet data</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"position\">Start position at which reading of the packet should start.</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"count\">Maximum number of bytes to be read from <paramref name=\"packet\"/>.\n{0}", _Indent);
			_Output.Write ("        /// If less than 0, <paramref name=\"packet\"/> is read to the end.</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The parsed packet.</returns>\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public {1} {2} Parse{3} (\n{0}", _Indent, isstatic.If("static"), packet.ClassName, packet.Id);
			_Output.Write ("                byte[] packet,\n{0}", _Indent);
			_Output.Write ("                int position=0,\n{0}", _Indent);
			_Output.Write ("                int count = -1) {{\n{0}", _Indent);
			_Output.Write ("            var result = new {1} () ;\n{0}", _Indent, packet.ClassName);
			if (  (!isstatic)              ) {
				_Output.Write ("            PacketIn=result;\n{0}", _Indent);
				}
			_Output.Write ("            // The plaintext part\n{0}", _Indent);
			_Output.Write ("            var outerReader = PacketReaderFactory(packet, position, count);\n{0}", _Indent);
			if (  (packet.HasPlaintext)  ) {
				if (  (plaintext.Ephemeral) ) {
					_Output.Write ("            result.HostKeyId = outerReader.ReadString ();\n{0}", _Indent);
					_Output.Write ("            result.ClientEphemeral = outerReader.ReadBinary ();\n{0}", _Indent);
					} else if (  (plaintext.KeyId)) {
					_Output.Write ("            result.HostKeyId = outerReader.ReadString ();\n{0}", _Indent);
					}
				}
			_Output.Write ("            result.PlaintextExtensions = outerReader.ReadExtensions();\n{0}", _Indent);
			if (  (packet.HasPlaintext)  ) {
				if (  (plaintext.Credential) ) {
					_Output.Write ("            CredentialOther = CredentialSelf.GetCredentials (result.PlaintextExtensions);\n{0}", _Indent);
					}
				}
			if (  (packet.Completer) ) {
				_Output.Write ("            // Parsing the inner packet is deferred until plaintext is parsed.\n{0}", _Indent);
				_Output.Write ("            result.Reader = outerReader;\n{0}", _Indent);
				} else {
				ParseMezzanine (packet);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            return result;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			}
		

		//
		// ParseMezzanine
		//
		public void ParseMezzanine (Packet packet) {
			 var plaintext = packet.Plaintext;
			 var mezzanine = packet.Mezzanine;
			if (  (packet.HasPlaintext)  ) {
				if (  (plaintext.Ephemeral) ) {
					_Output.Write ("            ClientKeyExchange (result.ClientEphemeral, result.HostKeyId);\n{0}", _Indent);
					} else if (  (plaintext.KeyId)) {
					_Output.Write ("            ClientKeyExchange (result.HostKeyId);\n{0}", _Indent);
					}
				}
			if (  (packet.HasMezzanine)  ) {
				_Output.Write ("            // Mezzanine\n{0}", _Indent);
				_Output.Write ("            var mezanineReader = outerReader.Decrypt (ClientKeyIn);\n{0}", _Indent);
				if (  (mezzanine.Ephemeral) ) {
					_Output.Write ("            result.ClientKeyId = mezanineReader.ReadString ();\n{0}", _Indent);
					_Output.Write ("            result.HostEphemeral = mezanineReader.ReadBinary ();\n{0}", _Indent);
					} else if (  (mezzanine.KeyId)) {
					_Output.Write ("            result.ClientKeyId = mezanineReader.ReadString ();\n{0}", _Indent);
					}
				_Output.Write ("            result.MezzanineExtensions = mezanineReader.ReadExtensions();\n{0}", _Indent);
				if (  (mezzanine.Credential) ) {
					_Output.Write ("            CredentialOther = CredentialSelf.GetCredentials (result.MezzanineExtensions);\n{0}", _Indent);
					}
				if (  (mezzanine.Ephemeral) ) {
					_Output.Write ("            MutualKeyExchange (result.HostEphemeral, result.ClientKeyId);\n{0}", _Indent);
					} else if (  (mezzanine.KeyId)) {
					_Output.Write ("            MutualKeyExchange (result.ClientKeyId);\n{0}", _Indent);
					}
				if (  (packet.HasEncrypted)  ) {
					_Output.Write ("            // Encrypted inside Mezzanine\n{0}", _Indent);
					_Output.Write ("            var innerReader = mezanineReader.Decrypt (MutualKeyIn, false);\n{0}", _Indent);
					_Output.Write ("            result.CiphertextExtensions = innerReader.ReadExtensions();\n{0}", _Indent);
					_Output.Write ("            result.SourceId = innerReader.ReadBinary();\n{0}", _Indent);
					_Output.Write ("            result.Payload = innerReader.ReadBinary();\n{0}", _Indent);
					} else {
					_Output.Write ("            result.SourceId = mezanineReader.ReadBinary();\n{0}", _Indent);
					_Output.Write ("            result.Payload = mezanineReader.ReadBinary();\n{0}", _Indent);
					}
				} else if (  (packet.HasEncrypted) ) {
				_Output.Write ("            // Encrypted inside Mezzanine\n{0}", _Indent);
				_Output.Write ("            var innerReader = mezanineReader.Decrypt (MutualKeyIn);\n{0}", _Indent);
				_Output.Write ("            result.CiphertextExtensions = innerReader.ReadExtensions();\n{0}", _Indent);
				_Output.Write ("            result.SourceId = innerReaderg.ReadBinary();\n{0}", _Indent);
				_Output.Write ("            result.Payload = innerReader.ReadBinary();\n{0}", _Indent);
				} else {
				_Output.Write ("            // Only have plaintext\n{0}", _Indent);
				if (  (packet.IsClient) ) {
					_Output.Write ("            result.SourceId = outerReader.ReadBinary();\n{0}", _Indent);
					}
				_Output.Write ("            result.Payload = outerReader.ReadBinary();\n{0}", _Indent);
				}
			}
		

		//
		// GeneratePacket
		//
		public void GeneratePacket (Packet packet) {
			 var plaintext = packet.Plaintext;
			 var mezzanine = packet.Mezzanine;
			_Output.Write ("\n{0}", _Indent);
			if (  (packet.Completer) ) {
				_Output.Write ("        ///<summary>Packet reader used to complete reading of the packet.</summary> \n{0}", _Indent);
				_Output.Write ("        public PacketReader Reader{{ get; set; }}\n{0}", _Indent);
				}
			if (  (packet.HasMezzanine)  ) {
				_Output.Write ("        ///<summary>Options specified in the packet mezzanine.</summary> \n{0}", _Indent);
				_Output.Write ("        public List<PacketExtension> MezzanineExtensions{{ get; set; }}\n{0}", _Indent);
				}
			if (  (packet.HasEncrypted) ) {
				_Output.Write ("        ///<summary>Options specified in the packet ciphertext.</summary> \n{0}", _Indent);
				_Output.Write ("        public List<PacketExtension> CiphertextExtensions {{ get; set; }}\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			if (  (packet.HasPlaintext)  ) {
				if (  (plaintext.Ephemeral) ) {
					_Output.Write ("        ///<summary>Host chosen ephemeral key.</summary> \n{0}", _Indent);
					_Output.Write ("        public byte[] ClientEphemeral  {{ get; set; }}\n{0}", _Indent);
					}
				if (  (plaintext.KeyId) ) {
					_Output.Write ("        ///<summary>Client Key Identifier.</summary> \n{0}", _Indent);
					_Output.Write ("        public string HostKeyId {{ get; set; }}\n{0}", _Indent);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			if (  (packet.HasMezzanine)  ) {
				if (  (mezzanine.Ephemeral) ) {
					_Output.Write ("        ///<summary>Client chosen ephemeral key.</summary> \n{0}", _Indent);
					_Output.Write ("        public byte[] HostEphemeral  {{ get; set; }}\n{0}", _Indent);
					}
				if (  (mezzanine.KeyId) ) {
					_Output.Write ("        ///<summary>Host Key Identifier.</summary> \n{0}", _Indent);
					_Output.Write ("        public string ClientKeyId {{ get; set; }}\n{0}", _Indent);
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// GenerateMD
		//
		public void GenerateMD (YaschemaStruct YaschemaStruct) {
			_Output.Write ("\n{0}", _Indent);
			}
		}
	}
