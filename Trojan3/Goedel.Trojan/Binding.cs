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


        public abstract ObjectHandle GetHandle(Object Object);

        }

    public abstract class BindingData {
        public abstract void Refresh();

        }

    public interface IFieldBinding {

        string ReasonInvalid { get; set; }

        }


    }
