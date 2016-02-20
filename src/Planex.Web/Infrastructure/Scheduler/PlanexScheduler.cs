using System;
using System.Threading;

using FluentScheduler;

namespace Planex.Web.Infrastructure.Scheduler
{
    using Planex.Services.Messages;
    using Planex.Services.Projects;
    using Planex.Services.Tasks;
    using Planex.Services.Users;


    public class PlanexScheduler : Registry
    {
        public PlanexScheduler()
        {
            // Schedule an ITask to run at an interval
            this.Schedule<ProjectStateTask>().ToRunNow().AndEvery(10).Seconds();
        }
    }
}