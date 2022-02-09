using Goedel.Registry;
using Goedel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.Tool.Exceptional {


    public interface IEvent {

        string LogLevel { get; }
        string Name { get; }
        string Pattern { get; }

        int EventId { get; }
        List<TypedParameter> TypedParameters { get; }



        public bool IsEmpty => TypedParameters == null ||
            TypedParameters?.Count == 0;

        }



    public partial class Trace : IEvent {
        public string LogLevel => "Trace";

        public string Name => Id.Label;

        public string Pattern => Text;

        public int EventId => Code;

        public List<TypedParameter> TypedParameters => Parameters;
        }

    public partial class Debug : IEvent {
        public string LogLevel => "Debug";

        public string Name => Id.Label;

        public string Pattern => Text;

        public int EventId => Code;

        public List<TypedParameter> TypedParameters => Parameters;
        }

    public partial class Information : IEvent {
        public string LogLevel => "Information";

        public string Name => Id.Label;

        public string Pattern => Text;

        public int EventId => Code;

        public List<TypedParameter> TypedParameters => Parameters;
        }

    public partial class Warning : IEvent {
        public string LogLevel => "Warning";

        public string Name => Id.Label;

        public string Pattern => Text;

        public int EventId => Code;

        public List<TypedParameter> TypedParameters => Parameters;
        }

    public partial class Error : IEvent {
        public string LogLevel => "Error";

        public string Name => Id.Label;

        public string Pattern => Text;

        public int EventId => Code;

        public List<TypedParameter> TypedParameters => Parameters;
        }

    public partial class Critical : IEvent {
        public string LogLevel => "Critical";

        public string Name => Id.Label;

        public string Pattern => Text;

        public int EventId => Code;

        public List<TypedParameter> TypedParameters => Parameters;
        }




    public partial class Exception {
        public List<Object> Objects = new();
        public List<Console> Consoles = new();


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
