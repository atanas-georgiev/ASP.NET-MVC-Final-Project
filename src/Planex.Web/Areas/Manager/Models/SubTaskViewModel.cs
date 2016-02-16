using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models
{
    public class SubTaskViewModel : IGanttTask, IMapFrom<Planex.Data.Models.Subtask>, IHaveCustomMappings
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

        public string Resources { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Planex.Data.Models.Subtask, SubTaskViewModel>("")
                .ForMember(m => m.TaskID, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.ParentID, opt => opt.MapFrom(c => c.ParentId))
                .ForMember(m => m.Expanded, opt => opt.MapFrom(c => true))
                .ForMember(m => m.Summary, opt => opt.MapFrom(c => true))
                .ForMember(m => m.Resources, opt => opt.MapFrom(c => "asdasd, asdas"));
        }
    }
}