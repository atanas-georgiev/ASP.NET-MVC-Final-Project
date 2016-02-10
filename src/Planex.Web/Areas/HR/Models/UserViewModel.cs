using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Planex.Data.Models;
using Planex.Web.App_LocalResources;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.HR.Models
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
     //   [Display(Name = "UserFirstName", ResourceType = typeof(Default))]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
   //     [Display(Name = "UserLastName", ResourceType = typeof(Default))]
        public string LastName { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }

        public ICollection<Skill> Skills;        
    }
}