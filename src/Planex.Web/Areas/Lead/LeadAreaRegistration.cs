namespace Planex.Web.Areas.Lead
{
    using System.Web.Mvc;

    public class LeadAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Lead";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Lead_default", 
                "Lead/{controller}/{action}/{id}", 
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}