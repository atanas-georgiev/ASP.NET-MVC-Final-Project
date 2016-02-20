namespace Planex.Web.Areas.HR.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class UserEditViewModel : IMapFrom<User>, IHaveCustomMappings
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
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public int? ImageId { get; set; }

        [LocalizedDisplay("UserLastName")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("String")]
        public string LastName { get; set; }

        [LocalizedDisplay("Salary")]
        [LocalizedRequired("RequiredFiled")]
        [UIHint("Currency")]
        public decimal Salary { get; set; }

        [LocalizedDisplay("UserRole")]
        [LocalizedRequired("RequiredFiled")]
        public string RoleId { get; set; }

        [LocalizedDisplay("SkillName")]
        public List<string> Skills { get; set; }

        [LocalizedDisplay("UserImage")]
        public HttpPostedFileBase UploadedImage { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserEditViewModel>(string.Empty)
                .ForMember(m => m.Skills, opt => opt.MapFrom(c => c.Skills.Select(s => s.Name)))
                .ForMember(m => m.RoleId, opt => opt.MapFrom(c => c.Roles.FirstOrDefault().RoleId));
        }
    }
}