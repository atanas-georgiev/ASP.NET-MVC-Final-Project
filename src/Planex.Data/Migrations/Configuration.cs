namespace Planex.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Planex.Data.Models;

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

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var user = new User { UserName = "System", Email = "System@System.com", FirstName = "System", LastName = "Message", Salary = 0, CreatedOn = DateTime.UtcNow };
            userManager.Create(user, "System");
            userManager.AddToRole(user.Id, "Manager");

            var user2 = new User { UserName = "Admin", Email = "admin@planex.com", FirstName = "Admin", LastName = "Admin", Salary = 0, CreatedOn = DateTime.UtcNow };
            userManager.Create(user2, "123456");
            userManager.AddToRole(user2.Id, "Manager");

            context.SaveChanges();
        }
    }
}