using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planex.Data.Models;

namespace Planex.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<PlanexDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("StoreKeeper"));
            context.SaveChanges();
        }
    }
}