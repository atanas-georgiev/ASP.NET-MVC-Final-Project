namespace Planex.Web.Tests
{
    using System.Security.Policy;
    using System.Web.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;

    using Planex.Web.Areas.HR.Controllers;
    using Planex.Web.Areas.Lead.Controllers;
    using Planex.Web.Controllers;

    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void HomePageShould()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap("/Home").To<HomeController>(x => x.Index());
        }
    }
}
