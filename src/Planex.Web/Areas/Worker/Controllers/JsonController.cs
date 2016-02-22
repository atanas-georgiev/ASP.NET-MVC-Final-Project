namespace Planex.Web.Areas.Worker.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Services.Messages;
    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Worker.Models.Assignments;
    using Planex.Web.Infrastructure.Mappings;

    public class JsonController : BaseController
    {
        private readonly IMessageService messageService;

        private readonly IProjectService projectService;

        private readonly ISkillService skillService;

        private readonly ITaskService subTaskService;

        public JsonController(
            IUserService userService, 
            ISkillService skillService, 
            ITaskService subTaskService, 
            IProjectService projectService, 
            IMessageService messageService)
            : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
            this.messageService = messageService;
        }

        public virtual ActionResult EditAssignment(
            [DataSourceRequest] DataSourceRequest request, 
            AssignmentViewModel task)
        {
            if (this.ModelState.IsValid)
            {
                var taskDb = this.subTaskService.GetById(task.Id);
                taskDb.PercentComplete = task.PercentComplete / 100;
                this.subTaskService.UpdateProgress(taskDb);

                if (task.PercentComplete == 100)
                {
                    this.messageService.SendSystemMessage(
                        this.UserProfile.Id, 
                        taskDb.Project.LeadId, 
                        SystemMessageType.TaskComplete, 
                        taskDb.Project.Id, 
                        taskDb.Id);
                }
            }

            return this.Json(request);
        }

        public virtual ActionResult GetAllAssignments([DataSourceRequest] DataSourceRequest request)
        {
            var tasks =
                this.subTaskService.GetAll()
                    .Where(x => x.Users.Any(u => u.Id == this.UserProfile.Id) && x.PercentComplete != 1)
                    .To<AssignmentViewModel>();
            return this.Json(tasks.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}