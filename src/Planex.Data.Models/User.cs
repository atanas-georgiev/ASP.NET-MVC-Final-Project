using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Planex.Data.Models
{
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Skill> skills;
        private ICollection<SubTask> subtasks;

        public User()
        {
            this.skills = new HashSet<Skill>();
            this.subtasks = new List<SubTask>();
        }

        public int? IntId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public int? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        public virtual ICollection<Skill> Skills
        {
            get { return this.skills; }
            set { this.skills = value; }
        }

        public virtual ICollection<SubTask> SubTasks
        {
            get { return this.subtasks; }
            set { this.subtasks = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}