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
        }

        public void AddAttachments(
            SubTask dbTask, 
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

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId));
            }

            if (
                !Directory.Exists(
                    server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId + "\\" + dbTask.Id)))
            {
                Directory.CreateDirectory(
                    server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId + "\\" + dbTask.Id));
            }

            foreach (var file in uploadedAttachments)
            {
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(
                    server.MapPath(
                        TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId + "\\" + dbTask.Id + "\\" + filename));

                // dbTask.Attachments.Add(new Attachment()
                // {
                // Name = file.FileName
                // });
                this.Update(dbTask);
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
            this.tasks.Delete(id);
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
            this.tasks.Update(task);
            this.UpdateProjectCompleteness(task.Project);
        }

        public void UpdateDependency(SubTaskDependency task)
        {
            this.dependencies.Update(task);
        }

        private void UpdateProjectCompleteness(Project project)
        {
            var subTasks = project.Subtasks; // .Where(x => x.ParentId == null);

            if (subTasks.Count() != 0)
            {
                project.PercentComplete = subTasks.Sum(x => x.PercentComplete) / subTasks.Count();
                this.projects.Update(project);
            }
        }
    }
}