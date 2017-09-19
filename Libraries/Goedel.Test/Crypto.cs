﻿using System;
using System.Collections.Generic;
using UT = Microsoft.VisualStudio.TestTools.UnitTesting;
using Goedel.Cryptography;
using Goedel.Cryptography.PKIX;
using Goedel.Utilities;

namespace Goedel.Test {


    public static class Crypto {

        static Crypto() {
            }

        public const string TestString = "This is a test";

        public static void Test_EncryptDecrypt(this KeyPair KeyPair, string Test = TestString) {
            var Encrypter = KeyPair.ExchangeProvider();
            Encrypter.Test_EncryptDecrypt(Test);
            }

        public static void Test_EncryptDecrypt(this CryptoProviderExchange Encrypter, string Test = TestString) {
            var Result = Encrypter.Encrypt(Test);

            UT.Assert.IsTrue(Result.Exchanges.Count == 1);
            UT.Assert.IsTrue(Result.Signatures == null | Result.Signatures?.Count == 0);

            CheckDecrypt(Encrypter, Test, Result);
            }


        static void CheckDecrypt(CryptoProviderExchange Provider, string Expected, CryptoDataEncoder Result) {
            foreach (var Decrypt in Result.Exchanges) {
                CheckDecrypt(Provider, Expected, Result.BulkID, Result.ProcessedData, Result.IV,
                    Decrypt.Exchange, Decrypt.Ephemeral);
                }
            }

        static void CheckDecrypt(CryptoProviderExchange Provider, string Expected, CryptoAlgorithmID Bulk,
                    byte[] CipherText, byte[] IV, byte[] Exchange, KeyPair Ephemeral) {

            var Key = Provider.Decrypt(Exchange, Ephemeral);
            var BulkProvider = CryptoCatalog.Default.GetEncryption(Bulk);
            var Plaintext = BulkProvider.Decrypt(CipherText, Key, IV);

            UT.Assert.IsTrue(Expected == Plaintext.ToUTF8());
            }





        public static void Test_LifecycleMaster(this CryptoAlgorithmID CryptoAlgorithmID) {
            var Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID);
            Encrypter.Generate(KeySecurity.Master, KeySize: 2048);
            Encrypter.Test_EncryptDecrypt();
            var UDF = Encrypter.UDF;

            ExportPrivate(UDF, true);
            Persist(UDF, true);


            }

        public static void Test_LifecycleAdmin(this CryptoAlgorithmID CryptoAlgorithmID) {
            var Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID);
            Encrypter.Generate(KeySecurity.Admin, KeySize: 2048);
            Encrypter.Test_EncryptDecrypt();
            var UDF = Encrypter.UDF;

            ExportPrivate(UDF, false);
            Persist(UDF, true);
            }



        public static void Test_LifecycleDevice(this CryptoAlgorithmID CryptoAlgorithmID) {
            var Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID);
            Encrypter.Generate(KeySecurity.Device, KeySize: 2048);
            Encrypter.Test_EncryptDecrypt();
            var UDF = Encrypter.UDF;

            ExportPrivate(UDF, false);
            Persist(UDF, true);
            }


        /// <summary>Test for lifecycle of ephemeral key. Key can be created and used but FindLocal
        /// fails as the key is never written to the local store</summary>
        /// <param name="CryptoAlgorithmID"></param>
        public static void Test_LifecycleEphemeral(this CryptoAlgorithmID CryptoAlgorithmID) {
            var Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID);
            Encrypter.Generate(KeySecurity.Ephemeral, KeySize: 2048);
            Encrypter.Test_EncryptDecrypt();
            var UDF = Encrypter.UDF;

            Persist(UDF, false);

            IPKIXPrivateKey Private = null;
            try {
                Private = Encrypter.KeyPair.PKIXPrivateKey;
                UT.Assert.Fail();
                }
            catch (NotExportable) {
                UT.Assert.IsNull(Private);
                }

            
            }

        /// <summary>Test for lifecycle of ephemeral key. Key can be created and used but FindLocal
        /// fails as the key is never written to the local store</summary>
        /// <param name="CryptoAlgorithmID"></param>
        public static void Test_LifecycleExportable(this CryptoAlgorithmID CryptoAlgorithmID) {
            var Encrypter = CryptoCatalog.Default.GetExchange(CryptoAlgorithmID);
            Encrypter.Generate(KeySecurity.Exportable, KeySize: 2048);
            Encrypter.Test_EncryptDecrypt();
            var UDF = Encrypter.UDF;

            Persist(UDF, false);
            var Private = Encrypter.KeyPair.PKIXPrivateKey;
            UT.Assert.IsNotNull(Private);
            }



        /// <summary>
        /// Check persistence of the key, that it can be found in the local store and
        /// then that it cannot be found after deletion.
        /// </summary>
        /// <param name="UDF"></param>
        static void Persist(string UDF, bool Succeed) {
            var Encrypter2 = KeyPair.FindLocal(UDF);

            if (!Succeed) {
                UT.Assert.IsNull(Encrypter2);
                return; // No more testing possible
                }

            UT.Assert.IsNotNull(Encrypter2);
            Encrypter2.Test_EncryptDecrypt();

            Encrypter2.EraseFromDevice();

            var Encrypter3 = KeyPair.FindLocal(UDF);
            UT.Assert.IsNull(Encrypter3);
            }


        static void ExportPrivate(string UDF, bool Succeed) {
            try {
                var Encrypter2 = KeyPair.FindLocal(UDF);
                var Private = Encrypter2.PKIXPrivateKey;
                UT.Assert.IsTrue(Succeed);
                }

            catch (NotExportable) {
                UT.Assert.IsFalse(Succeed);
                }
            }


        }
    }
