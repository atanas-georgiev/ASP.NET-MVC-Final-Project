namespace Planex.Web.Areas.Lead.Models.Gantt
{
    using AutoMapper;

    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectDetailsDependencyViewModel : IGanttDependency, IMapFrom<SubTaskDependency>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
//            configuration.CreateMap<SubTask, ProjectDetailsDependencyViewModel>(string.Empty)
//                .ForMember(m => m.DependencyId, opt => opt.MapFrom(c => c.Id))
//                .ForMember(m => m.SuccessorId, opt => opt.MapFrom(c => (int)c.Id))
//                .ForMember(m => m.Type, opt => opt.MapFrom(c => DependencyType.StartFinish));
        }
    }
}