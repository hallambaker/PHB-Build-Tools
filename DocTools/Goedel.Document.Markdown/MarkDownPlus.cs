using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Goedel.FSR;

namespace Goedel.Document.Markdown {
    
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

 
	public partial class MarkDownLex {
        public int WhiteSpace = 0;

        public BlockType BlockType;
        public int Level = 0;
        public string Text;
        public string XText;

        public override void Reset () {
            WhiteSpace = 0;
            Level = 0;
            BlockType = BlockType.Paragraph;
            Text = ""; XText = "";
			}

        public virtual void Ignore (int c) {

			}

        public virtual void Reset(int c) => Reset();

        public virtual void CountWhite (int c) {
			}

        public virtual void AddText (int c) {
            Text = Text + XText + (char) c;
            XText = "";
			}


        public virtual void AddSpace(int c) => AddText(' ');


        public virtual void StartPre(int c) {
            BlockType = BlockType.Preformatted;
            Text = "";
            XText = "";
            }

        public virtual void AddPre(int c) {
            Text = Text + XText + (char)c;
            XText = "";
            }

        public virtual void PreClose(int c) => XText += (char)c;


        public virtual void AddHeading (int c) {
            BlockType = BlockType.Heading;
            Level++;
			}

        public virtual void WasHeading1 (int c) {
            BlockType = BlockType.Heading;
            XText += (char) c;
            Level = 0;
			}

        public virtual void WasHeading2 (int c) {
            BlockType = BlockType.Heading;
            XText += (char) c;
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
