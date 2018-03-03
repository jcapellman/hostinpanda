using hostinpanda.web.Common;
using hostinpanda.web.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hostinpanda.web.Controllers
{
    [Authorize]
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