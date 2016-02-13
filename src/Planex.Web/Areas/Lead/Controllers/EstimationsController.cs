using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class EstimationsController : BaseController
    {
        private ITaskService taskService;

        public EstimationsController(IUserService userService, ITaskService taskService)
            : base(userService)
        {
            this.taskService = taskService;
        }


        public ActionResult Projects_Read([DataSourceRequest] DataSourceRequest request)
        {
            var requestedEstimationTasks = taskService.GetAll().To<EstimationRequestedViewModel>();
            return Json(requestedEstimationTasks.ToDataSourceResult(request));
        }

        public ActionResult Requested()
        {
            var requestedEstimationTasks = taskService.GetAll().To<EstimationRequestedViewModel>();
            return View(requestedEstimationTasks);
        }
    }
}