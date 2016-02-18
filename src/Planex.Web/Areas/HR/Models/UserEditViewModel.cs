namespace Planex.Web.Areas.HR.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class UserEditViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [Required]
        [UIHint("Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string FirstName { get; set; }

        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public int? ImageId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string LastName { get; set; }

        [Required]
        [UIHint("Currency")]
        public decimal PricePerHour { get; set; }

        [Required]
        [UIHint("DropDown")]
        public string Role { get; set; }

        public List<string> Skills { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserEditViewModel>(string.Empty)
                .ForMember(m => m.Skills, opt => opt.MapFrom(c => c.Skills.Select(s => s.Name)));
        }
    }
}