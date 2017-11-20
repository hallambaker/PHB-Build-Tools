using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Goedel.Tool.Makey {

    public static class Utilities {


        public static string UnixCanonicalPath (this string Path) {
            var Directories = Path.Split('\\');

            for (var i = 0; i < Directories.Length; i++) {
                if (Directories[i] == "." | Directories[i] == "") {
                    Directories[i] = null;
                    }
                else if (Directories[i] == "..") {
                    if (StrikeDirectory(Directories, i)) {
                        Directories[i] = null;
                        }
                    }

                }

            var Builder = new StringBuilder();
            foreach (var Directory in Directories) {
                if (Directory != null) {
                    Builder.Append(Directory);
                    Builder.Append("/");
                    }
                }

            return Builder.ToString();
            }


        static bool StrikeDirectory (string[] Directories, int Index) {
            for (var i = Index - 1; i >= 0; i--) {
                if (Directories[i] != null) {
                    Directories[i] = null;
                    return true;
                    }
                }
            return false;

            }

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

    public class SaneXmlTextReader : XmlTextReader {
        public SaneXmlTextReader (TextReader reader): base(reader) { }

        public override string NamespaceURI => "";
        }


    public class VSProject : VSFile  {

        public SortedSet<string> ManualAddLibraries = new SortedSet<string> {
            "WindowsBase",
            "System.Numerics",
            "Microsoft.VisualStudio.QualityTools.UnitTestFramework"
            };


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
        public List<string> FixedLinkDependency = new List<string>();
        public List<string> LinkDependency = new List<string>();
        public List<string> AdditionalLinkDependency { get; set; } = new List<string>();

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



        void FuckerParse2 (TextReader TextReader) {
            Project = new Project();
            Project.ItemGroup = new List<ItemGroupType>();
            Project.PropertyGroup = new List<PropertyGroupType>();
            ItemGroupType ItemGroup = null;
            PropertyGroupType PropertyGroup = null;
            ReferenceType Reference = null;
            CompileType Compile = null;
            NoneType None = null;
            ProjectReferenceType ProjectReference = null;

            object Current = Project;
            var XmlReader = new XmlTextReader(TextReader) { };



            while (XmlReader.Read()) {
                if (XmlReader.NodeType== XmlNodeType.Element) {
                    switch (XmlReader.Name) {
                        case "PropertyGroup": {
                            PropertyGroup = new PropertyGroupType();
                            Project.PropertyGroup.Add(PropertyGroup);
                            Current = PropertyGroup;
                            break;
                            }
                        case "ItemGroup": {
                            ItemGroup = new ItemGroupType();
                            ItemGroup.Reference = new List<ReferenceType>();
                            ItemGroup.Compile = new List<CompileType>();
                            ItemGroup.None = new List<NoneType>();
                            ItemGroup.ProjectReference = new List<ProjectReferenceType>();
                            Project.ItemGroup.Add(ItemGroup);
                            Current = ItemGroup;
                            break;
                            }
                        case "Reference": {
                            Reference = new ReferenceType();
                            ItemGroup.Reference.Add (Reference);
                            Reference.Include = XmlReader.GetAttribute("Include");
                            Current = Reference;
                            break;
                            }
                        case "Compile": {
                            Compile = new CompileType();
                            ItemGroup.Compile.Add(Compile);
                            Compile.Include = XmlReader.GetAttribute("Include");
                            Current = Compile;
                            break;
                            }
                        case "None": {
                            None = new NoneType();
                            ItemGroup.None.Add(None);
                            None.Include = XmlReader.GetAttribute("Include");
                            Current = None;
                            break;
                            }
                        case "ProjectReference": {
                            ProjectReference = new ProjectReferenceType();
                            ItemGroup.ProjectReference.Add(ProjectReference);
                            ProjectReference.Include = XmlReader.GetAttribute("Include");
                            Current = ProjectReference;
                            break;
                            }

                        // now the strings
                        //case "Include": {
                            
                        //    if (Current == Reference) {
                        //        XmlReader.Read();
                        //        Reference.Include = XmlReader.Value;
                        //        }
                        //    if (Current == Compile) {
                        //        XmlReader.Read();
                        //        Compile.Include = XmlReader.Value;
                        //        }
                        //    if (Current == None) {
                        //        XmlReader.Read();
                        //        None.Include = XmlReader.Value;
                        //        }
                        //    if (Current == ProjectReference) {
                        //        XmlReader.Read();
                        //        ProjectReference.Include = XmlReader.Value;
                        //        }


                        //    break;
                        //    }
                        case "Project": {
                            
                            if (Current == ProjectReference) {
                                XmlReader.Read();
                                ProjectReference.Project = XmlReader.Value;
                                }
                            break;
                            }

                        // one off

                        case "ProjectTypeGuids": {
                            
                            if (Current == PropertyGroup) {
                                XmlReader.Read();
                                PropertyGroup.ProjectTypeGuids = XmlReader.Value;
                                }

                            break;
                            }
                        case "ProjectGuid": {

                            if (Current == PropertyGroup) {
                                XmlReader.Read();
                                PropertyGroup.ProjectGuid = XmlReader.Value;
                                }

                            break;
                            }
                        case "OutputType": {

                            if (Current == PropertyGroup) {
                                XmlReader.Read();
                                PropertyGroup.OutputType = XmlReader.Value;
                                }

                            break;
                            }
                        case "AssemblyName": {

                            if (Current == PropertyGroup) {
                                XmlReader.Read();
                                PropertyGroup.AssemblyName = XmlReader.Value;
                                }

                            break;
                            }

                        case "HintPath": {

                            if (Current == Reference) {
                                XmlReader.Read();
                                Reference.HintPath = XmlReader.Value;
                                }

                            break;
                            }
                        case "Private": {

                            if (Current == Reference) {
                                XmlReader.Read();
                                Reference.Private = XmlReader.Value;
                                }

                            break;
                            }
                        case "DependentUpon": {

                            if (Current == Compile) {
                                XmlReader.Read();
                                Compile.DependentUpon = XmlReader.Value;
                                }

                            break;
                            }
                        case "Generator": {

                            if (Current == None) {
                                XmlReader.Read();
                                None.Generator = XmlReader.Value;
                                }

                            break;
                            }
                        case "LastGenOutput": {

                            if (Current == None) {
                                XmlReader.Read();
                                None.LastGenOutput = XmlReader.Value;
                                }

                            break;
                            }
                        case "Name": {
                            if (Current == ProjectReference) {
                                XmlReader.Read();
                                ProjectReference.Name = XmlReader.Value;
                                }

                            break;
                            }
                        //default: {
                        //    Current = null;
                        //    break;
                        //    }
                        }



                    }





                //switch (XmlReader.NodeType) {
                //    case XmlNodeType.Element:
                //    Console.Write("<{0}>", XmlReader.Name);
                //    break;


                //    case XmlNodeType.Text:
                //    Console.Write(XmlReader.Value);
                //    break;

                //    default: {
                //        break;
                //        }
                //    }



                }


            }



        // Fucking fuckwittery. XmlSerializer has two modes, throw a stupid
        // error if the utterly unnecessary namespace is missing if expected and
        // throw a stupid error if it is encountered unexpectedly. 
        void FuckerParse (TextReader TextReader) {
            var ProjectType = typeof(Project);
            var XmlRootAttribute = new XmlRootAttribute() {
                ElementName = "Project",
                IsNullable = false,
                };
            var Serializer = new XmlSerializer(ProjectType);
            var XmlReader = new XmlTextReader(TextReader) { };
            Project = Serializer.Deserialize(XmlReader) as Project;


            //try {
            //    var XmlRootAttribute = new XmlRootAttribute() {
            //        ElementName = "Project",
            //        Namespace = "http://schemas.microsoft.com/developer/msbuild/2003",
            //        IsNullable = false,
            //        DataType = "string"
            //        };
            //    var Serializer = new XmlSerializer(ProjectType, XmlRootAttribute);
            //    Project = Serializer.Deserialize(TextReader) as Project;
            //    }

            //catch {
            //    var XmlRootAttribute = new XmlRootAttribute() {
            //        ElementName = "Project",
            //        IsNullable = false,
            //        DataType = "string"
            //        };
            //    var Serializer = new XmlSerializer(ProjectType, XmlRootAttribute);
            //    Project = Serializer.Deserialize(TextReader) as Project;
            //    }

            }


        public void Parse (TextReader TextReader, bool Expand) {
            FuckerParse2(TextReader);

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
                    Target = AssemblyName + ".dll";
                    IsLibrary = true;
                    break;
                    }
                }

 
            if (!Expand) {
                return;
                }
            if (Project.ItemGroup != null) {
                foreach (var ItemGroup in Project.ItemGroup) {
                    if (ItemGroup.Reference != null) {
                        foreach (var Item in ItemGroup?.Reference) {
                            Reference.Add(Item);

                            var Len = Item.Include.IndexOf(',');
                            var Name = Len > 0 ? Item.Include.Substring(0, Len) : Item.Include;
                            //Console.WriteLine("Library {0}", Name);

                            if (Item.Private == "True") {
                                PrivateReference.Add(Item);
                                //Console.WriteLine("Nuget Package Path {0}", Item.HintPath);
                                }
                            else if (ManualAddLibraries.Contains(Name)) {
                                AdditionalLinkDependency.Add(Name);
                                //Console.WriteLine("Assembly library Path {0}", Name);
                                }
                            else if (Item.HintPath != null) {
                                //Console.WriteLine("Link library Path {0}", Item.HintPath);
                                FixedLinkDependency.Add(Path.GetFileName(Item.HintPath));
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
        }



    public partial class ProjectReferenceType {
        [System.Xml.Serialization.XmlIgnore]
        public VSProject SubProject;
        }
    }