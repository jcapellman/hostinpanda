using Microsoft.AspNetCore.Mvc;

using hostinpanda.serverlibrary.Wrappers;
using hostinpanda.web.Common;

namespace hostinpanda.web.Controllers
{
    public class BaseController : Controller
    {
        protected GlobalSettings globalSettings;

        public BaseController(GlobalSettings argGlobalSettings)
        {
            globalSettings = argGlobalSettings;
        }

        protected ManagerWrapper Wrapper => new ManagerWrapper
        {
            DBConnectionString = globalSettings.DatabaseConnection
        };

        protected int CurrentUserID { get; set; }

        public ActionResult ErrorView(string content)
        {
            return View("Error", "Home");
        }
    }
}