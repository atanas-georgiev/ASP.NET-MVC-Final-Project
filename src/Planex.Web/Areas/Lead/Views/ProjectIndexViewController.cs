namespace Planex.Web.Areas.Lead.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectIndexViewController : BaseProjectViewModel, IMapFrom<Project>, IHaveCustomMappings
    {
        [Required]
        [UIHint("Number")]
        public int Completed { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectIndexViewController>(string.Empty)
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)))
                .ForMember(m => m.Completed, opt => opt.MapFrom(c => 0));
        }
    }
}