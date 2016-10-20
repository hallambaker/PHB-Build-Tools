using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Goedel.Cryptography;


namespace Goedel.Cryptography.Framework {
    /// <summary>
    /// Initialize the cryptographic framework
    /// </summary>
    public static class Cryptography {

        /// <summary>
        /// Perform initialization of the Goedel.Cryptography portable class
        /// with delegates to the .NET framework methods.
        /// </summary>
        public static void Initialize() {
            // This is actually a duplicate of Goedel.Platform but it is 
            // needed so often as to make this the easiest solution.
            Goedel.Cryptography.Platform.GetRandomBytesDelegate = GetRandomBytes;

            // Load thje default algorithms first

            Platform.SHA2_512       = CryptoCatalog.Default.Add(new CryptoProviderSHA2_512());
            Platform.HMAC_SHA2_512  = CryptoCatalog.Default.Add(new CryptoProviderHMACSHA2_512());
            Platform.AES_256        = CryptoCatalog.Default.Add(new CryptoProviderEncryptAES(256));
            CryptoCatalog.Default.Add(new CryptoProviderSignatureRSA(2048));
            CryptoCatalog.Default.Add(new CryptoProviderExchangeRSA(2048));

            // The rest
            Platform.SHA2_256       = CryptoCatalog.Default.Add(new CryptoProviderSHA2_256());
            Platform.SHA1           = CryptoCatalog.Default.Add(new CryptoProviderSHA1());
            Platform.HMAC_SHA2_256  = CryptoCatalog.Default.Add(new CryptoProviderHMACSHA2_256());
            CryptoCatalog.Default.Add(new CryptoProviderEncryptAES(128));

            //Add(new CryptoProviderEncryptAES(128, CipherMode.CTS));
            //Add(new CryptoProviderEncryptAES(256, CipherMode.CTS));

            CryptoCatalog.Default.Add(new CryptoProviderSignatureRSA(4096));
            CryptoCatalog.Default.Add(new CryptoProviderExchangeRSA(4096));
            CryptoCatalog.Default.Add(new CryptoProviderExchangeRSAPKCS(2048));
            CryptoCatalog.Default.Add(new CryptoProviderExchangeRSAPKCS(4096));


            Platform.FindLocalDelegates.Add(RSAKeyPair.FindLocal);

            }


        /// <summary>
        /// Cryptographic random number generator.
        /// </summary>
        private static RNGCryptoServiceProvider RNGCryptoServiceProvider =
            new RNGCryptoServiceProvider();



        /// <summary>
        /// Fill a byte array with cryptographically strong random data.
        /// </summary>
        /// <param name="Data">The array to fill with cryptographically strong random bytes.</param>
        /// <param name="Offset">The index of the array to start the fill operation.</param>
        /// <param name="Count">The number of bytes to fill</param>
        public static void GetRandomBytes(byte[] Data, int Offset, int Count) {
            RNGCryptoServiceProvider.GetBytes(Data, Offset, Count);
            }


        }
    }
