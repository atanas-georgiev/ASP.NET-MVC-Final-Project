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

    public class EstimationEditViewModelSubTask : SubTaskViewModel, IMapFrom<Planex.Data.Models.Subtask>, IHaveCustomMappings
    {
        public string ParentName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Subtask, EstimationEditViewModelSubTask>("")
                .ForMember(m => m.SelectedSkill, opt => opt.MapFrom(c => c.Skill.Name))
                .ForMember(m => m.SelectedUsers, opt => opt.MapFrom(c => c.Users.Select(x => x.Email)));
            // .ForMember(m => m.ParentName, opt => opt.MapFrom(c => c.Parent.Title));
        }
    }
}