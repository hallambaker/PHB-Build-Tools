using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// should add in a class that the _Parser class can inherit from.
// Will make it easier to then pass the parser to the lexer as a class rather than
// as a delegate.


namespace Goedel.Registry {

    public abstract class Parser {
        public Dispatch    Options;
        public virtual void Process(
                        TokenType Token, Position Position, string Text) {
            }
        }

    public abstract class Dispatch {
        public DateTime Started = DateTime.Now;
        public TimeSpan Elapsed { get { return DateTime.Now - Started; } }
        }


    public abstract class Type {
        public string TagText;
        public string Text;

        public Type() {
            }
        public Type(string Value) {
            Default(Value);
            }

        public override string ToString() {
            return Text;
            }

        public virtual void Register(string Tag, Registry Registry, int Index) {
            Registry.Register(Tag, Index);
            }
        public virtual int Tag(string TagIn) {
            TagText = TagIn;
            return 1;
            }
        public virtual void Parameter(string TextIn) {
            Text = TextIn;
            }

        public virtual void Default(string TextIn) {
            Parameter(TextIn);
            }
        public virtual string Usage(string Tag, string Value, char Usage) {
            if (Tag == null) {
                return Value;
                }
            return Usage + Tag + " " + Value;
            }
        }

    public class RegistryEntry {
        public string              Tag;
        public int                 Index;
        }

    public class Registry {
        List <RegistryEntry>        Entries = new List<RegistryEntry>();
        
        public void Register(string Tag, int Index) {
            RegistryEntry Entry = new RegistryEntry ();
            Entry.Tag = Tag;
            Entry.Index = Index;
            Entries.Add (Entry);
            }

		public int Find (string Match) {
			RegistryEntry Entry = Entries.Find(delegate(RegistryEntry TT) {return TT.Tag == Match; });

			if (Entry == null) {
				throw new Exception ("Unknown option: " + Match);
				}
			return Entry.Index;
			}

        }

    struct StackItem {
        }


    public class Source {
        string _Name;
        public string Name {
            get { return _Name;}
            private set { _Name = value;}
            }

        public Source(string NameIn) {
            Name = NameIn;
            }
        }
    
    public class Position {
        public Source             File;
        public int              Ln = 0;
        public int              Col = 0;
        public int              Ch = 0;

        public override string ToString() {
            return ("Line "+ Ln.ToString() + " Col "+ Col.ToString () + " in :" + File.Name );
            }

        public Position(string NameIn) {
            File = new Source (NameIn);
            }
        }

    public class Registry <T> where T : class  {
        public List <Source>     Files = new List<Source> ();
        public List <TYPE<T>>     Types = new List<TYPE<T>> ();
        public List <ID<T>>       IDs = new List<ID<T>> ();

        public Registry() {
            }

        //http://msdn.microsoft.com/en-us/library/x0b5b5bc.aspx

        public Source SetFile (string file) {
            Source result = new Source (file);
            Files.Add (result);
            return result;
            }

        public TYPE<T> FindType (string Token) {
            TYPE<T> result = Types.Find (
                delegate (TYPE<T> TT) {
                    return TT.Label == Token;
                    } );
            return result;   
            }

        private ID<T> FindID(string Token, TYPE<T> Type) {
            try {
                ID<T> result = IDs.Find(
                    delegate(ID<T> TT) {
                        return TT.Label == Token;
                        });
                return result;
                }
            catch {
                return null;
                }
            }

        public TYPE<T> TYPE (string Text) {
            TYPE<T> result = new TYPE<T> ();

            result.Label = Text;
            Types.Add (result);
            return result;

            }

        public ID<T> ID (Position Position, string Text, TYPE<T> Type, T ObjectIn) {
            //Console.WriteLine ("Declare ID {0}", Text);
            
            ID<T> ID = FindID (Text, Type);

            if (ID != null) {
                if (ID.Declared) {
                    throw new Exception ("Label already defined [" + Text + "]" );
                    }
                ID.Object = ObjectIn;
                ID.Declared = true;
                return ID;
                }
            else {
                ID = new ID<T> (Position, Text, Type, true, ObjectIn);
                IDs.Add (ID);
                //Type.IDs.Add (ID);
                return ID;
                }
            }

        public REF<T>  REF (Position Position, string Text, TYPE<T> Type, T ObjectIn)  {
            ID<T> ID = FindID (Text, Type);

            if (ID == null) {
                ID = new ID<T>(Position, Text, Type, false, null);
                IDs.Add (ID);
                //Type.IDs.Add (ID);
                }
            
            REF<T> result = new REF<T> (Position, ID, ObjectIn);

            return result;
            }

        public TOKEN<T> TOKEN (Position Position, string Text, TYPE<T> Type, T ObjectIn) {
            ID<T> ID = FindID (Text, Type);

            if (ID == null) {
                ID = new ID<T>(Position, Text, Type, false, null);
                IDs.Add (ID);
                //Type.IDs.Add (ID);
                }
            
            TOKEN<T> result = new TOKEN<T> (Position, ID, ObjectIn);

            return result;           
            }
        }

    public class TYPE <T> where T : class  {
        public string           Label;
        public List <ID <T>>        IDs = new List<ID <T>> ();
        }
    
    public class ID <T> where T : class  {
        public Position         Position;
        public string           Label;
        public TYPE <T>            Type;
        public List <REF<T>>       REFs;
        public bool             Declared;
        public T           Object;

        public ID(Position PositionIn, string LabelIn, TYPE <T> Type, bool DeclaredIn, T ObjectIn) {
            Position = PositionIn;
            Label = LabelIn;
            Declared = DeclaredIn;
            REFs = new List <REF<T>> ();
            Object = ObjectIn;
            Type.IDs.Add (this);

            }

        public override string ToString() {
            return Label;
            }
        }

    public class REF<T> where T : class {
        public Position        Position;
        public ID <T>             ID;
        public T           Object;

        public T Definition {
            get { return ID == null ? (T) null : ID.Object; }
            }

        public REF() {
            }
        public REF(Position PositionIn, ID <T> IDIn, T ObjectIn) {
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

    public class TOKEN <T> : REF <T>  where T : class  {

        public TOKEN(Position PositionIn, ID <T> IDIn, T ObjectIn) {
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
