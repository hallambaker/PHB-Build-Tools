using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Goedel.Document.Office {
    public class Dispatch {
        public static void Process(string InPath, string OutPath) {
            int[] ImageSizes = new int[] { 500, 800, 1280 };

            Console.WriteLine("Process {0} to {1}", InPath, OutPath);
            string Extension = System.IO.Path.GetExtension(InPath).ToLower();

            switch (Extension) {
                case ".vsd": {
                    Visio1.Process(InPath, OutPath);
                    break;
                    }
                case ".xls":
                case ".xlsx": {
                    Excel1.Process(InPath, OutPath);
                    break;
                    }
                case ".ppt":
                case ".pptx": {
                    PowerPoint1.Process(InPath, OutPath);
                    break;
                    }
                case ".gif":
                case ".bmp":
                case ".png": {
                    //ImageUtil.ToPng(InPath, OutPath, ImageSizes);
                    //Console.WriteLine("Image resize not yet supported");
                    break;
                    }
                case ".jpg":
                case ".nef": {
                    //ImageUtil.ToPng(InPath, OutPath, ImageSizes);
                    ////Console.WriteLine("Image resize not yet supported");
                    break;
                    }

                default: {
                    Console.WriteLine("No handler for file type {0}", Extension);
                    break;
                    }
                }

            }

        }
    }
