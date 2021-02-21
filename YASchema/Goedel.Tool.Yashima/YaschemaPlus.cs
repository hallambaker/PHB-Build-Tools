
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Registry;
using Goedel.Utilities;

namespace Goedel.Tool.Yaschema {
    /// <summary>
    /// 
    /// </summary>
    public partial class YaschemaStruct {


        public const string PacketPrefix = "Xpacket";

        public Namespace Namespace;

        public string NameSpaceName => Namespace?.Id.Label;
        public string Class => Namespace.Class.Label;

        public List<Packet> Packets = new();
        public override void Init() {

            if (_Initialized) {
                return;
                }
            _InitChildren();


            //foreach (var entry in Top) {
            //    switch (entry) {
            //        case Namespace nameSpace: {
            //            Namespace = nameSpace;
            //            break;
            //            }

            //        }
            //    //entry.Init(null);
            //    }

            }

        }
    public partial class Packet {

        public string ClassName => YaschemaStruct.PacketPrefix + Id.Label;

        public bool IsInitial => Initial != null;
        public bool IsInitialHostCredential => IsInitial && (Initial.HostCredential.Count > 0);



        public bool HasPlaintext => Plaintext != null;
        public bool HasMezzanine => Mezzanine != null;
        public bool HasEncrypted => Encrypted != null;


        public bool Completer => IsClient & (HasMezzanine | HasEncrypted);


        public Initial Initial;
        public Respond Respond;
        public Plaintext Plaintext;
        public Mezzanine Mezzanine;
        public Encrypted Encrypted;


        public bool IsClient;

        public string PacketType => IsClient ? "Client" : "Host";

        public override void Init(_Choice Parent) {
            base.Init(Parent);
            _Base.Packets.Add(this);


            IsClient = _Parent is Client;
            //if (Parent is Client client) {
            //    if (IsInitial) {
            //        if (IsInitialHostCredential) {
            //            client.WithHostCredential = this;
            //            }
            //        else {
            //            client.WithoutHostCredential = this;
            //            }
            //        }
            //    }

            }
        }

    public partial class _Choice {
        public bool Ephemerals = false;
        public bool Ephemeral = false;
        public bool KeyId = false;
        public bool Challenge = false;
        public bool Response = false;
        public bool Credential = false;


        public bool AddExtensions => Ephemerals | Credential | Challenge | Response;
        }

    public partial class Client {
        //public Packet WithHostCredential;
        //public Packet WithoutHostCredential;

        //public override void Init(_Choice Parent) : base{
        //    }
        }
    public partial class Host {

        }
    public partial class Namespace {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            _Base.Namespace = this;
            }
        }



    public partial class Initial {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            if (Parent is Packet packet) {
                packet.Initial = this;
                }
            }
        }
    public partial class Respond {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            if (Parent is Packet packet) {
                packet.Respond = this;
                }
            }
        }
    public partial class Plaintext {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            if (Parent is Packet packet) {
                packet.Plaintext = this;
                }
            }
        }
    public partial class Mezzanine {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            if (Parent is Packet packet) {
                packet.Mezzanine = this;
                }
            }
        }
    public partial class Encrypted {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            if (Parent is Packet packet) {
                packet.Encrypted = this;
                }
            }
        }


    public partial class ClientCredential {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Credential = true;
            }
        }
    public partial class HostCredential {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Credential = true;
            }
        }

    public partial class ClientEphemerals {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Ephemerals = true;
            }
        }
    public partial class HostEphemerals {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Ephemerals = true;
            }
        }
    public partial class ClientEphemeral {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Ephemeral = true;
            }
        }

    public partial class HostEphemeral {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Ephemeral = true;
            }
        }
    public partial class HostKeyID {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.KeyId = true;
            }
        }
    public partial class ClientKeyID {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.KeyId = true;
            }
        }

    public partial class Challenge {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Challenge = true;
            }
        }
    public partial class Response {
        public override void Init(_Choice Parent) {
            base.Init(Parent);
            Parent.Response = true;
            }
        }

    }