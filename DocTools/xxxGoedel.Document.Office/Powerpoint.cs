using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using MT = Microsoft.Office.Core.MsoTriState;


// To make use of these we need to install
//     The COM interface for Visio - in the Visio SDK ???
//     The Microsoft Office Primary Interop Assemblies

namespace Goedel.Document.Office {
    public class PowerPoint1 {


        public static void Process(string Doc, string To) {
            string Catalog = To + @"\" +
                    System.IO.Path.GetFileNameWithoutExtension(Doc) + ".txt";

            if (!ImageUtil.RemakeTarget(Doc, Catalog)) {
                return;
                }

            string Stem = To + @"\" +
                        System.IO.Path.GetFileNameWithoutExtension(Doc) + "_";

            using (var FileStream = new FileStream(Catalog, FileMode.Create)) {
                using (var CatalogWriter = new StreamWriter(FileStream)) {
                    CatalogWriter.WriteLine("Input:{0}", Doc);

                    var application = new PowerPoint.Application();
                    application.Visible = MT.msoTrue;

                    var Presentation = application.Presentations.Open(Doc,
                            MT.msoTrue, MT.msoFalse, MT.msoFalse);

                    foreach (PowerPoint.Slide Slide in Presentation.Slides) {
                        Console.WriteLine("Got Slide {0}", Slide.Name);
                        var file = Stem + Slide.Name + ".png";

                        Slide.Export(file, "PNG");

                        CatalogWriter.WriteLine("Output:{0}", file);
                        }

                    application.Quit();
                    }
                }
            }

        }
    }
