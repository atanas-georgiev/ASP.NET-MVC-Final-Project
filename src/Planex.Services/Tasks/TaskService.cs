namespace Planex.Services.Tasks
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;

    using Planex.Common;
    using Planex.Data.Common;
    using Planex.Data.Models;

    public class TaskService : ITaskService
    {
        private DbContext context;

        private IRepository<SubTaskDependency, int> dependencies;

        private IRepository<Project, int> projects;

        private IRepository<SubTask, int> tasks;

        public TaskService(
            DbContext context, 
            IRepository<SubTask, int> tasks, 
            IRepository<SubTaskDependency, int> dependencies, 
            IRepository<Project, int> projects)
        {
            this.context = context;
            this.tasks = tasks;
            this.dependencies = dependencies;
            this.projects = projects;
        }

        public void Add(SubTask task)
        {
            this.tasks.Add(task);
            this.UpdateProjectDetails(task.Project);
        }

        public void AddAttachments(
            SubTask dbtask, 
            List<HttpPostedFileBase> uploadedAttachments, 
            HttpServerUtility server)
        {
            if (uploadedAttachments == null)
            {
                return;
            }

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder));
            }

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.ProjectId)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.ProjectId));
            }

            if (
                !Directory.Exists(
                    server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.ProjectId + "\\" + dbtask.Id)))
            {
                Directory.CreateDirectory(
                    server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.ProjectId + "\\" + dbtask.Id));
            }

            foreach (var file in uploadedAttachments)
            {
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(
                    server.MapPath(
                        TasksConstants.MainContentFolder + "\\" + dbtask.ProjectId + "\\" + dbtask.Id + "\\" + filename));
                this.Update(dbtask);
            }
        }

        public void AddDependency(SubTaskDependency dep)
        {
            this.dependencies.Add(dep);
        }

        public IQueryable<SubTaskDependency> AllDependencies()
        {
            return this.dependencies.All();
        }

        public void Delete(int id)
        {
            var taskProject = this.GetById(id).Project;            
            this.tasks.Delete(id);
            this.UpdateProjectDetails(taskProject);
        }

        public void DeleteDependency(int id)
        {
            this.dependencies.Delete(id);
        }

        public IQueryable<SubTask> GetAll()
        {
            return this.tasks.All();
        }

        public SubTask GetById(int id)
        {
            return this.tasks.GetById(id);
        }

        public void Update(SubTask task)
        {
            var durationInDays = (task.End - task.Start).Days;
            task.Price = task.Users.Sum(user => (user.Salary / 20) * durationInDays);
            this.tasks.Update(task);
            
            this.UpdateProjectDetails(task.Project);
        }

        public void UpdateDependency(SubTaskDependency task)
        {
            this.dependencies.Update(task);
        }

        public void UpdateProgress(SubTask task)
        {
            this.Update(task);
            
            while (true)
            {
                if (task.ParentId != null)
                {
                    task = GetById(task.ParentId.Value);
                    task.PercentComplete = task.Subtasks.Sum(x => x.PercentComplete) / task.Subtasks.Count;
                    this.Update(task);
                }
                else
                {
                    break;
                }                
            }
        }

        private void UpdateProjectDetails(Project project)
        {
            if (project != null)
            {
                var subTasks = project.Subtasks;

                if (subTasks.Count() != 0)
                {
                    project.PercentComplete = subTasks.Sum(x => x.PercentComplete) / subTasks.Count();
                    project.Price = subTasks.Sum(x => x.Price);
                    project.Start = subTasks.OrderBy(x => x.Start).First().Start;
                    project.End = subTasks.OrderByDescending(x => x.End).First().End;

                    this.projects.Update(project);
                }
            }
        }
    }
}