using System;
using System.Collections.Generic;
using AutoMapper;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models
{
    public class ProjectDetailsViewModel : IGanttTask, IMapFrom<Planex.Data.Models.Subtask>, IHaveCustomMappings
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public int? ParentTaskId { get; set; }
        public int TaskOrderId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal PercentComplete { get; set; }
        public bool Summary { get; set; }
        public bool Expanded { get; set; }
        public int OrderId { get; set; }

      //  public List<ProjectDetailsResourseViewModel> Users { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Planex.Data.Models.Subtask, ProjectDetailsViewModel>("")
                .ForMember(m => m.TaskId, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.ParentTaskId, opt => opt.MapFrom(c => c.ParentId))
                .ForMember(m => m.Expanded, opt => opt.MapFrom(c => true))
                .ForMember(m => m.Summary, opt => opt.MapFrom(c => true));
        }
    }
}