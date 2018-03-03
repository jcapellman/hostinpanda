using System;
using System.Runtime.Serialization;

namespace hostinpanda.web.Transports.Hosts
{
    [DataContract]
    public class HostListingResponseItem
    {
        [DataMember]
        public string HostAddress { get; set; }

        [DataMember]
        public bool Alive { get; set; }

        [DataMember]
        public DateTime? LastPingBack { get; set; }
    }
}