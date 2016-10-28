using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

// Code taken from https://github.com/softwx/SoftWx.Numerics
//The MIT License(MIT)

//Copyright(c) 2015 SoftWx, Inc.

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace Goedel.Cryptography {
    /// <summary>
    /// Miller Rabin primality test class.
    /// </summary>
    public static class MillerRabin {
        // constants for common referenced BigInteger values
        private static BigInteger two = 2;
        private static BigInteger three = 3;

        /// <summary>
        /// Determines if the specified value is a prime number, deterministically for
        /// values 64 bits and less, and probabilistically for values larger than 64 bits.
        /// </summary>
        /// <remarks>This method chooses the algorithm to use based on
        /// the magnitude of the specified value. For smaller values, a
        /// simple trial division algorithm is used. For larger values up to
        /// and including 64 bit values, a deterministic version of the 
        /// Miller-Rabin algorithm is used. For values more than 64 bits,
        /// a probabilistic Miller-Rabin algorithm with a default of 64
        /// witness iterations is used.</remarks>
        /// <param name="value">The value to be tested for primality.</param>
        /// <param name="maxWitnessCount">The maximum number of witness iterations. Default is 64.</param>
        /// <returns>True if the value is prime, otherwise, false.</returns>
        public static bool IsPrime(this BigInteger value, int maxWitnessCount = 64) {
            if (value < two) return false;
            //if (value <= ulong.MaxValue) return ((ulong)value).IsPrime();
            return value.IsPrimeMR(maxWitnessCount);
            }

        /// <summary>
        /// Determines if the specified value is a prime number, using the
        /// probabilistic Miller-Rabin algorithm.
        /// </summary>
        /// <param name="value">The value to be tested for primality.</param>
        /// <param name="maxWitnessCount">The maximum number of witness iterations.</param>
        /// <returns>True if the value is probably prime, otherwise, false.</returns> 
        public static bool IsPrimeMR(this BigInteger value, int maxWitnessCount = 64) {
            // take care of the simple cases of small primes and the
            // common composites having those primes as factors
            if (value <= BigInteger.One) return false;
            if ((value % two) == BigInteger.Zero) return value == two;
            if ((value % three) == BigInteger.Zero) return value == three;
            if ((value % 5) == BigInteger.Zero) return value == 5;
            if (((value % 7) == BigInteger.Zero) || ((value % 11) == BigInteger.Zero)
                || ((value % 13) == BigInteger.Zero) || ((value % 17) == BigInteger.Zero)
                || ((value % 19) == BigInteger.Zero) || ((value % 23) == BigInteger.Zero)
                || ((value % 29) == BigInteger.Zero) || ((value % 31) == BigInteger.Zero)
                || ((value % 37) == BigInteger.Zero) || ((value % 41) == BigInteger.Zero)
                || ((value % 43) == BigInteger.Zero)) {
                return (value <= 43);
                }
            return InternalIsPrimeMR(value, maxWitnessCount, new ulong[0]);
            }

        /// <summary>
        /// Determines if the specified odd, >= 3 value is a prime number, using the
        /// Miller-Rabin algorithm.
        /// </summary>
        /// <param name="value">The value to be tested for primality.</param>
        /// <param name="witnessCount">The number of witnesses to be tested.</param>
        /// <param name="witnesses">The witnesses to be used for the first n tests.</param>
        /// <returns>True if the value is prime, otherwise, false.</returns>
        internal static bool InternalIsPrimeMR(BigInteger value, int witnessCount, params ulong[] witnesses) {
            // compute n − 1 as (2^s)·d (where d is odd)
            BigInteger valLessOne = value - BigInteger.One;
            BigInteger d = valLessOne / two; // we know that value is odd and valLessOne is even, so unroll 1st iter of loop
            uint s = 1;
            while ((d % two) == BigInteger.Zero) {
                d /= two;
                s++;
                }

            // test value against each witness
            RandomBigInteger rand = null;
            for (int i = 0; i < witnessCount; i++) {
                BigInteger a;
                if (i < witnesses.Length) {
                    a = witnesses[i];
                    if (a >= valLessOne) {
                        a %= value - three;
                        a += three;
                        }
                    }
                else {
                    if (rand == null) rand = new RandomBigInteger(3, valLessOne);
                    a = rand.Next();
                    }
                BigInteger x = BigInteger.ModPow(a, d, value);

                if (x == BigInteger.One) continue;
                for (uint r = 1; (r < s) && (x != valLessOne); r++) {
                    x = BigInteger.ModPow(x, two, value);
                    if (x == BigInteger.One) return false;
                    }
                if (x != valLessOne) return false;
                }
            // witnesses confirm value is prime
            return true;
            }
        }

    /// <summary>
    /// This is really just a placeholder right now. It could probably be improved
    /// speedwise since there probably isn't a need to use a cryptographically secure random number generator.
    /// </summary>
    internal class RandomBigInteger {
        BigInteger Floor;
        BigInteger Ceiling;
        int Bits;

        public RandomBigInteger(BigInteger Floor, BigInteger Ceiling) {
            Bits = Ceiling.ToByteArray().Length * 8;
            }

        public BigInteger Next() {
            var Test = Platform.GetRandomBigInteger(Bits);

            while ((Test >= Ceiling) | (Test <= Floor)) {
                Test = Platform.GetRandomBigInteger(Bits);
                }

            return Test;
            }
        }

    }
