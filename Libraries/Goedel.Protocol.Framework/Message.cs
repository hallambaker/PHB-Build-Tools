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
using Goedel.Utilities;
using Goedel.Cryptography.Ticket;


namespace Goedel.Protocol.Framework {

    /// <summary>
    /// Message types
    /// </summary>
    public enum MessageType {
        /// <summary>Request</summary>
        Request,
        /// <summary>Response</summary>
        Response
        }

    /// <summary>
    /// Message bound to transport context
    /// </summary>
    public abstract class BoundMessage {

        /// <summary></summary>
        public string Payload;
        /// <summary></summary>
        public byte[] Ticket = null;
        //TicketData TicketData = null;
        /// <summary></summary>
        public byte[] MAC = null;

        /// <summary></summary>
        public string HTTPBinding {
            get { return HTTP () ;}
            }

        /// <summary></summary>
        public abstract string HTTP() ;

        /// <summary>Default constructor.</summary>
        public BoundMessage() {
            Payload = "{NOT-YET-IMPLEMENTED}";
            }

        Encoding UTF8 = new UTF8Encoding(false);

        /// <summary></summary>
        public int ByteCount {
            get { return UTF8.GetByteCount(Payload); }
            }

        /// <summary></summary>
        public string Base64Ticket {
            get { return BaseConvert.ToBase64urlString(Ticket); }
            }

        /// <summary></summary>
        public string Base64Mac {
            get { return BaseConvert.ToBase64urlString(MAC); }
            }

        /// <summary></summary>
        public byte[] GetMAC(Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
            Goedel.Cryptography.Ticket.Cryptography.Key Key) {
            if (Authentication != Goedel.Cryptography.Ticket.Cryptography.Authentication.Unknown) {
                return Goedel.Cryptography.Ticket.Cryptography.GetMAC(Payload, Authentication, Key);
                }
            else {
                return null;
                }
            }

        /// <summary></summary>
        public void MakeMAC(Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
            Goedel.Cryptography.Ticket.Cryptography.Key Key) {
            MAC = GetMAC(Authentication, Key);
            }

        /// <summary></summary>
        public bool VerifyMAC(Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
            Goedel.Cryptography.Ticket.Cryptography.Key Key) {
            byte[] Test = GetMAC(Authentication, Key);

            return Goedel.Cryptography.Ticket.Cryptography.ArraysEqual(Test, MAC);
            }

        /// <summary></summary>
        public BoundMessage(string PayloadIn) {
            Payload = PayloadIn;
            }


        /// <summary></summary>
        public BoundMessage(string PayloadIn, byte[] TicketIn,
                     Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
                     Goedel.Cryptography.Ticket.Cryptography.Key Key) {
            BindMessage(PayloadIn, TicketIn, Authentication, Key);
            }

        private void BindMessage(string PayloadIn, byte[] TicketIn,
                    Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
                    Goedel.Cryptography.Ticket.Cryptography.Key Key) {
            Payload = PayloadIn;
            Ticket = TicketIn;
            MakeMAC(Authentication, Key);
            }



        }


    /// <summary></summary>
    public class BoundRequest : BoundMessage {

        static string HeaderFormat = @"Post {0} HTTP/1.1
Host: {1}
Cache-Control: no-store
Content-Type: Application/json;charset=UTF-8
Content-Length: {2}
";
        static string ContentIntegrityFormat = "Session: Value={0}; Id={1}\n";


        //public BoundRequest(string PayloadIn, Cryptographic Cryptographic)
        //    : base(PayloadIn, Cryptographic) { }
        /// <summary></summary>
        public BoundRequest(string PayloadIn) 
            : base (PayloadIn) { }

        /// <summary></summary>
        public BoundRequest(string PayloadIn, byte[] TicketIn,
                     Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
                     Goedel.Cryptography.Ticket.Cryptography.Key Key)
            : base(PayloadIn, TicketIn, Authentication, Key) { }

        /// <summary></summary>
        public override string HTTP () {
            string Header = String.Format (HeaderFormat, "/", "example.com", Payload.Length);
            string ContentIntegrity = "";
            if (Ticket != null) {
                ContentIntegrity = String.Format (ContentIntegrityFormat, Base64Mac, Base64Ticket);
                }

            return Header + ContentIntegrity + "\n" + Payload;
            }
        }

    /// <summary></summary>
    public class BoundResponse : BoundMessage {

        /// <summary></summary>
        public static BoundResponse ErrorBadMac = new BoundResponse (401, "Not Authorized");

        /// <summary></summary>
        public static BoundResponse ErrorUnknown = new BoundResponse (500, "Internal Server Error");

        /// <summary></summary>
        public static BoundResponse ErrorSyntax =  new BoundResponse (400, "Bad Request");


        /// <summary></summary>
        public int Status = 200;

        /// <summary></summary>
        public string StatusDescription = "OK";

        static string HeaderFormat = @"HTTP/1.1 {0} {1}
Content-Type: application/json;charset=UTF-8
Content-Length: {2}
";
        static string ContentIntegrityFormat = "Session: Value={0}; Id={1}\n";
        //public BoundResponse(string PayloadIn, Cryptographic Cryptographic)
        //    : base(PayloadIn, Cryptographic) { }

        /// <summary></summary>
        public BoundResponse(string PayloadIn) 
            : base (PayloadIn) { }

        /// <summary></summary>
        public BoundResponse(string PayloadIn, byte[] TicketIn,
                     Goedel.Cryptography.Ticket.Cryptography.Authentication Authentication,
                     Goedel.Cryptography.Ticket.Cryptography.Key Key)
            : base(PayloadIn, TicketIn, Authentication, Key) { }

        /// <summary></summary>
        public BoundResponse(int ErrorCode, string Explanation) {
            Payload = Explanation;
            Status = ErrorCode;
            }

        /// <summary></summary>
        public override string HTTP () {
            string Header = String.Format (HeaderFormat, Status, StatusDescription, Payload.Length);
            string ContentIntegrity = "";
            if (Ticket != null) {
                ContentIntegrity = String.Format (ContentIntegrityFormat, Base64Mac, Base64Ticket);
                }

            return Header + ContentIntegrity + "\n" + Payload;
            }

        }


    }