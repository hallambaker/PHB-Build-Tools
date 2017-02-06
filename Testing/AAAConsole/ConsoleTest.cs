using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using Goedel.Utilities;
using Goedel.IO;
using Goedel.Test;
using Goedel.Cryptography;
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
            var Start = new Start();
            }
        }


    /// <summary>
    /// 
    /// </summary>
    public class Start {

        public Start() {
            var Keys = KeyContainer.GetKeyContainerNames();
            var Prefix = Container.PrefixTest;
            foreach (var Key in Keys) {
                if (Key.StartsWith(Prefix)) {
                    Delete(Key);
                    }
                }


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