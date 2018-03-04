using Microsoft.AspNetCore.Mvc;

using hostinpanda.web.Common;

namespace hostinpanda.web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly GlobalSettings GlobalSettings;

        public BaseController(GlobalSettings argGlobalSettings)
        {
            GlobalSettings = argGlobalSettings;
        }

        protected ManagerWrapper Wrapper => new ManagerWrapper
        {
            GSettings = GlobalSettings
        };

        protected int CurrentUserID { get; set; }

        public ActionResult ErrorView(string content)
        {
            return View("Error", "Home");
        }
    }
}