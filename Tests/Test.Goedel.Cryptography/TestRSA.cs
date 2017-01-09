using System.Numerics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UT=Microsoft.VisualStudio.TestTools.UnitTesting;

using Goedel.Utilities;
using Goedel.Test;
using Goedel.Cryptography;
//using Goedel.Cryptography.Framework;

namespace Goedel.Cryptography.Test {
    public partial class TestCryptography {



        /// <summary>
        /// Delete all test keys
        /// </summary>

        [TestCleanupAttribute]
        public void Cleanup() {
            // Test: Delete all test keys
            }



        /// <summary>
        /// Test sign and verify.
        /// </summary>

        [TestMethod]
        public void TestRSA_Sign() {
            // Test: 
            }

        /// <summary>
        /// Test Encrypt and decrypt
        /// </summary>

        [TestMethod]
        public void TestRSA_Encrypt() {
            // Test: 
            }


        /// <summary>
        /// Test Key generation
        /// </summary>

        [TestMethod]
        public void TestRSA_KeyGen() {
            // Test: 
            }

        /// <summary>
        /// Test read key from PKIX parameter blob
        /// </summary>
        [TestMethod]
        public void TestRSA_ReadKey() {
            // Test: 
            }

        /// <summary>
        /// Test generate 
        /// </summary>
        [TestMethod]
        public void TestRSA_DeleteKey() {
            // Test: 
            }

        /// <summary>
        /// Test attempt to export machine bound key fails
        /// </summary>
        [TestMethod]
        public void TestRSA_BoundKey() {
            // Test: 
            }

        [TestMethod]
        public void TestRSA_PersistKey() {
            // Test: 
            }

        [TestMethod]
        public void TestRSA_EphemeralKey() {
            // Test: 
            }



        }
    }
