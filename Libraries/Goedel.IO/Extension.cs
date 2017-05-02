using System;
using System.IO;
using System.Collections.Generic;

namespace Goedel.IO {

    /// <summary>
    /// Extension methods to simplify file operations. While there 
    /// is a combinatorial explosion of file access modes and sharing
    /// permissions, only a few of these combinations make sense.
    /// </summary>
    public  static partial class Extension {

        /// <summary>
        /// Open a file for read access allowing other processes to read the file..
        /// </summary>
        /// <param name="Filename">The file name</param>
        /// <returns>A file stream</returns>
        public static FileStream OpenFileRead(this string Filename) {
            return new FileStream(Filename, FileMode.Open, FileAccess.Read,
                FileShare.Read);
            }

        /// <summary>
        /// Open a file for read access in shared mode, allowing concurrent 
        /// reads and writes.
        /// </summary>
        /// <param name="Filename">The file name</param>
        /// <returns>A file stream</returns>
        public static FileStream OpenFileReadShared(this string Filename) {
            return new FileStream(Filename, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite);
            }

        /// <summary>
        /// Create a text reader on a file stream.
        /// </summary>
        /// <param name="FileStream">The base file.</param>
        /// <returns>The text reader.</returns>
        public static TextReader OpenTextReader(this FileStream FileStream) {
            return new StreamReader(FileStream);
            }

        /// <summary>
        /// Create a text reader for a file permitting other processes to
        /// perform concurrent reads.
        /// </summary>
        /// <param name="Filename">The file to read.</param>
        /// <returns>The text reader.</returns>
        public static TextReader OpenTextReader (this string Filename) {
            var FileStream = Filename.OpenFileRead();
            return new StreamReader(FileStream);
            }

        /// <summary>
        /// Create a new file for exclusive write access, overwriting 
        /// any existing file.
        /// </summary>
        /// <param name="Filename">The new file name.</param>
        /// <returns>File stream to write to the file.</returns>
        public static FileStream OpenFileNew(this string Filename) {
            return new FileStream(Filename, FileMode.Create, FileAccess.Write);
            }

        /// <summary>
        /// Open an existing file for exclusive write access, or create new file.
        /// </summary>
        /// <param name="Filename">The file to write to.</param>
        /// <returns>File stream to write to the file.</returns>
        public static FileStream OpenFileWrite(this string Filename) {
            return new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write);
            }

        /// <summary>
        /// Open an existing file for exclusive write access, or create new file.
        /// </summary>
        /// <param name="Filename">The file to write to.</param>
        /// <returns>File stream to write to the file.</returns>
        public static FileStream OpenFileWriteShare(this string Filename) {
            return new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write,
                FileShare.Read);
            }

        /// <summary>
        /// Open a new or existing file for append only write access. Permit
        /// concurrent reads but not writes.
        /// </summary>
        /// <param name="Filename">The file to write to.</param>
        /// <returns>File stream to write to the file.</returns>
        public static FileStream OpenFileAppend(this string Filename) {
            return new FileStream(Filename, FileMode.Append, FileAccess.Write,
                FileShare.Read);
            }

        /// <summary>
        /// Open a new or existing file for append only write access. Permit
        /// concurrent reads and writes.
        /// </summary>
        /// <param name="Filename">The file to write to.</param>
        /// <returns>File stream to write to the file.</returns>
        public static FileStream OpenFileAppendShare(this string Filename) {
            return new FileStream(Filename, FileMode.Append, FileAccess.Write,
                FileShare.ReadWrite);
            }

        /// <summary>
        /// Open a text writer to the specified file stream.
        /// </summary>
        /// <param name="FileStream">The file stream to write to.</param>
        /// <returns>The text writer.</returns>
        public static TextWriter OpenTextWriter(this FileStream FileStream) {
            return new StreamWriter(FileStream);
            }

        /// <summary>
        /// Open a text writer to the specified file in append mode permitting
        /// shared reads but not writes.
        /// </summary>
        /// <param name="Filename">The file to write to.</param>
        /// <returns>The text writer.</returns>
        public static TextWriter OpenTextWriter(this string Filename) {
            var FileStream = Filename.OpenFileAppend();
            return new StreamWriter(FileStream);
            }


        /// <summary>
        /// Create a new file for exclusive write access, overwriting 
        /// any existing file.
        /// </summary>
        /// <param name="Filename">The new file name.</param>
        /// <param name="Text">Text to write to file.</param>
        /// <returns>File stream to write to the file.</returns>
        public static void WriteFileNew(this string Filename, string Text) {
            using (var OutStream = Filename.OpenFileNew()) {
                using (var TextWriter = new StreamWriter(OutStream)) {
                    TextWriter.Write(Text);
                    }
                }
            }

        /// <summary>
        /// Create a new file for exclusive write access, overwriting 
        /// any existing file.
        /// </summary>
        /// <param name="Filename">The new file name.</param>
        /// <param name="Data">Data to write to file</param>
        /// <returns>File stream to write to the file.</returns>
        public static void WriteFileNew(this string Filename, byte[] Data) {
            using (var OutStream = Filename.OpenFileNew()) {
                OutStream.Write(Data, 0, Data.Length);
                }
            }


        public static void Write (this FileStream FileStream, byte[] Data) {
            FileStream.Write(Data, 0, Data.Length);
            }
        }
    }
