namespace Planex.Web.Areas.Worker
{
    using System.Web.Mvc;

    public class WorkerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Worker";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Worker_default", 
                "Worker/{controller}/{action}/{id}", 
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}