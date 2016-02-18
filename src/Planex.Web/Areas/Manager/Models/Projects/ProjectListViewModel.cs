namespace Planex.Web.Areas.Manager.Models.Projects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectListViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        [Required]
        [UIHint("Number")]
        public string Completed { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Lead { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Manager { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public PriorityType Priority { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("Date")]
        public DateTime Start { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public TaskStateType State { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("String")]
        public string Title { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectListViewModel>(string.Empty)
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.Lead, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => c.PercentComplete));
        }
    }
}