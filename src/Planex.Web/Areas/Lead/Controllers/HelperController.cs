using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Services.Projects;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Extensions;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class HelperController : BaseController
    {

       // private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ITaskService subTaskService;
        private readonly IProjectService taskService;

        public HelperController(IUserService userService,
                                ISkillService skillService,
                                ITaskService subTaskService,
                                IProjectService taskService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
         //   this.userService = userService;
            this.taskService = taskService;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }

        //[ChildActionOnly]
        public JsonResult GetAllSkills()
        {
            var result = skillService.GetAll().Select(x => new { id = x.Id, skill = x.Name });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeUsersWithSkill(int? skillId)
        {
            if (skillId != null)
            {
                var users = userService.GetAll().Where(x => x.Skills.Any(s => s.Id == skillId));
                return Json(users.Select(u => new { id = u.Id, value = u.Email }), JsonRequestBehavior.AllowGet);
            }

            return Json("");
        }

        // [ChildActionOnly]
        public JsonResult GetAllSubTasks()
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(s => s.ProjectId == projectId && s.ParentId == null).Select(x => new { id = x.Id, title = x.Title });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubTasksTreeView(int? id)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var hasChildrenDb = subTaskService.GetAll().Any(x => x.ParentId == id);
            var result = subTaskService.GetAll().Where(x => x.ProjectId == projectId && x.ParentId == id).Select(s => 
                            new
                            {
                                id = s.Id,
                                title = s.Title,
                                start = s.Start,
                                end = s.End,
                                hasChildren = hasChildrenDb
                            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSubTasksDependency(int? id)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.ProjectId == projectId && x.ParentId == id).Select(s =>
                            new
                            {
                                id = s.Id,
                                title = s.Title,
                            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProjectsIndex_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = taskService.GetAll().Where(x => x.LeadId == UserProfileId && x.State >= TaskStateType.Started).To<ProjectIndexViewController>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadTasks([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.ProjectId == projectId).To<ProjectDetailsViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public virtual JsonResult CreateTask([DataSourceRequest] DataSourceRequest request, ProjectDetailsViewModel task)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());

            if (ModelState.IsValid)
            {
                 subTaskService.Add(new SubTask()
                 {
                    ProjectId = projectId,
                    Title = task.Title,
                    ParentId = task.ParentTaskId,
                    Start = task.Start,
                    End = task.End
                 });
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual JsonResult DestroyTask([DataSourceRequest] DataSourceRequest request, ProjectDetailsViewModel task)
        {
            if (ModelState.IsValid)
            {
// todo
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual JsonResult UpdateTask([DataSourceRequest] DataSourceRequest request, ProjectDetailsViewModel task)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());

            if (ModelState.IsValid)
            {
                var taskDb = subTaskService.GetById(task.TaskId);

                taskDb.Title = task.Title;
                taskDb.ParentId = task.ParentTaskId;
                taskDb.Start = task.Start;
                taskDb.End = task.End;
                subTaskService.Update(taskDb);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult ReadDependencies([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.ProjectId == projectId && x.DependencyId != null).To<ProjectDetailsDependencyViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadResources([DataSourceRequest] DataSourceRequest request)
        {
            var result = userService.GetAll().To<ProjectDetailsResourseViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadAssignments([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = new List<ProjectDetailsAssignmentsViewModel>();
            var subtasks = subTaskService.GetAll().Where(x => x.ProjectId == projectId);
            var idCount = 1000;

            foreach (var subtask in subtasks)
            {
                result.AddRange(subtask.Users.Select(user => new ProjectDetailsAssignmentsViewModel()
                {
                    AssignmentID = idCount++, ResourceID = (int)user.IntId, TaskID = subtask.Id, Units = 1
                }));
            }

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAssignments([DataSourceRequest] DataSourceRequest request, ProjectDetailsAssignmentsViewModel assignment)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = subTaskService.GetById(assignment.TaskID);
                var user = userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceID);
                subtask.Users.Add(user);
                subTaskService.Update(subtask);
            }
            return this.Json(assignment);
        }

        public ActionResult DestroyAssignments([DataSourceRequest] DataSourceRequest request, ProjectDetailsAssignmentsViewModel assignment)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = subTaskService.GetById(assignment.TaskID);
                var user = userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceID);
                subtask.Users.Remove(user);
                subTaskService.Update(subtask);
            }
            return this.Json(assignment);
        }

        public ActionResult CreateAssignments([DataSourceRequest] DataSourceRequest request, ProjectDetailsAssignmentsViewModel assignment)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = subTaskService.GetById(assignment.TaskID);
                var user = userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceID);
                subtask.Users.Add(user);
                subTaskService.Update(subtask);
            }
            return this.Json(assignment);
        }
    }
}