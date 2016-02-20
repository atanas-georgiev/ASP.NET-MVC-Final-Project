namespace Planex.Web.Areas.Manager.Models.Projects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectListViewModel : IMapFrom<Project>, IHaveCustomMappings
    {        
        [UIHint("Number")]
        [LocalizedDisplay("ProjectCompleted")]
        public string Completed { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [MaxLength(100)]
        [UIHint("String")]
        [LocalizedDisplay("ProjectLead")]
        public string Lead { get; set; }

        [UIHint("String")]
        [LocalizedDisplay("ProjectManager")]
        public string Manager { get; set; }

        [UIHint("String")]
        [LocalizedDisplay("ProjectPriority")]
        public PriorityType Priority { get; set; }

        [UIHint("Date")]
        [LocalizedDisplay("ProjectStartDate")]
        public DateTime Start { get; set; }

        [UIHint("String")]
        [LocalizedDisplay("ProjectState")]
        public TaskStateType State { get; set; }

        [UIHint("String")]
        [LocalizedDisplay("ProjectTitle")]
        public string Title { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectListViewModel>(string.Empty)
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.Lead, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => c.PercentComplete * 100));
        }
    }
}