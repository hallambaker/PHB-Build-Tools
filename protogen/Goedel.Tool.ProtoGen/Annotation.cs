using System;
using System.Collections.Generic;
using System.Text;
using Goedel.Registry;

// TODO: Should add a summary of each object/message into the markdown documentation output


namespace Goedel.Tool.ProtoGen {


    public interface IEntries {
        public List<_Choice> _Choices { get;}
        }
    public partial class ProtoStruct : Parser {
        bool HaveRun = false;


        virtual public void Complete() {
            if (!HaveRun) {
                HaveRun = true;
                // Set all the parents first to avoide weirdness due to inheritance
                foreach (ProtoGen._Choice Entry in Top) {
                    Entry.SetParent(null);
                    }

                foreach (ProtoGen._Choice Entry in Top) {
                    Entry.Complete(null);
                    }
                }
            }

        }

    public abstract partial class _Choice {
        public bool             IsAbstract = false;
        public _Choice          Superclass = null;
        public List<_Choice>    Subclasses = new() ;


        public virtual void SetParent(_Choice parent) {
            Parent = parent;

            if (this is IEntries entries) {
                foreach (_Choice entry in entries._Choices) {

                    entry.SetParent(this);

                    switch (entry) {
                        case CamelCase: {
                            AssignedTypeCase = TypeCase.CamelCase;
                            break;
                            }
                        case PascalCase: {
                            AssignedTypeCase = TypeCase.PascalCase;
                            break;
                            }
                        case SnakeCase: {
                            AssignedTypeCase = TypeCase.SnakeCase;
                            break;
                            }
                        }
                    }

                }

            }


        virtual public void Complete(_Choice parent) {
            //Console.WriteLine ("Completing");
            Parent = parent;
            Normalize();
            }

        static public void Complete(List<_Choice> Entries, _Choice parent) {
            foreach (_Choice Entry in Entries) {
                Entry.Complete(parent);
                }
            }
        }


    public partial class Protocol : IEntries {

        public List<_Choice> _Choices => Entries;

        public override void  Complete(_Choice parent) {
            //Console.WriteLine ("Completing {0}", Id.ToString());
            Parent = parent;
            Normalize();
            foreach (_Choice Entry in Entries) {
                Entry.Complete(this);
                }
            }
        }

    public partial class Transaction : IEntries {
        public List<_Choice> _Choices => Entries;

        public override void Complete(_Choice parent) {
            //Console.WriteLine ("Completing Transaction {0}", Id.ToString());
            Parent = parent;
            Complete(Entries, parent);
            }
        }

    public partial class Class : _Choice {

        }


    public partial class Message : IEntries {
        public List<_Choice> _Choices => Entries;
        public override void  Complete(_Choice parent) {
            //Console.WriteLine ("Completing Message {0}", Id.ToString());

            foreach (_Choice Entry in Entries) {
                Entry.Parent = this;
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

    public partial class Structure : IEntries {

        public List<_Choice> _Choices => Entries;
        public override void  Complete(_Choice parent) {
            //Console.WriteLine ("Completing Structure {0}", Id.ToString());

            foreach (_Choice Entry in Entries) {
                Entry.Parent = this;
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
