using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.HR.Models
{
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