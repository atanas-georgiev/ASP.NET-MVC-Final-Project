using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using Planex.Web.Areas.HR;
using Planex.Web.Areas.HR.Controllers;
using NUnit.Framework;

namespace Planex.Web.Tests.Routes
{
    [TestFixture]
    public class RouteTestsHRArea : AreaRouteFactsBase<HRAreaRegistration>
    {
        [Test]
        public void HrSkillsRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Skills").To<SkillsController>(x => x.Index());
        }

        [Test]
        public void HrUsersRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Users").To<UsersController>(x => x.Index());
        }

        [Test]
        public void HrUserDetailsRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Users/Details/1").To<UsersController>(x => x.Details("1"));
        }

        [Test]
        public void HrUserRemoveRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/HR/Users/Remove/1").To<UsersController>(x => x.Remove("1"));
        }
    }
}
