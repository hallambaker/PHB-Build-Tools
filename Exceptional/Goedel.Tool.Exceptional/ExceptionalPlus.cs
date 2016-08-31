using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.Tool.Exceptional {
    public partial class Exception {
        public List<Object> Objects = new List<Object>();

        public string Console = "An error occurred";

        public void Complete () {
            

            foreach (var Option in Options) {
                if (Option as Console != null) {

                    Console = (Option as Console).Message;
                    }


                if (Option as Object != null) {

                    var Object = (Option as Object);

                    Objects.Add(Object);
                    }
                }

            }

        }
    }
