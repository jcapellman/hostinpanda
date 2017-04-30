using Microsoft.AspNetCore.Mvc;

using hostinpanda.serverlibrary.Managers;

namespace hostinpanda.web.Controllers
{
    public class HostsController : BaseController
    {
        public ActionResult Index()
        {
            var hostResponse = new HostManager(Wrapper).GetHostListing(CurrentUserID);

            return hostResponse.HasError ? ErrorView(hostResponse.ErrorString) : View("Index", hostResponse.ObjectValue);
        }
    }
}