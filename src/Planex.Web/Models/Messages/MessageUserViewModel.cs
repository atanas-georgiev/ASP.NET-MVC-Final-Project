using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Areas.HR.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Models.Messages
{
    public class MessageUserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [UIHint("Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string Name { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, MessageUserViewModel>("")
                .ForMember(m => m.Name, opt => opt.MapFrom(c => c.FirstName + " " + c.LastName));
        }
    }
}