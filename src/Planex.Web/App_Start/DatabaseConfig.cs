namespace Planex.Web
{
    using System.Data.Entity;

    using Planex.Data;
    using Planex.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PlanexDbContext, Configuration>());
        }
    }
}