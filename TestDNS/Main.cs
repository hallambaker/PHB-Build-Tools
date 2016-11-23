using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Goedel.Utilities;
using Goedel.IO;
using Goedel.Platform;
using Goedel.Platform.Framework;

/// <summary>
/// 
/// </summary>
namespace TestDNS {

    /// <summary>
    /// 
    /// </summary>
    public class Entry {

        public static void Main () {
            Debug.Initialize();
            Debug.WriteLine("Test DNS");





            //Register DNS Client with the portable libraries
            Framework.Initialize();

            var Service = DNSClient.ResolveService("prismproof.org",
                "mmm", Fallback: DNSFallback.Prefix);

            var Next = Service.Next;

            var HTTP = Next.HTTPEndpoint;
            var Domain = Next.Address;

            }


        public static void TCPListen () {
            var server = new TcpListener(IPAddress.Any, 80);

            server.Start();
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");

            Byte[] bytes = new Byte[256];
            NetworkStream stream = client.GetStream();

            int i;
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) {
                // Translate data bytes to a ASCII string.
                string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);


                }

            }
        }
    }