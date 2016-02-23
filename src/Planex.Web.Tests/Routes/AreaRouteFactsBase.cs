using System.Web.Mvc;
using System.Web.Routing;

namespace Planex.Web.Tests.Routes
{
    public abstract class AreaRouteFactsBase<T>
        where T : AreaRegistration, new()
    {
        public RouteCollection Routes { get; private set; }

        public AreaRouteFactsBase()
        {
            // Arrange
            Routes = new RouteCollection();
            T area = new T();
            AreaRegistrationContext context = new AreaRegistrationContext(area.AreaName, Routes);
            area.RegisterArea(context);
        }
    }
}