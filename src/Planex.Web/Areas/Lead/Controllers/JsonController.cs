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
using Planex.Web.Areas.Lead.Models.Estimation;
using Planex.Web.Areas.Lead.Models.Gantt;
using Planex.Web.Infrastructure.Extensions;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class JsonController : BaseController
    {
        private readonly ISkillService skillService;
        private readonly ITaskService subTaskService;
        private readonly IProjectService projectService;

        public JsonController(IUserService userService,
                                ISkillService skillService,
                                ITaskService subTaskService,
                                IProjectService projectService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
        }

        public virtual JsonResult ReadEstimations([DataSourceRequest]DataSourceRequest request)
        {
            var result = projectService.GetAll().Where(x => /*x.LeadId == UserProfile.Id &&*/ x.State == TaskStateType.UnderEstimation).To<EstimationListViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt tasks
        public virtual JsonResult ReadTasks([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.ProjectId == projectId).To<ProjectDetailsViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

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
                    End = task.End,
                    PercentComplete = 0
                 });
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult DestroyTask([DataSourceRequest] DataSourceRequest request, ProjectDetailsViewModel task)
        {
            subTaskService.Delete(task.TaskId);
            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult UpdateTask([DataSourceRequest] DataSourceRequest request, ProjectDetailsViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskDb = subTaskService.GetById(task.TaskId);

                taskDb.Title = task.Title;
                taskDb.ParentId = task.ParentTaskId;
                taskDb.Start = task.Start;
                taskDb.End = task.End;
                taskDb.PercentComplete = task.PercentComplete;
                subTaskService.Update(taskDb);

                var projectId = int.Parse(Session["ProjectId"].ToString());
                var project = projectService.GetById(projectId);
                var subTasks = subTaskService.GetAll().Where(x => x.ProjectId == projectId && x.ParentId == null);
                project.PercentComplete = subTasks.Sum(x => x.PercentComplete)/subTasks.Count();
                projectService.Update(project);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        // Gantt dependences
        public virtual JsonResult ReadDependencies([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());
            var result = subTaskService.AllDependencies().To<ProjectDetailsDependencyViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult UpdateDependency([DataSourceRequest] DataSourceRequest request, ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                var depDb = subTaskService.AllDependencies().FirstOrDefault(x => x.DependencyId == dep.DependencyId);
                depDb.PredecessorId = dep.PredecessorId;
                depDb.SuccessorId = dep.SuccessorId;
                depDb.Type = dep.Type;
                this.subTaskService.UpdateDependency(depDb);
            }
            return Json(dep);
        }

        public virtual ActionResult DestroyDependency([DataSourceRequest] DataSourceRequest request, ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                subTaskService.DeleteDependency(dep.DependencyId);
            }
            return Json(dep);
        }

        public virtual ActionResult CreateDependency([DataSourceRequest] DataSourceRequest request, ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                subTaskService.AddDependency(new SubTaskDependency()
                {
                    SuccessorId = dep.SuccessorId,
                    PredecessorId = dep.PredecessorId,
                    Type = dep.Type
                });
            }
            return Json(dep);
        }

        // Gantt resources
        public virtual JsonResult ReadResources([DataSourceRequest] DataSourceRequest request)
        {
            var result = userService.GetAllByRole("Worker").To<ProjectDetailsResourseViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt assignments
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
                    AssignmentId = idCount++, ResourceId = (int)user.IntId, TaskId = subtask.Id, Units = 1
                }));
            }

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAssignments([DataSourceRequest] DataSourceRequest request, ProjectDetailsAssignmentsViewModel assignment)
        {
            var projectId = int.Parse(Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = subTaskService.GetById(assignment.TaskId);
                var user = userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
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
                var subtask = subTaskService.GetById(assignment.TaskId);
                var user = userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
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
                var subtask = subTaskService.GetById(assignment.TaskId);
                var user = userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Add(user);
                subTaskService.Update(subtask);
            }
            return this.Json(assignment);
        }
    }
}