using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Goedel.Registry;

namespace Goedel.Registry {

    public abstract class _Flag : Goedel.Registry.Type {
        public _Flag() {
            }
        public _Flag(string Value) {
            Default(Value);
            }

        // Builtin for flag
        public bool IsSet;

        public bool Value {
            get { return IsSet; }
            }

        public override void Register(string Tag, Goedel.Registry.Registry Registry, int Index) {
            Registry.Register(Tag, Index);
            Registry.Register("no" + Tag, Index);
            }

        public override int Tag(string Tag) {
            if ((Tag.Length > 2) && Tag[0] == 'n' && Tag[1] == 'o') {
                IsSet = false;
                }
            else {
                IsSet = true;
                }

            return 0; // number of required parameters is 0
            }

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
        public override string ToString() {
            return IsSet ? "true" : "false";
            }

        public override string Usage(string Tag, string Value, char Usage) {
            return Usage + "[no]" + Tag;
            }

        }
    
    
    // Parameter type NewFile
    public abstract class _File : Goedel.Registry.Type {
        public _File() {
            }
        public _File (string Value) {
            Default(Value);
            }

        public string Extension = "";

        public override void Default(string TextIn) {
            Extension = TextIn;
            }
        public string Value {
            get { return Text; }
            }

        public string DefaultFile(_File Source) {
            return DefaultFile(Source.Text);
            }

        public string DefaultFile(string Source) {
            Text = FileTools.DefaultFile(this, Source);
            return Text;
            }

        } 
    
    public class FileTools {

        // Get the time at which the specified file was created
        // Return DateTime.MinValue if the file does not exist
        public static DateTime GetFileDateTime(string FileName) {
            if (!File.Exists(FileName)) {
                return DateTime.MinValue;
                }
            return File.GetLastWriteTimeUtc(FileName);
            }

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
        public static string DefaultExtension(string FileName, string Extension) {
            if (File.Exists (FileName)) return FileName;

            return FileName + "." + Extension;
            }

        public static string DefaultOutput(string SourcePath, string DestinationPath,
                        string Extension) {
            if (DestinationPath != null) {
                return DestinationPath;
                }
            return Path.GetFileNameWithoutExtension(SourcePath) + "." + Extension;
            }
        }
    }
