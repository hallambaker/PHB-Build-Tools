using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Document.Markdown;

namespace Goedel.Document.RFC {

    public struct StringSet {
        public string First;
        public string Second;
        public string Third;
        public string Fourth;
        public StringSet(string First, string Second = null, string Third = null, string Fourth = null) {
            this.First = First;
            this.Second = Second;
            this.Third = Third;
            this.Fourth = Fourth;
            }
        }

    /// <summary>
    /// Generate boilerplate and ipr statements. This should eventually be
    /// replaced by a configuration file that can be read during processing.
    /// </summary>
    public static class RFCEditorBoilerplate {

        public static readonly string StatusScriptURL =
            "https://www.rfc-editor.org/js/metadata.min.js";

        public static readonly string DefaultWorkGroup =
            "Network Working Group";
        public static readonly string RFCLocationURL =
            "https://www.rfc-editor.org/rfc/";

        public static readonly string IfApproved =
            "(if approved)";


        public static readonly Dictionary<string, StringSet> SeriesTexts =
            new() {
            { "rfc",  new StringSet("RFC") },
            { "draft",  new StringSet("Internet-Draft") },
            };



        public static readonly Dictionary<string, StringSet> StreamTexts =
            new() {
            { "ietf", new StringSet("Internet Engineering Task Force",
                    "This document is a product of the Internet Engineering Task Force (IETF).",
                    "It represents the consensus of the IETF community. " +
                        "It has received public review and has been approved for "+
                        "publication by the Internet Engineering Steering Group (IESG).",
                    "It has been approved for publication by the Internet Engineering Steering Group (IESG).") },
            { "iab", new StringSet("Internet Architecture Board",
                    "This document is a product of the Internet Architecture " +
                        "Board (IAB), and represents information that the IAB has deemed " +
                        "valuable to provide for permanent record.",
                    "It represents the consensus of the Internet Architecture Board (IAB).")},
            { "irtf", new StringSet("Internet Research Task Force",
                    "This document is a product of the Internet Research Task Force (IRTF). "+
                        "The IRTF publishes the results of Internet-related research and "+
                        "development activities.  These results might not be suitable for deployment.",
                    "This RFC represents the consensus of the {0} Research Group of the "+
                        "Internet Research Task Force (IRTF).",
                    "This RFC represents the individual opinion(s) of one or more members of the {0} "+
                        "Research Group of the Internet Research Task Force (IRTF)")},
            { "independent", new StringSet("Independent Submission",
                    "This is a contribution to the RFC Series, independently of any other RFC stream. "+
                        "The RFC Editor has chosen to publish this document at its discretion "+
                        "and makes no statement about its value for implementation or deployment.")}
            };

        const string Prefix1 = "This document is not an Internet Standards Track specification;";

        public static readonly Dictionary<string, StringSet> StatusTexts =
            new() {
            {"std", new StringSet("Standards Track", "This is an Internet Standards Track document.") },
            {"bcp", new StringSet("Best Current Practices", "This memo documents an Internet Best Current Practice.") },
            {"info", new StringSet("Informational",
                Prefix1 + "it is published for informational purposes.") },
            {"exp", new StringSet("Experimental",
                Prefix1 + "it is published for informational purposes.",
                "This document defines an Experimental Protocol for the Internet community.") },
            {"historic", new StringSet("Historic",
                Prefix1 + "it is published for the historical record.",
                "This document defines a Historic Document for the Internet community.") }
            };

        public const string StatusNonIETF = "Documents approved for publication by the {0} are not " +
            "a candidate for any level of Internet Standard; see Section 2 of [!RFC 7841].";
        public const string StatusIETFStdBcp = "Further information on (BCPs or Internet Standards) is available in Section 2 of RFC 7841.";

        public const string StatusIAB = "The IAB members at the time this memo was approved were (in alphabetical order):";

        public const string Paragraph3 = "Information about the current status of this "+
            "document, any errata, and how to provide feedback on it may be obtained at "+
            "<a=\"https://www.rfc-editor.org/{0}/rfc{1}\">https://www.rfc-editor.org/{0}/rfc{1}</a>.";




        public static readonly Dictionary<string, List<string>> IPR =
            new() {
                    { "trust200902", new List<string>() { TLP6b_1 }  },
                    { "noModificationTrust200902", new List<string>() { } },
                    { "noDerivativesTrust200902", new List<string>() { } },
                    { "pre5378Trust200902", new List<string>() { } },
            };

        const string TLP6a = @"This Internet-Draft is submitted in full conformance with the provisions of BCP 78 and BCP 79.";
        const string TLP6b_1 = @"Copyright (c) IETF Trust and the persons identified as the document authors. All rights reserved.";
        const string TLP6bi_2 = @"This document is subject to BCP 78 and the IETF Trust’s Legal Provisions Relating to IETF Documents (http://trustee.ietf.org/license-info) in effect on the date of publication of this document. Please review these documents carefully, as they describe your rights and restrictions with respect to this document. Code Components extracted from this document must include Simplified BSD License text as described in Section 4.e of the Trust Legal Provisions and are provided without warranty as described in the Simplified BSD License.";
        const string TLP6bii_2 = @"This document is subject to BCP 78 and the IETF Trust’s Legal Provisions Relating to IETF Documents (http://trustee.ietf.org/license-info) in effect on the date of publication of this document. Please review these documents carefully, as they describe your rights and restrictions with respect to this document.";
        const string TLP6ci = @"This document may not be modified, and derivative works of it may not be created, except to format it for publication as an RFC or to translate it into languages other than English.";
        const string TLP6cii = @"This document may not be modified, and derivative works of it may not be created, and it may not be published except as an Internet-Draft.";
        const string TLP6ciii = @"This document may contain material from IETF Documents or IETF Contributions published or made publicly available before November 10, 2008. The person(s) controlling the copyright in some of this material may not have granted the IETF Trust the right to allow modifications of such material outside the IETF Standards Process. Without obtaining an adequate license from the person(s) controlling the copyright in such materials, this document may not be modified outside the IETF Standards Process, and derivative works of it may not be created outside the IETF Standards Process, except to format it for publication as an RFC or to translate it into languages other than English.";
        public const string CopyrightURL = "https://trustee.ietf.org/license-info/IETF-TLP-5.htm";


        const string DraftStatus2 = "Internet-Drafts are working documents of the Internet Engineering Task " +
            "Force (IETF).  Note that other groups may also distribute working documents as Internet-Drafts. " +
            "The list of current Internet-Drafts is at <a=\"http://datatracker.ietf.org/drafts/current/\">" +
            "http://datatracker.ietf.org/drafts/current/</a>.";

        const string DraftStatus3 = "Internet-Drafts are draft documents valid for a maximum of six months and"+
            " may be updated, replaced, or obsoleted by other documents at any time.It is inappropriate "+
            "to use Internet-Drafts as reference material or to cite them other than as \"work in progress.\"";

        const string DraftStatus4 = "This Internet-Draft will expire on {0}";

        public static void Set (BlockDocument document) {
            bool haveCopyright = false;
            bool havestatus = false;

            RFCEditorBoilerplate.StreamTexts.TryGetValue(document.Stream.ToLower(), out document.StreamTexts);
            RFCEditorBoilerplate.StatusTexts.TryGetValue(document.Status.ToLower(), out document.StatusTexts);
            RFCEditorBoilerplate.SeriesTexts.TryGetValue(document.Series.ToLower(), out document.SeriesTexts);

            document.SeriesText = document.SeriesTexts.First;
            document.StatusText = document.StatusTexts.First;
            document.StreamText = document.StreamTexts.First;


            foreach (var section in document.Boilerplate) {
                switch (section.Heading) {
                    case "Status of This Memo": {
                        havestatus = true;
                        break;
                        }
                    case "Copyright Notice": {
                        haveCopyright = true;
                        break;
                        }
                    }
                }


            if (!havestatus) {
                Section StatusSection = new("Status of This Memo", "n-status-of-this-memo") {
                    Automatic = true
                    };
                AddParagraphs(StatusSection, StatusOfThisDocument(document));
                document.Boilerplate.Add(StatusSection);
                }


            if (!haveCopyright) {
                var CopyrightSection = new Section("Copyright Notice", "n-copyright-notice") {
                    Automatic = true
                    };
                AddParagraphs(CopyrightSection, Copyright(document));
                document.Boilerplate.Add(CopyrightSection);
                }




            if (document.Also != null) {
                var Block = new P();
                var Lexer = new MarkNewParagraph();
                Lexer.Push("This document is also available online at <a=\"");
                Lexer.Push(document.Also);
                Lexer.PushEnd("\"/>.");
                Block.Segments = Lexer.Segments;

                document.Abstract.Add(Block);
                }

            }


        

        static void AddParagraphs (Section section, List<string> texts) {


            foreach (var Text in texts) {
                var Block = new P();
                var Lexer = new MarkNewParagraph();
                Lexer.PushEnd(Text);
                Block.Segments = Lexer.Segments;

                section.TextBlocks.Add(Block);
                }

            }

        public static List<string> StatusOfThisDocument(BlockDocument document) {
            var Result = new List<string>();


            if (document.IsDraft) {
                Result.Add(TLP6a);
                Result.Add(DraftStatus2);
                Result.Add(DraftStatus3);
                Result.Add(String.Format(DraftStatus4, document.Expires));
                }
            else {
                Result.Add(document.StatusTexts.Second);

                var Paragraph2 = new StringBuilder();
                Paragraph2.Append(document.StreamTexts.Second);
                Paragraph2.Append(" ");
                if (document.IsConsensus) {
                    Paragraph2.Append(String.Format(document.StreamTexts.Third, document.WorkgroupText));
                    }
                else {
                    Paragraph2.Append(String.Format(document.StreamTexts.Fourth, document.WorkgroupText));
                    }


                Paragraph2.Append(String.Format(StatusIETFStdBcp));

                Result.Add(Paragraph2.ToString());

                Result.Add(String.Format(Paragraph3,"info", document.Number));

                }

            return Result;
            }


        public static List<string> Copyright (BlockDocument document) {

            if (document.Ipr == null) {
                return new List<string>();
                }

            var Result = new List <string> () { TLP6b_1 };

            if (document.Stream == "ietf") {
                Result.Add(TLP6bi_2);
                }
            else {
                Result.Add(TLP6bii_2);
                }

            switch (document?.Ipr?.Trim()) {
                case "trust200902": {
                    break;
                    }
                case "noModificationTrust200902": {
                    Result.Add(TLP6ci);
                    break;
                    }
                case "noDerivativesTrust200902": {
                    Result.Add(TLP6cii);
                    break;
                    }
                case "pre5378Trust200902": {
                    Result.Add(TLP6ciii);
                    break;
                    }
                default: throw new IPRInvalid(document.Ipr);
                }

            return Result;
            }





        }
    }



