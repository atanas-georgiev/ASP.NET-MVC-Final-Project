namespace Planex.Services.Projects
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;

    using Planex.Common;
    using Planex.Data;
    using Planex.Data.Models;

    public class ProjectService : IProjectService
    {
        private DbContext context;

        private IRepository<Project> tasks;

        public ProjectService(DbContext context, IRepository<Project> tasks)
        {
            this.context = context;
            this.tasks = tasks;
        }

        public void Add(Project task)
        {
            this.tasks.Add(task);
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
            return this.tasks.All();
        }

        public Project GetById(int id)
        {
            return this.tasks.GetById(id);
        }

        public void Remove(int id)
        {
            this.tasks.Delete(id);
        }

        public void StartEstimation(int taskId, string userId)
        {
            var task = this.tasks.GetById(taskId);
            task.State = TaskStateType.UnderEstimation;
            task.LeadId = userId;
            this.Update(task);
        }

        public void Update(Project task)
        {
            this.tasks.Update(task);
        }
    }
}