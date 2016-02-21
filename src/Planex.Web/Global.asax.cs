namespace Planex.Web
{
    using System.Reflection;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using FluentScheduler;

    using Planex.Web.Infrastructure.Localization;
    using Planex.Web.Infrastructure.Scheduler;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            AutofacConfig.RegisterAutofac();

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}