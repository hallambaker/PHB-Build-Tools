using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Visio = Microsoft.Office.Interop.Visio;


// To make use of these we need to install
//     The COM interface for Visio - in the Visio SDK ???
//     The Microsoft Office Primary Interop Assemblies

namespace Goedel.Document.Office {
    public class Visio1 {

        public static void Process(string Doc, string To) {
            string Catalog = To + @"\" +
                    System.IO.Path.GetFileNameWithoutExtension(Doc) + ".txt";

            //if (!ImageUtil.RemakeTarget(Doc, Catalog)) {
            //    return;
            //    }

            string Stem = To + @"\" +
                        System.IO.Path.GetFileNameWithoutExtension(Doc) + "_";

            using (var FileStream = new FileStream(Catalog, FileMode.Create)) {
                using (var CatalogWriter = new StreamWriter(FileStream)) {

                    var application = new Visio.InvisibleApp(); // Application();

                    var Document = application.Documents.OpenEx(Doc,
                            ((short)Visio.VisOpenSaveArgs.visOpenDocked +
                             (short)Visio.VisOpenSaveArgs.visOpenRO));

                    CatalogWriter.WriteLine("Input:{0}", Doc);

                    foreach (Visio.Page Page in Document.Pages) {
                        var file = Path.GetFullPath(Stem + Page.Name + ".png");
                        Console.WriteLine("Got Page {0}", file);
                        Page.Export(file);

                        CatalogWriter.WriteLine("Output:{0}", file);
                        }


                    application.Quit();
                    }
                }
            }

        }
    }
