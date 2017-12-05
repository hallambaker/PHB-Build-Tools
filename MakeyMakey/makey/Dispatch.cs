using System;
using System.IO;

using System.Collections.Generic;
using Goedel.IO;
using Goedel.Command;
using Goedel.Tool.Makey;

namespace Goedel.Shell.Makey {
    public partial class MakeyShell {

        public override void Project (Project Options) {
            string Inputfile = Options.InputFile.Text;
            string Outputfile = Options?.OutputFile?.Text ?? "makefile";

            var InType = Path.GetExtension(Inputfile);
            if (InType == ".sln") {
                var Solution = new VSSolution(Inputfile);
                var SolutionPath = Path.GetDirectoryName(Inputfile);

                Solution.Directory = SolutionPath;

                var OutputFile = Path.Combine(SolutionPath, "Makefile");

                foreach (var Item in Solution.Projects) {
                    var ProjectFile = Path.Combine(SolutionPath, Item.Directory);
                    Console.WriteLine("Make Project {0}\n   {1}", ProjectFile, Item.TypeGUID);

                    

                    if (Item.Recurse) {


                        Item.Project = new VSProject(ProjectFile, true) {
                            ProjectType = Item.ProjectType
                            };

                        var ProjectPath = Path.GetDirectoryName(Item.Directory);
                        var TargetFile = Path.Combine(SolutionPath, ProjectPath, "Makefile");

                        Console.WriteLine("Make Project {0} -> {1}", ProjectFile, TargetFile);

                        //using (var outputStream = TargetFile.OpenFileNew()) {
                        //    using (var outputText = outputStream.OpenTextWriter()) {
                        //        var Generate = new Generate(outputText);

                        //        Generate.GenerateMakefile(Item.Project);
                        //        }
                        //    }

                        var TargetFile2 = Path.Combine(SolutionPath, ProjectPath, "VS.Make");

                        var Project = (Item.ProjectType == ProjectType.shared) ?
                                Item.Project.SharedProject[0] : Item.Project;

                        using (var outputStream = TargetFile2.OpenFileNew()) {
                            using (var outputText = outputStream.OpenTextWriter()) {
                                var Generate = new Generate(outputText);

                                Generate.GenerateVSMakefile(Project);
                                }
                            }

                        }

                    }

                }

            }


        ///// <summary>
        ///// Convert project file
        ///// </summary>
        ///// <param name="Options">Command line parameters</param>
        //public override void Project(Project Options) {


        //    string Inputfile = Options.InputFile.Text;
        //    string Outputfile = Options?.OutputFile?.Text ?? "makefile";

        //    if (Outputfile == null) {
        //        Outputfile = Path.GetFileNameWithoutExtension(Inputfile) +
        //            "." + Options.OutputFile.Extension;
        //        }
        //    if (Options.Lazy.Value & FileTools.UpToDate(Inputfile, Outputfile)) {
        //        return;
        //        }

        //    var InType = Path.GetExtension(Inputfile);

        //    if (InType == ".sln") {
        //        var Solution = new VSSolution(Inputfile);
        //        var SolutionPath = Path.GetDirectoryName(Inputfile);

        //        Solution.Directory = SolutionPath;

        //        var OutputFile = Path.Combine(SolutionPath, "Makefile");

        //        foreach (var Item in Solution.Projects) {

        //            if (Item.Recurse()) {
        //                var ProjectFile = Path.Combine(SolutionPath, Item.Directory);

        //                Item.Project = new VSProject(ProjectFile, true);

        //                var ProjectPath = Path.GetDirectoryName(Item.Directory);
        //                var TargetFile = Path.Combine(SolutionPath, ProjectPath, "Makefile");

        //                Console.WriteLine("Make Project {0} -> {1}", ProjectFile, TargetFile);

        //                using (var outputStream = TargetFile.OpenFileNew()) {
        //                    using (var outputText = outputStream.OpenTextWriter()) {
        //                        var Generate = new Generate(outputText);

        //                        Generate.GenerateMakefile(Item.Project);
        //                        }
        //                    }

        //                var TargetFile2 = Path.Combine(SolutionPath, ProjectPath, "VS.Make");
        //                using (var outputStream = TargetFile2.OpenFileNew()) {
        //                    using (var outputText = outputStream.OpenTextWriter()) {
        //                        var Generate = new Generate(outputText);

        //                        Generate.GenerateVSMakefile(Item.Project);
        //                        }
        //                    }

        //                }
        //            }

        //        using (var outputStream = OutputFile.OpenFileNew()) {
        //            using (var outputText = outputStream.OpenTextWriter()) {
        //                var Generate = new Generate(outputText);
        //                Generate.GenerateMakefile(Solution);
        //                }
        //            }
        //        }
        //    else if (InType == ".csproj") {
        //        Console.WriteLine("Process project {0} to {1}", Inputfile, Outputfile);

        //        var Project = new VSProject(Inputfile, true); 

        //        using (var outputStream =Outputfile.OpenFileNew()) {
        //            using (var outputText = outputStream.OpenTextWriter()) {
        //                var Generate = new Generate(outputText);

        //                Generate.GenerateMakefile(Project);
        //                }
        //            }
        //        }
        //    }
        }
    }
