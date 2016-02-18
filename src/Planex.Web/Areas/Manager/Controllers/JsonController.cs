namespace Planex.Web.Areas.Manager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Planex.Common;
    using Planex.Data.Models;
    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Lead.Models.Gantt;
    using Planex.Web.Areas.Manager.Models.Projects;
    using Planex.Web.Infrastructure.Mappings;

    using ProjectDetailsViewModel = Planex.Web.Areas.Manager.Models.Gantt.ProjectDetailsViewModel;

    public class JsonController : BaseController
    {
        private readonly IProjectService projectService;

        private readonly ISkillService skillService;

        private readonly ITaskService subTaskService;

        public JsonController(
            IUserService userService, 
            ISkillService skillService, 
            ITaskService subTaskService, 
            IProjectService projectService)
            : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
        }

        public ActionResult CreateAssignments(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsAssignmentsViewModel assignment)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = this.subTaskService.GetById(assignment.TaskId);
                var user = this.userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Add(user);
                this.subTaskService.Update(subtask);
                this.UpdatePriceSubTask(assignment.TaskId);
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
                this.subTaskService.Add(
                    new SubTask()
                        {
                            ProjectId = projectId, 
                            Title = task.Title, 
                            ParentId = task.ParentTaskId, 
                            Start = task.Start, 
                            End = task.End, 
                            PercentComplete = 0, 
                            Price = 0
                        });
            }

            return this.Json(new[] { task }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult DestroyAssignments(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsAssignmentsViewModel assignment)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = this.subTaskService.GetById(assignment.TaskId);
                var user = this.userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Remove(user);
                this.subTaskService.Update(subtask);
                this.UpdatePriceSubTask(assignment.TaskId);
            }

            return this.Json(assignment);
        }

        public virtual ActionResult DestroyDependency(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                this.subTaskService.DeleteDependency(dep.DependencyId);
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

        public ActionResult GetAllLeadUsers([DataSourceRequest] DataSourceRequest request)
        {
            var leads =
                this.userService.GetAllByRole("Lead")
                    .Select(x => new { id = x.Id, name = x.FirstName + " " + x.LastName });
            return this.Json(leads, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAllProjects([DataSourceRequest] DataSourceRequest request)
        {
            var tasks = this.projectService.GetAll().To<ProjectListViewModel>();
            return this.Json(tasks.ToDataSourceResult(request));
        }

        // Gantt assignments
        public virtual JsonResult ReadAssignments([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());
            var result = new List<ProjectDetailsAssignmentsViewModel>();
            var subtasks = this.subTaskService.GetAll().Where(x => x.ProjectId == projectId);
            var idCount = 1000;

            foreach (var subtask in subtasks)
            {
                result.AddRange(
                    subtask.Users.Select(
                        user =>
                        new ProjectDetailsAssignmentsViewModel()
                            {
                                AssignmentId = idCount++, 
                                ResourceId = (int)user.IntId, 
                                TaskId = subtask.Id, 
                                Units = 1
                            }));
            }

            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt dependences
        public virtual JsonResult ReadDependencies([DataSourceRequest] DataSourceRequest request)
        {
            var projectId = int.Parse(this.Session["ProjectId"].ToString());
            var result = this.subTaskService.AllDependencies().To<ProjectDetailsDependencyViewModel>();
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Gantt resources
        public virtual JsonResult ReadResources([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.userService.GetAllByRole("Worker").To<ProjectDetailsResourseViewModel>();
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
            var projectId = int.Parse(this.Session["ProjectId"].ToString());

            if (assignment != null)
            {
                var subtask = this.subTaskService.GetById(assignment.TaskId);
                var user = this.userService.GetAll().FirstOrDefault(x => x.IntId == assignment.ResourceId);
                subtask.Users.Add(user);
                this.subTaskService.Update(subtask);
                this.UpdatePriceSubTask(assignment.TaskId);
            }

            return this.Json(assignment);
        }

        public virtual JsonResult UpdateDependency(
            [DataSourceRequest] DataSourceRequest request, 
            ProjectDetailsDependencyViewModel dep)
        {
            if (dep != null)
            {
                var depDb = this.subTaskService.AllDependencies()
                    .FirstOrDefault(x => x.DependencyId == dep.DependencyId);
                depDb.PredecessorId = dep.PredecessorId;
                depDb.SuccessorId = dep.SuccessorId;
                depDb.Type = dep.Type;
                this.subTaskService.UpdateDependency(depDb);
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
                taskDb.Price = 0;

                var projectId = int.Parse(this.Session["ProjectId"].ToString());
                var project = this.projectService.GetById(projectId);
                var subTasks = this.subTaskService.GetAll().Where(x => x.ProjectId == projectId && x.ParentId == null);
                project.PercentComplete = subTasks.Sum(x => x.PercentComplete) / subTasks.Count();
                this.projectService.Update(project);

                this.UpdatePriceSubTask(taskDb.Id);
            }

            return this.Json(new[] { task }.ToDataSourceResult(request, this.ModelState));
        }

        private void UpdatePriceSubTask(int taskId)
        {
            var taskDb = this.subTaskService.GetById(taskId);
            taskDb.Price = 0;
            var durationInDays = (taskDb.End - taskDb.Start).Days;

            foreach (var user in taskDb.Users)
            {
                taskDb.Price += user.Salary / UserConstants.WorkingDays * durationInDays;
            }

            this.subTaskService.Update(taskDb);
        }
    }
}