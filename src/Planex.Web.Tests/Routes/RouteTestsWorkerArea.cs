using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using NUnit.Framework;
using Planex.Web.Areas.Worker;
using Planex.Web.Areas.Worker.Controllers;

namespace Planex.Web.Tests.Routes
{
    [TestFixture]
    public class RouteTestsWorkerArea : AreaRouteFactsBase<WorkerAreaRegistration>
    {
        [Test]
        public void WorkerTasksListRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Worker/Assignments").To<AssignmentsController>(x => x.Index());
        }
    }
}
