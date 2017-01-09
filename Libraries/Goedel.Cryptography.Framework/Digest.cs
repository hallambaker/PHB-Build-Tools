//   Copyright © 2015 by Comodo Group Inc.
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Goedel.Utilities;

namespace Goedel.Cryptography.Framework {

    /// <summary>
    /// <para>Base class for all cryptographic hash providers.</para>
    /// 
    /// <para>Provides utility and convenience functions that are employed in derived
    /// classes. This provides consistency when using either the built in .NET
    /// providers or those from other sources.</para>
    /// 
    /// <para>Unlike the .NET API, the wrapper provider completely conceals the details 
    /// of the cryptographic algorithm implementation. It is not necessary to 
    /// observe block boundaries when using the TransformData methods.</para>
    /// </summary>
    public abstract class CryptoProviderDigest : 
                Goedel.Cryptography.CryptoProviderDigest {


        /// <summary>
        /// Hash algorithm provider.
        /// </summary>
        public HashAlgorithm HashAlgorithm;
        

        /// <summary>
        /// Initializes an instance of the hash provider with the specified
        /// implementation.
        /// </summary>
        /// <param name="HashAlgorithm">Digest algorithm to construct from</param>
        protected CryptoProviderDigest(HashAlgorithm HashAlgorithm) {
            this.HashAlgorithm = HashAlgorithm;
            }


        /// <summary>
        /// Register this set of providers to the specified catalog.
        /// </summary>
        /// <param name="Catalog">Catalog to register the providers to</param>
        /// <returns>Registration for the preferred provider (SHA-2-512)</returns>
        public static CryptoAlgorithm Register(CryptoCatalog Catalog = null) {
            CryptoProviderSHA2_256.Register(Catalog);
            CryptoProviderSHA1.Register(Catalog);
            return CryptoProviderSHA2_512.Register(Catalog);
            }


        ///// <summary>
        ///// ASN.1 Object Identifier.
        ///// </summary>
        //public override string OID {
        //    get {
        //        return CryptoConfig.MapNameToOID(Name);
        //        }
        //    }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected CryptoProviderDigest() {
            }


        /// <param name="Algorithm">Ignored</param>
        /// <param name="Bulk">Ignored</param>
        /// <param name="OutputStream">Output stream. Data written to the input 
        /// stream is written to the output without modification. This permits
        /// multiple digest values to be calculated simultaneously.</param>
        /// <returns>Instance describing the key agreement parameters.</returns>
        public override CryptoDataEncoder MakeEncoder(
                            CryptoProviderBulk Bulk = null,
                            CryptoAlgorithmID Algorithm = CryptoAlgorithmID.Default,
                            Stream OutputStream = null
                            ) {


            var Result = new CryptoDataEncoder(Algorithm, this);
            Result.OutputStream = OutputStream ?? Stream.Null;
            BindEncoder(Result);

            return Result;
            }




        /// <summary>
        /// Create a crypto stream from this provider.
        /// </summary>
        /// <param name="Encoder"></param>
        public override void BindEncoder(CryptoDataEncoder Encoder) {
            Encoder.InputStream = new CryptoStream(
                    Encoder.OutputStream, HashAlgorithm, CryptoStreamMode.Write);
            }


        /// <summary>
        /// Processes the specified byte array
        /// </summary>
        /// <param name="Data">The input to process</param>
        /// <param name="Offset">Offset within array</param>
        /// <param name="Count">Number of bytes to process</param>
        /// <param name="Key">The key</param>
        /// <returns>The result of the cryptographic operation.</returns>
        public override byte[] ProcessData(byte[] Data, int Offset,
                            int Count, byte[] Key = null) {
            return HashAlgorithm.ComputeHash(Data, Offset, Count);
            }

        /// <summary>
        /// Complete processing at the end of an encoding or decoding operation
        /// </summary>
        /// <param name="CryptoData">Structure to write result to</param>
        public override void Complete(CryptoData CryptoData) {
            var CryptoStream = CryptoData.InputStream as CryptoStream;
            CryptoStream.FlushFinalBlock();
            CryptoData.Integrity = HashAlgorithm.Hash;

            return;
            }

        }



    class CryptoDataEncoderDigest : CryptoDataEncoder {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="Identifier">The Goedel Cryptography identifier.</param>
        /// <param name="Bulk">Provider to use to process the bulk data
        /// signature operations where the asymmetric operation is performed after the
        /// bulk operation completes. </param> 
        public CryptoDataEncoderDigest(CryptoAlgorithmID Identifier, 
                        CryptoProviderBulk Bulk) :
                            base (Identifier, Bulk){ 
            }

        /// <summary>
        /// Close the crypto stream and get the digest value.
        /// </summary>
        public override void Complete () {
            InputStream.Close();
            

            }
        }


    /// <summary>
    /// Provider for the SHA-2 256 bit Hash Algorithm
    /// </summary>
    public class CryptoProviderSHA2_256 : CryptoProviderDigest {


        static CryptoAlgorithmID _CryptoAlgorithmID = CryptoAlgorithmID.SHA_2_256;

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
            get { return _CryptoAlgorithm; }
            }


        static CryptoAlgorithm _CryptoAlgorithm = new CryptoAlgorithm(
                    _CryptoAlgorithmID, 512, _AlgorithmClass, Factory);


        /// <summary>
        /// Register this provider in the specified crypto catalog. A provider may 
        /// register itself multiple times to describe different configurations that 
        /// are supported.
        /// </summary>
        /// <param name="Catalog">The catalog to register the provider to, if
        /// null, the default catalog is used.</param>
        /// <returns>Description of the principal algorithm registration.</returns>
        public static new CryptoAlgorithm Register(CryptoCatalog Catalog = null) {
            Catalog = Catalog ?? CryptoCatalog.Default;
            return Catalog.Add(_CryptoAlgorithm);
            }

        /// <summary>
        /// Default output size.
        /// </summary>
        public override int Size {
            get {
                return 256;
                }
            }

        private static CryptoProvider Factory(int KeySize, CryptoAlgorithmID DigestAlgorithm) {
            return new CryptoProviderSHA2_256();
            }

        /// <summary>
        /// Create a SHA-2-256 digest provider.
        /// </summary>
        public CryptoProviderSHA2_256()
            : base(new SHA256Cng()) {
            }
        }

    /// <summary>
    /// Provider for the SHA-2 512 bit Hash Algorithm
    /// </summary>
    public class CryptoProviderSHA2_512 : CryptoProviderDigest {

        static CryptoAlgorithmID _CryptoAlgorithmID = CryptoAlgorithmID.SHA_2_512;

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
            get { return _CryptoAlgorithm; } }


        static CryptoAlgorithm _CryptoAlgorithm = new CryptoAlgorithm(
                    _CryptoAlgorithmID, 512, _AlgorithmClass, Factory);


        /// <summary>
        /// Register this provider in the specified crypto catalog. A provider may 
        /// register itself multiple times to describe different configurations that 
        /// are supported.
        /// </summary>
        /// <param name="Catalog">The catalog to register the provider to, if
        /// null, the default catalog is used.</param>
        /// <returns>Description of the principal algorithm registration.</returns>
        public static new CryptoAlgorithm Register(CryptoCatalog Catalog = null) {
            Catalog = Catalog ?? CryptoCatalog.Default;
            return Catalog.Add(_CryptoAlgorithm);
            }

        /// <summary>
        /// Default output size.
        /// </summary>
        public override int Size {
            get {
                return 512;
                }
            }

        private static CryptoProvider Factory(int KeySize, 
                            CryptoAlgorithmID Bulk=CryptoAlgorithmID.Default) {
            return new CryptoProviderSHA2_512();
            }
        
        /// <summary>
        /// Create a SHA-2-256 digest provider.
        /// </summary>
        public CryptoProviderSHA2_512() : base (new SHA512Cng()) {
            }
        }

    /// <summary>
    /// Provider for the SHA-1 Hash algorithm
    /// </summary>
    public class CryptoProviderSHA1 : CryptoProviderDigest {


        static CryptoAlgorithmID _CryptoAlgorithmID = CryptoAlgorithmID.SHA_1_DEPRECATED;

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
            get { return _CryptoAlgorithm; }
            }

        static CryptoAlgorithm _CryptoAlgorithm = new CryptoAlgorithm(
                    _CryptoAlgorithmID, 512, _AlgorithmClass, Factory);


        /// <summary>
        /// Register this provider in the specified crypto catalog. A provider may 
        /// register itself multiple times to describe different configurations that 
        /// are supported.
        /// </summary>
        /// <param name="Catalog">The catalog to register the provider to, if
        /// null, the default catalog is used.</param>
        /// <returns>Description of the principal algorithm registration.</returns>
        public static new CryptoAlgorithm Register(CryptoCatalog Catalog = null) {
            Catalog = Catalog ?? CryptoCatalog.Default;
            return Catalog.Add(_CryptoAlgorithm);
            }

        /// <summary>
        /// Default output size.
        /// </summary>
        public override int Size {
            get {
                return 160;
                }
            }


        private static CryptoProvider Factory(int KeySize, CryptoAlgorithmID DigestAlgorithm) {
            return new CryptoProviderSHA1();
            }
        /// <summary>
        /// Create a SHA-1 digest provider.
        /// </summary>
        public CryptoProviderSHA1() : base(new SHA1Cng()) {
            }
        }


    }
