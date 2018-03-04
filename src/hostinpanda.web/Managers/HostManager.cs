using System;
using System.Collections.Generic;
using System.Linq;

using hostinpanda.web.Common;
using hostinpanda.web.Transports.Hosts;

namespace hostinpanda.web.Managers
{
    public class HostManager : BaseManager
    {
        public HostManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public ReturnContainer<List<HostListingResponseItem>> GetHostListing(int userID)
        {
            var hostsResult = Wrapper.DbContext.Hosts.Where(a => a.UserID == userID).ToList();
                
            return new ReturnContainer<List<HostListingResponseItem>>(hostsResult.Select(a => new HostListingResponseItem
            {
                Alive = a.Active,
                HostAddress = a.HostName,
                LastPingBack = DateTime.Now
            }).ToList());         
        }
    }
}