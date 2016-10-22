using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Goedel.Utilities {
    public static partial class Extension {

        /// <summary>
        /// Convert integer to ASCII character if in the range 1-127, otherwise
        /// return .
        /// </summary>
        /// <param name="In">The character to convert</param>
        /// <returns>ASCII character if 0 &lt; In &lt; 128, otherwise '.'</returns>
        public static char ToASCII(this int In) {
            if (In > 0 & In < 128) return (char)In;
            return '.';
            }

        /// <summary>
        /// Test to see if an input character is a Base64 character.
        /// </summary>
        /// <param name="c">The input character value</param>
        /// <returns>true if and only if the input corresponds to an ASCII 
        /// character used to encode Base64 in traditional or URL encoding
        /// format.</returns>
        public static bool IsBase64(this int c) {
            return ((c >= 'a' & c <= 'z') | (c >= 'A' & c <= 'Z') |
                (c >= '0' & c <= '9') | c == '+' | c == '/' | c == '_' | c == '-');

            }


        /// <summary>
        /// Count the number of bytes that are required to encode
        /// a string in UTF8.
        /// </summary>
        /// <param name="Text">Input string</param>
        /// <returns>Number of bytes required to encode the string.</returns>
        public static int CountUTF8(this string Text) {
            var Encoding = new UTF8Encoding();
            return Encoding.GetByteCount(Text);
            }

        /// <summary>
        /// Convert a string to a UTF byte array
        /// </summary>
        /// <param name="Text">Text to convert</param>
        /// <returns>UTF8 character data as array</returns>
        public static byte [] ToUTF8 (this string Text) {
            var Encoding = new UTF8Encoding();
            return Encoding.GetBytes(Text);
            }

        /// <summary>
        /// Convert a string to a UTF byte array
        /// </summary>
        /// <param name="Text">Text to convert</param>
        /// <param name="Buffer">Output buffer to write result to.</param>
        /// <param name="Position">Starting position to write data to.</param>
        /// <returns>Number of characters converted</returns>
        public static int ToUTF8(this string Text, byte[] Buffer, int Position) {
            var Encoding = new UTF8Encoding();
            return Encoding.GetBytes(Text, 0, Text.Length, Buffer, Position);
            }

        }
    }
