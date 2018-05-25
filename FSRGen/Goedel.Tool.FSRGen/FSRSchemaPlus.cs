using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace Goedel.Tool.FSRGen {


    public partial class FSRSchema {
        public bool Completed = false;

        public virtual void Complete() {
            if (Completed) {
                return;
                }

            Completed = true;

            foreach (_Choice Choice in Top) {
                Choice.Parent = this;
                Choice.Complete ();
                }

            }
        }    
    
    
    public abstract partial class _Choice {
        public bool Completed = false;
        public FSRSchema Parent = null;

        public virtual void Complete () {
            if (Completed) {
                return;
                }

            Completed = true;
            }
        }

    public partial class Token {
        public string           Tag;
        public int              Index;

        public static void Add (List<Token> List, string Tag) {
            var Token = new Token {
                Tag = Tag,
                Index = List.Count
                };
            List.Add (Token);
            }
        }

    public partial class Action {
        public string           Tag;
        public int              Index;

        public static void Add (List<Action> List, string Tag) {
            var Action = new Action {
                Tag = Tag,
                Index = List.Count
                };
            List.Add (Action);
            }
        }

    public partial class State {
        public int Index;
        }

    public partial class FSR : _Choice {

        public int MaxChar = 128;

        public int[,] TransitionTable;
        public int[,] CompressedTable;
        public int[] MappingTable;
        public int MaxMap = 0;

        public int[] ActionTable;
        public int[] TokenTable;
        public List<State> States = new List<State>();
        public List<Token> Tokens = new List<Token>();
        public List<Action> Actions = new List<Action>();

        public string StateType = "char";

        private void SetState(State State, char c, _Choice Action) {
            int Index = (int) c;

            MaxChar = MaxChar > c ? MaxChar : c;

            if (Action._Tag() == FSRSchemaType.GoTo) {
                var GoTo = (GoTo)Action;
                var Next = (State)GoTo.Next.Definition;
                //Console.WriteLine ("Fill {0}, {1}", Index, Next.Index);

                if (TransitionTable[State.Index, Index] < -1) {
                    TransitionTable[State.Index, Index] = Next.Index;
                    }
                }
            else {
                //Console.WriteLine("Do something");
                TransitionTable[State.Index, Index] = -1;
                }
            }

        private void CopyColumns (int Index) {
            foreach (var State in States) {
                CompressedTable[State.Index, MaxMap] = TransitionTable[State.Index, Index];
                }
            MappingTable [Index] = MaxMap ++;
            }

        private bool MatchColumns (int Index1, int Index2) {
            foreach (var State in States) {
                if (CompressedTable[State.Index, Index1] != TransitionTable[State.Index, Index2]) {

                    //Console.WriteLine ("Check {0}={1}", CompressedTable[State.Index, Index1],
                    //        TransitionTable[State.Index, Index2]);

                    return false;
                    }
                }

            return true;
            }

        private void CompressTable() {
            CopyColumns (0);
            for (int i = 1; i < MaxChar; i++) {
                bool Match = false;
                for (int j = 0; (j < MaxMap) & ! Match; j++) {
                    if (MatchColumns (j, i)) {
                        MappingTable [i] = j;
                        Match = true;
                        }
                    }
                if (!Match) {
                    CopyColumns (i);
                    }
                }
            }


        public override void Complete() {
            if (Completed) {
                return;
                }

            Completed = true;
            foreach (var Choice in Entries) {
                Choice.Complete();

                if (Choice._Tag() == FSRSchemaType.State) {
                    State State = (State)Choice;
                    State.Index = States.Count;
                    States.Add(State);
                    }
                }

            foreach (var ID  in this.Parent.TYPE__tToken.IDs) {
                Token.Add (Tokens, ID.Label);
                }

            foreach (var ID  in this.Parent.TYPE__tAction.IDs) {
                Action.Add (Actions, ID.Label);
                }

            

            TransitionTable = new int[States.Count, MaxChar];
            CompressedTable = new int[States.Count, MaxChar];
            MappingTable = new int[MaxChar];


            ActionTable = new int[States.Count];
            TokenTable = new int[States.Count];


            for (int i = 0; i < States.Count; i++) {
                ActionTable [i] = -1;
                TokenTable [i] = -1;
                for (int j = 0; j < MaxChar; j++) {
                    TransitionTable [i,j] = -2;
                    }
                }

            // scan the state list again fill in the transitions
            foreach (var State in States) {
                //Console.WriteLine("State  {0}  Action {1}  Token {2}",
                //        State.Id.Label, State.Action.Label, State.Token.Label);

                foreach (var Entry in State.Entries) {
                    if (Entry.Is._Tag() == FSRSchemaType.On) {
                        var On = (On) Entry.Is;         // is a character in a set
                        foreach (char c in On.Match) {
                            SetState (State, c, Entry.Action);
                            }
                        }
                    else if (Entry.Is._Tag() == FSRSchemaType.Any) {
                        //var Any = (Any) Entry.Is;        // is any character
                        for (int i = 0; i < MaxChar; i++) {
                            SetState (State, (char) i, Entry.Action);
                            }
                        }
                    else if (Entry.Is._Tag() == FSRSchemaType._Label) {
                        var Label = (_Label) Entry.Is;  // is a reference to a charset
                        var Charset = (Charset) Label.Label.Definition;


                        if (Charset.First.Length < 0 | Charset.Last.Length <0) {
                            throw new Exception ("Null charset specified");
                            }

                        if (Charset.First[0] > Charset.Last[0]) {
                            throw new Exception ("Bad charset");
                            }

                        for (char c = Charset.First[0]; c <= Charset.Last[0]; c++) {
                            SetState (State, c, Entry.Action);
                            }
                        }
                    }
                }

            CompressTable();

            }

        }
    }
