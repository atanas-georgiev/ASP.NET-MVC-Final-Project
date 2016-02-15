using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planex.Data.Models;

namespace Planex.Services.SubTasks
{
    public interface ISubTaskService
    {
        void Add(Subtask task);
        void Update(Subtask task);
        void AddAttachments(Subtask dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);
        IQueryable<Subtask> GetAll();
        Subtask GetById(int id);
    }
}
