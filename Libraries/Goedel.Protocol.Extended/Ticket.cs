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

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Goedel.Utilities;

namespace Goedel.Protocol.Extended {
    
    /// <summary>Represent symmetric key ticket.</summary>
    public class TicketData {

        /// <summary></summary>
        public static Cryptography.Authentication TicketAuthentication = 
                        Cryptography.Authentication.HS256T128;

        /// <summary>Number of authentication bytes.</summary>
        protected static int AuthenticationBytes = 0;

        /// <summary>Number of master key bytes</summary>
        protected int MasterKeyBytes = Cryptography.KeyLength (TicketAuthentication) / 8;

        static Encoding UTF8Encoding = new UTF8Encoding ();

        /// <summary>Account name.</summary>
        public string Account;

        /// <summary>Account domain.</summary>
        public string Domain;

        /// <summary>Message Authentication Code</summary>
        public byte[] MAC;

        /// <summary>The account ID in username@domain form.</summary>
        public byte[] AccountID {
            get { return UTF8Encoding.GetBytes (Account + "@" + Domain); }
            }

        /// <summary>The ticket data.</summary>
        public byte[] Ticket;

        /// <summary>The authentication algorithm identifier.</summary>
        public Cryptography.Authentication Authentication;

        /// <summary>The encryption algorithm identifier.</summary>
        public Cryptography.Encryption Encryption;

        /// <summary>The master encryption key</summary>
        public Cryptography.Key MasterKey;

        /// <summary>The authentication key derrived from the master key.</summary>
        public Cryptography.Key AuthenticationKey;

        /// <summary>The encryption key derrived from the master key.</summary>
        public Cryptography.Key EncryptionKey;

        /// <summary>If true, ticket has been authenticated.</summary>
        public Boolean Authenticated;


        /// <summary>Default constructor, initialize new master key.</summary>
        public TicketData() {
            MasterKey = new Cryptography.Key (TicketAuthentication);
            // Derrive the Authentication and Encryption Keys
            }

        /// <summary>Constructor specifying encryption and authentication algorithms.</summary>
        /// <param name="Authentication">The authentication algorithm identifier.</param>
        /// <param name="Encryption">The encryption algorithm identifier.</param>
        public TicketData(Cryptography.Authentication Authentication,
                Cryptography.Encryption Encryption) : this (){

            this.Authentication = Authentication;
            this.Encryption = Encryption;

            }

        /// <summary>
        /// Unpack ticket data.
        /// </summary>
        /// <returns>Number of bytes read.</returns>
        protected int Unpack() {
            int index = 0;
            byte x;

            x = Ticket [index++];
            if (x != 0) throw new Exception ("Bad ticket");
            x = Ticket [index++]; // ignore, checked already
            Authentication = (Cryptography.Authentication) Ticket [index++];
            Encryption = (Cryptography.Encryption) Ticket [index++];

            byte [] MasterKeyData = new byte [MasterKeyBytes];
            for (int i = 0; i < MasterKeyBytes; i++) {
                MasterKeyData [i] = Ticket [index++];
                }
            MasterKey = new Cryptography.Key (MasterKeyData, Authentication, Encryption);

            x = Ticket [index++];
            byte [] AccountIDData = new byte [x];
            int At = x; // No @ in string would mean it is all account, no domain
            for (int i = 0; i < x; i++) {
                if (Ticket[index] == '@') {
                    At = i;
                    }
                AccountIDData [i] = Ticket [index++];
                }
            Account = UTF8Encoding.GetString (AccountIDData, 0, At);
            Account = UTF8Encoding.GetString (AccountIDData, At-1, x-At-1);

            return index;
            }


        /// <summary>
        /// Construct authenticated ticket from data and authentication value.
        /// </summary>
        /// <param name="TicketIn"></param>
        /// <param name="Hash"></param>
        public TicketData(byte[] TicketIn, byte [] Hash) {
            Ticket = TicketIn;
            this.MAC = Hash;
            Unpack() ;
            }

        /// <summary>
        /// Debug utility.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="Ticket"></param>
        /// <param name="SeedKey"></param>
        static void DumpCrypto (string tag, byte[] Ticket, Cryptography.Key SeedKey) {
            Console.WriteLine ("Dump Crypto {0} Seed={1} Ticket={2}",
                tag, BaseConvert.ToBase64urlString(SeedKey.KeyData), 
                        BaseConvert.ToBase64urlString(Ticket));
            }

        /// <summary>
        /// Package ticket data to form ticket.
        /// </summary>
        /// <param name="Ticket">The raw ticket data.</param>
        /// <param name="SeedKey">The master key for encrypting and authenticating tickets.</param>
        /// <returns>Packed ticket.</returns>
        public static byte[] Pack(byte[] Ticket, Cryptography.Key SeedKey) {
            byte [] Result = null;
            byte [] Hash = null;

            KeyedHashAlgorithm MAC = new HMACSHA256(SeedKey.KeyData);
            using (CryptoStream cs = new CryptoStream(Stream.Null, MAC, CryptoStreamMode.Write)) {
                        cs.Write(Ticket, 0, Ticket.Length);
                        }
            Hash = MAC.Hash;
            
            using (AesManaged Aes = new AesManaged()) {
                Aes.Key = SeedKey.KeyData;
                using (MemoryStream ms = new MemoryStream()) {
                    ms.Write (Aes.IV, 0, Aes.IV.Length);
                    using (CryptoStream cs = new CryptoStream(ms,
                        Aes.CreateEncryptor(Aes.Key, Aes.IV), CryptoStreamMode.Write)) {
                        cs.Write(Ticket, 0, Ticket.Length);
                        cs.Write(Hash, 0, 16);
                        }
                    Result = ms.ToArray ();
                    }
                }
            return Result;
            }

        /// <summary>
        /// Unpack ticket data.
        /// </summary>
        /// <param name="Ticket">The binary ticket</param>
        /// <param name="SeedKey">The master key.</param>
        /// <param name="Hash">The authentication value.</param>
        /// <returns></returns>
        public static byte[] UnPack(byte[] Ticket, Cryptography.Key SeedKey, out byte [] Hash) {
            byte[] Decrypted = null;
            int IVlength;
            using (AesManaged Aes = new AesManaged()) {
                Aes.Key = SeedKey.KeyData;
                IVlength = Aes.IV.Length;
                byte [] IV = new byte [Aes.IV.Length];
                for (int i=0; i< IV.Length; i++) {
                    IV [i] = Ticket [i];
                    }
                Aes.IV = IV;
                using (MemoryStream ms = new MemoryStream()) {
                    using (CryptoStream cs = new CryptoStream(ms,
                        Aes.CreateDecryptor(Aes.Key, Aes.IV), CryptoStreamMode.Write)) {
                        cs.Write(Ticket, 0, Ticket.Length);
                        }
                    Decrypted = ms.ToArray();
                    }
                }

            
            Hash = null;

            int outLen =Decrypted.Length-IVlength - 16;

            KeyedHashAlgorithm MAC = new HMACSHA256(SeedKey.KeyData);
            using (CryptoStream cs = new CryptoStream(Stream.Null, MAC, CryptoStreamMode.Write)) {
                cs.Write(Decrypted, IVlength, outLen);
                }
            Hash = MAC.Hash;

            int offset = Decrypted.Length - 16;
            for (int i = 0; i < 16; i++) {
                if (Decrypted[offset + i] != Hash[i]) {
                    return null;
                    }
                }
            
            byte [] Result = new byte [outLen];
            Array.Copy (Decrypted, IVlength, Result, 0, outLen);

            return Result;
            }

        /// <summary>
        /// Verify ticket
        /// </summary>
        /// <param name="Ticket">Raw ticket data.</param>
        /// <param name="SeedKey">Ticket seed key.</param>
        /// <returns></returns>
        public static bool VerifyTicket (byte[] Ticket, Cryptography.Key SeedKey) {
            int Sign = Ticket.Length - AuthenticationBytes;

            byte [] Hash = Cryptography.GetMAC (Ticket, Sign, TicketAuthentication, SeedKey);

            for (int i = 0; i < AuthenticationBytes; i++) {
                if (Hash [i] != Ticket [Sign+i]) throw new Exception ("Bad Ticket");
                }
            DumpCrypto ("Verify ticket", Hash, SeedKey);
            return true;
            }

        /// <summary>
        /// Convert binary ticket to ticket data.
        /// </summary>
        /// <param name="WrappedTicket">Binary ticket</param>
        /// <param name="SeedKey">Master key for cryptographic operations.</param>
        /// <returns></returns>
        public static TicketData MakeTicket(byte[] WrappedTicket, Cryptography.Key SeedKey) {
            byte [] Hash;
            byte[] Ticket = UnPack (WrappedTicket, SeedKey, out Hash);

            //VerifyTicket (Ticket, SeedKey);

            int index = 0;
            byte x;

            x = Ticket [index++];
            if (x != 0) throw new Exception ("Bad ticket");
            x = Ticket [index++];
            if (x == 0) return new TicketData (Ticket, Hash);
            if (x == 1) return new TemporaryTicketData (Ticket, Hash);
            throw new Exception ("Bad ticket");
            }


        // Create a binary ticket
        // Format is:
        //
        //  [1]         Version number  = 0
        //  [1]         Key identifier  (rolls round)
        //  [1]         Authentication Code ( = 0 for HMAC-SHA256
        //  [1]         Encryption Code ( = 0 for AES-CBC-128
        //  [KEY-SIZE]  MasterKeyData
        //  [1]         Username Name Length
        //  [A-SIZE]    Username (username@domain)


        //  [KEY-SIZE]  MAC Data

        /// <summary>
        /// Return size of ticket in encoded form.
        /// </summary>
        /// <returns>Size of encoded ticket in bytes.</returns>
        public virtual int Length() {
            return (5 + MasterKey.KeyData.Length + AccountID.Length + AuthenticationBytes);
            }

        /// <summary>
        /// Write ticket data to ticket.
        /// </summary>
        /// <returns>Number of bytes written.</returns>
        protected virtual int Fill() {
            int index = 0;

            Ticket [index++] = 0; // Version number
            Ticket [index++] = 0; //
            Ticket [index++] = (byte) Authentication; //
            Ticket [index++] = (byte) Encryption; //
            Array.Copy (MasterKey.KeyData, 0, Ticket, index, MasterKey.KeyData.Length); // The master key
            index += MasterKey.KeyData.Length;
            Ticket [index++] = (byte) AccountID.Length; //
            Array.Copy (AccountID, 0, Ticket, index,  AccountID.Length);
            index += AccountID.Length;

            return (index);
            }

        /// <summary>
        /// Convert to binary ticket under the specified key.
        /// </summary>
        /// <param name="MasterKey"></param>
        /// <returns></returns>
        public byte[] GetTicket(Cryptography.Key MasterKey) {
            Ticket = new byte [Length()];

            return Pack (Ticket, MasterKey);
            }

        }

    /// <summary>Represents the temporary ticket that is created while in the process of authenticating a ticket 
    /// grant request.</summary>
    public class TemporaryTicketData : TicketData {
        
        /// <summary>The client challenge data.</summary>
        public byte[] ClientChallenge;
        
        /// <summary>The server challenge data.</summary>
        public byte[] ServerChallenge;

        /// <summary>Construct a temporary ticket.</summary>
        public TemporaryTicketData() {
            }

        /// <summary>
        /// Construct a temporary ticket from the specified data.
        /// </summary>
        /// <param name="TicketIn">The ticket data.</param>
        /// <param name="Hash">The authentication value.</param>
        public TemporaryTicketData(byte[] TicketIn, byte [] Hash) {
            Ticket = TicketIn;
            MAC = Hash;
            int index = Unpack() ;
            int x;

            x = Ticket [index++];
            ClientChallenge = new byte [x];
            for (int i = 0; i < x; i++) {
                ClientChallenge [i] = Ticket [index++];
                }
            
            x = Ticket [index++];
            ServerChallenge = new byte [x];
            for (int i = 0; i < x; i++) {
                ServerChallenge [i] = Ticket [index++];
                }
            }

        /// <summary>
        /// The ticket lenght in bytes.
        /// </summary>
        /// <returns></returns>
        public override int Length() {
            return (7 + MasterKeyBytes + AccountID.Length + AuthenticationBytes + ClientChallenge.Length + ServerChallenge.Length);
            }

        /// <summary>
        /// Write ticket data to binary ticket.
        /// </summary>
        /// <returns></returns>
        protected override int Fill() {
            int index = 0;

            Ticket [index++] = 0; // Version number
            Ticket [index++] = 1; //
            Ticket [index++] = (byte) Authentication; //
            Ticket [index++] = (byte) Encryption; //
            Array.Copy (MasterKey.KeyData, 0, Ticket, index, MasterKey.KeyData.Length); // The master key
            index += MasterKey.KeyData.Length;
            Ticket [index++] = (byte) AccountID.Length; //
            Array.Copy (AccountID, 0, Ticket, index,  AccountID.Length);
            index += AccountID.Length;
            Ticket [index++] = (byte) ClientChallenge.Length;
            Array.Copy (ClientChallenge, 0, Ticket, index, ClientChallenge.Length); // The server challenge value
            index += ClientChallenge.Length;
            Ticket [index++] = (byte) ServerChallenge.Length;
            Array.Copy (ServerChallenge, 0, Ticket, index, ServerChallenge.Length); // The client challenge value
            index += ServerChallenge.Length;

            return (index);
            }


        }
    }
