﻿//   Copyright © 2015 by Comodo Group Inc.
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
using System.Diagnostics;

namespace Goedel.Debug {

    /// <summary>
    /// Collection of static methods that provide debugging utilities. These may well have been
    /// superceded by the capabilities in the System.Diagnostics.Debug package
    /// </summary>
    public partial class Trace {


        /// <summary>
        /// Flag allowing output to be disabled.
        /// </summary>
        public static bool Disable {
            get { return _Disable; }
            set { _Disable = value; }
            }
        private static bool _Disable = false;


        /// <summary>
        /// Not Yet Implemented method. Cannot be disabled.
        /// </summary>
        /// <param name="Report">Text to display on the console.</param>
        public static void NYI(string Report) {
            System.Diagnostics.Debug.WriteLine(Report);
            }


        /// <summary>
        /// Not Yet Implemented method. Cannot be disabled.
        /// </summary>
        /// <param name="Report">Text to display on the console.</param>
        public static void TBS(string Report) {
            WriteLine(Report);
            }


        /// <summary>
        /// Output functions, pipe data to the console right now. In the future, will
        /// pipe to a file as well.
        /// </summary>
        /// <param name="Text"></param>
        public static void WriteLine(string Text) {
            if (Disable) return;
            System.Diagnostics.Debug.WriteLine(Text);
            //System.Diagnostics.Trace.WriteLine(Text);
            }

        /// <summary>
        /// Write the specified text to the debug output.
        /// </summary>
        /// <param name="Text"></param>
        public static void Write(string Text) {
            if (Disable) return;
            System.Diagnostics.Debug.WriteLine(Text);
            //System.Diagnostics.Trace.Write(Text);
            }

        /// <summary>
        /// Write a newline to the debug output.
        /// </summary>
        public static void WriteLine() {
            Write("\n");
            }

        /// <summary>
        /// Write formatted data to the debug output with following newline.
        /// </summary>
        /// <param name="Text">Format string</param>
        /// <param name="Data">Data object to write.</param>
        public static void WriteLine(string Text, Object Data) {
            if (Disable) return;
            var Message = String.Format(Text, Data);
            WriteLine(Message);
            }

        /// <summary>
        /// Write formatted data to the debug output with following newline.
        /// </summary>
        /// <param name="Text">Format string</param>
        /// <param name="Data">Data objects to write.</param>
        public static void WriteLine(string Text, params Object[] Data) {
            if (Disable) return;
            var Message = String.Format(Text, Data);
            WriteLine(Message);
            }

        /// <summary>
        /// Write formatted data to the debug output.
        /// </summary>
        /// <param name="Text">Format string</param>
        /// <param name="Data">Data object to write.</param>
        public static void Write(string Text, Object Data) {
            if (Disable) return;
            var Message = String.Format(Text, Data);
            Write(Message);
            }

        /// <summary>
        /// Write formatted data to the debug output.
        /// </summary>
        /// <param name="Text">Format string</param>
        /// <param name="Data">Data object to write.</param>
        public static void Write(string Text, params Object[] Data) {
            if (Disable) return;
            var Message = String.Format(Text, Data);
            Write(Message);
            }

        /// <summary>
        /// Write out a buffer to the console as hex.
        /// </summary>
        /// <param name="Text">Tag to identify the data</param>
        /// <param name="Data">Data to be written.</param>
        public static void WriteHex(string Text, byte[] Data) {
            WriteLine(Text);

            string Line = "   ";

            for (int i = 0; i < Data.Length; i++) {
                if (i > 0) {
                    if (i % 16 == 0) {
                        WriteLine(Line);
                        Line = "   ";
                        }
                    else if (i % 8 == 0) {
                        Line = Line + "   ";
                        }
                    }
                Line = Line + string.Format (" {0:x2}", Data[i]);
                }
            WriteLine(Line);
            }

        }

    }

