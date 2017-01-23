using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Goedel.Cryptography;
using Goedel.Cryptography.Framework;
using Goedel.Cryptography.PKIX;
using Goedel.Utilities;

namespace Goedel.Cryptography.KeyFile {

    /// <summary>
    /// Recognized key file formats
    /// </summary>
    public enum KeyFileFormat {
        /// <summary>PEM private key file, used for many SSH implementations</summary>
        PEMPrivate,
        /// <summary>PEM public key file</summary>
        PEMPublic,
        /// <summary>PuTTY private key file</summary>
        PuTTY,
        /// <summary>OpenSSH native format</summary>
        OpenSSH
        }

    /// <summary>Extension methods</summary>
    /// <remarks>This currently hard wires to the .NET framework providers
    /// rather than the portable base classes.</remarks>
    public static class Extension {

        /// <summary>
        /// Convert key pair to PEM formatted string.
        /// </summary>
        /// <param name="KeyPair">A  Key pair</param>
        /// <returns>Key Pair in PEM format</returns>
        public static string ToPEM (this KeyPair KeyPair) {
            if (KeyPair as RSAKeyPair != null) {
                return ToPEM(KeyPair as RSAKeyPair);
                }

            return null;
            }

        /// <summary>
        /// Convert key pair to PEM formatted string.
        /// </summary>
        /// <param name="RSAKeyPair">An RSA Key pair</param>
        /// <returns>Key Pair in PEM format</returns>
        public static string ToPEM (RSAKeyPair RSAKeyPair) {

            var Provider = RSAKeyPair.Provider;
            Assert.NotNull(Provider, NoProviderSpecified.Throw);

            var RSAParameters = Provider.ExportParameters(true);
            Assert.NotNull(RSAParameters, PrivateKeyNotAvailable.Throw);

            var NewProvider = new RSACryptoServiceProvider();
            NewProvider.ImportParameters(RSAParameters);


            RSAParameters.Dump();

            var RSAPrivateKey = RSAParameters.RSAPrivateKey();

            var Builder = new StringBuilder();

            Builder.Append("-----BEGIN RSA PRIVATE KEY-----");
            var KeyDER = RSAPrivateKey.DER();
            Builder.AppendBase64(KeyDER);
            Builder.Append("\n-----END RSA PRIVATE KEY-----\n");

            return Builder.ToString();
            }

        /// <summary>
        /// Debug utility
        /// </summary>
        /// <param name="RSAParameters">RSA Parameters in /NET format</param>
        public static void Dump (this RSAParameters RSAParameters) {
            RSAParameters.Modulus.Dump("Modulus");
            RSAParameters.Exponent.Dump("Exponent");
            RSAParameters.P.Dump("P");
            RSAParameters.Q.Dump("Q");
            RSAParameters.D.Dump("D");
            RSAParameters.DP.Dump("DP");
            RSAParameters.DQ.Dump("DQ");
            RSAParameters.InverseQ.Dump("InverseQ");
            }

        /// <summary>
        /// Debug output utility
        /// </summary>
        /// <param name="Data">Data to print</param>
        /// <param name="Tag">Tag to prepend to data</param>
        public static void Dump (this byte[] Data, string Tag) {
            

            //Console.WriteLine("{0} : [{1}]  {2}.{3}.{4} ... {5}.{6}.{7}",
            //    Tag, Data.Length, Data[0], Data[1], Data[2],
            //        Data[Data.Length - 3],  Data[Data.Length - 2],  Data[Data.Length - 1]);

            }

        }
    }
