using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GU=Goedel.Utilities;
using Goedel.Cryptography;
using Goedel.Cryptography.Framework;

namespace Goedel.Cryptography.Test {
    [TestClass]
    public partial class TestCryptography {

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

        }
    }
