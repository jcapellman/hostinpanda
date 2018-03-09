using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using hostinpanda.web.DAL;
using hostinpanda.web.Managers;
using hostinpanda.web.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(DALdbContext dbContext) : base(dbContext)
        {
        }

        public ActionResult AttemptLogin(LoginModel model)
        {
            var result = new UserManager(Wrapper).Login(model.Username, model.Password);

            if (!result.HasError && result.ObjectValue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.ObjectValue.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
            }

            return result.HasError ? ErrorView(result.ErrorString) : RedirectToAction("Index", "Hosts");
        }

        public ActionResult Index() => View(new LoginModel());

        public ActionResult Register() => View(new RegisterModel());

        public async Task<ActionResult> AttemptRegister(RegisterModel model)
        {
            var result = await new UserManager(Wrapper).CreateUser(model.Username, model.Password);

            return result.HasError ? ErrorView(result.ErrorString) : RedirectToAction("Index", "Home");
        }
    }
}