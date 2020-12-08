using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Goedel.Tool.Makey {

    public static class Utilities {



        public static string StripMacro (this string Text) {
            var Index = Text.LastIndexOf(')');
            if (Index < 0) {
                return Text;
                }
            return Text.Substring(Index + 1);
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
        public List<ImportType> ImportList { get; set; } = new List<ImportType>();

        public string CompileAll = "";
        public string LinkAll = "";

        public List<string> SourceDependency = new List<string>();
        public List<string> FixedLinkDependency = new List<string>();
        public List<string> LinkDependency = new List<string>();
        public List<string> AdditionalLinkDependency { get; set; } = new List<string>();
        
        public string Directory;
        public string RelativeDirectory=null;
        public ProjectType ProjectType;

        public List<VSProject> SharedProject = new List<VSProject>();

        public VSProject(string Filename, bool Expand) {

            Directory = Path.GetDirectoryName(Filename);

            using Stream scriptfile =
                new FileStream(Filename, FileMode.Open, FileAccess.Read);
            using var TextReader = new StreamReader(scriptfile);
            Parse(TextReader, Expand, Filename);
            }

        public VSProject(TextReader TextReader) => Parse(TextReader, true, "");



        void FuckerParse2 (TextReader TextReader) {
            Project = new Project() {
                ItemGroup = new List<ItemGroupType>(),
                PropertyGroup = new List<PropertyGroupType>()
                };
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
                            ItemGroup = new ItemGroupType() {
                                Reference = new List<ReferenceType>(),
                                Compile = new List<CompileType>(),
                                None = new List<NoneType>(),
                                ProjectReference = new List<ProjectReferenceType>()
                                };
                            Project.ItemGroup.Add(ItemGroup);
                            Current = ItemGroup;
                            break;
                            }
                        case "Reference": {
                            Reference = new ReferenceType();
                            ItemGroup.Reference.Add (Reference);
                            Reference.Include = XmlReader.GetAttribute("Include");
                            Reference.Update = XmlReader.GetAttribute("Update");
                            Current = Reference;
                            break;
                            }
                        case "Compile": {
                            Compile = new CompileType();
                            ItemGroup.Compile.Add(Compile);
                            Compile.Include = XmlReader.GetAttribute("Include");
                            Compile.Update = XmlReader.GetAttribute("Update");
                            Current = Compile;
                            break;
                            }
                        case "Import": {
                            var Import = new ImportType();
                            ImportList.Add(Import);
                            Import.Condition = XmlReader.GetAttribute("Condition");
                            Import.Project = XmlReader.GetAttribute("Project");
                            Import.Label = XmlReader.GetAttribute("Label");
                            Current = Import;

                            if (Import.Label == "Shared") {
                                var File = Path.Combine(Directory, Import.Project);
                                var Shared = new VSProject(File, true) {
                                    ProjectType = ProjectType.Shared,
                                    RelativeDirectory = Path.GetDirectoryName(Import.Project)
                                    };
                                SharedProject.Add(Shared);

                                }


                            break;
                            }
                        case "None": {
                            None = new NoneType();
                            ItemGroup.None.Add(None);
                            None.Include = XmlReader.GetAttribute("Include");
                            None.Update = XmlReader.GetAttribute("Update");
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
                            if (AssemblyName == "meshman") {
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


        public void Parse (TextReader TextReader, bool Expand, string FilePath) {
            FuckerParse2(TextReader);

            foreach (var PropertyGroup in Project.PropertyGroup) {
                OutputType ??= PropertyGroup.OutputType;
                AssemblyName ??= PropertyGroup.AssemblyName;
                ProjectGuid ??= PropertyGroup.ProjectGuid;
                ProjectTypeGuids ??= PropertyGroup.ProjectTypeGuids;
                }

            AssemblyName ??= Path.GetFileNameWithoutExtension(FilePath);

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


    public partial class NoneType {
        public static Dictionary<string, BuildDescription> BuildTypes = new Dictionary<string, BuildDescription>() {
            { "commandcs", new BuildDescription ("cs", "commandparse", " /cs ") },
            { "verbcs", new BuildDescription ("cs", "verb", " /cs ") },
            { "fsrcs", new BuildDescription ("cs", "fsrgen", " /cs ") },
            { "exceptional", new BuildDescription("cs", "exceptional", " /cs ") },
            { "constant", new BuildDescription("cs", "constant", " /cs ") },
            { "gscript", new BuildDescription ("cs", "gscript") },
            { "goedel3", new BuildDescription ("cs", "goedel3", " /cs ") },
            { "asn2cs", new BuildDescription ("cs", "asn2", " /cs ") },
            { "domainercs", new BuildDescription ("cs", "domainer") },
            { "registrycs", new BuildDescription ("cs", "registryconfig") },
            { "vsixbuild", new BuildDescription ("cs", "vsixbuild") },
            { "protogen", new BuildDescription ("cs", "protogen", " /cs ") },
            { "rfc2txt", new BuildDescription ("txt", "rfctool") },
            { "rfc2xml", new BuildDescription("xml", "rfctool") },
            { "rfc2md", new BuildDescription ("md", "rfctool") },
            { "rfc2html", new BuildDescription ("html", "rfctool") },
            { "md2aml", new BuildDescription ("aml", "rfctool") },
            };

        BuildDescription _BuildDescription = null;
        BuildDescription BuildDescription {
            get {
                _BuildDescription ??= GetBuildDescription();
                return _BuildDescription;
                }
            }


        BuildDescription GetBuildDescription () {
            if (Generator == null) {
                return null;
                }
            BuildTypes.TryGetValue(Generator.ToLower(), out var Result);
            return Result;
            }


        public bool BuildTool => BuildDescription != null;

        public string BuildExtension => BuildDescription?.Extension;

        public string BuildCommand => BuildDescription?.Command;

        public string BuildFlag => BuildDescription?.Flag;

        public string BuildTarget => Path.ChangeExtension(BuildSource, BuildExtension);

        public string BuildSource => (Include ?? Update).StripMacro();
        }




    public partial class BuildDescription {
        public string Extension;

        public string Command;

        public string Flag;

        public BuildDescription (string Extension, string Command, string Flag="") {

            this.Extension = Extension;
            this.Command = Command;
            this.Flag = Flag;
            }

        }

    public partial class ProjectReferenceType {
        [System.Xml.Serialization.XmlIgnore]
        public VSProject SubProject;
        }
    }