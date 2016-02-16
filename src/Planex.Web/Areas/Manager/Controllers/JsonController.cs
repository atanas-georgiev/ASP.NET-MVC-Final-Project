using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Services.Skills;
using Planex.Services.SubTasks;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Manager.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Controllers
{
    public class JsonController : BaseController
    {
        // GET: Manager/Json
        private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ISubTaskService subTaskService;
        private readonly ITaskService taskService;

        public JsonController(IUserService userService,
                                ISkillService skillService,
                                ISubTaskService subTaskService,
                                ITaskService taskService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.userService = userService;
            this.taskService = taskService;
        }

        public ActionResult Projects_Read([DataSourceRequest]DataSourceRequest request)
        {
            var tasks = taskService.GetAll().To<ProjectViewModel>();
            return Json(tasks.ToDataSourceResult(request));
        }
    }
}