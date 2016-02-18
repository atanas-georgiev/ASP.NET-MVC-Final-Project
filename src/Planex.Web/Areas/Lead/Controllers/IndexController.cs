namespace Planex.Web.Areas.Lead.Controllers
{
    using System.Web.Mvc;

    using Planex.Services.Projects;
    using Planex.Services.Skills;
    using Planex.Services.Tasks;
    using Planex.Services.Users;

    public class IndexController : BaseController
    {
        // private readonly IUserService userService;
        private readonly ITaskService subTaskService;

        private readonly IProjectService taskService;

        public IndexController(
            IUserService userService, 
            IProjectService taskService, 
            ISkillService skillService, 
            ITaskService subTaskService)
            : base(userService)
        {
            this.taskService = taskService;

            // this.userService = userService;
            this.subTaskService = subTaskService;
        }

        public virtual ActionResult Index()
        {
            return this.View();
        }
    }
}