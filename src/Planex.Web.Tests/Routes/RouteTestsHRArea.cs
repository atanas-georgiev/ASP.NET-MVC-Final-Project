namespace Planex.Web.Tests.Routes
{
    using MvcRouteTester;

    using NUnit.Framework;

    using Planex.Web.Areas.HR;
    using Planex.Web.Areas.HR.Controllers;

    [TestFixture]
    public class RouteTestsHRArea : AreaRouteFactsBase<HRAreaRegistration>
    {
        [Test]
        public void HrSkillsRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/HR/Skills").To<SkillsController>(x => x.Index());
        }

        [Test]
        public void HrUserDetailsRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/HR/Users/Details/1").To<UsersController>(x => x.Details("1"));
        }

        [Test]
        public void HrUserRemoveRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/HR/Users/Remove/1").To<UsersController>(x => x.Remove("1"));
        }

        [Test]
        public void HrUsersRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/HR/Users").To<UsersController>(x => x.Index());
        }
    }
}