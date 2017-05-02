using System.Threading.Tasks;

using hostinpanda.serverlibrary.Managers;
using hostinpanda.web.Models;

using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    public class AccountController : BaseController
    {
        public async Task<ActionResult> AttemptLogin(LoginModel model)
        {
            var result = await new UserManager(Wrapper).Login(model.Username, model.Password);

            return result.HasError ? ErrorView(result.ErrorString) : RedirectToAction("Index", "Account");
        }

        public ActionResult Index() => View();

        public ActionResult Register() => View();

        public async Task<ActionResult> AttemptRegister(RegisterModel model)
        {
            var result = await new UserManager(Wrapper).CreateUser(model.Username, model.Password);

            return result.HasError ? ErrorView(result.ErrorString) : RedirectToAction("Index", "Home");
        }
    }
}