using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Goedel.Tool.Makey {

    public static class Utilities {
        public static string UnixPath(this string File) {
            var Directory = Path.GetDirectoryName(File);
            return Directory.Replace('\\', '/'); ;
            }
        }

    public class VSProject {

        public List<string> CrossTargets { get; set; }  = new List<string> {

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

        public string OutputType { get; set; } = null;
        public string AssemblyName { get; set; } = null;

        public List<ReferenceType> Reference { get; set; } = new List<ReferenceType>();
        public List<CompileType> Compile { get; set; } = new List<CompileType>();
        public List<NoneType> None { get; set; } = new List<NoneType>();
        public List<ProjectReferenceType> ProjectReference { get; set; } = new List<ProjectReferenceType>();

        public string CompileAll = "";
        public string LinkAll = "";

        public List<string> SourceDependency = new List<string>();
        public List<string> LinkDependency = new List<string>();

        public VSProject (TextReader TextReader) {
            var Serializer = new XmlSerializer(typeof(Project));
            Project = Serializer.Deserialize(TextReader) as Project;

            foreach (var PropertyGroup in Project.PropertyGroup) {
                OutputType = OutputType != null ? OutputType : PropertyGroup.OutputType;
                AssemblyName = AssemblyName != null ? AssemblyName : PropertyGroup.AssemblyName;
                }

            foreach (var ItemGroup in Project.ItemGroup) {
                if (ItemGroup.Reference != null) {
                    foreach (var Item in ItemGroup?.Reference) {
                        Reference.Add(Item);
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
                        SourceDependency.Add(Item.Name);
                        }
                    }
                }

            }
        }


    }
