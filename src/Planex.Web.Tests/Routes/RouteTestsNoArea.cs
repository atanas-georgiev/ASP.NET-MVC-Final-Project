using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using NUnit.Framework;
using Planex.Web.Areas.Lead;
using Planex.Web.Areas.Lead.Controllers;
using Planex.Web.Controllers;

namespace Planex.Web.Tests.Routes
{
    [TestFixture]
    public class RouteTestsNoArea
    {
        private RouteCollection routeCollection;

        [OneTimeSetUp]
        public void InitTests()
        {
            this.routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routeCollection);
        }

        public void RegisterAreaRoutes<TAreaRegistration>(RouteCollection routes) where TAreaRegistration : AreaRegistration, new()
        {
            var area = new TAreaRegistration();
            var ctx = new AreaRegistrationContext(area.AreaName, routes);
            area.RegisterArea(ctx);
        }

        // Home and login
        [Test]
        public void HomeRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Home").To<HomeController>(x => x.Index());
        }

        [Test]
        public void LogoffRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Account/Logoff").To<AccountController>(x => x.LogOff());
        }

        [Test]
        public void ResetRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Account/ResetPassword").To<AccountController>(x => x.ResetPassword());
        }

        // Profile user page
        [Test]
        public void ProfileRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Profile").To<ProfileController>(x => x.Index());
        }

        // Messages
        [Test]
        public void MessagesInboxRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Messages/Inbox").To<MessagesController>(x => x.Inbox());
        }

        [Test]
        public void MessagesSendRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Messages/Send").To<MessagesController>(x => x.Send());
        }

        [Test]
        public void MessagesDetailsRouteShouldCallCorrespondingController()
        {
            this.routeCollection.ShouldMap("/Messages/Details/1").To<MessagesController>(x => x.Details("1"));
        }
    }
}
