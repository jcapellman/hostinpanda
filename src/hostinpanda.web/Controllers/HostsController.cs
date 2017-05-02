using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using hostinpanda.serverlibrary.Managers;

namespace hostinpanda.web.Controllers
{
    public class HostsController : BaseController
    {
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