using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Goedel.Trojan {


    public class About {

        public string Version;
        public string Name;
        public DateTime CreatedDate;

        public About(Model Model) {
            var Assembly = Model.GetType().Assembly;
            var AssemblyName = Assembly.GetName();
            var AssemblyVersion = AssemblyName.Version;

            Version = AssemblyVersion.ToString();
            Name = AssemblyName.FullName;

            CreatedDate = new DateTime(2000, 1, 1).AddDays(
                    AssemblyVersion.Build).AddSeconds(AssemblyVersion.MinorRevision * 2);

            }
        }


    public abstract class Wizard : Object {
        public abstract List<string> Texts { get; }
        public abstract List<Step> Steps { get; }

        public abstract bool Dispatch(int Step);
        public override List<ObjectEntry> Entries {
            get { return null; }  set { }
            }
        private int _StepIndex = -1;

        public Step CurrentStep {
            get { return _StepIndex >= 0 ? Steps[_StepIndex] : null; }
            }

        public int StepIndex {
            get {
                return _StepIndex;
                }

            set {
                _StepIndex = value;
                if (CurrentStep != null) {
                    CurrentStep.Value.Initialize(this);
                    }
                }
            }


        /// <summary>
        /// Called if the user exits a wizard part way through.
        /// </summary>
        public virtual void Exit() {
            }


        /// <summary>
        /// Default base constructor, bind the Wizard to a Model.
        /// </summary>
        /// <param name="Model"></param>
        public Wizard(Model Model) {
            this.Model = Model;
            foreach (var Step in Steps) {
                Step.Value.Initialize(Model);
                }
            }

        }


    public class Step {
        public string Title = "";
        public List<string> Description = new List<string>();
        public Object Value;

        }

    public abstract class Model {
        public List<Object> Selector = new List<Object>();
        public About _About;

        public abstract void Dispatch(string ID);


        /// <summary>
        /// The currently active selection or null if no object is selected.
        /// </summary>
        public Object Selected = null;
        }

    public abstract class Menu {
        public abstract List<MenuEntry> Entries {
            get; set;
            }
        }

    public class MenuEntry {
        public string Id;
        public string Label;
        }

    public class MenuDivider : MenuEntry {
        }

    public class SubMenu : MenuEntry {
        public Menu Sub;
        public List<MenuEntry> Entries {
            get; set;
            }
        }


    public enum WidgetType {
        Integer,
        String,
        Secret,
        DateTime,
        Date,
        Time,
        Boolean,
        Set,
        Option,
        Item,
        List,
        Chooser
        }


    public abstract class Object : IEnumerable {
        public BindingData BindingData;

        /// <summary>
        /// Every object has a Name handle for display use.
        /// </summary>
        public virtual string Title { get; set; }

        public abstract List<ObjectEntry> Entries {
            get; set;
            }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator)GetEnumerator();
            }

        public virtual ObjectEnum GetEnumerator() {
            return new ObjectEnum(this);
            }

        /// <summary>
        /// Create a list containing all the current children.
        /// </summary>
        /// <returns></returns>
        public virtual List<Object> GetChildren() {
            return new List<Object>();
            }


        public Model Model { get; set; }
        protected Wizard Wizard { get; set; }

        /// <summary>
        /// Initialize object with parameters from the model
        /// </summary>
        public virtual void Initialize(Model Model) {
            this.Model = Model;
            }

        /// <summary>
        /// Initialize object with parameters from a Wizard
        /// </summary>
        public virtual void Initialize(Wizard Wizard) {
            this.Wizard = Wizard;
            }


        /// <summary>
        /// Check object for validity.
        /// </summary>
        /// <returns>true if the validity check fails, false otherwise.</returns>
        public virtual bool Valid() {
            bool Result = true;

            foreach (var Entry in Entries) {
                if (Entry as ObjectField != null) {
                    var EntryField = Entry as ObjectField;
                    Result = Result & EntryField.Valid();

                    }
                }


            return Result;
            }

        /// <summary>
        /// Dispatch callback called on 'Accept' in edit/dialog mode 
        /// </summary>
        /// <returns>true if success, otherwise error.</returns>
        public virtual bool Dispatch() {
            return true;
            }

        /// <summary>
        /// Dispatch callback called on 'next' in Wizard mode. 
        /// </summary>
        /// <returns>true if success, otherwise error.</returns>
        public virtual bool Dispatch(Wizard Wizard) {
            return true;
            }

        }


    public class ObjectEnum : IEnumerator {
        List<Object> Children;
        int Index;


        public ObjectEnum(Object Object) {
            Children = Object.GetChildren();
            Reset();
            }

        public bool MoveNext() {
            if (++Index < Children.Count) {
                return true;
                }
            else {
                return false;
                }
            }

        public void Reset() {
            Index = 0;
            }

        public Object Current {
            get {
                return Children?[Index];
                }
            }

        object IEnumerator.Current {
            get {
                return Children?[Index];
                }
            }

        }

    public class ObjectEntry {
        public string Id;
        public string Label;


        //public virtual ObjectField ObjectValue { get { return null; }  set { } }

        }

    public class ObjectText : ObjectEntry {
        public string Text;

        }

    public class ObjectAction : ObjectEntry {
        public string Text;

        }

    public abstract class ObjectField : ObjectEntry {
        bool Constant = false;

        public int Index;

        public virtual string ReasonInvalid {
            set {
                if (FieldBindings != null) {
                    foreach (var Binding in FieldBindings) {
                        Binding.ReasonInvalid = value;
                        }
                    }
                }
            }

        public List<IFieldBinding> FieldBindings = new List<IFieldBinding> ();


        public virtual void Destroy(IFieldBinding FieldBinding) {
            // remove FieldBinding from FieldBindings;
            FieldBindings.Remove(FieldBinding);
            }


        public virtual bool Valid() {
            return true;
            }

        public abstract void Apply();

        bool _ReadOnly = false;
        public virtual bool ReadOnly {
            get { return _ReadOnly; }
            set {
                if (FieldBindings != null) {
                    foreach (var Binding in FieldBindings) {
                        Binding.ReadOnly = value;
                        }


                    }
                }
            }

        public virtual string Tip { get; set; }

        }

    public class ObjectCommand : ObjectEntry {

        }

    }
