namespace Planex.Web.Areas.HR.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [LocalizedDisplay("UserEmail")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Email")]
        public string Email { get; set; }

        [LocalizedDisplay("UserFirstName")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string FirstName { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [LocalizedDisplay("UserLastName")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string LastName { get; set; }

        [UIHint("DropDown")]
        [LocalizedDisplay("UserRole")]
        [LocalizedRequired("RequiredFiled")]
       
        public string RoleId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Role { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>(string.Empty)
                .ForMember(m => m.RoleId, opt => opt.MapFrom(c => c.Roles.FirstOrDefault().RoleId));
        }
    }
}