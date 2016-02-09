using System.Data.Entity.Migrations;

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
            // todo: seed some data
        }
    }
}