namespace Planex.Web.Areas.Lead.Models.Gantt
{
    using AutoMapper;

    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectDetailsDependencyViewModel : IGanttDependency, IMapFrom<SubTaskDependency>
    {
        public int Id { get; set; }

        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }
    }
}