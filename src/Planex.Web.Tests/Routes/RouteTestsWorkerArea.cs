using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;
using Planex.Web.Areas.Worker;
using Planex.Web.Areas.Worker.Controllers;

namespace Planex.Web.Tests.Routes
{
    [TestClass]
    public class RouteTestsWorkerArea : AreaRouteFactsBase<WorkerAreaRegistration>
    {
        [TestMethod]
        public void WorkerTasksListRouteShouldCallCorrespondingController()
        {
            Routes.ShouldMap("/Worker/Assignments").To<AssignmentsController>(x => x.Index());
        }
    }
}
