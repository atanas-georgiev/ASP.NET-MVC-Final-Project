namespace Planex.Web.Models.Home
{
    using System;
    using System.Linq;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectHomeViewModel : IMapFrom<SubTask>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool HasChildren { get; set; }

        public decimal PercentComplete { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubTask, ProjectHomeViewModel>(string.Empty)
                .ForMember(m => m.ParentId, opt => opt.MapFrom(c => c.ParentId ?? c.ProjectId + 100000))
                .ForMember(m => m.PercentComplete, opt => opt.MapFrom(c => c.PercentComplete * 100))
                .ForMember(m => m.HasChildren, opt => opt.MapFrom(c => c.Subtasks.Any()));
        }
    }
}