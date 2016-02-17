using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Services.Projects;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class IndexController : BaseController
    {

        private readonly IProjectService taskService;
    //    private readonly IUserService userService;
        private readonly ITaskService subTaskService;

        public IndexController(IUserService userService, IProjectService taskService, ISkillService skillService, ITaskService subTaskService)
            : base(userService)
        {
            this.taskService = taskService;
        //    this.userService = userService;
            this.subTaskService = subTaskService;
        }

        public virtual ActionResult Index()
        {
            return View();
        }
    }
}