using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planex.Web.Areas.Lead.Models.SubTask;

namespace Planex.Web.Areas.Lead.Models
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mappings;

    public class EstimationEditViewModelSubTask : SubTaskViewModel, IMapFrom<Planex.Data.Models.SubTask>, IHaveCustomMappings
    {
        public string ParentName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.SubTask, EstimationEditViewModelSubTask>("")
                .ForMember(m => m.SelectedSkill, (IMemberConfigurationExpression<Data.Models.SubTask> opt) => opt.MapFrom(c => c.Skill.Name))
                .ForMember(m => m.SelectedUsers, (IMemberConfigurationExpression<Data.Models.SubTask> opt) => opt.MapFrom(c => c.Users.Select(x => x.Email)));
            // .ForMember(m => m.ParentName, opt => opt.MapFrom(c => c.Parent.Title));
        }
    }
}