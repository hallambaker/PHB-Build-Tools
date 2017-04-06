﻿using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Goedel.Cryptography;
using Goedel.Cryptography.Framework;
using Goedel.Cryptography.KeyFile;
using Goedel.IO;
using Goedel.Test;


namespace Test.Goedel.Cryptography.Jose {


    [TestClass]
    public partial class TestCryptography {

        static CryptoProviderExchange Encrypter;
        static CryptoProviderSignature Signer;

        static RSAKeyPairBase EncrypterKeyPair;
        static RSAKeyPairBase SignerKeyPair;

        static DHKeyPair AliceKeyPair;
        static DHKeyPair BobKeyPair;
        static DHKeyPair GroupKeyPair;

        [AssemblyInitialize]
        public static void Initialize(TestContext Context) {
            global::Goedel.IO.Debug.Initialize();
            CryptographyWindows.Initialize();

            SignerKeyPair = (RSAKeyPairBase)KeyFileDecode.DecodePEM(Directories.TestKey_OpenSSH_Private);
            Signer = SignerKeyPair.SignatureProvider();

            EncrypterKeyPair = (RSAKeyPairBase)KeyFileDecode.DecodePEM(Directories.TestKey_OpenSSH_Private);
            Encrypter = EncrypterKeyPair.ExchangeProvider();
            
            AliceKeyPair = new DHKeyPair();
            BobKeyPair = new DHKeyPair();
            GroupKeyPair = new DHKeyPair();

            }


        }
    }
