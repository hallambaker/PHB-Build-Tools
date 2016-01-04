using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.MarkLib;

namespace Goedel.WordLib {
    public class Dispatch {
        public static Document Process(DocumentSet Parent, FileInfo FileInfo,
                    TagCatalog TagCatalog) {

            Console.WriteLine("Parse {0}", FileInfo.Name);

            switch (FileInfo.Extension) {
                case ".doc":
                case ".docx": {
                        return WordDocument.Create(Parent, FileInfo, TagCatalog);
                        }
                default: {
                        Console.WriteLine("No handler for file type {0}", FileInfo.Extension);
                        break;
                        }
                }

            return null;
            }
        }
    }