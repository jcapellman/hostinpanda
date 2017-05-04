using System.Threading.Tasks;

using hostinpanda.clientlibrary.Common;
using hostinpanda.serverlibrary.Managers;
using hostinpanda.web.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hostinpanda.web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IOptions<GlobalSettings> argGlobalSettings) : base(argGlobalSettings.Value)
        {
        }

        public async Task<ActionResult> AttemptLogin(LoginModel model)
        {
            var result = await new UserManager(Wrapper).Login(model.Username, model.Password);

            return result.HasError ? ErrorView(result.ErrorString) : RedirectToAction("Index", "Account");
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