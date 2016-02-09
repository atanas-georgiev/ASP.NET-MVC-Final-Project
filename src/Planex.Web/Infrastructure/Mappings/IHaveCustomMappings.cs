using AutoMapper;

namespace Planex.Web.Infrastructure.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}