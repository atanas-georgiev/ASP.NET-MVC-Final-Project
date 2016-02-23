namespace Planex.Web.Tests.Routes
{
    using MvcRouteTester;

    using NUnit.Framework;

    using Planex.Web.Areas.Lead;
    using Planex.Web.Areas.Lead.Controllers;

    [TestFixture]
    public class RouteTestsLeadArea : AreaRouteFactsBase<LeadAreaRegistration>
    {
        [Test]
        public void LeadEstimationsEditShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Lead/Estimations/Edit/1").To<EstimationsController>(x => x.Edit("1"));
        }

        [Test]
        public void LeadEstimationsRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Lead/Estimations").To<EstimationsController>(x => x.Index());
        }

        [Test]
        public void LeadEstimationsSendForApprovalShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Lead/Estimations/SendForApproval")
                .To<EstimationsController>(x => x.SendForApproval());
        }

        [Test]
        public void LeadProjectsEditRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Lead/Projects/Edit/1").To<ProjectsController>(x => x.Edit("1"));
        }

        [Test]
        public void LeadProjectsListRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Lead/Projects").To<ProjectsController>(x => x.Index());
        }
    }
}