using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.Tool.Domainer;




public abstract partial class _Choice {
    public virtual string TypeCS => null;
    public virtual string IdLabel => null;
    public virtual string Tag => null;
    public virtual string Buffer => null;
    }

public interface DnsRecord {
    string IdLabel { get; }

    int TypeCode { get; }
    }

public partial class RR : DnsRecord {
    public override string IdLabel => Id.Label;
    public int TypeCode => Code;
    }
public partial class Q : DnsRecord {
    public override string IdLabel => Id.Label;
    public int TypeCode => Code;
    }

public partial class IPv4 : _Choice {
    public override string TypeCS => "IPAddress";
    public override string IdLabel => Id.Label;
    public override string Tag => "IPv4";
    }
public partial class IPv6 : _Choice {
    public override string TypeCS => "IPAddress";
    public override string IdLabel => Id.Label;
    public override string Tag => "IPv6";
    }
public partial class Domain : _Choice {
    public override string TypeCS => "Domain";
    public override string IdLabel => Id.Label;
    public override string Tag => "Domain";
    }
public partial class Mail : _Choice {
    public override string TypeCS => "string";
    public override string IdLabel => Id.Label;
    public override string Tag => "Mail";
    }
public partial class NodeID : _Choice {
    public override string TypeCS => "ulong";
    public override string IdLabel => Id.Label;
    public override string Tag => "NodeID";
    }
public partial class Byte : _Choice {
    public override string TypeCS => "byte";
    public override string IdLabel => Id.Label;
    public override string Tag => "Byte";
    }
public partial class Int16 : _Choice {
    public override string TypeCS => "ushort";
    public override string IdLabel => Id.Label;
    public override string Tag => "Int16";
    }
public partial class LByte : _Choice {
    public override string TypeCS => "byte";
    public override string IdLabel => Id.Label;
    public override string Tag => "Byte";
    }
public partial class LInt16 : _Choice {
    public override string TypeCS => "ushort";
    public override string IdLabel => Id.Label;
    public override string Tag => "Int16";
    }
public partial class Int32 : _Choice {
    public override string TypeCS => "uint";
    public override string IdLabel => Id.Label;
    public override string Tag => "Int32";
    }
public partial class Time32 : _Choice {
    public override string TypeCS => "uint";
    public override string IdLabel => Id.Label;
    public override string Tag => "Time32";
    }
public partial class Time48 : _Choice {
    public override string TypeCS => "ulong";
    public override string IdLabel => Id.Label;
    public override string Tag => "Time48";
    }
public partial class String : _Choice {
    public override string TypeCS => "string";
    public override string IdLabel => Id.Label;
    public override string Tag => "String";
    }
public partial class OptionalString : _Choice {
    public override string TypeCS => "string";
    public override string IdLabel => Id.Label;
    public override string Tag => "OptionalString";
    }
public partial class Strings : _Choice {
    public override string TypeCS => "List<string>";
    public override string IdLabel => Id.Label;
    public override string Tag => "Strings";
    }
public partial class StringX : _Choice {
    public override string TypeCS => "string";
    public override string IdLabel => Id.Label;
    public override string Tag => "StringX";
    }
public partial class Binary : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "Binary";
    }
public partial class Binary8 : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "Binary8";
    }
public partial class Binary16 : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "Binary16";
    }
public partial class LBinary : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "LBinary";
    }
public partial class Hex : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "Hex";
    }
public partial class Hex8 : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "Hex8";
    }
public partial class Hex16 : _Choice {
    public override string TypeCS => "byte[]";
    public override string IdLabel => Id.Label;
    public override string Tag => "Hex16";
    }
public partial class OptionList : _Choice {
    public override string TypeCS => "List<DNSOption>";
    public override string IdLabel => Id.Label;
    }
public partial class Gateway : _Choice {
    public override string TypeCS => "DNSGateway";
    public override string IdLabel => Id.Label;
    }
