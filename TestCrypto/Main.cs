using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Goedel.Utilities;
using Goedel.Cryptography;
using Goedel.Cryptography.Jose;


/// <summary>
/// Test the cryptography routines
/// </summary>
namespace TestCrypto {

    /// <summary>
    /// Shell class
    /// </summary>
    public class _Main {

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="Args"></param>
        public static void Main (string [] Args) {

            var _Main = new _Main();
            _Main.TestDH();
            }


        public _Main () {
            //Goedel.Platform.Framework.Platform.Initialize();
            //Goedel.Mesh.Platform.Windows.Platform.Initialize();

            Goedel.Cryptography.Framework.Cryptography.Initialize();
            }


        public void TestRSA () {
            
            var CryptoProvider = CryptoCatalog.Default.GetAsymmetric(CryptoAlgorithmID.RSASign2048);
            CryptoProvider.Generate(KeySecurity.Exportable);

            var KeyPair = CryptoProvider.KeyPair;
            var PublicParameters = new PublicKeyRSA(KeyPair as RSAKeyPairBase);

            }


        public void TestDH() {
            
            var AliceKeyPair = new DiffeHellmanPrivate();
            var BobKeyPair = new DiffeHellmanPrivate();

            var AlicePublic = AliceKeyPair.DiffeHellmanPublic;
            var BobPublic = BobKeyPair.DiffeHellmanPublic;

            var AliceAgree = AliceKeyPair.Agreement(BobPublic);
            var BobAgree = BobKeyPair.Agreement(AlicePublic);


            if (AliceAgree!=BobAgree) {
                Console.WriteLine("Fail");
                }

            var GroupKeyPair = new DiffeHellmanPrivate();
            var GroupKeyPublic = GroupKeyPair.DiffeHellmanPublic;

            var BobSplit = GroupKeyPair.MakeRecryption(2);
            var BobRecryption = BobSplit[0];
            var BobDecryption = BobSplit[1];

            var AliceAgreeW = AliceKeyPair.Agreement(GroupKeyPublic);

            //var ServerAgreeW = BobRecryption.Agreement(AlicePublic);
            //var BobAgreeW = BobDecryption.Agreement(AlicePublic, ServerAgreeW);


            //if (AliceAgreeW != BobAgreeW) {
            //    Console.WriteLine("Fail");
            //    }

       
            var ServerRecrypt = BobRecryption.Agreement(AlicePublic);
            var BobRecrypt = BobDecryption.Agreement(AlicePublic);

            var Recrypts = new BigInteger[] { ServerRecrypt, BobRecrypt };
            var BobAgreeW = BobDecryption.Agreement(Recrypts);

            }

        }

    }