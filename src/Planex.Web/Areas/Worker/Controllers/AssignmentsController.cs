namespace Planex.Web.Areas.Worker.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Services.Tasks;
    using Planex.Services.Users;
    using Planex.Web.Areas.Worker.Models.Assignments;
    using Planex.Web.Infrastructure.Mappings;

    public class AssignmentsController : BaseController
    {
        private readonly ITaskService subTaskService;

        public AssignmentsController(IUserService userService, ITaskService subTaskService)
            : base(userService)
        {
            this.subTaskService = subTaskService;
        }

        public ActionResult Edit(string id)
        {
            var intId = int.Parse(id);
            var task = this.subTaskService.GetAll().Where(x => x.Id == intId).To<AssignmentViewModel>().FirstOrDefault();
            return this.View(task);
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}