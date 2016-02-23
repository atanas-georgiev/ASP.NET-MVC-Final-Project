using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using Planex.Web.Areas.Manager;
using Planex.Web.Areas.Manager.Controllers;

namespace Planex.Web.Tests.Routes
{
    [TestClass]
    public class RouteTestsManagerArea : AreaRouteFactsBase<ManagerAreaRegistration>
    {
        // Manager
        [TestMethod]
        public void ManagerProjectListRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Manager/Projects").To<ProjectsController>(x => x.Index());
        }

        [TestMethod]
        public void ManagerProjectCreateRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Manager/Projects/Create").To<ProjectsController>(x => x.Create());
        }

        [TestMethod]
        public void ManagerProjectDetailsRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Manager/Projects/Details/1").To<ProjectsController>(x => x.Details("1"));
        }

        [TestMethod]
        public void ManagerProjectRemoveRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Manager/Projects/Remove/1").To<ProjectsController>(x => x.Remove("1"));
        }

        [TestMethod]
        public void ManagerProjectApproveRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Manager/Projects/Approve").To<ProjectsController>(x => x.Approve());
        }

        [TestMethod]
        public void ManagerProjectSendForEstimationRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Manager/Projects/SendForEstimation/1").To<ProjectsController>(x => x.SendForEstimation("1"));
        }
    }
}
