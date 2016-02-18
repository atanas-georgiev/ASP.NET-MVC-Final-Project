namespace Planex.Services.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Planex.Data.Models;

    public interface ITaskService
    {
        void Add(SubTask task);

        void AddAttachments(SubTask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);

        void AddDependency(SubTaskDependency dep);

        IQueryable<SubTaskDependency> AllDependencies();

        void Delete(int id);

        void DeleteDependency(int id);

        IQueryable<SubTask> GetAll();

        SubTask GetById(int id);

        void Update(SubTask task);

        void UpdateDependency(SubTaskDependency task);
    }
}