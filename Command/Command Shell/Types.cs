using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandShell {

    public partial class ExistingFile : _ExistingFile{
        public string Extension = "";

        public override void Default(string TextIn) {
            Extension = TextIn;
            }
        }


    public partial class NewFile : _NewFile{
        public string Extension = "";

        public override void Default(string TextIn) {
            Extension = TextIn;
            }
        }

    public partial class  Flag : _Flag {
            public bool         IsSet;

            public override void  Register(string Tag, Registry Registry, int Index) {
                Registry.Register (Tag, Index);
                Registry.Register ("no" + Tag, Index);
                }

            public override int Tag(string Tag) {
                if ((Tag.Length > 2) && Tag[0] == 'n' && Tag[1] == 'o') {
                    IsSet = false;
                    }
                else {
                    IsSet = true;
                    }

                return 0; // number of required parameters is 0
                }

            public override void Parameter(string Text) {
                //Text = (Text == null) ? "true" : Text;
                switch (Text.ToLower()) {
                    case "true":
                    case "1":
                        IsSet = true;
                        break;
                    case "false":
                    case "0":
                        IsSet = false;
                        break;
                    default :
                        throw new Exception ("Flag value not recognized" + Text);
                    }
                }
            public override string ToString() {
                return IsSet ? "true" : "false";
                }

		    public override string Usage (string Tag, string Value, char Usage) {
			    return Usage + "[no]" + Tag;
			    }
        } 


    }


