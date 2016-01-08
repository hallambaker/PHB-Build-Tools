using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoedelDomainer {

    public abstract partial class _Choice {
        public virtual string TypeCS { get { return null; } }
        public virtual string IdLabel {get {return null; } }
        public virtual string Tag { get { return null; } }
        public virtual string Buffer { get { return null; } }
        }
    public partial class IPv4 : _Choice {
        public override string TypeCS { get { return "IPAddress"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "IPv4"; }}
        }
    public partial class IPv6 : _Choice {
        public override string TypeCS { get { return "IPAddress"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "IPv6"; }}
        }
    public partial class Domain : _Choice {
        public override string TypeCS { get { return "Domain"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Domain"; }}
        }
    public partial class Mail : _Choice {
        public override string TypeCS { get { return "string"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Mail"; }}
        }
    public partial class NodeID : _Choice {
        public override string TypeCS { get { return "ulong"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "NodeID"; }}
        }
    public partial class Byte : _Choice {
        public override string TypeCS { get { return "byte"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Byte"; }}
        }
    public partial class Int16 : _Choice {
        public override string TypeCS { get { return "ushort"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Int16"; }}
        }
    public partial class LByte : _Choice {
        public override string TypeCS { get { return "byte"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Byte"; }}
        }
    public partial class LInt16 : _Choice {
        public override string TypeCS { get { return "ushort"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Int16"; }}
        }
    public partial class Int32 : _Choice {
        public override string TypeCS { get { return "uint"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Int32"; }}
        }
    public partial class Time32 : _Choice {
        public override string TypeCS { get { return "uint"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Time32"; }}
        }
    public partial class Time48 : _Choice {
        public override string TypeCS { get { return "ulong"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Time48"; }}
        }
    public partial class String : _Choice {
        public override string TypeCS { get { return "string"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "String"; }}
        }
    public partial class OptionalString : _Choice {
        public override string TypeCS { get { return "string"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "OptionalString"; }}
        }
    public partial class Strings : _Choice {
        public override string TypeCS { get { return "List<string>"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Strings"; }}
        }
    public partial class StringX : _Choice {
        public override string TypeCS { get { return "string"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "StringX"; }}
        }
    public partial class Binary : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Binary"; }}
        }
    public partial class Binary8 : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Binary8"; }}
        }
    public partial class Binary16 : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Binary16"; }}
        }
    public partial class LBinary : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "LBinary"; }}
        }
    public partial class Hex : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Hex"; }}
        }
    public partial class Hex8 : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Hex8"; }}
        }
    public partial class Hex16 : _Choice {
        public override string TypeCS { get { return "byte[]"; } }
        public override string IdLabel {get {return Id.Label; } }
        public override string Tag {get {return "Hex16"; }}
        }
    public partial class OptionList : _Choice {
        public override string TypeCS { get { return "List<DNSOption>"; } }
        public override string IdLabel {get {return Id.Label; } }
        }
    public partial class Gateway : _Choice {
        public override string TypeCS { get { return "DNSGateway"; } }
        public override string IdLabel {get {return Id.Label; } }
        }
    }
