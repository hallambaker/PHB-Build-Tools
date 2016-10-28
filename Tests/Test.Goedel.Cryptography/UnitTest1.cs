using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GU=Goedel.Utilities;
using Goedel.Cryptography;
using Goedel.Cryptography.Framework;

namespace Goedel.Cryptography.Test {
    [TestClass]
    public class TestCryptography {

        [AssemblyInitialize]
        public static void Initialize (TestContext Context) {
            Framework.Cryptography.Initialize();
            }


        [TestMethod]
        public void TestInitialize() {
            Framework.Cryptography.Initialize();
            }


        [TestMethod]
        public void TestRandom() {
            var Random1 = Platform.GetRandomBytes(10);
            var Random2 = Platform.GetRandomBigInteger(2048);
            }


        [TestMethod]
        public void TestDH1() {

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
        public void TestDH2() {

            var BobPrivate = new DiffeHellmanPrivate();
            var BobPublic = BobPrivate.DiffeHellmanPublic;

            DiffeHellmanPublic AlicePublic;
            var AliceKey = BobPublic.Agreement(out AlicePublic);

            var BobKey = BobPrivate.Agreement(AlicePublic);

            Assert.IsTrue(AliceKey.Equals(BobKey));
            Assert.IsTrue(BobKey.Equals(AliceKey));

            }

        }
    }
