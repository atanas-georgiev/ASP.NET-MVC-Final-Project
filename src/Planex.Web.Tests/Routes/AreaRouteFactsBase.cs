namespace Planex.Web.Tests.Routes
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public abstract class AreaRouteFactsBase<T>
        where T : AreaRegistration, new()
    {
        public AreaRouteFactsBase()
        {
            // Arrange
            this.Routes = new RouteCollection();
            T area = new T();
            AreaRegistrationContext context = new AreaRegistrationContext(area.AreaName, this.Routes);
            area.RegisterArea(context);
        }

        public RouteCollection Routes { get; private set; }
    }
}