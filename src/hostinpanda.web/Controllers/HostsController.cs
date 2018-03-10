using hostinpanda.web.DAL;
using hostinpanda.web.Managers;
using hostinpanda.web.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    [Authorize]
    public class HostsController : BaseController
    {
        public HostsController(DALdbContext dbContext) : base(dbContext)
        {
        }
        
        public ActionResult CreateHost(NewHostModel model)
        {
            var response = new HostManager(Wrapper).AddHost(model);

            if (response.HasError)
            {
                return ErrorView(response.ErrorString);
            }

            return Index();
        }

        public ActionResult AddNew() => View();

        public ActionResult Index()
        {
            var hostResponse = new HostManager(Wrapper).GetHostListing();

            if (hostResponse.HasError)
            {
                return ErrorView(hostResponse.ErrorString);
            }

            return hostResponse.HasError ? ErrorView(hostResponse.ErrorString) : View("Index", hostResponse.ObjectValue);
        }
    }
}