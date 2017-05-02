using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using hostinpanda.clientlibrary;
using hostinpanda.clientlibrary.Transports.Hosts;
using hostinpanda.serverlibrary.DAL;
using hostinpanda.serverlibrary.DAL.Tables;
using hostinpanda.serverlibrary.Wrappers;

namespace hostinpanda.serverlibrary.Managers
{
    public class HostManager : BaseManager
    {
        public HostManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public async Task<ReturnContainer<List<HostListingResponseItem>>> GetHostListingAsync(int userID)
        {
            using (var eFactory = new EntityFactory(Wrapper.DBConnectionString))
            {
                var hostsResult = await eFactory.GetListAsync<Hosts>($"HOSTS_{userID}");

                if (hostsResult.HasError)
                {
                    throw new Exception(hostsResult.ErrorString);
                }

                return new ReturnContainer<List<HostListingResponseItem>>(hostsResult.ObjectValue.Select(a => new HostListingResponseItem
                {
                    Alive = a.Active,
                    HostAddress = a.HostName,
                    LastPingBack = DateTime.Now
                }).ToList());
            }
        }
    }
}