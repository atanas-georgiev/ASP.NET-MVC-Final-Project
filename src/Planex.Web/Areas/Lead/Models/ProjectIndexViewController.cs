using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Areas.Lead.Models.Project;
using Planex.Web.Infrastructure.Mappings;
namespace Planex.Web.Areas.Lead.Models
{
    public class ProjectIndexViewController : ProjectViewModel, IMapFrom<Data.Models.Project>, IHaveCustomMappings
    {
        [Required]
        [UIHint("Number")]
        public int Completed { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Project, ProjectIndexViewController>("")
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.Lead, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => 0));
        }
    }
}