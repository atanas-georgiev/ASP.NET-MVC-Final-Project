namespace Planex.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Planex.Data.Common.Models;
    using Planex.Data.Models;

    public class PlanexDbContext : IdentityDbContext<User>
    {
        public PlanexDbContext()
            : base("PlanexDbConnection", false)
        {
        }

        public virtual IDbSet<Attachment> Attachments { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }

        public virtual IDbSet<Project> Projects { get; set; }

        public virtual IDbSet<Skill> Skills { get; set; }

        public virtual IDbSet<SubTaskDependency> SubTaskDependencies { get; set; }

        public virtual IDbSet<SubTask> Tasks { get; set; }

        public static PlanexDbContext Create()
        {
            return new PlanexDbContext();
        }

        public override int SaveChanges()
        {
            try
            {
                this.ApplyAuditInfoRules();
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)))
                )
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}