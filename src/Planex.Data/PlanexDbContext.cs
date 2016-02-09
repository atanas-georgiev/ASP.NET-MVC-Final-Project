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

        public static PlanexDbContext Create()
        {
            return new PlanexDbContext();
        }
    }
}