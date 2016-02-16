using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Data.Models;
using Planex.Services.Skills;
using Planex.Services.SubTasks;
using Planex.Services.Tasks;
using Planex.Services.Users;
using Planex.Web.Areas.Lead.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Lead.Controllers
{
    public class HelperController : BaseController
    {

        private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ISubTaskService subTaskService;
        private readonly ITaskService taskService;

        public HelperController(IUserService userService,
                                ISkillService skillService,
                                ISubTaskService subTaskService,
                                ITaskService taskService
                                ) : base(userService)
        {
            this.skillService = skillService;
            this.subTaskService = subTaskService;
            this.userService = userService;
            this.taskService = taskService;
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
            var projectId = int.Parse(Session["mainTaskId"].ToString());
            var result = subTaskService.GetAll().Where(s => s.MainTaskId == projectId && s.ParentId == null).Select(x => new { id = x.Id, title = x.Title });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubTasksTreeView(int? id)
        {
            var projectId = int.Parse(Session["mainTaskId"].ToString());
            var hasChildrenDb = subTaskService.GetAll().Any(x => x.ParentId == id);
            var result = subTaskService.GetAll().Where(x => x.MainTaskId == projectId && x.ParentId == id).Select(s => 
                            new
                            {
                                id = s.Id,
                                title = s.Title,
                                start = s.Start,
                                end = s.End,
                                hasChildren = hasChildrenDb
                            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSubTasksDependency(int? id)
        {
            var projectId = int.Parse(Session["mainTaskId"].ToString());
            var result = subTaskService.GetAll().Where(x => x.MainTaskId == projectId && x.ParentId == id).Select(s =>
                            new
                            {
                                id = s.Id,
                                title = s.Title,
                            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProjectsIndex_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = taskService.GetAll().Where(x => x.LeadId == UserProfile.Id && x.State >= TaskStateType.Started).To<ProjectIndexViewController>();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}