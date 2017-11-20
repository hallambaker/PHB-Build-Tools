using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


// To make use of these we need to install
//     The COM interface for Visio - in the Visio SDK ???
//     The Microsoft Office Primary Interop Assemblies

namespace Goedel.Document.Office {
    public class Excel1 {


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
 
                    var application = new Excel.Application();
                    application.Visible = false;

                    var Workbook = application.Workbooks.Open(Doc, Type.Missing, true);

                    CatalogWriter.WriteLine("Input:{0}", Doc);

                    foreach (Excel.Worksheet sheet in Workbook.Sheets) {
                        Console.WriteLine("Got Page {0}", sheet.Name);

                        var charts = (Excel.ChartObjects)sheet.ChartObjects();

                        foreach (Excel.ChartObject chartObject in charts) {
                            Excel.Chart chart = chartObject.Chart;

                            var file = Stem + chart.Name + ".png";
                            Console.WriteLine("Got Page {0}", file);

                            chart.Export(file, "PNG");

                            CatalogWriter.WriteLine("Output:{0}", file);
                            }
                        }

                    application.Quit();
                    }
                }
            }

        }
    }
