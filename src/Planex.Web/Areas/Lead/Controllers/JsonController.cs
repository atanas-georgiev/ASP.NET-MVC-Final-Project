﻿namespace Planex.Web.Areas.Lead.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Services.Projects;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Lead.Models.Estimation;
    using Planex.Web.Areas.Lead.Models.Gantt;
    using Planex.Web.Infrastructure.Mappings;

    public class JsonController : BaseController
    {
        private readonly IProjectService projectService;

        private readonly ITaskService subTaskService;

        public JsonController(IUserService userService, ITaskService subTaskService, IProjectService projectService)
            : base(userService)
        {
            this.subTaskService = subTaskService;
            this.projectService = projectService;
        }

        public ActionResult CreateAssignments(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsAssignmentsViewModel assignment)
        {
            if (assignment != null)
            {
                var subtask = this.subTaskService.GetById(assignment.TaskId);
                var user = this.UserService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Add(user);
                this.subTaskService.Update(subtask);
            }

            return this.Json(assignment);
        }

        public virtual ActionResult CreateDependency(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                this.subTaskService.AddDependency(
                    new SubTaskDependency()
                        {
                            SuccessorId = dep.SuccessorId, 
                            PredecessorId = dep.PredecessorId, 
                            Type = dep.Type
                        });
            }

            return this.Json(dep);
        }

        public virtual JsonResult CreateTask(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsViewModel task)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());

            if (this.ModelState.IsValid)
            {
                var taskdb = new SubTask()
                                 {
                                     ProjectId = projectId, 
                                     Title = task.Title, 
                                     ParentId = task.ParentTaskId, 
                                     Start = task.Start, 
                                     End = task.End, 
                                     PercentComplete = 0, 
                                     Price = 0, 
                                     IsUserNotified = false
                                 };

                this.subTaskService.Add(taskdb);

                task.TaskId = taskdb.Id;
                task.ParentTaskId = taskdb.ParentId;
            }

            return this.Json(new[] { task }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult DestroyAssignments(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsAssignmentsViewModel assignment)
        {
            if (assignment != null)
            {
                var subtask = this.subTaskService.GetById(assignment.TaskId);
                var user = this.UserService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Remove(user);
                this.subTaskService.Update(subtask);
            }

            return this.Json(assignment);
        }

        public virtual ActionResult DestroyDependency(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                this.subTaskService.DeleteDependency(dep.Id);
            }

            return this.Json(dep);
        }

        public virtual JsonResult DestroyTask(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsViewModel task)
        {
            this.subTaskService.Delete(task.TaskId);
            return this.Json(new[] { task }.ToDataSourceResult(request, this.ModelState));
        }

        // Gantt assignments
        public virtual JsonResult ReadAssignments([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());
            var result = new List<ProjectDetailsAssignmentsViewModel>();
            var subtasks = this.subTaskService.GetAll().Where(x => x.ProjectId == projectId);

            foreach (var subtask in subtasks)
            {
                result.AddRange(
                    subtask.Users.Select(
                        user =>
                        new ProjectDetailsAssignmentsViewModel()
                            {
                                AssignmentId = user.IntId, 
                                ResourceId = user.IntId, 
                                TaskId = subtask.Id, 
                                Units = 1
                            }));
            }

            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt dependences
        public virtual JsonResult ReadDependencies([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.subTaskService.AllDependencies().To<ProjectDetailsDependencyViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadEstimations([DataSourceRequest] DataSourceRequest request)
        {
            var result =
                this.projectService.GetAll()
                    .Where(x => x.LeadId == this.UserProfile.Id && x.State == TaskStateType.UnderEstimation)
                    .To<EstimationListViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ReadProjects([DataSourceRequest] DataSourceRequest request)
        {
            var result =
                this.projectService.GetAll()
                    .Where(x => x.LeadId == this.UserProfile.Id && x.State == TaskStateType.Started)
                    .To<EstimationListViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt resources
        public virtual JsonResult ReadResources([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.UserService.GetAllByRole("Worker").To<ProjectDetailsResourseViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt tasks
        public virtual JsonResult ReadTasks([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());
            var result = this.subTaskService.GetAll().Where(x => x.ProjectId == projectId).To<ProjectDetailsViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAssignments(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsAssignmentsViewModel assignment)
        {
            if (assignment != null)
            {
                var subtask = this.subTaskService.GetById(assignment.TaskId);
                var user = this.UserService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Add(user);
                this.subTaskService.Update(subtask);
            }

            return this.Json(assignment);
        }

        public virtual JsonResult UpdateDependency(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                var depDb = this.subTaskService.AllDependencies().FirstOrDefault(x => x.Id == dep.Id);
                if (depDb != null)
                {
                    depDb.PredecessorId = dep.PredecessorId;
                    depDb.SuccessorId = dep.SuccessorId;
                    depDb.Type = dep.Type;
                    this.subTaskService.UpdateDependency(depDb);
                }
            }

            return this.Json(dep);
        }

        public virtual JsonResult UpdateTask(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsViewModel task)
        {
            if (this.ModelState.IsValid)
            {
                var taskDb = this.subTaskService.GetById(task.TaskId);

                taskDb.Title = task.Title;
                taskDb.ParentId = task.ParentTaskId;
                taskDb.Start = task.Start;
                taskDb.End = task.End;
                taskDb.PercentComplete = task.PercentComplete;

                this.subTaskService.Update(taskDb);

                task.TaskId = taskDb.Id;
                task.ParentTaskId = taskDb.ParentId;
            }

            return this.Json(new[] { task }.ToDataSourceResult(request, this.ModelState));
        }

        public virtual JsonResult UpdateTaskOnlyProgress(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsViewModel task)
        {
            if (this.ModelState.IsValid)
            {
                var taskDb = this.subTaskService.GetById(task.TaskId);

                taskDb.PercentComplete = task.PercentComplete;

                this.subTaskService.Update(taskDb);

                task.TaskId = taskDb.Id;
                task.ParentTaskId = taskDb.ParentId;
            }

            return this.Json(new[] { task }.ToDataSourceResult(request, this.ModelState));
        }
    }
}