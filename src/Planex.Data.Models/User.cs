namespace Planex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Planex.Data.Common;
    using Planex.Data.Common.Models;

    public class User : IdentityUser, IHavePrimaryKey<string>, IAuditInfo, IDeletableEntity
    {
        private ICollection<Skill> skills;

        private ICollection<SubTask> subtasks;

        public User()
        {
            this.skills = new HashSet<Skill>();
            this.subtasks = new List<SubTask>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        public int? ImageId { get; set; }

        public int IntId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool ResetPassword { get; set; }

        public virtual ICollection<Skill> Skills
        {
            get
            {
                return this.skills;
            }

            set
            {
                this.skills = value;
            }
        }

        public virtual ICollection<SubTask> SubTasks
        {
            get
            {
                return this.subtasks;
            }

            set
            {
                this.subtasks = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}