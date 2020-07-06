using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Command;
using Goedel.Registry;
using Goedel.IO;

namespace Goedel.Shell.DNSConfig {

    // The stub class just contains routines that echo their arguments and
    // write 'not yet implemented'

    // Eventually there will be a compiler option to suppress the debugging
    // to eliminate the redundant code
    public partial class DNSConfigShell : _DNSConfigShell {

        public override void DNS (DNS Options) {
            string inputfile = null;

            inputfile = Options.DNSConfig.Text;

            Goedel.Tool.DNSConfig.DNSConfig Parse = new Goedel.Tool.DNSConfig.DNSConfig() {
                };


            using (Stream infile =
                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {

                Lexer Schema = new Lexer(inputfile);

                Schema.Process(infile, Parse);
                }

            Parse.Init();

            using (var OutputWriter = "named.conf.options".OpenTextWriterNew()) {
                var Script = new Goedel.Tool.DNSConfig.Generate(OutputWriter);
                Script.GenerateOptions(Parse);
                }

            using (var OutputWriter = "named.conf.local".OpenTextWriterNew()) {
                var Script = new Goedel.Tool.DNSConfig.Generate(OutputWriter);
                Script.GenerateLocal(Parse);
                }

            foreach (var Domain in Parse.Domains) {
                var FileName = "zones/db." + Domain.Id.Label;

                using var OutputWriter = FileName.OpenTextWriterNew();
                var Script = new Goedel.Tool.DNSConfig.Generate(OutputWriter);
                Script.GenerateDomain(Domain);
                }

            using (var OutputWriter = "named.addresses".OpenTextWriterNew()) {
                var Script = new Goedel.Tool.DNSConfig.Generate(OutputWriter);
                Script.GenerateAdressRecords(Parse);
                }
            }


        } // class _DNSConfigShell

    }
