using System;

namespace Drafts {
    class Program {
        static void Main(string[] args) {
            

            var program = new Program();
            program.Start();

            }

        public bool All = true;

        void Start() {
            Console.WriteLine("Hello World!");
            MakeDocs();
            }

        public void MakeDocs() {
            var Process = All ? System.Diagnostics.Process.Start("CMD.exe", "/C Scripts\\MakeDocs") :
                System.Diagnostics.Process.Start("CMD.exe", "/C Scripts\\MakeOneDoc");
            Process.WaitForExit();
            }
        }
    }
