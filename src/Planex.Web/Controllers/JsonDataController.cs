using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Services.Messages;
using Planex.Services.Projects;
using Planex.Services.Skills;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models.Estimation;
using Planex.Web.Infrastructure.Mappings;
using Planex.Web.Models.Messages;

namespace Planex.Web.Controllers
{
    public class JsonDataController : BaseController
    {
        private readonly ISkillService skillService;
        private readonly ITaskService subTaskService;
        private readonly IProjectService projectService;
        private readonly IMessageService messageService;

        public JsonDataController(IUserService userService,
                                ISkillService skillService,
                                ITaskService subTaskService,
                                IProjectService projectService,
                                IMessageService messageService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.projectService = projectService;
            this.messageService = messageService;
        }

        public virtual JsonResult ReadMessages([DataSourceRequest]DataSourceRequest request)
        {
            var result = messageService.GetAll().Where(x => x.To.Id == UserProfile.Id).To<MessageViewModel>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllUsers([DataSourceRequest]DataSourceRequest request)
        {
            var users = userService.GetAll().Where(x => x.Id != UserProfile.Id).Select(x => new { id = x.Id, name = x.FirstName + " " + x.LastName });
            return Json(users, JsonRequestBehavior.AllowGet);
        }

    }
}