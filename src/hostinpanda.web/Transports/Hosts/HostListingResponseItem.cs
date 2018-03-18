using System;
using System.Runtime.Serialization;

namespace hostinpanda.web.Transports.Hosts
{
    [DataContract]
    public class HostListingResponseItem
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int PortNumber { get; set; }

        [DataMember]
        public string HostAddress { get; set; }

        [DataMember]
        public bool Alive { get; set; }

        [DataMember]
        public DateTime? LastPingBack { get; set; }
    }
}