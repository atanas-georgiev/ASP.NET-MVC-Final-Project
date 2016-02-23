using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using Planex.Web.Areas.HR;
using Planex.Web.Areas.HR.Controllers;

namespace Planex.Web.Tests.Routes
{
    [TestClass]
    public class RouteTestsHRArea : AreaRouteFactsBase<HRAreaRegistration>
    {
        [TestMethod]
        public void HrSkillsRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Skills").To<SkillsController>(x => x.Index());
        }

        [TestMethod]
        public void HrUsersRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Users").To<UsersController>(x => x.Index());
        }

        [TestMethod]
        public void HrUserDetailsRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Users/Details/1").To<UsersController>(x => x.Details("1"));
        }

        [TestMethod]
        public void HrUserRemoveRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Users/Remove/1").To<UsersController>(x => x.Remove("1"));
        }
    }
}
