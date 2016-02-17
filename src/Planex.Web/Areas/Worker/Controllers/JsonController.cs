using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Services.Projects;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Worker.Models.Assignments;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Worker.Controllers
{
    public class JsonController : BaseController
    {
        private readonly ISkillService skillService;
        private readonly ITaskService subTaskService;
        private readonly IProjectService projectService;

        public JsonController(IUserService userService,
                                ISkillService skillService,
                                ITaskService subTaskService,
                                IProjectService projectService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
        }
        public virtual ActionResult GetAllAssignments([DataSourceRequest]DataSourceRequest request)
        {
            var tasks = subTaskService.GetAll().Where(x => x.Users.Any(u => u.Id == UserProfile.Id)).To<AssignmentViewModel>();
            return Json(tasks.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult EditAssignment([DataSourceRequest]DataSourceRequest request, AssignmentViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskDb = subTaskService.GetById(task.Id);
                taskDb.PercentComplete = task.PercentComplete/100;
                subTaskService.Update(taskDb);
            }

            return Json(request);
        }
    }
}