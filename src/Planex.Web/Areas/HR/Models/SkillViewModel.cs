namespace Planex.Web.Areas.HR.Models
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Mappings;

    public class SkillViewModel : IMapFrom<Skill>
    {
        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [UIHint("String")]
        [LocalizedDisplay("SkillName")]
        [LocalizedRequired("RequiredFiled")]
        public string Name { get; set; }
    }
}