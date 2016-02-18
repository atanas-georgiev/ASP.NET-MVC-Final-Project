namespace Planex.Web.Models.Messages
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class MessageUserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [Required]
        [UIHint("Email")]
        public string Email { get; set; }

        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string Name { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, MessageUserViewModel>(string.Empty)
                .ForMember(m => m.Name, opt => opt.MapFrom(c => c.FirstName + " " + c.LastName));
        }
    }
}