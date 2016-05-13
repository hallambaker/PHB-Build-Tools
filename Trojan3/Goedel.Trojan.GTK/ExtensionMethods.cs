using System;
using System.Collections.Generic;
using Gtk;

namespace Goedel.Trojan.GTK {
    public static class ExtensionMethods {
        static CssProvider _DefaultProvider;
        static uint _DefaultPriority = 1000;

        /// <summary>
        /// The CSS provider to be applied by ApplyCss()
        /// </summary>
        public static CssProvider DefaultProvider {
            get {
                return _DefaultProvider;
                }

            set {
                _DefaultProvider = value;
                }
            }

        /// <summary>
        /// The priority for the CSS provider to be applied by ApplyCss()
        /// </summary>
        public static uint DefaultPriority {
            get {
                return _DefaultPriority;
                }

            set {
                _DefaultPriority = value;
                }
            }

        /// <summary>
        /// Apply CSS with DefaultProvider and DefaultPriority
        /// </summary>
        /// <param name="Widget">The widget to which the CSS parameters are to be applied</param>
        public static void ApplyCss(this Widget Widget) {
            Widget.ApplyCss(DefaultProvider, DefaultPriority);
            }

        /// <summary>
        /// Apply CSS provider to widget and all children.
        /// </summary>
        /// <param name="Widget">The widget to which the CSS parameters are to be applied</param>
        /// <param name="Provider">Provider to apply.</param>
        /// <param name="Priority">Priority of provider.</param>
        public static void ApplyCss(this Widget Widget, CssProvider Provider, uint Priority) {
            Widget.StyleContext.AddProvider(Provider, Priority);
            var Container = Widget as Container;
            if (Container != null) {
                foreach (var Child in Container.Children) {
                    Child.ApplyCss(Provider, Priority);
                    }
                }
            }

        }
    }
