using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistryConfig {

    public partial class ConfigItems {

        public void Normalize() {
            foreach (var Entry in Top) {
                Entry.Normalize();
                }
            }

        }

    public partial class _Choice {

        public virtual void Normalize() {
            }

        }

    public partial class Class {

        public override void Normalize() {
            foreach (var Field in Fields) {
                Field.Normalize();
                }
            }

        }

    public partial class Field {
        public string CType {
            get {
                if (Type.GetType() == typeof(String)) {
                    return "string";
                    }
                if (Type.GetType() == typeof(Int)) {
                    return "uint";
                    }
                if (Type.GetType() == typeof(Binary)) {
                    return "byte[]";
                    }
                return null;
                }
            }

        /// <summary>
        /// 
        /// </summary>
        public string RegistryType {
            get {
                if (Type.GetType() == typeof(String)) {
                    return "SZ";
                    }
                if (Type.GetType() == typeof(Int)) {
                    return "DWORD";
                    }
                if (Type.GetType() == typeof(Binary)) {
                    return "BINARY";
                    }
                return null;
                }
            }

        string _Key = null;
        public string Key {
            get {
                return _Key == null ? Id.ToString () : _Key;
                }

            }


        public override void Normalize() {
            foreach (var Option in Options) {
                if (Option.GetType() == typeof (AltID)) {
                    _Key = (Option as AltID).Name;
                    }
                }
            }
        }

    }
