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
			// Yaschema.Init();
			_Output.Write ("\n{0}", _Indent);
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
			_Output.Write ("    public partial class ConnectionClient : Connection {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach (var _client in Yaschema.Top) {  if (_client.GetType() == typeof (Client)) { var client = (Client) _client; 
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("	    /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Return an initial packet for this connection.\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("        //public Packet GetInitial () =>\n{0}", _Indent);
				_Output.Write ("        //    HostCredential==null ? new {1} () :\n{0}", _Indent, client.WithoutHostCredential.ClassName);
				_Output.Write ("        //        new {1} (HostCredential);\n{0}", _Indent, client.WithHostCredential.ClassName);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var packet in client.Entries) {
					GenersateSerializer (packet);
					}
					}
	}
			_Output.Write ("\n{0}", _Indent);
			foreach (var _host in Yaschema.Top) {  if (_host.GetType() == typeof (Host)) { var host = (Host) _host; 
				foreach  (var packet in host.Entries) {
					GenerateParser (packet);
					}
					}
	}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	/// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Host connection class. Tracks the state of a host connection.\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    public partial class ConnectionHost : Connection {{\n{0}", _Indent);
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
					if (  (packet.IsInitial) ) {
						_Output.Write ("        // Skip Client packet {1} (initial packets parsed by the listener)\n{0}", _Indent, packet.Id);
						} else {
						GenerateParser (packet);
						}
					}
					}
	}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public partial class Listner {{\n{0}", _Indent);
			foreach (var _client in Yaschema.Top) {  if (_client.GetType() == typeof (Client)) { var client = (Client) _client; 
				foreach  (var packet in client.Entries) {
					if (  (packet.IsInitial) ) {
						GenerateParser (packet);
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
				GenerateCompleter (packet);
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
			 var plaintext = packet.Plaintext.SafeIndex();
			 var mezzanine = packet.Mezzanine.SafeIndex();
			_Output.Write ("        // Serialize {1} packet {2}\n{0}", _Indent, packet.PacketType, packet.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Create a serialised packet of type {1} packet.\n{0}", _Indent, packet.Id);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"payload\">The payload data.</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"plaintextExtensions\">Extensions to be presented in the plaintext segment.</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The serialized data.</returns>\n{0}", _Indent);
			_Output.Write ("        public byte[] Serialize{1} (\n{0}", _Indent, packet.ClassName);
			_Output.Write ("                byte[] payload = null,\n{0}", _Indent);
			_Output.Write ("                List<PacketExtension> plaintextExtensionsIn = null", _Indent);
			if (  (packet.HasMezzanine) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("                List<PacketExtension> mezanineExtensionsIn = null", _Indent);
				}
			if (  (packet.HasEncrypted) ) {
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("                List<PacketExtension> encryptedExtensions = null", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("                ) {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // The plaintext part\n{0}", _Indent);
			_Output.Write ("            var outerWriter = new PacketWriterAesGcm();\n{0}", _Indent);
			if (  (plaintext == null) ) {
				_Output.Write ("            // There are no plaintext fields.\n{0}", _Indent);
				_Output.Write ("            outerWriter.WriteExtensions(plaintextExtensionsIn);\n{0}", _Indent);
				} else {
				_Output.Write ("            // Plaintext fields..\n{0}", _Indent);
				if (  (plaintext.AddExtensions) ) {
					_Output.Write ("            var plaintextExtensions = new List<PacketExtension>();\n{0}", _Indent);
					}
				if (  (plaintext.Ephemeral) ) {
					_Output.Write ("            // ClientKeyExchange (out var ephemeral, out var keyId);\n{0}", _Indent);
					_Output.Write ("            // outerWriter.Write (keyId);\n{0}", _Indent);
					_Output.Write ("            // outerWriter.Ephemeral (ephemeral);\n{0}", _Indent);
					} else if (  (plaintext.KeyId)) {
					_Output.Write ("            // ClientKeyExchange (out var keyId);\n{0}", _Indent);
					_Output.Write ("            // outerWriter.Write (keyId);\n{0}", _Indent);
					}
				if (  (plaintext.Ephemerals) ) {
					_Output.Write ("            // AddEphemerals (plaintextExtensions);\n{0}", _Indent);
					}
				if (  (plaintext.Challenge) ) {
					_Output.Write ("            // AddChallenge (plaintextExtensions);\n{0}", _Indent);
					} else if (  (plaintext.Response)) {
					_Output.Write ("            // AddResponse (plaintextExtensions);\n{0}", _Indent);
					}
				if (  (plaintext.Credential) ) {
					_Output.Write ("            // AddCredentials (plaintextExtensions);\n{0}", _Indent);
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
				_Output.Write ("            var mezanineWriter = new PacketWriterAesGcm();\n{0}", _Indent);
				if (  (mezzanine.AddExtensions) ) {
					_Output.Write ("            var mezanineExtensions = new List<PacketExtension>();\n{0}", _Indent);
					}
				if (  (mezzanine.Ephemeral) ) {
					_Output.Write ("            // MutualKeyExchange (out var ephemeral, out var keyId);\n{0}", _Indent);
					_Output.Write ("            // mezanineWriter.Write (keyId);\n{0}", _Indent);
					_Output.Write ("            // mezanineWriter.Ephemeral (ephemeral);\n{0}", _Indent);
					} else if (  (mezzanine.KeyId)) {
					_Output.Write ("            // MutualKeyExchange (out var keyId);\n{0}", _Indent);
					_Output.Write ("            // mezanineWriter.Write (keyId);\n{0}", _Indent);
					}
				if (  (mezzanine.Credential) ) {
					_Output.Write ("            // AddCredentials (mezanineExtensions);\n{0}", _Indent);
					}
				if (  (mezzanine.AddExtensions) ) {
					_Output.Write ("            mezanineExtensions.AddRangeSafe(mezanineExtensionsIn);\n{0}", _Indent);
					_Output.Write ("            outerWriter.WriteExtensions(mezanineExtensions);\n{0}", _Indent);
					} else {
					_Output.Write ("            outerWriter.WriteExtensions(plaintextExtensionsIn);\n{0}", _Indent);
					}
				if (  (packet.HasEncrypted)  ) {
					_Output.Write ("            // Encrypted inside Mezzanine\n{0}", _Indent);
					_Output.Write ("            var innerWriter = new PacketWriter();\n{0}", _Indent);
					_Output.Write ("            Write(innerWriter, payload, ciphertextExtensions);\n{0}", _Indent);
					_Output.Write ("            mezanineWriter.Encrypt(MutualKeyOut, innerWriter);\n{0}", _Indent);
					}
				_Output.Write ("            outerWriter.Encrypt(ClientKeyOut, mezanineWriter);\n{0}", _Indent);
				} else if (  (packet.HasEncrypted) ) {
				_Output.Write ("            // Encrypted in plaintext\n{0}", _Indent);
				_Output.Write ("            var innerWriter = new PacketWriter();\n{0}", _Indent);
				_Output.Write ("            Write(innerWriter, payload, ciphertextExtensions);\n{0}", _Indent);
				_Output.Write ("            outerWriter.Encrypt(MutualKeyOut, innerWriter);\n{0}", _Indent);
				} else {
				_Output.Write ("            // Only have plaintext\n{0}", _Indent);
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
		public void GenerateParser (Packet packet) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        /// <summary>\n{0}", _Indent);
			_Output.Write ("        /// Parse the packet <paramref name=\"packet\"/> received from <paramref name=\"sourceId\"/>\n{0}", _Indent);
			_Output.Write ("        /// as a {1} packet.\n{0}", _Indent, packet.Id);
			_Output.Write ("        /// </summary>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"sourceId\">The packet source.</param>\n{0}", _Indent);
			_Output.Write ("        /// <param name=\"packet\">The packet data</param>\n{0}", _Indent);
			_Output.Write ("        /// <returns>The parsed packet.</returns>\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public {1} Parse{2} (PortId sourceId, byte[] packet) {{\n{0}", _Indent, packet.ClassName, packet.ClassName);
			_Output.Write ("            var result = new {1} () {{\n{0}", _Indent, packet.ClassName);
			_Output.Write ("                SourcePortId = sourceId\n{0}", _Indent);
			_Output.Write ("                }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // Read the plaintext part\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            return result;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			}
		

		//
		// GenerateCompleter
		//
		public void GenerateCompleter (Packet packet) {
			if (  (packet.HasMezzanine) ) {
				_Output.Write ("        /// <summary>\n{0}", _Indent);
				_Output.Write ("        /// Perform key exchanges and complete parsing of the packet\n{0}", _Indent);
				_Output.Write ("        /// </summary>\n{0}", _Indent);
				_Output.Write ("        public override void Complete () {{\n{0}", _Indent);
				_Output.Write ("            // perform the Mezzanine key exchange here\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				if (  (packet.HasEncrypted) ) {
					_Output.Write ("            // perform the Mutual key exchange here\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("            }}\n{0}", _Indent);
				}
			}
		

		//
		// GenerateMD
		//
		public void GenerateMD (YaschemaStruct YaschemaStruct) {
			_Output.Write ("\n{0}", _Indent);
			}
		}
	}
