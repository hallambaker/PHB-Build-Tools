using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// should add in a class that the _Parser class can inherit from.
// Will make it easier to then pass the parser to the lexer as a class rather than
// as a delegate.


namespace Registry {

    public class Parser {
        }
    
    struct StackItem {
        }


    public class File {
        string _Name;
        public string Name {
            get { return _Name;}
            private set { _Name = value;}
            }

        public File(string NameIn) {
            Name = NameIn;
            }
        }
    
    public class Position {
        public File             File;
        public int              Ln = 0;
        public int              Col = 0;
        public int              Ch = 0;

        public override string ToString() {
            return ("Line "+ Ln.ToString() + " Col "+ Col.ToString () + " in :" + File.Name );
            }

        public Position(string NameIn) {
            File = new File (NameIn);
            }
        }

    public class Registry {
        public List <File>     Files = new List<File> ();
        public List <TYPE>     Types = new List<TYPE> ();
        public List <ID>       IDs = new List<ID> ();

        public Registry() {
            }

        //http://msdn.microsoft.com/en-us/library/x0b5b5bc.aspx

        public File SetFile (string file) {
            File result = new File (file);
            Files.Add (result);
            return result;
            }

        public TYPE FindType (string Token) {
            TYPE result = Types.Find (
                delegate (TYPE T) {
                    return T.Label == Token;
                    } );
            return result;   
            }

        private ID FindID(string Token, TYPE Type) {
            try {
                ID result = IDs.Find(
                    delegate(ID T) {
                        return T.Label == Token;
                        });
                return result;
                }
            catch {
                return null;
                }
            }

        public TYPE TYPE (string Text) {
            TYPE result = new TYPE ();

            result.Label = Text;
            Types.Add (result);
            return result;

            }

        public ID ID (Position Position, string Text, TYPE Type, Object ObjectIn) {
            ID ID = FindID (Text, Type);

            if (ID != null) {
                if (ID.Declared) {
                    throw new Exception ("Label already defined");
                    }
                ID.Object = ObjectIn;
                ID.Declared = true;
                return ID;
                }
            else {
                ID = new ID(Position, Text, Type, true, ObjectIn);
                IDs.Add (ID);
                //Type.IDs.Add (ID);
                return ID;
                }
            }

        public REF REF (Position Position, string Text, TYPE Type, Object ObjectIn) {
            ID ID = FindID (Text, Type);

            if (ID == null) {
                ID = new ID(Position, Text, Type, false, null);
                IDs.Add (ID);
                //Type.IDs.Add (ID);
                }
            
            REF result = new REF (Position, ID, ObjectIn);

            return result;
            }

        public TOKEN TOKEN (Position Position, string Text, TYPE Type, Object ObjectIn) {
            ID ID = FindID (Text, Type);

            if (ID == null) {
                ID = new ID(Position, Text, Type, false, null);
                IDs.Add (ID);
                //Type.IDs.Add (ID);
                }
            
            TOKEN result = new TOKEN (Position, ID, ObjectIn);

            return result;           
            }
        }

    public class TYPE {
        public string           Label;
        public List <ID>        IDs = new List<ID> ();
        }
    
    public class ID {
        public Position         Position;
        public string           Label;
        public TYPE             Type;
        public List <REF>       REFs;
        public bool             Declared;
        public Object           Object;

        public ID(Position PositionIn, string LabelIn, TYPE Type, bool DeclaredIn, Object ObjectIn) {
            Position = PositionIn;
            Label = LabelIn;
            Declared = DeclaredIn;
            REFs = new List <REF> ();
            Object = ObjectIn;
            Type.IDs.Add (this);

            }

        public override string ToString() {
            return Label;
            }
        }

    public class REF {
        public Position        Position;
        public ID              ID;
        public Object           Object;

        public Object Definition {
            get { return ID == null ? null : ID.Object; }
            }

        public REF() {
            }
        public REF(Position PositionIn, ID IDIn, Object ObjectIn) {
            Position = PositionIn;
            ID = IDIn;
            Object = ObjectIn;
            ID.REFs.Add (this);
            }

        public override string ToString() {
            return ID.Label;
            }

        public string Label {
            get { return ToString(); }
            }
        }

    public class TOKEN : REF {

        public TOKEN(Position PositionIn, ID IDIn, Object ObjectIn) {
            Position = PositionIn;
            ID = IDIn;
            Object = ObjectIn;
            ID.REFs.Add(this);
            }
        public override string ToString() {
            return ID.Label;
            }
        }

    }
