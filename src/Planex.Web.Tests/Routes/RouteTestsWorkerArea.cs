namespace Planex.Web.Tests.Routes
{
    using MvcRouteTester;

    using NUnit.Framework;

    using Planex.Web.Areas.Worker;
    using Planex.Web.Areas.Worker.Controllers;

    [TestFixture]
    public class RouteTestsWorkerArea : AreaRouteFactsBase<WorkerAreaRegistration>
    {
        [Test]
        public void WorkerTasksListRouteShouldCallCorrespondingController()
        {
            this.Routes.ShouldMap("/Worker/Assignments").To<AssignmentsController>(x => x.Index());
        }
    }
}