using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planex.Data.Models;

namespace Planex.Data
{
    public class PlanexDbContext : IdentityDbContext<User>
    {
        public PlanexDbContext()
            : base("PlanexDbConnection", false)
        {
        }

        public virtual IDbSet<Skill> Skills { get; set; }

        public virtual IDbSet<Attachment> Attachments { get; set; }

        public virtual IDbSet<Project> Projects { get; set; }

        public virtual IDbSet<SubTask> Tasks { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<SubTaskDependency> SubTaskDependencies { get; set; }

        public static PlanexDbContext Create()
        {
            return new PlanexDbContext();
        }
    }
}