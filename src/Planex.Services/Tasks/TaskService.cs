using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Planex.Common;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private DbContext context;
        private IRepository<SubTask> tasks;

        public TaskService(DbContext context, IRepository<SubTask> tasks)
        {
            this.context = context;
            this.tasks = tasks;
        }

        public void Add(SubTask task)
        {
            this.tasks.Add(task);
        }

        public void Update(SubTask task)
        {
            this.tasks.Update(task);
        }

        public void AddAttachments(SubTask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server)
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

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId + "\\" + dbTask.Id)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId + "\\" + dbTask.Id));
            }

            foreach (var file in uploadedAttachments)
            {
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.ProjectId + "\\" + dbTask.Id + "\\" + filename));
//                dbTask.Attachments.Add(new Attachment()
//                {
//                    Name = file.FileName
//                });

                Update(dbTask);
            }
        }

        public IQueryable<SubTask> GetAll()
        {
            return this.tasks.All();
        }

        public SubTask GetById(int id)
        {
            return this.tasks.GetById(id);
        }
    }
}
