using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Services.Skills;
using Planex.Services.SubTasks;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
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

        public virtual JsonResult ReadSubTasks([DataSourceRequest] DataSourceRequest request)
        {
            var taskId = int.Parse(Session["mainTaskId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.MainTaskId == taskId).To<SubTaskViewModel>();
            
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
            var taskId = int.Parse(Session["mainTaskId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.MainTaskId == taskId && x.DependencyId != null).To<DependencyViewModel>();
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
            var taskId = int.Parse(Session["mainTaskId"].ToString());
            var subtasks = subTaskService.GetAll().Where(x => x.MainTaskId == taskId);

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