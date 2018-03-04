using hostinpanda.web.DAL;
using hostinpanda.web.Managers;

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

        public ActionResult Index()
        {
            var hostResponse = new HostManager(Wrapper).GetHostListing(CurrentUserID);

            if (hostResponse.HasError)
            {
                return ErrorView(hostResponse.ErrorString);
            }

            return hostResponse.HasError ? ErrorView(hostResponse.ErrorString) : View("Index", hostResponse.ObjectValue);
        }
    }
}