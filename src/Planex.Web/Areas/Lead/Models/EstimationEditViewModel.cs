using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Areas.Lead.Models.Project;
using Planex.Web.Areas.Lead.Models.SubTask;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Models
{
    public class EstimationEditViewModel : ProjectViewModel, IMapFrom<MainTask>, IHaveCustomMappings
    {
        //public List<EstimationEditViewModelSubTask> SubTasks { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MainTask, EstimationEditViewModel>("")
                .ForMember(m => m.Manager, opt => opt.MapFrom(c => c.Manager.FirstName + " " + c.Manager.LastName))
                .ForMember(m => m.Lead, opt => opt.MapFrom(c => c.Lead.FirstName + " " + c.Lead.LastName))
                .ForMember(m => m.UploadedAttachmentFiles, opt => opt.MapFrom(c => c.Attachments.Select(x => x.Name)));
        }
    }
}