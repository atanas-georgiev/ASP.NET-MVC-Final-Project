using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models
{
    public class ProjectViewModel : IMapTo<MainTask>, IHaveCustomMappings
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string Title { get; set; }

        [Required]
        [UIHint("EnumDropDown")]
        public PriorityType Priority { get; set; }

        [Required]
        [UIHint("String")]
        public string Manager { get; set; }

        [Required]
        [UIHint("String")]
        public string Lead { get; set; }

        [Required]
        [UIHint("Number")]
        public int Completed { get; set; }

        [Required]
        public TaskStateType State { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MainTask, ProjectViewModel>("")
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.Lead, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => 0));
        }
    }
}