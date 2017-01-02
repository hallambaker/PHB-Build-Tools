using System;
using System.Collections.Generic;
using System.Numerics;
using Goedel.Utilities;
using Goedel.Cryptography;
using Goedel.Cryptography.Jose;
using Goedel.IO;

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
            _Main.Test();
            }


        public _Main () {
            Debug.Initialize();
            Debug.WriteLine("Test Crypto");

            //Goedel.Platform.Framework.Platform.Initialize();
            //Goedel.Mesh.Platform.Windows.Platform.Initialize();

            Goedel.Cryptography.Framework.Cryptography.Initialize();
            }

        CryptoProviderExchange Encrypter;
        CryptoProviderSignature Signer;

        KeyPair EncrypterKeyPair;
        KeyPair SignerKeyPair;


        byte[] MakeKek (int Length) {
            var Result = new byte[Length];
            for (var i =0; i< Length; i++) {
                Result[i] = (byte) i;
                }
            return Result;
            }

        byte[] MakeKey(int Length) {
            var Result = new byte[Length];
            for (var i = 0; i < 16; i++) {
                Result[i] = (byte) (i*17);
                }
            for (var i = 16; i < Length; i++) {
                Result[i] = (byte)(i - 16);
                }
            return Result;
            }

        public void Test() {
            var Wrapper = new KeyWrapRFC3394();

            var Kek256 = MakeKek(32);
            var Key128 = MakeKey(16);
            var Key192 = MakeKey(24);
            var Key256 = MakeKey(32);

            var Wrapped = Wrapper.Wrap(Kek256, Key128);
            var Result = Wrapper.Unwrap(Kek256, Wrapped);

            Wrapped = Wrapper.Wrap(Kek256, Key192);
            Result = Wrapper.Unwrap(Kek256, Wrapped);

            Wrapped = Wrapper.Wrap(Kek256, Key256);
            Result = Wrapper.Unwrap(Kek256, Wrapped);

            //Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch);
            //Encrypter.Generate(KeySecurity.Ephemeral, KeySize:2048);
            //EncrypterKeyPair = Encrypter.KeyPair;

            //Signer = CryptoCatalog.Default.GetSignature(CryptoAlgorithmID.RSASign);
            //Signer.Generate(KeySecurity.Ephemeral, KeySize:2048);
            //SignerKeyPair = Signer.KeyPair;

            ////TestDigest();
            ////TestDigest512();
            ////TestSign();
            //TestJose();
            }

        string TestString = "This is a test";


        public void TestDigest() {
            var Digest = Platform.SHA2_256;
            var Result = Digest.Process(TestString);
            var Text = BaseConvert.ToBase16String(Result);
            }

        public void TestDigest512() {
            var Digest = Platform.SHA2_512;
            var Result = Digest.Process(TestString);
            var Text = BaseConvert.ToBase16String(Result);
            }

        public void TestSign () {

            var Result = Signer.Sign(TestString);
            }

        public void TestEncrypt() {
            
            
            var Result = Encrypter.Encrypt(TestString);
            }


        public void TestJose() {

            var JWE = new JoseWebEncryption(TestString, EncrypterKeyPair);
            var JWEText = JWE.ToString();
            var JWEProt = JWE.Protected.ToUTF8();
            Console.WriteLine(JWEText);
            Console.WriteLine(JWEProt);

            var Data = JWE.Decrypt(EncrypterKeyPair);
            var Text = Data.ToUTF8();


            var JWS = new JoseWebSignature(TestString, SignerKeyPair);
            var JWSText = JWS.ToString();

            Console.WriteLine(JWSText);
            foreach (var Signer in JWS.Signatures) {
                var JWSProt = Signer.Protected.ToUTF8();
                Console.WriteLine(JWSProt);
                }

            var Verify1 = JWS.Verify(SignerKeyPair);

            var JWES = new JoseWebEncryption(TestString, EncrypterKeyPair, SignerKeyPair);
            var JWESText = JWES.ToString();
            var JWESProt = JWES.Protected.ToUTF8();

            Console.WriteLine("Sign + encrypt");
            Console.WriteLine(JWESText);
            Console.WriteLine(JWESProt);
            foreach (var Signer in JWES.Signatures) {
                var JWSProt = Signer.Protected.ToUTF8();
                Console.WriteLine(JWSProt);
                }


            var Data2 = JWES.Decrypt(EncrypterKeyPair);
            var Text2 = Data2.ToUTF8();

            // Verification result, returns the UDF of the key that validated.

            var Verify2 = JWES.Verify(SignerKeyPair);

            }


        //public void TestSign2() {
        //    var Signer = CryptoCatalog.Default.GetSignature(CryptoAlgorithmID.RSASign2048,
        //            CryptoAlgorithmID.SHA_3_512);
        //    Signer.Generate(KeySecurity.Exportable);
        //    var Result = Signer.Sign(TestString);
        //    }


        //public void TestSign3() {
        //    var Signer = CryptoCatalog.Default.GetSignature(CryptoAlgorithmID.RSASign2048);
        //    Signer.Generate(KeySecurity.Exportable);

        //    var UDF = Signer.UDF;

        //    var CopyKey = KeyPair.FindLocal(UDF);
        //    var CopyProvider = CopyKey.SignatureProvider ();
        //    var Result = CopyProvider.Sign(TestString);
        //    }


        //public void TestEncrypt() {

        //    var Encryptor = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch2048,
        //        CryptoAlgorithmID.AES256CBC);
        //    Encryptor.Generate(KeySecurity.Exportable);

        //    var UDF = Encryptor.UDF;
        //    var Public = Encryptor.KeyPair;

        //    CryptoProviderExchange KeyExchange = Public.ExchangeProvider(CryptoAlgorithmID.AES256CBC);
        //    CryptoDataEncoder Wrap = KeyExchange.MakeEncoder();
        //    CryptoData Result = Wrap.Bulk.Process(TestString);

        //    }

        //public void TestEncrypt2() {

        //    var Encryptor = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch2048,
        //        CryptoAlgorithmID.AES256CBC);
        //    Encryptor.Generate(KeySecurity.Exportable);

        //    var UDF = Encryptor.UDF;
        //    var Public = Encryptor.KeyPair;


        //    CryptoProviderEncryption Bulk = Platform.AES_256.CryptoProviderEncryption();
        //    CryptoProviderExchange KeyExchange1 = Public.ExchangeProvider();
        //    CryptoDataEncoder Wrap1 = KeyExchange1.MakeEncoder(Bulk);
        //    CryptoProviderExchange KeyExchange2 = Public.ExchangeProvider();
        //    CryptoDataEncoder Wrap2 = KeyExchange2.MakeEncoder(Bulk);
        //    CryptoData Result = Bulk.Process(TestString);

        //    }

        //public void TestEncryptAuthenticate() {

        //    var Encryptor = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch2048,
        //        CryptoAlgorithmID.AES256CBC);
        //    Encryptor.Generate(KeySecurity.Exportable);

        //    var UDF = Encryptor.UDF;
        //    var Public = Encryptor.KeyPair;


        //    var Bulk = CryptoCatalog.Default.GetEncryption(CryptoAlgorithmID.ModeGCM);
        //    CryptoProviderExchange KeyExchange1 = Public.ExchangeProvider();
        //    CryptoDataEncoder Wrap1 = KeyExchange1.MakeEncoder(Bulk);
        //    CryptoProviderExchange KeyExchange2 = Public.ExchangeProvider();
        //    CryptoDataEncoder Wrap2 = KeyExchange2.MakeEncoder(Bulk);
        //    CryptoData Result = Bulk.Process(TestString);
        //    }


        //public void TestEncryptMulti() {

        //    var Encryptor = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch2048,
        //        CryptoAlgorithmID.AES256CBC);
        //    Encryptor.Generate(KeySecurity.Exportable);

        //    var UDF = Encryptor.UDF;
        //    var Public = Encryptor.KeyPair;


        //    var KeyExchange = Public.ExchangeProvider(CryptoAlgorithmID.AES256CBC);
        //    CryptoDataEncoder Wrap = KeyExchange.MakeEncoder();
        //    CryptoDataEncoder WrapWrap1 = Wrap.Bulk.MakeEncoder();
        //    CryptoData Result1 = WrapWrap1.Bulk.Process(TestString);
        //    CryptoDataEncoder WrapWrap2 = Wrap.Bulk.MakeEncoder();
        //    CryptoData Result2 = WrapWrap2.Bulk.Process(TestString);

        //    }

        //public void TestEncryptMultiMulti() {

        //    var Encryptor = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID.RSAExch2048,
        //        CryptoAlgorithmID.AES256CBC);
        //    Encryptor.Generate(KeySecurity.Exportable);

        //    var UDF = Encryptor.UDF;
        //    var Public = Encryptor.KeyPair;


        //    CryptoProviderBulk Bulk = CryptoCatalog.Default.GetEncryption(CryptoAlgorithmID.ModeGCM);
        //    CryptoProviderExchange KeyExchange1 = Public.ExchangeProvider();
        //    CryptoDataEncoder Wrap1 = KeyExchange1.MakeEncoder(Bulk);
        //    CryptoProviderExchange KeyExchange2 = Public.ExchangeProvider();
        //    CryptoDataEncoder Wrap2 = KeyExchange2.MakeEncoder(Bulk);

        //    CryptoDataEncoder WrapWrap1 = Bulk.MakeEncoder();
        //    CryptoData Result1 = WrapWrap1.Bulk.Process(TestString);
        //    CryptoDataEncoder WrapWrap2 = Bulk.MakeEncoder();
        //    CryptoData Result2 = WrapWrap2.Bulk.Process(TestString);

        //    }



        public void TestRSA () {
            
            var CryptoProvider = CryptoCatalog.Default.GetAsymmetric(CryptoAlgorithmID.RSASign);
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