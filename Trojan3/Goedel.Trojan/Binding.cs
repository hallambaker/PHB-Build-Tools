using System;
using System.Collections.Generic;
using Goedel.Trojan;

namespace Goedel.Trojan {

    /// <summary>
    /// The class that carries all the binding specific code
    /// such as how to make and unmake windows, widgets, etc.
    /// </summary>
    public abstract class Binding {

        public abstract void Initialize(Window Window);

        public abstract void Run();

        public abstract void Quit();

        public abstract void About(About About);

        public abstract void Wizard(Wizard Wizard);

        public abstract bool Dialog(Object Object);


        public bool Dispatch(Object Object) {
            if (Object as Wizard != null) {
                Wizard(Object as Wizard);
                return true;
                }
            return Dialog(Object);

            }


        public abstract ObjectHandle GetHandle(Object Object);



        }

    public abstract class BindingData {
        public abstract void Refresh();

        }

    /// <summary>
    /// Every field in the model must map to an object in the binding that
    /// exposes the IFieldBinding interface.
    /// </summary>
    public interface IFieldBinding {

        /// <summary>
        /// The object field that this object maps to.
        /// </summary>
        ObjectField ObjectField { get; }




        /// <summary>
        /// Optional text explaining why the field is considered invalid.
        /// </summary>
        string  ReasonInvalid { set; }

        /// <summary>
        /// If set true, the field is an output only field.
        /// </summary>
        bool ReadOnly { set; }

        /// <summary>
        /// Optional text giving the user additional advice.
        /// </summary>
        string Tip { set; }

        /// <summary>
        /// Copy from Widget to field Test for test purposes.
        /// </summary>
        void Test();

        /// <summary>
        /// Copy from field Test to field Value
        /// </summary>
        void Apply();

        /// <summary>
        /// Fill widget with value from field Value.
        /// </summary>
        void Fill();

        }


    }
