using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Goedel.Registry;

namespace GoedelDomainer {

    public class Conversion {
        DomainerType Type;
        string Declaration;

        public Conversion(DomainerType Type, string Declaration) {
            this.Type = Type;
            this.Declaration = Declaration;
            }
 
        // There are faster ways to do this, but this is the most readable
        static Conversion Lookup(DomainerType Key, IList<Conversion> Items) {
            foreach (Conversion Entry in Items) {
                if (Entry.Type == Key) return Entry;
                }
            return null;
            }
        }

    public partial class Domainer : Goedel.Registry.Parser {

        static readonly IList<Conversion> ConversionCS = new ReadOnlyCollection<Conversion>(new[] {
            new Conversion (DomainerType.IPv4, "IPAddress"),
            new Conversion (DomainerType.IPv6, "IPAddress"),
            new Conversion (DomainerType.Domain, "string"),
            new Conversion (DomainerType.Mail, "string"),
            new Conversion (DomainerType.NodeID, "string"),
            new Conversion (DomainerType.Byte, "byte"),
            new Conversion (DomainerType.Int16, "ushort"),
            new Conversion (DomainerType.Int32, "uint"),
            new Conversion (DomainerType.Time32, "uint"),        
            new Conversion (DomainerType.Time48, "ulong"),
            new Conversion (DomainerType.String, "string"),
            new Conversion (DomainerType.OptionalString, "string"),        
            new Conversion (DomainerType.Strings, "string"),
            new Conversion (DomainerType.StringX, "string"),
            new Conversion (DomainerType.Binary, "byte []"),
            new Conversion (DomainerType.Binary8, "byte []"),
            new Conversion (DomainerType.Binary16, "byte []"),
            new Conversion (DomainerType.LBinary, "byte []"),
            new Conversion (DomainerType.Hex, "byte []"),
            new Conversion (DomainerType.Hex8, "byte []"),
            new Conversion (DomainerType.Hex16, "byte []"),
            });
        }

    public abstract partial class _Choice {
        public Boolean IsType;
        public ID<_Choice> TypeId;

        public Conversion ConversionCS;
        }
    }
