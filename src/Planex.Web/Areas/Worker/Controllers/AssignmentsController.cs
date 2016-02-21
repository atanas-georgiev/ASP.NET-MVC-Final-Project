namespace Planex.Web.Areas.Worker.Controllers
{
    using System.Web.Mvc;

    using Planex.Services.Tasks;
    using Planex.Services.Users;

    public class AssignmentsController : BaseController
    {
        private readonly ITaskService subTaskService;

        public AssignmentsController(IUserService userService, ITaskService subTaskService)
            : base(userService)
        {
            this.subTaskService = subTaskService;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}