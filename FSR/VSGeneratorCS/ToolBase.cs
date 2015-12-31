using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;
using Goedel.Registry;

/// <summary>
/// Implement a test single file generator using the approach described in
/// http://www.codeproject.com/Tips/1023337/Custom-Tool-Single-File-Generator-for-Visual-Studi
/// 
/// See also
/// https://mnaoumov.wordpress.com/2012/09/26/developing-custom-tool-aka-single-file-generators-for-visual-studio-2012/
/// 
/// and
/// http://www.codeproject.com/Articles/31257/Custom-Tools-Explained
/// </summary>

namespace Goedel.Script {

    /// <summary>
    /// Base class, implements a single file generator.
    /// </summary>
    [Guid("83A2D491-F2C5-407F-B1A6-1D4FC9B4C053")]
    public class Generator : IVsSingleFileGenerator {

        /// <summary>
        /// Set the default file extension
        /// </summary>
        /// <param name="InputfileRqExtension">The requested extension.</param>
        /// <returns>Status code from VSConstants (0=OK)</returns>
        public int DefaultExtension(out string InputfileRqExtension) {
            InputfileRqExtension = ".cs";         // the extension must include the leading period
            return VSConstants.S_OK;              // signal successful completion
            }

        /// <summary>
        /// Generate the output data.
        /// </summary>
        /// <param name="wszInputFilePath">The input file name.</param>
        /// <param name="bstrInputFileContents">The input file contents.</param>
        /// <param name="wszDefaultNamespace">The default namespace in which to generate code.</param>
        /// <param name="rgbOutputFileContents">Returns an array of bytes to be written to the generated file.
        /// This is a raw bytestream.</param>
        /// <param name="pcbOutput">Count of bytes in rgbOutputFileContents</param>
        /// <param name="pGenerateProgress">Progress bar.</param>
        /// <returns></returns>
        public int Generate(string wszInputFilePath, string bstrInputFileContents, 
            string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, 
            IVsGeneratorProgress pGenerateProgress) {

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data

            var Parse = new FSRGen.FSRStruct();

            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);
            var Script = new FSRGen.Generate(Writer);
            Script.GenerateCS(Parse);


            // Convert writer data to a string and then a byte array
            var Text = Writer.ToString();
            var Data = Encoding.UTF8.GetBytes(Text);
            var Length = Data.Length;

            // Copy the result to the output
            rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);
            Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);
            pcbOutput = (uint)Length;
            return VSConstants.S_OK;
            }



        }
    }
