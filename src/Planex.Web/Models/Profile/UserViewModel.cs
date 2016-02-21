namespace Planex.Web.Models.Profile
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [LocalizedDisplay("UserEmail")]
        [UIHint("Email")]
        public string Email { get; set; }

        [LocalizedDisplay("UserFirstName")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string FirstName { get; set; }

        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public int? ImageId { get; set; }

        [LocalizedDisplay("UserLastName")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string LastName { get; set; }

        [LocalizedDisplay("UserRole")]
        public string RoleId { get; set; }

        [LocalizedDisplay("Salary")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Currency")]
        public decimal Salary { get; set; }

        [LocalizedDisplay("SkillName")]
        public List<string> Skills { get; set; }

        [LocalizedDisplay("Theme")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("DropDownThemes")]
        public string Theme { get; set; }

        [LocalizedDisplay("UserImage")]
        public HttpPostedFileBase UploadedImage { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>(string.Empty)
                .ForMember(m => m.Skills, opt => opt.MapFrom(c => c.Skills.Select(s => s.Name)))
                .ForMember(m => m.RoleId, opt => opt.MapFrom(c => c.Roles.FirstOrDefault().RoleId));
        }
    }
}