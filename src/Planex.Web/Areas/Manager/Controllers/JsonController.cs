using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Services.Projects;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Areas.Lead.Models.Gantt;
using Planex.Web.Areas.Manager.Models;
using Planex.Web.Areas.Manager.Models.Projects;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Controllers
{
    public class JsonController : BaseController
    {
        // GET: Manager/Json
     //   private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ITaskService subTaskService;
        private readonly IProjectService taskService;

        public JsonController(IUserService userService,
                                ISkillService skillService,
                                ITaskService subTaskService,
                                IProjectService taskService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
          //  this.userService = userService;
            this.taskService = taskService;
        }

     
        public ActionResult GetAllLeadUsers([DataSourceRequest]DataSourceRequest request)
        {
            var leads = userService.GetAllByRole("Lead")
                .Select(x => new {id = x.Id, name = x.FirstName + " " + x.LastName } );
            return Json(leads, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAllProjects([DataSourceRequest]DataSourceRequest request)
        {
            var tasks = taskService.GetAll().To<ProjectListViewModel>();
            return Json(tasks.ToDataSourceResult(request));
        }

        public virtual JsonResult ReadSubTasks([DataSourceRequest] DataSourceRequest request)
        {
            var taskId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.ProjectId == taskId).To<SubTaskViewModel>();
            
//            foreach (var t in result)
//            {
//                t.resources = new List<SubTaskResourceViewModel>();
//                
//                foreach (var user in t.)
//                {
//                        
//                }

            return Json(result.ToDataSourceResult(request));
        }

        public virtual JsonResult ReadSubTaskDependencies([DataSourceRequest] DataSourceRequest request)
        {
            var taskId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.ProjectId == taskId && x.DependencyId != null).To<ProjectDetailsDependencyViewModel>();
            return Json(result.ToDataSourceResult(request));
        }

        public virtual JsonResult ReadSubTaskResources([DataSourceRequest] DataSourceRequest request)
        {
            var result = userService.GetAll().To<SubTaskResourceViewModel>().ToList();
            return Json(result.ToDataSourceResult(request));
        }

        public virtual JsonResult ReadSubTaskAssignments([DataSourceRequest] DataSourceRequest request)
        {
            var asssignments = new List<SubTaskAssignentsViewModel>();
            var taskId = int.Parse(Session["ProjectId"].ToString());
            var subtasks = subTaskService.GetAll().Where(x => x.ProjectId == taskId);

            foreach (var subtask in subtasks)
            {
                foreach (var user in subtask.Users)
                {
                    asssignments.Add(new SubTaskAssignentsViewModel()
                    {
                        taskId = subtask.Id,
                        resourceId = user.Id.GetHashCode(),
                        value = 1
                    });
                }
            }

            return Json(asssignments.ToDataSourceResult(request));
        }
    }
}