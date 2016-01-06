using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Goedel.Registry;

namespace Goedel.Trojan {

    public abstract partial class _Choice {
        public _Choice Parent;
        public Dialog Dialog = null;
        public virtual string XID { get { return "<Invalid>"; } }
        public virtual string DID { get { return "<Invalid>"; } }

        public virtual void Normalize(_Choice Parent) {
            XNormalize(Parent);
            }

        public virtual void XNormalize(_Choice Parent) {
            this.Parent = Parent;
            if (Parent!= null && Parent.Dialog != null) {
                Parent.Dialog.Add(this);
                }
            }

        }

    public partial class GUI {


        public override void Normalize(_Choice Parent) {
            XNormalize(Parent);
            foreach (var Entry in Entries) {
                Entry.Normalize(this);
                }
            }
        }

    public partial class Window {
        public override string XID { get { return "Window_" + Id; } }
        public override string DID { get { return "Data_" + Id; } }
        public GUI GUI;

        public override void Normalize(_Choice Parent) {
            XNormalize(Parent);
            GUI = Parent as GUI;

            foreach (var Entry in Entries) {
                Entry.Normalize(this);
                }
            }
        }

    public partial class Wizard {
        public override string XID { get { return "Wizard_" + Id; } }
        public override string DID { get { return "Data_" + Id; } }

        public GUI GUI;
        public List<Dialog> Dialogs = new List<Dialog>();

        public override void Normalize(_Choice Parent) {
            XNormalize(Parent);
            GUI = Parent as GUI;

            foreach (var Entry in Entries) {
                Entry.Normalize(this);
                }
            }
        }

    public partial class Task {
        public override string XID { get { return "Task_" + Target; } }
        }

    public partial class Dialog {
        public override string XID { get { return "Dialog_" + Id; } }
        public override string DID { get { return "Data_" + Id; } }

        public GUI GUI;
        public Wizard Wizard;

        public List<Action> Actions = new List<Action>();
        public List<Task> Tasks = new List<Task>();
        public List<Next> Nexts = new List<Next>();
        public List<Input> Inputs = new List<Input>();
        public List<Output> Outputs = new List<Output>();

        public void Add(_Choice Entry) {
            Entry.Dialog = this;
            if (Entry.GetType() == typeof(Action)) {
                Actions.Add(Entry as Action);
                }
            if (Entry.GetType() == typeof(Task)) {
                Tasks.Add(Entry as Task);
                }
            if (Entry.GetType() == typeof(Next)) {
                Nexts.Add(Entry as Next);
                }
            if (Entry.GetType() == typeof(Input)) {
                Inputs.Add(Entry as Input);
                }
            if (Entry.GetType() == typeof(Output)) {
                Outputs.Add(Entry as Output);
                }
            }


        public override void Normalize(_Choice Parent) {
            XNormalize(Parent);
            Wizard = Parent as Wizard;
            Wizard.Dialogs.Add(this);

            GUI = Wizard.GUI;
            Dialog = this;

            foreach (var Entry in Entries) {
                Entry.Normalize(this);
                }
            }

        }

    public partial class Action {
        public override string XID { get { return "Action_" + Id; } }
        }

    public partial class Input {
        public override string XID { get { return "Input_" + Id; } }
        }

    public partial class Output {
        public override string XID { get { return "Output_" + Id; } }
        }

    public partial class Horizontal {

        public override void Normalize(_Choice Parent) {
            XNormalize(Parent);

            foreach (var Entry in Entries) {
                Entry.Normalize(this);
                }
            }
        }

    public partial class Vertical {

        public override void Normalize(_Choice Parent) {
            XNormalize(Parent);

            foreach (var Entry in Entries) {
                Entry.Normalize(this);
                }
            }
        }

    }
