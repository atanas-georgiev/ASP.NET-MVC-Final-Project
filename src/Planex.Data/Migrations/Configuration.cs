namespace Planex.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<PlanexDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PlanexDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            this.SeedRoles(context);
        }

        private void SeedRoles(PlanexDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Manager"));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Lead"));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Worker"));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("HR"));
            context.SaveChanges();
        }
    }
}