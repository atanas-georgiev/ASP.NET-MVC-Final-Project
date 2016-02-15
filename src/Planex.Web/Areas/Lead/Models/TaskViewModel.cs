using System;
using AutoMapper;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models
{
    public class TaskViewModel : IGanttTask, IMapFrom<Planex.Data.Models.Subtask>, IHaveCustomMappings
    {
        public int TaskID { get; set; }
        public int? ParentID { get; set; }

        public string Title { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        private DateTime end;
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public bool Summary { get; set; }
        public bool Expanded { get; set; }
        public decimal PercentComplete { get; set; }
        public int OrderId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Planex.Data.Models.Subtask, TaskViewModel>("")
                .ForMember(m => m.TaskID, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.ParentID, opt => opt.MapFrom(c => c.ParentId))
                .ForMember(m => m.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(m => m.Start, opt => opt.MapFrom(c => c.Start))
                .ForMember(m => m.End, opt => opt.MapFrom(c => c.End))
                .ForMember(m => m.Expanded, opt => opt.MapFrom(c => true))
                .ForMember(m => m.Summary, opt => opt.MapFrom(c => true));
        }
    }
}