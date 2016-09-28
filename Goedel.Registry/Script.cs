using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Goedel.Registry {
    public partial class Script {
		public TextWriter _Output = null;
		public string _Indent = "";
        public string _Filename = null;

        public Script () {
			}

		public Script (TextWriter Output) {
			_Output = Output;
			}

        public void SetTextWriter (TextWriter Output) {
            Close();
            _Output = Output;
            }

        public void SetTextWriter (string FileName) {
            SetTextWriter (new StreamWriter(FileName));
            _Filename = FileName;
            }

        public void Close() {
            if (_Output != null) {
                _Output.Close ();
                }
            _Filename = null;
            }

        public static bool _TestEntryAssembly = true;
        public static Assembly _EntryAssembly = null;

        public static Assembly EntryAssembly {
            get {
                if (_TestEntryAssembly) {
                    _EntryAssembly = Assembly.GetEntryAssembly();
                    _TestEntryAssembly = false;
                    }
                return _EntryAssembly;
                }
            }

        static System.OperatingSystem OperatingSystem = System.Environment.OSVersion;

        public static string Platform {
            get { return OperatingSystem.Platform.ToString (); }
            }

        public static string PlatformVersion {
            get { return OperatingSystem.Version.ToString(); }
            }


        public static string AssemblyTitle {
            get { return GetAssemblyTitle(EntryAssembly); }
            }

        public static string GetAssemblyTitle(Assembly Assembly) {
            object[] attributes = Assembly.
                    GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0) {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "")
                    return titleAttribute.Title;
                }
            // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
            return System.IO.Path.GetFileNameWithoutExtension(Assembly.CodeBase);
            }

        public static string AssemblyVersion {
            get {
                return EntryAssembly != null ? GetAssemblyVersion(EntryAssembly) :
                        "Unknown";
                }
            }

        public static string GetAssemblyVersion(Assembly Assembly) {
            return Assembly.GetName().Version.ToString();
            }

        public static string AssemblyDescription {
            get {
                return EntryAssembly != null ? GetAssemblyDescription(EntryAssembly) :
                        "Unknown";
                }
            }

        public static string GetAssemblyDescription(Assembly Assembly) {

            // Get all Description attributes on this assembly
            object[] attributes = Assembly.
                    GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            // If there aren't any Description attributes, return an empty string
            if (attributes.Length == 0)
                return "";
            // If there is a Description attribute, return its value
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;

            }


        public static string AssemblyProduct {
            get {
                return EntryAssembly != null ? GetAssemblyProduct(EntryAssembly) :
                        "Unknown";
                }
            }

        public static string GetAssemblyProduct(Assembly Assembly) {
            // Get all Product attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().
                GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            // If there aren't any Product attributes, return an empty string
            if (attributes.Length == 0)
                return "";
            // If there is a Product attribute, return its value
            return ((AssemblyProductAttribute)attributes[0]).Product;
            }


        public static string AssemblyCopyright {
            get {
                return EntryAssembly != null ? GetAssemblyCopyright(EntryAssembly) :
                        "Unknown";
                }
            }
        public static string GetAssemblyCopyright(Assembly Assembly) {
            // Get all Copyright attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().
                GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            // If there aren't any Copyright attributes, return an empty string
            if (attributes.Length == 0)
                return "";
            // If there is a Copyright attribute, return its value
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }

        public static string AssemblyCompany {
            get {
                return EntryAssembly != null ? GetAssemblyCompany(EntryAssembly) :
                        "Unknown";
                }
            }

        public static string GetAssemblyCompany(Assembly Assembly) {

            // Get all Company attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().
                GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            // If there aren't any Company attributes, return an empty string
            if (attributes.Length == 0)
                return "";
            // If there is a Company attribute, return its value
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }


        // Get the date at which the running assembly was linked

        // Method due to Jeff Atwood and Dustin Aleksiuk
        // http://www.codinghorror.com/blog/2005/04/determining-build-date-the-hard-way.html

        // This approach may well be dependent on the MSFT linker format not
        // changing but seems to be the best currently available. Hopefully
        // MSFT will do the right thing and provide a proper call for this before
        // they change the linker format.

        public static DateTime AssemblyBuildTime(Assembly Assembly) {

            string FilePath = Assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try {
                s = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
                }
            finally {
                if (s != null) {
                    s.Close();
                    }
                }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int SecondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime DateTime = new DateTime(1970, 1, 1, 0, 0, 0);
            DateTime = DateTime.AddSeconds(SecondsSince1970);

            return DateTime.SpecifyKind(DateTime, DateTimeKind.Utc); ;
            }

        public static string LocalizeTime(DateTime Time, bool UTC) {
            string TimeZoneName = "UTC";
            DateTime ZoneTime = Time;
            string Format = "u";

            if (!UTC) {
                TimeZoneInfo TimeZoneInfo = TimeZoneInfo.Local;
                bool DaylightSavings = TimeZoneInfo.IsDaylightSavingTime(Time);
                TimeZoneName = DaylightSavings ?
                    TimeZoneInfo.DaylightName : TimeZoneInfo.StandardName;

                ZoneTime = TimeZoneInfo.ConvertTimeFromUtc(Time, TimeZoneInfo);
                Format = "yyyy-MM-dd HH:mm:ss zzz";
                }

            return ZoneTime.ToString(Format);
            }

        /// <summary>
        /// Build an indented comment string at the current position
        /// for the currently selected language.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public string CommentSummary(int Spaces, string Text) {
            var Indent = _Indent + new string(' ', Spaces);
            var Builder = new StringBuilder();

            Builder.Append (Indent);
            Builder.Append("/// <summary>\n");

            var Split = Text.Split('\n');

            foreach (var Part in Split) {
                Builder.Append(Indent);
                Builder.Append("///");
                Builder.Append(Part);
                Builder.Append("\n");
                }

            Builder.Append(Indent);
            Builder.Append("/// </summary>\n");

            return Builder.ToString();
            }

        ////
        //// Header
        ////
        //public static void Header(TextWriter _Output, string Comment, DateTime GenerateTime) {
        //    // #% System.OperatingSystem OperatingSystem = System.Environment.OSVersion; 
        //    System.OperatingSystem OperatingSystem = System.Environment.OSVersion;
        //    // #prefix "//" 
        //    string _Indent = Comment;
        //        {
        //        //  
        //        _Output.Write("\n{0}", _Indent);
        //        // This file was automatically generated at #{GenerateTime.ToLocalTime()} 
        //        _Output.Write("This file was automatically generated at {1}\n{0}", _Indent, GenerateTime.ToLocalTime());
        //        //   
        //        _Output.Write(" \n{0}", _Indent);
        //        // Changes to this file may be overwritten without warning 
        //        _Output.Write("Changes to this file may be overwritten without warning\n{0}", _Indent);
        //        //  
        //        _Output.Write("\n{0}", _Indent);
        //        // Generator:  #{Script.AssemblyTitle} version #{Script.AssemblyVersion} 
        //        _Output.Write("Generator:  {1} version {2}\n{0}", _Indent, Script.AssemblyTitle, Script.AssemblyVersion);
        //        //     Goedel Script Version : 0.1   Generated  
        //        _Output.Write("    Goedel Script Version : 0.1   Generated \n{0}", _Indent);
        //        //     Goedel Schema Version : 0.1   Generated 
        //        _Output.Write("    Goedel Schema Version : 0.1   Generated\n{0}", _Indent);
        //        //  
        //        _Output.Write("\n{0}", _Indent);
        //        //     Copyright : #{Script.AssemblyCopyright} 
        //        _Output.Write("    Copyright : {1}\n{0}", _Indent, Script.AssemblyCopyright);
        //        //  
        //        _Output.Write("\n{0}", _Indent);
        //        // Build Platform: #{OperatingSystem.Platform} #{OperatingSystem.Version} 
        //        _Output.Write("Build Platform: {1} {2}\n{0}", _Indent, OperatingSystem.Platform, OperatingSystem.Version);
        //        //  
        //        _Output.Write("\n{0}", _Indent);
        //        // #end prefix 
        //        }
        //    // #end method 
        //    }
        ////  
        ////  
        //// #method3 MITLicense string Comment string Year string Holder 


        ////
        //// MITLicense
        ////
        //public static void MITLicense(TextWriter _Output, string Comment,
        //                                string Year, string Holder) {
        //    // #prefix "//" 
        //    string _Indent = Comment;
        //    //  
        //    _Output.Write("\n{0}", _Indent);
        //    // Copyright (C) #{Year} by #{Holder} 
        //    _Output.Write("{1} by {2}\n{0}", _Indent, Year, Holder);
        //    //  
        //    _Output.Write("\n{0}", _Indent);
        //    // Permission is hereby granted, free of charge, to any person obtaining a copy 
        //    _Output.Write("Permission is hereby granted, free of charge, to any person obtaining a copy\n{0}", _Indent);
        //    // of this software and associated documentation files (the "Software"), to deal 
        //    _Output.Write("of this software and associated documentation files (the \"Software\"), to deal\n{0}", _Indent);
        //    // in the Software without restriction, including without limitation the rights 
        //    _Output.Write("in the Software without restriction, including without limitation the rights\n{0}", _Indent);
        //    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
        //    _Output.Write("to use, copy, modify, merge, publish, distribute, sublicense, and/or sell\n{0}", _Indent);
        //    // copies of the Software, and to permit persons to whom the Software is 
        //    _Output.Write("copies of the Software, and to permit persons to whom the Software is\n{0}", _Indent);
        //    // furnished to do so, subject to the following conditions: 
        //    _Output.Write("furnished to do so, subject to the following conditions:\n{0}", _Indent);
        //    //  
        //    _Output.Write("\n{0}", _Indent);
        //    // The above copyright notice and this permission notice shall be included in 
        //    _Output.Write("The above copyright notice and this permission notice shall be included in\n{0}", _Indent);
        //    // all copies or substantial portions of the Software. 
        //    _Output.Write("all copies or substantial portions of the Software.\n{0}", _Indent);
        //    //  
        //    _Output.Write("\n{0}", _Indent);
        //    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
        //    _Output.Write("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\n{0}", _Indent);
        //    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
        //    _Output.Write("IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\n{0}", _Indent);
        //    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
        //    _Output.Write("FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\n{0}", _Indent);
        //    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
        //    _Output.Write("AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\n{0}", _Indent);
        //    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
        //    _Output.Write("LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\n{0}", _Indent);
        //    // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
        //    _Output.Write("OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN\n{0}", _Indent);
        //    // THE SOFTWARE. 
        //    _Output.Write("THE SOFTWARE.\n{0}", _Indent);
        //    //  
        //    _Output.Write("\n{0}", _Indent);
        //    // #end prefix 

        //    // #end method 
        //    }


        }
    }
