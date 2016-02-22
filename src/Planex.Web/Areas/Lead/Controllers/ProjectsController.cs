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
        private readonly IProjectService projectService;

        public ProjectsController(
            IUserService userService, 
            IProjectService projectService)
            : base(userService)
        {
            this.projectService = projectService;
        }

        public ActionResult Edit(string id)
        {
            this.Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var requestedProjectTask = this.projectService.GetAll().Where(x => x.Id == intId).To<EstimationEditViewModel>().FirstOrDefault();
                                 
            if (requestedProjectTask != null)
            {
                if (requestedProjectTask.LeadId != UserProfile.Id)
                {
                    return this.HttpNotFound();
                }

                var sanitizer = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
                requestedProjectTask.Description = sanitizer.Sanitize(requestedProjectTask.Description);                
            }

            return this.View(requestedProjectTask);
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}