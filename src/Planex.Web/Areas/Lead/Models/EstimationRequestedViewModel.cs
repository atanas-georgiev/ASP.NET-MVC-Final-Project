using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models
{
    public class EstimationRequestedViewModel : IMapFrom<MainTask>, IHaveCustomMappings
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
        [MaxLength(500)]
        [UIHint("Editor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [UIHint("EnumDropDown")]
        public PriorityType Priority { get; set; }

        public List<string> UploadedAttachmentFiles { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MainTask, EstimationRequestedViewModel>("")
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(s => s.Name)));
        }
    }
}