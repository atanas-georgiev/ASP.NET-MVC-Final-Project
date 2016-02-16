using System;
using AutoMapper;
using Kendo.Mvc.UI;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models
{
    public class ProjectDetailsDependencyViewModel : IGanttDependency, IMapFrom<Planex.Data.Models.Subtask>, IHaveCustomMappings
    {
        public int DependencyID { get; set; }
        public int PredecessorID { get; set; }
        public int SuccessorID { get; set; }
        public DependencyType Type { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Planex.Data.Models.Subtask, ProjectDetailsDependencyViewModel>("")
                .ForMember(m => m.DependencyID, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.SuccessorID, opt => opt.MapFrom(c => (int)c.DependencyId))
               // .ForMember(m => m.PredecessorID, opt => opt.MapFrom(c => ))
                .ForMember(m => m.Type, opt => opt.MapFrom(c => DependencyType.StartFinish));
        }
    }
}