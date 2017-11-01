using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Security.Cryptography;
using System.Collections.Generic;
using Goedel.Utilities;


namespace Goedel.Photo {

    /// <summary>
    /// 
    /// </summary>
    public class OAuthHeaders {

        SortedDictionary<string, string> Parameters = new SortedDictionary<string, string>();

        void SetValue (string Tag, string Value) {
            Parameters.Remove(Tag);
            Parameters.Add(Tag, Value);
            }

        //public bool Compare (string V1, string v2) {
        //    return V1 > V1;
        //    }

        public string Callback {
            set => SetValue("oauth_callback", value);
            }

        public string ConsumerKey {
            set => SetValue("oauth_consumer_key", value);
            }

        public string BaseURI { get; set; }


        public string ClientSharedSecret { get; set; } = null;
        public string TokenSharedSecret { get; set; } = null;

        public OAuthHeaders () {
            Random Random = new Random();

            var UnixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var Nonce = Random.Next();

            SetValue("oauth_timestamp", UnixTimestamp.ToString());
            SetValue("oauth_nonce", Nonce.ToString());
            SetValue("oauth_version", "1.0");
            SetValue("oauth_signature_method", "HMAC-SHA1");
            }

        public string GetURI () {
            var Builder = new StringBuilder();

            Builder.Append(BaseURI);
            Builder.Append("?");
            var First = true;
            foreach (var KVP in Parameters) {
                if (!First) {
                    Builder.Append("&");
                    }
                First = false;
                Builder.Append(KVP.Key);
                Builder.Append("=");
                Builder.Append(KVP.Value);
                }

            Builder.Append("&oauth_signature");
            Builder.Append("=");
            Builder.Append(GetSignature());

            return Builder.ToString();
            }

        public string GetBaseString () {
            var Builder = new StringBuilder();
            Builder.Append(BaseURI);
            Builder.Append("?");
            var First = true;
            foreach (var KVP in Parameters) {
                if (!First) {
                    Builder.Append("&");
                    }
                First = false;
                Builder.Append(KVP.Key);
                Builder.Append("=");
                Builder.Append(KVP.Value);
                }

            return Builder.ToString();
            }


        public string GetSignature () {

            

            var BaseString = GetBaseString();

            var BaseStringEncoded = "GET&" + HttpUtility.UrlEncode(BaseString);

            Console.WriteLine(BaseStringEncoded);


            var Builder = new StringBuilder();
            if (ClientSharedSecret != null) {
                Builder.Append(HttpUtility.UrlEncode(ClientSharedSecret));
                }
            //Builder.Append("&");
            if (TokenSharedSecret != null) {
                Builder.Append(HttpUtility.UrlEncode(TokenSharedSecret));
                }


            Console.WriteLine(Builder.ToString());

            var Key = Builder.ToString().ToBytes();
            var Provider = new HMACSHA1(Key);
            var SignatureBytes = Provider.ComputeHash(BaseStringEncoded.ToBytes());
            var SignatureBytesBase64 = SignatureBytes.ToBase64String(false);
            return HttpUtility.UrlEncode(SignatureBytesBase64);
            }


        public void GetResponse () {
            var WebClient = new WebClient();

            var Stream = WebClient.OpenRead(GetURI());
            string Result;
            using (var Reader = new StreamReader(Stream)) {
                Result = Reader.ReadToEnd();
                }

            }

        }
    }
