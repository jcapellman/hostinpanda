using hostinpanda.library.DAL;
using hostinpanda.web.Managers;
using hostinpanda.web.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hostinpanda.web.Controllers
{
    [Authorize]
    public class HostsController : BaseController
    {
        public HostsController(DALdbContext dbContext) : base(dbContext)
        {
        }

        public ActionResult Edit(int id)
        {
            var hostResponse = new HostManager(Wrapper).GetHost(id);

            if (hostResponse.HasError)
            {
                return ErrorView(hostResponse.ErrorString);
            }

            var model = new EditHostModel
            {
                ID = hostResponse.ObjectValue.ID,
                HostName = hostResponse.ObjectValue.HostName,
                PortNumber = hostResponse.ObjectValue.PortNumber,
                AllowableDowntimeMinutes = hostResponse.ObjectValue.AllowableDowntimeMinutes,
                PortType = hostResponse.ObjectValue.PortType
            };

            return View("Edit", model);
        }

        public ActionResult UpdateHost(EditHostModel model)
        {
            var response = new HostManager(Wrapper).UpdateHost(model.ID, model.HostName, model.PortNumber, model.AllowableDowntimeMinutes, model.PortType);

            return response.HasError ? ErrorView(response.ErrorString) : Index();
        }

        public ActionResult CreateHost(NewHostModel model)
        {
            var response = new HostManager(Wrapper).AddHost(model);

            return response.HasError ? ErrorView(response.ErrorString) : Index();
        }

        public ActionResult Delete(int id)
        {
            var deleteResponse = new HostManager(Wrapper).RemoveHost(id);

            return deleteResponse.HasError ? ErrorView(deleteResponse.ErrorString) : Index();
        }

        public ActionResult AddNew() => View(new NewHostModel());

        public ActionResult Index()
        {
            var hostResponse = new HostManager(Wrapper).GetHostListing();

            if (hostResponse.HasError)
            {
                return ErrorView(hostResponse.ErrorString);
            }

            return hostResponse.HasError ? ErrorView(hostResponse.ErrorString) : View("Index", hostResponse.ObjectValue);
        }
    }
}