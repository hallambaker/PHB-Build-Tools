using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Cryptography {

    /// <summary>
    /// Base provider for public key encryption and symmetric key wrap.
    /// </summary>
    public abstract class CryptoProviderRecryption : CryptoProviderExchange {


        ///// <summary>
        ///// Base provider for public key encryption and symmetric key wrap.
        ///// 
        ///// NB these classes do not support bulk encryption.
        ///// </summary>
        //public abstract class CryptoProviderAgree : CryptoProviderAsymmetric {
        //    /// <summary>
        //    /// The type of algorithm
        //    /// </summary>
        //    public override CryptoAlgorithmClass AlgorithmClass { get { return CryptoAlgorithmClass.Exchange; } }

        //    /// <summary>
        //    /// JSON Key use.
        //    /// </summary>
        //    public override string JSONKeyUse { get { return "enc"; } }

            ///// <summary>
            ///// Encrypt key data.
            ///// </summary>
            ///// <param name="Input">The key data to encrypt.</param>
            ///// <returns>Encrypted data</returns>
            //public abstract CryptoData Encrypt(CryptoData Input, KeyPair Target);

            ///// <summary>
            ///// Decrypt data. Note that this is only possibly when the corresponding private
            ///// key is available on the local machine.
            ///// </summary>
            ///// <param name="Input">The data to decrypt.</param>
            ///// <returns>Decrypted data.</returns>
            //public abstract CryptoData Decrypt(CryptoData Input);
            //}




        /// <summary>
        /// The maximum number of key shares that the provider will generate.
        /// </summary>
        public abstract int SharesMaximum { get; }


        /// <summary>
        /// Split the private key into a recryption pair. This is a convenience function
        /// to support the most common use case in an implementation.
        /// <para>
        /// Since the
        /// typical use case for recryption requires both parts of the generated machine
        /// to be used on a machine that is not the machine on which they are created, the
        /// key security level is always to permit export.</para>
        /// </summary>
        /// <param name="Recryption">The private key for use by the recryption provider.</param>
        /// <param name="Completion">The private key to be used to complete the decryption
        /// operation.</param>
        public virtual void GenerateRecryptionPair(out KeyPair Recryption, out KeyPair Completion) {
            var Keys = GenerateRecryptionSet(2);
            Recryption = Keys[0];
            Completion = Keys[1];
            }

        /// <summary>
        /// Split the private key into a number of recryption keys.
        /// <para>
        /// Since the
        /// typical use case for recryption requires both parts of the generated machine
        /// to be used on a machine that is not the machine on which they are created, the
        /// key security level is always to permit export.</para>
        /// </summary>
        /// <param name="Shares">The number of keys to create.</param>
        /// <returns>The created keys</returns>
        public abstract KeyPair[] GenerateRecryptionSet(int Shares);

        /// <summary>
        /// Perform a recryption operation on the input data. A recryption operation
        /// is any operation that is not a final decryption operation. When more 
        /// than two recryption keys are used, the 
        /// </summary>
        /// <param name="CryptoData"></param>
        /// <returns>The partially decrypted data</returns>
        public abstract CryptoDataEncoder Recrypt(CryptoDataEncoder CryptoData);


        /// <summary>
        /// Perform a recryption operation on the input data. A recryption operation
        /// is any operation that is not a final decryption operation. When more 
        /// than two recryption keys are used, the 
        /// </summary>
        /// <param name="CryptoDatas"></param>
        /// <returns>The partially decrypted data</returns>
        public abstract CryptoDataEncoder Recrypt(CryptoDataEncoder[] CryptoDatas);

        }

    }
