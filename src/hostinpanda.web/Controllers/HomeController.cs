using hostinpanda.web.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hostinpanda.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<GlobalSettings> argGlobalSettings) : base(argGlobalSettings.Value)
        {
        }

        public IActionResult Index() => View();

        public IActionResult Error() => View();
    }
}