using System.Collections.Generic;

using hostinpanda.clientlibrary;
using hostinpanda.clientlibrary.Transports.Hosts;
using hostinpanda.serverlibrary.Wrappers;

namespace hostinpanda.serverlibrary.Managers
{
    public class HostManager : BaseManager
    {
        public HostManager(ManagerWrapper wrapper) : base(wrapper)
        {
        }

        public ReturnContainer<List<HostListingResponseItem>> GetHostListing(int userID)
        {
            return new ReturnContainer<List<HostListingResponseItem>>(null);
        }
    }
}