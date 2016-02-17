using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planex.Data.Models;

namespace Planex.Services.Projects
{
    public interface IProjectService
    {
        void Add(Project task);
        void Update(Project task);
        void Remove(int id);
        void AddAttachments(Project dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);

        IQueryable<Project> GetAll();

        Project GetById(int id);

        void StartEstimation(int taskId, string userId);
    }
}
