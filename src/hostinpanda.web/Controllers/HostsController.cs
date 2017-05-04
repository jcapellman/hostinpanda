using System.Threading.Tasks;
using hostinpanda.clientlibrary.Common;
using Microsoft.AspNetCore.Mvc;

using hostinpanda.serverlibrary.Managers;
using Microsoft.Extensions.Options;

namespace hostinpanda.web.Controllers
{
    public class HostsController : BaseController
    {
        public HostsController(IOptions<GlobalSettings> argGlobalSettings) : base(argGlobalSettings.Value)
        {
        }

        public async Task<ActionResult> Index()
        {
            var hostResponse = await new HostManager(Wrapper).GetHostListingAsync(CurrentUserID);

            if (hostResponse.HasError)
            {
                return ErrorView(hostResponse.ErrorString);
            }

            return hostResponse.HasError ? ErrorView(hostResponse.ErrorString) : View("Index", hostResponse.ObjectValue);
        }
    }
}