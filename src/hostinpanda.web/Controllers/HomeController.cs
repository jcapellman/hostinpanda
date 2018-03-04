using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index() => View();

        public IActionResult Error() => View();
    }
}