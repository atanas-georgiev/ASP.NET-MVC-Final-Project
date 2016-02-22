namespace Planex.Web.Areas.Lead.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Services.Messages;
    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Lead.Models.Estimation;
    using Planex.Web.Infrastructure.Mappings;

    using Vereyon.Web;

    public class EstimationsController : BaseController
    {
        private readonly IMessageService messageService;

        private readonly IProjectService projectService;

        public EstimationsController(
            IUserService userService, 
            IProjectService projectService, 
            IMessageService messageService)
            : base(userService)
        {
            this.projectService = projectService;
            this.messageService = messageService;
        }

        public ActionResult Edit(string id)
        {
            this.Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var requestedEstimationTask = this.projectService.GetAll().Where(x => x.Id == intId).To<EstimationEditViewModel>().FirstOrDefault();
                     
            if (requestedEstimationTask != null)
            {
                if (requestedEstimationTask.LeadId != UserProfile.Id)
                {
                    return HttpNotFound();
                }

                var sanitizer = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
                requestedEstimationTask.Description = sanitizer.Sanitize(requestedEstimationTask.Description);                
            }

            return this.View(requestedEstimationTask);
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult SendForApproval()
        {
            var project = this.projectService.GetById(int.Parse(this.Session["ProjectId"].ToString()));

            if (project.LeadId != UserProfile.Id)
            {
                return HttpNotFound();
            }

            project.State = TaskStateType.Estimated;
            this.projectService.Update(project);
            this.messageService.SendSystemMessage(
                project.LeadId, 
                project.ManagerId, 
                SystemMessageType.ProjectEstimated, 
                project.Id, 
                null);
            return this.RedirectToAction("Index");
        }
    }
}