namespace Planex.Web.Areas.Lead.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Services.Messages;
    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Lead.Models.Estimation;
    using Planex.Web.Infrastructure.Mappings;

    using Vereyon.Web;

    public class ProjectsController : BaseController
    {
        private readonly IMessageService messageService;

        private readonly IProjectService projectService;

        private readonly ITaskService subTaskService;

        public ProjectsController(
            IUserService userService, 
            IProjectService projectService, 
            ISkillService skillService, 
            IMessageService messageService, 
            ITaskService subTaskService)
            : base(userService)
        {
            this.projectService = projectService;
            this.subTaskService = subTaskService;
            this.messageService = messageService;
        }

        public ActionResult Edit(string id)
        {
            this.Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var requestedEstimationTask =
                this.projectService.GetAll().Where(x => x.Id == intId).To<EstimationEditViewModel>().FirstOrDefault();
            
            var sanitizer = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
            
            if (requestedEstimationTask != null)
            {
                requestedEstimationTask.Description = sanitizer.Sanitize(requestedEstimationTask.Description);                
            }

            return this.View(requestedEstimationTask);
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}