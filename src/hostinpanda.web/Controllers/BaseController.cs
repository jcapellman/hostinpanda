using Microsoft.AspNetCore.Mvc;

using hostinpanda.web.Common;
using hostinpanda.web.DAL;

namespace hostinpanda.web.Controllers
{
    public class BaseController : Controller
    {
        private readonly DALdbContext dbContext;
        
        public BaseController(DALdbContext dbContext = null)
        {
            this.dbContext = dbContext;
        }

        protected ManagerWrapper Wrapper => new ManagerWrapper
        {
            DbContext = dbContext,
            CurrentUser = new HostinUser(User)
        };
        
        public ActionResult ErrorView(string content)
        {
            return View("Error", "Home");
        }
    }
}