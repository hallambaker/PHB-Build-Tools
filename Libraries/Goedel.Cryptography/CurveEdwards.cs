using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Goedel.ASN;
using Goedel.Utilities;
using Goedel.Cryptography.PKIX;

namespace Goedel.Cryptography {
    /// <summary>
    /// Edwards Curve [x^2 = (y^2 - 1) / (d y^2 + 1) (mod p)] for 2^255-19
    /// </summary>
    public abstract class CurveEdwards : Curve {

        /// <summary>The X coordinate</summary>
        public BigInteger X { get; set; }

        /// <summary>The Y coordinate</summary>
        public BigInteger Y { get; set; }

        /// <summary>The projected Z coordinate</summary>
        public BigInteger Z { get; set; }


        /// <summary>
        /// Add this point to a second point
        /// </summary>
        /// <param name="P2">Second point</param>
        /// <returns>The result of the addition.</returns>
        public abstract CurveEdwards Add(CurveEdwards P2);



        /// <summary>
        /// Multiply this point by a scalar
        /// </summary>
        /// <param name="S">Scalar factor</param>
        /// <param name="Neutral">The neutral point on the curve.</param>
        /// <returns>The result of the multiplication</returns>
        protected Curve Multiply(BigInteger S, Curve Neutral) {
            var Q = Neutral as CurveEdwards;
            Assert.NotNull(Q, InvalidOperation.Throw);
            var BitIndex = new BitIndex(S, Domain.Bits);

            while (BitIndex.More) {
                Q.Double();
                if (BitIndex.Next()) {
                    Q.Accumulate(this);
                    }
                }

            return Q;
            }

        /// <summary>Test to see if two points on a curve are equal</summary>
        /// <param name="Q"></param>
        /// <returns></returns>
        public bool Equal(CurveEdwards Q) {
            Assert.True(Domain.p == Q.Domain.p, InvalidOperation.Throw);

            if (((X * Q.Z) - (Q.X * Z)).Mod(Domain.p) != 0) {
                return false;
                }
            if (((Y * Q.Z) - (Q.Y * Z)).Mod(Domain.p) != 0) {
                return false;
                }
            return true;
            }


        /// <summary>
        /// Recover the X coordinate from the Y value and sign of X.
        /// </summary>
        /// <param name="X0">If true X is odd, otherwise, X is even.</param>
        /// <returns></returns>
        public BigInteger RecoverX(bool X0) {
            Assert.True(Y < Domain.p, InvalidOperation.Throw);
            var x2 = (Y * Y - 1) * (Domain.d * Y * Y + 1).ModularInverse(Domain.p);
            return x2.Sqrt(Domain.p, Domain.SqrtMinus1, X0);
            }


        /// <summary>
        /// Replace the current point value with the current value added to itself
        /// (used to implement multiply)
        /// </summary>
        public abstract void Double();

        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <returns>The result of the addition.</returns>
        public abstract void Accumulate(CurveEdwards Point);

        }


    /// <summary>
    /// Edwards Curve [x^2 = (y^2 - 1) / (d y^2 + 1) (mod p)] for 2^255-19
    /// </summary>
    public class CurveEdwards25519 : CurveEdwards {

        /// <summary>
        /// Additional parameter used in affine projection
        /// </summary>
        public BigInteger T { get; set; }


        /// <summary>The domain parameters</summary>
        public override DomainParameters Domain { get; } = DomainParameters.Curve25519;

        /// <summary>The base point for the subgroup</summary>
        public static CurveEdwards25519 Base;

        /// <summary>The point P such that P + Q = Q for all Q</summary>
        public static CurveEdwards25519 Neutral;

        static CurveEdwards25519() {
            Base = new CurveEdwards25519(DomainParameters.Curve25519.By, false);
            Neutral = new CurveEdwards25519() { X = 0, Y = 1, Z = 1, T = 0 };
            }

        CurveEdwards25519() {
            }

        /// <summary>
        /// Construct a point from a Y coordinate and sign.
        /// </summary>
        /// <param name="Y">The Y coordinate</param>
        /// <param name="X0">The sign of X</param>
        public CurveEdwards25519(BigInteger Y, bool X0) {
            this.Y = Y;
            this.Z = 1;
            X = RecoverX(X0);
            T = (X * Y) % Domain.p;
            }


        /// <summary>
        /// Multiply this point by a scalar
        /// </summary>
        /// <param name="S">Scalar factor</param>
        /// <returns>The result of the multiplication</returns>
        public override Curve Multiply(BigInteger S) {
            return Multiply(S, Neutral);
            }

        /// <summary>
        /// Replace the current point value with the current value added to itself
        /// (used to implement multiply)
        /// </summary>
        public override void Double() {
            var A = X * X;
            var B = Y * Y;
            var C = 2 * Z * Z;
            var H = A + B;
            var S = (X + Y);
            var E = H - S * S;
            var G = A - B;
            var F = C + G;

            X = (E * F) % Domain.p;
            Y = (G * H) % Domain.p;
            T = (E * H) % Domain.p;
            Z = (F * G) % Domain.p;
            }



        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <param name="X3"></param>
        /// <param name="Y3"></param>
        /// <param name="Z3"></param>
        /// <param name="T3"></param>
        /// <returns>The result of the addition.</returns>
        void Add(CurveEdwards Point,
                    out BigInteger X3, out BigInteger Y3, out BigInteger Z3, out BigInteger T3) {
            var P2 = Point as CurveEdwards25519;
            Assert.NotNull(P2, NYI.Throw);

            var A = (Y - X) * (P2.Y - P2.X);
            var B = (Y + X) * (P2.Y + P2.X);
            var C = T * 2 * Domain.d * P2.T;
            var D = Z * 2 * P2.Z;
            var E = B - A;
            var F = D - C;
            var G = D + C;
            var H = B + A;
            X3 = (E * F) % Domain.p;
            Y3 = (G * H) % Domain.p;
            T3 = (E * H) % Domain.p;
            Z3 = (F * G) % Domain.p;
            }

        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <returns>The result of the addition.</returns>
        public override CurveEdwards Add(CurveEdwards Point) {
            BigInteger X3, Y3, Z3, T3;
            Add(Point, out X3, out Y3, out Z3, out T3);
            return new CurveEdwards25519() { X = X3, Y = Y3, Z = Z3, T = T3 };
            }

        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <returns>The result of the addition.</returns>
        public override void Accumulate(CurveEdwards Point) {
            BigInteger X3, Y3, Z3, T3;
            Add(Point, out X3, out Y3, out Z3, out T3);
            X = X3;
            Y = Y3;
            Z = Z3;
            T = T3;
            }


        /// <summary>
        /// Generate a private key in encoded form
        /// </summary>
        /// <returns></returns>
        public byte[] Generate () {
            var Buffer = Platform.GetRandomBytes(32);

            return Buffer;
            }

        /// <summary>
        /// Calculate the hash value used to extend the private key and as
        /// an input to the signature function.
        /// </summary>
        /// <param name="Private"></param>
        /// <returns></returns>
        public byte[] KeyHash (byte[] Private) {
            var Buffer = Platform.SHA2_512.Process (Private);
            var Result = new byte[32];
            Array.Copy(Buffer, 32, Result, 0, 32); // copy lower 32 bytes to result.


            throw new NYI(); // NYI!

            }

        /// <summary>
        /// Create the extended private key. The Private key is extended using the
        /// hash value.
        /// </summary>
        /// <param name="Hash">The hash value</param>
        /// <returns></returns>
        public BigInteger ExtractPrivate (byte[]Hash) {
            var Copy = new byte[32];
            Array.Copy(Hash, Copy, 32); // bytes 0-31

            Copy[0] = (byte)(Copy[0] & 0xfb);
            Copy[31] = 0;
            Copy[30] = (byte)(Copy[30] | 0x80);
            return Copy.BigIntegerLittleEndian();
            }


        /// <summary>
        /// Generate the public parameter (a point on the curve)
        /// </summary>
        /// <param name="Private">The extended private key</param>
        /// <returns>The public key corresponding to Private (s.B)</returns>
        public static CurveEdwards25519 GetPublic (BigInteger Private) {
            return (CurveEdwards25519)Base.Multiply(Private);
            }

        /// <summary>
        /// Encode this point in the compressed buffer representation
        /// </summary>
        /// <returns></returns>
        public byte[] Encode () {
            var Buffer = Y.ToByteArray();
            if (!X.IsEven) {        // Encode the sign bit
                Buffer[31] = (byte)(Buffer[31] | 0x80);
                }
            return Buffer;
            }


        /// <summary>
        /// Construct a point on the curve from a buffer.
        /// </summary>
        /// <param name="Data">The encoded data</param>
        /// <returns>The point created</returns>
        public static CurveEdwards25519 Decode(byte[] Data) {
            if ((Data[31] & 0x80) == 0) {
                var Y0 = Data.BigIntegerLittleEndian();
                return new CurveEdwards25519(Y0, false);
                }
            var Copy = Data.Duplicate();
            var Y1 = Copy.BigIntegerLittleEndian();
            return new CurveEdwards25519(Y1, false);
            }


        BigInteger SHA512_ModQ  (byte[] A0, byte[] A1, byte[] A2, byte[] A3=null) {
            var SHA512 = Platform.SHA2_512.CryptoProviderDigest();
            if(A0!=null) {
                SHA512.ProcessData(A0);
                }
            if (A1 != null) {
                SHA512.ProcessData(A1);
                }
            if (A2 != null) {
                SHA512.ProcessData(A2);
                }
            if (A3 != null) {
                SHA512.ProcessData(A3);
                }
            var CryptoData = new CryptoDataEncoder(CryptoAlgorithmID.Default,SHA512);
            SHA512.Complete(CryptoData);

            var Digest = CryptoData.Integrity;
            var Result = Digest.BigIntegerLittleEndian();

            Result = Result % q;

            return Result;
            }

        BigInteger q = BigInteger.Pow(2, 252) + "27742317777372353535851937790883648493".DecimalToBigInteger();

        /// <summary>
        /// Sign a message using the public key according to RFC8032
        /// </summary>
        /// <remarks>This method does not prehash the message data since if
        /// prehashing is desired, it is because the data needs to be hashed
        /// before being presented.</remarks>
        /// <param name="Private">The private key</param>
        /// <param name="Message">The message</param>
        /// <param name="Context">Context value, if used.</param>
        /// <returns>The encoded signature data</returns>
        public byte[] Sign (byte[] Private, byte[] Message, byte[] Context = null) {
            var Buffer = Platform.SHA2_512.Process(Private);

            var a = ExtractPrivate(Buffer); // uses bytes 0-31

            var Prefix = new byte[32];
            Array.Copy(Buffer, 32, Prefix, 0, 32); // uses bytes 32-63 for prefix.

            var A = GetPublic(a).Encode();

            var r = SHA512_ModQ(Context, Prefix, Message);
            var R = (CurveEdwards25519) Base.Multiply(r);
            var Rs = R.Encode();

            var h = SHA512_ModQ(Context, Rs, A, Message);
            var s = (r + h * a) % q;
     
            var Bs = s.ToByteArray();

            var Result = new byte[64];
            Array.Copy(Rs, Result, 32);
            Array.Copy(Bs, 0, Result, 32, 32);

            return Result;
            }

        /// <summary>
        /// Verify a signature on a message according to RFC8032.
        /// </summary>
        /// <remarks>This method does not prehash the message data since if
        /// prehashing is desired, it is because the data needs to be hashed
        /// before being presented.</remarks>
        /// <param name="Public">The public key</param>
        /// <param name="Message">The message data.</param>
        /// <param name="Signature">The encoded signature data.</param>
        /// <param name="Context">Context value, if used.</param>
        /// <returns>True if signature verification succeeded, otherwise false.</returns>
        public bool Verify (byte[] Public, byte[] Message, byte[] Signature, byte[] Context = null) {
            Assert.True(Public.Length == 32, InvalidOperation.Throw);
            Assert.True(Signature.Length == 64, InvalidOperation.Throw);

            var A = Decode(Public);

            var Rs = Signature.Duplicate(0, 32);
            var R = Decode(Rs);

            var Bs = Signature.Duplicate(32, 32);
            var s = Bs.BigIntegerLittleEndian();

            if (s >q) {
                return false;
                }

            var h = SHA512_ModQ(Context, Rs, Public, Message);

            var sB = (CurveEdwards25519) Base.Multiply(s);
            var hA = (CurveEdwards25519)A.Multiply(h);

            return sB.Equal(hA);
            }
        }




    /// <summary>
    /// Edwards Curve [x^2 = (y^2 - 1) / (d y^2 + 1) (mod p)] for 2^255-19
    /// </summary>
    public class CurveEdwards448 : CurveEdwards {

        /// <summary>The domain parameters</summary>
        public override DomainParameters Domain { get; } = DomainParameters.Curve448;


        /// <summary>The base point for the subgroup</summary>
        public static CurveEdwards448 Base;

        /// <summary>The point P such that P + Q = Q for all Q</summary>
        public static CurveEdwards448 Neutral;

        static CurveEdwards448() {
            Base = new CurveEdwards448(DomainParameters.Curve448.By, false);
            Neutral = new CurveEdwards448() { X = 0, Y = 1, Z = 1 };
            }

        private CurveEdwards448() {
            }

        /// <summary>
        /// Construct a point from a Y coordinate and sign.
        /// </summary>
        /// <param name="Y">The Y coordinate</param>
        /// <param name="X0">The sign of X</param>
        public CurveEdwards448(BigInteger Y, bool X0) {
            this.Y = Y;
            this.Z = 1;
            X = RecoverX(X0);
            }


        /// <summary>
        /// Multiply this point by a scalar
        /// </summary>
        /// <param name="S">Scalar factor</param>
        /// <returns>The result of the multiplication</returns>
        public override Curve Multiply(BigInteger S) {
            return Multiply(S, Neutral);
            }


        /// <summary>
        /// Replace the current point value with the current value added to itself
        /// (used to implement multiply)
        /// </summary>
        public override void Double() {

            var B0 = (X + Y);
            var B = B0 * B0;
            var C = X * X;
            var D = Y * Y;
            var E = C + D;
            var H = Z * Z;
            var J = E - 2 * H;
            X = ((B - E) * J) % Domain.p;
            Y = (E * (C - D)) % Domain.p;
            Z = (E * J) % Domain.p;

            }



        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <param name="X3"></param>
        /// <param name="Y3"></param>
        /// <param name="Z3"></param>
        /// <returns>The result of the addition.</returns>
        private void Add(CurveEdwards Point, out BigInteger X3, out BigInteger Y3, out BigInteger Z3) {
            var P2 = Point as CurveEdwards448;
            Assert.NotNull(P2, NYI.Throw);

            var A = Z * P2.Z;
            var B = A * A;
            var C = X * P2.X;
            var D = Y * P2.Y;
            var E = Domain.d * C * D;
            var F = B - E;
            var G = B + E;
            var H = (X + Y) * (P2.X + P2.Y);
            X3 = A * F * (H - C - D) % Domain.p;
            Y3 = A * G * (D - C) % Domain.p;
            Z3 = F * G % Domain.p;
            }

        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <returns>The result of the addition.</returns>
        public override CurveEdwards Add(CurveEdwards Point) {
            BigInteger X3, Y3, Z3;
            Add(Point, out X3, out Y3, out Z3);

            var P2 = new CurveEdwards448() { X = X3, Y = Y3, Z = Z3 };
            return P2;
            }


        /// <summary>
        /// Add two points
        /// </summary>
        /// <param name="Point">Second point</param>
        /// <returns>The result of the addition.</returns>
        public override void Accumulate(CurveEdwards Point) {
            BigInteger X3, Y3, Z3;
            Add(Point, out X3, out Y3, out Z3);
            X = X3;
            Y = Y3;
            Z = Z3;
            }





        }



    }
