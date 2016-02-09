using System.Data.Entity;
using Planex.Data;
using Planex.Data.Migrations;

namespace Planex.Web.App_Start
{
    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PlanexDbContext, Configuration>());
        }
    }
}