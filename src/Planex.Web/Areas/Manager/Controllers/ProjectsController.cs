namespace Planex.Web.Areas.Manager.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Services.Projects;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Manager.Models.Projects;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectsController : BaseController
    {
        private readonly IProjectService projectsService;

        private readonly ITaskService subTaskService;

        public ProjectsController(
            IUserService userService, 
            IProjectService projectsService, 
            ITaskService subTaskService)
            : base(userService)
        {
            this.projectsService = projectsService;
            this.subTaskService = subTaskService;
        }

        public ActionResult Approve()
        {
            var taskId = int.Parse(this.Session["ProjectId"].ToString());
            var task = this.projectsService.GetById(taskId);
            task.State = TaskStateType.Started;
            this.projectsService.Update(task);
            return this.RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectCreateViewModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
                var dbTask = new Project()
                                 {
                                     Title = model.Title, 
                                     Description = model.Description, 
                                     ManagerId = this.UserProfile.Id, 
                                     Priority = model.Priority, 
                                     State = TaskStateType.Draft, 
                                     Start = model.Start, 
                                     PercentComplete = 0, 
                                     LeadId = model.LeadId, 
                                     Price = 0
                                 };

                this.projectsService.Add(dbTask);
                this.projectsService.AddAttachments(
                    dbTask, 
                    model.UploadedAttachments, 
                    System.Web.HttpContext.Current.Server);

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        public ActionResult Details(string id)
        {
            this.Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var result =
                this.projectsService.GetAll().Where(x => x.Id == intId).To<ProjectDetailsViewModel>().FirstOrDefault();

            var subTasks = this.subTaskService.GetAll().Where(x => x.ProjectId == intId);

            if (subTasks.Count() != 0)
            {
                result.Start = subTasks.OrderBy(x => x.Start).First().Start;
                result.End = subTasks.OrderByDescending(x => x.End).First().End;
                result.FinalPrice = subTasks.Sum(x => x.Price);
            }

            return this.View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ProjectDetailsViewModel model)
        {
            var project = this.projectsService.GetById(model.Id);

            if (this.ModelState.IsValid)
            {
                project.Title = model.Title;
                project.Description = model.Description;
                project.Priority = model.Priority;
                project.Start = model.Start;
                project.LeadId = model.LeadId;

                this.projectsService.Update(project);
                this.projectsService.AddAttachments(
                    project, 
                    model.UploadedAttachments, 
                    System.Web.HttpContext.Current.Server);

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Remove(string id)
        {
            this.Session["ProjectId"] = id;
            var intId = int.Parse(id);
            this.projectsService.Remove(intId);
            return this.RedirectToAction("Index");
        }

        public ActionResult SendForEstimation(string id)
        {
            this.Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var project = this.projectsService.GetById(intId);
            project.State = TaskStateType.UnderEstimation;
            this.projectsService.Update(project);
            return this.RedirectToAction("Index");
        }
    }
}