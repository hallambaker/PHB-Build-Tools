using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.MarkLib {
    
	// The following markup is accepted :
	//
	// P			Text
	// H(n)			=Text  ==Text ... ======Text
	// OL Item      #Text  ##Text ... ######Text
	// UL Item		*Text  **Text ... ******Text
	// DT Item      :Text
	// DD Item      ::Text


    // Not Yet Implemented:

    // Title/H1		Text\n====
	// H2			Text\n----

	// Verbatim     [{  \n}]
	// Comment		//

    // This part to go to the generator
    public partial class MarkDownLex {
        Reader Reader;
        State S;

        public bool EOF {
            get { return Reader.EOF; }
            }


        public MarkDownLex (Reader Reader) {
            this.Reader = Reader;
            Init();
            Reset();
            }

        public Token GetToken () {
            return GetToken((State)0);
            }

        public Token GetToken (State StartState) {
            Reset();
            S =  StartState;
            Token Token = MarkDownLex.Token.INVALID;

            bool Going = Reader.Get();
            while (Going) {

                //Console.Write(Reader.LastChar);

                int c = Reader.LastInt;
                int ct = ((c >= 0) & (c < Character_Mapping.Length)) ?
                    Character_Mapping[c] : 0;

                //Console.WriteLine("  {0} {1} {2}", S, c, ct);

                S = (State)Compressed_Transitions[(int)S, ct];

                if (S >= 0) {
                    Action Action = Actions[(int)S];
                    Action(Reader.LastInt);
                    Token = Tokens[(int)S];
                    Going = Reader.Get();
                    }
                else {
                    Going = false;
                    Reader.UnGet();
                    }
                }

            //Console.WriteLine();
            //Console.Write("State {0} ", S);
            return Token;
            }

        }
   


	public partial class MarkDownLex {
        public int WhiteSpace = 0;

        public BlockType BlockType;
        public int Level = 0;
        public string Text;
        public string XText;

        public virtual void Reset () {
            WhiteSpace = 0;
            Level = 0;
            BlockType = BlockType.Paragraph;
            Text = ""; XText = "";
			}

        public virtual void Ignore (int c) {

			}

        public virtual void Reset (int c) {
            Reset();
			}

        public virtual void CountWhite (int c) {
			}

        public virtual void AddText (int c) {
            Text = Text + XText + (char) c;
            XText = "";
			}


        public virtual void AddSpace(int c) {
            AddText(' ');
            }


        public virtual void StartPre(int c) {
            BlockType = BlockType.Preformatted;
            Text = "";
            XText = "";
            }

        public virtual void AddPre(int c) {
            Text = Text + XText + (char)c;
            XText = "";
            }

        public virtual void PreClose(int c) {
            XText = XText + (char)c;
            }


        public virtual void AddHeading (int c) {
            BlockType = BlockType.Heading;
            Level++;
			}

        public virtual void WasHeading1 (int c) {
            BlockType = BlockType.Heading;
            XText = XText + (char) c;
            Level = 0;
			}

        public virtual void WasHeading2 (int c) {
            BlockType = BlockType.Heading;
            XText = XText + (char) c;
            Level = 1;
			}

        public virtual void DefinedTerm (int c) {
            BlockType = BlockType.DefinedTerm;
            Level++;
			}

        public virtual void DefinedData (int c) {
            BlockType = BlockType.DefinedData;
            Level++;
			}

        public virtual void AddNumbered (int c) {
            BlockType = BlockType.Numbered;
            Level++;
			}

        public virtual void AddBullet (int c) {
            BlockType = BlockType.Bullet;
            Level++;
			}

		}
    }
