using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planex.Services.Skills;
using Planex.Services.SubTasks;
using Planex.Services.Users;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class HelperController : BaseController
    {

        private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ISubTaskService subTaskService;

        public HelperController(IUserService userService,
                                ISkillService skillService,
                                ISubTaskService subTaskService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.userService = userService;
        }

        //[ChildActionOnly]
        public JsonResult GetAllSkills()
        {
            var result = skillService.GetAll().Select(x => new { id = x.Id, skill = x.Name });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeUsersWithSkill(int? skillId)
        {
            if (skillId != null)
            {
                var users = userService.GetAll().Where(x => x.Skills.Any(s => s.Id == skillId));
                return Json(users.Select(u => new { id = u.Id, value = u.Email }), JsonRequestBehavior.AllowGet);
            }

            return Json("");
        }

        // [ChildActionOnly]
        public JsonResult GetAllSubTasks()
        {
            var result = subTaskService.GetAll().Select(x => new { id = x.Id, title = x.Title });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubTasksTreeView(int? id)
        {
            var result = subTaskService.GetAll().Where(x => x.ParentId == id).Select(s => 
                            new
                            {
                                id = s.Id,
                                title = s.Title,
                                hasChildren = s.Children.Any()
                            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}