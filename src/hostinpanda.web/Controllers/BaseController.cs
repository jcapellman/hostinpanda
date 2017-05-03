using Microsoft.AspNetCore.Mvc;

using hostinpanda.serverlibrary.Wrappers;

namespace hostinpanda.web.Controllers
{
    public class BaseController : Controller
    {
        protected ManagerWrapper Wrapper => new ManagerWrapper
        {
            DBConnectionString = "localhost"
        };

        protected int CurrentUserID { get; set; }

        public ActionResult ErrorView(string content)
        {
            return View("Error", "Home");
        }
    }
}