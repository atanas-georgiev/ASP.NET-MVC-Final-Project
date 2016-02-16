using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models
{
    public class ProjectDetailsViewModel : IMapFrom<MainTask>, IHaveCustomMappings
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
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("Editor")]
        public string Description { get; set; }

        [Required]
        [UIHint("EnumDropDown")]
        public PriorityType Priority { get; set; }

        [Required]
        [UIHint("String")]
        public string ManagerName { get; set; }

        [Required]
        [UIHint("String")]
        public string LeadName { get; set; }

        [Required]
        [UIHint("Number")]
        public int Completed { get; set; }

        public TaskStateType State { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }

        public List<HttpPostedFileBase> UploadedAttachments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MainTask, ProjectDetailsViewModel>("")
                .ForMember(m => m.ManagerName, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.LeadName, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => 0));
        }
    }
}