using hostinpanda.clientlibrary.Common;
using hostinpanda.serverlibrary.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hostinpanda.web.Controllers
{
    public class HostsController : BaseController
    {
        public HostsController(IOptions<GlobalSettings> argGlobalSettings) : base(argGlobalSettings.Value)
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