namespace Planex.Services.Projects
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;

    using Planex.Common;
    using Planex.Data;
    using Planex.Data.Common;
    using Planex.Data.Models;

    public class ProjectService : IProjectService
    {
        private DbContext context;

        private IRepository<Project, int> projects;
        private IRepository<SubTask, int> subtasks;
        private IRepository<Message, int> messages;

        public ProjectService(DbContext context, IRepository<Project, int> projects, IRepository<SubTask, int> subtasks, IRepository<Message, int> messages)
        {
            this.context = context;
            this.projects = projects;
            this.subtasks = subtasks;
            this.messages = messages;
        }

        public void Add(Project task)
        {
            this.projects.Add(task);
        }

        public void AddAttachments(
            Project dbtask, 
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

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.Id)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.Id));
            }

            foreach (var file in uploadedAttachments)
            {
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbtask.Id + "\\" + filename));
                dbtask.Attachments.Add(new Attachment() { Name = file.FileName });

                this.Update(dbtask);
            }
        }

        public IQueryable<Project> GetAll()
        {
            return this.projects.All();
        }

        public Project GetById(int id)
        {
            return this.projects.GetById(id);
        }

        public void Remove(int id)
        {
            var projectDb = this.GetAll().FirstOrDefault(x => x.Id == id);
            if (projectDb != null)
            {
                var tasks = projectDb.Subtasks;
                foreach (var task in tasks)
                {
                    this.subtasks.Delete(task);
                }

                var messagesDb = this.messages.All().Where(x => x.ProjectId == projectDb.Id);
                foreach (var message in messagesDb)
                {
                    this.messages.Delete(message);
                }
            }

            this.projects.Delete(id);
        }

        public void StartEstimation(int taskId, string userId)
        {
            var task = this.projects.GetById(taskId);
            task.State = TaskStateType.UnderEstimation;
            task.LeadId = userId;
            this.Update(task);
        }

        public void Update(Project task)
        {
            this.projects.Update(task);
        }
    }
}