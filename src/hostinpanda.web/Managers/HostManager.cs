using System;
using System.Collections.Generic;
using System.Linq;

using hostinpanda.library.Common;
using hostinpanda.web.Common;
using hostinpanda.library.DAL.Tables;
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
                UserID = Wrapper.CurrentUser.ID.Value,
                AllowableDowntimeMinutes = model.AllowableDowntimeMinutes,
                PortNumber = model.PortNumber
            };

            Wrapper.DbContext.Hosts.Add(host);

            return new ReturnContainer<bool>(Wrapper.DbContext.SaveChanges() > 0);
        }

        public ReturnContainer<bool> UpdateHost(int id, string hostName, int portNumber, int allowableDowntimeMinutes)
        {
            var host = Wrapper.DbContext.Hosts.FirstOrDefault(a => a.ID == id && a.UserID == Wrapper.CurrentUser.ID && a.Active);

            if (host == null)
            {
                return new ReturnContainer<bool>(false, $"UpdateHost: Could not obtain {id}");
            }

            host.HostName = hostName;
            host.PortNumber = portNumber;
            host.AllowableDowntimeMinutes = allowableDowntimeMinutes;

            Wrapper.DbContext.SaveChanges();

            return new ReturnContainer<bool>(true);
        }

        public ReturnContainer<Hosts> GetHost(int id)
        {
            var host = Wrapper.DbContext.Hosts.FirstOrDefault(a => a.ID == id && a.UserID == Wrapper.CurrentUser.ID && a.Active);

            return host == null ? new ReturnContainer<Hosts>(null, $"GetHost: Could not obtain {id}") : new ReturnContainer<Hosts>(host);
        }

        public ReturnContainer<bool> RemoveHost(int id)
        {
            var host = Wrapper.DbContext.Hosts.FirstOrDefault(a => a.ID == id && a.UserID == Wrapper.CurrentUser.ID && a.Active);

            if (host == null)
            {
                return new ReturnContainer<bool>(false, $"RemoveHost: Could not obtain {id} to remove");
            }

            Wrapper.DbContext.Hosts.Remove(host);
            Wrapper.DbContext.SaveChanges();

            return new ReturnContainer<bool>(true);
        }

        public ReturnContainer<List<HostListingResponseItem>> GetHostListing()
        {
            var hostsResult = Wrapper.DbContext.Hosts.Where(a => a.UserID == Wrapper.CurrentUser.ID.Value).ToList();
                
            return new ReturnContainer<List<HostListingResponseItem>>(hostsResult.Select(a => new HostListingResponseItem
            {
                ID = a.ID,
                PortNumber = a.PortNumber,
                Alive = a.Active,
                HostAddress = a.HostName,
                LastPingBack = DateTime.Now
            }).ToList());         
        }
    }
}