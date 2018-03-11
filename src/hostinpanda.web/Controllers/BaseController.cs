using Microsoft.AspNetCore.Mvc;

using hostinpanda.web.Common;
using hostinpanda.web.DAL;

using System.Security.Claims;
using System;

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
            CurrentUserID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value)
        };
        
        public ActionResult ErrorView(string content)
        {
            return View("Error", "Home");
        }
    }
}