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
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Manager"));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Lead"));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Worker"));
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("HR"));
            context.SaveChanges();

            var user = new User
                           {
                               UserName = "admin@planex.com", 
                               Email = "admin@planex.com", 
                               FirstName = "System", 
                               LastName = "Message", 
                               Salary = 0, 
                               CreatedOn = DateTime.UtcNow, 
                               ResetPassword = false,
                               IntId = 0
            };
            userManager.Create(user, "changeme");
            context.SaveChanges();
            userManager.AddToRole(user.Id, "Manager");
            context.SaveChanges();
        }
    }
}