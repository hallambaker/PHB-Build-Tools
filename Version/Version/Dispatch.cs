using System;
using System.Collections.Generic;
using System.IO;
using Goedel.IO;
using Goedel.Utilities;
namespace Goedel.Tool.Version {
    public partial class Command {

        public override void Version(Version Options) {

            VersionInfo versionInfo;
            
            try {
                versionInfo = GetVersionInfo(Options.InputFile.Value);
                Console.WriteLine($"Assembly {versionInfo.Assembly} File {versionInfo.File}");

                using var output = Options.OutputFile.Value.OpenTextWriterNew();

                var generate = new Generate() {
                    _Output = output
                    };
                generate.GenerateCS(versionInfo);
                }
            catch {
                Console.WriteLine("***** File read conflict");
                //versionInfo = new VersionInfo() {
                //    Assembly = "0.0.0.0",
                //    File = "0.0.0.0"
                //    };
                }


            }

        VersionInfo GetVersionInfo(string file) {
            var versionInfo = new VersionInfo();

            using var input = file.OpenFileReadWrite() ;
            using var streamReader = new StreamReader(input);
          

            versionInfo.Assembly = streamReader.ReadLine();
            versionInfo.File = streamReader.ReadLine();


            versionInfo.Assembly = Increment(versionInfo.Assembly);
            versionInfo.File = Increment(versionInfo.File);

            var newInput = (versionInfo.Assembly + "\n" + versionInfo.File + "\n").ToUTF8();

            input.Seek(0, SeekOrigin.Begin);
            //using var textWriter = new StreamWriter(input);

            //textWriter.WriteLine(versionInfo.Assembly);
            //textWriter.WriteLine(versionInfo.File);

            input.Write(newInput, 0, newInput.Length);
            input.SetLength(newInput.Length);


            return versionInfo;
            }



        string Increment(string version) {
            if (version == null) {
                return "0.0.0.0";
                }


            var split = version.LastIndexOf('.');
            var left = version.Substring(0, split);
            var right = version.Substring(split+1);

            var val = System.Convert.ToInt32(right);
            val++;

            return left + '.' + val.ToString();
            }
        }


    public class VersionInfo {
        public string Assembly;
        public string File;
        }

    }
