//   Copyright © 2015 by Default Deny Security Inc.
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

namespace Goedel.Tool.Script {

    /// <summary>
    /// Class with static methods to turn a file into a C# constant string.
    /// </summary>
    public class DoWrap {

        /// <summary>
        /// Read an input file and wrap it as a C# string.
        /// </summary>
        /// <param name="In">The input file</param>
        /// <param name="Out">The output file</param>
        /// <param name="Namespace">The namespace in which to create the constant.</param>
        /// <param name="Class">The (partial) class in which to create the constant.</param>
        /// <param name="Variable">The name of the variable to assign the constant to.</param>
        public static void CS(TextReader In, TextWriter Out, 
                    string Namespace, string Class, string Variable) {
            Out.WriteLine("namespace {0} {{", Namespace);
            Out.WriteLine("    public partial class {0} {{", Class);
            Out.WriteLine("        public const string {0} = @\"", Variable);

            var c = In.Read();
            while (c > 0) {
                var cc = (char)c;
                if (cc == '\"') {
                    Out.Write ("\"\"");
                    }
                else {
                    Out.Write(cc);
                    }
                c = In.Read();
                }

            Out.WriteLine("\";");
            Out.WriteLine("        }");
            Out.WriteLine("    }");
            }
        }
    }
