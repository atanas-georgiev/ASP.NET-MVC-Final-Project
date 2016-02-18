namespace Planex.Web.Areas.Lead.Models.Estimation
{
    using System.Linq;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class EstimationListViewModel : BaseProjectViewModel, IMapFrom<Project>, IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, EstimationListViewModel>(string.Empty)
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)));
        }
    }
}