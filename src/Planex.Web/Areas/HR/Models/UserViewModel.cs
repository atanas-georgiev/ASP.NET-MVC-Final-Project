using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Planex.Data.Models;
using Planex.Web.App_LocalResources;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.HR.Models
{
    public class UserViewModel : IMapFrom<User>
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
        public string FirstName { get; set; }

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