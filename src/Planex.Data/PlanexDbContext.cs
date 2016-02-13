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

        public virtual IDbSet<MainTask> MainTasks { get; set; }

        public virtual IDbSet<Subtask> Subtasks { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static PlanexDbContext Create()
        {
            return new PlanexDbContext();
        }
    }
}