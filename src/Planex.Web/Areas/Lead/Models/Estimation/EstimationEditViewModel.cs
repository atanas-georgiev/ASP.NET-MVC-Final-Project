using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models.Estimation
{
    public class EstimationEditViewModel : BaseProjectViewModel, IMapFrom<Data.Models.Project>, IHaveCustomMappings
    {
        [UIHint("Date")]
        public DateTime End { get; set; }

        [UIHint("Currency")]
        public decimal Price { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Project, EstimationEditViewModel>("")
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)));
        }
    }
}