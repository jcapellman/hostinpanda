using hostinpanda.clientlibrary.Common;
using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(GlobalSettings argGlobalSettings) : base(argGlobalSettings)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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