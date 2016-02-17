using AutoMapper;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models.Gantt
{
    public class ProjectDetailsDependencyViewModel : IGanttDependency, IMapFrom<SubTaskDependency>, IHaveCustomMappings
    {
        public int DependencyId { get; set; }

        public int PredecessorId { get; set; }

        public int SuccessorId { get; set; }

        public DependencyType Type { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Planex.Data.Models.SubTask, ProjectDetailsDependencyViewModel>("")
                .ForMember(m => m.DependencyId, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.SuccessorId, opt => opt.MapFrom(c => (int)c.DependencyId))
                .ForMember(m => m.Type, opt => opt.MapFrom(c => DependencyType.StartFinish));
        }
    }
}