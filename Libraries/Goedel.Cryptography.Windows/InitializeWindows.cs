using System;
using System.Threading;
using Security.Cryptography;
using Goedel.Utilities;
using Goedel.Cryptography;


/// <summary>
/// 
/// </summary>
namespace Goedel.Cryptography.Windows {

    /// <summary>
    /// 
    /// </summary>
    public static class Windows {

        static bool Initialized = false;
        static Mutex InitializationLock = new Mutex();

        /// <summary>
        /// Perform initialization of the Goedel.Cryptography portable class
        /// with delegates to the .NET framework methods.
        /// </summary>
        public static void Initialize() {
            InitializationLock.WaitOne();

            Framework.Cryptography.Initialize();
            try {
                if (Initialized) {
                    return;
                    }
                Initialized = true;

                Platform.FindInKeyStore = KeyStore.FindInKeyStore;
                Platform.WriteToKeyStore = KeyStore.WriteToKeyStore;
                Platform.EraseFromKeyStore = KeyStore.EraseFromDevice;
                }
            catch {
                throw new InitializationFailed();
                }
            finally {
                InitializationLock.ReleaseMutex();
                }

            }
        }

    }