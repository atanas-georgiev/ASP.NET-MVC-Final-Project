namespace Planex.Services.Projects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Planex.Data.Models;

    public interface IProjectService
    {
        void Add(Project task);

        void AddAttachments(Project dbTask, List<HttpPostedFileBase> uploadedAttachments, HttpServerUtility server);

        IQueryable<Project> GetAll();

        Project GetById(int id);

        void Remove(int id);

        void StartEstimation(int taskId, string userId);

        void Update(Project task);
    }
}