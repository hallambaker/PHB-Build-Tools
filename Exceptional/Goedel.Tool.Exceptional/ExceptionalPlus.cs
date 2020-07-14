using Goedel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.Tool.Exceptional {


 
    public partial class Exception {
        public List<Object> Objects = new List<Object>();
        public List<Console> Consoles = new List<Console>();


        public string Console = "An error occurred";
        public string BaseClass;
        public bool Base;


        /// <summary>
        /// The initialization method. This is automatically called once after
        /// the whole system has been created.
        /// </summary>
        /// <param name="Parent"></param>
        public override void Init (_Choice Parent) {
            if (Parent as Exception != null) {
                BaseClass = (Parent as Exception).Id.ToString();
                Base = false;
                }
            else {
                //BaseClass = "global::System.Exception";
                BaseClass = "global::Goedel.Utilities.GoedelException";
                Base = true;
                }
            
            foreach (var Option in Options) {
                if (Option is Console console) {
                    Console = console.Message;
                    Consoles.Add(console);
                    }


                if (Option as Object != null) {

                    var Object = (Option as Object);

                    Objects.Add(Object);
                    }
                }

            }

        }
    }
