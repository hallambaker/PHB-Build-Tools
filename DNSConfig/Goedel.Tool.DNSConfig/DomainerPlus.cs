using System;
using System.Collections.Generic;
using Goedel.Discovery;
using Goedel.Registry;
using System.Text;
using System.Net;

namespace Goedel.Tool.DNSConfig {

    public partial class Generate {

        }

    public partial class DNSConfig {



        public DNS DefaultDns = null;
        public List<Domain> Domains = new();
        public List<Machine> Machines = new();

        public override void Init () {
            if (_Initialized) {
                return;
                }

            foreach (var Entry in Top) {
                switch (Entry) {
                    case Machine Machine: {
                        Machines.Add(Machine);
                        break;
                        }
                    case DNS DNS: {
                        DefaultDns ??= DNS;
                        break;
                        }
                    case Site Site: {
                        Site.DNS = DefaultDns;
                        foreach (var Domain in Site.Domain) {
                            Domains.Add(Domain);
                            Domain.DNS = DefaultDns;
                            Domain.Web = Site.Web;
                            }
                        break;
                        }
                    }

                Entry.Init(null);
                }


            }

        }


    public partial class DNS {
        public DateTime Now = DateTime.Now;
        public int Serial => 01 + (Now.Day * 100) + (Now.Month * 10000) + (Now.Year * 1000000);

        public Authoritative Master => Authoritative[0];

        }

    public partial class Site {
        public DNS DNS;


        public override void Init (_Choice Parent) {


            if (Domain.Count > 1) {
                Domain[0].IsPrimary = true;
                }


            }

        }

    public partial class Domain {
        public DNS DNS;
        public Web Web;
        public bool IsPrimary;
        public override void Init (_Choice Parent) {

            }


        public List<DNSRecord_A> Records_A = new();
        public List<DNSRecord_AAAA> Records_AAAA = new();

        //public string MakeAddress (string Data, ID<_Choice> First, ID<_Choice> Second = null) {
        //    return MakeAddress(Data, First.Label, Second?.Label);
        //    }

        //public string MakeAddress (string Data, string First, ID<_Choice> Second = null) {
        //    return MakeAddress(Data, First, Second?.Label);
        //    }

        public string MakeAddress(Machine Machine, ID<_Choice> First, ID<_Choice> Second = null) => MakeAddress(Machine, First.Label, Second?.Label);

        public string MakeAddress(Machine Machine, string First, ID<_Choice> Second = null) => MakeAddress(Machine, First, Second?.Label);

        public string MakeAddress (Machine Machine, string First, string Second = null) {


            var Builder = new StringBuilder();
            Builder.Append(First);
            if (Second != null) {
                Builder.Append(".");
                Builder.Append(Second);
                }

            var Label = Builder.ToString();

            var Address = IPAddress.Parse(Machine.Data);

            DNSRecord DNSRecord;

            if (Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                DNSRecord = new DNSRecord_A() {
                    Domain = new Goedel.Discovery.Domain(Label),
                    Address = Address
                    };
                Records_A.Add(DNSRecord as DNSRecord_A);
                Machine.Records_A.Add(DNSRecord as DNSRecord_A);
                }
            else {
                DNSRecord = new DNSRecord_AAAA() {
                    Domain = new Goedel.Discovery.Domain(Label),
                    Address = Address
                    };
                Records_AAAA.Add(DNSRecord as DNSRecord_AAAA);
                Machine.Records_AAAA.Add(DNSRecord as DNSRecord_AAAA);
                }

            return DNSRecord.Canonical();
            }


        }

    public partial class Machine {
        public List<DNSRecord_A> Records_A = new();
        public List<DNSRecord_AAAA> Records_AAAA = new();
        }

    public partial class Authoritative {
        public Machine Machine => Data.ID.Object as Machine;
        }

    public partial class Address {
        public Machine Machine => Data.ID.Object as Machine;
        }

    public partial class Host {

        public Address Address => Id.ID.Object as Address;
        public Machine Machine => Address.Machine;

        public string IP => Machine?.Data;
        public string Name => Id.Label;
        }

    public partial class Refresh {
        public int DefaultedTime => Time > 0 ? Time : 3600;
        }

    public partial class Retry {
        public int DefaultedTime => Time > 0 ? Time : 1800;
        }

    public partial class Expire {
        public int DefaultedTime => Time > 0 ? Time : 604800;
        }

    public partial class TTL {
        public int DefaultedTime => Time > 0 ? Time : 600;
        }



    }
