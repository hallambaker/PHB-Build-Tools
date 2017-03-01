using System;
using System.Numerics;
using Goedel.Utilities;

namespace Goedel.Cryptography {
    /// <summary>
    /// Convert a BigInteger to a Bitfield and return successive bits
    /// beginning with the most significant and ending with the least.
    /// </summary>
    public struct BitIndex {

        byte[] BitField;

        int Byte;
        int Bit;
        byte Data;

        /// <summary>
        /// Returns true if there is further work to be completed, otherwise false.
        /// </summary>
        public bool More {
            get {
                return (Byte < 1) | (Bit < 0);
                }
            }

        /// <summary>
        /// Construct from a big integte
        /// </summary>
        /// <param name="Value">The bit field value</param>
        /// <param name="Bits">The number of bits to process</param>
        public BitIndex(BigInteger Value, int Bits) {
            Bits--;
            Byte = Bits / 8;
            Bit = Bits % 8;
            BitField = Value.ToByteArray();
            Data = BitField[Byte];
            }

        /// <summary>
        /// Return the value of the next bit as boolean value and advance the indicies
        /// </summary>
        /// <returns>True iff the next bit to be read is 1.</returns>
        public bool Next() {
            if (Bit < 0) {
                Byte--;
                Bit = 7;
                Assert.True(Byte >= 0, InvalidOperation.Throw);

                Data = BitField[Byte];
                }
            var Result = (Data & 0x80) > 0;
            Data = (byte)(Data << 1);
            Bit--;

            return Result;
            }
        }


    /// <summary>
    /// Extension methods for manipulating BigIntegers
    /// </summary>
    public static class BigNumber {

        /// <summary>
        /// Duplicate the values in the array
        /// </summary>
        /// <param name="Source">The source array</param>
        /// <returns>A new array containing a copy of the elements in the source.</returns>
        public static byte[] Duplicate (this byte[] Source) {
            var Result = new byte[Source.Length];
            Array.Copy(Source, Result, Source.Length);
            return Result;
            }

        /// <summary>
        /// Duplicate the values in the array
        /// </summary>
        /// <param name="Source">The source array</param>
        /// <param name="Index">The starting index for the copy</param>
        /// <param name="Length">The number of items to copy</param>
        /// <returns>A new array containing a copy of a selected range of the elements in the source.</returns>
        public static byte[] Duplicate(this byte[] Source, int Index, int Length) {
            var Result = new byte[Length];
            Array.Copy(Source, Index, Result, 0, Source.Length);
            return Result;
            }


        /// <summary>
        /// Convert an array of bytes in little endian format to a Big Integer
        /// </summary>
        /// <param name="Data">The data in little endian format.</param>
        /// <returns>The constructed integer</returns>
        public static BigInteger BigIntegerLittleEndian (this byte[] Data) {
            if ((Data[Data.Length - 1] >> 7) == 0) {
                return new BigInteger(Data);
                }
            var Extend = new byte[Data.Length + 1];
            Array.Copy(Data, Extend, Data.Length);
            return new BigInteger(Extend);
            }

        /// <summary>
        /// Convert an array of bytes in big endian format to a Big Integer
        /// </summary>
        /// <param name="Data">The data in big endian format.</param>
        /// <returns>The constructed integer</returns>
        public static BigInteger BigIntegerBigEndian(this byte[] Data) {
            byte[] Extend;

            if ((Data[0] >> 7) == 0) {
                Extend = new byte[Data.Length];
                Array.Copy(Data, Extend, Data.Length);
                }
            else {
                Extend = new byte[Data.Length+1];
                Array.Copy(Data, 0, Extend, 1, Data.Length);
                }
            Array.Reverse(Extend);
            return new BigInteger(Extend);
            }



        /// <summary>
        /// Create a Big Integer from a hexadecimal string constant. This is not optimized for
        /// speed since it is unlikely this will be called very often and may well 
        /// be optimized away. Note that the caller is responsible for making sure
        /// that the input is positive
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static BigInteger HexToBigInteger(this string Text) {
            var Bytes = BaseConvert.FromBase16String(Text);
            Array.Reverse(Bytes);
            return new BigInteger(Bytes);
            }

        /// <summary>
        /// Create a Big Integer from a decimal string constant. This is not optimized for
        /// speed since it is unlikely this will be called very often and may well 
        /// be optimized away. Note that the caller is responsible for making sure
        /// that the input is positive
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static BigInteger DecimalToBigInteger(this string Text) {
            BigInteger Result;
            BigInteger.TryParse(Text, out Result);
            return Result;
            }

        /// <summary>
        /// Calculate the modular inverse of a number using the x(p-2) approach
        /// </summary>
        /// <param name="x">Value</param>
        /// <param name="p">Modulus</param>
        /// <returns>The modular inverse, i.e. the number y such that 
        /// (x * y) mod p = 1.</returns>
        public static BigInteger ModularInverse(this BigInteger x, BigInteger p) {
            return BigInteger.ModPow(x, p - 2, p);
            }

        /// <summary>
        /// Calculate the modular inverse of a number using the x(p-2) approach
        /// </summary>
        /// <param name="x">Value</param>
        /// <param name="p">Modulus</param>
        /// <returns>The modular inverse, i.e. the number y such that 
        /// (x * y) mod p = 1.</returns>
        public static BigInteger ModularInverse(this int x, BigInteger p) {
            return ModularInverse((BigInteger)x, p);
            }

        /// <summary>
        /// Calculate the modulus of a number with correct handling for negative numbers.
        /// </summary>
        /// <param name="x">Value</param>
        /// <param name="p">Modulus</param>
        /// <returns>x mod p</returns>
        public static BigInteger Mod(this BigInteger x, BigInteger p) {
            var Result = x % p;
            return Result.Sign > 0 ? Result : Result + p;
            }

        /// <summary>
        /// Calculate the square root of -1 modulo p
        /// </summary>
        /// <param name="p">The modulus</param>
        /// <returns>A value x such that x*x mod p = -1 mod p</returns>
        public static BigInteger SqrtMinus1(this BigInteger p) {
            return BigInteger.ModPow(2, (p - 1) / 4, p);
            }


        /// <summary>
        /// Return a Square root of a number modulo the prime. Note that the
        /// </summary>
        /// <param name="x2">The value</param>
        /// <param name="p">The modulus</param>
        /// <param name="SqrtMinus1">The value of the square root -1 mod p.</param>
        /// <param name="Odd">If specified, specifies whether X is odd (true) or even (false).</param>
        /// <returns>A value x such that x*x = x2.</returns>
        /// <exception cref="InvalidOperation">Thrown if the value does not have a root.</exception>
        public static BigInteger Sqrt(this BigInteger x2, BigInteger p,
                            BigInteger? SqrtMinus1 = null,
                            bool? Odd = null) {
            var x = BigInteger.ModPow(x2, (p + 3) / 8, p);
            if (((x * x - x2) % p) != 0) {
                var RM1 = SqrtMinus1 ?? p.SqrtMinus1();
                x = (x * RM1) % p;
                Assert.True((((x * x - x2) % p) == 0), InvalidOperation.Throw);
                }
            if (Odd == null) {
                return x;
                }
            return (x.IsEven ^ (bool)Odd) ? x : p - x;
            }

        /// <summary>
        /// Calculate the modulus of a number with correct handling for negative numbers.
        /// </summary>
        /// <param name="x">Value</param>
        /// <param name="p">Modulus</param>
        /// <returns>x mod p</returns>
        public static BigInteger Mod(this int x, BigInteger p) {
            return Mod((BigInteger)x, p);
            }



        }
    }
