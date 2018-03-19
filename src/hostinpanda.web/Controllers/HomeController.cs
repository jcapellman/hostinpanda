using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index() {
            if (Wrapper.CurrentUser?.ID != null)
            {
                return RedirectToAction("Index", "Hosts");
            }

            return View("Index");
        }

        public IActionResult Error(string errorString) => View("Error", errorString);
    }
}