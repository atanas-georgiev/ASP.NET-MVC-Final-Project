using System.Collections.Generic;
using System.Linq;
using Planex.Data.Models;
using System.Web;

namespace Planex.Services.Tasks
{
    public interface ITaskService
    {
        void Add(MainTask task);
        void Update(MainTask task);
        void AddAttachments(MainTask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);

        IQueryable<MainTask> GetAll();

        MainTask GetById(int id);

        void StartEstimation(int taskId, string userId);
    }
}
