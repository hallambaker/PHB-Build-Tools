using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Trojan.GTK {
    class CSSProvider {

        static Gtk.CssProvider _Default = null;

        static Gtk.CssProvider Default {
            get {
                if (_Default == null) {
                    _Default = GetProvider();
                    }
                return _Default;
                }
            }

        static Gtk.CssProvider GetProvider() {

            return null;
            }


        }
    }
