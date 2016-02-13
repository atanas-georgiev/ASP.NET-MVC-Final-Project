using System.Collections.Generic;
using Planex.Data.Models;
using System.Web;

namespace Planex.Services.Tasks
{
    public interface ITaskService
    {
        void Add(MainTask task);
        void Update(MainTask task);
        void AddAttachments(MainTask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);
    }
}
