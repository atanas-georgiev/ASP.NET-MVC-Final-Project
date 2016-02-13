using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planex.Common;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private DbContext context;
        private IRepository<MainTask> tasks;

        public TaskService(DbContext context, IRepository<MainTask> tasks)
        {
            this.context = context;
            this.tasks = tasks;
        }

        public void Add(MainTask task)
        {
            this.tasks.Add(task);
        }

        public void Update(MainTask task)
        {
            this.tasks.Update(task);
        }

        public void AddAttachments(MainTask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server)
        {
            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder));
            }

            if (!Directory.Exists(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.Id)))
            {
                Directory.CreateDirectory(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.Id));
            }

            foreach (var file in uploadedAttachments)
            {
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(server.MapPath(TasksConstants.MainContentFolder + "\\" + dbTask.Id + "\\" + filename));
                dbTask.Attachments.Add(new Attachment()
                {
                    Name = file.FileName
                });

                Update(dbTask);
            }
        }

        public IQueryable<MainTask> GetAll()
        {
            return this.tasks.All();
        }
    }
}
