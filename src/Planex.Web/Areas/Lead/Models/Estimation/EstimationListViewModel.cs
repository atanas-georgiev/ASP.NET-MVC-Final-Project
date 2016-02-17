using System.Linq;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models.Estimation
{
    public class EstimationListViewModel : BaseProjectViewModel, IMapFrom<Data.Models.Project>, IHaveCustomMappings
    {
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, EstimationListViewModel>("")
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)));
        }
    }
}