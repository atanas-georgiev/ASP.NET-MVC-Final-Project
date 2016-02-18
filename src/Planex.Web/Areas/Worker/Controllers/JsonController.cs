namespace Planex.Web.Areas.Worker.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Worker.Models.Assignments;
    using Planex.Web.Infrastructure.Mappings;

    public class JsonController : BaseController
    {
        private readonly IProjectService projectService;

        private readonly ISkillService skillService;

        private readonly ITaskService subTaskService;

        public JsonController(
            IUserService userService, 
            ISkillService skillService, 
            ITaskService subTaskService, 
            IProjectService projectService)
            : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
        }

        public virtual ActionResult EditAssignment(
            [DataSourceRequest] DataSourceRequest request, 
            AssignmentViewModel task)
        {
            if (this.ModelState.IsValid)
            {
                var taskDb = this.subTaskService.GetById(task.Id);
                taskDb.PercentComplete = task.PercentComplete / 100;
                this.subTaskService.Update(taskDb);
            }

            return this.Json(request);
        }

        public virtual ActionResult GetAllAssignments([DataSourceRequest] DataSourceRequest request)
        {
            var tasks =
                this.subTaskService.GetAll()
                    .Where(x => x.Users.Any(u => u.Id == this.UserProfile.Id))
                    .To<AssignmentViewModel>();
            return this.Json(tasks.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}