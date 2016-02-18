namespace Planex.Web.Areas.Lead.Models.Gantt
{
    using System;

    using AutoMapper;

    using Kendo.Mvc.UI;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectDetailsViewModel : IGanttTask, IMapFrom<SubTask>, IHaveCustomMappings
    {
        public DateTime End { get; set; }

        public bool Expanded { get; set; }

        public int OrderId { get; set; }

        public int? ParentTaskId { get; set; }

        public decimal PercentComplete { get; set; }

        public DateTime Start { get; set; }

        public bool Summary { get; set; }

        public int TaskId { get; set; }

        public int TaskOrderId { get; set; }

        public string Title { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubTask, ProjectDetailsViewModel>(string.Empty)
                .ForMember(m => m.TaskId, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.ParentTaskId, opt => opt.MapFrom(c => c.ParentId))
                .ForMember(m => m.Expanded, opt => opt.MapFrom(c => true))
                .ForMember(m => m.Summary, opt => opt.MapFrom(c => true));
        }
    }
}