using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Planex.Common;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.SubTasks
{
    public class SubTaskService : ISubTaskService
    {
        private DbContext context;
        private IRepository<Subtask> tasks;

        public SubTaskService(DbContext context, IRepository<Subtask> tasks)
        {
            this.context = context;
            this.tasks = tasks;
        }

        public void Add(Subtask task)
        {
            //todo:
            task.End = DateTime.UtcNow;
            this.tasks.Add(task);
        }

        public void Update(Subtask task)
        {
            this.tasks.Update(task);
        }

        public void AddAttachments(Subtask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server)
        {
            if (uploadedAttachments == null)
            {
                return;
            }

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder));
            }

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.MainTaskId)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.MainTaskId));
            }

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.MainTaskId + "\\" + dbTask.Id)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.MainTaskId + "\\" + dbTask.Id));
            }

            foreach (var file in uploadedAttachments)
            {
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.MainTaskId + "\\" + dbTask.Id + "\\" + filename));
                dbTask.Attachments.Add(new Attachment()
                {
                    Name = file.FileName
                });

                Update(dbTask);
            }
        }

        public IQueryable<Subtask> GetAll()
        {
            return this.tasks.All();
        }

        public Subtask GetById(int id)
        {
            return this.tasks.GetById(id);
        }
    }
}
