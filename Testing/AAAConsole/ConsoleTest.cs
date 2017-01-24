using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Goedel.Utilities;
using Goedel.IO;
using Goedel.Test;
using Goedel.Cryptography;

/// <summary>
/// 
/// </summary>
namespace PHB_Framework_Library1 {

    public static class Entry {
        static void Main () {
            Goedel.IO.Debug.Initialize();
            Goedel.Cryptography.Framework.Cryptography.Initialize();
            Goedel.FSR.Lexer.Trace = true;
            var Start = new Start();
            }
        }


    /// <summary>
    /// 
    /// </summary>
    public class Start {

        public Start() {
            CryptoAlgorithmID.RSAExch.Test_LifecycleMaster();

            }

        }

    }