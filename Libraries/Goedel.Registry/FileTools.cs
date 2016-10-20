using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Goedel.Registry;

namespace Goedel.Registry {

    /// <summary>
    /// Command line boolean type for flags.
    /// </summary>
    public abstract class _Flag : Goedel.Registry.Type {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public _Flag() {
            }

        /// <summary>
        /// Construct flag with specified value
        /// </summary>
        /// <param name="Value">The flag value to set</param>
        public _Flag(string Value) {
            Default(Value);
            }

        /// <summary>
        /// If true flag is set.
        /// </summary>
        public bool IsSet;

        /// <summary>
        /// The flag value.
        /// </summary>
        public bool Value {
            get { return IsSet; }
            }

        /// <summary>
        /// Register the flag type
        /// </summary>
        /// <param name="Tag">The tag</param>
        /// <param name="Registry">Registry to add flag to</param>
        /// <param name="Index">flag index</param>
        public override void Register(string Tag, Goedel.Registry.Registry Registry, int Index) {
            Registry.Register(Tag, Index);
            Registry.Register("no" + Tag, Index);
            }

        /// <summary>
        /// Construct flag from tag. If the first two letters of the tag are 'no', the
        /// flag is unset. For example /nosoup
        /// </summary>
        /// <param name="Tag">The tag</param>
        /// <returns>The required number of parameters.</returns>
        public override int Tag(string Tag) {
            if ((Tag.Length > 2) && Tag[0] == 'n' && Tag[1] == 'o') {
                IsSet = false;
                }
            else {
                IsSet = true;
                }

            return 0; // number of required parameters is 0
            }

        /// <summary>
        /// Set tag value from parameter.
        /// </summary>
        /// <param name="Text">The values true and 1 set a true value, 0 and false set a false value.
        /// Otherwise an exception is thrown.</param>
        public override void Parameter(string Text) {
            //Text = (Text == null) ? "true" : Text;
            switch (Text.ToLower()) {
                case "true":
                case "1":
                IsSet = true;
                break;
                case "false":
                case "0":
                IsSet = false;
                break;
                default:
                throw new System.Exception("Flag value not recognized" + Text);
                }
            }

        /// <summary>
        /// Convert value to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return IsSet ? "true" : "false";
            }

        /// <summary>
        /// Describe the flag usage.
        /// </summary>
        /// <param name="Tag">The tag</param>
        /// <param name="Value">The description.</param>
        /// <param name="Usage">The platform default line mode flag.</param>
        /// <returns>The usage string.</returns>
        public override string Usage(string Tag, string Value, char Usage) {
            return Usage + "[no]" + Tag;
            }

        }
    
    
    /// <summary>
    /// Command line flag for file.
    /// </summary>
    public abstract class _File : Goedel.Registry.Type {
        /// <summary>
        /// Default constructor
        /// </summary>
        public _File() {
            }

        /// <summary>
        /// Constructor with specified default value.
        /// </summary>
        /// <param name="Value"></param>
        public _File (string Value) {
            Default(Value);
            }

        /// <summary>
        /// The default extension.
        /// </summary>
        public string Extension = "";

        /// <summary>
        /// Set the default.
        /// </summary>
        /// <param name="TextIn"></param>
        public override void Default(string TextIn) {
            Extension = TextIn;
            }

        /// <summary>
        /// The value.
        /// </summary>
        public string Value {
            get { return Text; }
            }

        /// <summary>
        /// Construct extension defaulted file name for specified file.
        /// </summary>
        /// <param name="Source">The source file.</param>
        /// <returns>File name.</returns>
        public string DefaultFile(_File Source) {
            return DefaultFile(Source.Text);
            }

        /// <summary>
        /// Construct extension defaulted file name for specified file.
        /// </summary>
        /// <param name="Source">The source file.</param>
        /// <returns>File name.</returns>
        public string DefaultFile(string Source) {
            Text = FileTools.DefaultFile(this, Source);
            return Text;
            }

        } 
    
    /// <summary>
    /// Utility class for managing files.
    /// </summary>
    public class FileTools {


        /// <summary>
        /// Get the time at which the specified file was created
        /// Return DateTime.MinValue if the file does not exist
        /// </summary>
        /// <param name="FileName">The file to test</param>
        /// <returns>The time the file was created.</returns>
        public static DateTime GetFileDateTime(string FileName) {
            if (!File.Exists(FileName)) {
                return DateTime.MinValue;
                }
            return File.GetLastWriteTimeUtc(FileName);
            }

        /// <summary>
        /// Write short form description of the current program to the console.
        /// </summary>
        public static void About() {
            DateTime CompilationDate = Script.AssemblyBuildTime(
                System.Reflection.Assembly.GetCallingAssembly());

            string Build = Script.LocalizeTime(CompilationDate, false);


            Console.WriteLine(Script.AssemblyTitle);
            Console.WriteLine("  {0}", Script.AssemblyDescription);
            Console.WriteLine("  CopyRight : {0} {1}", Script.AssemblyCopyright, Script.AssemblyCompany);
            Console.WriteLine("  Version   : {0}", Script.AssemblyVersion);
            Console.WriteLine("  Compiled  : {0}", Build);
            }

        /// <summary>
        /// Cehck to see if a Destination file is more recent than a source file.
        /// </summary>
        /// <param name="Source">The source file.</param>
        /// <param name="Destination">The destination file.</param>
        /// <returns>True if the source was created before the destination.</returns>
        public static bool UpToDate(string Source, string Destination) {

            DateTime OutputDateTime = FileTools.GetFileDateTime(Destination);
            if (OutputDateTime == DateTime.MinValue) {
                return false;
                }
            DateTime ToolDateTime = Script.AssemblyBuildTime(
                System.Reflection.Assembly.GetCallingAssembly());
            if (OutputDateTime < ToolDateTime) {
                return false;
                }
            DateTime SourceDateTime = FileTools.GetFileDateTime(Source);
            return (OutputDateTime > SourceDateTime);
            }

        /// <summary>
        /// Determine output file name using command line entry and default data.
        /// </summary>
        /// <param name="Entry">The command line entry.</param>
        /// <param name="Default">The default file name.</param>
        /// <returns></returns>
        public static string DefaultFile(_File Entry, string Default) {
            if (Entry.Text != null) {
                return Entry.Text;
                }
            if (Entry.TagText != null) {
                // we have the flag
                return Path.GetFileNameWithoutExtension(Default) + "." + Entry.Extension;
                }
            return null;
            }


        //
        //  Search for a file.
        //
        //  If FileName exists, use that, otherwise try FileName.Extension
        //

        /// <summary>
        /// Search for a file using specified extension if required.
        /// </summary>
        /// <param name="FileName">The base file name.</param>
        /// <param name="Extension">Default extension.</param>
        /// <returns>The defaulted file.</returns>
        public static string DefaultExtension(string FileName, string Extension) {
            if (File.Exists (FileName)) return FileName;

            return FileName + "." + Extension;
            }

        /// <summary>
        /// Calculate output file name.
        /// </summary>
        /// <param name="SourcePath">The source file path</param>
        /// <param name="DestinationPath">The destination file path.</param>
        /// <param name="Extension">The default extension.</param>
        /// <returns>The defaulted file name.</returns>
        public static string DefaultOutput(string SourcePath, string DestinationPath,
                        string Extension) {
            if (DestinationPath != null) {
                return DestinationPath;
                }
            return Path.GetFileNameWithoutExtension(SourcePath) + "." + Extension;
            }
        }
    }
