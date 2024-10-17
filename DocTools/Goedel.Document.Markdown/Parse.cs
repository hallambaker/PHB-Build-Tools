using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.FSR;


    //State DocStart Ignore Empty			// Starting State for Document		
    //    On "\n" GoTo Start
    //    Any GoTo GetTitle
    //State GetTitle AddChar Title


    //State GetTitle AddCharWS Title
    //    On "\r" GoTo Start

namespace Goedel.Document.Markdown {

    public partial class MarkdownDocument {

        public void Parse(LexReader Reader) => Parse(Reader, Paragraphs);



        public void Parse(LexReader Reader, List<Paragraph> Paragraphs) {
            bool First = true;
            MarkDownLex Lexer = new(Reader);

            while (!Reader.EOF) {
                MarkDownLex.Token Token = Lexer.GetToken();
                if (Token == MarkDownLex.Token.Block) {
                    //Console.WriteLine("Got Block {0} {1} {2}",
                    //        Lexer.BlockType, Lexer.Level, Lexer.Text);

                    var Paragraph = new Paragraph(Lexer);
                    Paragraphs.Add(Paragraph);

                    if (First) {
                        First = false;
                        Title = Paragraph.Text;
                        ShortTitle = Title;
                        }
                    }
                else {
                    //Console.WriteLine("oops");
                    }
                }
            }




        }
    }
