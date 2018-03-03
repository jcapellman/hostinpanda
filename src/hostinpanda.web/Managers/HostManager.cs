﻿using System;
using System.Collections.Generic;
using System.Linq;
using hostinpanda.serverlibrary.Wrappers;
using hostinpanda.web.Common;
using hostinpanda.web.DAL;
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
            using (var eFactory = new EntityFactory(Wrapper.DBConnectionString))
            {
                var hostsResult = eFactory.Hosts.Where(a => a.UserID == userID).ToList();
                
                return new ReturnContainer<List<HostListingResponseItem>>(hostsResult.Select(a => new HostListingResponseItem
                {
                    Alive = a.Active,
                    HostAddress = a.HostName,
                    LastPingBack = DateTime.Now
                }).ToList());
            }
        }
    }
}