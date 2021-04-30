using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.Tool.Constant {
    public partial class Constant {


        public Namespace Namespace;

        public string NameSpaceName => Namespace.Id.Label;
        public string Class => Namespace.Class.Label;


        public List<_Choice> All = new List<_Choice>();
        public List<File> Files = new List<File>();

        public List<_Choice> Entries = new List<_Choice>();

        public List<Enum> Enums = new List<Enum>();

        public override void Init() {

            if (_Initialized) {
                return;
                }

            foreach (var entry in Top) {
                All.Add(entry);
                switch (entry) {
                    case File File: {
                        this.Files.Add(File);

                        foreach (var item in File.Entries) {
                            Entries.Add(item);

                            switch (item) {
                                case Enum Enum: {
                                    Enum.Init(entry);
                                    Enums.Add(Enum);
                                    break;
                                    }
                                }


                            }

                        break;
                        }
                    case Namespace nameSpace: {
                        Namespace = nameSpace;
                        break;
                        }
                    }
                entry.Init(null);
                }

            }
        }
    public partial class Enum {
        public override void Init(_Choice Parent) {
            foreach (var item in Code) {
                item.Init(this);
                }
            foreach (var item in UDF) {
                item.Init(this);
                }
            foreach (var item in Integer) {
                item.Init(this);
                }
            foreach (var item in Tag) {
                item.Init(this);
                }
            }
        }

    public partial class UDF {
        public string Title;
        public string Id;

        public override void Init(_Choice Parent) {
            Title = "" ;
            Id = Class.Label;

            if (Algorithm?.Id != null) {
                Title = Class.Label + " " + Algorithm.Id.Label;
                Id += "_" + Algorithm.Id.Label;
                }

            if (Compress.Bits > 0) {
                Title += $" ({Compress.Bits} bits compressed)";
                Id += "_" + $"{Compress.Bits}";
                }
            if (Note?.Text != null) {
                Title = Note.Text;
                }
            }
        }

    public partial class Code {
        public override void Init(_Choice Parent) {


            }
        }
    }