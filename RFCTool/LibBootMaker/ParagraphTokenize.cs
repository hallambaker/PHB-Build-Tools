using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.MarkLib {
    public enum TagType {
        Empty,
        Implicit,
        Annotate,
        Meta
        }

    public class Parse {

        public static string[] catalog = {
            Element_a.Register ()
            };
        }

    public abstract class Element {
        public abstract TagType TagType { get; }
        public abstract string Name { get; }

        public string Class;
        public string ID;

        public static string Register(Element Element) {
            return Element.Name;
            }

        public string FirstMatch(string Tag, List<TagValue> Attributes) {
            return null;
            }
        public string TagMatch(string Tag, List<TagValue> Attributes) {
            return null;
            }
        }

    public class Element_Close : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        public Element Open;
        }

    public class Element_a : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        string href;

        Element_a() {
            }

        public Element_a(List<TagValue> Attributes) {
            href = FirstMatch("href", Attributes);
            }
        
        public static string Register() {
            var Object = new Element_a();
            return Register(Object);
            }
        
        }

    public class Element_img : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        string src;
        }

    public class Element_include : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        string file;
        }

    public class Element_bibliography : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        string file;
        }

    public class Element_bibsource : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        string file;
        }

    public class Element_ietf : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        string href;
        }


    public class Element_Layout : Element {
        public override TagType TagType { get { return TagType.Empty; } }
        public override string Name { get { return ""; } }
        bool master;
        bool navigator;
        bool toc;
        bool tof;
        bool tod;
        bool tor;
        }

    public class Element_row : Element {
        public override TagType TagType { get { return TagType.Implicit; } }
        public override string Name { get { return ""; } }
        }

    public class Element_col : Element {
        public override TagType TagType { get { return TagType.Implicit; } }
        public override string Name { get { return ""; } }
        int count;
        }

    // bold
    public class Element_b : Element {
        public override TagType TagType { get { return TagType.Annotate; } }
        public override string Name { get { return ""; } }
        }

    // italics
    public class Element_i : Element {
        public override TagType TagType { get { return TagType.Annotate; } }
        public override string Name { get { return ""; } }
        }

    // underline
    public class Element_u : Element {
        public override TagType TagType { get { return TagType.Annotate; } }
        public override string Name { get { return ""; } }
        }

    // strikethrough
    public class Element_s : Element {
        public override TagType TagType { get { return TagType.Annotate; } }
        public override string Name { get { return ""; } }
        }

    // code 
    public class Element_x : Element {
        public override TagType TagType { get { return TagType.Annotate; } }
        public override string Name { get { return ""; } }
        }

    // defined term
    public class Element_d : Element {
        public override TagType TagType { get { return TagType.Annotate; } }
        public override string Name { get { return ""; } }
        }

    // Block classes

    public class Element_title : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_titleshort : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_keyword : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_version : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_priority : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_author : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_authorfirstname : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class Element_authorlastname : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }


    public class Element_authorinitials : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }


    public class Element_authororganization : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }

    public class authoremail : Element {
        public override TagType TagType { get { return TagType.Meta; } }
        public override string Name { get { return ""; } }
        }



    public class ParagraphTokenize {
        }
    }
