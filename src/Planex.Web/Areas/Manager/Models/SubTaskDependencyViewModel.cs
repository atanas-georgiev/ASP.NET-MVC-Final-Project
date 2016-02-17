using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Kendo.Mvc.UI;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models
{
    public class SubTaskDependencyViewModel : IGanttDependency, IMapFrom<Planex.Data.Models.SubTask>, IHaveCustomMappings
    {
        public int DependencyID { get; set; }
        public int PredecessorID { get; set; }
        public int SuccessorID { get; set; }
        public DependencyType Type { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Planex.Data.Models.SubTask, SubTaskDependencyViewModel>("")
                .ForMember(m => m.DependencyID, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.PredecessorID, opt => opt.MapFrom(c => (int)c.DependencyId))
                .ForMember(m => m.Type, opt => opt.MapFrom(c => DependencyType.StartStart));
        }
    }
}