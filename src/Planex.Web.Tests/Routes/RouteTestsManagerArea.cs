namespace Planex.Web.Tests.Routes
{
    using MvcRouteTester;

    using NUnit.Framework;

    using Planex.Web.Areas.Manager;
    using Planex.Web.Areas.Manager.Controllers;

    [TestFixture]
    public class RouteTestsManagerArea : AreaRouteFactsBase<ManagerAreaRegistration>
    {
        [Test]
        public void ManagerProjectApproveRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Manager/Projects/Approve").To<ProjectsController>(x => x.Approve());
        }

        [Test]
        public void ManagerProjectCreateRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Manager/Projects/Create").To<ProjectsController>(x => x.Create());
        }

        [Test]
        public void ManagerProjectDetailsRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Manager/Projects/Details/1").To<ProjectsController>(x => x.Details("1"));
        }

        // Manager
        [Test]
        public void ManagerProjectListRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Manager/Projects").To<ProjectsController>(x => x.Index());
        }

        [Test]
        public void ManagerProjectRemoveRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Manager/Projects/Remove/1").To<ProjectsController>(x => x.Remove("1"));
        }

        [Test]
        public void ManagerProjectSendForEstimationRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Manager/Projects/SendForEstimation/1")
                .To<ProjectsController>(x => x.SendForEstimation("1"));
        }
    }
}