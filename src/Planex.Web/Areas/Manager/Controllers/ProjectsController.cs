using System.Linq;
using System.Web.Mvc;
using Planex.Data.Models;
using Planex.Services.Projects;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Manager.Models.Projects;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService projectsService;
        private readonly ITaskService subTaskService;

        public ProjectsController(IUserService userService, IProjectService projectsService, ITaskService subTaskService)
            : base(userService)
        {
            this.projectsService = projectsService;
            this.subTaskService = subTaskService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                var dbTask = new Project()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ManagerId = UserProfile.Id,
                    Priority = model.Priority,
                    State = TaskStateType.Draft,
                    Start = model.Start,
                    PercentComplete = 0,
                    LeadId = model.LeadId,
                    Price = 0
                };

                projectsService.Add(dbTask);
                projectsService.AddAttachments(dbTask, model.UploadedAttachments, System.Web.HttpContext.Current.Server);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Details(string id)
        {
            Session["ProjectId"] = id;            
            var intId = int.Parse(id);
            var result = projectsService.GetAll().Where(x => x.Id == intId).To<ProjectDetailsViewModel>().FirstOrDefault();

            var subTasks = subTaskService.GetAll().Where(x => x.ProjectId == intId);
            
            if (subTasks.Count() != 0)
            {
                result.Start = subTasks.OrderBy(x => x.Start).First().Start;
                result.End = subTasks.OrderByDescending(x => x.End).First().End;
                result.FinalPrice = subTasks.Sum(x => x.Price);
            }
            
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ProjectDetailsViewModel model)
        {
            var project = projectsService.GetById(model.Id);

            if (ModelState.IsValid)
            {
                project.Title = model.Title;
                project.Description = model.Description;
                project.Priority = model.Priority;
                project.Start = model.Start;
                project.LeadId = model.LeadId;
              
                projectsService.Update(project);
                projectsService.AddAttachments(project, model.UploadedAttachments, System.Web.HttpContext.Current.Server);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Remove(string id)
        {
            Session["ProjectId"] = id;
            var intId = int.Parse(id);
            projectsService.Remove(intId);
            return RedirectToAction("Index");
        }

        public ActionResult SendForEstimation(string id)
        {
            Session["ProjectId"] = id;
            var intId = int.Parse(id);
            var project = projectsService.GetById(intId);
            project.State = TaskStateType.UnderEstimation;
            projectsService.Update(project);
            return RedirectToAction("Index");
        }

        public ActionResult Approve()
        {
            var taskId = int.Parse(Session["ProjectId"].ToString());
            var task = projectsService.GetById(taskId);
            task.State = TaskStateType.Started;
            projectsService.Update(task);
            return RedirectToAction("Index");
        }
    }
}