using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;
using Goedel.FSR;


namespace Goedel.Document.Markdown {
    public static class SegmentToHTML {

        public static string ToHTML (this Block Block) {
            var Builder = new StringBuilder();

            foreach (var Segment in Block.Segments) {
                switch (Segment) {
                    case TextSegmentText Text: {
                        Builder.Append(Text.Text);
                        break;
                        }
                    case TextSegmentOpen Text: {
                        Builder.Append("<");
                        AddAttributes(Builder, Text.Attributes);
                        Builder.Append(">");
                        break;
                        }
                    case TextSegmentClose Text: {
                        Builder.Append("</");
                        Builder.Append(Text.Open.Tag);
                        Builder.Append(">");
                        break;
                        }
                    case TextSegmentEmpty Text: {
                        Builder.Append("<");
                        AddAttributes(Builder, Text.Attributes);
                        Builder.Append("/>");
                        break;
                        }
                    }
                }


            return Builder.ToString();
            }

        static void AddAttributes (StringBuilder Builder, List<TagValue> Attributes) {
            if (Attributes == null) {
                return;
                }

            bool first = true;
            foreach (var Attribute in Attributes) {
                if (!first) {
                    Builder.Append(" ");
                    }
                else {
                    first = false;
                    }
                Builder.Append(Attribute.Tag);
                if (Attribute.Value != null && Attribute?.Value != "") {
                    Builder.Append("=\"");
                    Builder.Append(Attribute.Value);
                    Builder.Append("\"");
                    }
                }
            }

        }
    }
