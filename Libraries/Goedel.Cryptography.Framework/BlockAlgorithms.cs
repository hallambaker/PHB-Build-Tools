using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Goedel.Cryptography;

namespace Goedel.Cryptography.Framework {

    /// <summary>
    /// AES block encryption/decryption transform.
    /// </summary>
    public class AesBlock : BlockProvider {
        static Aes Provider;
        ICryptoTransform Transform;

        /// <summary>
        /// Return the block size in bits.
        /// </summary>
        public override int BlockSize { get { return Provider.BlockSize; } }

        static AesBlock () {
            Provider = new AesManaged();
            Provider.Mode = CipherMode.ECB;
            Provider.Padding = PaddingMode.None;
            }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Encrypt"></param>
        public AesBlock(byte[] Key, bool Encrypt) {
            Transform = Encrypt ? Provider.CreateEncryptor(Key, null) : Provider.CreateDecryptor(Key, null);
            }

        /// <summary>
        /// Factory method
        /// </summary>
        /// <param name="Key">The key to initialize the method</param>
        /// <param name="Encrypt">If true, create an encryptor, if false, create a decryptor.</param>
        /// <returns></returns>
        public static BlockProvider Factory(byte[] Key, bool Encrypt) {
            return new AesBlock(Key, Encrypt);
            }

        /// <summary>
        /// Encrypt or decrypt a single block of data under the specified key
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="InputOffset"></param>
        /// <param name="Output"></param>
        /// <param name="OutputOffset"></param>
        public override void Process (byte [] Input, int InputOffset, byte[] Output, int OutputOffset) {
            Transform.TransformBlock(Input, InputOffset, Transform.InputBlockSize, Output, OutputOffset);
            }


        }
    }
