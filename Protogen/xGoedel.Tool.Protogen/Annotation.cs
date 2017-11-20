using System;
using System.Collections.Generic;
using System.Text;
using Goedel.Registry;

namespace Goedel.Tool.ProtoGen {
    public partial class ProtoStruct : Parser {
        bool HaveRun = false;


        virtual public void Complete() {
            if (!HaveRun) {
                HaveRun = true;
                foreach (ProtoGen._Choice Entry in Top) {
                    Entry.Complete();
                    }
                }
            }

        }

    public abstract partial class _Choice {
        public bool             IsAbstract = false;
        public _Choice          Superclass = null;
        public List<_Choice>    Subclasses = new List<_Choice> () ;

        virtual public void Complete() {
            //Console.WriteLine ("Completing");
            Normalize();
            }

        static public void Complete (List<_Choice> Entries) {
            foreach (_Choice Entry in Entries) {
                Entry.Complete ();
                }
            }
        }


    public partial class Protocol : _Choice {



        public override void  Complete() {
            //Console.WriteLine ("Completing {0}", Id.ToString());

            Normalize();
            foreach (_Choice Entry in Entries) {
                Entry.Complete ();
                }
            }
        }

    public partial class Transaction : _Choice {


        public override void  Complete() {
            //Console.WriteLine ("Completing Transaction {0}", Id.ToString());

            Complete(Entries);
            }
        }

    public partial class Class : _Choice {

        }


    public partial class Message : _Choice {

        public override void  Complete() {
            //Console.WriteLine ("Completing Message {0}", Id.ToString());

            foreach (_Choice Entry in Entries) {
                if (Entry._Tag() == ProtoStructType.Inherits) {
                    Inherits Inherits = (Inherits) Entry;
                    Superclass = (Message) (Inherits.Ref.ID.Object) ;
                    Superclass.Subclasses.Add (this);
                    Inherits.Ref.Object = this; 
                    }
                if (Entry._Tag() == ProtoStructType.Abstract) {
                    IsAbstract = true;
                    }
                }
            }
        }

    public partial class Structure : _Choice {


        public override void  Complete() {
            //Console.WriteLine ("Completing Structure {0}", Id.ToString());

            foreach (_Choice Entry in Entries) {
                if (Entry._Tag() == ProtoStructType.Inherits) {
                    Inherits Inherits = (Inherits) Entry;
                    Superclass =(Inherits.Ref.ID.Object) as Structure;
                    Superclass?.Subclasses.Add (this); // If this is a superclass, add it.
                    Inherits.Ref.Object = this; 
                    }
                if (Entry._Tag() == ProtoStructType.Abstract) {
                    IsAbstract = true;
                    }
                }
            }
        }

    }
