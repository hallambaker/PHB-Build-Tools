﻿using System.Numerics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UT = Microsoft.VisualStudio.TestTools.UnitTesting;

using Goedel.Utilities;
using Goedel.Test;
using Goedel.Cryptography;
//using Goedel.Cryptography.Framework;

namespace Test.Goedel.Cryptography {
    public partial class TestGoedelCryptography {

        List<TestVectorWrap> TestVectorsAESWrap = new List<TestVectorWrap>() {
            // 128 Key with 128 KEK
            new TestVectorWrap() {
                KEK = "000102030405060708090A0B0C0D0E0F".FromBase16String(),
                Key = "00112233445566778899AABBCCDDEEFF".FromBase16String(),
                Ciphertext = "1FA68B0A8112B447 AEF34BD8FB5A7B82 9D3E862371D2CFE5".FromBase16String(),
                },
            // 128 Key with 192 KEK
            new TestVectorWrap() {
                KEK = "000102030405060708090A0B0C0D0E0F1011121314151617".FromBase16String(),
                Key = "00112233445566778899AABBCCDDEEFF".FromBase16String(),
                Ciphertext = "96778B25AE6CA435 F92B5B97C050AED2 468AB8A17AD84E5D".FromBase16String(),
                },
                        // 128 Key with 256 KEK
            new TestVectorWrap() {
                KEK = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F".FromBase16String(),
                Key = "00112233445566778899AABBCCDDEEFF".FromBase16String(),
                Ciphertext = "64E8C3F9CE0F5BA2 63E9777905818A2A 93C8191E7D6E8AE7".FromBase16String(),
                },
                        // 192 Key with 129 KEK
            new TestVectorWrap() {
                KEK = "000102030405060708090A0B0C0D0E0F1011121314151617".FromBase16String(),
                Key = "00112233445566778899AABBCCDDEEFF0001020304050607".FromBase16String(),
                Ciphertext = ("031D33264E15D332 68F24EC260743EDC"+
                        "E1C6C7DDEE725A93 6BA814915C6762D2").FromBase16String(),
                },
                        // 129 Key with 256 KEK
            new TestVectorWrap() {
                KEK = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F".FromBase16String(),
                Key = "00112233445566778899AABBCCDDEEFF0001020304050607".FromBase16String(),
                Ciphertext = ("A8F9BC1612C68B3F F6E6F4FBE30E71E4" +
                            "769C8B80A32CB895 8CD5D17D6B254DA1").FromBase16String(),
                },
                        // 256 Key with 256 KEK
            new TestVectorWrap() {
                KEK = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F".FromBase16String(),
                Key = "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F".FromBase16String(),
                Ciphertext = ("28C9F404C4B810F4 CBCCB35CFB87F826 3F5786E2D80ED326"
                            + "CBC7F0E71A99F43B FB988B9B7A02DD21").FromBase16String(),
                },
            };

        [TestMethod]
        public void Test_Wrap_3394() {
            foreach (var TestVector in TestVectorsAESWrap) {
                var KeyWrap = new KeyWrapRFC3394();

                TestVector.Verify(KeyWrap);
                }
            }
        }


    public class TestVectorWrap {
        CryptoAlgorithmID ID { get; set; }

        public byte[] KEK { get; set; }

        public byte[] Key { get; set; }

        public byte[] Ciphertext { get; set; }


        public void Verify(KeyWrap KeyWrap) {
            var Wrapped = KeyWrap.Wrap(KEK, Key);
            var Unwrapped = KeyWrap.Unwrap(KEK, Wrapped);

            UT.Assert.IsTrue(Wrapped.IsEqualTo(Ciphertext));
            UT.Assert.IsTrue(Unwrapped.IsEqualTo(Key));
            }


        }
    }