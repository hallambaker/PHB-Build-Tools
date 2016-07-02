using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goedel.Trojan;

namespace Goedel.Trojan.GTK {
    public abstract class Widget : IFieldBinding {

        protected internal int Row;
        protected internal GridForm GridForm;
        protected internal ErrorLabel ErrorLabel = null;


        protected internal List<Gtk.Widget> WidgetSensitive = new List<Gtk.Widget>();
        protected internal List<Gtk.Widget> WidgetsAll = new List<Gtk.Widget>();

        protected internal bool _Changed = false;


        /// <summary>
        /// The widget to which tool tips should be applied
        /// </summary>
        public abstract Gtk.Widget WidgetTip { get; }

        /// <summary>
        /// The object field that this object maps to.
        /// </summary>
        public abstract ObjectField ObjectField { get; }

        /// <summary>
        /// Destruction method, called when the widget is being destroyed.
        /// </summary>
        public void Destroy() {
            foreach (var W in WidgetsAll) {
                W.Destroy();
                }
            ObjectField.Destroy(this);
            }


        /// <summary>
        /// Error text to report
        /// </summary>
        public string ReasonInvalid {
            set {
                ErrorLabel.Raise(ref ErrorLabel, GridForm, Row, value);
                }
            }

        /// <summary>
        /// When set true, user can modify contents, otherwise value is fixed.
        /// </summary>
        public virtual bool ReadOnly {
            set {
                foreach (var W in WidgetsAll) {
                    W.Sensitive = !value;
                    }
                }
            }

        /// <summary>
        /// Popup tool tip
        /// </summary>
        public string Tip { set { WidgetTip.TooltipText = value; } }

        /// <summary>
        /// Copy values from widget to model value field.
        /// </summary>
        public void Apply() {
            ObjectField.Apply();
            _Changed = false;
            }

        /// <summary>
        /// Set to true if the user has changed the value.
        /// </summary>
        public bool UserChangedValue {
            get { return _Changed; }
            set { _Changed = value; }
            }


        public abstract void Test();

        public abstract void Fill();

        }
    }
