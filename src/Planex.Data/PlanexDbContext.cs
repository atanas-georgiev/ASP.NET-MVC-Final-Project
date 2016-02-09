using System.Data.Entity;

namespace Planex.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Planex.Data.Models;

    public class PlanexDbContext : IdentityDbContext<User>
    {
        public PlanexDbContext()
            : base("PlanexDbConnection", false)
        {
        }

        public virtual IDbSet<Skill> Skills { get; set; }

        public virtual IDbSet<Resource> Resources { get; set; }

        public virtual IDbSet<MainTask> MainTasks { get; set; }

        public virtual IDbSet<Subtask> Subtasks { get; set; }        

        public static PlanexDbContext Create()
        {
            return new PlanexDbContext();
        }
    }
}