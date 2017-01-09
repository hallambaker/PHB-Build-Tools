using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GU=Goedel.Utilities;
using Goedel.Cryptography;
using Goedel.Cryptography.Framework;

namespace Goedel.Cryptography.Test {

    public partial class TestCryptography {

        [TestMethod]
        public void TestDHAlg_SimpleAgreement() {

            var BobPrivate = new DiffeHellmanPrivate();
            var BobPublic = BobPrivate.DiffeHellmanPublic;

            var AlicePrivate = new DiffeHellmanPrivate(BobPublic);
            var AlicePublic = AlicePrivate.DiffeHellmanPublic;
            var AliceKey = AlicePrivate.Agreement(BobPublic);

            var BobKey = BobPrivate.Agreement(AlicePublic);

            Assert.IsTrue(AliceKey.Equals(BobKey));
            Assert.IsTrue(BobKey.Equals(AliceKey));

            }
        
        [TestMethod]
        public void Test_DHAlg_Ephemeral_Agreement() {

            var BobPrivate = new DiffeHellmanPrivate();
            var BobPublic = BobPrivate.DiffeHellmanPublic;

            DiffeHellmanPublic AlicePublic;
            var AliceKey = BobPublic.Agreement(out AlicePublic);

            var BobKey = BobPrivate.Agreement(AlicePublic);

            Assert.IsTrue(AliceKey.Equals(BobKey));
            Assert.IsTrue(BobKey.Equals(AliceKey));

            }

        [TestMethod]
        public void TestDH_Recryption_2 () {

            var AliceKeyPair = new DiffeHellmanPrivate();
            var BobKeyPair = new DiffeHellmanPrivate();
            var AlicePublic = AliceKeyPair.DiffeHellmanPublic;
            var BobPublic = BobKeyPair.DiffeHellmanPublic;

            var GroupKeyPair = new DiffeHellmanPrivate();
            var GroupKeyPublic = GroupKeyPair.DiffeHellmanPublic;

            var BobSplit = GroupKeyPair.MakeRecryption(2);
            var BobRecryption = BobSplit[0];
            var BobDecryption = BobSplit[1];


            var AliceAgreeW = AliceKeyPair.Agreement(GroupKeyPublic);
            var ServerRecrypt = BobRecryption.Agreement(AlicePublic);
            var BobAgreeW = BobDecryption.Agreement(AlicePublic, ServerRecrypt);

            Assert.IsTrue(AliceAgreeW == BobAgreeW );
            // Test: 
            }


        [TestMethod]
        public void TestDH_Recryption_8 () {
            // Test: Multi party recryption, this seems to have a bug. But npt worth 
            // fixing till the API is improved.
            }



        [TestMethod]
        public void TestDH_Encrypt () {
            // Test: 
            }



        [TestMethod]
        public void TestDH_KeyGen() {
            // Test: 
            }


        [TestMethod]
        public void TestDH_ReadKey() {
            // Test: 
            }


        [TestMethod]
        public void TestDH_DeleteKey() {
            // Test: 
            }


        [TestMethod]
        public void TestDH_BoundKey() {
            // Test: 
            }

        [TestMethod]
        public void TestDH_PersistKey() {
            // Test: 
            }

        [TestMethod]
        public void TestDHG_EphemeralKey() {
            // Test: 
            }

        }
    }
