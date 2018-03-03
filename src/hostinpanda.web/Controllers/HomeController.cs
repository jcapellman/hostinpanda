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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}