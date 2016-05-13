using System;
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


    public abstract class Wizard {
        public abstract string Title { get; }
        public abstract List<string> Texts { get;  }
        public abstract List<Step> Steps { get; } 

        }


    public class Step {
        public string Title = "";
        public List<string> Description = new List<string>();
        public Object Object;



        }

    public abstract class Model {
        public List<Object> Selector = new List<Object>();


        public About _About;


        public abstract void Dispach(string Command);

            }

    public abstract class Menu {
        public abstract List<MenuEntry> Entries {
            get; set;
            }
        }

    public class MenuEntry {
        public string Id;
        public string Label;

        public MenuEntry(string Id, string Label) {
            this.Id = Id;
            this.Label = Label;
            }
        }

    public class SubMenu : MenuEntry {
        public Menu Sub;

        public SubMenu(string Id, string Label, Menu Sub) :
                base(Id, Label) {
            this.Sub = Sub;
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


    public abstract class Object {
        public BindingData BindingData;

        /// <summary>
        /// Every object has a Name handle for display use.
        /// </summary>
        public virtual string Title { get; set; }

        public abstract List<ObjectEntry> Entries {
            get; set;
            }

        public virtual void MakeWidgets() {
            }

        }

    public class ObjectEntry {
        public string Id;
        public string Label;


        public virtual object ObjectValue { get { return null; }  set { } }

        }

    public class ObjectText : ObjectEntry {
        public string Text;

        }

    public abstract class ObjectField : ObjectEntry {
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

        public List<IFieldBinding> FieldBindings;



        }

    public class ObjectCommand : ObjectEntry {

        }

    }
