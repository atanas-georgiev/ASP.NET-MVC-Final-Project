using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planex.Data.Models;

namespace Planex.Services.Tasks
{
    public interface ITaskService
    {
        void Add(SubTask task);
        void Update(SubTask task);
        void AddAttachments(SubTask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);
        IQueryable<SubTask> GetAll();
        SubTask GetById(int id);
        void Delete(int id);

        void AddDependency(SubTaskDependency dep);
        IQueryable<SubTaskDependency> AllDependencies();
        void DeleteDependency(int id);
        void UpdateDependency(SubTaskDependency task);
    }
}
