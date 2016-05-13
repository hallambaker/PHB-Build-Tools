using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Goedel.Trojan {

    public abstract class Window {
        public Model Model;
        public Binding Binding;


        public abstract string Title {
            get; set;
            }

        public virtual Menu Menu { get { return null; } } // Menu description

        public virtual void Initialize(Model Model, Binding Binding) {
            this.Model = Model;
            this.Binding = Binding;
            Binding.Initialize(this);
            }


        // Probably want to kill this
        public virtual void Populate() {
            }


        public void Add(ObjectHandle ObjectHandle) {
            }
        }

    public class ObjectHandle {

        }
    }
