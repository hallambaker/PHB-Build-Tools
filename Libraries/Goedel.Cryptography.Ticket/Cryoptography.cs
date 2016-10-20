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
using System.Text;
using System.Diagnostics;
using System.IO;
using Goedel.Utilities;
using Goedel.Cryptography;

namespace Goedel.Cryptography.Ticket {
    public partial class Cryptography {


        ///// <summary>
        ///// Debugging utility, writes out binary data buffer as hex value.
        ///// </summary>
        ///// <param name="Tag">Tag</param>
        ///// <param name="Data">Data</param>
        //public static void Dump(string Tag, byte[] Data) {
        //    Console.Write("KEY[{0}]::", Tag);
        //    for (int i = 0; i < Data.Length; i++) {
        //        Console.Write("{0:x2}-", Data[i]);
        //        }
        //    Console.WriteLine();
        //    }


        /// <summary>
        /// Ticket, public part corresponding to shared secret.
        /// </summary>
        public class Ticket {

            /// <summary>The master private key</summary>
            public Key      Key;
            /// <summary>Encryption key derrived from master private key.</summary>
            public Key      EncryptionKey;
            /// <summary>Authentication key derrived from master private key.</summary>
            public Key      AuthenticationKey;
            /// <summary>The encoded ticket data.</summary>
            public byte []  TicketData;


            /// <summary>The block size for encryption operations</summary>
            public int      BlockSize = 16;
            /// <summary>The MAC code output.</summary>
            public int      MACLength = 16;
            /// <summary>The length of the required initialization vector.</summary>
            public int      IVLength = 16;

            CryptoProviderEncryption EncryptionAlgorithm;
            CryptoProviderAuthentication AuthenticationAlgorithm;

            /// <summary>Constructor of ticket from ticket data and key data.</summary>
            /// <param name="TicketData">The ticket public value.</param>
            /// <param name="KeyDataIn">The ticket private data.</param>
            /// <param name="Authentication">The authentication algorithm to use.</param>
            /// <param name="Encryption">The encryption algorithm to use.</param>
            public Ticket(byte[] TicketData, byte[] KeyDataIn, 
                    string Authentication, string Encryption) : 
                        this (TicketData, 
                            new Key (KeyDataIn, Authentication, Encryption) ) {
                }

            /// <summary>Constructor of ticket from ticket data and key data.</summary>
            /// <param name="TicketData">The ticket public value.</param>
            /// <param name="Key">The ticket private data.</param>
            public Ticket(byte[] TicketData, Key Key) {
                this.Key = Key;
                this.TicketData = TicketData;

                this.EncryptionKey = Key;           // NYI derrive properly
                this.AuthenticationKey = Key;       // NYI derrive properly


                EncryptionAlgorithm = Platform.AES_256.CryptoProviderEncryption ();

                BlockSize = 32;
                IVLength = BlockSize;
                AuthenticationAlgorithm = Platform.HMAC_SHA2_256.CryptoProviderAuthentication();

                MACLength = BlockSize;
                }

            /// <summary>The initialization vector</summary>
            public byte[] IV() {
                return Nonce (IVLength);
                }

            /// <summary>
            /// Encryption method, this should be replaced with the common wrapper classes.
            /// </summary>
            /// <param name="IV"></param>
            /// <param name="ClearText"></param>
            /// <param name="Ciphertext"></param>
            /// <param name="To"></param>
            public void Encrypt(byte[] IV,
                    byte[] ClearText, byte[] Ciphertext, int To) {

                throw new NYI();
                }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="Input"></param>
            /// <param name="Count"></param>
            /// <returns></returns>
            public byte [] Authenticate(byte[] Input, int Count) {
                return Authenticate (Input, 0, Count, Input, Count);
                }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="Input"></param>
            /// <param name="Offset"></param>
            /// <param name="Count"></param>
            /// <param name="Output"></param>
            /// <param name="Index"></param>
            /// <returns></returns>
            public byte [] Authenticate(byte[] Input, int Offset, int Count, 
                        byte[] Output, int Index) {

                throw new NYI();

                //// NYI check that there is space in the output buffer for the 
                //// hash data

                //byte [] Hash = AuthenticationAlgorithm.ComputeHash (Input, Offset, Count);
                //Array.Copy (Hash, 0, Output, Index, MACLength);

                //return Hash;
                }

            }

        /// <summary>Represents a cryptographic key</summary>
        public class Key {

            /// <summary>The symmetric key</summary>
            public byte[] KeyData;

            /// <summary>The authentication algorithm.</summary>
            public Authentication Authentication = Authentication.Unknown;

            /// <summary>The encryption algorithm.</summary>
            public Encryption Encryption = Encryption.Unknown;

            /// <summary>Key constructor.</summary>
            public Key() {
                this.KeyData = Nonce(KeyLength() / 8);
                this.Authentication = Authentication.Unknown;
                this.Encryption = Encryption.Unknown;
                }

            /// <summary>
            /// Key constructor
            /// </summary>
            /// <param name="KeyDataIn"></param>
            /// <param name="Authentication"></param>
            /// <param name="Encryption"></param>
            public Key(byte[] KeyDataIn, string Authentication, string Encryption) {
                this.KeyData = KeyDataIn;
                this.Authentication = AuthenticationCode(Authentication);
                this.Encryption = EncryptionCode(Encryption);
                }

            /// <summary>
            /// Key Constructor
            /// </summary>
            /// <param name="KeyData"></param>
            /// <param name="Authentication"></param>
            /// <param name="Encryption"></param>
            public Key(byte[] KeyData, Authentication Authentication, Encryption Encryption) {
                this.KeyData = KeyData;
                this.Authentication = Authentication;
                this.Encryption = Encryption;
                }

            /// <summary>
            /// Key Constructor
            /// </summary>
            /// <param name="Master"></param>
            /// <param name="Authentication"></param>
            /// <param name="Salt"></param>
            public Key(Key Master, Authentication Authentication, string Salt) {
                this.Authentication = Authentication;
                this.Encryption = Encryption.None;
                this.KeyData = GetMAC(Salt, Authentication, Master);
                }

            /// <summary>
            /// Key Constructor
            /// </summary>
            /// <param name="Alg"></param>
            public Key(Authentication Alg) {
                Authentication = Alg;
                switch (Alg) {
                    case Authentication.HS256:
                    case Authentication.HS256T128: {
                            KeyData = Nonce(16);
                            break;
                            }
                    case Authentication.HS512: {
                            KeyData = Nonce(32);
                            break;
                            }
                    }
                }

            /// <summary>
            /// Key Constructor
            /// </summary>
            /// <param name="Alg"></param>
            public Key(Encryption Alg) {
                Encryption = Alg;
                switch (Alg) {
                    case Encryption.A128CBC:
                    case Encryption.A128GCM: {
                            KeyData = Nonce(16);
                            break;
                            }
                    case Encryption.A256CBC:
                    case Encryption.A256GCM: {
                            KeyData = Nonce(32);
                            break;
                            }
                    }
                }

            ///// <summary>
            ///// Debug method
            ///// </summary>
            ///// <param name="Tag"></param>
            //public void Dump(string Tag) {
            //    Console.Write("KEY[{0}]::", Tag);
            //    for (int i = 0; i < 8; i++) {
            //        Console.Write("{0:x2}-", KeyData[i]);
            //        }
            //    Console.WriteLine ();
            //    }
            }


        //static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();


        /// <summary>Return key length of algorithm.</summary>
        /// <returns>The key length in bits.</returns>
        public static int KeyLength(Encryption Algorithm) {
            switch (Algorithm) {
                case Encryption.A128CBC : return 128;
                case Encryption.A128GCM : return 128 ;
                case Encryption.A256CBC : return 256;
                case Encryption.A256GCM : return 256;
                default : return 256;
                }
            }

        /// <summary>Return key length of algorithm.</summary>
        /// <returns>The key length in bits.</returns>
        public static int KeyLength(Authentication Algorithm) {
            switch (Algorithm) {
                case Authentication.HS256 : return 256;
                case Authentication.HS256T128 : return 128 ;
                case Authentication.HS512 : return 256;
                default : return 256;
                }
            }

        /// <summary>Return longest key length of known algorithms.</summary>
        /// <returns>The key length in bits.</returns>
        public static int KeyLength() {
            return (KeyLength(Authentication.Unknown) > KeyLength (Encryption.Unknown)) ?
                KeyLength(Authentication.Unknown) : KeyLength (Encryption.Unknown);
            }

        /// <summary>Encryption algorithm identifiers.</summary>
        public enum Encryption {
            /// <summary></summary>
            None = -2,
            /// <summary></summary>
            Unknown = -1,
            /// <summary></summary>
            A128CBC = 0,
            /// <summary></summary>
            A256CBC = 1,
            /// <summary></summary>
            A128GCM = 2,
            /// <summary></summary>
            A256GCM = 3
            }

        /// <summary>Authentication algorithm identifiers.</summary>
        public enum Authentication {
            /// <summary></summary>
            None = -2,
            /// <summary></summary>
            Unknown = -1,
            /// <summary>HMAC SHA-2 256</summary>
            HS256 = 0,
            /// <summary>HMAC SHA-2 512</summary>
            HS512 = 2,
            /// <summary>HMAC SHA-2 256 truncated to 128 bits</summary>
            HS256T128 = 3
            }

        /// <summary>JOSE authentication identifier strings</summary>
        public static List<string> AuthenticationAlgorithms = 
            new List<string>() {"HS256", "HS384", "HS512", "HS256T128"};
        /// <summary>JOSE encryption identifier strings</summary>
        public static List<string> EncryptionAlgorithms = 
            new List<string>() {"A128CBC", "A256CBC", "A128GCM", "A256GCM"};

        /// <summary>
        /// Convert string identifier to enumerated identifier;
        /// </summary>
        /// <param name="Code">JOSE string identifier</param>
        /// <returns>Enumerated identifier</returns>
        public static Authentication AuthenticationCode (string Code) {
            for (int i = 0 ; i< AuthenticationAlgorithms.Count; i++ ) {
                if (AuthenticationAlgorithms[i] == Code) {
                    return (Authentication) i;
                    }
                }
            return Authentication.Unknown;
            }

        /// <summary>
        /// Convert enumerated identifier to string identifier;
        /// </summary>
        /// <param name="Code">Enumerated identifier</param>
        /// <returns>JOSE string identifier</returns>
        public static string AuthenticationCode(Authentication Code) {
            if (Code >= 0 & (int) Code < AuthenticationAlgorithms.Count) {
                return AuthenticationAlgorithms [(int) Code];
                }
            return null;
            }

        /// <summary>
        /// Convert string identifier to enumerated identifier;
        /// </summary>
        /// <param name="Code">JOSE string identifier</param>
        /// <returns>Enumerated identifier</returns>
        public static Encryption EncryptionCode (string Code) {
            for (int i = 0 ; i< EncryptionAlgorithms.Count; i++ ) {
                if (EncryptionAlgorithms[i] == Code) {
                    return (Encryption) i;
                    }
                }
            return Encryption.Unknown;
            }

        /// <summary>
        /// Convert enumerated identifier to string identifier;
        /// </summary>
        /// <param name="Code">Enumerated identifier</param>
        /// <returns>JOSE string identifier</returns>
        public static string EncryptionCode(Encryption Code) {
            if (Code >= 0 & (int) Code < EncryptionAlgorithms.Count) {
                return EncryptionAlgorithms [(int) Code];
                }
            return null;
            }

        /// <summary>Return a new nonce with the specified length in bytes.</summary>
        public static byte[] Nonce(int size) {
            return Platform.GetRandomBytes(size); 
            }

        /// <summary>Return a new nonce with the default length (16 bytes).</summary>
        public static byte[] Nonce() {
            return Nonce (16);
            }

        /// <summary>
        /// Convert a text string (e.g. 'account@example.com') to a PIN verification code
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="key"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string MakePin(string Text, byte [] key, int length) {
            byte [] Bytes = GetBytes (Text);

            throw new NYI();

            //HMACSHA256 MAC = new HMACSHA256(key);
            //CryptoStream CryptoStream = new CryptoStream(Stream.Null, MAC, CryptoStreamMode.Write);
            //CryptoStream.Write(Bytes, 0, Text.Length);
            //CryptoStream.Close();

            //byte[] Hash = MAC.Hash;

            //return BaseConvert.ToBase32hsString (Hash, length);
            }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Algorithm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        static CryptoProviderAuthentication GetKeyedHashAlgorithm(Authentication Algorithm, Key key) {

            throw new NYI();
            //switch (Algorithm) {
            //    case Authentication.HS256: {
            //            return new HMACSHA256(key.KeyData);
            //            }
            //    case Authentication.HS256T128: {
            //            return new HMACSHA256(key.KeyData);
            //            }
            //    case Authentication.HS384:{
            //            return new HMACSHA384(key.KeyData);
            //            }
            //    case Authentication.HS512:{
            //            return new HMACSHA512(key.KeyData);
            //            }
            //    default: throw new Exception("MAC not known");
            //    }
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Algorithm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] GetMAC(string Text, Authentication Algorithm, Key key) {
            byte[] Bytes = GetBytes(Text);
            
            return GetMAC(Bytes, Bytes.Length, Algorithm, key);
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bytes"></param>
        /// <param name="Length"></param>
        /// <param name="Algorithm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte [] GetMAC(byte [] Bytes, int Length, Authentication Algorithm, Key key) {
            throw new NYI();

            //KeyedHashAlgorithm MAC = GetKeyedHashAlgorithm (Algorithm, key);
                      
            //CryptoStream CryptoStream = new CryptoStream(Stream.Null, MAC, CryptoStreamMode.Write);
            //CryptoStream.Write(Bytes, 0, Length);
            //CryptoStream.Close();

            //return MAC.Hash;
            }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="StreamBuffer"></param>
        ///// <param name="Algorithm"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static byte [] GetMAC(StreamBuffer StreamBuffer, Authentication Algorithm, Key key) {
        //    throw new NYI();

        //    //KeyedHashAlgorithm MAC = GetKeyedHashAlgorithm (Algorithm, key);
        //    //StreamBuffer.Write (new CryptoStream(Stream.Null, MAC, CryptoStreamMode.Write));
        //    //return MAC.Hash;
        //    }

        /// <summary>
        /// Test to see if two arrays are equal.
        /// </summary>
        /// <param name="Test1">First test value</param>
        /// <param name="Test2">Second test value</param>
        /// <returns>true if and only if the two arrays are of the same size and each
        /// element is equal.</returns>
        public static bool ArraysEqual(byte[] Test1, byte [] Test2) {
            if ((Test1 == null) & (Test2 != null)) {
                return false;
                }
            if (Test2 == null) {
                return false;
                }
            if (Test1.Length != Test2.Length) {
                return false;
                }
            for (int i = 0; i < Test1.Length; i++) {
                if (Test1[i] != Test2[i]) {
                    return false;
                    }
                }

            return true;
            }


        /// <summary>Convert a string of characters to a byte array using UTF16 encoding</summary>
        /// <param name="Chars"></param>
        /// <returns></returns>
        static byte[] GetBytes(string Chars){
            byte[] Bytes = new byte[Chars.Length * sizeof(char)];
            System.Buffer.BlockCopy(Chars.ToCharArray(), 0, Bytes, 0, Bytes.Length);
            return Bytes;
            }

        /// <summary>Convert an array of UTF16 encoded characters to a string.</summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        static string GetString(byte[] Bytes){
            char[] Chars = new char[Bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(Bytes, 0, Chars, 0, Bytes.Length);
            return new string(Chars);
            }
        }
    }
