namespace Planex.Web.Areas.Manager.Models.Gantt
{
    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class ProjectDetailsResourseViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Color { get; set; }

        public string Name { get; set; }

        public int ResourseId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, ProjectDetailsResourseViewModel>(string.Empty)
                .ForMember(m => m.ResourseId, opt => opt.MapFrom(x => x.IntId))
                .ForMember(m => m.Name, opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
                .ForMember(m => m.Color, opt => opt.MapFrom(c => "#f44336"));
        }
    }
}