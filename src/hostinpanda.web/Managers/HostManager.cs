using System;
using System.Collections.Generic;
using System.Linq;

using hostinpanda.web.Common;
using hostinpanda.web.DAL.Tables;
using hostinpanda.web.Models;
using hostinpanda.web.Transports.Hosts;

namespace hostinpanda.web.Managers
{
    public class HostManager : BaseManager
    {
        public HostManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public ReturnContainer<bool> AddHost(NewHostModel model)
        {
            var host = new Hosts
            {
                AlertsEnabled = true,
                HostName = model.HostName,
                UserID = Wrapper.CurrentUser.ID.Value
            };

            Wrapper.DbContext.Hosts.Add(host);

            return new ReturnContainer<bool>(Wrapper.DbContext.SaveChanges() > 0);
        }

        public ReturnContainer<List<HostListingResponseItem>> GetHostListing()
        {
            var hostsResult = Wrapper.DbContext.Hosts.Where(a => a.UserID == Wrapper.CurrentUser.ID.Value).ToList();
                
            return new ReturnContainer<List<HostListingResponseItem>>(hostsResult.Select(a => new HostListingResponseItem
            {
                Alive = a.Active,
                HostAddress = a.HostName,
                LastPingBack = DateTime.Now
            }).ToList());         
        }
    }
}