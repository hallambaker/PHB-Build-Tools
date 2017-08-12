using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;
using Goedel.IO;
using Goedel.Document.RFC;
using Goedel.Document.Markdown;

namespace Goedel.Tool.RFCTool {
    public partial class Html2RFCOut  {

        public void WriteBlock (P Block, string Tag) {
            Start(Tag, true, false, "id", Block.GeneratedID);
            if (Block.Segments != null) {
                foreach (var Segment in Block.Segments) {
                    switch (Segment) {
                        case TextSegmentText Text: {
                            Output.Write(Text.Text);
                            break;
                            }
                        case TextSegmentOpen Text: {
                            Write(Text);
                            break;
                            }
                        case TextSegmentClose Text: {
                            Write(Text);
                            break;
                            }
                        case TextSegmentEmpty Text: {
                            WriteElement(Text.Tag);
                            break;
                            }
                        }
                    }
                }

            WriteElement("a", false, false, Pilcrow, "class", "pilcrow", "href", "#" + Block.GeneratedID);
            End(false,true);
            }


        public void Write (TextSegmentOpen Text) {


            switch (Text.Tag) {

                // Basic formatting
                case "em":
                case "i": {
                    Start("i", false, false);
                    break;
                    }
                case "strong":
                case "b": {
                    Start("b", false, false);
                    break;
                    }
                case "tt": {
                    Start("code", false, false);
                    break;
                    }
                case "sub": {
                    Start("sub", false, false);
                    break;
                    }
                case "sup": {
                    Start("sup", false, false);
                    break;
                    }

                //links
                case "eref":
                case "a": {
                    var Value = Text.Attributes?[0].Value;
                    Start("a", false, false, "class", "eref", "href", Value);
                    if (Text.IsEmpty) {
                        Output.Write(Text.Text ?? Value);
                        }
                    break;
                    }
                case "info":
                case "norm":
                case "xref": {
                    var Value = Text.Attributes?[0].Value;
                    Start("a", false, false, "class", "xref", "href", "#" + Value);
                    if (Text.IsEmpty) {
                        Output.Write(Text.Text ?? Value);
                        }
                    break;
                    }

                // Comment style
                case "cref": {
                    Start("span", false, false, "class", "cref");
                    break;
                    }
                case "bcp14": {
                    Start("span", false, false, "class", "bcp14");
                    break;
                    }

                }
            }

        void WriteEmpty (TextSegmentOpen Text) {
            }

        public void Write (TextSegmentClose Text) {
            var Tag = Text.Open.Tag;
            switch (Tag) {
                case "em":
                case "i":
                case "strong":
                case "b": 
                case "tt":
                case "sub":
                case "sup":
                case "eref":
                case "a":
                case "info":
                case "norm":
                case "xref":
                case "cref":
                case "bcp14": {
                    End(false, false);
                    break;
                    }

                }

            }
        }
    }
