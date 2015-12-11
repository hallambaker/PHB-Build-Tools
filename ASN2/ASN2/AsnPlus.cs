using System;
using System.Collections.Generic;
using System.Text;
using Goedel.Registry;

namespace Goedel.ASN {
    public partial class ASN2 : Parser {
        bool HaveRun = false;


        virtual public void Complete() {
            if (!HaveRun) {
                HaveRun = true;
                foreach (_Choice Entry in Top) {
                    Entry.Complete();
                    }
                }
            }

        }


    public abstract partial class _Choice {
        public List<OID> Children = new List<OID>();

        public int[] Binary = null;



        virtual public void Complete() {
            //Console.WriteLine ("Completing");
            }

        virtual public void MakeBinary() {
            }

        }




    public partial class Member {

        public bool Context = false;
        public bool Optional = false;
        public bool Implicit = false;
        public bool Explicit = false;
        public string Default = null;

        public int Code = -1;
        public int Flags = (int) ASNFlags.Nil;

        bool Once = true;

        private void SetFlags() {
            Flags = (int)  ((Optional ? ASNFlags.Optional : ASNFlags.Nil) | 
                    (Implicit ? ASNFlags.Implicit : ASNFlags.Nil) |
                    (Explicit ? ASNFlags.Explicit : ASNFlags.Nil) |
                    (Context ? ASNFlags.Context : ASNFlags.Nil) 
                    ) ;
            }

        public override void Complete() {
            if (Once) {
                foreach (Qualifier Qualifier in Qualifiers) {
                    _Choice Entry = Qualifier.Entry;

                    if (Entry._Tag() == ASN2Type.Context) {
                        Context = true;
                        }
                    if (Entry._Tag() == ASN2Type.Implicit) {
                        Implicit = true;
                        }
                    if (Entry._Tag() == ASN2Type.Explicit) {
                        Explicit = true;
                        }
                    if (Entry._Tag() == ASN2Type.Code) {
                        Code = ((Code)Entry).Value;
                        }
                    if (Entry._Tag() == ASN2Type.Optional) {
                        Optional = true;
                        }
                    if (Entry._Tag() == ASN2Type.Default) {
                        Default = ((Default)Entry).Value;
                        }
                    }
                SetFlags();

                if (Spec._Tag() == ASN2Type.Choice) {
                    Choice Choice = (Choice) Spec;
                    foreach (Member Sub in Choice.Entries) {
                        Sub.Optional = true;
                        Sub.SetFlags();
                        }
                    }

                }
            Once = false;
            }
        }

    public partial class ROOT {
        public override void Complete() {
            //Console.WriteLine ("Completing");

            if (Binary == null) MakeBinary();
            }

        public override void MakeBinary() {

            Binary = new int[Entries.Count];
            int i = 0;
            foreach (Entry Entry in Entries) {
                Binary[i++] = Entry.Value;
                }
            }
        }

    public partial class OID {
        public override void Complete() {
            Root.Definition.Children.Add(this);
            if (Binary == null) MakeBinary();
            }

        public override void MakeBinary() {
            if (Root.Definition.Binary == null) Root.Definition.MakeBinary();

            Binary = new int[Root.Definition.Binary.Length + 1];
            for (int i = 0; i < Root.Definition.Binary.Length; i++) {
                Binary[i] = Root.Definition.Binary[i];
                }
            Binary[Root.Definition.Binary.Length] = Value;
            }
        }


    public partial class Class {
        bool Once = true;
        public override void Complete() {
            if (Once) {
                Entries.Reverse();
                foreach (Member Member in Entries) {
                    Member.Complete();
                    }
                }
            Once = false;
            }
        }

    public partial class Object {
        bool Once = true;
        public override void Complete() {
            if (Once) {
                Entries.Reverse();
                foreach (Member Member in Entries) {
                    Member.Complete();
                    }
                }
            Once = false;
            }
        }

    public partial class SingularObject {
        bool Once = true;
        public override void Complete() {
            if (Once) {
                Entries.Reverse();
                foreach (Member Member in Entries) {
                    Member.Complete();
                    }
                }
            Once = false;
            }
        }
    }

