using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using Planex.Web.Areas.Lead;
using Planex.Web.Areas.Lead.Controllers;

namespace Planex.Web.Tests.Routes
{
    [TestClass]
    public class RouteTestsLeadArea : AreaRouteFactsBase<LeadAreaRegistration>
    {
        [TestMethod]
        public void LeadProjectsListRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Lead/Projects").To<ProjectsController>(x => x.Index());
        }

        [TestMethod]
        public void LeadProjectsEditRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Lead/Projects/Edit/1").To<ProjectsController>(x => x.Edit("1"));
        }

        [TestMethod]
        public void LeadEstimationsRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Lead/Estimations").To<EstimationsController>(x => x.Index());
        }

        [TestMethod]
        public void LeadEstimationsEditShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Lead/Estimations/Edit/1").To<EstimationsController>(x => x.Edit("1"));
        }

        [TestMethod]
        public void LeadEstimationsSendForApprovalShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Lead/Estimations/SendForApproval").To<EstimationsController>(x => x.SendForApproval());
        }
    }
}
