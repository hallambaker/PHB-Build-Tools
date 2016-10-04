using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Goedel.Tool.Makey {

    public static class Utilities {

        public static string UnixPath(this string File, string Sub) {
            var FilePath = Path.Combine(File, Sub);
            var Directory = Path.GetDirectoryName(FilePath);
            return Directory.Replace('\\', '/'); ;
            }

        public static string UnixPath(this string File) {
            var Directory = Path.GetDirectoryName(File);
            return Directory.Replace('\\', '/'); ;
            }

        public static string UnixFile(this string File) {
            return File.Replace('\\', '/'); ;
            }

        public static string UnixFile(this string File, string Sub) {
            var FilePath = Path.Combine(File, Sub);
            return FilePath.Replace('\\', '/'); ;
            }

        }

    public class VSProject : VSFile  {

        public List<string> CrossTargets { get; set; } = new List<string> {

            "4.4.0-linux-libc2.13-amd64",
            "4.4.0-linux-libc2.13-armel",
            "4.4.0-linux-libc2.13-armhf",
            "4.4.0-linux-libc2.13-i386",
            "4.4.0-macos-10.7-amd64",
            "4.4.0-macos-10.7-i386",
            "4.4.2-linux-libc2.13-amd64",
            "4.4.2-linux-libc2.13-armel",
            "4.4.2-linux-libc2.13-armhf",
            "4.4.2-linux-libc2.13-i386",
            "4.4.2-macos-10.7-amd64",
            "4.4.2-macos-10.7-i386"
            };


        public Project Project;

        public string Target = "unknown";

        public string OutputType { get; set; } = null;
        public bool IsExe = false;
        public bool IsLibrary = false;
        public string AssemblyName { get; set; } = null;
        public string ProjectGuid { get; set; } = null;
        public string ProjectTypeGuids { get; set; } = null;
        

        public List<ReferenceType> Reference { get; set; } = new List<ReferenceType>();
        public List<ReferenceType> PrivateReference { get; set; } = new List<ReferenceType>();

        public List<CompileType> Compile { get; set; } = new List<CompileType>();
        public List<NoneType> None { get; set; } = new List<NoneType>();
        public List<ProjectReferenceType> ProjectReference { get; set; } = new List<ProjectReferenceType>();

        public string CompileAll = "";
        public string LinkAll = "";

        public List<string> SourceDependency = new List<string>();
        public List<string> LinkDependency = new List<string>();


        public string Directory;

        public VSProject(string Filename, bool Expand) {

            Directory = Path.GetDirectoryName(Filename);

            using (Stream scriptfile =
                new FileStream(Filename, FileMode.Open, FileAccess.Read)) {

                using (var TextReader = new StreamReader(scriptfile)) {

                    Parse (TextReader, Expand);
                    }
                }
            }

        public VSProject(TextReader TextReader) {
            Parse(TextReader, true);
            }

        public void Parse (TextReader TextReader, bool Expand) { 
            var Serializer = new XmlSerializer(typeof(Project));
            Project = Serializer.Deserialize(TextReader) as Project;

            foreach (var PropertyGroup in Project.PropertyGroup) {
                OutputType = OutputType != null ? OutputType : PropertyGroup.OutputType;
                AssemblyName = AssemblyName != null ? AssemblyName : PropertyGroup.AssemblyName;
                ProjectGuid = ProjectGuid != null ? ProjectGuid : PropertyGroup.ProjectGuid;
                ProjectTypeGuids = ProjectTypeGuids != null ? ProjectTypeGuids : PropertyGroup.ProjectTypeGuids;
                }

            switch (OutputType) {
                case "Exe":
                case "AppContainerExe": {
                    Target = AssemblyName + ".exe";
                    IsExe = true;
                    break;
                    }
                case "Library": {
                    Target = AssemblyName + ".dll";
                    IsLibrary = true;
                    break;
                    }
                default: {
                    Target = null;
                    break;
                    }
                }

 
            if (!Expand) {
                return;
                }

            foreach (var ItemGroup in Project.ItemGroup) {
                if (ItemGroup.Reference != null) {
                    foreach (var Item in ItemGroup?.Reference) {
                        Reference.Add(Item);
                        if (Item.Private == "True") {
                            PrivateReference.Add(Item);
                            Console.WriteLine("Nuget Package Path {0}", Item.HintPath);
                            }
                        }
                    }
                if (ItemGroup.Compile != null) {
                    foreach (var Item in ItemGroup?.Compile) {
                        Compile.Add(Item);
                        CompileAll += Item.Include + " ";
                        SourceDependency.Add(Item.Include);
                        }
                    }
                if (ItemGroup.None != null) {
                    foreach (var Item in ItemGroup?.None) {
                        None.Add(Item);
                        }
                    }
                if (ItemGroup.ProjectReference != null) {
                    foreach (var Item in ItemGroup?.ProjectReference) {
                        ProjectReference.Add(Item);

                        var IncludeFile = Path.Combine(Directory, Item.Include);

                        Item.SubProject = new VSProject(IncludeFile, false);
                        LinkDependency.Add(Item.SubProject.Target);
                        }
                    }
                }

            }
        }



    public partial class ProjectReferenceType {
        [System.Xml.Serialization.XmlIgnore]
        public VSProject SubProject;
        }
    }