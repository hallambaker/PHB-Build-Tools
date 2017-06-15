using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.IO {
    public class DirectoryTools {



        public static void DirectoryDelete (string Path) {
            if (!Directory.Exists(Path)) {
                return;
                }

            try {
                var DirectoryInfo = new DirectoryInfo(Path);
                foreach (var Entry in DirectoryInfo.GetFiles()) {
                    Entry.Delete();
                    }

                Directory.Delete(Path, true);
                }
            catch {
                // Ignore failures, we have done as much as possible
                }

            }
        }
    }
