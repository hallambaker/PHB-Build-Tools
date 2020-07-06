using System;
using System.IO;
using System.Collections.Generic;
using Goedel.FSR;
//using Goedel.Registry;
//using Goedel.Utilities;

namespace Goedel.Tool.Makey {

    public partial class VSFile {

        }


    public enum ProjectType {
        NULL,
        Folder,
        Csproj,
        Wixproj,
        Cproj,
        Portable,
        Website,
        Vsix,
        Csstandard,  // includes core
        Shared
        }

    /// <summary>
    /// Represents a Visual Studio solution.
    /// 
    /// NB, since the format of the file is not published, it is not possible
    /// to know with any certainty how Visual Studio parses it. In particular
    /// there may be features of the file format that have not been expressed
    /// in any of the examples seen.
    /// 
    /// In particular, we have no way to tell if the file format supports 
    /// comments or if there is some line wrapping mechanism supported.
    /// </summary>
    public partial class VSSolution : VSFile {
        readonly static public Dictionary<string, ProjectType> MapProject = new Dictionary<string, ProjectType> {
                { "{2150e333-8fdc-42a3-9474-1a3956d46de8}", ProjectType.Folder},
                { "{fae04ec0-301f-11d3-bf4b-00c04f79efbc}", ProjectType.Csproj},
                { "{930c7802-8a8c-48f9-8165-68863bccd9dd}", ProjectType.Wixproj},
                { "{8bc9ceb8-8b4a-11d0-8d11-00a0c91bc942}", ProjectType.Cproj },
                { "{786c830f-07a1-408b-bd7f-6ee04809d6db}", ProjectType.Portable },
                { "{82b43b9b-a64c-4715-b499-d71e9ca2bd60}", ProjectType.Vsix},
                { "{9a19103f-16f7-4668-be54-9a1e7a4f7556}", ProjectType.Csstandard},
                { "{d954291e-2a0b-460d-934e-dc6b0785db48}", ProjectType.Shared},
                { "{e24c65dc-7377-472b-9aba-bc803b73c61a}", ProjectType.Website}
            };


        public string Directory;

        public string FormatVersion { get; set; }
        public string VisualStudioVersion { get; set; }
        public string MinimumVisualStudioVersion { get; set; }

        public List<SolutionProject> Projects { get; set; } = new List<SolutionProject>();

        public List<KeyValue> SolutionConfigurationPlatforms = new List<KeyValue>();
        public List<KeyValue> ProjectConfigurationPlatforms = new List<KeyValue>();
        public List<KeyValue> SolutionProperties = new List<KeyValue>();
        public List<KeyValue> NestedProjects = new List<KeyValue>();

        public Dictionary<string, SolutionProject> ProjectsByGUID = new Dictionary<string, SolutionProject>();

        public VSSolution(string Filename) => Parse(Filename);

        public void Parse(string Filename) {
            using var FileStream = new FileStream(
                        Filename, FileMode.Open, FileAccess.Read);
            LexReader LexReader = new LexReader(FileStream);
            Parse(LexReader);

            }



        enum Target {
            Base,
            Project,
            SolutionItems,
            ProjectDependencies,
            SolutionConfigurationPlatforms,
            ProjectConfigurationPlatforms,
            SolutionProperties,
            NestedProjects,
            Unknown
            }


        public void Parse(LexReader LexReader) {
            Stack<Target> Stack = new Stack<Target>();

            SolutionProject SolutionProject = null;


             {
                var Lexer = new Tokenizer(LexReader);

                var Token = Lexer.GetToken();
                while (Token != Tokenizer.Token.Empty) {
                    //Console.WriteLine("Token {0}", Token);

                    switch (Token) {
                        case Tokenizer.Token.Start: {
                            var Push = Target.Unknown;

                            switch (Lexer.Tag) {
                                case "Project": {
                                    SolutionProject = new SolutionProject(Lexer);
                                    Projects.Add(SolutionProject);

                                    ProjectsByGUID.Add(SolutionProject.ThisGUID, SolutionProject);

                                    Push = Target.Project;
                                    break;
                                    }
                                case "ProjectSection": {
                                    if (Lexer.Key == "ProjectDependencies") {
                                        Push = Target.ProjectDependencies;
                                        }
                                    else if (Lexer.Key == "SolutionItems") {
                                        Push = Target.SolutionItems;
                                        }
                                    break;
                                    }
                                case "Global": {
                                    break;
                                    }
                                case "GlobalSection": {
                                    if (Lexer.Key == "SolutionConfigurationPlatforms") {
                                        Push = Target.SolutionConfigurationPlatforms;
                                        }
                                    else if (Lexer.Key == "ProjectConfigurationPlatforms") {
                                        Push = Target.ProjectConfigurationPlatforms;
                                        }
                                    else if (Lexer.Key == "SolutionProperties") {
                                        Push = Target.SolutionProperties;
                                        }
                                    else if (Lexer.Key == "NestedProjects") {
                                        Push = Target.NestedProjects;
                                        }
                                    break;
                                    }
                                }

                            Stack.Push(Push);
                            break;
                            }

                        case Tokenizer.Token.End: {
                            if (Stack.Count > 0) {
                                Stack.Pop();
                                }
                            break;
                            }

                        case Tokenizer.Token.TagValue: {
                            if (Stack.Count == 0) {
                                switch (Lexer.Tag) {
                                    case "VisualStudioVersion": {
                                        VisualStudioVersion = Lexer.Value;
                                        break;
                                        }
                                    case "MinimumVisualStudioVersion": {
                                        MinimumVisualStudioVersion = Lexer.Value;
                                        break;
                                        }
                                    }
                                }
                            else if (Stack.Peek() == Target.ProjectDependencies) {
                                var KeyValue = new KeyValue(Lexer);
                                SolutionProject?.ProjectDependencies.Add(KeyValue);
                                }
                            else if (Stack.Peek() == Target.SolutionItems) {
                                var KeyValue = new KeyValue(Lexer);
                                SolutionProject?.SolutionItems.Add(KeyValue);
                                }
                            else if (Stack.Peek() == Target.SolutionConfigurationPlatforms) {
                                var KeyValue = new KeyValue(Lexer);
                                SolutionConfigurationPlatforms.Add(KeyValue);
                                }
                            else if (Stack.Peek() == Target.ProjectConfigurationPlatforms) {
                                var KeyValue = new KeyValue(Lexer);
                                ProjectConfigurationPlatforms.Add(KeyValue);
                                }
                            else if (Stack.Peek() == Target.SolutionProperties) {
                                var KeyValue = new KeyValue(Lexer);
                                SolutionProperties.Add(KeyValue);
                                }
                            else if (Stack.Peek() == Target.NestedProjects) {
                                var KeyValue = new KeyValue(Lexer);
                                NestedProjects.Add(KeyValue);
                                }
                            break;
                            }

                        case Tokenizer.Token.Line: {
                            //Console.WriteLine(Lexer.Tag);
                            break;
                            }
                        }


                    Token = Lexer.GetToken();
                    }
                }
            //catch (System.Exception Inner) {
            //    throw new ParseError(LexReader, Inner);
            //    }
            }


        public SolutionProject ByGuid (string GUID) {
            if (GUID == null) { return null; }

            ProjectsByGUID.TryGetValue(GUID, out var Result);

            return Result;
            }

        }


    public partial class SolutionProject {
        public string TypeGUID { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string ThisGUID { get; set; }

        public ProjectType ProjectType => _ProjectType;

        public VSProject Project;
        ProjectType _ProjectType = ProjectType.NULL; 

        public List<KeyValue> SolutionItems = new List<KeyValue>();
        public List<KeyValue> ProjectDependencies = new List<KeyValue>();
        
        public SolutionProject(Tokenizer Tokenizer) {
            TypeGUID = Tokenizer.Key;
            Name = Tokenizer.Value0;
            Directory = Tokenizer.Value1;
            ThisGUID = Tokenizer.Value2;

            VSSolution.MapProject.TryGetValue(TypeGUID.ToLower(), out _ProjectType);
            }

        public static bool IsRecurse (ProjectType ProjectType) {
            switch (ProjectType) {
                case ProjectType.Cproj:
                case ProjectType.Csproj:
                case ProjectType.Csstandard:
                case ProjectType.Shared: {
                    return true;
                    }
                }

            return false;
            }

        public bool Recurse => IsRecurse(ProjectType);

        readonly List<string> KillGUIDs = new List<string> {
            "{82b43b9b-a64c-4715-b499-d71e9ca2bd60}"
            };

        public bool MakeUnix () {
            if (Project == null) {
                return false;
                }
            if (Project.ProjectTypeGuids != null) {
                foreach (var Kill in KillGUIDs) {
                    if (Project.ProjectTypeGuids.IndexOf(Kill) >= 0) {
                        return false;
                        }
                    }
                Console.WriteLine("Projects{0}", Project.AssemblyName);
                Console.WriteLine("GUIDS: {0}", Project.ProjectTypeGuids);
                }

            return Recurse;
            }

        }


    public partial class KeyValue {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValue(Tokenizer Tokenizer) {
            Key = Tokenizer.Tag;
            Value = Tokenizer.Value;
            }

        }

    }