using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using Goedel.Utilities;
using System.IO;
using Goedel.Test;
using Goedel.Cryptography;
using Goedel.Cryptography.Jose;
using Goedel.Cryptography.Framework;
using Goedel.Cryptography.Windows;

/// <summary>
/// 
/// </summary>
namespace PHB_Framework_Library1 {

    public static class Entry {
        static void Main () {
            Goedel.IO.Debug.Initialize();
            CryptographyFramework.Initialize();
            Goedel.FSR.Lexer.Trace = true;
            var Start = new Start2();
            }
        }

    /// <summary>
    /// 
    /// </summary>
    public class Start2 {

        ///<summary></summary>
        public Start2() {

            var Key = Platform.GetRandomBits(256);
            var Plaintext = "This is a bigly test".ToUTF8();
            var EncryptID = CryptoAlgorithmID.AES256CBC;




            var EncryptedData = new JoseWebEncryption(Plaintext, Key, EncryptID: EncryptID);

            var Decrypt = EncryptedData.Decrypt(Key);

            }
        }

    /// <summary>
    /// 
    /// </summary>
    public class Start {

        ///<summary></summary>
        public Start() {
            var c1 = new CurveEdwards448(new BigInteger(1), false);

            var Curve448BaseY = (
    "298819210078481492676017930443930673437544040154080242095928241" +
    "372331506189835876003536878655418784733982303233503462500531545062" +
    "832660").DecimalToBigInteger();
            var b2 = new CurveEdwards448(Curve448BaseY, false);


            b2.Double();




            var Base = CurveEdwards448.Base.Copy();
            var Neutral = CurveEdwards448.Neutral.Copy();


            var Base2 = CurveEdwards448.Base.Copy();
            Base2.Accumulate(Base);
            Assert.True(b2.Y0 == Base2.Y0);

            var Curve1 = Base.Multiply(CurveEdwards448.q);
            var Curve2 = Base.Multiply(CurveEdwards448.q - 1);



            var KeyA = new CurveEdwards448Private();

            var KeyAPublic = KeyA.Public;
            var KeyAPrivate = KeyA.Private;


            var KeyACurve = KeyAPublic.Public.Copy();






            var Pub2 = Base.Multiply(KeyAPrivate);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            Pub2 = Base.Multiply(KeyAPrivate);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            var Pub3 = Base.Multiply(KeyAPrivate - 1);
            Pub3.Accumulate(CurveEdwards448.Base);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y0 == KeyAPublic.Public.Y0);
            Assert.True(Pub2.Y0 == Pub3.Y0);


            var KeyB = new CurveEdwards448Private();
            var KeyBPublic = KeyB.Public;
            var KeyBPrivate = KeyB.Private;

            Pub2 = KeyBPublic.Public.Multiply(KeyAPrivate);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            Pub3 = KeyBPublic.Public.Multiply(KeyAPrivate - 1);
            Pub3.Accumulate(KeyBPublic.Public);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);
            Assert.True(Pub2.Y0 == Pub3.Y0);

            var AgreeAB = KeyA.Agreement(KeyB.Public);
            var AgreeBA = KeyB.Agreement(KeyA.Public);

            Assert.True(AgreeAB.Y0 == AgreeBA.Y0);



            var Recrypt1 = Platform.GetRandomBigInteger(KeyAPrivate);
            var Recrypt2 = KeyAPrivate - Recrypt1;

            var Pub4a = KeyBPublic.Public.Multiply(Recrypt1);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            var Pub4b = KeyBPublic.Public.Multiply(Recrypt2);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            Pub4a.Accumulate(Pub4b);
            Assert.True(Pub2.Y0 == Pub4a.Y0);



            var RecryptKeys = KeyA.GenerateRecryptionSet(2);
            var Test = (KeyA.Private - RecryptKeys[0].Private - RecryptKeys[1].Private);
            var Test2 = Test.Mod(CurveEdwards448.q);

            var Pub5a = KeyBPublic.Public.Multiply(RecryptKeys[0].Private);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            var Pub5b = KeyBPublic.Public.Multiply(RecryptKeys[1].Private);
            Assert.True(Base.Y == CurveEdwards448.Base.Y);
            Assert.True(Neutral.Y == CurveEdwards448.Neutral.Y);
            Assert.True(KeyACurve.Y == KeyAPublic.Public.Y);

            Pub5a.Accumulate(Pub5b);
            Assert.True(Pub2.Y0 == Pub5a.Y0);


            //          181709681073901722637330951972001133588410340171829515070372549795146003961539585716195755291692375963310293709091662304773755859649779
            //{         181709681073901722637330951972001133588410340171829515070372549795146003961539585716195755291692375963310293709091662304773755859649779}

            ////var RecryptKeys = KeyA.GenerateRecryptionSet(2);


            ////var Test = (KeyA.Private - RecryptKeys[0].Private - RecryptKeys[1].Private);

            ////var Tot1 = CurveEdwards448.Base.Multiply(KeyA.Private);
            ////var Tot2 = CurveEdwards448.Base.Multiply(RecryptKeys[0].Private);
            ////var Tot3 = CurveEdwards448.Base.Multiply(RecryptKeys[0].Private);
            ////var Tot4 = Tot2.Add(Tot3);

            ////var Result = KeyAPublic.Agreement();

            ////CurveEdwards25519[] Carry = new CurveEdwards448[2];
            ////Carry[0] = RecryptKeys[0].Agreement(Result.Public);
            ////Carry[1] = RecryptKeys[1].Agreement(Result.Public);

            ////var AgreeAB = KeyAPublic.Agreement(Carry);


            ////var ResultValue = Result.Agreement.Encode();
            ////var AgreeEncode = AgreeAB.Encode();


            }


        public void Delete (string Key) {
            try {
                var CSP = new CspParameters();
                CSP.KeyContainerName = Key;
                var RSA = new RSACryptoServiceProvider(CSP);
                RSA.PersistKeyInCsp = false;
                }
            catch {

                }
            }


        }

    }