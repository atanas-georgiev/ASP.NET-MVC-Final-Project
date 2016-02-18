namespace Planex.Web.Areas.HR.Models
{
    using System.ComponentModel.DataAnnotations;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class SkillViewModel : IMapFrom<Skill>
    {
        [Key]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string Name { get; set; }
    }
}