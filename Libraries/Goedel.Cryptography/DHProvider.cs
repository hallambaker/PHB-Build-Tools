using System;
using System.IO;
using System.Numerics;
using Goedel.Utilities;

namespace Goedel.Cryptography {

    /// <summary>
    /// The Diffie Hellman Recryption provider
    /// </summary>
    public class CryptoProviderExchangeDH : CryptoProviderRecryption {

        static CryptoAlgorithmID _CryptoAlgorithmID =
                    Goedel.Cryptography.CryptoAlgorithmID.DH;

        /// <summary>
        /// The CryptoAlgorithmID Identifier.
        /// </summary>
        public override CryptoAlgorithmID CryptoAlgorithmID {
            get { return _CryptoAlgorithmID; }
            }

        /// <summary>
        /// Return a CryptoAlgorithm structure with properties describing this provider.
        /// </summary>
        public override CryptoAlgorithm CryptoAlgorithm {
            get { return CryptoAlgorithmAny; }
            }

        static CryptoAlgorithm CryptoAlgorithmAny = new CryptoAlgorithm(
                    Goedel.Cryptography.CryptoAlgorithmID.DH, 2048, _AlgorithmClass, Factory);

        /// <summary>
        /// Register this provider in the specified crypto catalog. A provider may 
        /// register itself multiple times to describe different configurations that 
        /// are supported.
        /// </summary>
        /// <param name="Catalog">The catalog to register the provider to, if
        /// null, the default catalog is used.</param>
        /// <returns>Description of the principal algorithm registration.</returns>
        public static CryptoAlgorithm Register(CryptoCatalog Catalog = null) {
            Catalog = Catalog ?? CryptoCatalog.Default;
            return Catalog.Add(CryptoAlgorithmAny);
            }



        /// <summary>
        /// Default algorithm key or output size.
        /// </summary>
        public override int Size { get { return 2048; } }

        /// <summary>
        /// Delegate to create a cryptographic provider with optional key size and/or
        /// bulk algorithm variants where needed.
        /// </summary>
        /// <param name="KeySize">Key size parameter (if needed).</param>
        /// <param name="BulkAlgorithmID">Algorithm identifier of bulk algorithm (if needed).</param>
        /// <returns></returns>
        private static CryptoProvider Factory(int KeySize, CryptoAlgorithmID BulkAlgorithmID) {
            return new CryptoProviderExchangeDH(KeySize:KeySize);
            }


        /// <summary>
        /// The UDF fingerprint of the key.
        /// </summary>
        /// <remarks>
        /// Keys are stored in  %APPDATA%\AppData\Roaming\CryptoMesh\Keys\DH on windows AND
        /// ~\.Mesh\Keys\DH on U*ix based systems.
        /// </remarks>
        public override string UDF {
            get { return DHKeyPair.UDF; }
            }


        DHKeyPair DHKeyPair;

        /// <summary>
        /// Return the provider key.
        /// </summary>
        public override KeyPair KeyPair {
            get { return DHKeyPair; }
            set {
                var DHKeyPair = KeyPair as DHKeyPair;
                Assert.NotNull(DHKeyPair, InvalidKeyPairType.Throw, "DH keypair expected");
                this.DHKeyPair = DHKeyPair;
                }
            }

        /// <summary>
        /// Construct a provider for a Keypair
        /// </summary>
        /// <param name="KeyPair">Keypair to construct from</param>
        /// <param name="Bulk">Default encryption algorithm.</param>
        public CryptoProviderExchangeDH(KeyPair KeyPair, CryptoAlgorithmID Bulk) {
            this.KeyPair = KeyPair;
            this.BulkAlgorithmDefault = Bulk;
            }

        /// <summary>
        /// Construct a provider for a Keypair
        /// </summary>
        /// <param name="KeySecurity">Specifies the protection level for the key.</param>
        /// <param name="KeySize">The Key size</param>
        public CryptoProviderExchangeDH(KeySecurity KeySecurity= KeySecurity.Ephemeral,
                    int  KeySize=2048) {
            Generate(KeySecurity.Ephemeral, KeySize);
            }

        // From CryptoProviderAsymmetric

        /// <summary>
        /// Generates a new signing key pair with the default key size.
        /// </summary>
        /// <param name="KeySecurity">Specifies the protection level for the key.</param>
        /// <param name="KeySize">The Key size</param>
        public override void Generate(KeySecurity KeySecurity, int KeySize = 2046) {
            DHKeyPair = new DHKeyPair(KeySecurity, KeySize);
            }

        /// <summary>
        /// Locate the private key in the local key store.
        /// </summary>
        /// <param name="UDF">Fingerprint of key to locate.</param>
        /// <returns>True if private key exists.</returns>
        public override bool FindLocal(string UDF) {
            KeyPair = Platform.FindInKeyStore(UDF, CryptoAlgorithmID.DH);
            return KeyPair != null;
            }

        /// <summary>The maximum number of shares into which a key may be split</summary>
        public override int SharesMaximum {
            get { return 16; }
            }


        // --------------------------------------



        // From CryptoProviderAgree

        /// <summary>
        /// Perform a key wrap operation and return a CryptoDataWrapped instance
        /// containing the wrapped key parameters and a bulk provider. 
        /// </summary>
        /// <param name="Algorithm">The key wrap algorithm</param>
        /// <param name="Bulk">The bulk provider to use. If specified, the parameters from
        /// the specified provider will be used. Otherwise a new bulk provider will 
        /// be created and returned as part of the result.</param>
        /// <param name="OutputStream"></param>
        /// <returns>Instance describing the key agreement parameters.</returns>
        public override CryptoDataEncoder MakeEncoder(
                            CryptoProviderBulk Bulk = null,
                            CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default,
                            Stream OutputStream = null
                            ) {

            // Generate an ephemeral DH key and perform a Key agreement against it.

            DiffeHellmanPublic DiffeHellmanPublic;
            var Result = DHKeyPair.Agreement(out DiffeHellmanPublic);

            // NYI: Replace this and return the DH Result instance

            // NYI: DH MakeEncoder

            throw new NYI("To do");
            }

        /// <summary>
        /// Encrypt the bulk key.
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Algorithm">Composite encryption algorithm.</param>
        /// <param name="Wrap">If true create a new CryptoData instance that
        /// wraps the parameters supplied in Data.</param> 
        public override CryptoDataExchange Encrypt(CryptoData Data, 
            CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default, bool Wrap = false){

            // NYI: DH Encrypt
            throw new NYI("To do");
            }

        /// <summary>
        /// Perform a key exchange to encrypt a bulk or wrapped key under this one.
        /// </summary>
        /// <param name="EncryptedKey">The encrypted session</param>
        /// <param name="AlgorithmID">The algorithm to use.</param>
        /// <returns>The decoded data instance</returns>
        public override byte[] Decrypt(
                    byte[] EncryptedKey,
                    CryptoAlgorithmID AlgorithmID = CryptoAlgorithmID.Default) {

            // NYI: DH Decrypt
            throw new NYI("To do");
            }




        // From CryptoProviderRecryption

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
        public override KeyPair[] GenerateRecryptionSet(int Shares) {
            Assert.True(Shares <= SharesMaximum, RecryptionShareLimitExceeded.Throw);
            return DHKeyPair.GenerateRecryptionSet(Shares);
            }


        /// <summary>
        /// Perform a recryption operation on the input data. A recryption operation
        /// is any operation that is not a final decryption operation. Multiple 
        /// recryption operations may be performed in series.
        /// </summary>
        /// <param name="CryptoData"></param>
        /// <returns>The partially decrypted data</returns>
        public override CryptoDataEncoder Recrypt(CryptoDataEncoder CryptoData) {
            // NYI: DH CryptoDataEncoder
            throw new NYI("To do");

            //var TargetDH = Target as DHKeyPair;
            //Assert.NotNull(TargetDH, InvalidKeyPairType.Throw);

            //// Calculate the shared parameters

            //BigInteger Agreed;

            //if (Input.Recrypt == null) {
            //    Agreed = TargetDH.Agreement(DHKeyPair);
            //    }
            //else {
            //    var Carry = new BigInteger(Input.Recrypt);
            //    Agreed = TargetDH.Agreement(DHKeyPair, Carry);
            //    }
            }

        /// <summary>
        /// Perform a recryption operation on the input data. A recryption operation
        /// is any operation that is not a final decryption operation. When more 
        /// than two recryption keys are used, the 
        /// </summary>
        /// <param name="CryptoDatas"></param>
        /// <returns>The partially decrypted data</returns>
        public override CryptoDataEncoder Recrypt(CryptoDataEncoder[] CryptoDatas) {
            // NYI: DH CryptoDataEncoder
            throw new NYI("To do");
            }
        }
    }
