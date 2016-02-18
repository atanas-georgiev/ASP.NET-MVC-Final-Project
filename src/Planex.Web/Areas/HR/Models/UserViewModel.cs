namespace Planex.Web.Areas.HR.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Web.Infrastructure.Mappings;

    public class UserViewModel : IMapFrom<User>
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
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [UIHint("String")]
        public string LastName { get; set; }

        [Required]
        [UIHint("DropDown")]
        public string Role { get; set; }
    }
}